using BTS.SP.PHB.SERVICE.HTDM.DmCoQuanThu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.PHB.ENTITY.Dm;
using Newtonsoft.Json.Linq;
using System.Web.Http.Description;
using OfficeOpenXml;
using BTS.SP.PHB.SERVICE.HTDM;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using AutoMapper;
using BTS.SP.PHB.ENTITY.Helper;
using Repository.Pattern.Infrastructure;
using BTS.SP.PHB.ENTITY;
using Repository.Pattern.UnitOfWork;
using BTS.SP.TOOLS.BuildQuery.Result;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.PHB.SERVICE.Helper;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/Dm/PHB_DM_COQUANTHU")]
    [Route("{id?}")]
    [Authorize]
    public class DmCoQuanThuController : ApiController
    {
        public readonly IDmCoQuanThuService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public DmCoQuanThuController(DmCoQuanThuService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        [Route("GetSelectData")]
        [HttpGet]
        public IList<SP.PHB.ENTITY.Helper.ChoiceObj> GetSelectData()
        {
            try
            {
                var list = new List<SP.PHB.ENTITY.Helper.ChoiceObj>();
                var kq = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA).ToList();
                if (kq.Count > 0)
                {
                    for (int i = 0; i < kq.Count; i++)
                    {
                        var tmp = new SP.PHB.ENTITY.Helper.ChoiceObj();
                        tmp.Value = kq[i].MA;
                        tmp.Text = kq[i].TEN;
                        tmp.ExtendValue = kq[i].TEN;
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
        [Route("Select_Page")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmCoQuanThu")]
        public async Task<IHttpActionResult> Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmCoQuanThuVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_COQUANTHU>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
                Filter = new QueryFilterLinQ()
                {
                    Property = SP.PHB.SERVICE.UTILS.ClassHelper.GetProperty(() => new PHB_DM_COQUANTHU().MA),
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
        [ResponseType(typeof(PHB_DM_COQUANTHU))]
        public async Task<IHttpActionResult> AddNew(PHB_DM_COQUANTHU model)
        {
            Response<PHB_DM_COQUANTHU> response = new Response<PHB_DM_COQUANTHU>();
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
        public async Task<IHttpActionResult> Update(string id, PHB_DM_COQUANTHU model)
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
        [ResponseType(typeof(PHB_DM_COQUANTHU))]
        [HttpPut]
        [Route("DeleteItem/{id}")]
        public async Task<IHttpActionResult> DeleteItem(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                Response<string> response = new Response<string>();
                PHB_DM_COQUANTHU item = await _service.FindByIdAsync(long.Parse(id));
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
                List<PHB_DM_COQUANTHU> lstData = _service.GetAll().ToList();
                DateTime now = DateTime.Now;
                string date = now.ToString("dd-MM-yyyy");
                var filename = "Export_PHB_DM_COQUANTHU_(" + date + ")_TM" + now.Ticks + ".xls";
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