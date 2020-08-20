using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU07TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU_07TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu07TT344;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU07TT344")]
    [Route("{id?}")]
    public class PhbBieu07TT344Controller : ApiController
    {
        private readonly IPhbBieu07TT344Service _bieu07TT344Service;
        private readonly IPhbBieu07TT344TemplateService _bieu07TT344TemplateService;
        private readonly IPhbBieu07TT344DetailService _bieu07TT344DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu07TT344Controller(IPhbBieu07TT344Service bieu07TT344Service, IPhbBieu07TT344TemplateService bieu07TT344TemplateService,
            IPhbBieu07TT344DetailService bieu07TT344DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu07TT344Service = bieu07TT344Service;
            _bieu07TT344TemplateService = bieu07TT344TemplateService;
            _bieu07TT344DetailService = bieu07TT344DetailService;
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
                        PHB_BIEU07TT344 bieu07 = new PHB_BIEU07TT344()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu07.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu07.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu07.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu07.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu07.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu07.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu07TT344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu07.MA_CHUONG) && x.MA_QHNS.Equals(bieu07.MA_QHNS) &&
                                x.NAM_BC == bieu07.NAM_BC && x.KY_BC == bieu07.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu07TT344Service.Insert(bieu07);

                            PHB_BIEU07TT344_DETAIL detail = new PHB_BIEU07TT344_DETAIL()
                            {
                                PHB_BIEU07TT344_REFID = bieu07.REFID,
                                ObjectState = ObjectState.Added
                            };

                            detail.TONGTHU = double.Parse(workSheet.Cells[9, 2].Text.Trim());
                            detail.THU_XAHUONG100 = double.Parse(workSheet.Cells[10, 2].Text.Trim());
                            detail.THU_CHIATYLE = double.Parse(workSheet.Cells[11, 2].Text.Trim());
                            detail.THU_BOSUNG = double.Parse(workSheet.Cells[12, 2].Text.Trim());
                            detail.THU_BOSUNGCANDOINS = double.Parse(workSheet.Cells[13, 2].Text.Trim());
                            detail.THU_BOSUNGCOMUCTIEU = double.Parse(workSheet.Cells[14, 2].Text.Trim());
                            detail.THU_KETDUNSNAMTRUOC = double.Parse(workSheet.Cells[15, 2].Text.Trim());
                            detail.THU_VIENTRO = double.Parse(workSheet.Cells[16, 2].Text.Trim());
                            detail.THU_CHUYENNGUON = double.Parse(workSheet.Cells[17, 2].Text.Trim());

                            detail.TONGCHI = double.Parse(workSheet.Cells[9, 4].Text.Trim());
                            detail.CHI_DAUTUPT = double.Parse(workSheet.Cells[10, 4].Text.Trim());
                            detail.CHI_THUONGXUYEN = double.Parse(workSheet.Cells[11, 4].Text.Trim());
                            detail.CHI_CHUYENNGUON = double.Parse(workSheet.Cells[12, 4].Text.Trim());
                            detail.CHI_NOPTRANS = double.Parse(workSheet.Cells[13, 4].Text.Trim());

                            detail.KETDUNS = double.Parse(workSheet.Cells[18, 2].Text.Trim());

                            _bieu07TT344DetailService.Insert(detail);
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
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = ex.Message;
                }
            }
            else
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
            }
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_BIEU07TT344 bieu07 = new PHB_BIEU07TT344()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            bieu07.MA_CHUONG = "423";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu07.MA_QHNS = "1032433";
                            //bieu07.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu07.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu07.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            bieu07.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu07.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu07.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu07.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu07TT344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu07.MA_DBHC) && x.MA_QHNS.Equals(bieu07.MA_QHNS) &&
                                x.NAM_BC == bieu07.NAM_BC && x.KY_BC == bieu07.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu07TT344Service.Insert(bieu07);

                            PHB_BIEU07TT344_DETAIL detail = new PHB_BIEU07TT344_DETAIL()
                            {
                                PHB_BIEU07TT344_REFID = bieu07.REFID,
                                ObjectState = ObjectState.Added
                            };
                            detail.TONGTHU = double.Parse(workSheet.Cells[9, 2].Text.Trim() == "" ? "0" : workSheet.Cells[9, 2].Text.Trim());
                            detail.THU_XAHUONG100 = double.Parse(workSheet.Cells[10, 2].Text.Trim() == "" ? "0" : workSheet.Cells[10, 2].Text.Trim());
                            detail.THU_CHIATYLE = double.Parse(workSheet.Cells[11, 2].Text.Trim() == "" ? "0" : workSheet.Cells[11, 2].Text.Trim());
                            detail.THU_BOSUNG = double.Parse(workSheet.Cells[12, 2].Text.Trim() == "" ? "0" : workSheet.Cells[12, 2].Text.Trim());
                            detail.THU_BOSUNGCANDOINS = double.Parse(workSheet.Cells[13, 2].Text.Trim() == "" ? "0" : workSheet.Cells[13, 2].Text.Trim());
                            detail.THU_BOSUNGCOMUCTIEU = double.Parse(workSheet.Cells[14, 2].Text.Trim() == "" ? "0" : workSheet.Cells[14, 2].Text.Trim());
                            detail.THU_KETDUNSNAMTRUOC = double.Parse(workSheet.Cells[15, 2].Text.Trim() == "" ? "0" : workSheet.Cells[15, 2].Text.Trim());
                            detail.THU_VIENTRO = double.Parse(workSheet.Cells[16, 2].Text.Trim() == "" ? "0" : workSheet.Cells[16, 2].Text.Trim());
                            detail.THU_CHUYENNGUON = double.Parse(workSheet.Cells[17, 2].Text.Trim() == "" ? "0" : workSheet.Cells[17, 2].Text.Trim());

                            detail.TONGCHI = double.Parse(workSheet.Cells[9, 4].Text.Trim() == "" ? "0" : workSheet.Cells[9, 4].Text.Trim());
                            detail.CHI_DAUTUPT = double.Parse(workSheet.Cells[10, 4].Text.Trim() == "" ? "0" : workSheet.Cells[10, 4].Text.Trim());
                            detail.CHI_THUONGXUYEN = double.Parse(workSheet.Cells[11, 4].Text.Trim() == "" ? "0" : workSheet.Cells[11, 4].Text.Trim());
                            detail.CHI_CHUYENNGUON = double.Parse(workSheet.Cells[12, 4].Text.Trim() == "" ? "0" : workSheet.Cells[12, 4].Text.Trim());
                            detail.CHI_NOPTRANS = double.Parse(workSheet.Cells[13, 4].Text.Trim() == "" ? "0" : workSheet.Cells[13, 4].Text.Trim());

                            detail.KETDUNS = double.Parse(workSheet.Cells[18, 2].Text.Trim() == "" ? "0" : workSheet.Cells[18, 2].Text.Trim());

                            _bieu07TT344DetailService.Insert(detail);
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
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = ex.Message;
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
            Response<BIEU07TT344Vm.ViewModel> response = new Response<BIEU07TT344Vm.ViewModel>();
            try
            {
                var item = await _bieu07TT344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(refid));
                if (item != null)
                {
                    var detail = await _bieu07TT344DetailService.Queryable().FirstOrDefaultAsync(x => x.PHB_BIEU07TT344_REFID.Equals(refid));

                    BIEU07TT344Vm.ViewModel data = new BIEU07TT344Vm.ViewModel();
                    data.REFID = refid;
                    data.NAM_BC = item.NAM_BC;
                    data.TINH = item.TINH;
                    data.HUYEN = item.HUYEN;
                    data.XA = item.XA;

                    data.TONGTHU = detail.TONGTHU;
                    data.THU_XAHUONG100 = detail.THU_XAHUONG100;
                    data.THU_CHIATYLE = detail.THU_CHIATYLE;
                    data.THU_BOSUNG = detail.THU_BOSUNG;
                    data.THU_BOSUNGCANDOINS = detail.THU_BOSUNGCANDOINS;
                    data.THU_BOSUNGCOMUCTIEU = detail.THU_BOSUNGCOMUCTIEU;
                    data.THU_KETDUNSNAMTRUOC = detail.THU_KETDUNSNAMTRUOC;
                    data.THU_VIENTRO = detail.THU_VIENTRO;
                    data.THU_CHUYENNGUON = detail.THU_CHUYENNGUON;

                    data.TONGCHI = detail.TONGCHI;
                    data.CHI_DAUTUPT = detail.CHI_DAUTUPT;
                    data.CHI_THUONGXUYEN = detail.CHI_THUONGXUYEN;
                    data.CHI_CHUYENNGUON = detail.CHI_CHUYENNGUON;
                    data.CHI_NOPTRANS = detail.CHI_NOPTRANS;

                    data.KETDUNS = detail.KETDUNS;

                    response.Error = false;
                    response.Data = data;
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.NOT_FOUND;
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
        public async Task<IHttpActionResult> Put(BIEU07TT344Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU07TT344 bieu07 = await _bieu07TT344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu07 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Edit
                    hasValue = true;
                        PHB_BIEU07TT344_DETAIL detail = await _bieu07TT344DetailService.Queryable().FirstOrDefaultAsync(x =>x.PHB_BIEU07TT344_REFID.Equals(model.REFID));
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                    detail.TONGTHU = model.TONGTHU;
                    detail.THU_XAHUONG100 = model.THU_XAHUONG100;
                    detail.THU_CHIATYLE = model.THU_CHIATYLE;
                    detail.THU_BOSUNG = model.THU_BOSUNG;
                    detail.THU_BOSUNGCANDOINS = model.THU_BOSUNGCANDOINS;
                    detail.THU_BOSUNGCOMUCTIEU = model.THU_BOSUNGCOMUCTIEU;
                    detail.THU_KETDUNSNAMTRUOC = model.THU_KETDUNSNAMTRUOC;
                    detail.THU_VIENTRO = model.THU_VIENTRO;
                    detail.THU_CHUYENNGUON = model.THU_CHUYENNGUON;

                    detail.TONGCHI = model.TONGCHI;
                    detail.CHI_DAUTUPT = model.CHI_DAUTUPT;
                    detail.CHI_THUONGXUYEN = model.CHI_THUONGXUYEN;
                    detail.CHI_CHUYENNGUON = model.CHI_CHUYENNGUON;
                    detail.CHI_NOPTRANS = model.CHI_NOPTRANS;

                    detail.KETDUNS = model.KETDUNS;
                    _bieu07TT344DetailService.Update(detail);
                        }
                #endregion

                if (hasValue)
                {
                    bieu07.NGAY_SUA = DateTime.Now;
                    bieu07.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu07TT344Service.Update(bieu07);

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
            var response = await _bieu07TT344Service.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

    }
}