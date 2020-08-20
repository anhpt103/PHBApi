using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmTaiKhoan;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmTaiKhoan")]
    [Route("{id?}")]
    public class DmTaiKhoanController:ApiController
    {
        private readonly IDmTaiKhoanService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmTaiKhoanController(IDmTaiKhoanService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmTaiKhoan")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_TAIKHOAN>>();
            var postData = (dynamic)jsonData;
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmTaiKhoanVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_DUAN>>();
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
            return _service.Queryable().Where(x=>x.TRANG_THAI.Equals("A")).OrderBy(x=>x.MA_TAI_KHOAN).Select(x => new ChoiceObj()
            {
                Value = x.MA_TAI_KHOAN,
                Text = x.TEN_TAI_KHOAN,
                Parent = x.MA_CHA,
                ExtendValue = x.MO_TA
            }).ToList();
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