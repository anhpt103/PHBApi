using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU12TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU12TT344;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.SERVICE.REPORT.C_B03B_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03B_X;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.Models.C_B03D_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03D_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03D_X;
using BTS.SP.PHB.ENTITY.Dm;
using static BTS.SP.PHB.SERVICE.Models.C_B03D_X.C_B03d_XVm;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbc_b03D_X")]
    [Route("{id?}")]
    public class PhbCB03dXController : ApiController
    {
        private readonly IPhb_C_B03D_XService _phb_C_B03D_XService;
        private readonly IPhb_C_B03D_XDetailService _phb_C_B03D_XDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbCB03dXController(IPhb_C_B03D_XService phb_C_B03D_XService, IPhb_C_B03D_XDetailService phb_C_B03D_XDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phb_C_B03D_XService = phb_C_B03D_XService;
            _phb_C_B03D_XDetailService = phb_C_B03D_XDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var httpRequest = HttpContext.Current.Request;
            var test = httpRequest.MapPath("~/Upload/");
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_C_B03D_X bieu = new PHB_C_B03D_X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                                //MA_DBHC = firstOrDefault?.Value,
                                MA_DBHC_CHA = firstOrDefault?.Value,
                                TEN_QHNS = "ABC",
                            };
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            bieu.MA_CHUONG = "a";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu.MA_QHNS = "a";
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu.KY_BC = int.Parse(httpRequest["KY_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có địa bàn hành chính."
                            });
                            bieu.MA_DBHC = httpRequest["MA_DBHC"];

                            var checkReport = await _phb_C_B03D_XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb_C_B03D_XService.Insert(bieu);

                            int startRow = 14;
                            int loai = 1;
                            while (workSheet.Cells[startRow, 3].Value != null)
                            {
                                var abc = workSheet.Cells[startRow, 3].Value.ToString();
                                int a = 0;
                                bool checkField = int.TryParse(abc, out a);
                                if (checkField)
                                {
                                    var stt = workSheet.Cells[startRow, 1].Value !=null ? workSheet.Cells[startRow, 1].Value.ToString() : "";
                                    var ten = workSheet.Cells[startRow, 2].Value.ToString();
                                    var ma = workSheet.Cells[startRow, 3].Value.ToString();
                                    var dtS = workSheet.Cells[startRow, 4].Value !=null ? workSheet.Cells[startRow, 4].Value.ToString(): "0";
                                    var qtS = workSheet.Cells[startRow, 5].Value != null ? workSheet.Cells[startRow, 5].Value.ToString() : "0";
                                    var dt = double.Parse(dtS);
                                    var qt = double.Parse(qtS);
                                    PHB_C_B03D_X_DETAIL detail = new PHB_C_B03D_X_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_C_B03D_X_REFID = bieu.REFID,
                                        STTCHITIEU = stt,
                                        TENCHITIEU = ten,
                                        MACHITIEU = ma,
                                        DUTOANNAM = dt,
                                        QUYETTOANNAM = qt,
                                    };
                                    _phb_C_B03D_XDetailService.Insert(detail);

                                    startRow += 1;
                                }
                                else
                                {
                                    startRow += 1;
                                    continue;
                                }

                            }



                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Error = false;
                                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = ErrorMessage.ERROR_DATA;
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_DATA;
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                }
            }
            else
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
            }
            return Ok(response);
        }


        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var httpRequest = HttpContext.Current.Request;
            var test = httpRequest.MapPath("~/Upload/");
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_C_B03D_X bieu = new PHB_C_B03D_X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                                //MA_DBHC = firstOrDefault?.Value,
                                MA_DBHC_CHA = firstOrDefault?.Value,
                                TEN_QHNS = "ABC",
                            };
                            bieu.MA_CHUONG = "423";
                            bieu.MA_QHNS = "1032433";
                            bieu.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phb_C_B03D_XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu.MA_DBHC) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb_C_B03D_XService.Insert(bieu);

                            int startRow = 14;
                            int loai = 1;
                            while (workSheet.Cells[startRow, 3].Value != null)
                            {
                                var abc = workSheet.Cells[startRow, 3].Value.ToString();
                                int a = 0;
                                bool checkField = int.TryParse(abc, out a);
                                if (checkField)
                                {
                                    var stt = workSheet.Cells[startRow, 1].Value != null ? workSheet.Cells[startRow, 1].Value.ToString() : "";
                                    var ten = workSheet.Cells[startRow, 2].Value.ToString();
                                    var ma = workSheet.Cells[startRow, 3].Value.ToString();
                                    var dtS = workSheet.Cells[startRow, 4].Value != null ? workSheet.Cells[startRow, 4].Value.ToString() : "0";
                                    var qtS = workSheet.Cells[startRow, 5].Value != null ? workSheet.Cells[startRow, 5].Value.ToString() : "0";
                                    var dt = double.Parse(dtS);
                                    var qt = double.Parse(qtS);
                                    PHB_C_B03D_X_DETAIL detail = new PHB_C_B03D_X_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_C_B03D_X_REFID = bieu.REFID,
                                        STTCHITIEU = stt,
                                        TENCHITIEU = ten,
                                        MACHITIEU = ma,
                                        DUTOANNAM = dt,
                                        QUYETTOANNAM = qt,
                                    };
                                    _phb_C_B03D_XDetailService.Insert(detail);

                                    startRow += 1;
                                }
                                else
                                {
                                    startRow += 1;
                                    continue;
                                }

                            }



                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Error = false;
                                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = ErrorMessage.ERROR_DATA;
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_DATA;
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                }
            }
            else
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
            }
            return Ok(response);
        }
        [Route("UploadReportFile")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportFile()
        {
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var httpRequest = HttpContext.Current.Request;
            var test = httpRequest.MapPath("~/Upload/");
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_C_B03D_X bieu = new PHB_C_B03D_X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                                MA_DBHC = firstOrDefault?.Value,
                                TEN_QHNS = "ABC",
                            };
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu.MA_QHNS = httpRequest["MA_QHNS"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu.KY_BC = int.Parse(httpRequest["KY_BC"]);


                            var checkReport = await _phb_C_B03D_XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb_C_B03D_XService.Insert(bieu);

                            int startRow = 14;
                            int loai = 1;
                            while (workSheet.Cells[startRow, 3].Value != null)
                            {
                                var abc = workSheet.Cells[startRow, 3].Value.ToString();
                                int a = 0;
                                bool checkField = int.TryParse(abc, out a);
                                if (checkField)
                                {
                                    var stt = workSheet.Cells[startRow, 1].Value != null ? workSheet.Cells[startRow, 1].Value.ToString() : "";
                                    var ten = workSheet.Cells[startRow, 2].Value.ToString();
                                    var ma = workSheet.Cells[startRow, 3].Value.ToString();
                                    var dtS = workSheet.Cells[startRow, 4].Value != null ? workSheet.Cells[startRow, 4].Value.ToString() : "0";
                                    var qtS = workSheet.Cells[startRow, 5].Value != null ? workSheet.Cells[startRow, 5].Value.ToString() : "0";
                                    var dt = double.Parse(dtS);
                                    var qt = double.Parse(qtS);
                                    PHB_C_B03D_X_DETAIL detail = new PHB_C_B03D_X_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_C_B03D_X_REFID = bieu.REFID,
                                        STTCHITIEU = stt,
                                        TENCHITIEU = ten,
                                        MACHITIEU = ma,
                                        DUTOANNAM = dt,
                                        QUYETTOANNAM = qt,
                                    };
                                    _phb_C_B03D_XDetailService.Insert(detail);

                                    startRow += 1;
                                }
                                else
                                {
                                    startRow += 1;
                                    continue;
                                }

                            }



                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Error = false;
                                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = ErrorMessage.ERROR_DATA;
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_DATA;
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                }
            }
            else
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
            }
            return Ok(response);
        }


        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
             if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<C_B03d_XVm.ViewModelDTO> response = new Response<C_B03d_XVm.ViewModelDTO>();
            var httpRequest = HttpContext.Current.Request;
            string a = httpRequest.Path;
            string b = httpRequest.Url.Port.ToString();
            string domain = "http://"+httpRequest.Url.Host.ToString() + ":" + httpRequest.Url.Port.ToString();
            try
            {
                C_B03d_XVm.ViewModelDTO data = new C_B03d_XVm.ViewModelDTO();
                data.REFID = refid;
                var objTemp = await _phb_C_B03D_XService.Queryable().Where(x => x.REFID == refid).FirstOrDefaultAsync();
                data.NGUOI_SUA = objTemp != null ? domain+"/Upload/"+objTemp.MA_DBHC+ "/C_B03D_X/"+objTemp.NAM_BC+"/"+objTemp.KY_BC+"/"+ objTemp.NGUOI_SUA : "";
                var lstTemp =  await _phb_C_B03D_XDetailService.Queryable().Where(x => x.PHB_C_B03D_X_REFID.Equals(refid))
                    .OrderBy(x => x.MACHITIEU).ThenBy(x=> x.ID)
                    .ToListAsync();
                
                if (lstTemp.Count > 0)
                {
                    foreach(var item in lstTemp)
                    {
                        var dataTemp = new PHB_C_B03D_X_DETAIL_DTO();
                        var obj = _unitOfWorkAsync.Repository<DM_PHC_CHITIEUTHU_CHI>().Queryable().Where(x => x.MACHITIEU == item.MACHITIEU && x.PHAN_HE == "C" && x.TRANG_THAI =="A" && x.LOAICHITIEU == 2).FirstOrDefault();
                        var objTemplate = _unitOfWorkAsync.Repository<PHB_C_B03D_X_TEMPLATE>().Queryable().Where(x => x.MACHITIEU == item.MACHITIEU).FirstOrDefault();
                        dataTemp.INDAM = objTemplate != null ? objTemplate.INDAM : 0;
                        if (obj!=null)
                        {
                            dataTemp.MACHA = obj.MACHA;
                            dataTemp.CAP = obj.CAP;
                        }
                        dataTemp.ID = item.ID;
                        dataTemp.MACHITIEU = item.MACHITIEU;
                        dataTemp.PHB_C_B03D_X_REFID = item.PHB_C_B03D_X_REFID;
                        dataTemp.TENCHITIEU = item.TENCHITIEU;
                        dataTemp.STTCHITIEU = item.STTCHITIEU;
                        dataTemp.DUTOANNAM = item.DUTOANNAM;
                        dataTemp.QUYETTOANNAM = item.QUYETTOANNAM;
                        data.DETAIL.Add(dataTemp);
                    }
                    var lstMACHA = data.DETAIL.Select(x => x.MACHA);
                    foreach (var item in data.DETAIL)
                    {
                        if(lstMACHA.Contains(item.MACHITIEU))
                        {
                            item.ISCHILD = 0;
                        }
                        else
                        {
                            item.ISCHILD = 1;
                        }
                    }
                    
                }
                response.Error = false;
                response.Data = data;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(C_B03d_XVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_C_B03D_X bieu = new PHB_C_B03D_X()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = model.MA_CHUONG,
                    MA_QHNS = model.MA_QHNS,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N")
                };

                var checkReport = await _phb_C_B03D_XService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                    x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phb_C_B03D_XService.Insert(bieu);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_C_B03D_X_REFID = bieu.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phb_C_B03D_XDetailService.Insert(detail);
                }
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    response.Error = false;
                    response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put(C_B03d_XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B03D_X bieu =
                    await _phb_C_B03D_XService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_C_B03D_X_DETAIL item = await _phb_C_B03D_XDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _phb_C_B03D_XDetailService.Delete(item);
                        }
                    }
                }
                #endregion

                #region Add

                if (model.LstAdd != null && model.LstAdd.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstAdd)
                    {
                        item.ObjectState = ObjectState.Added;
                        item.PHB_C_B03D_X_REFID = model.REFID;
                        _phb_C_B03D_XDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B03D_X_DETAIL detail = await _phb_C_B03D_XDetailService.FindByIdAsync(item.ID);
                        detail.DUTOANNAM = item.DUTOANNAM;
                        detail.QUYETTOANNAM = item.QUYETTOANNAM;
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            _phb_C_B03D_XDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu.NGAY_SUA = DateTime.Now;
                    bieu.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phb_C_B03D_XService.Update(bieu);

                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var response = await _phb_C_B03D_XService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("GetNewParam")]
        [HttpGet]
        public async Task<IHttpActionResult> GetNewParam()
        {
            Response<C_B03d_XVm.ViewModelDTO> response = new Response<C_B03d_XVm.ViewModelDTO>();
            try
            {
                C_B03d_XVm.ViewModelDTO data = new C_B03d_XVm.ViewModelDTO();
                var lstTemp = _unitOfWorkAsync.Repository<DM_PHC_CHITIEUTHU_CHI>().Queryable()
                    .Where(x => x.PHAN_HE == "C" && x.TRANG_THAI == "A" && x.LOAICHITIEU == 2)
                    .OrderBy(x => x.MACHITIEU).ThenBy(x => x.ID)
                    .ToList();

                if (lstTemp.Count > 0)
                {
                    foreach (var item in lstTemp)
                    {
                        var dataTemp = new PHB_C_B03D_X_DETAIL_DTO();
                        var objTemplate = _unitOfWorkAsync.Repository<PHB_C_B03D_X_TEMPLATE>().Queryable().Where(x => x.MACHITIEU == item.MACHITIEU).FirstOrDefault();
                        dataTemp.INDAM = objTemplate != null ? objTemplate.INDAM : 0;
                        dataTemp.MACHA = item.MACHA;
                        dataTemp.CAP = item.CAP;
                        dataTemp.MACHITIEU = item.MACHITIEU;
                        dataTemp.TENCHITIEU = item.TENCHITIEU;
                        dataTemp.STTCHITIEU = item.SAPXEP;
                        dataTemp.DUTOANNAM = 0;
                        dataTemp.QUYETTOANNAM = 0;
                        data.DETAIL.Add(dataTemp);
                    }
                    var lstMACHA = data.DETAIL.Select(x =>  x.MACHA);
                    lstMACHA = lstMACHA.Distinct();
                    foreach (var item in data.DETAIL)
                    {
                        if (lstMACHA.Contains(item.MACHITIEU))
                        {
                            item.ISCHILD = 0;
                        }
                        else
                        {
                            item.ISCHILD = 1;
                        }
                    }
                    data.DETAIL.OrderBy(x => x.MACHITIEU);
                }
                response.Error = false;
                response.Data = data;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC_CHA"));
            var madbhc_cha = "256";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc_cha = firstOrDefault.Value;
            }
            var response = await _phb_C_B03D_XService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.TENCHITIEU == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B03D_X_DETAIL detail = await _phb_C_B03D_XDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.TENCHITIEU_OLD = entry.TENCHITIEU;
                                detail.TENCHITIEU = rqmodel.newName;
                                _phb_C_B03D_XDetailService.Update(detail);
                            }
                        }
                    }
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        msg.Error = false;
                        msg.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                }
                else
                {
                    msg.Error = true;
                    msg.Message = "Không tìm thấy chỉ tiêu!";
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                msg.Error = true;
                msg.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(msg);
        }

        [Route("Sumreport_HTML")]
        [HttpPost]
        public async Task<IHttpActionResult> SumReport_HTML(ReportRqModel rqmodel)
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC_CHA"));
            var madbhc_cha = "256";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc_cha = firstOrDefault.Value;
            }
            var response = await _phb_C_B03D_XService.SumReport_HTML(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC);
            return Ok(response);
        }

        [Route("MergeReportcomeback")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReportcomeback(ReportRqModelBack rqmodel)
        {
            //var response = await _bieu01BService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName, rqmodel.PHAN, rqmodel.CAP);
            Response<string> msg = new Response<string>();

            try
            {
                var foundLst = await _phb_C_B03D_XDetailService.Queryable()
                    .Where(x => x.TENCHITIEU == rqmodel.TENCHITIEU && x.TENCHITIEU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_C_B03D_X_DETAIL detail = await _phb_C_B03D_XDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.TENCHITIEU = entry.TENCHITIEU_OLD;
                        detail.TENCHITIEU_OLD = null;
                        _phb_C_B03D_XDetailService.Update(detail);
                    }
                }
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    msg.Error = false;
                    msg.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                msg.Error = true;
                msg.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(msg);
        }

    }
}
