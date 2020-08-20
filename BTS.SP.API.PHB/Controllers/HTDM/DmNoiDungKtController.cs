using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmNoiDungKt;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using BTS.SP.TOOLS.BuildQuery.Types;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.SERVICE.Helper;
//using BTS.SP.PHB.SERVICE.Helper;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmNoiDungKt")]
    [Route("{id?}")]
    public class DmNoiDungKtController:ApiController
    {
        private readonly IDmNoiDungKtService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DmNoiDungKtController(IDmNoiDungKtService service,IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmNoiDungKt")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_NOIDUNGKT>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmNoiDungKtVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_NOIDUNGKT>>();
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
        public IList<SP.PHB.ENTITY.Helper.ChoiceObj> GetSelectData()
        {
            return _service.Queryable().Where(x=>x.TRANG_THAI).OrderBy(x=>x.MA).Select(y => new SP.PHB.ENTITY.Helper.ChoiceObj()
            {
                Value = y.MA,
                Text = y.TEN,
                Parent = y.MA_CHA,
                ExtendValue = y.MA_NHOMMC
            }).ToList(); ;
        }
        [Route("Select_MultiQuery")]
        [CustomAuthorize(Method = "XEM", State = "phb_dmNoiDungKt")]
        public async Task<IHttpActionResult> Select_MultiQuery(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmNoiDungKtVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_NOIDUNGKT>>();
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
        [Route("GetSelectMuc")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSelectMuc(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_NOIDUNGKT>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmNoiDungKtVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_NOIDUNGKT>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
                Filter = new QueryFilterLinQ()
                {
                    Method = FilterMethod.Or,
                    SubFilters =new List<IQueryFilter>()
                    {
                        new QueryFilterLinQ()
                        {
                            Method = FilterMethod.And,
                            SubFilters = new List<IQueryFilter>()
                            {
                                new QueryFilterLinQ()
                                {
                                    Property = SP.PHB.SERVICE.Helper.ClassHelper.GetProperty(()=>new PHB_DM_NOIDUNGKT().MA),
                                    Method = FilterMethod.GreaterThanOrEqualTo,
                                    Value = "0001"
                                },
                                new QueryFilterLinQ()
                                {
                                    Property = SP.PHB.SERVICE.Helper.ClassHelper.GetProperty(()=>new PHB_DM_NOIDUNGKT().MA),
                                    Method = FilterMethod.LessThanOrEqualTo,
                                    Value = "0099"
                                }
                            }
                        },
                        new QueryFilterLinQ() 
                        {
                            Method = FilterMethod.And,
                            SubFilters = new List<IQueryFilter>()
                            {
                                new QueryFilterLinQ()
                                {
                                    Property = SP.PHB.SERVICE.Helper.ClassHelper.GetProperty(()=>new PHB_DM_NOIDUNGKT().MA),
                                    Method = FilterMethod.GreaterThanOrEqualTo,
                                    Value = "0800"
                                },
                                new QueryFilterLinQ()
                                {
                                    Property = SP.PHB.SERVICE.Helper.ClassHelper.GetProperty(()=>new PHB_DM_NOIDUNGKT().MA),
                                    Method = FilterMethod.LessThanOrEqualTo,
                                    Value = "9989" 
                                },
                                new QueryFilterLinQ()
                                {
                                    Property = SP.PHB.SERVICE.Helper.ClassHelper.GetProperty(()=>new PHB_DM_NOIDUNGKT().LA_CHITIET),
                                    Method = FilterMethod.EqualTo,
                                    Value = 0
                                }
                            }
                        }
                    }
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
            catch (Exception e)
            {
                return InternalServerError();
            }
            //Response<List<ChoiceObj>> response =new Response<List<ChoiceObj>>();
            //try
            //{
            //    response.Error = false;
            //    response.Data = await _service.Queryable()
            //        .Where(x => (String.Compare(x.MA, "0001", StringComparison.Ordinal) >= 0 && String.Compare(x.MA, "0099", StringComparison.Ordinal) <= 0) ||
            //                    (String.Compare(x.MA, "0800", StringComparison.Ordinal) >= 0 && String.Compare(x.MA, "9989", StringComparison.Ordinal) <= 0 && x.LA_CHITIET==0)
            //        )
            //        .OrderBy(x => x.MA).Select(
            //            x => new ChoiceObj()
            //            {
            //                Value = x.MA,
            //                Text = x.TEN,
            //                Parent = x.MA_CHA,
            //                ExtendValue = x.MA_NHOMMC
            //            }).ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    WriteLogs.LogError(ex);
            //    response.Error = true;
            //    response.Message = ErrorMessage.ERROR_SYSTEM;
            //}
            //return Ok(response);
        }

        [Route("GetSelectTieuMuc/{maMuc}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectTieuMuc(string maMuc)
        {
            if (string.IsNullOrEmpty(maMuc)) return BadRequest();
            Response<List<PHB_DM_NOIDUNGKT>> response =new Response<List<PHB_DM_NOIDUNGKT>>();
            try
            {
                response.Error = false;
                response.Data = await _service.Queryable().Where(x => x.MA == maMuc).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
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