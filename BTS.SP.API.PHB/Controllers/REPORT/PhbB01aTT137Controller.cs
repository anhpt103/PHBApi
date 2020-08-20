using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Helper;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity;
using BTS.SP.PHB.SERVICE.REPORT.B01A_TT137;
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;
using BTS.SP.PHB.SERVICE.REPORT.Bm48TT342;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using BTS.SP.API.PHB.ViewModels.REPORT.B01A_TT137;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB01A_TT137")]
    [Route("{id?}")]
    public class PhbB01aTT137Controller : ApiController
    {
        private readonly IPhbBm48TT342Service _bm48TT342Service_2;
        private readonly IPhbBm48TT342DetailService _bm48TT342DetailService_2;

        private readonly IPhbB01aTT137Service _PhbB01aTT137Service;
        private readonly IPhbB01aTT137TemplateService _PhbB01aTT137TemplateService;
        private readonly IPhbB01aTT137DetailService _PhbB01aTT137DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB01aTT137Controller(IPhbBm48TT342Service bm48TT342Service_2, IPhbBm48TT342DetailService bm48TT342DetailService_2,IPhbB01aTT137Service PhbB01aTT137Service, IPhbB01aTT137TemplateService PhbB01aTT137TemplateService,
            IPhbB01aTT137DetailService PhbB01aTT137Detail, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bm48TT342DetailService_2 = bm48TT342DetailService_2;
            _bm48TT342Service_2 = bm48TT342Service_2;
            _PhbB01aTT137Service = PhbB01aTT137Service;
            _PhbB01aTT137TemplateService = PhbB01aTT137TemplateService;
            _PhbB01aTT137DetailService = PhbB01aTT137Detail;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region get template
        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTeamplate()
        {
            var response = new Response<List<PHB_B01A_TT137_TEMPLATE>>();
            try
            {
                response.Data = _PhbB01aTT137TemplateService.Queryable().ToList();
            }
            catch(Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            response.Error = false;
            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }
        #endregion

        #region thêm mới
        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(B01A_TT137ViewModel.AddModel model)
        {
            var respone = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;


            var data = new PHB_B01A_TT137
            {
                REFID = Guid.NewGuid().ToString("n"),
                MA_CHUONG = model.data.MA_CHUONG,
                MA_QHNS = model.data.MA_QHNS,
                TEN_QHNS = model.data.TEN_QHNS,
                MA_QHNS_CHA = "",
                MA_DV_SDNS = "",
                MA_SO_SDNS = "",
                MA_KBNN = "",
                NAM_BC = model.data.NAM_BC,
                KY_BC = model.data.KY_BC,
                TRANG_THAI = 0,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = identity.Name,
                NGUOI_SUA = null,
                NGAY_SUA = null
            };
            _PhbB01aTT137Service.Insert(data);

            foreach (var item in model.datadetail)
            {
                item.PHB_B01A_TT137_REFID = data.REFID;
                _PhbB01aTT137DetailService.Insert(item);
            }

            var checkExits = _PhbB01aTT137Service.Queryable().Where(x => x.MA_QHNS == data.MA_QHNS && x.NAM_BC == data.NAM_BC && x.KY_BC == data.KY_BC).Count();
            if(checkExits > 0)
            {
                respone.Error = true;
                respone.Message = ErrorMessage.EXITS_REPORT;
                return Ok(respone);
            }

            try
            {
                _unitOfWorkAsync.SaveChanges();
            }
            catch(Exception ex)
            {
                respone.Error = true;
                WriteLogs.LogError(ex);
                respone.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(respone);
            }

            respone.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(respone);
        }
        #endregion

        #region edit
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(B01A_TT137ViewModel.EditModel model)
        {
            var respone = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var form = new PHB_B01A_TT137();
            var listDetail = new List<PHB_B01A_TT137_DETAIL>();

            if (model.datadetail.Count <= 0)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }
          
            try
            {
                form = _PhbB01aTT137Service.Queryable().FirstOrDefault(x => x.REFID == model.refid);
                listDetail = _PhbB01aTT137DetailService.Queryable().Where(x => x.PHB_B01A_TT137_REFID == form.REFID).ToList();

            }
            catch(Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            form.NGUOI_SUA = identity.Name;
            form.NGAY_SUA = DateTime.Now;
            form.ObjectState = ObjectState.Modified;

            try
            {
                foreach(var item in listDetail)
                {
                    _PhbB01aTT137DetailService.Delete(item);
                }

                foreach(var item in model.datadetail)
                {
                    item.ID = 0;
                    item.PHB_B01A_TT137_REFID = form.REFID;
                    _PhbB01aTT137DetailService.Insert(item);
                }
            }
            catch(Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }
        
            try
            {
                _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                respone.Error = true;
                WriteLogs.LogError(ex);
                respone.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(respone);
            }

            respone.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(respone);
        }

        #endregion

        #region ApproveReport 
        [Route("ApproveReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ApproveReport(B01A_TT137ViewModel.EditModel model)
        {
            var respone = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var form = new PHB_B01A_TT137();
            var listDetail = new List<PHB_B01A_TT137_DETAIL>();

            if (model.datadetail.Count <= 0)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }
            try
            {
                form = _PhbB01aTT137Service.Queryable().FirstOrDefault(x => x.REFID == model.refid);
                listDetail = _PhbB01aTT137DetailService.Queryable().Where(x => x.PHB_B01A_TT137_REFID == form.REFID).ToList();

            }
            catch (Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            form.TRANG_THAI = 1;
            form.NGUOI_SUA = identity.Name;
            form.NGAY_SUA = DateTime.Now;
            form.ObjectState = ObjectState.Modified;

            try
            {
                foreach (var item in listDetail)
                {
                    _PhbB01aTT137DetailService.Delete(item);
                }

                foreach (var item in model.datadetail)
                {
                    item.ID = 0;
                    item.PHB_B01A_TT137_REFID = form.REFID;
                    _PhbB01aTT137DetailService.Insert(item);
                }
            }
            catch (Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            try
            {
                _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                respone.Error = true;
                WriteLogs.LogError(ex);
                respone.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(respone);
            }

            respone.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(respone);
        }
        #endregion

        #region Get Detail
        [Route("getDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> getDetailByRefId(string refid)
        {
            var result = new Response<List<PHB_B01A_TT137_DETAIL>>();
            try
            {
                result.Data = _PhbB01aTT137DetailService.Queryable().Where(x => x.PHB_B01A_TT137_REFID == refid).OrderBy(x => x.STT).ToList();
            }
            catch(Exception ex)
            {
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            result.Error = false;
            result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(result);
        }

        #endregion
        #region Upload
        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()   
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var b01Bcqt = new PHB_B01A_TT137()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };

                        var workSheet = excelPackage.Workbook.Worksheets["Sheet1"];
                        if (workSheet != null)
                        {

                            b01Bcqt.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            b01Bcqt.MA_QHNS = httpRequest["MA_QHNS"];
                            b01Bcqt.TEN_QHNS = httpRequest["TEN_QHNS"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b01Bcqt.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            b01Bcqt.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _PhbB01aTT137Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(b01Bcqt.MA_CHUONG) && x.MA_QHNS.Equals(b01Bcqt.MA_QHNS) &&
                                x.NAM_BC == b01Bcqt.NAM_BC && x.KY_BC == b01Bcqt.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _PhbB01aTT137Service.Insert(b01Bcqt);

                            int start_Row = 8;                       
                            int start_Col = 1;
                            int end_Col = 8;
                            int count = 1;

                            bool isDt,  isDt_sxd, isDt_cl, isTh, isTh_sxd, isTh_cl;
                            double _Dt, _Dt_sxd, _Dt_cl, _Th, _Th_sxd, Th_cl;
                            var STTCHITIEU = "";
                            while (workSheet.Cells[start_Row, 1].Value != null || workSheet.Cells[start_Row, 2].Value != null)
                            {
                                if (workSheet.Cells[start_Row, 1].Value == null)
                                {
                                    workSheet.Cells[start_Row, 1].Value = "";
                                }
                                if (workSheet.Cells[start_Row, 1].Value.ToString() == "I")
                                {
                                    STTCHITIEU = "I";

                                }
                                if (workSheet.Cells[start_Row, 1].Value.ToString() == "II")
                                {
                                    STTCHITIEU = "II";

                                }
                                if (workSheet.Cells[start_Row, 1].Value.ToString() != "I" && workSheet.Cells[start_Row, 1].Value.ToString() != "II")
                                {
                                    var obj = new PHB_B01A_TT137_DETAIL() { PHB_B01A_TT137_REFID = b01Bcqt.REFID, ObjectState = ObjectState.Added };
                                    obj.STT = count;
                                    obj.STT_CHI_TIEU = STTCHITIEU;
                                    obj.MA_CHI_TIEU = workSheet.Cells[start_Row, 1].Value != null ? workSheet.Cells[start_Row, 1].Value.ToString() : null;
                                    obj.TEN_CHI_TIEU = workSheet.Cells[start_Row, 2].Value != null ? workSheet.Cells[start_Row, 2].Value.ToString() : null;
                                    isDt = double.TryParse(workSheet.Cells[start_Row, 3].Value == null ? "0" : workSheet.Cells[start_Row, 3].Value.ToString(), out _Dt);
                                    obj.DU_TOAN = isDt ? _Dt : 0;
                                    isDt_sxd = double.TryParse(workSheet.Cells[start_Row, 4].Value == null ? "0" : workSheet.Cells[start_Row, 4].Value.ToString(), out _Dt_sxd);
                                    obj.DT_SXD = isDt_sxd ? _Dt_sxd : 0;
                                    isDt_cl = double.TryParse(workSheet.Cells[start_Row, 5].Value == null ? "0" : workSheet.Cells[start_Row, 5].Value.ToString(), out _Dt_cl);
                                    obj.DT_CL = isDt_cl ? _Dt_cl : 0;
                                    isTh = double.TryParse(workSheet.Cells[start_Row, 6].Value == null ? "0" : workSheet.Cells[start_Row, 6].Value.ToString(), out _Th);
                                    obj.THUC_HIEN = isTh ? _Th : 0;
                                    isTh_sxd = double.TryParse(workSheet.Cells[start_Row, 7].Value == null ? "0" : workSheet.Cells[start_Row, 7].Value.ToString(), out _Th_sxd);
                                    obj.TH_SXD = isTh_sxd ? _Th_sxd : 0;
                                    isTh_cl = double.TryParse(workSheet.Cells[start_Row, 8].Value == null ? "0" : workSheet.Cells[start_Row, 8].Value.ToString(), out Th_cl);
                                    obj.TH_CL = isTh_cl ? Th_cl : 0;

                                    _PhbB01aTT137DetailService.Insert(obj);
                                    count += 1;
                                }
                            start_Row++;
                                
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b01Bcqt.REFID;
                                response.Error = false;
                                response.Message = "Cập nhật thành công.";
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = "Lỗi cập nhật dữ liệu.";
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = "Lỗi định dạng dữ liệu.";
                        }
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "Không có dữ liệu upload.";
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        #endregion
        //public class ExportParams
        //{
        //    public string MA_DVQHNS { get; set; }
        //    public int NAM_BC { get; set; }

        //}

        //#region get template
        //[Route("QuerryTemplate")]
        //[HttpPost]
        //public async Task<IHttpActionResult> QuerryTemplate(PHB_B01A_TT137 model)
        //{
        //    var httpRequest = HttpContext.Current.Request;
        //    var response = new Response<string>();
        //    var lstPhi = new List<PHB_BM48_TT342_DETAIL>();
        //    var lstLePhi = new List<PHB_BM48_TT342_DETAIL>();

        //    try
        //    {
        //        var tempObj = _bm48TT342Service_2.Queryable().FirstOrDefault(x => x.MA_QHNS == model.MA_QHNS && x.NAM_BC == model.NAM_BC);
        //        if(tempObj == null)
        //        {
        //            response.Error = true;
        //            response.Message = "Chưa nhập báo cáo Biểu 48 - TT342";
        //        }
        //        else
        //        {
        //            var lstDetails = _bm48TT342DetailService_2.Queryable().Where(x => x.PHB_BM48_TT342_REFID == tempObj.REFID).OrderBy(x=>x.STT).ToList();
        //            foreach(var item in lstDetails)
        //            {
        //                if(item.STT_CHI_TIEU == "1.2")
        //                {

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        response.Message = ex.Message;
        //    }
        //    return Ok(response);
        //}

        //#endregion

        //[Route("ExportExcel")]

        //public HttpResponseMessage ExportExcel(ExportParams model)
        //{
        //    var lstPhi = new List<PHB_BM48_TT342_DETAIL>();
        //    var lstLePhi = new List<PHB_BM48_TT342_DETAIL>();

        //    HttpResponseMessage result = null;
        //    string file = null;
        //    var tempObj = _bm48TT342Service_2.Queryable().FirstOrDefault(x => x.MA_QHNS == model.MA_DVQHNS && x.NAM_BC == model.NAM_BC);
        //    if (tempObj == null)
        //    {
        //        //result.Error = true;
        //        //result.Message = "Chưa nhập báo cáo Biểu 48 - TT342";
        //    }
        //    else
        //    {
        //        lstPhi.AddRange(_bm48TT342DetailService_2.Queryable().Where(x => x.PHB_BM48_TT342_REFID == tempObj.REFID && x.MA_CHI_TIEU == "I-1-1.2").OrderBy(x => x.STT).ToList());
        //        lstLePhi.AddRange(_bm48TT342DetailService_2.Queryable().Where(x => x.PHB_BM48_TT342_REFID == tempObj.REFID && x.MA_CHI_TIEU == "I-3-3.1").OrderBy(x => x.STT).ToList());
        //    }

        //    file = _CreateExcelFile1C(lstPhi, lstLePhi);
        //    if (!string.IsNullOrEmpty(file))
        //    {
        //        if (!File.Exists(file))
        //        {
        //            result = Request.CreateResponse(HttpStatusCode.NoContent);
        //        }
        //        else
        //        {
        //            result = Request.CreateResponse(HttpStatusCode.OK);
        //            result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
        //            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //            result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
        //        }
        //    }
        //    return result;
        //}

        //public string _CreateExcelFile1C(List<PHB_BM48_TT342_DETAIL> lstPhi, List<PHB_BM48_TT342_DETAIL> lstLePhi)

        //{
        //    var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
        //    var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
        //    DateTime now = DateTime.Now;
        //    string date = now.ToString("dd-MM-yyyy");
        //    //var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/API_PHB/Template/BIEU4BP1/BIEU4B1TT137.xlsx";
        //    var fileNameInPut = "B01A_TT137_Template.xlsx";
        //    string folderServer = @"\Template\";
        //    string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
        //    if (!Directory.Exists(filePathResult))
        //    {
        //        Directory.CreateDirectory(filePathResult);
        //    }
        //    string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/B01A_TT137/");
        //    if (!Directory.Exists(resourceTemplate))
        //    {
        //        Directory.CreateDirectory(resourceTemplate);
        //    }
        //    string filePathSource = resourceTemplate + fileNameInPut;
        //    var urlFile = "C:/ExportOutPut/";
        //    var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
        //    if (System.IO.File.Exists(filePathSource))
        //    {

        //        using (var excelPackage = new ExcelPackage())
        //        {
        //            using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
        //            {
        //                excelPackage.Load(filetemplate);
        //                var workSheet = excelPackage.Workbook.Worksheets[1];
        //                _BindingDataToExcel1C(workSheet, lstPhi, lstLePhi);
        //                FileStream stream = new FileStream(filename, FileMode.Create);
        //                excelPackage.SaveAs(stream);
        //                stream.Close();
        //            }
        //        }

        //    }

        //    else
        //    {
        //        filename = "";
        //    }

        //    return filename;
        //}

        //public void _BindingDataToExcel1C(ExcelWorksheet ws, List<PHB_BM48_TT342_DETAIL> lstPhi, List<PHB_BM48_TT342_DETAIL> lstLePhi)
        //{
        //    var starRow = 9;
        //    var starCol = 1;
        //    var STT = 2;
        //    var nextSTT = 0;
        //    int count = 1;
        //    ws.Cells[8 , 1].Value = "I";
        //    ws.Cells[8, 2].Value = "PHÍ";


        //    foreach (var item in lstPhi)
        //    {
        //        ws.Cells[starRow, 1].Value = count;
        //        ws.Cells[starRow, 2].Value = item.TEN_CHI_TIEU;
        //        starRow++;
        //        ws.Cells[starRow, 2].Value = "- Tổng số thu";
        //        starRow++;
        //        ws.Cells[starRow, 2].Value = "- Số phải nộp NSNN";
        //        starRow++;
        //        ws.Cells[starRow, 2].Value = "- Số được khấu trừ hoặc để lại";
        //        starRow++;
        //        count ++;

        //    }
        //    ws.Cells[starRow, 1].Value = "II";
        //    ws.Cells[starRow, 2].Value = "LỆ PHÍ";
        //    starRow++;
        //    count = 1;
        //    foreach (var item in lstLePhi)
        //    {
        //        ws.Cells[starRow, 1].Value = count;
        //        ws.Cells[starRow, 2].Value = item.TEN_CHI_TIEU;
        //        starRow++;
        //        count++;
        //    }
        //    starRow++;
        //    starRow++;

        //    //ws.Cells[starRow, 1, starRow+1, 4].Merge = true;
        //    ws.Cells[starRow, 1].Value = "Ghi chú: Số liệu xét duyệt, thẩm định biểu này trên cơ sở Thuyết minh báo cáo quyết toán theo Mẫu số 03/BCQT ban hành kèm theo Thông tư số 107/2017/TT-BTC";

        //}

    }
}