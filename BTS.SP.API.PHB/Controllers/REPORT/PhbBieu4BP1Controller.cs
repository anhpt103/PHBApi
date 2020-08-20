using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU4BP1;
using BTS.SP.PHB.SERVICE.Models.BIEU4BP1;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4BP1;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using BTS.SP.PHB.ENTITY.Dm;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using OfficeOpenXml.Style;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBieu4BP1")]
    [Route("{id?}")]
    public class PhbBieu4BP1Controller : ApiController
    {
        private readonly IPhbBieu4BP1Service _bieu4Bp1Service;
        private readonly IPhbBieu4BP1DetailService _bieu4Bp1DetailService;
        private readonly IPhbBieu4BP1TemplateService _bieu4Bp1TemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDmChuongService _dmChuong;

        public PhbBieu4BP1Controller(IPhbBieu4BP1Service bieu4BP1Service, IPhbBieu4BP1TemplateService bieu4BP1TemplateService,
            IPhbBieu4BP1DetailService bieu4BP1DetailService,IUnitOfWorkAsync unitOfWorkAsync, IDmChuongService dmChuong)
        {
            _bieu4Bp1Service = bieu4BP1Service;
            _bieu4Bp1TemplateService = bieu4BP1TemplateService;
            _bieu4Bp1DetailService = bieu4BP1DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
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
                            var bieu4Bp1 = new PHB_BIEU4BP1()
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
                            bieu4Bp1.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu4Bp1.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu4Bp1.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu4Bp1.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu4Bp1.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu4Bp1.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu4Bp1Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu4Bp1.MA_CHUONG) && x.MA_QHNS.Equals(bieu4Bp1.MA_QHNS) &&
                                x.NAM_BC == bieu4Bp1.NAM_BC && x.KY_BC == bieu4Bp1.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu4Bp1Service.Insert(bieu4Bp1);
                            var startRowPhan1 = 11;
                            var endRowPhan1 = 66;
                            for (var i = startRowPhan1; i <= endRowPhan1; i++)
                            {
                                #region get LOAI
                                var loai = 0;
                                if (i <= 18)
                                {
                                    loai = 1;
                                }
                                else if (i <= 24)
                                {
                                    loai = 2;
                                }
                                else if (i <= 30)
                                {
                                    loai = 3;
                                }
                                else if (i <= 36)
                                {
                                    loai = 4;
                                }
                                else if (i <= 32)
                                {
                                    loai = 5;
                                }
                                else if (i <= 58)
                                {
                                    loai = 6;
                                }
                                else loai = 7;


                                #endregion
                                var startCol = 7;

                                #region get TEN_CHI_TIEU
                                var tenchitieu = workSheet.Cells[i, 2].Value.ToString();
                                #endregion

                                var maLoai = string.Empty;
                                while (startCol <= workSheet.Dimension.End.Column)
                                {
                                    if (workSheet.Cells[9, startCol].Value.Equals("Tổng"))
                                    {
                                        maLoai = workSheet.Cells[8, startCol].Value.ToString();
                                        startCol += 1;
                                    }
                                    else
                                    {
                                        var maKhoan = workSheet.Cells[9, startCol].Value.ToString();
                                        var detail = new PHB_BIEU4BP1_DETAIL
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_BIEU4BP1_REFID = bieu4Bp1.REFID,
                                            MA_LOAI = maLoai,
                                            MA_KHOAN = maKhoan,
                                            STT_CHI_TIEU = workSheet.Cells[i, 1].Value.ToString(),
                                        };
                                        detail.MA_CHI_TIEU = detail.STT_CHI_TIEU;
                                        detail.SO_TIEN = workSheet.Cells[i, startCol].Value != null ? double.Parse(workSheet.Cells[i, startCol].Value.ToString()) : 0;
                                        detail.PHAN = loai;
                                        detail.TEN_CHI_TIEU = tenchitieu;
                                        _bieu4Bp1DetailService.Insert(detail);
                                        startCol += 1;
                                    }
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_BIEU4BP1_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _bieu4Bp1TemplateService.Queryable().OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            var response = new Response<BIEU4BP1Vm.ViewModel>();
            try
            {
                var model = new BIEU4BP1Vm.ViewModel();
                model.REFID = refid;
                model.DETAIL = await _bieu4Bp1DetailService.Queryable().Where(x => x.PHB_BIEU4BP1_REFID.Equals(refid))
                    .OrderBy(x=>x.MA_LOAI).ThenBy(x=>x.MA_KHOAN).ThenBy(x=>x.MA_CHI_TIEU.Length).ThenBy(x=>x.MA_CHI_TIEU)
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
        public async Task<IHttpActionResult> Post(BIEU4BP1Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu4Bp1 = new PHB_BIEU4BP1()
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
                var checkReport = await _bieu4Bp1Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu4Bp1.MA_CHUONG) && x.MA_QHNS.Equals(bieu4Bp1.MA_QHNS) &&
                    x.NAM_BC == bieu4Bp1.NAM_BC && x.KY_BC == bieu4Bp1.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu4Bp1Service.Insert(bieu4Bp1);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU4BP1_REFID = bieu4Bp1.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu4Bp1DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU4BP1Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu4Bp1 =
                    await _bieu4Bp1Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu4Bp1 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete theo loại
                if (model.LstLoaiDelete != null && model.LstLoaiDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var maloai in model.LstLoaiDelete)
                    {
                        var details = await _bieu4Bp1DetailService.Queryable().Where(x => x.MA_LOAI.Equals(maloai)).ToListAsync();
                        foreach (var item in details)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu4Bp1DetailService.Delete(item);
                        }
                    }
                }
                #endregion

                #region delete theo khoản
                if (model.LstKhoanDelete != null && model.LstKhoanDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var makhoan in model.LstKhoanDelete)
                    {
                        var details = await _bieu4Bp1DetailService.Queryable().Where(x => x.MA_KHOAN.Equals(makhoan)).ToListAsync();
                        foreach (var item in details)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu4Bp1DetailService.Delete(item);
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
                        item.PHB_BIEU4BP1_REFID = bieu4Bp1.REFID;
                        _bieu4Bp1DetailService.Insert(item);
                    }
                }
                #endregion

                #region edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _bieu4Bp1DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.SO_TIEN = item.SO_TIEN;
                            _bieu4Bp1DetailService.Update(detail);
                        }
                    }
                }
                #endregion
                if (hasValue)
                {
                    bieu4Bp1.NGAY_SUA = DateTime.Now;
                    bieu4Bp1.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu4Bp1Service.Update(bieu4Bp1);
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

        //Export mới
        public class ExportParams
        {
            public string MA_CTMTQG { get; set; }
            public string MA_KBNN { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_CAP { get; set; }
            public string MA_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
            public DateTime TUNGAY_HIEULUC { get; set; }
            public DateTime DENNGAY_HIEULUC { get; set; }
            public DateTime TUNGAY_KETSO { get; set; }
            public DateTime DENNGAY_KETSO { get; set; }
            public string LST_MA_DVQHNS { get; set; }
        }


        public class ResultItems
        {
            public string MA_DIABAN { get; set; }
            public string MA_CAP { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
            public string TEN_TIEUMUC { get; set; }
            public string MA_DVQHNS { get; set; }
            public decimal GIA_TRI_HACH_TOAN { get; set; }
            public string TUCHU { get; set; }
        }

        public class Result1CItems
        {
            public string MA_CHITIEU { get; set; }
            public string SAPXEP { get; set; }
            public string TEN_CHITIEU { get; set; }
            public string STT { get; set; }
            public string CONGTHUC_WHERE { get; set; }
            public int INDAM { get; set; }
            public int INNGHIENG { get; set; }
            public decimal GIA_TRI_HACH_TOAN { get; set; }
        }


        [Route("Export1C")]

        public HttpResponseMessage Export1C(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            file = _CreateExcelFile1C(para);

           
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                }
            }
            return result;
        }


        public string _CreateExcelFile1C(ExportParams para)

        {
            var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            //var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/API_PHB/Template/BIEU4BP1/BIEU4B1TT137.xlsx";
            var fileNameInPut = "BIEU4BPINew1.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/BIEU4BP1/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInPut;
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {
                List<DM_CHITIEU_BAOCAO> items = new List<DM_CHITIEU_BAOCAO>();
                OracleCommand cmd = new OracleCommand();
                OracleDataReader dr;
                cmd.CommandText = @"SELECT MA_CHITIEU,SAPXEP,TEN_CHITIEU,STT,CONGTHUC_WHERE,INDAM,INNGHIENG FROM DM_CHITIEU_BAOCAO WHERE MA_BAOCAO = 'BIEU4BP1'";
                cmd.Connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DM_CHITIEU_BAOCAO dt = new DM_CHITIEU_BAOCAO
                    {

                        MA_CHITIEU = dr["MA_CHITIEU"].ToString(),
                        SAPXEP = dr["SAPXEP"].ToString(),    
                        TEN_CHITIEU = dr["TEN_CHITIEU"].ToString(),
                        STT = dr["STT"].ToString(),
                        CONGTHUC_WHERE = dr["CONGTHUC_WHERE"].ToString(),
                        INDAM = int.Parse(dr["INDAM"].ToString()),
                        INNGHIENG = int.Parse(dr["INNGHIENG"].ToString())
                    };
                    items.Add(dt);
                }
                dr.Close();
                cmd.Connection.Close();
                //check code
                var itemsF = items.Where(x => x.STT != null).OrderBy(y => int.Parse(y.STT)).ToList();
                List<string> chi = new List<string>(new[] { "24", "25", "26", "27", "31", "32", "33", "34", "35" });
                List<string> dt04 = new List<string>(new[] { "56", "57", "58", "62" });
                List<PHA_HACHTOAN_CHI> result = new List<PHA_HACHTOAN_CHI>();
                foreach (var t in itemsF)
                {
                    if (!string.IsNullOrEmpty(t.CONGTHUC_WHERE))
                    {
                        var tbl = "PHA_TH_MLNS";
                        var gtht = "GIA_TRI_HACH_TOAN";
                        if (chi.Contains(t.STT))
                        {
                            gtht = "GIA_TRI_HACH_TOAN";

                        }
                        else if (dt04.Contains(t.STT))
                        {
                            gtht = "ACCOUNTED_CR-ACCOUNTED_DR";
                        }
                        else
                        {
                            gtht = "ACCOUNTED_DR-ACCOUNTED_CR";

                        }
                        OracleCommand cmdt = new OracleCommand();
                        OracleDataReader drt;
                        cmdt.CommandText = string.Format("SELECT MA_CAP,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,MA_DVQHNS,SUM(" + gtht + ") AS GIA_TRI_HACH_TOAN FROM " + tbl + " WHERE ");
                        if (!string.IsNullOrEmpty(para.MA_CAP))
                        {
                            cmdt.CommandText += "MA_CAP = '" + para.MA_CAP + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_CHUONG))
                        {
                            cmdt.CommandText += "MA_CHUONG = '" + para.MA_CHUONG + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_LOAI))
                        {
                            cmdt.CommandText += "MA_LOAI = '" + para.MA_LOAI + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_NGANHKT))
                        {
                            cmdt.CommandText += "MA_NGANHKT = '" + para.MA_NGANHKT + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_MUC))
                        {
                            cmdt.CommandText += "MA_MUC = '" + para.MA_MUC + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
                        {
                            cmdt.CommandText += "MA_TIEUMUC = '" + para.MA_TIEUMUC + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_DVQHNS))
                        {
                            cmdt.CommandText += "MA_DVQHNS IN (" + para.MA_DVQHNS + ") AND ";
                        }

                        if (chi.Contains(t.STT))
                        {
                            if (!string.IsNullOrEmpty(para.MA_DBHC))
                            {
                                cmdt.CommandText += " (MA_DIABAN IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC IN (SELECT MA_DBHC FROM DM_DBHC Where MA_DBHC = '" + para.MA_DBHC + "' or MA_DBHC_CHA = '" + para.MA_DBHC +
                                   "') or MA_DBHC_CHA IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '" + para.MA_DBHC + "' or MA_DBHC_CHA = '" + para.MA_DBHC + "'))) AND ";
                            }
                        }
                        else
                        {
                            if (int.Parse(para.MA_DBHC) != 08)
                            {
                                if (!string.IsNullOrEmpty(para.MA_KBNN))
                                {
                                    cmdt.CommandText += " MA_KBNN like '" + para.MA_KBNN + "' AND ";
                                }
                            }
                        }

                        cmdt.CommandText += "TO_DATE (NGAY_HIEU_LUC,'DD-MM-YY') >= TO_DATE ('" + para.TUNGAY_HIEULUC.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_HIEU_LUC,'DD-MM-YY') <= TO_DATE ('" + para.DENNGAY_HIEULUC.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_KET_SO,'DD-MM-YY') >= TO_DATE ('" + para.TUNGAY_KETSO.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_KET_SO,'DD-MM-YY') <= TO_DATE ('" + para.DENNGAY_KETSO.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY') AND ";
                        cmdt.CommandText += t.CONGTHUC_WHERE;
                        cmdt.CommandText += " GROUP BY MA_CAP,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,MA_DVQHNS";
                        cmdt.Connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                        cmdt.Connection.Open();
                        drt = cmdt.ExecuteReader();
                        while (drt.Read())
                        {
                            PHA_HACHTOAN_CHI tmpItem = new PHA_HACHTOAN_CHI();
                            tmpItem.MA_CAP = drt["MA_CAP"].ToString();
                            tmpItem.MA_CHUONG = drt["MA_CHUONG"].ToString();
                            tmpItem.MA_LOAI = drt["MA_LOAI"].ToString();
                            tmpItem.MA_NGANHKT = drt["MA_NGANHKT"].ToString();
                            tmpItem.MA_MUC = drt["MA_MUC"].ToString();
                            tmpItem.MA_TIEUMUC = drt["MA_TIEUMUC"].ToString();
                            tmpItem.MA_DVQHNS = drt["MA_DVQHNS"].ToString();
                            //Gán STT vào SEG12 để group khi xuất file -- Không ảnh hưởng dữ liệu
                            tmpItem.SEGMENT12 = t.STT;
                            if (drt["GIA_TRI_HACH_TOAN"].ToString() != "")
                            {
                                tmpItem.GIA_TRI_HACH_TOAN = decimal.Parse(drt["GIA_TRI_HACH_TOAN"].ToString());
                            }
                            result.Add(tmpItem);
                        }
                        drt.Close();
                        cmdt.Connection.Close();
                    }
                }
                
                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        _BindingDataToExcel1C(workSheet, result, itemsF, para);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }

            }

            else
            {
                filename = "";
            }

            return filename;         
        }


        public void _BindingDataToExcel1C(ExcelWorksheet ws, List<PHA_HACHTOAN_CHI> result, List<DM_CHITIEU_BAOCAO> items, ExportParams para)
        {
            var starRow = 10;
            var starCol = 4;
            //Ghi STT và Tên Chỉ Tiêu
            for (int i = 0; i < items.Count; i++)
            {
                ws.Cells[starRow + i, 1].Value = items[i].SAPXEP;
                ws.Cells[starRow + i, 2].Value = items[i].TEN_CHITIEU;
                if (items[i].INDAM == 1)
                {
                    ws.Cells[starRow + i, 1].Style.Font.Bold = true;
                    ws.Cells[starRow + i, 2].Style.Font.Bold = true;
                }
                if (items[i].INNGHIENG == 1)
                {
                    ws.Cells[starRow + i, 1].Style.Font.Italic = true;
                    ws.Cells[starRow + i, 1].Style.Font.Italic = true;
                }
                if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                {
                    ws.Cells[starRow + i, 3].Value = result.Where(y => y.SEGMENT12 == items[i].STT).Sum(x => x.GIA_TRI_HACH_TOAN);
                    ws.Cells[starRow + i, 3].Style.Numberformat.Format = "###,###,###,###,###";
                }
            }

            //Thêm Động loại
            var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();

            for (int j = 0; j < loaiS.Count; j++)
            {

                var khoanS = result.Where(x => x.MA_LOAI == loaiS[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
                // Thêm mã dòng loại
                ws.Cells[7, starCol, 7, starCol + khoanS.Count].Merge = true;
                ws.Cells[7, starCol, 7, starCol + khoanS.Count].Style.Font.Bold = true;
                ws.Cells[7, starCol, 7, starCol + khoanS.Count].Style.WrapText = true;
                ws.Cells[7, starCol, 7, starCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[7, starCol].Value = "Loại" + loaiS[j];
                ws.Cells[8, starCol].Value = "Tổng số";


                //Thêm Tiền Tổng Loại
                for (int i = 0; i < items.Count; i++)
                {
                    if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                    {
                        ws.Cells[starRow + i, starCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";

                    }
                }

                //Thêm Khoản
                for (int k = 0; k < khoanS.Count; k++)
                {
                    var nextCol = starCol + k + 1;
                    ws.Cells[8, nextCol].Value = "Khoản" + khoanS[k];

                    ws.Cells[8, nextCol].Style.Font.Bold = true;
                    ws.Cells[8, nextCol].Style.WrapText = true;
                    ws.Cells[8, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                        {
                            ws.Cells[starRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[starRow + i, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }


                }
                //Gán số cột bằng số cột hiện tại
                starCol += khoanS.Count + 1;

            }
            var lstDvqhns = result.Select(x => x.MA_DVQHNS).Distinct().ToList();
            ws.Cells[7, starCol, 7, starCol + lstDvqhns.Count - 1].Merge = true;
            ws.Cells[7, starCol, 7, starCol + lstDvqhns.Count - 1].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            ws.Cells[7, starCol, 7, starCol + lstDvqhns.Count - 1].Style.Font.Bold = true;
            ws.Cells[7, starCol, 7, starCol + lstDvqhns.Count - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            for (int j = 0; j < lstDvqhns.Count; j++)
            {
                ws.Cells[8, starCol + j, 9, starCol + j].Merge = true;
                ws.Cells[8, starCol + j, 9, starCol + j].Value = lstDvqhns[j];
                ws.Cells[8, starCol + j, 9, starCol + j].Style.Font.Bold = true;
                ws.Cells[8, starCol + j, 9, starCol + j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                for (int i = 0; i < items.Count; i++)
                {
                    ws.Cells[starRow + i, starCol + j].Value = result.Where(x => x.SEGMENT12 == items[i].STT && x.MA_DVQHNS == lstDvqhns[j]).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[starRow + i, starCol + j].Style.Numberformat.Format = "###,###,###,###,###";
                }

            }
            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp" + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương" + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại" + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk = "Khoản" + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk = "Mục" + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk = "Tiểu Mục" + para.MA_TIEUMUC + ";";
            }

            var DVBAOCAO = _dmChuong.Queryable().FirstOrDefault(x => x.MA_CHUONG == para.MA_CHUONG).TEN_CHUONG;
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Đơn vị báo cáo :" + DVBAOCAO;
            ws.Cells[3, 1].Value = "Từ ngày hiệu lực :" + para.TUNGAY_HIEULUC.ToString("d") + "đến ngày hiệu lực" + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[4, 1].Value = "Từ ngày kết sổ :" + para.TUNGAY_KETSO.ToString("d") + "đến ngày kết sổ:" + para.DENNGAY_KETSO.ToString("d");

            ws.Cells[7, 1, 8, 1].Merge = true;
            ws.Cells[7, 1, 8, 1].Value = "Chỉ Tiêu";
            ws.Cells[7, 1, 8, 1].Style.Font.Bold = true;
            ws.Cells[7, 1, 8, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[9, 1].Value = "A";
            ws.Cells[9, 1].Style.Font.Bold = true;
            ws.Cells[9, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[7, 2, 8, 2].Merge = true;
            ws.Cells[7, 2, 8, 2].Value = "Nội dung";
            ws.Cells[7, 2, 8, 2].Style.Font.Bold = true;
            ws.Cells[7, 2, 8, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[9, 2].Value = "B";
            ws.Cells[9, 2].Style.Font.Bold = true;
            ws.Cells[9, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[7, 3, 8, 3].Merge = true;
            ws.Cells[7, 3, 8, 3].Value = "Tổng số";
            ws.Cells[7, 3, 8, 3].Style.Font.Bold = true;
            ws.Cells[7, 3, 8, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[9, 3].Value = "1";
            ws.Cells[9, 3].Style.Font.Bold = true;
            ws.Cells[9, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[2, 4, 2, 7].Merge = true;
            ws.Cells[2, 4, 2, 7].Value = "SỐ LIỆU XÉT DUYỆT (HOẶC THẨM ĐỊNH)";
            ws.Cells[2, 4, 2, 7].Style.Font.Bold = true;
            ws.Cells[2, 4, 2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[2, 4, 2, 7].Style.Font.Size = 15;

            ws.Cells[3, 4, 3, 7].Merge = true;
            ws.Cells[3, 4, 3, 7].Value = "QUYẾT TOÁN CHI NGÂN SÁCH NĂM";
            ws.Cells[3, 4, 3, 7].Style.Font.Bold = true;
            ws.Cells[3, 4, 3, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[3, 4, 3, 7].Style.Font.Size = 15;

            ws.Cells[4, 4, 4, 7].Merge = true;
            ws.Cells[4, 4, 4, 7].Value = "(Kèm theo Thông báo xét duyệt (hoặc thẩm định) quyết toán số …../….. ngày …/…/….)";
            ws.Cells[4, 4, 4, 7].Style.Font.Italic = true;
            ws.Cells[4, 4, 4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




            ws.SelectedRange[7, 1, 10, starCol + lstDvqhns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 1, 10, starCol + lstDvqhns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 1, 10, starCol + lstDvqhns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 1, 10, starCol + lstDvqhns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[11, 1, starRow + items.Count, starCol + lstDvqhns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, starRow + items.Count, starCol + lstDvqhns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, starRow + items.Count, starCol + lstDvqhns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, starRow + items.Count, starCol + lstDvqhns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

    }
}
