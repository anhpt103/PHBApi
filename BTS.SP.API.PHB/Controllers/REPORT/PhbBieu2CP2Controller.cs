using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP2;
using BTS.SP.PHB.SERVICE.REPORT.BIEU2CP2;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU2CP2")]
    [Route("{id?}")]
    public class PhbBieu2CP2Controller : ApiController
    {
       
        private readonly IPhbBIEU2CP2Service _BIEU2CP2Service;
        private readonly IPhbBIEU2CP2DetailService _BIEU2CP2DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu2CP2Controller(IPhbBIEU2CP2Service BIEU2CP2Service,
            IPhbBIEU2CP2DetailService BIEU2CP2DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {           
            _BIEU2CP2Service = BIEU2CP2Service;           
            _BIEU2CP2DetailService = BIEU2CP2DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region UploadData 
        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var httpRequest = HttpContext.Current.Request;

            var response = new Response<string>();
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_BIEU2CP2 BIEU2CP2 = new PHB_BIEU2CP2()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            TRANG_THAI = 0
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            BIEU2CP2.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            BIEU2CP2.MA_QHNS = httpRequest["MA_QHNS"];
                            BIEU2CP2.TEN_QHNS = httpRequest["TEN_QHNS"];
                            BIEU2CP2.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            BIEU2CP2.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            BIEU2CP2.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _BIEU2CP2Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(BIEU2CP2.MA_CHUONG) && x.MA_QHNS.Equals(BIEU2CP2.MA_QHNS) &&
                                x.NAM_BC == BIEU2CP2.NAM_BC && x.KY_BC == BIEU2CP2.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _BIEU2CP2Service.Insert(BIEU2CP2);

                            int start_Row = 16;                            

                            for (int r = start_Row; r <= workSheet.Dimension.End.Row; r++)
                            {
                                var obj = new PHB_BIEU2CP2_DETAIL() { PHB_BIEU2CP2_REFID = BIEU2CP2.REFID, ObjectState = ObjectState.Added };
                                obj.MA_LOAI = workSheet.Cells[r, 1].Text;
                                obj.MA_KHOAN = workSheet.Cells[r, 2].Text;
                                obj.MA_MUC = workSheet.Cells[r, 3].Text;
                                obj.MA_TIEU_MUC = workSheet.Cells[r, 4].Text;
                                obj.NOI_DUNG_CHI = workSheet.Cells[r, 5].Text;

                                obj.NS_TRONGNUOC = workSheet.Cells[r, 7].Value != null ? decimal.Parse(workSheet.Cells[r, 7].Value.ToString()) : 0;
                                obj.VIEN_TRO = workSheet.Cells[r, 8].Value != null ? decimal.Parse(workSheet.Cells[r, 8].Value.ToString()) : 0;
                                obj.VAY_NO_NN = workSheet.Cells[r, 9].Value != null ? decimal.Parse(workSheet.Cells[r, 9].Value.ToString()) : 0;
                                obj.PHI_KT_DELAI = workSheet.Cells[r, 10].Value != null ? decimal.Parse(workSheet.Cells[r, 10].Value.ToString()) : 0;
                                obj.NGUON_KHAC = workSheet.Cells[r, 11].Value != null ? decimal.Parse(workSheet.Cells[r, 11].Value.ToString()) : 0;
                                _BIEU2CP2DetailService.Insert(obj);
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = BIEU2CP2.REFID;
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


        #region Get Detail
        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            var response = new Response<PHB_BIEU2CP2Vm.DataRes>();
            var res = new PHB_BIEU2CP2Vm.DataRes();
            try
            {
                // lay nam BC
                var ins = _BIEU2CP2Service.GetAll().FirstOrDefault(x => x.REFID.Equals(refid));                
                var data = _BIEU2CP2DetailService.GetAll().Where(x=>x.PHB_BIEU2CP2_REFID.Equals(refid)).ToList();

                res.Body = data;
                res.NAM_BC = ins.NAM_BC;
                response.Error = false;
                response.Data = res;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }

            return Ok(response);
        }
        #endregion


        #region Update
        [HttpPut]
        public async Task<IHttpActionResult> Put(PHB_BIEU2CP2Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu2cp2 =
                    await _BIEU2CP2Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu2cp2 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var id in model.LstDelete)
                    {
                        var item = await _BIEU2CP2DetailService.FindByIdAsync(id);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _BIEU2CP2DetailService.Delete(item);
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
                        item.PHB_BIEU2CP2_REFID = bieu2cp2.REFID;
                        _BIEU2CP2DetailService.Insert(item);
                    }
                }

                #endregion

                #region edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _BIEU2CP2DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.NS_TRONGNUOC = item.NS_TRONGNUOC;
                            detail.VIEN_TRO = item.VIEN_TRO;
                            detail.VAY_NO_NN = item.VAY_NO_NN;
                            detail.PHI_KT_DELAI = item.PHI_KT_DELAI;
                            detail.NGUON_KHAC = item.NGUON_KHAC;
                            _BIEU2CP2DetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    bieu2cp2.NGAY_SUA = DateTime.Now;
                    bieu2cp2.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _BIEU2CP2Service.Update(bieu2cp2);
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
        #endregion
    }
}