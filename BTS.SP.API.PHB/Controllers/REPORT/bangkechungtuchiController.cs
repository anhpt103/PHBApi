using BTS.SP.API.PHB.ViewModels.REPORT.BANG_KE_CHUNG_TU_CHI;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.REPORT.BM14_TT134;
using BTS.SP.PHB.SERVICE.REPORT.PHB_BM14_TT134;
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
    [RoutePrefix("api/Nv/BANG_KE_CHUNG_TU_CHI")]
    [Route("{id?}")]
    public class bangkechungtuchiController : ApiController
    {
        private readonly IkekhaichungtuService _formService;
        private readonly IkekhaichungtuDetailService _detailService;
        private readonly IAuNguoiDungService _auService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly string MA_KTC = "";

        public bangkechungtuchiController(
           IkekhaichungtuService service,
           IkekhaichungtuDetailService detailService,
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
            var result = new Response<PagedObj<KEKHAICHUNGTU>>();
            var postData = ((dynamic)jsonData);
            try
            {
                var filtered = ((JObject)postData.filtered).ToObject<FilterObj<kekhaichungtuVm.Search>>();
                var paged = ((JObject)postData.paged).ToObject<PagedObj<KEKHAICHUNGTU>>();
                var query = new QueryBuilder
                {
                    Take = paged.itemsPerPage,
                    Skip = paged.fromItem - 1,
                    Filter = new QueryFilterLinQ()
                    {
                        Property = ClassHelper.GetProperty(() => new KEKHAICHUNGTU().MA_KTC),
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

            var formCout = _formService.Queryable().Where(x => x.MA_KTC == model.Form.MA_KTC).Count();

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

            var form = new KEKHAICHUNGTU()
            {
                MA_KTC = model.Form.MA_KTC,
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
                var Detail = new KEKHAICHUNGTUDETAIL()
                {
                    KEKHAICHUNGTUREFID = form.REFID,
                    SO_CTU = item.SO_CTU,
                    NGAY_THANG = item.NGAY_THANG,
                    NOIDUNG = item.NOIDUNG,
                    SO_TIEN = item.SO_TIEN,
                };
                _detailService.Insert(Detail);
            }
            try
            {
              
                 _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
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
            var response = new Response<List<KEKHAICHUNGTUDETAIL>>();
           
            if (string.IsNullOrEmpty(id))
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {

                response.Data = _detailService.Queryable().Where(x => x.KEKHAICHUNGTUREFID == id).ToList();
            }
            catch(Exception ex)
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
            var formOld = new KEKHAICHUNGTU();
            var lstDetailOld = new List<KEKHAICHUNGTUDETAIL>();
            if (model.Details == null || model.Form == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {
                formOld = _formService.Queryable().FirstOrDefault(x => x.REFID == model.Form.REFID);
                lstDetailOld = _detailService.Queryable().Where(x => x.KEKHAICHUNGTUREFID == model.Form.REFID).ToList();
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
                foreach(var detail in lstDetailOld)
                {
                    _detailService.Delete(detail);
                }
                _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
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

                var form = new KEKHAICHUNGTU()
                {
                    MA_KTC = model.Form.MA_KTC,
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
                    var Detail = new KEKHAICHUNGTUDETAIL()
                    {
                        KEKHAICHUNGTUREFID = form.REFID,
                        SO_CTU = item.SO_CTU,
                        NGAY_THANG = item.NGAY_THANG,
                        NOIDUNG = item.NOIDUNG,
                        SO_TIEN = item.SO_TIEN,
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
            var formOld = new KEKHAICHUNGTU();
            var lstDetailOld = new List<KEKHAICHUNGTUDETAIL>();
            if (string.IsNullOrEmpty(id))
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {
                formOld = _formService.Queryable().FirstOrDefault(x => x.REFID == id);
                lstDetailOld = _detailService.Queryable().Where(x => x.KEKHAICHUNGTUREFID == id).ToList();
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

        [Route("NumberToText/{num}")]
        [HttpGet]
        public async Task<IHttpActionResult> NumberToText(string num)
        {
            if (string.IsNullOrEmpty(num)) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                decimal tmp = decimal.Parse(num);
                var t = ConvertSoThanhChu.ChuyenDoiSoThanhChu(tmp);
                response.Data = t;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        public class ConvertSoThanhChu
        {
            private static string Chu(string gNumber)
            {
                string result = "";
                switch (gNumber)
                {
                    case "0":
                        result = "không";
                        break;
                    case "1":
                        result = "một";
                        break;
                    case "2":
                        result = "hai";
                        break;
                    case "3":
                        result = "ba";
                        break;
                    case "4":
                        result = "bốn";
                        break;
                    case "5":
                        result = "năm";
                        break;
                    case "6":
                        result = "sáu";
                        break;
                    case "7":
                        result = "bảy";
                        break;
                    case "8":
                        result = "tám";
                        break;
                    case "9":
                        result = "chín";
                        break;
                }
                return result;
            }
            private static string Donvi(string so)
            {
                string Kdonvi = "";

                if (so.Equals("1"))
                    Kdonvi = "";
                if (so.Equals("2"))
                    Kdonvi = "nghìn";
                if (so.Equals("3"))
                    Kdonvi = "triệu";
                if (so.Equals("4"))
                    Kdonvi = "tỷ";
                if (so.Equals("5"))
                    Kdonvi = "nghìn tỷ";
                if (so.Equals("6"))
                    Kdonvi = "triệu tỷ";
                if (so.Equals("7"))
                    Kdonvi = "tỷ tỷ";

                return Kdonvi;
            }
            private static string Tach(string tach3)
            {
                string Ktach = "";
                if (tach3.Equals("000"))
                    return "";
                if (tach3.Length == 3)
                {
                    string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                    string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                    string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                    if (tr.Equals("0") && ch.Equals("0"))
                        Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                    if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                        Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                    if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                        Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                    if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                        Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                    if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                        Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                    if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                        Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                    if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                        Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                    if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                        Ktach = " không trăm mười ";
                    if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                        Ktach = " không trăm mười lăm ";
                    if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                        Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                    if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                        Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                    if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                        Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                    if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                        Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";

                    if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                        Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                    if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                        Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
                }
                return Ktach;
            }
            public static string ChuyenDoiSoThanhChu(decimal gNum)
            {
                if (gNum == 0)
                    return "Không đồng";

                string lso_chu = "";
                string tach_mod = "";
                string tach_conlai = "";
                decimal Num = Math.Round(gNum, 0);
                string gN = Convert.ToString(Num);
                int m = Convert.ToInt32(gN.Length / 3);
                int mod = gN.Length - m * 3;
                string dau = "[+]";

                // Dau [+ , - ]
                if (gNum < 0)
                    dau = "[-]";
                dau = "";

                // Tach hang lon nhat
                if (mod.Equals(1))
                    tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
                if (mod.Equals(2))
                    tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
                if (mod.Equals(0))
                    tach_mod = "000";
                // Tach hang con lai sau mod :
                if (Num.ToString().Length > 2)
                    tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();

                ///don vi hang mod
                int im = m + 1;
                if (mod > 0)
                    lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
                /// Tach 3 trong tach_conlai

                int i = m;
                int _m = m;
                int j = 1;
                string tach3 = "";
                string tach3_ = "";

                while (i > 0)
                {
                    tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                    tach3_ = tach3;
                    lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                    m = _m + 1 - j;
                    if (!tach3_.Equals("000"))
                        lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                    tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                    i = i - 1;
                    j = j + 1;
                }
                if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                    lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
                if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                    lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
                if (lso_chu.Trim().Length > 0)
                    lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";

                return lso_chu.ToString().Trim();
            }
        }


    }
}