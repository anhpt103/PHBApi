using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Helper;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity;
using BTS.SP.PHB.SERVICE.REPORT.DUTOANLUONG;
using BTS.SP.PHB.ENTITY.Rp.DUTOANLUONG;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using BTS.SP.PHB.ENTITY;
using System.Linq;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/PhbDUTOANLUONG")]
    [Route("{id?}")]
    public class PhbDuToanLuonController : ApiController
    {
        private readonly IPhbDuToanLuongService _bm48TT342Service;
        private readonly IPhbDuToanLuongTemplateService _bm48TT342TemplateService;
        private readonly IPhbDuToanLuongDetailService _bm48TT342DetailService;
        private ISysDvqhns_QuanLyService _sysDvqhns_QuanLyService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbDuToanLuonController(IPhbDuToanLuongService bm48TT342Service, IPhbDuToanLuongTemplateService bm48TT342TemplateService,ISysDvqhns_QuanLyService sysDvqhns_QuanLyService,
        IPhbDuToanLuongDetailService bm48TT342DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bm48TT342Service = bm48TT342Service;
            _bm48TT342TemplateService = bm48TT342TemplateService;
            _bm48TT342DetailService = bm48TT342DetailService;
            _sysDvqhns_QuanLyService = sysDvqhns_QuanLyService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region Upload
        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var b01Bcqt = new PHB_DUTOANLUONG()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };

                        var workSheet = excelPackage.Workbook.Worksheets["BTTTL"];
                        if (workSheet != null)
                        {
                            b01Bcqt.MA_CHUONG = "1";
                            //if (string.IsNullOrEmpty(workSheet.Cells[1, 3].Text)) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị."
                            //});
                            //b01Bcqt.MA_DV_SDNS = workSheet.Cells[1, 3].Text;
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            b01Bcqt.MA_QHNS = httpRequest["MA_QHNS"];
                            //b01Bcqt.TEN_QHNS = httpRequest["TEN_QHNS"];
                            //b01Bcqt.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b01Bcqt.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            //if (string.IsNullOrEmpty(workSheet.Cells[5, 20].Text)) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có kỳ báo cáo."
                            //});
                            //b01Bcqt.KY_BC = int.Parse(workSheet.Cells[5, 20].Text);
                            if (string.IsNullOrEmpty(workSheet.Cells[3, 3].Text)) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có hệ số lương"
                            });
                            b01Bcqt.HE_SO = double.Parse(workSheet.Cells[3, 3].Text);

                            var checkReport = await _bm48TT342Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DV_SDNS.Equals(b01Bcqt.MA_DV_SDNS) && x.MA_QHNS.Equals(b01Bcqt.MA_QHNS) &&
                                x.NAM_BC == b01Bcqt.NAM_BC && x.KY_BC == b01Bcqt.KY_BC);
                            if (checkReport != null)
                            {
                                b01Bcqt.REFID = checkReport.REFID;
                                _bm48TT342Service.Update(checkReport);
                                var lstDetail = _bm48TT342DetailService.Queryable().Where(x => x.PHB_DUTOANLUONG_REFID == checkReport.REFID).ToList();
                                if(lstDetail.Count > 0)
                                {
                                    foreach(var item in lstDetail)
                                    {
                                        _bm48TT342DetailService.Delete(item);
                                    }
                                }
                                //if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                                //{
                                //    response.Data = b01Bcqt.REFID;
                                //    response.Error = false;
                                //    response.Message = "Cập nhật thành công.";
                                //}
                                //else
                                //{
                                //    response.Error = true;
                                //    response.Message = "Lỗi cập nhật dữ liệu.";
                                //}
                                //return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            }
                            else
                            {
                                _bm48TT342Service.Insert(b01Bcqt);
                            }
                            int start_Row = 9;
                            int end_Row = 20;
                            int start_Col = 3;
                            int count = 1;
                            int countAll = 1;

                            var lama = "";
                            for (int r = start_Row ; r <= int.MaxValue; r++)
                            {
                                if(string.IsNullOrEmpty( workSheet.Cells[r, 2].Text))
                                {
                                    break;
                                }
                                else
                                {
                                    var obj = new PHB_DUTOANLUONG_DETAIL() { PHB_DUTOANLUONG_REFID = b01Bcqt.REFID, ObjectState = ObjectState.Added };
                                    double cot_1 = 0,
                                        cot_2 = 0,
                                        cot_3 = 0,
                                        cot_4 = 0,
                                       cot_5 = 0,
                                       cot_6 = 0,
                                       cot_7 = 0,
                                       cot_8 = 0,
                                       cot_9 = 0,
                                       cot_10 = 0,
                                       cot_11 = 0,
                                       cot_12 = 0,
                                       cot_13 = 0,
                                       cot_14 = 0,
                                       cot_15 = 0,
                                       cot_16 = 0,
                                       cot_17 = 0,
                                       cot_18 = 0,
                                       cot_19 = 0,
                                       cot_20 = 0,
                                       cot_21 = 0,
                                       cot_22 = 0,
                                       cot_23 = 0,
                                       cot_24 = 0,
                                       cot_25 = 0,
                                       cot_26 = 0,
                                       cot_27 = 0,
                                       cot_28 = 0,
                                       cot_29 = 0,
                                       cot_30 = 0,
                                       cot_31 = 0,
                                       cot_32 = 0,
                                       cot_33 = 0,
                                       cot_34 = 0,
                                       cot_35 = 0,
                                       cot_36 = 0,
                                       cot_37 = 0,
                                       cot_38 = 0;
                                    ;
                                    if (workSheet.Cells[r, 1].Text.Equals("I") || workSheet.Cells[r, 1].Text.Equals("II"))
                                    {
                                        lama = workSheet.Cells[r, 1].Text;
                                        count = 1;
                                        obj.STT = countAll;
                                        obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Value?.ToString();
                                        obj.MA_CHI_TIEU = lama;
                                        obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;

                                    }
                                    else
                                    {
                                        obj.STT = countAll;
                                        obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Value?.ToString();
                                        obj.MA_CHI_TIEU = lama + count;
                                        obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;

                                    }
                                    double.TryParse(workSheet.Cells[r, 3].Text.ToString(), out cot_1);
                                    double.TryParse(workSheet.Cells[r, 4].Text.ToString(), out cot_2);
                                    //double.TryParse(workSheet.Cells[r, 5].Text.ToString(), out cot_3);
                                    double.TryParse(workSheet.Cells[r, 6].Text.ToString(), out cot_4);
                                    double.TryParse(workSheet.Cells[r, 7].Text.ToString(), out cot_5);
                                    double.TryParse(workSheet.Cells[r, 8].Text.ToString(), out cot_6);
                                    double.TryParse(workSheet.Cells[r, 9].Text.ToString(), out cot_7);
                                    double.TryParse(workSheet.Cells[r, 10].Text.ToString(), out cot_8);
                                    double.TryParse(workSheet.Cells[r, 11].Text.ToString(), out cot_9);
                                    double.TryParse(workSheet.Cells[r, 12].Text.ToString(), out cot_10);
                                    double.TryParse(workSheet.Cells[r, 12].Text.ToString(), out cot_11);
                                    double.TryParse(workSheet.Cells[r, 14].Text.ToString(), out cot_12);
                                    double.TryParse(workSheet.Cells[r, 15].Text.ToString(), out cot_13);
                                    double.TryParse(workSheet.Cells[r, 16].Text.ToString(), out cot_14);
                                    double.TryParse(workSheet.Cells[r, 17].Text.ToString(), out cot_15);
                                    double.TryParse(workSheet.Cells[r, 18].Text.ToString(), out cot_16);
                                    double.TryParse(workSheet.Cells[r, 19].Text.ToString(), out cot_17);
                                    double.TryParse(workSheet.Cells[r, 20].Text.ToString(), out cot_18);
                                    double.TryParse(workSheet.Cells[r, 21].Text.ToString(), out cot_19);
                                    double.TryParse(workSheet.Cells[r, 22].Text.ToString(), out cot_20);
                                    double.TryParse(workSheet.Cells[r, 23].Text.ToString(), out cot_21);
                                    double.TryParse(workSheet.Cells[r, 24].Text.ToString(), out cot_22);
                                    double.TryParse(workSheet.Cells[r, 25].Text.ToString(), out cot_23);
                                    double.TryParse(workSheet.Cells[r, 26].Text.ToString(), out cot_24);
                                    double.TryParse(workSheet.Cells[r, 27].Text.ToString(), out cot_25);
                                    double.TryParse(workSheet.Cells[r, 28].Text.ToString(), out cot_26);
                                    double.TryParse(workSheet.Cells[r, 29].Text.ToString(), out cot_27);
                                    double.TryParse(workSheet.Cells[r, 30].Text.ToString(), out cot_28);
                                    double.TryParse(workSheet.Cells[r, 31].Text.ToString(), out cot_29);
                                    double.TryParse(workSheet.Cells[r, 32].Text.ToString(), out cot_30);
                                    double.TryParse(workSheet.Cells[r, 33].Text.ToString(), out cot_31);
                                    double.TryParse(workSheet.Cells[r, 34].Text.ToString(), out cot_32);
                                    double.TryParse(workSheet.Cells[r, 35].Text.ToString(), out cot_33);
                                    double.TryParse(workSheet.Cells[r, 36].Text.ToString(), out cot_34);
                                    double.TryParse(workSheet.Cells[r, 37].Text.ToString(), out cot_35);
                                    double.TryParse(workSheet.Cells[r, 38].Text.ToString(), out cot_36);
                                    double.TryParse(workSheet.Cells[r, 39].Text.ToString(), out cot_37);
                                    double.TryParse(workSheet.Cells[r, 40].Text.ToString(), out cot_38);
                                    //
                                    obj.BC_DUOC_CAP = cot_1;
                                    obj.BC_CO_MAT = cot_2;
                                    obj.MA_NGACH = workSheet.Cells[r, 5].Text;
                                    obj.HS_LUONG = cot_4;
                                    obj.HS_PC_CV = cot_5;
                                    obj.HS_PC_KV = cot_6;
                                    obj.HS_PC_TH = cot_7;
                                    obj.HS_PC_LT = cot_8;
                                    obj.HS_PC_NN_DH = cot_9;
                                    obj.HS_HD_DBQH_DBND = cot_10;
                                    obj.HS_PC_UDN = cot_11;
                                    obj.HS_PC_TNN = cot_12;
                                    obj.HS_PC_TN_NGHE_CV = cot_13;
                                    obj.HS_PC_TRUC = cot_14;
                                    obj.HS_PC_TN_VUOT_KHUNG = cot_15;
                                    obj.HS_PC_DB_KHAC = cot_16;
                                    obj.HS_PC_CT_LN = cot_17;
                                    obj.HS_PC_LX = cot_18;
                                    obj.HS_PC_CT_D_DTCT_XH = cot_19;
                                    obj.HS_PC_CVU = cot_20;
                                    obj.HS_PC_KHAC = cot_21;
                                    obj.CONG_HS = cot_22;
                                    obj.TIEN_LUONG_THANG = cot_23;
                                    obj.BHXH_CP = cot_24;
                                    obj.BHXH_LUONG = cot_25;
                                    obj.BHYT_CP = cot_26;
                                    obj.BHYT_LUONG = cot_27;
                                    obj.BHTN_CP = cot_28;
                                    obj.BHTN_LUONG = cot_29;
                                    obj.BH_TNLD_BNN_CP = cot_30;
                                    obj.BH_TNLD_BNN_LUONG = cot_31;
                                    obj.KPCD_CP = cot_32;
                                    obj.KPCD_LUONG = cot_33;
                                    obj.KPCD_NOP_CD = cot_34;
                                    obj.KPCD_DE_LAI_DV = cot_35;
                                    obj.THUE_TNCN = cot_36;
                                    obj.GT_DC = cot_37;
                                    obj.SO_THUC_LINH = cot_38;
                                    obj.GHI_CHI = workSheet.Cells[r, 41].Text;
                                    _bm48TT342DetailService.Insert(obj);
                                    count++;
                                    countAll++;
                                }

                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b01Bcqt.REFID;
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

        #region GetDsDvQhnsByMaDBHC
        [Route("GetDsDvQhnsByMaDBHC/{madbhc}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByMaDBHC(string madbhc)
        {
            var response = new List<SysDvqhns_QuanLyVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        if (!string.IsNullOrEmpty(madbhc))
                        {
                            command.CommandText = "select MA_DVQHNS,TEN_DVQHNS,MA_CHUONG, MA_DBHC from sys_dvqhns_quanly  where MA_DBHC like '" + madbhc + "' order by MA_DBHC";
                            command.Parameters.Clear();
                            using (var dataReader = command.ExecuteReaderAsync())
                            {
                                if (dataReader.Result.HasRows)
                                {
                                    while (dataReader.Result.Read())
                                    {
                                        response.Add(new SysDvqhns_QuanLyVm.ViewModel()
                                        {
                                            MA_DVQHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                            TEN_DVQHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                            MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                            MA_DBHC = dataReader.Result["MA_DBHC"].ToString(),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
            }
            return Ok(response);
        }
        #endregion

    }
}