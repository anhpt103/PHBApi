using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.B02BCQT;
using BTS.SP.PHB.ENTITY.Rp.PHB.B02BCQT;
using BTS.SP.PHB.SERVICE.Models.B02BCQT;
using BTS.SP.PHB.SERVICE.REPORT.B02BCQT;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.SERVICE.HTDM.DmDBHC;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB02bcqt")]
    [Route("{id?}")]
    public class PhbB02BCQTController : ApiController
    {
        private readonly IPhbB02BcqtService _b02BcqtService;
        private readonly IPhbB02BcqtDetailService _b02BcqtDetailService;
        private readonly IPhbB02BcqtTemplateService _b02BcqtTemplateService;
        private readonly IAuNguoiDungService _auService;
        private readonly IDmDVQHNSService _dvqhnsService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB02BCQTController(IPhbB02BcqtService b02BcqtService, IPhbB02BcqtDetailService b02BcqtDetailService, IPhbB02BcqtTemplateService b02BcqtTemplateService, IUnitOfWorkAsync unitOfWorkAsync, IAuNguoiDungService auService,
            IDmDVQHNSService dvqhnsService)
        {
            _b02BcqtService = b02BcqtService;
            _b02BcqtDetailService = b02BcqtDetailService;
            _b02BcqtTemplateService = b02BcqtTemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _auService = auService;
            _dvqhnsService = dvqhnsService;
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<B02BCQTVm.ViewModel> response = new Response<B02BCQTVm.ViewModel>();
            try
            {
                B02BCQTVm.ViewModel data = new B02BCQTVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _b02BcqtDetailService.Queryable().Where(x => x.PHB_B02BCQT_REFID.Equals(refid))
                    .OrderBy(x => x.PHAN).ThenBy(x => x.STT_CHI_TIEU).ThenBy(x => x.MA_CHI_TIEU)
                    .ToListAsync();
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

        [Route("GetDetailByRefIdForEdit/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefIdForEdit(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<B02BCQTVm.ViewModel> response = new Response<B02BCQTVm.ViewModel>();
            try
            {
                B02BCQTVm.ViewModel data = new B02BCQTVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _b02BcqtDetailService.Queryable().Where(x => x.PHB_B02BCQT_REFID.Equals(refid))
                    .OrderBy(x => x.PHAN).ThenBy(x=>x.STT_CHI_TIEU).ThenBy(x => x.MA_CHI_TIEU)
                    .ToListAsync();
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHB_B02BCQT_TEMPLATE>> response = new Response<List<PHB_B02BCQT_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _b02BcqtTemplateService.Queryable().OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
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
        public async Task<IHttpActionResult> Post(B02BCQTVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var b02bcqt = new PHB_B02BCQT()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = model.MA_CHUONG,
                    MA_QHNS = model.MA_QHNS,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                    TEN_QHNS = model.TEN_QHNS,
                    MA_QHNS_CHA = model.MA_QHNS_CHA
                };

                var checkReport = await _b02BcqtService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(b02bcqt.MA_CHUONG) && x.MA_QHNS.Equals(b02bcqt.MA_QHNS) &&
                    x.NAM_BC == b02bcqt.NAM_BC && x.KY_BC == b02bcqt.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _b02BcqtService.Insert(b02bcqt);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_B02BCQT_REFID = b02bcqt.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _b02BcqtDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(B02BCQTVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var b02Bcqt =
                    await _b02BcqtService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (b02Bcqt == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_B02BCQT_DETAIL item = await _b02BcqtDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _b02BcqtDetailService.Delete(item);
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
                        item.PHB_B02BCQT_REFID = model.REFID;
                        _b02BcqtDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_B02BCQT_DETAIL detail = await _b02BcqtDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;

                            detail.SKN_TONGSO = item.SKN_TONGSO;
                            detail.SKN_THANHTRA = item.SKN_THANHTRA;
                            detail.SKN_KIEMTOAN = item.SKN_KIEMTOAN;
                            detail.SKN_TAICHINH = item.SKN_TAICHINH;

                            detail.SDXL_TONGSO = item.SDXL_TONGSO;
                            detail.SDXL_THANHTRA = item.SDXL_THANHTRA;
                            detail.SDXL_KIEMTOAN = item.SDXL_KIEMTOAN;
                            detail.SDXL_TAICHINH = item.SDXL_TAICHINH;

                            detail.SCXL_TONGSO = item.SCXL_TONGSO;
                            detail.SCXL_THANHTRA = item.SCXL_THANHTRA;
                            detail.SCXL_KIEMTOAN = item.SCXL_KIEMTOAN;
                            detail.SCXL_TAICHINH = item.SCXL_TAICHINH;

                            _b02BcqtDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    b02Bcqt.NGAY_SUA = DateTime.Now;
                    b02Bcqt.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _b02BcqtService.Update(b02Bcqt);

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

        //[Route("UploadReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> UploadReport()
        //{
        //    Response<string> response = new Response<string>();
        //    var httpRequest = HttpContext.Current.Request;

        //    try
        //    {
        //        if (HttpContext.Current.Request.Files.Count > 0)
        //        {
        //            using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
        //            {
        //                var workSheet = excelPackage.Workbook.Worksheets[1];
        //                if (workSheet != null)
        //                {
        //                    PHB_B02BCQT b02bcqt = new PHB_B02BCQT()
        //                    {
        //                        NGAY_TAO = DateTime.Now,
        //                        NGUOI_TAO = RequestContext.Principal.Identity.Name,
        //                        ObjectState = ObjectState.Added,
        //                        TRANG_THAI = 0,
        //                        REFID = Guid.NewGuid().ToString("N")
        //                    };

        //                    if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
        //                    {
        //                        Error = true,
        //                        Message = "Không có mã chương."
        //                    });
        //                    b02bcqt.MA_CHUONG = httpRequest["MA_CHUONG"];
        //                    if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
        //                    {
        //                        Error = true,
        //                        Message = "Không có mã đơn vị QHNS."
        //                    });
        //                    b02bcqt.MA_QHNS = httpRequest["MA_QHNS"];
        //                    b02bcqt.TEN_QHNS = httpRequest["TEN_QHNS"];
        //                    b02bcqt.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
        //                    if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
        //                    {
        //                        Error = true,
        //                        Message = "Không có năm báo cáo."
        //                    });
        //                    b02bcqt.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
        //                    if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
        //                    {
        //                        Error = true,
        //                        Message = "Không có kỳ báo cáo."
        //                    });
        //                    b02bcqt.KY_BC = int.Parse(httpRequest["KY_BC"]);

        //                    var checkReport = await _b02BcqtService.Queryable().FirstOrDefaultAsync(x =>
        //                        x.MA_QHNS.Equals(b02bcqt.MA_QHNS) && x.NAM_BC == b02bcqt.NAM_BC && x.KY_BC == b02bcqt.KY_BC);
        //                    if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
        //                    _b02BcqtService.Insert(b02bcqt);

        //                    //chi tiết
        //                    string machitieu = string.Empty;
        //                    string sothutu = string.Empty;
        //                    int startRow = 11;
        //                    int count = 1;
        //                    #region xử lý phần I
        //                    while (workSheet.Cells[startRow, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "II")
        //                    {
        //                        PHB_B02BCQT_DETAIL detail = new PHB_B02BCQT_DETAIL()
        //                        {
        //                            ObjectState = ObjectState.Added,
        //                            PHB_B02BCQT_REFID = b02bcqt.REFID
        //                        };

        //                        //loại
        //                        if (workSheet.Cells[startRow, 3].Value != null)
        //                        {
        //                            count = 1;
        //                            machitieu = workSheet.Cells[startRow, 3].Value?.ToString();
        //                            if (int.Parse(machitieu) >= 3 && int.Parse(machitieu) <= 12)
        //                            {
        //                                detail.LOAI = 1;
        //                            }
        //                            else if (machitieu == "1" || machitieu == "2")
        //                            {
        //                                detail.LOAI = 2;
        //                            }
        //                            detail.MA_CHI_TIEU = machitieu;
        //                        }
        //                        else
        //                        {
        //                            detail.LOAI = 3;
        //                            if (int.Parse(machitieu) < 10)
        //                            {
        //                                detail.MA_CHI_TIEU = "0" + machitieu + (count++);
        //                            }
        //                            else
        //                            {
        //                                detail.MA_CHI_TIEU = machitieu + (count++);
        //                            }
        //                            detail.MA_CHI_TIEU_CHA = machitieu;
        //                        }

        //                        //phần
        //                        if (workSheet.Cells[startRow, 1].Value != null)
        //                        {
        //                            sothutu = workSheet.Cells[startRow, 1].Value?.ToString();
        //                            detail.PHAN = "1" + sothutu;
        //                        }
        //                        else
        //                        {
        //                            detail.PHAN = "1" + sothutu;
        //                        }

        //                        detail.STT_CHI_TIEU = workSheet.Cells[startRow, 1].Value?.ToString();
        //                        detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
        //                        if (workSheet.Cells[startRow, 4].Value != null) detail.SKN_TONGSO = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
        //                        if (workSheet.Cells[startRow, 5].Value != null) detail.SKN_THANHTRA = double.Parse(workSheet.Cells[startRow, 5].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 6].Value != null) detail.SKN_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 7].Value != null) detail.SKN_TAICHINH = double.Parse(workSheet.Cells[startRow, 7].Value.ToString().Trim());

        //                        if (workSheet.Cells[startRow, 8].Value != null) detail.SDXL_TONGSO = double.Parse(workSheet.Cells[startRow, 8].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 9].Value != null) detail.SDXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 9].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 10].Value != null) detail.SDXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 10].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 11].Value != null) detail.SDXL_TAICHINH = double.Parse(workSheet.Cells[startRow, 11].Value.ToString().Trim());

        //                        if (workSheet.Cells[startRow, 12].Value != null) detail.SCXL_TONGSO = double.Parse(workSheet.Cells[startRow, 12].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 13].Value != null) detail.SCXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 13].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 14].Value != null) detail.SCXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 14].Value.ToString().Trim());
        //                        if (workSheet.Cells[startRow, 15].Value != null) detail.SCXL_TAICHINH = double.Parse(workSheet.Cells[startRow, 15].Value.ToString().Trim());

        //                        _b02BcqtDetailService.Insert(detail);
        //                        startRow += 1;

        //                    }
        //                    #endregion
        //                    #region xử lý phần II
        //                    while (workSheet.Cells[startRow, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "III")
        //                    {
        //                        if (workSheet.Cells[startRow, 1].Value?.ToString() != "II")
        //                        {
        //                            PHB_B02BCQT_DETAIL detail = new PHB_B02BCQT_DETAIL()
        //                            {
        //                                ObjectState = ObjectState.Added,
        //                                PHB_B02BCQT_REFID = b02bcqt.REFID
        //                            };

        //                            //loại
        //                            if (workSheet.Cells[startRow, 3].Value != null)
        //                            {
        //                                count = 1;
        //                                machitieu = workSheet.Cells[startRow, 3].Value?.ToString();

        //                                if (int.Parse(machitieu) >= 15 && int.Parse(machitieu) <= 24)
        //                                {
        //                                    detail.LOAI = 1;
        //                                }
        //                                else if (machitieu == "13" || machitieu == "14")
        //                                {
        //                                    detail.LOAI = 2;
        //                                }
        //                                detail.MA_CHI_TIEU = machitieu;

        //                            }
        //                            else
        //                            {
        //                                detail.LOAI = 3;
        //                                if (int.Parse(machitieu) < 10)
        //                                {
        //                                    detail.MA_CHI_TIEU = "0" + machitieu + (count++);
        //                                }
        //                                else
        //                                {
        //                                    detail.MA_CHI_TIEU = machitieu + (count++);
        //                                }
        //                                detail.MA_CHI_TIEU_CHA = machitieu;
        //                            }

        //                            //phần
        //                            if (workSheet.Cells[startRow, 1].Value != null)
        //                            {
        //                                sothutu = workSheet.Cells[startRow, 1].Value?.ToString();
        //                                detail.PHAN = "2" + sothutu;
        //                            }
        //                            else
        //                            {
        //                                detail.PHAN = "2" + sothutu;
        //                            }

        //                            detail.STT_CHI_TIEU = workSheet.Cells[startRow, 1].Value?.ToString();
        //                            detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
        //                            if (workSheet.Cells[startRow, 4].Value != null) detail.SKN_TONGSO = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
        //                            if (workSheet.Cells[startRow, 5].Value != null) detail.SKN_THANHTRA = double.Parse(workSheet.Cells[startRow, 5].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 6].Value != null) detail.SKN_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 7].Value != null) detail.SKN_TAICHINH = double.Parse(workSheet.Cells[startRow, 7].Value.ToString().Trim());

        //                            if (workSheet.Cells[startRow, 8].Value != null) detail.SDXL_TONGSO = double.Parse(workSheet.Cells[startRow, 8].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 9].Value != null) detail.SDXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 9].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 10].Value != null) detail.SDXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 10].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 11].Value != null) detail.SDXL_TAICHINH = double.Parse(workSheet.Cells[startRow, 11].Value.ToString().Trim());

        //                            if (workSheet.Cells[startRow, 12].Value != null) detail.SCXL_TONGSO = double.Parse(workSheet.Cells[startRow, 12].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 13].Value != null) detail.SCXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 13].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 14].Value != null) detail.SCXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 14].Value.ToString().Trim());
        //                            if (workSheet.Cells[startRow, 15].Value != null) detail.SCXL_TAICHINH = double.Parse(workSheet.Cells[startRow, 15].Value.ToString().Trim());

        //                            _b02BcqtDetailService.Insert(detail);
        //                            startRow += 1;
        //                        }
        //                        else
        //                        {
        //                            startRow += 1;
        //                        }

        //                    }
        //                    #endregion




        //                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
        //                    {
        //                        response.Error = false;
        //                        response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //                    }
        //                    else
        //                    {
        //                        response.Error = true;
        //                        response.Message = ErrorMessage.ERROR_UPDATE_DATA;
        //                    }
        //                }
        //                else
        //                {
        //                    response.Error = true;
        //                    response.Message = ErrorMessage.ERROR_DATA;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_DATA;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = "-1";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc = firstOrDefault.Value;
            }
            var response = await _b02BcqtService.Sumreport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()), madbhc);
            return Ok(response);
        }

        [Route("Sumreport_HTML")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport_HTML(ReportRqModel rqmodel)
        {
            var response = await _b02BcqtService.SumReport_HTML(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var response = await _b02BcqtService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName, rqmodel.isPHAN);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.TEN_CHI_TIEU == item && x.PHAN==rqmodel.isPHAN);

                        foreach (var entry in foundLst)
                        {
                            PHB_B02BCQT_DETAIL detail = await _b02BcqtDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.TEN_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                                detail.TEN_CHI_TIEU = rqmodel.newName;
                                _b02BcqtDetailService.Update(detail);
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

        [Route("MergeReportcomeback")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReportcomeback(ReportRqModelBack rqmodel)
        {
            //var response = await _bieu01BService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName, rqmodel.PHAN, rqmodel.CAP);
            Response<string> msg = new Response<string>();

            try
            {
                var foundLst = await _b02BcqtDetailService.Queryable()
                    .Where(x => x.TEN_CHI_TIEU == rqmodel.TEN_CHI_TIEU && x.TEN_CHI_TIEU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_B02BCQT_DETAIL detail = await _b02BcqtDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.TEN_CHI_TIEU = entry.TEN_CHI_TIEU_OLD;
                        detail.TEN_CHI_TIEU_OLD = null;
                        _b02BcqtDetailService.Update(detail);
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

        [Route("UploadReport")]
        [HttpPost]
        public IHttpActionResult UploadReport()
        {
            var response = new Response();
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            string chuong = "";
            string nguoiTao = "";
            string maDonVi = "";
            int namBaoCao;
            int kyBaoCao = 0;

            try
            {
                namBaoCao = int.Parse(httpRequest["Nam"].ToString());
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                nguoiTao = identity.Name;

                maDonVi = _auService.Queryable().Where(u => u.USERNAME == nguoiTao).FirstOrDefault()?.MA_DONVI;


            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            if (String.IsNullOrEmpty(maDonVi))
            {
                response.Error = true;
                response.Message = "Lỗi không tồn tại đơn vị tương ứng với người dùng hiện tại";
                return Ok(response);
            }

            var form = new PHB_B02BCQT()
            {
                MA_CHUONG = chuong,
                MA_QHNS = maDonVi,
                NAM_BC = namBaoCao,
                KY_BC = kyBaoCao,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = nguoiTao,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0
            };

            _b02BcqtService.Insert(form);

            var lstTemplate = _b02BcqtTemplateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP);
            var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.TEN_CHI_TIEU).ToList();


            using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
            {
                var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (sheet == null)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                    return Ok(response);
                }

                var startRow = GetStartRow(sheet, lstChiTieuTemplate.FirstOrDefault());
                var endRow = GetEndRow(sheet, lstChiTieuTemplate.LastOrDefault());
                var stt_SapXep = 1;

                for (int row = startRow; row <= endRow; row++)
                {
                    try
                    {
                        var detail = new PHB_B02BCQT_DETAIL
                        {
                            STT_CHI_TIEU = sheet.Cells[row, 1].Value?.ToString(),
                            TEN_CHI_TIEU = sheet.Cells[row, 2].Value.ToString(),
                            IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
                            IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
                            PHB_B02BCQT_REFID = form.REFID,
                            MA_CHI_TIEU = sheet.Cells[row, 3].Value?.ToString(),
                            SKN_TONGSO = double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
                            SKN_THANHTRA = double.Parse(sheet.Cells[row, 5].Value?.ToString() ?? "0"),
                            SKN_KIEMTOAN = double.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0"),
                            SDXL_TONGSO = double.Parse(sheet.Cells[row, 7].Value?.ToString() ?? "0"),
                            SDXL_THANHTRA = double.Parse(sheet.Cells[row, 8].Value?.ToString() ?? "0"),
                            SDXL_KIEMTOAN = double.Parse(sheet.Cells[row, 9].Value?.ToString() ?? "0"),
                            SCXL_TONGSO =double.Parse(sheet.Cells[row, 10].Value?.ToString() ?? "0"),
                            SCXL_THANHTRA = double.Parse(sheet.Cells[row, 11].Value?.ToString() ?? "0"),
                            SCXL_KIEMTOAN = double.Parse(sheet.Cells[row, 12].Value?.ToString() ?? "0"),
                            STT_SAPXEP = stt_SapXep
                        };

                        _b02BcqtDetailService.Insert(detail);
                     }
                    catch (Exception ex)
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.ERROR_DATA;
                        return Ok(response);
                    }
                    stt_SapXep++;
                }


            }

            try
            {
                _unitOfWorkAsync.SaveChanges();
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

        private int GetStartRow(ExcelWorksheet sheet, string firstChiTieuTpl)
        {
            var firstRow = sheet.Dimension.Start.Row;
            var lastRow = sheet.Dimension.End.Row;

            for (int row = firstRow; row <= lastRow; row++)
            {
                if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == firstChiTieuTpl.Trim().ToLower())
                {
                    return row;
                }
            }

            throw new Exception();
        }

        private int GetEndRow(ExcelWorksheet sheet, string lastChiTieuTpl)
        {
            var firstRow = sheet.Dimension.Start.Row;
            var lastRow = sheet.Dimension.End.Row;

            for (int row = lastRow; row >= firstRow; row--)
            {
                if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == lastChiTieuTpl.Trim().ToLower())
                {
                    return row;
                }
            }

            throw new Exception();
        }
    }
}