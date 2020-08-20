using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU69NS;
using BTS.SP.PHB.SERVICE.Models.BIEU69NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu69NS;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Web;
using OfficeOpenXml;
using BTS.SP.PHB.ENTITY.Rp;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU69Ns")]
    [Route("{id?}")]
    public class PhbBieu69NsController : ApiController
    {
        private readonly IPhbBieu69NsService _phb69NsService;
        private readonly IPhbBieu69NsDetailService _phb69NsDetailService;
        private readonly IPhbBieu69NsTemplateService _phb69NsTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu69NsController(IPhbBieu69NsService phb69NsService, IPhbBieu69NsDetailService phb69NsDetailService, IPhbBieu69NsTemplateService phb69NsTemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phb69NsService = phb69NsService;
            _phb69NsDetailService = phb69NsDetailService;
            _phb69NsTemplateService = phb69NsTemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<BIEU69NSVm.ViewModel> response = new Response<BIEU69NSVm.ViewModel>();
            try
            {
                BIEU69NSVm.ViewModel data = new BIEU69NSVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _phb69NsDetailService.Queryable().Where(x => x.PHB_BIEU69NS_REFID.Equals(refid))
                    .OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
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
            Response<BIEU69NSVm.ViewModel> response = new Response<BIEU69NSVm.ViewModel>();
            try
            {
                BIEU69NSVm.ViewModel data = new BIEU69NSVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _phb69NsDetailService.Queryable().Where(x => x.PHB_BIEU69NS_REFID.Equals(refid))
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHB_BIEU69NS_TEMPLATE>> response = new Response<List<PHB_BIEU69NS_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _phb69NsTemplateService.Queryable()
                    .OrderBy(x => x.MA_CHI_TIEU)
                    .ToListAsync();
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
        public async Task<IHttpActionResult> Post(BIEU69NSVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu70Ns = new PHB_BIEU69NS()
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
                    REFID = Guid.NewGuid().ToString("N")
                };
                var checkReport = await _phb69NsService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu70Ns.MA_CHUONG) && x.MA_QHNS.Equals(bieu70Ns.MA_QHNS) &&
                    x.NAM_BC == bieu70Ns.NAM_BC && x.KY_BC == bieu70Ns.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phb69NsService.Insert(bieu70Ns);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU69NS_REFID = bieu70Ns.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phb69NsDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU69NSVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU69NS b69ns =
                    await _phb69NsService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (b69ns == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU69NS_DETAIL item = await _phb69NsDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _phb69NsDetailService.Delete(item);
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
                        item.PHB_BIEU69NS_REFID = model.REFID;
                        _phb69NsDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU69NS_DETAIL detail = await _phb69NsDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;
                            detail.SKN_THANHTRA = item.SKN_THANHTRA;
                            detail.SKN_KIEMTOAN = item.SKN_KIEMTOAN;
                            detail.SXL_THANHTRA = item.SXL_THANHTRA;
                            detail.SXL_KIEMTOAN = item.SXL_KIEMTOAN;
                            detail.SCXL_THANHTRA = item.SCXL_THANHTRA;
                            detail.SCXL_KIEMTOAN = item.SCXL_KIEMTOAN;
                            _phb69NsDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    b69ns.NGAY_SUA = DateTime.Now;
                    b69ns.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phb69NsService.Update(b69ns);

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

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_BIEU69NS b69ns = new PHB_BIEU69NS()
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
                            b69ns.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            b69ns.MA_QHNS = httpRequest["MA_QHNS"];
                            b69ns.TEN_QHNS = httpRequest["TEN_QHNS"];
                            b69ns.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b69ns.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            b69ns.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phb69NsService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_QHNS.Equals(b69ns.MA_QHNS) && x.NAM_BC == b69ns.NAM_BC && x.KY_BC == b69ns.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb69NsService.Insert(b69ns);

                            //chi tiết
                            string machitieu = string.Empty;
                            string sothutu = string.Empty;
                            int startRow = 9;
                            int count = 1;
                            #region xử lý phần I
                            while (workSheet.Cells[startRow, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "II" && workSheet.Cells[startRow, 1].Value?.ToString() != "III")
                            {
                                PHB_BIEU69NS_DETAIL detail = new PHB_BIEU69NS_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU69NS_REFID = b69ns.REFID
                                };

                                //loại
                                if (workSheet.Cells[startRow, 3].Value != null)
                                {
                                    count = 1;
                                    machitieu = workSheet.Cells[startRow, 3].Value?.ToString();
                                    if (int.Parse(machitieu) >= 3 && int.Parse(machitieu) <= 12)
                                    {
                                        detail.LOAI = 1;
                                        detail.MA_CHI_TIEU = machitieu;
                                    }
                                    else if (machitieu == "1" || machitieu == "2")
                                    {
                                        detail.LOAI = 2;
                                        detail.MA_CHI_TIEU = "0" + machitieu;
                                    }
                                    //detail.MA_CHI_TIEU = machitieu;
                                }
                                else
                                {
                                    detail.LOAI = 3;
                                    if (int.Parse(machitieu) < 10)
                                    {
                                        detail.MA_CHI_TIEU = "0" + machitieu + (count++);
                                    }
                                    else
                                    {
                                        detail.MA_CHI_TIEU = machitieu + (count++);
                                    }
                                    detail.MA_CHI_TIEU_CHA = machitieu;
                                }

                                //phần
                                if (workSheet.Cells[startRow, 1].Value != null)
                                {
                                    sothutu = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.PHAN = "1" + sothutu;
                                }
                                else
                                {
                                    detail.PHAN = "1" + sothutu;
                                }

                                detail.STT_CHI_TIEU = workSheet.Cells[startRow, 1].Value?.ToString();
                                detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
                                if (workSheet.Cells[startRow, 4].Value != null) detail.SKN_THANHTRA = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                if (workSheet.Cells[startRow, 5].Value != null) detail.SKN_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 5].Value.ToString().Trim());
                                if (workSheet.Cells[startRow, 6].Value != null) detail.SXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 6].Value.ToString().Trim());
                                if (workSheet.Cells[startRow, 7].Value != null) detail.SXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 7].Value.ToString().Trim());
                                if (workSheet.Cells[startRow, 8].Value != null) detail.SCXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 8].Value.ToString().Trim());
                                if (workSheet.Cells[startRow, 9].Value != null) detail.SCXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 9].Value.ToString().Trim());
                                if (workSheet.Cells[startRow, 10].Value != null) detail.GHI_CHU = workSheet.Cells[startRow, 10].Value.ToString().Trim();
                                _phb69NsDetailService.Insert(detail);
                                startRow += 1;

                            }
                            #endregion
                            #region xử lý phần II
                            while (workSheet.Cells[startRow, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "III")
                            {
                                if (workSheet.Cells[startRow, 1].Value?.ToString() != "I")
                                {
                                    PHB_BIEU69NS_DETAIL detail = new PHB_BIEU69NS_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU69NS_REFID = b69ns.REFID
                                    };

                                    //loại
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        count = 1;
                                        machitieu = workSheet.Cells[startRow, 3].Value?.ToString();

                                        if (int.Parse(machitieu) >= 15 && int.Parse(machitieu) <= 24)
                                        {
                                            detail.LOAI = 1;
                                        }
                                        else if (machitieu == "13" || machitieu == "14")
                                        {
                                            detail.LOAI = 2;
                                        }
                                        detail.MA_CHI_TIEU = machitieu;

                                    }
                                    else
                                    {
                                        detail.LOAI = 3;
                                        if (int.Parse(machitieu) < 10)
                                        {
                                            detail.MA_CHI_TIEU = "0" + machitieu + (count++);
                                        }
                                        else
                                        {
                                            detail.MA_CHI_TIEU = machitieu + (count++);
                                        }
                                        detail.MA_CHI_TIEU_CHA = machitieu;
                                    }

                                    //phần
                                    if (workSheet.Cells[startRow, 1].Value != null)
                                    {
                                        sothutu = workSheet.Cells[startRow, 1].Value?.ToString();
                                        detail.PHAN = "2" + sothutu;
                                    }
                                    else
                                    {
                                        detail.PHAN = "2" + sothutu;
                                    }

                                    detail.STT_CHI_TIEU = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
                                    if (workSheet.Cells[startRow, 4].Value != null) detail.SKN_THANHTRA = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                    if (workSheet.Cells[startRow, 5].Value != null) detail.SKN_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 5].Value.ToString().Trim());
                                    if (workSheet.Cells[startRow, 6].Value != null) detail.SXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 6].Value.ToString().Trim());
                                    if (workSheet.Cells[startRow, 7].Value != null) detail.SXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 7].Value.ToString().Trim());
                                    if (workSheet.Cells[startRow, 8].Value != null) detail.SCXL_THANHTRA = double.Parse(workSheet.Cells[startRow, 8].Value.ToString().Trim());
                                    if (workSheet.Cells[startRow, 9].Value != null) detail.SCXL_KIEMTOAN = double.Parse(workSheet.Cells[startRow, 9].Value.ToString().Trim());
                                    if (workSheet.Cells[startRow, 10].Value != null) detail.GHI_CHU = workSheet.Cells[startRow, 10].Value.ToString().Trim();
                                    _phb69NsDetailService.Insert(detail);
                                    startRow += 1;
                                }
                                else
                                {
                                    startRow += 1;
                                }

                            }
                            #endregion

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
            var response = await _phb69NsService.SumReport("-1", rqmodel.LOAI_BC, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()),rqmodel.CHI_TIET);
            return Ok(response);
        }
    }
}
