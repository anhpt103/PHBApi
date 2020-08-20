using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP2;
using BTS.SP.PHB.SERVICE.Models.BIEU3BP2;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3BP2;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBieu3bp2")]
    [Route("{id?}")]
    public class PhbBieu3BP2Controller : ApiController
    {
        private readonly IPhbBieu3BP2Service _bieu3BP2Service;
        private readonly IPhbBieu3BP2DetailService _bieu3BP2DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu3BP2Controller(IPhbBieu3BP2Service bieu3Bp2Service, IPhbBieu3BP2DetailService bieu3Bp2DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu3BP2Service = bieu3Bp2Service;
            _bieu3BP2DetailService = bieu3Bp2DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var response = new Response();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            var bieu3Bp2 = new PHB_BIEU3BP2()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N")
                            };
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu3Bp2.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu3Bp2.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu3Bp2.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu3Bp2.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu3Bp2.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu3Bp2.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu3BP2Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu3Bp2.MA_CHUONG) && x.MA_QHNS.Equals(bieu3Bp2.MA_QHNS) &&
                                x.NAM_BC == bieu3Bp2.NAM_BC && x.KY_BC == bieu3Bp2.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu3BP2Service.Insert(bieu3Bp2);

                            var startRowPhan2 = 12;
                            var inRow = 0;
                            while (workSheet.Cells[startRowPhan2 + inRow, 5].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRowPhan2 + inRow, 5].Value.ToString()))
                            {
                                var temp = startRowPhan2 + inRow;
                                var detail = new PHB_BIEU3BP2_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU3BP2_REFID = bieu3Bp2.REFID
                                };
                                detail.MA_LOAI = workSheet.Cells[temp, 1].Value != null
                                    ? workSheet.Cells[temp, 1].Value.ToString()
                                    : null;
                                detail.MA_KHOAN = workSheet.Cells[temp, 2].Value != null
                                    ? workSheet.Cells[temp, 2].Value.ToString()
                                    : null;
                                detail.MA_MUC = workSheet.Cells[temp, 3].Value != null
                                    ? workSheet.Cells[temp, 3].Value.ToString()
                                    : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[temp, 4].Value != null
                                    ? workSheet.Cells[temp, 4].Value.ToString()
                                    : null;
                                detail.NOI_DUNG_CHI = workSheet.Cells[temp, 5].Value != null
                                    ? workSheet.Cells[temp, 5].Value.ToString()
                                    : null;
                                detail.NSTN_SOBC = workSheet.Cells[temp, 9].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 9].Value.ToString())
                                    : 0;
                                detail.NSTN_SOXDTD = workSheet.Cells[temp, 10].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 10].Value.ToString())
                                    : 0;
                                detail.VT_SOBC = workSheet.Cells[temp, 12].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 12].Value.ToString())
                                    : 0;
                                detail.VT_SOXDTD = workSheet.Cells[temp, 13].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 13].Value.ToString())
                                    : 0;
                                detail.VN_SOBC = workSheet.Cells[temp, 15].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 15].Value.ToString())
                                    : 0;
                                detail.VN_SOXDTD = workSheet.Cells[temp, 16].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 16].Value.ToString())
                                    : 0;
                                detail.PKT_SOBC = workSheet.Cells[temp, 18].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 18].Value.ToString())
                                    : 0;
                                detail.PKT_SOXDTD = workSheet.Cells[temp, 19].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 19].Value.ToString())
                                    : 0;
                                detail.HDKDL_SOBC = workSheet.Cells[temp, 20].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 20].Value.ToString())
                                    : 0;
                                detail.HDKDL_SOXDTD = workSheet.Cells[temp, 21].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 20].Value.ToString())
                                    : 0;
                                _bieu3BP2DetailService.Insert(detail);
                                inRow++;
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
                            response.Message = ErrorMessage.EMPTY_DATA;
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }
                catch (DbEntityValidationException ex)
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
            var response = new Response<BIEU3BP2Vm.ViewModel>();
            try
            {
                var model = new BIEU3BP2Vm.ViewModel();
                model.REFID = refid;
                model.DETAIL = await _bieu3BP2DetailService.Queryable().Where(x => x.PHB_BIEU3BP2_REFID.Equals(refid))
                    .OrderBy(x=>x.MA_LOAI).ThenBy(x=>x.MA_KHOAN).ThenBy(x=>x.MA_MUC).ThenBy(x=>x.MA_TIEU_MUC)
                    .ToListAsync();
                response.Error = false;
                response.Data = model;
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
        public async Task<IHttpActionResult> Post(BIEU3BP2Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu3Bp2 = new PHB_BIEU3BP2()
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
                var checkReport = await _bieu3BP2Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu3Bp2.MA_CHUONG) && x.MA_QHNS.Equals(bieu3Bp2.MA_QHNS) &&
                    x.NAM_BC == bieu3Bp2.NAM_BC && x.KY_BC == bieu3Bp2.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu3BP2Service.Insert(bieu3Bp2);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU3BP2_REFID = bieu3Bp2.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu3BP2DetailService.Insert(detail);
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
            catch (DbEntityValidationException ex)
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
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(BIEU3BP2Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu3Bp2 =
                    await _bieu3BP2Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu3Bp2 == null) return Ok(new Response() {Error = true, Message = ErrorMessage.NOT_FOUND});

                #region delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var id in model.LstDelete)
                    {
                        var item = await _bieu3BP2DetailService.FindByIdAsync(id);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu3BP2DetailService.Delete(item);
                        }
                    }
                }

                #endregion

                #region add

                if (model.LstAdd != null && model.LstAdd.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstAdd)
                    {
                        item.ObjectState = ObjectState.Added;
                        item.PHB_BIEU3BP2_REFID = bieu3Bp2.REFID;
                        _bieu3BP2DetailService.Insert(item);
                    }
                }

                #endregion

                #region edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _bieu3BP2DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.NSTN_SOBC = item.NSTN_SOBC;
                            detail.NSTN_SOXDTD = item.NSTN_SOXDTD;
                            detail.VT_SOBC = item.VT_SOBC;
                            detail.VT_SOXDTD = item.VT_SOXDTD;
                            detail.VN_SOBC = item.VN_SOBC;
                            detail.VN_SOXDTD = item.VN_SOXDTD;
                            detail.PKT_SOBC = item.PKT_SOBC;
                            detail.PKT_SOXDTD = item.PKT_SOXDTD;
                            detail.HDKDL_SOBC = item.HDKDL_SOBC;
                            detail.HDKDL_SOXDTD = item.HDKDL_SOXDTD;
                            _bieu3BP2DetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    bieu3Bp2.NGAY_SUA = DateTime.Now;
                    bieu3Bp2.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu3BP2Service.Update(bieu3Bp2);
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
            catch (DbEntityValidationException ex)
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
            return Ok(response);
        }
    }
}
