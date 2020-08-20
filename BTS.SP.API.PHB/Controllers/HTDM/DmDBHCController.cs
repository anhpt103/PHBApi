using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Security.Claims;
using System.Web;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmDBHC;
using Repository.Pattern.UnitOfWork;
using AutoMapper;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmDBHC")]
    [Route("{id?}")]
    public class DmDBHCController : ApiController
    {
        private readonly IDmDBHCService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmDBHCController(IDmDBHCService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        [Route("GetSelectData")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                var list = new List<ChoiceObj>();
                var kq = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA_DBHC).ToList();
                if (kq.Count > 0)
                {
                    for (int i = 0; i < kq.Count; i++)
                    {
                        var tmp = new ChoiceObj();
                        tmp.Value = kq[i].MA_DBHC;
                        tmp.Text = kq[i].TEN_DBHC;
                        tmp.ExtendValue = kq[i].MA_DBHC_CHA;
                        tmp.Id = kq[i].ID;
                        list.Add(tmp);
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetDBHC_byMaCha/{macha}")]
        [HttpGet]
        public IList<ChoiceObj> GetDBHC_byMaCha(string macha)
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            try
            {
                var lst =  _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.MA_DBHC_CHA == macha).OrderBy(x => x.MA_DBHC)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_DBHC,
                        Text = x.TEN_DBHC,
                        ExtendValue = x.MA_DBHC_CHA.ToString(),
                        Id = x.ID
                    }).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
            }
        }

        [Route("GetDBHC_byMaDBHC/{ma_dbhc}")]
        [HttpGet]
        [Authorize]
        public IList<ChoiceObj> GetDBHC_byMaDBHC(string ma_dbhc)
        {
            try
            {
                var a = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.MA_DBHC == ma_dbhc).OrderBy(x => x.MA_DBHC)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_DBHC,
                        Text = x.TEN_DBHC,
                        Parent = string.IsNullOrEmpty(x.MA_DBHC_CHA.ToString()) ? "" : x.MA_DBHC_CHA,
                        Id = x.ID,
                        ExtendValue = x.MA_T
                    }).ToList();

                return a;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
            }
        }

        [Route("getMaH_byMaDBHC/{ma_dbhc}")]
        [HttpGet]
        public IList<ChoiceObj> getMaH_byMaDBHC(string ma_dbhc)
        {
            try
            {
                
                if (ma_dbhc == "08")
                {
                    var a = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.MA_DBHC == ma_dbhc).OrderBy(x => x.MA_DBHC)
                   .ToList().Select(x => new ChoiceObj()
                   {
                       Value = x.MA_T,
                       Text = x.TEN_DBHC,
                   }).ToList();
                    return a;

                }
                else
                {
                    var  a = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.MA_DBHC == ma_dbhc).OrderBy(x => x.MA_DBHC)
                 .ToList().Select(x => new ChoiceObj()
                 {
                     Value = x.MA_H,
                     Text = x.TEN_DBHC,
                 }).ToList();
                    return a;
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
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