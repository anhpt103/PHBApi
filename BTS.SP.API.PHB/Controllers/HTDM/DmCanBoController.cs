using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmCanBo;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System.Web;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using System.IO;
using System.Xml.Linq;
using BTS.SP.PHB.ENTITY;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Globalization;
using BTS.SP.API.PHB.Services;
using BTS.SP.API.PHB.ViewModels;
using Repository.Pattern.Infrastructure;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmCanBo")]
    [Route("{id?}")]
    public class DmCanBoController : ApiController
    {
        private readonly IDmCanBoService _service;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public DmCanBoController(IDmCanBoService service, IUnitOfWorkAsync unitOfWorkAsync, IDmDVQHNSService serviceDMDVQHNS)
        {
            _service = service;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmCanBo")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_CANBO>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmCanBoVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_CANBO>>();
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

        [HttpPost]
        [CustomAuthorize(Method = "THEM", State = "phb_dmCanBo")]
        public async Task<IHttpActionResult> Post(PHB_DM_CANBO model)
        {
            Response<PHB_DM_CANBO> response = new Response<PHB_DM_CANBO>();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ObjectState = ObjectState.Added;
                    _service.Insert(model);
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = "Cập nhật thành công.";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Lỗi cập nhật dữ liệu.";
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ex.Message;
                }

                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [CustomAuthorize(Method = "SUA", State = "phb_dmCanBo")]
        public async Task<IHttpActionResult> Put(string id, PHB_DM_CANBO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(id) || !id.Equals(model.ID.ToString())) return BadRequest();
            model.ObjectState = ObjectState.Modified;
            _service.Update(model);
            Response<string> response = new Response<string>();
            try
            {
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    response.Error = false;
                    response.Message = "Cập nhật thành công.";
                }
                else
                {
                    response.Error = true;
                    response.Message = "Lỗi cập nhật dữ liệu.";
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        [CustomAuthorize(Method = "XOA", State = "phb_dmCanBo")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                Response<string> response = new Response<string>();
                PHB_DM_CANBO item = await _service.FindByIdAsync(long.Parse(id));
                if (item == null) return NotFound();
                item.ObjectState = ObjectState.Deleted;
                _service.Delete(item);
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    response.Error = false;
                    response.Message = "Xóa thành công.";
                }
                else
                {
                    response.Error = true;
                    response.Message = "Lỗi cập nhật dữ liệu.";
                }
                return Ok(response);
            }
            catch (FormatException ex)
            {
                WriteLogs.LogError(ex);
                return BadRequest();
            }
        }

        [Route("GetSelectData")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA_CAN_BO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_CAN_BO,
                        Text = x.TEN_CAN_BO,
                        ReportValue = x.HE_SO_LUONG,
                        ExtendValue = x.CHUC_VU,
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[Route("GetSelectDataChucDanh")]
        //[HttpGet]
        //public IList<ChoiceObj> GetSelectDataChucDanh()
        //{
        //    try
        //    {
        //        return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA_CAN_BO)
        //            .ToList().Select(x => new ChoiceObj()
        //            {
        //                Value = x.MA_CAN_BO,
        //                Text = x.CHUC_VU,
        //                //ExtendValue = x.TEMPLATE.ToString(),
        //                Id = x.ID
        //            }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //[Route("GetSelectDataHSL")]
        //[HttpGet]
        //public IList<ChoiceObj> GetSelectDataHSL()
        //{
        //    try
        //    {
        //        return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA_CAN_BO)
        //            .ToList().Select(x => new ChoiceObj()
        //            {
        //                Value = x.MA_CAN_BO,
        //                Text = x.TEN_CAN_BO,
        //                ReportValue = x.HE_SO_LUONG,
        //                Id = x.ID
        //            }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

    }
}