using BTS.SP.API.PHB.ViewModels.REPORT.BM16_TT344;

using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BM16_TT344;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.REPORT.BM16_TT344;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using BTS.SP.TOOLS.BuildQuery.Types;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/Nv/DE_NGHI_THANH_TOAN")]
    [Route("{id?}")]
    public class DeNghiThanhToanController : ApiController
    {
        private readonly IPhbBm16TT344Service _formService;
        private readonly IPhbBm16TT344DetailService _detailService;
        private readonly IAuNguoiDungService _auService;
        private readonly IUnitOfWorkAsync _unitOfWork;


        public DeNghiThanhToanController(
           IPhbBm16TT344Service service,
           IPhbBm16TT344DetailService detailService,
           IAuNguoiDungService auService,
           IUnitOfWorkAsync unitOfWork)
        {
            _formService = service;
            _detailService = detailService;
            _auService = auService;
            _unitOfWork = unitOfWork;

        }

        [Route("paging")]
        [HttpPost]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_BM16_TT344>>();
            var postData = ((dynamic)jsonData);
            try
            {
                var filtered = ((JObject)postData.filtered).ToObject<FilterObj<PhbBm16TT344Vm.Search>>();
                var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_BM16_TT344>>();
                var query = new QueryBuilder
                {
                    Take = paged.itemsPerPage,
                    Skip = paged.fromItem - 1,
                    Filter = new QueryFilterLinQ()
                    {
                        Property = ClassHelper.GetProperty(() => new PHB_BM16_TT344().MA_BAOCAO_TU),
                        Method = FilterMethod.All,

                    }
                };
                var filterResult = await _formService.FilterAsync(filtered, query);
                if (!filterResult.WasSuccessful)
                {
                    if (filterResult.Logs.Count > 0)
                    {
                        foreach (var ex in filterResult.Logs)
                        {
                            WriteLogs.LogError(ex.Exception);
                        }
                    }
                    return NotFound();
                }
                result.Data = filterResult.Value;
                result.Error = false;
                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                WriteLogs.LogError(ex);
                return BadRequest();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return InternalServerError();
            }
        }


        [Route("AddNew")]
        [HttpPost]
        public IHttpActionResult AddNew(AddContent_ViewModel model)
        {
            var response = new Response();
            if (model.Details == null || model.Form == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            var formCout = _formService.Queryable().Where(x => x.MA_BAOCAO_TU == model.Form.MA_BAOCAO_TU).Count();

            if (formCout > 0)
            {
                response.Error = true;
                response.Message = "Đã Tồn Tại bản kê chứng từ";
                return Ok(response);
            }

            string nguoitao = "";
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                nguoitao = identity.Name;

            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            var form = new PHB_BM16_TT344()
            {
                MA_BAOCAO_TU = model.Form.MA_BAOCAO_TU,
                MA_XA = model.Form.MA_XA,
                NGUOI_TAO = nguoitao,
                NGAY_TAO = model.Form.NGAY_TAO,
                NGUOI_SUA = null,
                NGAY_SUA = null,
                TONG_TIEN = model.Form.TONG_TIEN,
                REFID = Guid.NewGuid().ToString("n"),
            };

            _formService.Insert(form);

            foreach (var item in model.Details)
            {
                var Detail = new PHB_BM16_TT344_DETAIL()
                {
                    PHB_BM16_TT344_REFID = form.REFID,
                    CHUONG = item.CHUONG,
                    LOAI = item.LOAI,
                    KHOAN = item.KHOAN,
                    MUC = item.MUC,
                    TIEUMUC = item.TIEUMUC,
                    SCTTU = item.SCTTU,
                    SOTIENTU = item.SOTIENTU,
                    SOTIENTT = item.SOTIENTT,
                    MA_KBNN = item.MA_KBNN,
                };
                _detailService.Insert(Detail);
            }
            try
            {

                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }


            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }

        [Route("GetDetail/{id}")]
        public IHttpActionResult GetDetail(string id)
        {
            var response = new Response<List<PHB_BM16_TT344_DETAIL>>();

            if (string.IsNullOrEmpty(id))
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {

                response.Data = _detailService.Queryable().Where(x => x.PHB_BM16_TT344_REFID == id).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }


            return Ok(response);
        }

        [Route("Edit")]
        [HttpPut]
        public IHttpActionResult Edit(EditContent_ViewModel model)
        {
            var response = new Response();
            var formOld = new PHB_BM16_TT344();
            var lstDetailOld = new List<PHB_BM16_TT344_DETAIL>();
            if (model.Details == null || model.Form == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {
                formOld = _formService.Queryable().FirstOrDefault(x => x.REFID == model.Form.REFID);
                lstDetailOld = _detailService.Queryable().Where(x => x.PHB_BM16_TT344_REFID == model.Form.REFID).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            //Xoa ca bang cha va bang con cu
            try
            {
                _formService.Delete(formOld);
                foreach (var detail in lstDetailOld)
                {
                    _detailService.Delete(detail);
                }
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            //Them moi 
            try
            {
                string nguoitao = "";

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                nguoitao = identity.Name;

                var form = new PHB_BM16_TT344()
                {
                    MA_BAOCAO_TU = model.Form.MA_BAOCAO_TU,
                    MA_XA = model.Form.MA_XA,
                    NGUOI_TAO = nguoitao,
                    NGAY_TAO = model.Form.NGAY_TAO,
                    NGUOI_SUA = null,
                    NGAY_SUA = null,
                    TONG_TIEN = model.Form.TONG_TIEN,
                    REFID = Guid.NewGuid().ToString("n"),
                };

                _formService.Insert(form);

                foreach (var item in model.Details)
                {
                    var Detail = new PHB_BM16_TT344_DETAIL()
                    {
                        PHB_BM16_TT344_REFID = form.REFID,
                        CHUONG = item.CHUONG,
                        LOAI = item.LOAI,
                        KHOAN = item.KHOAN,
                        MUC = item.MUC,
                        TIEUMUC = item.TIEUMUC,
                        SCTTU = item.SCTTU,
                        SOTIENTU = item.SOTIENTU,
                        SOTIENTT = item.SOTIENTT,
                        MA_KBNN = item.MA_KBNN,
                    };
                    _detailService.Insert(Detail);
                }
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }
        [Route("Delete/{id}")]
        [HttpGet]
        public IHttpActionResult Delete(string id)
        {
            var response = new Response();
            var formOld = new PHB_BM16_TT344();
            var lstDetailOld = new List<PHB_BM16_TT344_DETAIL>();
            if (string.IsNullOrEmpty(id))
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {
                formOld = _formService.Queryable().FirstOrDefault(x => x.REFID == id);
                lstDetailOld = _detailService.Queryable().Where(x => x.PHB_BM16_TT344_REFID == id).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            //Xoa ca bang cha va bang con cu
            try
            {
                _formService.Delete(formOld);
                foreach (var detail in lstDetailOld)
                {
                    _detailService.Delete(detail);
                }
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            response.Message = "Xoá Dữ Liệu Thành Công";
            return Ok(response);

        }

    }
}