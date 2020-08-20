using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.ENTITY.Sys;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmDVQHNS")]
    [Route("{id?}")]
    public class DmDVQHNSController:ApiController
    {
        private readonly IDmDVQHNSService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmDVQHNSController(IDmDVQHNSService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM",State = "phb_dmDVQHNS")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_DVQHNS>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmDVQHNSVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_DVQHNS>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1
            };
            try
            {
                var filterResult = await _service.FilterAsync(filtered, query);
                if (!filterResult.WasSuccessful)
                {
                    return NotFound();
                }
                result.Data = filterResult.Value;
                result.Error = false;
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [Route("GetSelectData")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                return _service.Queryable().Where(x=>x.TRANG_THAI.Equals("A") && x.DON_VI_TONG_HOP==0).Select(x => new ChoiceObj()
                {
                    Value = x.MA_QHNS,
                    Text = x.TEN_QHNS,
                    Parent = x.MA_CHA,
                    ExtendValue = x.MA_CHUONG
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [Route("GetSelectData_By_DBHC")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData_By_DBHC()
        {
            var claimDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.DON_VI_TONG_HOP == 0 && x.MA_DBHC == claimDbhc.Value).Select(x => new ChoiceObj()
                {
                    Value = x.MA_QHNS,
                    Text = x.TEN_QHNS,
                    Parent = x.MA_CHA,
                    ExtendValue = x.MA_CHUONG
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("getListDataByChuong/{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> getListDataByChuong(string code)
        {
            var result = new SP.PHB.SERVICE.Helper.TransferObj();
            var instance = _unitOfWorkAsync.Repository<SYS_DVQHNS>().Queryable().Where(x => x.MA_CHUONG == code && x.MA_DVQHNS.StartsWith("1")).ToList();
            if (instance.Count == 0)
            {
                result.Data = null;
                result.Status = false;
            }
            else
            {
                result.Data = instance;
                result.Status = true;
            }
            return Ok(result);
        }

        [Route("GetSelectData_by_MaChuong/{machuong}")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData_by_MaChuong(string machuong)
        {
            try
            {
                return _unitOfWorkAsync.Repository<SYS_DVQHNS>().Queryable().Where(x => x.MA_CHUONG == machuong && x.MA_DVQHNS.StartsWith("1"))
                    .Select(x => new ChoiceObj()
                    {
                        Value = x.MA_DVQHNS,
                        Text = x.TEN_DVQHNS,
                        ExtendValue = x.MA_CHUONG
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetByMaQhns/{maQhns}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByMaQhns(string maQhns)
        {
            var claimLoaiUser = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("LOAIUSER"));
            if (claimLoaiUser == null) return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
            if(string.IsNullOrEmpty(claimLoaiUser.Value)) return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
            if (claimLoaiUser.Value.Equals("1"))
            {
                //user theo DBHC
                var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
                if (claimMaDbhc == null) return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                if (string.IsNullOrEmpty(claimMaDbhc.Value)) return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                try
                {
                    return Ok(await _service.Queryable()
                        .Where(x => x.MA_DBHC.Equals(claimMaDbhc.Value) && x.MA_QHNS.StartsWith(maQhns)).OrderBy(x=>x.MA_QHNS).ToListAsync());
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                }
            }
            else
            {
                //user theo QHNS
                var claimMaQhnsQl = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_QHNS_QL"));
                if (claimMaQhnsQl == null) return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                if (string.IsNullOrEmpty(claimMaQhnsQl.Value)) return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                try
                {
                    return Ok(await _service.Queryable()
                        .Where(x => x.MA_QHNS_DVQL.Equals(claimMaQhnsQl.Value) && x.MA_QHNS.StartsWith(maQhns)).OrderBy(x=>x.MA_QHNS).ToListAsync());
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}