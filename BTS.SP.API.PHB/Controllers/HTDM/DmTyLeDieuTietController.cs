﻿using AutoMapper;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM;
using BTS.SP.PHB.SERVICE.HTDM.DmTyLeDieuTiet;
using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using BTS.SP.TOOLS.BuildQuery.Types;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/Dm/PHB_DM_TYLEDIEUTIET")]
    [Route("{id?}")]
    [Authorize]
    public class DmTyLeDieuTietController : ApiController
    {
        public readonly IDmTyLeDieuTietService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public DmTyLeDieuTietController(DmTyLeDieuTietService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        [Route("GetAll_Muc")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll_Muc()
        {
            var result = new TransferObj<IList<SP.PHB.ENTITY.Helper.ChoiceObj>>();
            try
            {
                var repository = _service.GetAll().ToList();
                var listData = Mapper.Map<List<PHB_DM_TYLEDIEUTIET>, List<SP.PHB.ENTITY.Helper.ChoiceObj>>(repository);
                if (listData.Count > 0)
                {
                    result.Status = true;
                    result.Data = listData;
                }
                else
                {
                    result.Status = false;
                    result.Data = null;
                }

            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
                result.Data = null;
            }
            return Ok(result);
        }
        [Route("Select_Page")]
        [HttpPost]
        public async Task<IHttpActionResult> Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmTyLeDieuTietVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_TYLEDIEUTIET>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
                Filter = new QueryFilterLinQ()
                {
                    Property = SP.PHB.SERVICE.UTILS.ClassHelper.GetProperty(() => new PHB_DM_TYLEDIEUTIET().MA),
                    Method = FilterMethod.EqualTo,
                    Value = filtered.AdvanceData.MA
                },
                Orders = new List<IQueryOrder>()
                {
                    new QueryOrder()
                    {
                        Field ="MA",
                        Method = OrderMethod.ASC
                    }
                }
            };
            try
            {
                var filterResult = _service.Filter(filtered, query);
                if (!filterResult.WasSuccessful)
                {
                    return NotFound();
                }
                result.Data = filterResult.Value;
                result.Status = true;
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [Route("AddNew")]
        [ResponseType(typeof(PHB_DM_TYLEDIEUTIET))]
        public async Task<IHttpActionResult> AddNew(PHB_DM_TYLEDIEUTIET model)
        {
            Response<PHB_DM_TYLEDIEUTIET> response = new Response<PHB_DM_TYLEDIEUTIET>();
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

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IHttpActionResult> Update(string id, PHB_DM_TYLEDIEUTIET model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (int.Parse(id) <= 0 || !id.Equals(model.ID.ToString())) return BadRequest();
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
        [ResponseType(typeof(PHB_DM_TYLEDIEUTIET))]
        [HttpPut]
        [Route("DeleteItem/{id}")]
        public async Task<IHttpActionResult> DeleteItem(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                Response<string> response = new Response<string>();
                PHB_DM_TYLEDIEUTIET item = await _service.FindByIdAsync(long.Parse(id));
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

        [Route("ExportExcel")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ExportExcel()
        {
            try
            {
                List<PHB_DM_TYLEDIEUTIET> lstData = _service.GetAll().ToList();
                DateTime now = DateTime.Now;
                string date = now.ToString("dd-MM-yyyy");
                var filename = "Export_PHB_DM_TYLEDIEUTIET_(" + date + ")_TM" + now.Ticks + ".xls";
                var urlFile = HttpContext.Current.Server.MapPath("~/UploadFile/ExcelTemp/") + filename;
                ExcelPackage excelPackage = CommonService.ExportData(urlFile, lstData);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(new FileStream(urlFile, FileMode.Open, FileAccess.Read))
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filename
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = ResponseMessage(result);
                return response;
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}