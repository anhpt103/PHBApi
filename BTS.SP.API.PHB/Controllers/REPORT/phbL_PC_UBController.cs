using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using BTS.SP.PHB.SERVICE.Models.L_PC_UB;
using BTS.SP.PHB.SERVICE.REPORT.L_PC_UB;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using BTS.SP.PHB.ENTITY.Rp.L_PC_UB;
using System.Security.Claims;
using AutoMapper;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbL_PC_UB")]
    [Route("{id?}")]
    public class phbL_PC_UBController : ApiController
    {
        private readonly IPhbL_PC_UBService _L_PC_UBService;
        private readonly IPhbL_PC_UBDetailService _L_PC_UBDetailService;
        private IUnitOfWorkAsync _unitOfWorkAsync;
        public phbL_PC_UBController(IPhbL_PC_UBService L_PC_UBService, IPhbL_PC_UBDetailService L_PC_UBDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _L_PC_UBService = L_PC_UBService;
            _L_PC_UBDetailService = L_PC_UBDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
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
                        PHB_L_PC_UB L_PC_UB = new PHB_L_PC_UB()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            MA_DBHC_CHA = claimMaDbhc?.Value
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            L_PC_UB.MA_CHUONG = "423";
                            L_PC_UB.MA_QHNS = "27";
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã địa bàn hành chính"
                            });
                            L_PC_UB.MA_DBHC = httpRequest["MA_DBHC"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            L_PC_UB.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            L_PC_UB.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _L_PC_UBService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(L_PC_UB.MA_CHUONG) && x.MA_DBHC.Equals(L_PC_UB.MA_DBHC) &&
                                x.NAM_BC == L_PC_UB.NAM_BC && x.KY_BC == L_PC_UB.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _L_PC_UBService.Insert(L_PC_UB);

                            #region insert TRONG BẢNG
                            int startRowTB = 14;
                            bool isLoop = true;
                            bool isHE_SOLUONG_NO, isHE_SOLUONG_CO, isPC_KV_NO, isPC_KV_CO, isPC_CHUCVU_NO, isPC_CHUCVU_CO, isPC_TRACHNHIEM_NO, isPC_TRACHNHIEM_CO,
                                isPC_CONGVU_NO, isPC_CONGVU_CO, isPC_LOAIXA_NO, isPC_LOAIXA_CO, isPC_LAUNAM_NO, isPC_LAUNAM_CO, isPC_THUHUT_NO, isPC_THUHUT_CO, isCKPT_BHXH_NO, isCKPT_BHXH_CO, isCKPT_BHYT_NO, isCKPT_BHYT_CO;
                            decimal _HE_SOLUONG_NO, _HE_SOLUONG_CO, _PC_KV_NO, _PC_KV_CO, _PC_CHUCVU_NO, _PC_CHUCVU_CO, _PC_TRACHNHIEM_NO, _PC_TRACHNHIEM_CO,
                                _PC_CONGVU_NO, _PC_CONGVU_CO, _PC_LOAIXA_NO, _PC_LOAIXA_CO, _PC_LAUNAM_NO, _PC_LAUNAM_CO, _PC_THUHUT_NO, _PC_THUHUT_CO, _CKPT_BHXH_NO, _CKPT_BHXH_CO, _CKPT_BHYT_NO, _CKPT_BHYT_CO;
                            while (workSheet.Cells[startRowTB, 1].Value != null && isLoop)
                            {
                                if (workSheet.Cells[startRowTB, 1].Value != null &&
                                    workSheet.Cells[startRowTB, 1].Value.ToString().Trim().Contains("B."))
                                {
                                    isLoop = false;
                                }
                                else
                                {
                                    if (workSheet.Cells[startRowTB, 1].Value != null &&
                                        workSheet.Cells[startRowTB, 1].Value.ToString().Trim().Contains("A."))
                                    {
                                        startRowTB += 1;
                                    }
                                    else
                                    {
                                        PHB_L_PC_UB_DETAIL detail = new PHB_L_PC_UB_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_L_PC_UB_REFID = L_PC_UB.REFID,
                                            LOAI = 1,
                                        };
                                        detail.STT = workSheet.Cells[startRowTB, 1].Value != null ? workSheet.Cells[startRowTB, 1].Value.ToString() : null;
                                        detail.HO_VATEN = workSheet.Cells[startRowTB, 2].Value != null ? workSheet.Cells[startRowTB, 2].Value.ToString() : null;
                                        detail.CHUC_DANH = workSheet.Cells[startRowTB, 3].Value != null ? workSheet.Cells[startRowTB, 3].Value.ToString() : null;
                                        isHE_SOLUONG_CO = decimal.TryParse(workSheet.Cells[startRowTB, 4].Value == null ? "0" : workSheet.Cells[startRowTB, 4].Value.ToString(), out _HE_SOLUONG_NO);
                                        detail.HE_SOLUONG = isHE_SOLUONG_CO ? _HE_SOLUONG_NO : 0;
                                        isPC_KV_CO = decimal.TryParse(workSheet.Cells[startRowTB, 5].Value == null ? "0" : workSheet.Cells[startRowTB, 5].Value.ToString(), out _PC_KV_NO);
                                        detail.PC_KV = isPC_KV_CO ? _PC_KV_NO : 0;
                                        isPC_CHUCVU_CO = decimal.TryParse(workSheet.Cells[startRowTB, 6].Value == null ? "0" : workSheet.Cells[startRowTB, 6].Value.ToString(), out _PC_CHUCVU_NO);
                                        detail.PC_CHUCVU = isPC_CHUCVU_CO ? _PC_CHUCVU_NO : 0;
                                        isPC_TRACHNHIEM_CO = decimal.TryParse(workSheet.Cells[startRowTB, 7].Value == null ? "0" : workSheet.Cells[startRowTB, 7].Value.ToString(), out _PC_TRACHNHIEM_NO);
                                        detail.PC_TRACHNHIEM = isPC_TRACHNHIEM_CO ? _PC_TRACHNHIEM_NO : 0;
                                        isPC_TRACHNHIEM_CO = decimal.TryParse(workSheet.Cells[startRowTB, 8].Value == null ? "0" : workSheet.Cells[startRowTB, 8].Value.ToString(), out _PC_TRACHNHIEM_NO);
                                        detail.PC_TRACHNHIEM = isPC_TRACHNHIEM_CO ? _PC_TRACHNHIEM_NO : 0;
                                        isPC_CONGVU_CO = decimal.TryParse(workSheet.Cells[startRowTB, 9].Value == null ? "0" : workSheet.Cells[startRowTB, 9].Value.ToString(), out _PC_CONGVU_NO);
                                        detail.PC_CONGVU = isPC_CONGVU_CO ? _PC_CONGVU_NO : 0;
                                        isPC_LOAIXA_CO = decimal.TryParse(workSheet.Cells[startRowTB, 10].Value == null ? "0" : workSheet.Cells[startRowTB, 10].Value.ToString(), out _PC_LOAIXA_NO);
                                        detail.PC_LOAIXA = isPC_LOAIXA_CO ? _PC_LOAIXA_NO : 0;
                                        isPC_LAUNAM_CO = decimal.TryParse(workSheet.Cells[startRowTB, 11].Value == null ? "0" : workSheet.Cells[startRowTB, 11].Value.ToString(), out _PC_LAUNAM_NO);
                                        detail.PC_LAUNAM = isPC_LAUNAM_CO ? _PC_LAUNAM_NO : 0;
                                        isPC_THUHUT_CO = decimal.TryParse(workSheet.Cells[startRowTB, 12].Value == null ? "0" : workSheet.Cells[startRowTB, 12].Value.ToString(), out _PC_THUHUT_NO);
                                        detail.PC_THUHUT = isPC_THUHUT_CO ? _PC_THUHUT_NO : 0;
                                        isCKPT_BHXH_CO = decimal.TryParse(workSheet.Cells[startRowTB, 13].Value == null ? "0" : workSheet.Cells[startRowTB, 13].Value.ToString(), out _CKPT_BHXH_NO);
                                        detail.PC_THUHUT = isCKPT_BHXH_CO ? _CKPT_BHXH_NO : 0;
                                        isCKPT_BHYT_CO = decimal.TryParse(workSheet.Cells[startRowTB, 14].Value == null ? "0" : workSheet.Cells[startRowTB, 14].Value.ToString(), out _CKPT_BHYT_NO);
                                        detail.PC_THUHUT = isCKPT_BHYT_CO ? _CKPT_BHYT_NO : 0;
                                        _L_PC_UBDetailService.Insert(detail);
                                        startRowTB += 1;
                                    }
                                }
                            }
                            #endregion

                            #region insert NGOÀI BẢNG

                            int startRowNB = startRowTB;
                            if (workSheet.Cells[startRowNB, 1].Value != null &&
                                workSheet.Cells[startRowNB, 1].Value.ToString().Contains("B.")) startRowNB += 1;
                            while (workSheet.Cells[startRowNB, 1].Value != null)
                            {
                                PHB_L_PC_UB_DETAIL detail = new PHB_L_PC_UB_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_L_PC_UB_REFID = L_PC_UB.REFID,
                                    LOAI = 2,
                                };
                                detail.STT = workSheet.Cells[startRowTB, 1].Value != null ? workSheet.Cells[startRowTB, 1].Value.ToString() : null;
                                detail.HO_VATEN = workSheet.Cells[startRowTB, 2].Value != null ? workSheet.Cells[startRowTB, 2].Value.ToString() : null;
                                detail.CHUC_DANH = workSheet.Cells[startRowTB, 3].Value != null ? workSheet.Cells[startRowTB, 3].Value.ToString() : null;
                                isHE_SOLUONG_CO = decimal.TryParse(workSheet.Cells[startRowTB, 4].Value == null ? "0" : workSheet.Cells[startRowTB, 4].Value.ToString(), out _HE_SOLUONG_NO);
                                detail.HE_SOLUONG = isHE_SOLUONG_CO ? _HE_SOLUONG_NO : 0;
                                isPC_KV_CO = decimal.TryParse(workSheet.Cells[startRowTB, 5].Value == null ? "0" : workSheet.Cells[startRowTB, 5].Value.ToString(), out _PC_KV_NO);
                                detail.PC_KV = isPC_KV_CO ? _PC_KV_NO : 0;
                                isPC_CHUCVU_CO = decimal.TryParse(workSheet.Cells[startRowTB, 6].Value == null ? "0" : workSheet.Cells[startRowTB, 6].Value.ToString(), out _PC_CHUCVU_NO);
                                detail.PC_CHUCVU = isPC_CHUCVU_CO ? _PC_CHUCVU_NO : 0;
                                isPC_TRACHNHIEM_CO = decimal.TryParse(workSheet.Cells[startRowTB, 7].Value == null ? "0" : workSheet.Cells[startRowTB, 7].Value.ToString(), out _PC_TRACHNHIEM_NO);
                                detail.PC_TRACHNHIEM = isPC_TRACHNHIEM_CO ? _PC_TRACHNHIEM_NO : 0;
                                isPC_TRACHNHIEM_CO = decimal.TryParse(workSheet.Cells[startRowTB, 8].Value == null ? "0" : workSheet.Cells[startRowTB, 8].Value.ToString(), out _PC_TRACHNHIEM_NO);
                                detail.PC_TRACHNHIEM = isPC_TRACHNHIEM_CO ? _PC_TRACHNHIEM_NO : 0;
                                isPC_CONGVU_CO = decimal.TryParse(workSheet.Cells[startRowTB, 9].Value == null ? "0" : workSheet.Cells[startRowTB, 9].Value.ToString(), out _PC_CONGVU_NO);
                                detail.PC_CONGVU = isPC_CONGVU_CO ? _PC_CONGVU_NO : 0;
                                isPC_LOAIXA_CO = decimal.TryParse(workSheet.Cells[startRowTB, 10].Value == null ? "0" : workSheet.Cells[startRowTB, 10].Value.ToString(), out _PC_LOAIXA_NO);
                                detail.PC_LOAIXA = isPC_LOAIXA_CO ? _PC_LOAIXA_NO : 0;
                                isPC_LAUNAM_CO = decimal.TryParse(workSheet.Cells[startRowTB, 11].Value == null ? "0" : workSheet.Cells[startRowTB, 11].Value.ToString(), out _PC_LAUNAM_NO);
                                detail.PC_LAUNAM = isPC_LAUNAM_CO ? _PC_LAUNAM_NO : 0;
                                isPC_THUHUT_CO = decimal.TryParse(workSheet.Cells[startRowTB, 12].Value == null ? "0" : workSheet.Cells[startRowTB, 12].Value.ToString(), out _PC_THUHUT_NO);
                                detail.PC_THUHUT = isPC_THUHUT_CO ? _PC_THUHUT_NO : 0;
                                isCKPT_BHXH_CO = decimal.TryParse(workSheet.Cells[startRowTB, 13].Value == null ? "0" : workSheet.Cells[startRowTB, 13].Value.ToString(), out _CKPT_BHXH_NO);
                                detail.PC_THUHUT = isCKPT_BHXH_CO ? _CKPT_BHXH_NO : 0;
                                isCKPT_BHYT_CO = decimal.TryParse(workSheet.Cells[startRowTB, 14].Value == null ? "0" : workSheet.Cells[startRowTB, 14].Value.ToString(), out _CKPT_BHYT_NO);
                                detail.PC_THUHUT = isCKPT_BHYT_CO ? _CKPT_BHYT_NO : 0;
                                _L_PC_UBDetailService.Insert(detail);
                                _L_PC_UBDetailService.Insert(detail);
                                startRowNB += 1;
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

        [HttpPost]
        public async Task<IHttpActionResult> Post(PHB_L_PC_UBVm.DTO model)
        {
            var response = new Response<string>();
            try
            {
                var data = new PHB_L_PC_UB();
                //data = Mapper.Map<PHB_L_PC_UBVm.DTO, PHB_L_PC_UB>(model);
                var checkExist = _L_PC_UBService.Queryable().FirstOrDefault(x => x.MA_DBHC.Equals(model.MA_DBHC) &&
                    x.MA_CHUONG.Equals(model.MA_CHUONG) && x.NAM_BC.Equals(model.NAM_BC) && x.MA_QHNS.Equals(model.MA_DVQHNS) && x.KY_BC.Equals(model.KY_BC));
                if (checkExist != null)
                {
                    response.Error = true;
                    response.Message = "Báo cáo đã tồn tại";
                }
                else
                {
                    if (model.MA_BAO_CAO.Equals("L_PC_UB"))
                    {
                        data.REFID = Guid.NewGuid().ToString("N");
                        data.MA_QHNS = model.MA_DVQHNS;
                        data.MA_DBHC = model.MA_DBHC;
                        data.MA_CHUONG = model.MA_CHUONG;
                        data.NAM_BC = model.NAM_BC;
                        data.KY_BC = model.KY_BC;
                        data.TRANG_THAI = 0;
                        data.TEN_QHNS = model.TEN_DVQHNS;
                        data.NGAY_TAO = DateTime.Now;
                        data.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                    }

                    if (!string.IsNullOrEmpty(data.MA_DBHC))
                    {
                        data.ObjectState = ObjectState.Added;
                        _L_PC_UBService.Insert(data);
                        if (model.MA_BAO_CAO == "L_PC_UB")
                        {
                            if (model.DataDetails != null)
                            {
                                foreach (var bm in model.DataDetails)
                                {
                                    bm.PHB_L_PC_UB_REFID = data.REFID;
                                    bm.LOAI = model.KY_BC;
                                    bm.ObjectState = ObjectState.Added;
                                    _L_PC_UBDetailService.Insert(bm);
                                }
                            }
                        }
                    }

                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = "Thêm mới thành công";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Thêm mới không thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [Route("GetDetailByRefId/{MA_QHNS}/{NAM_BC}/{KY_BC}")]
        [HttpGet]
        public IHttpActionResult GetDetailByRefId(string MA_QHNS, int NAM_BC, int KY_BC)
        {
            if (string.IsNullOrEmpty(MA_QHNS)) return BadRequest();
            //var response = new Response<PHB_L_PC_UB_DETAIL>();
            var result = new Response<List<PHB_L_PC_UB_DETAIL>>();
            //Response<List<PHB_L_PC_UB_DETAIL>> listResult = new Response<List<PHB_L_PC_UB_DETAIL>>();
            List<PHB_L_PC_UB_DETAIL> listResult = new List<PHB_L_PC_UB_DETAIL>();
            string connectString = ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString;
            using (OracleConnection connection = new OracleConnection(connectString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    OracleTransaction transaction;
                    OracleCommand command = new OracleCommand();
                    command.BindByName = true;

                    command.Connection = connection;
                    // Start a local transaction
                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    // Assign transaction object for a pending local transaction
                    command.Transaction = transaction;
                    try
                    {
                        int KY_BC_TRUOC = KY_BC;
                        int NAM_BC_TRUOC = NAM_BC;
                        command.CommandType = CommandType.Text;
                        if (KY_BC == 1)
                        {
                            KY_BC_TRUOC = 12;
                            NAM_BC_TRUOC = NAM_BC - 1;
                        }
                        else
                        {
                            KY_BC_TRUOC = KY_BC - 1;
                        }
                        command.CommandText = @"SELECT a.KY_BC,a.ID,b.PHB_L_PC_UB_REFID,b.STT,b.HO_VATEN,b.CHUC_DANH,b.HE_SOLUONG,
                        b.PC_KV,b.PC_CHUCVU,b.PC_THAMNIEN,b.PC_TRACHNHIEM,b.PC_CONGVU,b.PC_LOAIXA,b.PC_LAUNAM,b.PC_THUHUT,b.CKPT_BHXH,b.CKPT_BHYT
                        FROM PHB_L_PC_UB a INNER JOIN PHB_L_PC_UB_DETAIL b ON a.REFID = b.phb_l_pc_ub_refid AND a.KY_BC = b.LOAI
                        WHERE a.MA_QHNS = :MA_QHNS AND a.NAM_BC = :NAM_BC_TRUOC AND a.KY_BC = :KY_BC_TRUOC
                        UNION
                        SELECT a.KY_BC,a.ID,b.PHB_L_PC_UB_REFID,b.STT,b.HO_VATEN,b.CHUC_DANH,b.HE_SOLUONG,
                        b.PC_KV,b.PC_CHUCVU,b.PC_THAMNIEN,b.PC_TRACHNHIEM,b.PC_CONGVU,b.PC_LOAIXA,b.PC_LAUNAM,b.PC_THUHUT,b.CKPT_BHXH,b.CKPT_BHYT
                        FROM PHB_L_PC_UB a INNER JOIN PHB_L_PC_UB_DETAIL b ON a.REFID = b.phb_l_pc_ub_refid AND a.KY_BC = b.LOAI
                        WHERE a.MA_QHNS = :MA_QHNS AND a.NAM_BC = :NAM_BC AND a.KY_BC = :KY_BC
                        ORDER BY KY_BC
                        ";
                        command.Parameters.Add(@"MA_QHNS", OracleDbType.Varchar2, 50).Value = MA_QHNS;
                        //command.Parameters.Add(@"REFID", OracleDbType.Varchar2, 50).Value = REFID;
                        command.Parameters.Add(@"NAM_BC", OracleDbType.Int32).Value = NAM_BC;
                        command.Parameters.Add(@"KY_BC", OracleDbType.Int32).Value = KY_BC;
                        command.Parameters.Add(@"KY_BC_TRUOC", OracleDbType.Int32).Value = KY_BC_TRUOC;
                        command.Parameters.Add(@"NAM_BC_TRUOC", OracleDbType.Int32).Value = NAM_BC_TRUOC;
                        OracleDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                PHB_L_PC_UB_DETAIL rc = new PHB_L_PC_UB_DETAIL();
                                rc.PHB_L_PC_UB_REFID = dataReader["PHB_L_PC_UB_REFID"].ToString();
                                rc.STT = dataReader["STT"].ToString();
                                rc.HO_VATEN = dataReader["HO_VATEN"].ToString();
                                rc.CHUC_DANH = dataReader["CHUC_DANH"].ToString();
                                decimal HE_SOLUONG = 0;
                                decimal.TryParse(dataReader["HE_SOLUONG"].ToString(), out HE_SOLUONG);
                                rc.HE_SOLUONG = HE_SOLUONG;

                                decimal PC_KV = 0;
                                decimal.TryParse(dataReader["PC_KV"].ToString(), out PC_KV);
                                rc.PC_KV = PC_KV;

                                decimal PC_CHUCVU = 0;
                                decimal.TryParse(dataReader["PC_CHUCVU"].ToString(), out PC_CHUCVU);
                                rc.PC_CHUCVU = PC_CHUCVU;

                                decimal PC_THAMNIEN = 0;
                                decimal.TryParse(dataReader["PC_THAMNIEN"].ToString(), out PC_THAMNIEN);
                                rc.PC_THAMNIEN = PC_THAMNIEN;

                                decimal PC_TRACHNHIEM = 0;
                                decimal.TryParse(dataReader["PC_TRACHNHIEM"].ToString(), out PC_TRACHNHIEM);
                                rc.PC_TRACHNHIEM = PC_TRACHNHIEM;

                                decimal PC_CONGVU = 0;
                                decimal.TryParse(dataReader["PC_CONGVU"].ToString(), out PC_CONGVU);
                                rc.PC_CONGVU = PC_CONGVU;

                                decimal PC_LOAIXA = 0;
                                decimal.TryParse(dataReader["PC_LOAIXA"].ToString(), out PC_LOAIXA);
                                rc.PC_LOAIXA = PC_LOAIXA;

                                decimal PC_LAUNAM = 0;
                                decimal.TryParse(dataReader["PC_LAUNAM"].ToString(), out PC_LAUNAM);
                                rc.PC_LAUNAM = PC_LAUNAM;

                                decimal PC_THUHUT = 0;
                                decimal.TryParse(dataReader["PC_THUHUT"].ToString(), out PC_THUHUT);
                                rc.PC_THUHUT = PC_THUHUT;

                                decimal CKPT_BHXH = 0;
                                decimal.TryParse(dataReader["CKPT_BHXH"].ToString(), out CKPT_BHXH);
                                rc.CKPT_BHXH = CKPT_BHXH;

                                decimal CKPT_BHYT = 0;
                                decimal.TryParse(dataReader["CKPT_BHYT"].ToString(), out CKPT_BHYT);
                                rc.CKPT_BHYT = CKPT_BHYT;

                                listResult.Add(rc);
                            }
                        }
                        result.Data = listResult;
                        result.Error = false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.Data = null;
                        result.Error = false;
                    }
                }
            }
            return Ok(result);
        }

        [Route("GetDetailByRefIdUpdate/{REFID}/{KY_BC}")]
        [HttpGet]
        public IHttpActionResult GetDetailByRefIdUpdate(string refid, int KY_BC)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            var response = new Response<PHB_L_PC_UB_DETAIL>();
            try
            {
                Response<List<PHB_L_PC_UB_DETAIL>> result = new Response<List<PHB_L_PC_UB_DETAIL>>();
                var DataDetails = _L_PC_UBDetailService.Queryable().Where(x => x.PHB_L_PC_UB_REFID.Equals(refid) && x.LOAI.Equals(KY_BC)).ToList();
                result.Data = DataDetails;
                return Ok(result);

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        //Sửa bằng dto
        //[HttpPut]
        //public async Task<IHttpActionResult> Put(PHB_L_PC_UBVm.EditModel model)
        //{
        //    if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
        //    Response<string> response = new Response<string>();
        //    bool hasValue = false;
        //    try
        //    {
        //        PHB_L_PC_UB L_PC_UB = await _L_PC_UBService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID) && x.KY_BC.Equals(model.KY_BC));
        //        if (L_PC_UB == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
        //        #region Delete
        //        if (model.LstDelete != null && model.LstDelete.Count > 0)
        //        {
        //            hasValue = true;
        //            foreach (var itemId in model.LstDelete)
        //            {
        //                PHB_L_PC_UB_DETAIL item = await _L_PC_UBDetailService.FindByIdAsync(itemId);
        //                if (item != null)
        //                {
        //                    item.ObjectState = ObjectState.Deleted;
        //                    _L_PC_UBDetailService.Delete(item);
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Add
        //        if (model.LstAdd != null && model.LstAdd.Count > 0)
        //        {
        //            hasValue = true;
        //            foreach (var item in model.LstAdd)
        //            {
        //                item.ObjectState = ObjectState.Added;
        //                item.PHB_L_PC_UB_REFID = model.REFID;
        //                _L_PC_UBDetailService.Insert(item);
        //            }
        //        }
        //        #endregion

        //        #region Edit
        //        if (model.LstEdit != null && model.LstEdit.Count > 0)
        //        {
        //            hasValue = true;
        //            foreach (var item in model.LstEdit)
        //            {
        //                PHB_L_PC_UB_DETAIL detail = await _L_PC_UBDetailService.FindByIdAsync(item.ID);
        //                if (detail != null)
        //                {
        //                    detail.ObjectState = ObjectState.Modified;
        //                    detail.HE_SOLUONG = item.HE_SOLUONG;
        //                    detail.PC_KV = item.PC_KV;
        //                    detail.PC_CHUCVU = item.PC_CHUCVU;
        //                    detail.PC_THAMNIEN = item.PC_THAMNIEN;
        //                    detail.PC_TRACHNHIEM = item.PC_TRACHNHIEM;
        //                    detail.PC_CONGVU = item.PC_CONGVU;
        //                    detail.PC_LOAIXA = item.PC_LOAIXA;
        //                    detail.PC_LAUNAM = item.PC_LAUNAM;
        //                    detail.PC_THUHUT = item.PC_THUHUT;
        //                    detail.CKPT_BHXH = item.CKPT_BHXH;
        //                    detail.CKPT_BHYT = item.CKPT_BHYT;
        //                    _L_PC_UBDetailService.Update(detail);
        //                }
        //            }
        //        }
        //        #endregion

        //        if (hasValue)
        //        {
        //            L_PC_UB.NGAY_SUA = DateTime.Now;
        //            L_PC_UB.NGUOI_SUA = RequestContext.Principal.Identity.Name;
        //            _L_PC_UBService.Update(L_PC_UB);

        //            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
        //            {
        //                response.Error = false;
        //                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //            }
        //            else
        //            {
        //                response.Error = true;
        //                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
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
        //        response.Message = ErrorMessage.EMPTY_DATA;
        //    }
        //    return Ok(response);
        //}

        [HttpPut]
        public async Task<IHttpActionResult> Put(PHB_L_PC_UBVm.DTO model)
        {
            Response<string> response = new Response<string>();
            try
            {
                var temp = _L_PC_UBService.FindById(model.ID);
                if (temp != null)
                {
                    var lstDetail = _L_PC_UBDetailService.Queryable().Where(x => x.PHB_L_PC_UB_REFID == temp.REFID).ToList();
                    if (lstDetail.Count > 0)
                    {
                        foreach (var item in lstDetail)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _L_PC_UBDetailService.Delete(item);
                        }
                        if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                        {
                            temp.MA_CHUONG = model.MA_CHUONG;
                            temp.NAM_BC = model.NAM_BC;
                            temp.NGAY_SUA = DateTime.Now;
                            temp.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                            temp.ObjectState = ObjectState.Modified;
                            if (model.DataDetails != null && model.DataDetails.Count > 0)
                            {
                                foreach (var itemDetail in model.DataDetails)
                                {
                                    PHB_L_PC_UB_DETAIL itemInsert = new PHB_L_PC_UB_DETAIL();
                                    itemInsert.PHB_L_PC_UB_REFID = temp.REFID;
                                    itemInsert.LOAI = model.KY_BC;
                                    itemInsert.STT = itemDetail.STT;
                                    itemInsert.HO_VATEN = itemDetail.HO_VATEN;
                                    itemInsert.HE_SOLUONG = itemDetail.HE_SOLUONG;
                                    itemInsert.CHUC_DANH = itemDetail.CHUC_DANH;
                                    itemInsert.PC_KV = itemDetail.PC_KV;
                                    itemInsert.PC_CHUCVU = itemDetail.PC_CHUCVU;
                                    itemInsert.PC_THAMNIEN = itemDetail.PC_THAMNIEN;
                                    itemInsert.PC_TRACHNHIEM = itemDetail.PC_TRACHNHIEM;
                                    itemInsert.PC_CONGVU = itemDetail.PC_CONGVU;
                                    itemInsert.PC_LOAIXA = itemDetail.PC_LOAIXA;
                                    itemInsert.PC_LAUNAM = itemDetail.PC_LAUNAM;
                                    itemInsert.PC_THUHUT = itemDetail.PC_THUHUT;
                                    itemInsert.CKPT_BHXH = itemDetail.CKPT_BHXH;
                                    itemInsert.CKPT_BHYT = itemDetail.CKPT_BHYT;

                                    itemInsert.ObjectState = ObjectState.Added;
                                    _L_PC_UBDetailService.Insert(itemInsert);
                                }
                            }
                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Error = false;
                                response.Message = "Cập nhật thành công.";
                                return Ok(response);
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = "Lỗi cập nhật dữ liệu.";
                                return Ok(response);

                            }
                        }
                    }
                    else
                    {
                        temp.MA_CHUONG = model.MA_CHUONG;
                        temp.NAM_BC = model.NAM_BC;
                        temp.NGAY_SUA = DateTime.Now;
                        temp.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                        temp.ObjectState = ObjectState.Modified;
                        if (model.DataDetails != null && model.DataDetails.Count > 0)
                        {
                            foreach (var itemDetail in model.DataDetails)
                            {
                                PHB_L_PC_UB_DETAIL itemInsert = new PHB_L_PC_UB_DETAIL();
                                itemInsert.PHB_L_PC_UB_REFID = temp.REFID;
                                itemInsert.LOAI = model.KY_BC;
                                itemInsert.STT = itemDetail.STT;
                                itemInsert.HO_VATEN = itemDetail.HO_VATEN;
                                itemInsert.HE_SOLUONG = itemDetail.HE_SOLUONG;
                                itemInsert.CHUC_DANH = itemDetail.CHUC_DANH;
                                itemInsert.PC_KV = itemDetail.PC_KV;
                                itemInsert.PC_CHUCVU = itemDetail.PC_CHUCVU;
                                itemInsert.PC_THAMNIEN = itemDetail.PC_THAMNIEN;
                                itemInsert.PC_TRACHNHIEM = itemDetail.PC_TRACHNHIEM;
                                itemInsert.PC_CONGVU = itemDetail.PC_CONGVU;
                                itemInsert.PC_LOAIXA = itemDetail.PC_LOAIXA;
                                itemInsert.PC_LAUNAM = itemDetail.PC_LAUNAM;
                                itemInsert.PC_THUHUT = itemDetail.PC_THUHUT;
                                itemInsert.CKPT_BHXH = itemDetail.CKPT_BHXH;
                                itemInsert.CKPT_BHYT = itemDetail.CKPT_BHYT;

                                itemInsert.ObjectState = ObjectState.Added;
                                _L_PC_UBDetailService.Insert(itemInsert);
                            }
                        }
                        if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                        {
                            response.Error = false;
                            response.Message = "Cập nhật thành công.";
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = "Lỗi cập nhật dữ liệu.";
                        }
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "Không tìm thấy kết quả";
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

    }
}