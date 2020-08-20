using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.SERVICE.Models.C_B04X;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.SERVICE.REPORT.C_B04X;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.C_B04X;
using BTS.SP.PHB.ENTITY.Rp;
using System.IO;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbC_B04X")]
    [Route("{id?}")]
    public class PhbBieuB04XController : ApiController
    {
        private readonly IPhbBieuC_B04XService _bieub04xService;
        private readonly IPhbBieuC_B04XDetailService _bieub04xDetailService;
        private readonly IPhbBieuC_B04XDetail_TSCDService _bieub04xDetailTSCDService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public PhbBieuB04XController(IPhbBieuC_B04XService bieub04xService, 
            IPhbBieuC_B04XDetailService bieub04xDetailService, 
            IPhbBieuC_B04XDetail_TSCDService bieub04xDetailTSCDService,
            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieub04xService = bieub04xService;
            _bieub04xDetailService = bieub04xDetailService;
            _bieub04xDetailTSCDService = bieub04xDetailTSCDService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetDetails/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetails(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<C_B04XVm.ViewModel> response = new Response<C_B04XVm.ViewModel>();
            try
            {
                C_B04XVm.ViewModel data = new C_B04XVm.ViewModel();
                PHB_C_B04X bieub04x = _bieub04xService.Queryable().FirstOrDefault(x => x.REFID.Equals(refid));
                if (bieub04x != null)
                {
                    data.KY_BC = bieub04x.KY_BC;
                    data.NAM_BC = bieub04x.NAM_BC;
                    data.MA_CHUONG = bieub04x.MA_CHUONG;
                    data.MA_QHNS = bieub04x.MA_QHNS;
                    data.NGAY_TAO = bieub04x.NGAY_TAO;
                    data.NGUOI_TAO = bieub04x.NGUOI_TAO;
                    data.TRANG_THAI = bieub04x.TRANG_THAI;
                    data.REFID = bieub04x.REFID;
                    data.MA_DBHC = bieub04x.MA_DBHC;
                    data.MA_DBHC_CHA = bieub04x.MA_DBHC_CHA;
                    data.TEN_QHNS = bieub04x.TEN_QHNS;
                    data.DIEN_TICH = bieub04x.DIEN_TICH;
                    data.DIEN_TICH_DAT = bieub04x.DIEN_TICH_DAT;
                    data.DANSO = bieub04x.DANSO;
                    data.NGANH_NGHE = bieub04x.NGANH_NGHE;
                    data.MUCTIEU_NHIEMVU = bieub04x.MUCTIEU_NHIEMVU;
                    data.DANH_GIA = bieub04x.DANH_GIA;
                    data.NGUYEN_NHAN = bieub04x.NGUYEN_NHAN;
                    data.KHACH_QUAN = bieub04x.KHACH_QUAN;
                    data.CHU_QUAN = bieub04x.CHU_QUAN;
                    data.DENGHI_KIENXUAT = bieub04x.DENGHI_KIENXUAT;
                    data.DataDetails =
                        await _bieub04xDetailService.Queryable().Where(x => x.PHB_C_B04X_REFID.Equals(refid))
                            .OrderBy(x => x.ID).ThenBy(x => x.CHI_TIEU)
                            .ToListAsync();
                    data.DataDetailTSCD =
                        await _bieub04xDetailTSCDService.Queryable().Where(x => x.PHB_C_B04X_REFID.Equals(refid))
                            .OrderBy(x => x.ID).ThenBy(x => x.CHI_TIEU)
                            .ToListAsync();
                    response.Error = false;
                    response.Data = data;
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
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

        [HttpPost]
        [Route("Post")]
        public async Task<IHttpActionResult> Post(C_B04XVm.InsertModel model)
        {
            model.MA_QHNS = "NoN";
            model.MA_CHUONG = "NoN";
            var maDiaBanHanhChinhCha = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DataDetails.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_C_B04X bieub04x = new PHB_C_B04X()
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
                    MA_DBHC = model.MA_DBHC,
                    MA_DBHC_CHA = maDiaBanHanhChinhCha?.Value,
                    TEN_QHNS = model.TEN_QHNS,
                    DIEN_TICH = model.DIEN_TICH,
                    DIEN_TICH_DAT = model.DIEN_TICH_DAT,
                    DANSO = model.DANSO,
                    NGANH_NGHE = model.NGANH_NGHE,
                    MUCTIEU_NHIEMVU = model.MUCTIEU_NHIEMVU,
                    DANH_GIA = model.DANH_GIA,
                    NGUYEN_NHAN = model.NGUYEN_NHAN,
                    KHACH_QUAN = model.KHACH_QUAN,
                    CHU_QUAN = model.CHU_QUAN,
                    DENGHI_KIENXUAT = model.DENGHI_KIENXUAT,
                };
                var checkReport = await _bieub04xService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_DBHC.Equals(bieub04x.MA_DBHC) &&
                    x.NAM_BC == bieub04x.NAM_BC && x.KY_BC == bieub04x.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieub04xService.Insert(bieub04x);
                foreach (var detail in model.DataDetails)
                {
                    detail.PHB_C_B04X_REFID = bieub04x.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieub04xDetailService.Insert(detail);
                }

                foreach (var detailTSCD in model.DataDetailTSCD)
                {
                    detailTSCD.PHB_C_B04X_REFID = bieub04x.REFID;
                    detailTSCD.ObjectState = ObjectState.Added;
                    _bieub04xDetailTSCDService.Insert(detailTSCD);
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
        public async Task<IHttpActionResult> Put(C_B04XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B04X bieub04x = await _bieub04xService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieub04x == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                bieub04x.KY_BC = model.KY_BC;
                bieub04x.NAM_BC = model.NAM_BC;
                bieub04x.MA_CHUONG = model.MA_CHUONG;
                bieub04x.MA_QHNS = model.MA_QHNS;
                bieub04x.NGAY_TAO = model.NGAY_TAO;
                bieub04x.NGUOI_TAO = model.NGUOI_TAO;
                bieub04x.TRANG_THAI = model.TRANG_THAI;
                bieub04x.REFID = model.REFID;
                bieub04x.MA_DBHC = model.MA_DBHC;
                bieub04x.MA_DBHC_CHA = model.MA_DBHC_CHA;
                bieub04x.TEN_QHNS = model.TEN_QHNS;
                bieub04x.DIEN_TICH = model.DIEN_TICH;
                bieub04x.DIEN_TICH_DAT = model.DIEN_TICH_DAT;
                bieub04x.DANSO = model.DANSO;
                bieub04x.NGANH_NGHE = model.NGANH_NGHE;
                bieub04x.MUCTIEU_NHIEMVU = model.MUCTIEU_NHIEMVU;
                bieub04x.DANH_GIA = model.DANH_GIA;
                bieub04x.NGUYEN_NHAN = model.NGUYEN_NHAN;
                bieub04x.KHACH_QUAN = model.KHACH_QUAN;
                bieub04x.CHU_QUAN = model.CHU_QUAN;
                bieub04x.DENGHI_KIENXUAT = model.DENGHI_KIENXUAT;
                #region Edit
                if (model.DataDetails != null && model.DataDetails.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.DataDetails)
                    {
                        PHB_C_B04X_DETAIL detail = await _bieub04xDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.CHI_TIEU = item.CHI_TIEU;
                            detail.SO_DAUNAM = item.SO_DAUNAM;
                            detail.PHATSINH_TANG = item.PHATSINH_TANG;
                            detail.PHATSINH_GIAM = item.PHATSINH_GIAM;
                            detail.CUOINAM = item.CUOINAM;
                            _bieub04xDetailService.Update(detail);
                        }
                    }
                }

                if (model.DataDetailTSCD != null && model.DataDetailTSCD.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.DataDetailTSCD)
                    {
                        PHB_C_B04X_DETAIL_TSCD detailTSCD = await _bieub04xDetailTSCDService.FindByIdAsync(item.ID);
                        if (detailTSCD != null)
                        {
                            detailTSCD.ObjectState = ObjectState.Modified;
                            detailTSCD.CHI_TIEU = item.CHI_TIEU;
                            detailTSCD.DON_VI_TINH = item.DON_VI_TINH;

                            detailTSCD.DAUNAM_SL = item.DAUNAM_SL;
                            detailTSCD.DAUNAM_NG = item.DAUNAM_NG;

                            detailTSCD.TANG_SL = item.TANG_SL;
                            detailTSCD.TANG_NG = item.TANG_NG;

                            detailTSCD.GIAM_SL = item.GIAM_SL   ;
                            detailTSCD.GIAM_NG = item.GIAM_NG;

                            detailTSCD.CUOINAM_SL = item.CUOINAM_SL;
                            detailTSCD.CUOINAM_NG = item.CUOINAM_NG;

                            _bieub04xDetailTSCDService.Update(detailTSCD);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieub04x.NGAY_SUA = DateTime.Now;
                    bieub04x.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieub04xService.Update(bieub04x);

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

        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            //var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.ToArray();
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    Stream stream;
                    if (httpRequest.Files[0].FileName.EndsWith(".xls"))
                    {
                        stream = ConvertToXlsx(HttpContext.Current.Request.Files[0].InputStream);
                    }
                    else
                    {
                        stream = HttpContext.Current.Request.Files[0].InputStream;
                    }

                    using (var excelPackage = new ExcelPackage(stream))
                    {
                        PHB_C_B04X report = new PHB_C_B04X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            MA_DBHC_CHA = madbhc[3].Value
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            report.MA_CHUONG = "423";
                            report.MA_QHNS = "1032433";
                            report.MA_DBHC = httpRequest["MA_DBHC"];
                            report.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            report.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            report.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieub04xService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(report.MA_DBHC) && x.MA_DBHC.Equals(report.MA_DBHC) &&
                                x.NAM_BC == report.NAM_BC && x.KY_BC == report.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            var startRow = workSheet.Dimension.Start.Row;
                            var endRow = workSheet.Dimension.End.Row;
                            var startCol = workSheet.Dimension.Start.Column;
                            var endCol = workSheet.Dimension.End.Column;
                            var startRowCongNo = 0;
                            var startRowTSCD = 0;

                            for(var i = startRow; i <= endRow; i++)
                            {
                                for(var j = startCol; j < endCol; j++)
                                {
                                    var cellstr = workSheet.Cells[i, j].Value?.ToString()?.Trim();

                                    if(cellstr != null)
                                    {
                                        if (cellstr.ToLower().StartsWith("- diện tích:"))
                                        {
                                            var dienTichStr = cellstr.ToLower().Split(':')[1].Trim();
                                            report.DIEN_TICH = decimal.Parse(dienTichStr == "" ? "0" : dienTichStr);
                                        }

                                        if (cellstr.ToLower().StartsWith("- diện tích đất"))
                                        {
                                            var dientichDatStr = cellstr.ToLower().Split(':')[1].Trim();
                                            report.DIEN_TICH_DAT = decimal.Parse(dientichDatStr == "" ? "0" : dientichDatStr);
                                        }

                                        if (cellstr.ToLower().StartsWith("- dân số"))
                                        {
                                            var danSoStr = "";
                                            if (cellstr.ToLower().StartsWith("- dân số đến:"))
                                            {
                                                danSoStr = cellstr.ToLower().Replace("- dân số đến:", "").Trim();
                                            }
                                            else if (cellstr.ToLower().StartsWith("- dân số đến"))
                                            {
                                                danSoStr = cellstr.ToLower().Replace("- dân số đến", "").Trim();
                                            }

                                            report.DANSO = decimal.Parse(danSoStr == "" ? "0" : danSoStr);
                                        }

                                        if (cellstr.ToLower().StartsWith("- ngành nghề"))
                                        {
                                            var nganhNgheStr = cellstr.ToLower().Split(':')[1].Trim();
                                            report.NGANH_NGHE = nganhNgheStr;
                                        }

                                        if (cellstr.ToLower().StartsWith("- mục tiêu"))
                                        {
                                            var mucTieuStr = cellstr.ToLower().Split(':')[1].Trim();
                                            report.MUCTIEU_NHIEMVU = mucTieuStr;
                                        }

                                        if (cellstr.ToLower().StartsWith("- đánh giá thu"))
                                        {
                                            var mucTieuStr = cellstr.ToLower().Split(':')[1].Trim();
                                            report.DANH_GIA = mucTieuStr;
                                        }

                                        if (cellstr.ToLower().StartsWith("- nguyên nhân"))
                                        {
                                            var mucTieuStr = cellstr.ToLower().Split(':')[1].Trim();
                                            report.NGUYEN_NHAN = mucTieuStr;
                                        }

                                        if (cellstr.ToLower().Contains("các khoản phải thu"))
                                        {
                                            startRowCongNo = i;
                                        }

                                        if (cellstr.ToLower().StartsWith("thiết bị, dụng cụ quản lý"))
                                        {
                                            startRowTSCD = i;
                                        }
                                    }
                                }
                            }

                            _bieub04xService.Insert(report);

                            for(var i = startRowCongNo; i < startRowCongNo + 5; i++)
                            {
                                var detail = new PHB_C_B04X_DETAIL
                                {
                                    PHB_C_B04X_REFID = report.REFID,
                                    CHI_TIEU = workSheet.Cells[i, 2]?.Value?.ToString()?.Trim(),
                                    SO_DAUNAM = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 6]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 6]?.Value?.ToString()?.Trim()),
                                    PHATSINH_TANG = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 8]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 8]?.Value?.ToString()?.Trim()),
                                    PHATSINH_GIAM = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 11]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 11]?.Value?.ToString()?.Trim()),
                                    CUOINAM = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 15]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 15]?.Value?.ToString()?.Trim())
                                };

                                _bieub04xDetailService.Insert(detail);
                            }

                            for(var i = startRowTSCD; i < startRowTSCD + 2; i++)
                            {
                                var detailTSCD = new PHB_C_B04X_DETAIL_TSCD
                                {
                                    PHB_C_B04X_REFID = report.REFID,
                                    STT = workSheet.Cells[i, 2]?.Value?.ToString()?.Trim(),
                                    CHI_TIEU = workSheet.Cells[i, 4]?.Value?.ToString()?.Trim(),
                                    DON_VI_TINH = workSheet.Cells[i, 6]?.Value?.ToString()?.Trim(),

                                    DAUNAM_SL = int.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 7]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 7]?.Value.ToString()?.Trim()),
                                    DAUNAM_NG = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 9]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 9]?.Value.ToString()?.Trim()),

                                    TANG_SL = int.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 12]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 12]?.Value?.ToString()?.Trim()),
                                    TANG_NG = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 14]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 14]?.Value?.ToString()?.Trim()),

                                    GIAM_SL = int.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 16]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 16]?.Value.ToString()?.Trim()),
                                    GIAM_NG = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 21]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 21]?.Value?.ToString()?.Trim()),

                                    CUOINAM_SL = int.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 22]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 22]?.Value?.ToString()?.Trim()),
                                    CUOINAM_NG = decimal.Parse(string.IsNullOrEmpty(workSheet.Cells[i, 24]?.Value?.ToString()?.Trim()) ? "0" : workSheet.Cells[i, 24]?.Value?.ToString()?.Trim()),
                                };

                                _bieub04xDetailTSCDService.Insert(detailTSCD);
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
                    response.Message = ErrorMessage.EMPTY_DATA;
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
            var response = await _bieub04xService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.CHI_TIEU == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B04X_DETAIL detail = await _bieub04xDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.CHI_TIEU_OLD = entry.CHI_TIEU;
                                detail.CHI_TIEU = rqmodel.newName;
                                _bieub04xDetailService.Update(detail);
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
            var response = await _bieub04xService.SumReport_HTML(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC);
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
                var foundLst = await _bieub04xDetailService.Queryable()
                    .Where(x => x.CHI_TIEU == rqmodel.CHI_TIEU && x.CHI_TIEU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_C_B04X_DETAIL detail = await _bieub04xDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.CHI_TIEU = entry.CHI_TIEU_OLD;
                        detail.CHI_TIEU_OLD = null;
                        _bieub04xDetailService.Update(detail);
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

        private Stream ConvertToXlsx(Stream inputStream)
        {
            var fileDir = System.Web.Hosting.HostingEnvironment.MapPath(@"~/UploadFile/ExcelTemp");
            var xlsFileName = Guid.NewGuid() + ".xls";
            var xlsxFileName = xlsFileName + "x";
            var xlsFilePath = Path.Combine(fileDir, xlsFileName);
            var xlsxFilePath = Path.Combine(fileDir, xlsxFileName);

            SaveStreamAsFile(fileDir, inputStream, xlsFileName);


            var app = new Microsoft.Office.Interop.Excel.Application();
            var wb = app.Workbooks.Open(xlsFilePath);
            wb.SaveAs(Filename: xlsxFilePath, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            wb.Close();
            app.Quit();

            byte[] fileData = File.ReadAllBytes(xlsxFilePath);
            var stream = new MemoryStream(fileData);

            File.Delete(xlsFilePath);
            File.Delete(xlsxFilePath);

            return stream;
        }

        private static void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }
        }
    }
}
