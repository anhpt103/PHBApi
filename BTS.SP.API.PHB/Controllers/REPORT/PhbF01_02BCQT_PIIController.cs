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
using BTS.SP.PHB.ENTITY.Rp.F01_02BCQT_PII;
using BTS.SP.PHB.SERVICE.Models.F01_02BCQT_PII;
using BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT_PII;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Security.Claims;
using System.IO;
using System.Net.Http.Headers;
using BTS.SP.PHB.ENTITY.Dm;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbF01_02BCQT_PII")]
    [Route("{id?}")]
    public class PhbF01_02BCQT_PIIController : ApiController
    {
        private readonly IF01_02BCQT_PIIService _f0102Bcqtp2Service;
        private readonly IF01_02BCQT_PIIDetailService _f0102Bcqtp2DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbF01_02BCQT_PIIController(IF01_02BCQT_PIIService f0102Bcqtp2Service, IF01_02BCQT_PIIDetailService f0102Bcqtp2DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _f0102Bcqtp2Service = f0102Bcqtp2Service;
            _f0102Bcqtp2DetailService = f0102Bcqtp2DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_F01_02BCQT_PII bieuF01_02 = new PHB_F01_02BCQT_PII()
                        {
                            NGAY_TAO = DateTime.Now,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            ObjectState = ObjectState.Added,
                            TRANG_THAI = 0,
                            REFID = Guid.NewGuid().ToString("N")
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieuF01_02.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieuF01_02.MA_QHNS = httpRequest["MA_QHNS"];
                            bieuF01_02.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieuF01_02.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieuF01_02.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieuF01_02.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _f0102Bcqtp2Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieuF01_02.MA_CHUONG) && x.MA_QHNS.Equals(bieuF01_02.MA_QHNS) &&
                                x.NAM_BC == bieuF01_02.NAM_BC && x.KY_BC == bieuF01_02.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _f0102Bcqtp2Service.Insert(bieuF01_02);

                            //string noidungchi = string.Empty;
                            int startRow = 15;
                            var loai = 1;
                            while (workSheet.Cells[startRow, 5].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 5].Value.ToString()))
                            {
                                {
                                    //chi tieu danh mục
                                    //noidungchi = workSheet.Cells[startRow, 5].Value.ToString();
                                    PHB_F01_02BCQT_PII_DETAIL detail = new PHB_F01_02BCQT_PII_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_F01_02BCQT_PII_REFID = bieuF01_02.REFID,
                                        LOAI = loai,
                                    };
                                    //switch (noidungchi)
                                    //{
                                    //    case "I. Kinh phí thường xuyên/ tự chủ":
                                    //        startRow += 1;
                                    //        break;
                                    //    case "II. Kinh phí không thường xuyên/không tự chủ":
                                    //        startRow += 1;
                                    //        loai++;
                                    //        detail.LOAI = loai;
                                    //        break;
                                    //}
                                    detail.NOI_DUNG_CHI = workSheet.Cells[startRow, 5].Value != null
                                    ? workSheet.Cells[startRow, 5].Value.ToString()
                                    : null;
                                    detail.MA_LOAI = workSheet.Cells[startRow, 1].Value != null
                                    ? workSheet.Cells[startRow, 1].Value.ToString()
                                    : null;
                                    detail.MA_KHOAN = workSheet.Cells[startRow, 2].Value != null
                                    ? workSheet.Cells[startRow, 2].Value.ToString()
                                    : null;
                                    detail.MA_MUC = workSheet.Cells[startRow, 3].Value != null
                                    ? workSheet.Cells[startRow, 3].Value.ToString()
                                    : null;
                                    detail.MA_TIEU_MUC = workSheet.Cells[startRow, 4].Value != null
                                    ? workSheet.Cells[startRow, 4].Value.ToString()
                                    : null;
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_NAM_NAY = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_NAM_NAY = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSNN_TRONGNUOC_NAM_NAY = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSNN_TRONGNUOC_NAM_NAY = 0;
                                    }

                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.VIEN_TRO_NAM_NAY = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VIEN_TRO_NAM_NAY = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.VAYNO_NUOCNGOAI_NAM_NAY = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VAYNO_NUOCNGOAI_NAM_NAY = 0;
                                    }
                                    if (workSheet.Cells[startRow, 10].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_LUY_KE = double.Parse(workSheet.Cells[startRow, 10].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_LUY_KE = 0;
                                    }
                                    if (workSheet.Cells[startRow, 11].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSNN_TRONGNUOC_LUY_KE = double.Parse(workSheet.Cells[startRow, 11].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSNN_TRONGNUOC_LUY_KE = 0;
                                    }

                                    if (workSheet.Cells[startRow, 12].Value != null)
                                    {
                                        try
                                        {
                                            detail.VIEN_TRO_LUY_KE = double.Parse(workSheet.Cells[startRow, 12].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VIEN_TRO_LUY_KE = 0;
                                    }
                                    if (workSheet.Cells[startRow, 13].Value != null)
                                    {
                                        try
                                        {
                                            detail.VAYNO_NUOCNGOAI_LUY_KE = double.Parse(workSheet.Cells[startRow, 13].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VAYNO_NUOCNGOAI_LUY_KE = 0;
                                    }
                                    _f0102Bcqtp2DetailService.Insert(detail);
                                    startRow += 1;
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
            Response<F01_02BCQT_PIIVm.ViewModel> response = new Response<F01_02BCQT_PIIVm.ViewModel>();
            try
            {
                F01_02BCQT_PIIVm.ViewModel data = new F01_02BCQT_PIIVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _f0102Bcqtp2DetailService.Queryable().Where(x => x.PHB_F01_02BCQT_PII_REFID.Equals(refid))
                    .OrderBy(x => x.LOAI).ThenBy(x=>x.MA_LOAI).ThenBy(x=>x.MA_KHOAN).ThenBy(x=>x.MA_MUC).ThenBy(x=>x.MA_TIEU_MUC)
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

        [HttpPost]
        public async Task<IHttpActionResult> Post(F01_02BCQT_PIIVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_F01_02BCQT_PII f0102Bcqtp2 = new PHB_F01_02BCQT_PII()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = model.MA_CHUONG,
                    MA_QHNS = model.MA_QHNS,
                    TEN_QHNS = model.TEN_QHNS,
                    MA_QHNS_CHA = model.MA_QHNS_CHA,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                };

                var checkReport = await _f0102Bcqtp2Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(f0102Bcqtp2.MA_CHUONG) && x.MA_QHNS.Equals(f0102Bcqtp2.MA_QHNS) &&
                    x.NAM_BC == f0102Bcqtp2.NAM_BC && x.KY_BC == f0102Bcqtp2.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _f0102Bcqtp2Service.Insert(f0102Bcqtp2);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_F01_02BCQT_PII_REFID = f0102Bcqtp2.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _f0102Bcqtp2DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(F01_02BCQT_PIIVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_F01_02BCQT_PII bieuF01_02BCQT_PII =
                    await _f0102Bcqtp2Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieuF01_02BCQT_PII == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_F01_02BCQT_PII_DETAIL item = await _f0102Bcqtp2DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _f0102Bcqtp2DetailService.Delete(item);
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
                        //item.LOAI = 3;
                        item.PHB_F01_02BCQT_PII_REFID = model.REFID;
                        _f0102Bcqtp2DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_F01_02BCQT_PII_DETAIL detail = await _f0102Bcqtp2DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.TONG_SO_NAM_NAY = item.TONG_SO_NAM_NAY;
                            detail.NSNN_TRONGNUOC_NAM_NAY = item.NSNN_TRONGNUOC_NAM_NAY;
                            detail.VIEN_TRO_NAM_NAY = item.VIEN_TRO_NAM_NAY;
                            detail.VAYNO_NUOCNGOAI_NAM_NAY = item.VAYNO_NUOCNGOAI_NAM_NAY;
                            detail.TONG_SO_LUY_KE = item.TONG_SO_LUY_KE;
                            detail.NSNN_TRONGNUOC_LUY_KE = item.NSNN_TRONGNUOC_LUY_KE;
                            detail.VIEN_TRO_LUY_KE = item.VIEN_TRO_LUY_KE;
                            detail.VAYNO_NUOCNGOAI_LUY_KE = item.VAYNO_NUOCNGOAI_LUY_KE;
                            _f0102Bcqtp2DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieuF01_02BCQT_PII.NGAY_SUA = DateTime.Now;
                    bieuF01_02BCQT_PII.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _f0102Bcqtp2Service.Update(bieuF01_02BCQT_PII);

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
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = "-1";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc = firstOrDefault.Value;
            }
            var response = await _f0102Bcqtp2Service.SumReport(madbhc,rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

       



    }
}
