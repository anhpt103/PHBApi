using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Types;
using System.Linq;
using BTS.SP.PHB.SERVICE.Helper;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmLoaiKhoan")]
    [Route("{id?}")]
    public class DmLoaiKhoanController:ApiController
    {
        private readonly IDmLoaiKhoanService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmLoaiKhoanController(IDmLoaiKhoanService service,IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmLoaiKhoan")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_LOAIKHOAN>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmLoaiKhoanVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_LOAIKHOAN>>();
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
                WriteLogs.LogError(e);
                return InternalServerError();
            }
        }
        [Route("Select_MultiQuery")]
        [CustomAuthorize(Method = "XEM", State = "phb_dmLoaiKhoan")]
        public async Task<IHttpActionResult> Select_MultiQuery(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmLoaiKhoanVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_LOAIKHOAN>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
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
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [Route("GetSelectLoai")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSelectLoai(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_LOAIKHOAN>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmLoaiKhoanVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_LOAIKHOAN>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
                Filter = new QueryFilterLinQ()
                {
                    Method = FilterMethod.EqualTo,
                    Property = SP.PHB.SERVICE.Helper.ClassHelper.GetProperty(() => new PHB_DM_LOAIKHOAN().MA_CHA),
                    Value = null
                }
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return InternalServerError();
            }
        }

        [Route("GetSelectKhoan/{maLoai}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectKhoan(string maLoai)
        {
            if (string.IsNullOrEmpty(maLoai)) return BadRequest();
            Response<List<PHB_DM_LOAIKHOAN>> response = new Response<List<PHB_DM_LOAIKHOAN>>();
            try
            {
                response.Error = false;
                response.Data = await _service.Queryable().Where(x => x.MA== maLoai).OrderBy(x => x.MA).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("GetSelectData")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSelectData(DmLoaiKhoanVm.LoaiItem key)
        {
            try
            {
                var result = new Response<DmLoaiKhoanVm.ResponseData>();                
                var lstLoai = new List<DmLoaiKhoanVm.LoaiItem>();
                var lstKhoan = new List<DmLoaiKhoanVm.KhoanItem>();
                var data = _service.GetAll();
                lstLoai = data.Where(x => string.IsNullOrEmpty(x.MA_CHA)).Select(x => new DmLoaiKhoanVm.LoaiItem() { MA_LOAI = x.MA, TEN_LOAI = x.TEN }).OrderBy(x=>x.MA_LOAI).ToList();                
                lstKhoan = data.Where(x => !string.IsNullOrEmpty(x.MA_CHA) && x.MA_CHA.Equals(key.MA_LOAI)).Select(x => new DmLoaiKhoanVm.KhoanItem() { MA_KHOAN = x.MA, TEN_KHOAN = x.TEN, MA_LOAI = x.MA_CHA }).OrderBy(x=>x.MA_KHOAN).ToList();    
                var res = new DmLoaiKhoanVm.ResponseData() { lstKhoan = lstKhoan, lstLoai = lstLoai };
                result.Data = res;
                result.Error = false;
                return Ok(result);
            }
            catch (Exception e)
            {                
                return BadRequest();
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