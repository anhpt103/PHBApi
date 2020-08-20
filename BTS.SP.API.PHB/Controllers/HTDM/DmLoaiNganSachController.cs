using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiNganSach;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmLoaiNganSach")]
    [Route("{id?}")]
    public class DmLoaiNganSachController:ApiController
    {
        private readonly IDmLoaiNganSachService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmLoaiNganSachController(IDmLoaiNganSachService service,IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmLoaiNganSach")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_LOAINGANSACH>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmLoaiNganSachVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_LOAINGANSACH>>();
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
            return _service.Queryable().Where(x=>x.TRANG_THAI.Equals("A")).OrderBy(x=>x.MA_LOAINS).Select(y => new ChoiceObj()
            {
                Value = y.MA_LOAINS,
                Text = y.TEN_LOAINS,
                Parent = y.MA_CHA,
                ExtendValue = y.TEN_MORONG
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