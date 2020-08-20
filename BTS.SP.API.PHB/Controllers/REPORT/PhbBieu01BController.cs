using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU01B;
using BTS.SP.PHB.SERVICE.Models.BIEU01B;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01B;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Security.Claims;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBieu01b")]
    [Route("{id?}")]
    public class PhbBieu01BController : ApiController
    {
        private readonly IPhbBieu01BService _bieu01BService;
        private readonly IPhbBieu01BDetailService _bieu01BDetailService;
        private readonly IPhbBieu01BTemplateService _bieu01BTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu01BController(IPhbBieu01BService bieu01BService, IPhbBieu01BDetailService bieu01BDetailService, IPhbBieu01BTemplateService bieu01BTemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu01BService = bieu01BService;
            _bieu01BDetailService = bieu01BDetailService;
            _bieu01BTemplateService = bieu01BTemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var response = new Response<string>();
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
                            var bieu01B = new PHB_BIEU01B()
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
                            bieu01B.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu01B.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu01B.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu01B.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu01B.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu01B.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu01BService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu01B.MA_CHUONG) && x.MA_QHNS.Equals(bieu01B.MA_QHNS) &&
                                x.NAM_BC == bieu01B.NAM_BC && x.KY_BC == bieu01B.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu01BService.Insert(bieu01B);

                            #region xử lý phần I
                            var rowStartPhanI = 8;
                            var rowEndPhanI = rowStartPhanI + 9;
                            var detailI = new PHB_BIEU01B_DETAIL
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                STT_CHI_TIEU = null,
                                MA_CHI_TIEU = "I",
                                SO_DCKT = 0,
                                SO_BC = 0,
                                PHAN = 1,
                                CAP = 1,
                                LOAI = 0
                            };
                            detailI.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanI, 2].Value.ToString().TrimEnd();
                            _bieu01BDetailService.Insert(detailI);
                            for (var j = rowStartPhanI + 1; j <= rowEndPhanI; j++)
                            {
                                var detail = new PHB_BIEU01B_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    PHAN = 1
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.TEN_CHI_TIEU = workSheet.Cells[j, 2].Value.ToString().TrimEnd();
                                if (detail.STT_CHI_TIEU.Equals("01") || detail.STT_CHI_TIEU.Equals("05") ||
                                    detail.STT_CHI_TIEU.Equals("09"))
                                {
                                    detail.CAP = 2;
                                    detail.LOAI = 0;
                                }
                                else
                                {
                                    detail.CAP = 3;
                                    detail.LOAI = 1;
                                }
                                if (workSheet.Cells[j, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[j, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[j, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[j, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);
                            }
                            #endregion

                            #region xử lý phần II
                            var rowStartPhanII = rowEndPhanI + 1;
                            var rowEndPhanII = rowStartPhanII + 3;
                            var detailII = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                SO_BC = 0,
                                SO_DCKT = 0,
                                PHAN = 2,
                                CAP = 1,
                                LOAI = 0,
                                STT_CHI_TIEU = null,
                                MA_CHI_TIEU = "II"
                            };
                            detailII.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanII, 2].Value.ToString().TrimEnd();
                            _bieu01BDetailService.Insert(detailII);
                            for (var j = rowStartPhanII + 1; j <= rowEndPhanII; j++)
                            {
                                var detail = new PHB_BIEU01B_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    PHAN = 2,
                                    CAP = 2
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.LOAI = detail.STT_CHI_TIEU.Equals("12") ? 0 : 1;
                                detail.TEN_CHI_TIEU = workSheet.Cells[j, 2].Value.ToString().TrimEnd();
                                if (workSheet.Cells[j, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[j, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[j, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[j, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);
                            }
                            #endregion

                            #region xử lý phần III
                            var rowStartPhanIII = rowEndPhanII + 1;
                            var rowEndPhanIII = rowStartPhanIII + 3;
                            var detailIII = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                SO_BC = 0,
                                SO_DCKT = 0,
                                PHAN = 3,
                                CAP = 1,
                                LOAI = 0,
                                STT_CHI_TIEU = null,
                                MA_CHI_TIEU = "III"
                            };
                            detailIII.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanIII, 2].Value.ToString().TrimEnd();
                            _bieu01BDetailService.Insert(detailIII);
                            for (var j = rowStartPhanIII + 1; j <= rowEndPhanIII; j++)
                            {
                                var detail = new PHB_BIEU01B_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    PHAN = 3,
                                    CAP = 2
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.LOAI = detail.STT_CHI_TIEU.Equals("15") ? 0 : 1;
                                detail.TEN_CHI_TIEU = workSheet.Cells[j, 2].Value.ToString().TrimEnd();
                                if (workSheet.Cells[j, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[j, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[j, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[j, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);
                            }
                            #endregion

                            #region xử lý phần IV
                            var rowStartPhanIV = rowEndPhanIII + 1;
                            var rowEndPhanIV = rowStartPhanIV + 3;
                            var detailIV = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                SO_BC = 0,
                                SO_DCKT = 0,
                                PHAN = 4,
                                CAP = 1,
                                LOAI = 0,
                                STT_CHI_TIEU = null,
                                MA_CHI_TIEU = "IV"
                            };
                            detailIV.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanIV, 2].Value.ToString().TrimEnd();
                            _bieu01BDetailService.Insert(detailIV);
                            for (var j = rowStartPhanIV + 1; j <= rowEndPhanIV; j++)
                            {
                                var detail = new PHB_BIEU01B_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    PHAN = 4,
                                    CAP = 2
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.LOAI = detail.STT_CHI_TIEU.Equals("18") ? 0 : 1;
                                detail.TEN_CHI_TIEU = workSheet.Cells[j, 2].Value.ToString().TrimEnd();
                                if (workSheet.Cells[j, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[j, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[j, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[j, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);
                            }
                            #endregion

                            #region xử lý phần V
                            var rowStartPhanV = rowEndPhanIV + 1;
                            var rowEndPhanV = rowStartPhanV;
                            var detailV = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                SO_BC = 0,
                                SO_DCKT = 0,
                                PHAN = 5,
                                CAP = 1,
                                LOAI = 0,
                                STT_CHI_TIEU = null,
                                MA_CHI_TIEU = "V"
                            };
                            detailV.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanV, 2].Value.ToString().TrimEnd();
                            _bieu01BDetailService.Insert(detailV);
                            #region ma chi tieu 19
                            rowEndPhanV++;
                            var detailV_19 = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                PHAN = 5,
                                CAP = 2,
                                LOAI = 1,
                                MA_CHI_TIEU = "19",
                                STT_CHI_TIEU = "19"
                            };
                            detailV_19.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanV + 1, 2].Value.ToString().TrimEnd();
                            if (workSheet.Cells[rowStartPhanV + 1, 4].Value != null)
                            {
                                try
                                {
                                    detailV_19.SO_BC = double.Parse(workSheet.Cells[rowStartPhanV + 1, 4].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detailV_19.SO_BC = 0;
                            }
                            if (workSheet.Cells[rowStartPhanV + 1, 5].Value != null)
                            {
                                try
                                {
                                    detailV_19.SO_DCKT = double.Parse(workSheet.Cells[rowStartPhanV + 1, 5].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detailV_19.SO_DCKT = 0;
                            }
                            _bieu01BDetailService.Insert(detailV_19);

                            #endregion
                            #region ma chi tieu 20
                            rowEndPhanV++;

                            var detailV_20 = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                PHAN = 5,
                                CAP = 2,
                                LOAI = 0,
                                MA_CHI_TIEU = "20",
                                STT_CHI_TIEU = "20"
                            };
                            detailV_20.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanV + 2, 2].Value.ToString().TrimEnd();
                            if (workSheet.Cells[rowStartPhanV + 2, 4].Value != null)
                            {
                                try
                                {
                                    detailV_20.SO_BC = double.Parse(workSheet.Cells[rowStartPhanV + 2, 4].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detailV_20.SO_BC = 0;
                            }
                            if (workSheet.Cells[rowStartPhanV + 2, 5].Value != null)
                            {
                                try
                                {
                                    detailV_20.SO_DCKT = double.Parse(workSheet.Cells[rowStartPhanV + 2, 5].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detailV_20.SO_DCKT = 0;
                            }
                            _bieu01BDetailService.Insert(detailV_20);
                            #endregion
                            var tempPhanV = 1;
                            while (workSheet.Cells[++rowEndPhanV, 1].Value == null)
                            {
                                var detail = new PHB_BIEU01B_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    PHAN = 5,
                                    CAP = 3,
                                    LOAI = 3
                                };
                                detail.STT_CHI_TIEU = "20" + (tempPhanV++);
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.TEN_CHI_TIEU = workSheet.Cells[rowEndPhanV, 2].Value.ToString().TrimEnd();
                                if (workSheet.Cells[rowEndPhanV, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[rowEndPhanV, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[rowEndPhanV, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[rowEndPhanV, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);
                            }
                            #endregion

                            #region xử lý phần VI

                            var rowStartPhanVI = rowEndPhanV;
                            var rowEndPhanVI = rowStartPhanVI;
                            var detailVI = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHAN = 6,
                                CAP = 1,
                                LOAI = 0,
                                STT_CHI_TIEU = "21",
                                MA_CHI_TIEU = "21",
                                PHB_BIEU01B_REFID = bieu01B.REFID
                            };
                            detailVI.TEN_CHI_TIEU = workSheet.Cells[rowStartPhanVI, 2].Value.ToString().TrimEnd();
                            if (workSheet.Cells[rowStartPhanVI, 4].Value != null)
                            {
                                try
                                {
                                    detailVI.SO_BC = double.Parse(workSheet.Cells[rowStartPhanVI, 4].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detailVI.SO_BC = 0;
                            }
                            if (workSheet.Cells[rowStartPhanVI, 5].Value != null)
                            {
                                try
                                {
                                    detailVI.SO_DCKT = double.Parse(workSheet.Cells[rowStartPhanVI, 5].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detailVI.SO_DCKT = 0;
                            }
                            _bieu01BDetailService.Insert(detailVI);

                            #region machitieu22
                            rowEndPhanVI++;
                            var detail_22 = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                CAP = 2,
                                PHAN = 6,
                                LOAI = 1,
                                STT_CHI_TIEU = "22",
                                MA_CHI_TIEU = "22"
                            };
                            detail_22.TEN_CHI_TIEU = workSheet.Cells[rowEndPhanVI, 2].Value.ToString().TrimEnd();
                            if (workSheet.Cells[rowEndPhanVI, 4].Value != null)
                            {
                                try
                                {
                                    detail_22.SO_BC = double.Parse(workSheet.Cells[rowEndPhanVI, 4].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detail_22.SO_BC = 0;
                            }
                            if (workSheet.Cells[rowEndPhanVI, 5].Value != null)
                            {
                                try
                                {
                                    detail_22.SO_DCKT = double.Parse(workSheet.Cells[rowEndPhanVI, 5].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detail_22.SO_DCKT = 0;
                            }
                            _bieu01BDetailService.Insert(detail_22);
                            #endregion

                            #region machitieu23
                            rowEndPhanVI++;
                            var detail_23 = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                CAP = 2,
                                PHAN = 6,
                                LOAI = 0,
                                STT_CHI_TIEU = "23",
                                MA_CHI_TIEU = "23"
                            };
                            detail_23.TEN_CHI_TIEU = workSheet.Cells[rowEndPhanVI, 2].Value.ToString().TrimEnd();
                            if (workSheet.Cells[rowEndPhanVI, 4].Value != null)
                            {
                                try
                                {
                                    detail_23.SO_BC = double.Parse(workSheet.Cells[rowEndPhanVI, 4].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detail_23.SO_BC = 0;
                            }
                            if (workSheet.Cells[rowEndPhanVI, 5].Value != null)
                            {
                                try
                                {
                                    detail_23.SO_DCKT = double.Parse(workSheet.Cells[rowEndPhanVI, 5].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detail_23.SO_DCKT = 0;
                            }
                            _bieu01BDetailService.Insert(detail_23);
                            #endregion

                            #region chitieucon23
                            for (var k = 1; k <= 6; k++)
                            {
                                rowEndPhanVI++;
                                var detail = new PHB_BIEU01B_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    CAP = 3,
                                    PHAN = 6
                                };
                                detail.STT_CHI_TIEU = "23" + k;
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.TEN_CHI_TIEU = workSheet.Cells[rowEndPhanVI, 2].Value.ToString().TrimEnd();
                                if (k == 6)
                                {
                                    detail.LOAI = 0;
                                }
                                else
                                {
                                    detail.LOAI = 1;
                                }
                                if (workSheet.Cells[rowEndPhanVI, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[rowEndPhanVI, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[rowEndPhanVI, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[rowEndPhanVI, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);

                            }
                            #endregion

                            #region chitieucon236
                            var temp236 = 1;
                            while (workSheet.Cells[++rowEndPhanVI, 1].Value == null)
                            {
                                var detail = new PHB_BIEU01B_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU01B_REFID = bieu01B.REFID,
                                    PHAN = 6,
                                    CAP = 4,
                                    LOAI = 3
                                };
                                detail.STT_CHI_TIEU = "236" + (temp236++);
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                detail.TEN_CHI_TIEU = workSheet.Cells[rowEndPhanVI, 2].Value.ToString().TrimEnd();
                                if (workSheet.Cells[rowEndPhanVI, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_BC = double.Parse(workSheet.Cells[rowEndPhanVI, 4].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_BC = 0;
                                }
                                if (workSheet.Cells[rowEndPhanVI, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_DCKT = double.Parse(workSheet.Cells[rowEndPhanVI, 5].Value.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_DCKT = 0;
                                }
                                _bieu01BDetailService.Insert(detail);
                            }
                            #endregion

                            #region machitieu24
                            var detail_24 = new PHB_BIEU01B_DETAIL()
                            {
                                ObjectState = ObjectState.Added,
                                PHB_BIEU01B_REFID = bieu01B.REFID,
                                PHAN = 6,
                                LOAI = 1,
                                CAP = 2,
                                STT_CHI_TIEU = "24",
                                MA_CHI_TIEU = "24",
                            };
                            detail_24.TEN_CHI_TIEU = workSheet.Cells[rowEndPhanVI, 2].Value.ToString().TrimEnd();
                            if (workSheet.Cells[rowEndPhanVI, 4].Value != null)
                            {
                                try
                                {
                                    detail_24.SO_BC = double.Parse(workSheet.Cells[rowEndPhanVI, 4].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detail_24.SO_BC = 0;
                            }
                            if (workSheet.Cells[rowEndPhanVI, 5].Value != null)
                            {
                                try
                                {
                                    detail_24.SO_DCKT = double.Parse(workSheet.Cells[rowEndPhanVI, 5].Value.ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    WriteLogs.LogError(ex);
                                    return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                }
                            }
                            else
                            {
                                detail_24.SO_DCKT = 0;
                            }
                            _bieu01BDetailService.Insert(detail_24);

                            #endregion

                            #endregion

                            //#region xử lý phần VII

                            //var rowStartPhanVII = rowEndPhanVI + 1;
                            //var rowEndPhanVII = rowStartPhanVII + 6;
                            //var detail_VII = new PHB_BIEU01B_DETAIL
                            //{
                            //    ObjectState = ObjectState.Added,
                            //    PHB_BIEU01B_REFID = bieu01B.REFID,
                            //    STT_CHI_TIEU = null,
                            //    MA_CHI_TIEU = "VII",
                            //    LOAI = 0,
                            //    PHAN = 7,
                            //    CAP = 1,
                            //    SO_BC = 0,
                            //    SO_DCKT = 0,
                            //    TEN_CHI_TIEU = workSheet.Cells[rowStartPhanVII, 2].Value.ToString().TrimEnd()
                            //};
                            //_bieu01BDetailService.Insert(detail_VII);

                            //var temp27 = 1;
                            //for (var j = rowStartPhanVII + 1; j <= rowEndPhanVII; j++)
                            //{
                            //    var detail = new PHB_BIEU01B_DETAIL
                            //    {
                            //        ObjectState = ObjectState.Added,
                            //        PHB_BIEU01B_REFID = bieu01B.REFID,
                            //        PHAN = 7
                            //    };
                            //    if (workSheet.Cells[j, 1].Value == null ||
                            //        string.IsNullOrEmpty(workSheet.Cells[j, 1].Value.ToString()))
                            //    {
                            //        detail.CAP = 3;
                            //        detail.LOAI = 1;
                            //        detail.STT_CHI_TIEU = "27" + (temp27++);
                            //    }
                            //    else
                            //    {
                            //        detail.STT_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                            //        detail.CAP = 2;
                            //        detail.LOAI = 1;
                            //        if (detail.STT_CHI_TIEU.Equals("27")) detail.LOAI = 0;
                            //    }
                            //    detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                            //    detail.TEN_CHI_TIEU = workSheet.Cells[j, 2].Value.ToString().TrimEnd();
                            //    if (workSheet.Cells[j, 4].Value != null)
                            //    {
                            //        try
                            //        {
                            //            detail.SO_BC = double.Parse(workSheet.Cells[j, 4].Value.ToString().Trim());
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            WriteLogs.LogError(ex);
                            //            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                            //        }
                            //    }
                            //    else
                            //    {
                            //        detail.SO_BC = 0;
                            //    }
                            //    if (workSheet.Cells[j, 5].Value != null)
                            //    {
                            //        try
                            //        {
                            //            detail.SO_DCKT = double.Parse(workSheet.Cells[j, 5].Value.ToString().Trim());
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            WriteLogs.LogError(ex);
                            //            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                            //        }
                            //    }
                            //    else
                            //    {
                            //        detail.SO_DCKT = 0;
                            //    }
                            //    _bieu01BDetailService.Insert(detail);
                            //}
                            //#endregion

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
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            var response = new Response<BIEU01BVm.ViewModel>();
            try
            {
                var data = new BIEU01BVm.ViewModel
                {
                    REFID = refid,
                    DETAIL = await _bieu01BDetailService.Queryable()
                        .Where(x => x.PHB_BIEU01B_REFID.Equals(refid) && x.STT_CHI_TIEU != null)
                        .OrderBy(x => x.PHAN).ThenBy(x => x.MA_CHI_TIEU)
                        .ToListAsync()
                };
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
            var response = new Response<BIEU01BVm.ViewModel>();
            try
            {
                var data = new BIEU01BVm.ViewModel
                {
                    REFID = refid,
                    DETAIL = await _bieu01BDetailService.Queryable().Where(x => x.PHB_BIEU01B_REFID.Equals(refid))
                        .OrderBy(x => x.PHAN).ThenBy(x => x.MA_CHI_TIEU)
                        .ToListAsync()
                };
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
            var response = new Response<List<PHB_BIEU01B_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _bieu01BTemplateService.Queryable().OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
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
        public async Task<IHttpActionResult> Post(BIEU01BVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu01B = new PHB_BIEU01B()
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

                var checkReport = await _bieu01BService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu01B.MA_CHUONG) && x.MA_QHNS.Equals(bieu01B.MA_QHNS) &&
                    x.NAM_BC == bieu01B.NAM_BC && x.KY_BC == bieu01B.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu01BService.Insert(bieu01B);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU01B_REFID = bieu01B.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu01BDetailService.Insert(detail);
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
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(BIEU01BVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu01B = await _bieu01BService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu01B == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU01B_DETAIL item = await _bieu01BDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu01BDetailService.Delete(item);
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
                        item.PHB_BIEU01B_REFID = model.REFID;
                        _bieu01BDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU01B_DETAIL detail = await _bieu01BDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;
                            detail.SO_BC = item.SO_BC;
                            detail.SO_DCKT = item.SO_DCKT;
                            _bieu01BDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu01B.NGAY_SUA = DateTime.Now;
                    bieu01B.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu01BService.Update(bieu01B);

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
            return Ok(response);
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var response = await _bieu01BService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("Sumreport_HTML")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport_HTML(ReportRqModel rqmodel)
        {
            var response = await _bieu01BService.SumReport_HTML(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var response = await _bieu01BService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName, rqmodel.PHAN, rqmodel.CAP);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.TEN_CHI_TIEU == item && x.PHAN== rqmodel.PHAN && x.CAP==rqmodel.CAP);

                        foreach (var entry in foundLst)
                        {
                            PHB_BIEU01B_DETAIL detail = await _bieu01BDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                                detail.TEN_CHI_TIEU = rqmodel.newName;
                                _bieu01BDetailService.Update(detail);
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
                        var foundLst = await _bieu01BDetailService.Queryable()
                            .Where(x => x.TEN_CHI_TIEU == rqmodel.TEN_CHI_TIEU && x.PHAN == rqmodel.PHAN && x.CAP == rqmodel.CAP && x.MA_CHI_TIEU_OLD != null).ToListAsync();

                        foreach (var entry in foundLst)
                        {
                            PHB_BIEU01B_DETAIL detail = await _bieu01BDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                                // detail.TEN_CHI_TIEU = rqmodel.newName;
                                detail.TEN_CHI_TIEU = entry.MA_CHI_TIEU_OLD;
                                detail.MA_CHI_TIEU_OLD =null;
                                _bieu01BDetailService.Update(detail);
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

        [Route("SumAllReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SumAllReport(ReportRqModel rqmodel)
        {
            var response = await _bieu01BService.SumAllReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
               string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            var data = await _bieu01BService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU01B/Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU01B/" + 
                    RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        var sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                        if (rqmodel.LOAI_BC == 1)
                        {
                            sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " Toàn tỉnh";
                        }
                        else
                        {
                            sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);
                        }
                        var startInsertRow = 10;
                        var litstByMaQhns = data.Data.DETAIL.GroupBy(x => x.MA_QHNS);
                        foreach (var lstQhns in litstByMaQhns)
                        {
                            if (lstQhns.Key.Equals("0"))
                            {
                                // tổng hợp
                                var listByPhan = lstQhns.GroupBy(x => x.PHAN).OrderBy(x=>x.Key);
                                foreach (var list in listByPhan)
                                {
                                    if (list.Key == 1)
                                    {
                                        #region export phần 1
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow+=1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key > 1 && list.Key < 5)
                                    {
                                        #region export phân 2,3,4
                                        startInsertRow += 1;
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow += 1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key == 5)
                                    {
                                        #region export phần 5
                                        startInsertRow += 1;
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length == 2)
                                            {
                                                sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length > 3)
                                            {
                                                sheet.Cells[startInsertRow, 2, startInsertRow, 5].Style.Font.Italic =
                                                    true;
                                            }
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow+=1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key == 6)
                                    {
                                        #region export phần 6
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length == 2)
                                            {
                                                sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            }
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            if (lst[i].STT_CHI_TIEU.Equals("21"))
                                            {
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Top.Style= ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12,FontStyle.Bold));
                                            }
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow+=1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key == 7)
                                    {
                                        #region export phần 7
                                        startInsertRow += 1;
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length == 2)
                                            {
                                                sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            }

                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow+=1;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                var listQhnsToList = lstQhns.ToList();
                                var listByPhan = listQhnsToList.GroupBy(x => x.PHAN);
                                sheet.InsertRow(startInsertRow+1, 1);
                                startInsertRow += 2;
                                sheet.Cells[startInsertRow, 2].Value = lstQhns.Key + "--" + listQhnsToList[0].TEN_QHNS;
                                sheet.Cells[startInsertRow, 2].Style.Font.Bold = true;
                                startInsertRow += 1;
                                sheet.InsertRow(startInsertRow, 3);
                                sheet.Cells[7, 1, 8, 5].Copy(sheet.Cells[startInsertRow, 1, startInsertRow + 1, 5]);
                                startInsertRow += 2;
                                foreach (var list in listByPhan)
                                {
                                    if (list.Key == 1)
                                    {
                                        #region export phần 1
                                        sheet.InsertRow(startInsertRow,1);
                                        sheet.Cells[startInsertRow, 1].Value = "I";
                                        sheet.Cells[startInsertRow, 2].Value = "I. Hoạt động hành chính, sự nghiệp";
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow,1,startInsertRow,5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                                        startInsertRow += 1;
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow += 1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key > 1 && list.Key < 5)
                                    {
                                        #region export phân 2,3,4
                                        sheet.InsertRow(startInsertRow, 1);
                                        switch (list.Key)
                                        {
                                            case 2:
                                                sheet.Cells[startInsertRow, 1].Value = "II";
                                                sheet.Cells[startInsertRow, 2].Value = "II. Hoạt động sản xuất kinh doanh, dịch vụ";
                                                break;
                                            case 3:
                                                sheet.Cells[startInsertRow, 1].Value = "III";
                                                sheet.Cells[startInsertRow, 2].Value = "III. Hoạt động tài chính";
                                                break;
                                            case 4:
                                                sheet.Cells[startInsertRow, 1].Value = "IV";
                                                sheet.Cells[startInsertRow, 2].Value = "IV. Hoạt động khác";
                                                break;
                                        }
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                                        startInsertRow += 1;

                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow += 1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key == 5)
                                    {
                                        #region export phần 5
                                        sheet.InsertRow(startInsertRow, 1);
                                        sheet.Cells[startInsertRow, 1].Value = "V";
                                        sheet.Cells[startInsertRow, 2].Value = "V. Số phải nộp ngân sách nhà nước";
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                                        startInsertRow += 1;
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length == 2)
                                            {
                                                sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length > 3)
                                            {
                                                sheet.Cells[startInsertRow, 2, startInsertRow, 5].Style.Font.Italic =
                                                    true;
                                            }
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow += 1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key == 6)
                                    {
                                        #region export phần 6
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {
                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length == 2)
                                            {
                                                sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            }
                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            if (lst[i].STT_CHI_TIEU.Equals("21"))
                                            {
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                                            }
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow += 1;
                                        }
                                        #endregion
                                    }
                                    else if (list.Key == 7)
                                    {
                                        #region export phần 7
                                        sheet.InsertRow(startInsertRow, 1);
                                        sheet.Cells[startInsertRow, 1].Value = "VII";
                                        sheet.Cells[startInsertRow, 2].Value = "VII. Tình hình sử dụng nguồn kinh phí cải cách tiền lương";
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startInsertRow, 1, startInsertRow, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                                        startInsertRow += 1;
                                        var lst = list.OrderBy(x => x.STT_CHI_TIEU).ToList();
                                        sheet.InsertRow(startInsertRow, lst.Count);
                                        for (var i = 0; i < lst.Count; i++)
                                        {

                                            for (var j = 1; j <= 5; j++)
                                            {
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startInsertRow, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            if (lst[i].STT_CHI_TIEU.Length == 2)
                                            {
                                                sheet.Cells[startInsertRow, 1].Value = lst[i].STT_CHI_TIEU;
                                            }

                                            sheet.Cells[startInsertRow, 2].Value = lst[i].TEN_CHI_TIEU;
                                            sheet.Cells[startInsertRow, 3].Value = lst[i].SO_BC;
                                            sheet.Cells[startInsertRow, 4].Value = lst[i].SO_DCKT;
                                            sheet.Cells[startInsertRow, 5].Value = lst[i].SO_DCKT - lst[i].SO_BC;
                                            startInsertRow += 1;
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_BIEU01B.xlsx"
                        };
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        var response = ResponseMessage(result);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    return InternalServerError();
                }
            }
            return Ok(data);
        }
    }
}
