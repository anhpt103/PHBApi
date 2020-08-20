using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiNganSach;
using BTS.SP.PHB.SERVICE.HTDM.DmNhomMucChi;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmNhomMucChi")]
    [Route("{id?}")]
    public class DmNhomMucChiController:ApiController
    {
        private readonly IDmNhomMucChiService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmNhomMucChiController(IDmNhomMucChiService service,IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmNhomMucChi")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_NHOMMUCCHI>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmNhomMucChiVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_NHOMMUCCHI>>();
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
            return _service.Queryable().Where(x=>x.TRANG_THAI).OrderBy(x=>x.MA_NHOMMC).Select(y => new ChoiceObj()
            {
                Value = y.MA_NHOMMC,
                Text = y.TEN_NHOMMC,
            }).ToList(); ;
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