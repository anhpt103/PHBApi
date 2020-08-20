using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmCtMucTieu;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmCtMucTieu")]
    [Route("{id?}")]
    public class DmCtMucTieuController : ApiController
    {
        private readonly IDmCtMucTieuService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmCtMucTieuController(IDmCtMucTieuService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmCtMucTieu")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<DM_CTMUCTIEU>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmCtMucTieuVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<DM_CTMUCTIEU>>();
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return InternalServerError();
            }
        }

        [Route("GetSelectData")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).Select(x => new ChoiceObj()
                {
                    Value = x.MA,
                    Text = x.TEN,
                    Parent = x.MA_CTMUCTIEU_CHA,
                    ExtendValue = x.TEN_RUTGON
                }).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
            }
        }

        [HttpPost]
        [CustomAuthorize(Method = "THEM", State = "phb_dmCtMucTieu")]
        public async Task<IHttpActionResult> Post(DM_CTMUCTIEU model)
        {
            Response<DM_CTMUCTIEU> response = new Response<DM_CTMUCTIEU>();
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
        [CustomAuthorize(Method = "SUA", State = "phb_dmCtMucTieu")]
        public async Task<IHttpActionResult> Put(string id, DM_CTMUCTIEU model)
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
        [CustomAuthorize(Method = "XOA", State = "phb_dmCtMucTieu")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                Response<string> response = new Response<string>();
                DM_CTMUCTIEU item = await _service.FindByIdAsync(long.Parse(id));
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

        [Route("SelectConfig")]
        [HttpPost]
        public async Task<IHttpActionResult> SelectConfig(JObject jsonData)
        {
            var result = new Response<PagedObj<DM_CTMUCTIEU>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmCtMucTieuVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<DM_CTMUCTIEU>>();
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
