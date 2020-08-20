using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Repository.Pattern.UnitOfWork;
using AutoMapper;
using System.Linq;
using BTS.SP.PHB.SERVICE.BuildQuery.Types;
using BTS.SP.PHB.SERVICE.BuildQuery;
using BTS.SP.PHB.SERVICE.Helper;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmChuong")]
    [Route("{id?}")]
    public class DmChuongController:ApiController
    {
        private readonly IDmChuongService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmChuongController(IDmChuongService service, IUnitOfWorkAsync unitOfWorkAsync)
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
                var kq = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA_CHUONG).ToList();
                if (kq.Count > 0)
                {
                    for (int i = 0; i < kq.Count; i++)
                    {
                        var tmp = new SP.PHB.ENTITY.Helper.ChoiceObj();
                        tmp.Value = kq[i].MA_CHUONG;
                        tmp.Text = kq[i].TEN_CHUONG;
                        tmp.ExtendValue = kq[i].MA_CAP;
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
        [Route("Select_MultiQuery")]
        [CustomAuthorize(Method = "XEM", State = "phb_dmChuong")]
        public async Task<IHttpActionResult> Select_MultiQuery(JObject jsonData)
        {
            var result = new TransferObj(); 
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmChuongVm.Search>>();
            var paged = ((JObject) postData.paged).ToObject<PagedObj<DM_CHUONG>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
            };
            try
            {
                var filterResult = _service.Filter(filtered, query);
                filterResult.Value.currentPage = paged.currentPage;
                filterResult.Value.itemsPerPage = paged.itemsPerPage;
                if (!filterResult.WasSuccessful)
                {
                    return NotFound();
                }

                result.Data = filterResult.Value;
                result.Status = true;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmChuong")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<DM_CHUONG>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmChuongVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<DM_CHUONG>>();
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

        [Route("ExportExcel")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ExportReport()
        {
            try
            {
                List<DM_CHUONG> lstData =await _service.Queryable().ToListAsync();
                ExcelPackage excelPackage = CommonService.ExportData("C:\\LogsStc\\1.xlsx", lstData);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "export_DmChuong.xlsx"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = ResponseMessage(result);
                return response;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return InternalServerError();
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