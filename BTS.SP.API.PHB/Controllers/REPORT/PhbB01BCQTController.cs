using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B01;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT.B01BCQT;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB01BCQT")]
    [Route("{id?}")]
    public class PhbB01BCQTController : ApiController
    {
        private readonly IPhbB01BCQTService _b01BcqtService;
        private readonly IPhbB01BCQTTemplateService _b01BcqtTemplateService;
        private readonly IPhbB01BCQTDetailService _b01BcqtDetailService;
        private readonly ISysDvqhnsService _sysDvqhnsService;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDmChuongService _dmChuong;

        public PhbB01BCQTController(IPhbB01BCQTService b01BcqtService, IPhbB01BCQTTemplateService b01BcqtTemplateService,
            IPhbB01BCQTDetailService b01BcqtDetailService, IDmLoaiKhoanService loaiKhoanservice, ISysDvqhnsService sysDvqhnsService, IDM_NGANHKTService dmNganhktService, IUnitOfWorkAsync unitOfWorkAsync, IDmChuongService dmChuong)
        {
            _b01BcqtService = b01BcqtService;
            _b01BcqtTemplateService = b01BcqtTemplateService;
            _b01BcqtDetailService = b01BcqtDetailService;
            _sysDvqhnsService = sysDvqhnsService;
            _dmNganhktService = dmNganhktService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
        }

        #region Get Template
        [Route("GetTemplate")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHB_B01BCQT_TEMPLATE>> response = new Response<List<PHB_B01BCQT_TEMPLATE>>();
            
            try
            {
                response.Error = false;
                response.Data = await _b01BcqtTemplateService.Queryable().OrderBy(x => x.SAP_XEP).ToListAsync();
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
                    using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
                    {
                        var b01Bcqt = new PHB_B01BCQT()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,                 
                            ObjectState = ObjectState.Added,
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
                            b01Bcqt.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            b01Bcqt.MA_QHNS = httpRequest["MA_QHNS"];
                            b01Bcqt.TEN_QHNS = httpRequest["TEN_QHNS"];
                            b01Bcqt.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b01Bcqt.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            b01Bcqt.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _b01BcqtService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(b01Bcqt.MA_CHUONG) && x.MA_QHNS.Equals(b01Bcqt.MA_QHNS) &&
                                x.NAM_BC == b01Bcqt.NAM_BC && x.KY_BC == b01Bcqt.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _b01BcqtService.Insert(b01Bcqt);

                            int start_Row = 9; 
                            int end_Row = 112;
                            int start_Col = 6;
                            int count = 1;
                           
                            for (int r = start_Row + 2; r <= end_Row; r++)
                            {
                               
                                    for (int c = start_Col; c <= workSheet.Dimension.End.Column; c++)
                                    {
                                        var tmpKhoan = workSheet.Cells[start_Row, c].Text;
                                        if (tmpKhoan.StartsWith("K"))
                                        {
                                            var obj = new PHB_B01BCQT_DETAIL() { PHB_B01BCQT_REFID = b01Bcqt.REFID, ObjectState = ObjectState.Added };
                                            obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Value?.ToString();
                                            obj.MA_CHI_TIEU = count.ToString();
                                            obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;
                                            obj.MA_SO = workSheet.Cells[r, 3].Text;
                                            var Khoan = tmpKhoan.Substring(6, tmpKhoan.Length - 6);
                                            obj.MA_KHOAN = Khoan;
                                            obj.GIA_TRI = workSheet.Cells[r,c].Value != null ? decimal.Parse(workSheet.Cells[r,c].Value.ToString()) : 0;
                                            _b01BcqtDetailService.Insert(obj);
                                        }

                                    }
                                count++;
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

        #region Get Detail
        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            //if (string.IsNullOrEmpty(refid) || string.IsNullOrEmpty(matemplate)) return BadRequest();
            var response = new Response<PHB_B01BCQTVm.DataRes<PHB_B01BCQTVm.BaoCaoDetail>>();
            List<string> MaLoai = new List<string>();
            List <PHB_B01BCQTVm.LoaiKhoanItem> lstLK = new List<PHB_B01BCQTVm.LoaiKhoanItem>();
            decimal sumTotal = 0;
            var indextotal = new List<int>();
            var valuetotal = new List<decimal?>();
            int index = 0; 
            MaLoai = GetK(refid, "2",null);
            foreach(var item in MaLoai)
            {
                var obj = new PHB_B01BCQTVm.LoaiKhoanItem();
                obj.LoaiItem = item;
                obj.KhoanItem = GetK(refid, "1", item).ToList();
                obj.KhoanItem.Insert(0,"000");
                indextotal.Add(index);
                lstLK.Add(obj);
                index += obj.KhoanItem.Count;
            }
            List<PHB_B01BCQTVm.BaoCaoDetail> lstbc = new List<PHB_B01BCQTVm.BaoCaoDetail>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B01BCQT_GET_DETAIL";
                        command.Parameters.Clear();

                        OracleParameter rID = command.Parameters.Add("REF_ID", OracleDbType.Varchar2);
                        rID.Size = 200;
                        rID.Direction = ParameterDirection.Input;
                        rID.Value = refid;

                        OracleParameter tpl = command.Parameters.Add("TYPE_DATA", OracleDbType.Varchar2);
                        tpl.Size = 10;
                        tpl.Direction = ParameterDirection.Input;
                        tpl.Value = "0";
                                              

                        OracleParameter maL = command.Parameters.Add("MALOAI", OracleDbType.Varchar2);
                        maL.Size = 10;
                        maL.Direction = ParameterDirection.Input;
                        maL.Value = null;

                        OracleParameter p_cur = command.Parameters.Add("cur", OracleDbType.RefCursor);
                        p_cur.Direction = ParameterDirection.Output;
                        await command.ExecuteNonQueryAsync();
                      
                        using (var oracleDataReader = ((OracleRefCursor)p_cur.Value).GetDataReader())
                        {
                            
                            while (oracleDataReader.Read())
                            {
                                var obj = new PHB_B01BCQTVm.BaoCaoDetail();
                                obj.MA_SO = oracleDataReader["MA_SO"]?.ToString();
                                obj.SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"]?.ToString());
                                obj.STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString();
                                obj.TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString();

                                foreach(var l in lstLK)
                                {
                                    foreach(var k in l.KhoanItem)
                                    {
                                        var gt = new Dictionary<string, decimal?>();
                                        if (!k.Equals("000"))
                                        {

                                            var value = oracleDataReader["'"+k+"'"]?.ToString();
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                gt.Add(k, decimal.Parse(value));
                                                sumTotal += decimal.Parse(value);
                                            }
                                            else
                                            {
                                                gt.Add(k, null);
                                            }

                                        }                                       
                                        
                                        obj.lstCOL.Add(gt);
                                        
                                    }
                                    
                                    valuetotal.Add(sumTotal);
                                    sumTotal = 0;

                                }

                                
                                for(int i = 0;i < indextotal.Count; i++)
                                {
                                    var tt = new Dictionary<string, decimal?>();
                                    tt.Add("000", valuetotal[i]);
                                    obj.lstCOL[indextotal[i]] = tt;
                                }
                                
                                obj.TONG_SO = valuetotal.Sum();
                                valuetotal.Clear();

                                lstbc.Add(obj);
                            }
                           
                        }
                        //A
                        lstbc[1].TONG_SO = lstbc[2].TONG_SO + lstbc[9].TONG_SO + lstbc[12].TONG_SO + lstbc[15].TONG_SO + lstbc[18].TONG_SO + lstbc[21].TONG_SO + lstbc[30].TONG_SO;
                        lstbc[37].TONG_SO = lstbc[38].TONG_SO + lstbc[39].TONG_SO + lstbc[40].TONG_SO + lstbc[43].TONG_SO + lstbc[44].TONG_SO + lstbc[45].TONG_SO;
                        lstbc[46].TONG_SO = lstbc[47].TONG_SO + lstbc[51].TONG_SO + lstbc[52].TONG_SO + lstbc[53].TONG_SO +lstbc[55].TONG_SO + lstbc[56].TONG_SO + lstbc[60].TONG_SO + lstbc[63].TONG_SO;
                        lstbc[0].TONG_SO = lstbc[1].TONG_SO + lstbc[37].TONG_SO + lstbc[46].TONG_SO;

                        //B
                        lstbc[64].TONG_SO = lstbc[65].TONG_SO + lstbc[68].TONG_SO + lstbc[71].TONG_SO + lstbc[74].TONG_SO + lstbc[77].TONG_SO + lstbc[80].TONG_SO;

                        //C
                        lstbc[84].TONG_SO = lstbc[84].TONG_SO + lstbc[87].TONG_SO + lstbc[90].TONG_SO + lstbc[93].TONG_SO + lstbc[96].TONG_SO + lstbc[99].TONG_SO;

                        // lay nam BC
                        var templst = _b01BcqtService.GetAll();
                        var ins = templst.FirstOrDefault(x => x.REFID.Equals(refid));
                        

                        var res = new PHB_B01BCQTVm.DataRes<PHB_B01BCQTVm.BaoCaoDetail>();
                        res.Body = lstbc;
                        res.Header = lstLK;
                        res.NAM_BC = ins.NAM_BC;

                        response.Error = false;
                        response.Data = res;
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }

            return Ok(response);
        }

        private List<string> GetK(string refID, string _type, string _maloai)
        {
            string type = null;
            if(_type.Equals("1"))
            {
                type = "MA_KHOAN";
            }
            else if(_type.Equals("2"))
            {
                type = "MA_LOAI";
            }
            var result = new List<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                     connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B01BCQT_GET_DETAIL";
                        command.Parameters.Clear();

                        OracleParameter rID = command.Parameters.Add("REF_ID", OracleDbType.Varchar2);
                        rID.Size = 200;
                        rID.Direction = ParameterDirection.Input;
                        rID.Value = refID;

                        OracleParameter tpl = command.Parameters.Add("TYPE_DATA", OracleDbType.Varchar2);
                        tpl.Size = 10;
                        tpl.Direction = ParameterDirection.Input;
                        tpl.Value = _type;
                       

                        OracleParameter maL = command.Parameters.Add("MALOAI", OracleDbType.Varchar2);
                        maL.Size = 10;
                        maL.Direction = ParameterDirection.Input;
                        maL.Value = _maloai;

                        OracleParameter p_cur = command.Parameters.Add("cur", OracleDbType.RefCursor);
                        p_cur.Direction = ParameterDirection.Output;
                        command.ExecuteNonQueryAsync();

                        using (var oracleDataReader = ((OracleRefCursor)p_cur.Value).GetDataReader())
                        {

                            while (oracleDataReader.Read())
                            {
                                result.Add(oracleDataReader[type]?.ToString());                               
                            }

                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region Get Edit
        [Route("GetEditByRefId")]
        [HttpPost]
        public async Task<IHttpActionResult> GetEditByRefId(PHB_B01BCQTVm.InputPara para)
        {
            var response = new Response<List<PHB_B01BCQT_DETAIL>>();
            try
            {
                var rawData = _b01BcqtDetailService.GetAll().Where(x => x.PHB_B01BCQT_REFID.Equals(para.REFID)).ToList();
                rawData.ForEach(x =>
                {
                    x.SAP_XEP = int.Parse(x.MA_CHI_TIEU);
                });
                response.Error = false;
                response.Data = rawData;
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
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(List<PHB_B01BCQT_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_B01BCQT();
            try
            {
                var refid = model.First().PHB_B01BCQT_REFID;
                report = await _b01BcqtService.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();

                if (report == null)
                {
                    response.Message = ErrorMessage.EMPTY_DATA;
                    response.Error = true;
                    return Ok(response);
                }

                //check if report is already censored or not
                if (report.TRANG_THAI == 1)
                {
                    response.Message = "Báo cáo đã được duyệt, không thể chỉnh sửa!";
                    response.Error = true;
                    return Ok(response);
                }

                //add informations about editing user and editing date
                report.NGAY_SUA = DateTime.Now;
                report.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                report.ObjectState = ObjectState.Modified;
                _b01BcqtService.Update(report);

                var lstDetail = await _b01BcqtDetailService.Queryable().Where(detail => detail.PHB_B01BCQT_REFID == report.REFID).ToListAsync();

                //loop to edit each detail
                foreach (var detail in lstDetail)
                {
                    PHB_B01BCQT_DETAIL first = null;
                    foreach (var e in model)
                    {
                        if (e.MA_SO == detail.MA_SO && e.MA_LOAI == detail.MA_LOAI && e.MA_KHOAN == detail.MA_KHOAN)
                        {
                            first = e;
                            detail.GIA_TRI = first.GIA_TRI;
                            detail.ObjectState = ObjectState.Modified;
                            _b01BcqtDetailService.Update(detail);
                            break;
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }
            return Ok(response);
        }

        #endregion

        [Route("GetDMLoaiKhoan")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDMLoaiKhoan()
        {
            Response<List<DM_NGANHKT>> response = new Response<List<DM_NGANHKT>>();
            try
            {
                var toDay = DateTime.Today;
                var result = await _dmNganhktService.Queryable().Where(x => x.NGAY_HET_HL == null || x.NGAY_HET_HL >= toDay).OrderBy(x => x.MA_LOAI).ToListAsync();
                response.Error = false;
                response.Data = result;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        public class InsertObject
        {
            public PHB_B01BCQT PHB_B01BCQT { get; set; }
            public List<PHB_B01BCQT_DETAIL> DETAILS { get; set; }
        }

        [Route("Detail/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(string refid)
        {
            var response = new Response<List<PHB_B01BCQT_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _b01BcqtDetailService.Queryable().Where(detail => detail.PHB_B01BCQT_REFID == refid).OrderBy(detail => detail.SAP_XEP).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                return Ok(response);
            }

            return Ok(response);
        }

        #region Add Content
        [HttpPost]
        [Route("Post")]
        public async Task<IHttpActionResult> Post(InsertObject model)
        {
            var response = new Response<string>();
            try
                {
                    PHB_B01BCQT itemData = new PHB_B01BCQT();
                    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    itemData.MA_QHNS = model.PHB_B01BCQT.MA_QHNS;
                    itemData.TEN_QHNS = model.PHB_B01BCQT.TEN_QHNS;
                    itemData.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                    itemData.NGAY_TAO = DateTime.Now;
                    itemData.NAM_BC = model.PHB_B01BCQT.NAM_BC;
                    itemData.KY_BC = model.PHB_B01BCQT.KY_BC;
                    itemData.MA_CHUONG = model.PHB_B01BCQT.MA_CHUONG;
                    itemData.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _b01BcqtService.Queryable().Count(report => report.MA_QHNS == itemData.MA_QHNS && report.NAM_BC == itemData.NAM_BC);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _b01BcqtService.Insert(itemData);

                foreach (var item in model.DETAILS)
                {
                    item.PHB_B01BCQT_REFID = itemData.REFID;
                    _b01BcqtDetailService.Insert(item);
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
                response.Error = false;
                response.Data = "Thêm thành công!";
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

        #region Sum Report
        [Route("GetSumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSumReport(PHB_B01BCQTVm.SumPara para)
        {
            var response = new Response<PHB_B01BCQTVm.DataRes<PHB_B01BCQTVm.SumDetail>>();
            try
            {
                response.Error = false;
                response.Data = SumRpData(para);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }


            return Ok(response);
        }


        private PHB_B01BCQTVm.DataRes<PHB_B01BCQTVm.SumDetail> SumRpData(PHB_B01BCQTVm.SumPara para)
        {
            var res = new PHB_B01BCQTVm.DataRes<PHB_B01BCQTVm.SumDetail>();
            //============================================================================
            var lstLK = new List<PHB_B01BCQTVm.LoaiKhoanItem>();            

                var MaLoai = new List<string>();
                try
                {
                    var tmplk = new List<PHB_B01BCQT>();
                    if (para.LOAI_BC.Equals("2"))
                    {
                        var sysdvqhs = _sysDvqhnsService.GetAll().Where(x => x.MA_DVQHNS_CHA == para.DSDVQHNS||x.MA_DVQHNS.Equals(para.DSDVQHNS)).Select(x=>x.MA_DVQHNS).ToList();
                        var tmplkbc = _b01BcqtService.GetAll().Where(x => x.KY_BC == para.KY_BC && x.NAM_BC == para.NAM_BC).ToList();
                      
                        foreach (var item in sysdvqhs)
                        {
                            var obj = tmplkbc.SingleOrDefault(x => x.MA_QHNS.Equals(item));
                            if (obj != null) tmplk.Add(obj);
                        }

                    }
                    else if (para.LOAI_BC.Equals("3"))
                    {
                        tmplk = _b01BcqtService.GetAll().Where(x => x.KY_BC == para.KY_BC && x.NAM_BC == para.NAM_BC && x.MA_QHNS.Equals(para.DSDVQHNS)).ToList();
                    }



                    var lstfilter = new List<PHB_B01BCQT_DETAIL>();
                    tmplk.ForEach(x =>
                    {
                        var tmp = _b01BcqtDetailService.GetAll().Where(u => u.PHB_B01BCQT_REFID.Equals(x.REFID)).ToList();
                        lstfilter.AddRange(tmp);
                    });


                    MaLoai = lstfilter.Select(x => x.MA_LOAI).Distinct().OrderBy(x => x).ToList();

                    foreach (var item in MaLoai)
                    {
                        var obj = new PHB_B01BCQTVm.LoaiKhoanItem();
                        obj.LoaiItem = item;
                        obj.KhoanItem = lstfilter.Where(x => x.MA_LOAI.Equals(item)).Select(x => x.MA_KHOAN).Distinct().ToList();
                        obj.KhoanItem.Sort();
                        obj.KhoanItem.Insert(0, "000");
                        lstLK.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

                //============================================================================
                try
                {
                    var lstbc = new List<PHB_B01BCQTVm.SumDetail>();
                    using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                    {
                        connection.OpenAsync();
                        using (OracleCommand command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "PHB_B01BCQT_SUMREPORT";
                            command.Parameters.Clear();

                            OracleParameter loaibc = command.Parameters.Add("LOAIBC", OracleDbType.Varchar2);
                            loaibc.Size = 2;
                            loaibc.Direction = ParameterDirection.Input;
                            loaibc.Value = para.LOAI_BC;

                            OracleParameter maqhns = command.Parameters.Add("MADVQHNS", OracleDbType.Varchar2);
                            maqhns.Size = 100;
                            maqhns.Direction = ParameterDirection.Input;
                            maqhns.Value = para.DSDVQHNS;


                            OracleParameter dbhc = command.Parameters.Add("MADBHC", OracleDbType.Varchar2);
                            dbhc.Size = 10;
                            dbhc.Direction = ParameterDirection.Input;
                            dbhc.Value = para.MA_DBHC;

                            OracleParameter nambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                            nambc.Direction = ParameterDirection.Input;
                            nambc.Value = para.NAM_BC.ToString();

                            OracleParameter kybc = command.Parameters.Add("KY_BC", OracleDbType.Varchar2);
                            kybc.Size = 10;
                            kybc.Direction = ParameterDirection.Input;
                            kybc.Value = para.KY_BC.ToString();

                            OracleParameter p_cur = command.Parameters.Add("cur", OracleDbType.RefCursor);
                            p_cur.Direction = ParameterDirection.Output;
                            command.ExecuteNonQueryAsync();

                            using (var oracleDataReader = ((OracleRefCursor)p_cur.Value).GetDataReader())
                            {

                                while (oracleDataReader.Read())
                                {
                                    var obj = new PHB_B01BCQTVm.SumDetail();
                                    obj.MA_SO = oracleDataReader["MA_SO"]?.ToString();
                                    obj.SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"]?.ToString());
                                    obj.STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString();
                                    obj.TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString();
                                    obj.IS_BOLD = Int32.Parse(oracleDataReader["IS_BOLD"]?.ToString());
                                    obj.IS_ITALIC = Int32.Parse(oracleDataReader["IS_ITALIC"]?.ToString());

                                    foreach (var l in lstLK)
                                    {
                                        decimal sumK = 0;
                                        foreach (var k in l.KhoanItem)
                                        {

                                            if (!k.Equals("000"))
                                            {
                                                var value = oracleDataReader["'" + k + "'"]?.ToString();
                                                if (!string.IsNullOrEmpty(value))
                                                {
                                                    var gt = new PHB_B01BCQTVm.ColObject(k, decimal.Parse(value));
                                                    obj.lstCOL.Add(gt);
                                                    sumK += decimal.Parse(value);
                                                }
                                                else
                                                {
                                                    var gt = new PHB_B01BCQTVm.ColObject(k, 0);
                                                    obj.lstCOL.Add(gt);
                                                }

                                            }
                                            else
                                            {
                                                var gt = new PHB_B01BCQTVm.ColObject("T" + l.LoaiItem, 0);
                                                obj.lstCOL.Add(gt);
                                            }

                                        }
                                        int idx = obj.lstCOL.FindIndex(x => x.KHOAN.Equals("T" + l.LoaiItem));
                                        obj.lstCOL[idx].GIA_TRI = sumK;
                                    }
                                    lstbc.Add(obj);
                                }
                            }

                        }

                    }
                    lstbc.ForEach(x =>
                    {
                        decimal sumK = 0;
                        x.lstCOL.ForEach(u =>
                        {
                            if (!u.KHOAN.StartsWith("T"))
                            {
                                sumK += u.GIA_TRI;
                            }

                        });
                        x.TONG_SO = sumK;
                    });
                    res.Header = lstLK;
                    res.Body = lstbc;
                    return res;
                }
                catch (Exception ex)
                {
                    return null;
                }
        }

        #endregion
        //Export mới
        public class ExportParams
        {
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
            public string MA_DONG { get; set; }
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
            var fileNameInPut = "B01_BCQT.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/B01BCQT/");
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
                cmd.CommandText = @"SELECT MA_CHITIEU,SAPXEP,TEN_CHITIEU,STT,CONGTHUC_WHERE,INDAM,INNGHIENG,MA_DONG FROM DM_CHITIEU_BAOCAO WHERE MA_BAOCAO = 'B01BCQT'";
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
                        INNGHIENG = int.Parse(dr["INNGHIENG"].ToString()),
                        MA_DONG = dr["MA_DONG"].ToString()
                    };
                    items.Add(dt);
                }
                dr.Close();
                cmd.Connection.Close();
                
                List<string> chi = new List<string>(new[] { "16", "17", "18", "19", "20", "21", "41", "42", "43", "44", "45", "53", "54", "55", "56" });
                List<string> dt04 = new List<string>(new[] { "31", "32", "35", "46" });
                var itemsF = items.Where(x => x.STT != null).OrderBy(y => int.Parse(y.STT)).ToList();
                List<PHA_HACHTOAN_CHI> result = new List<PHA_HACHTOAN_CHI>();
                foreach (var t in itemsF)
                {
                    if (!string.IsNullOrEmpty(t.CONGTHUC_WHERE))
                    {
                        var tbl = "PHA_TH_MLNS";
                        var gtht = "";
                        if (chi.Contains(t.STT))
                        {
                             gtht = "GIA_TRI_HACH_TOAN";

                        }
                        else if(dt04.Contains(t.STT))
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
                            if(int.Parse(para.MA_DBHC) !=  08)
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
            var starRow = 11;
            var starCol = 5;
            var STT = 2;
            var nextSTT= 0 ;
            //Ghi STT và Tên Chỉ Tiêu
            for (int i = 0; i < items.Count; i++)
            {
                ws.Cells[starRow + i, 1].Value = items[i].SAPXEP;
                ws.Cells[starRow + i, 2].Value = items[i].TEN_CHITIEU;
                ws.Cells[starRow + i, 3].Value = items[i].MA_DONG;
                ws.Cells[starRow + i, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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
                    ws.Cells[starRow + i, 4].Value = result.Where(y => y.SEGMENT12 == items[i].STT).Sum(x => x.GIA_TRI_HACH_TOAN);
                    ws.Cells[starRow + i, 4].Style.Numberformat.Format = "###,###,###,###,###";
                }
            }

            //Thêm Động loại
            var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();

            for (int j = 0; j < loaiS.Count; j++)
            {

                var khoanS = result.Where(x => x.MA_LOAI == loaiS[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
                // Thêm mã dòng loại
                ws.Cells[8, starCol, 8, starCol + khoanS.Count].Merge = true;
                ws.Cells[8, starCol, 8, starCol + khoanS.Count].Style.Font.Bold = true;
                ws.Cells[8, starCol, 8, starCol + khoanS.Count].Style.WrapText = true;
                ws.Cells[8, starCol, 8, starCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[8, starCol].Value = "LOẠI " + loaiS[j];
                ws.Cells[9, starCol].Value = "TỔNG SỐ";
                ws.Cells[9, starCol].Style.Font.Bold = true;
                ws.Cells[10, starCol].Value = STT;
                ws.Cells[10, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                    ws.Cells[9, nextCol].Value = "KHOẢN " + khoanS[k];

                    ws.Cells[9, nextCol].Style.Font.Bold = true;
                    ws.Cells[9, nextCol].Style.WrapText = true;
                    ws.Cells[9, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[10, nextCol].Value = STT + khoanS.Count;
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                        {
                            ws.Cells[starRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }


                }
                //Gán số cột bằng số cột hiện tại
                starCol += khoanS.Count + 1;
                STT += khoanS.Count + 1;
                nextSTT = STT;
            }           
            var dk = "Điều kiện lọc: ";
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
            ws.Cells[1, 1].Value =  dk;
            ws.Cells[2, 1].Value = "Đơn vị báo cáo : " + DVBAOCAO;
            ws.Cells[3, 1].Value = "Từ ngày hiệu lực : " + para.TUNGAY_HIEULUC.ToString("d") + " đến ngày hiệu lực " + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[4, 1].Value = "Từ ngày kết sổ : " + para.TUNGAY_KETSO.ToString("d") + " đến ngày kết sổ: " + para.DENNGAY_KETSO.ToString("d");

            ws.SelectedRange[8, 5, 10, starCol -1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[8, 5, 10, starCol -1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[8, 5, 10, starCol -1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[8, 5, 10, starCol -1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[11, 1, starRow + items.Count, starCol -1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, starRow + items.Count, starCol -1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, starRow + items.Count, starCol -1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, starRow + items.Count, starCol -1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        [Route("ImportXML")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportXML(XmlViewModel.InsertObj model)
        {
            var response = new Response<string>();
            try
            {
                var bc = new PHB_B01BCQT
                {
                    NAM_BC = model.ReportHeader.ReportYear,
                    MA_QHNS = model.ReportHeader.CompanyID,
                    MA_CHUONG = model.ReportHeader.BudgetChapterID.ToString(),
                    NGAY_TAO = DateTime.Now,
                    TRANG_THAI = 0,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    REFID = Guid.NewGuid().ToString()
                };

                //check đã có báo cáo chưa
                var reportCount = _b01BcqtService
                    .Queryable()
                    .Count(report => report.MA_QHNS == model.ReportHeader.CompanyID && report.NAM_BC == model.ReportHeader.ReportYear);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _b01BcqtService.Insert(bc);

                var details = model.B01BCQTDetail;
                foreach (var t in details)
                {
                    var item = new PHB_B01BCQT_DETAIL
                    {
                        PHB_B01BCQT_REFID = bc.REFID,
                        TEN_CHI_TIEU = t.ReportItemName,
                        MA_CHI_TIEU = t.ReportItemCode.ToString(),
                        STT_CHI_TIEU = t.ReportItemAlias,
                        SAP_XEP = t.ReportItemIndex,
                        MA_SO = t.ReportItemCode.ToString(),
                        GIA_TRI = t.Amount.GetValueOrDefault(0)
                    };
                    _b01BcqtDetailService.Insert(item);
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
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }

            return Ok(response);
        }

        [Route("ReceiveDataFromService")]
        [HttpPost]
        public async Task<IHttpActionResult> ReceiveDataFromService(List<B01BCQTModel> model)
        {
            var response = new Response<string>();
            if (model == null || model.Count == 0)
            {
                response.Message = "param model Task<IHttpActionResult> ReceiveDataFromService is null or empty";
                return Ok(response);
            }

            using (var context = new PHBContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rpB01_BCQT in model)
                        {
                            string msg = _b01BcqtService.IfExistsRpPeriodThenDelete(rpB01_BCQT.ReportHeader.CompanyID, rpB01_BCQT.ReportHeader.ReportPeriod, rpB01_BCQT.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _b01BcqtService.InsertData(rpB01_BCQT, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        response.Message = "";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Message = ex.Message;
                        return Ok(response);
                    }
                    finally
                    {
                        transaction.Dispose();
                        context.Dispose();
                    }
                }
            }
            return Ok(response);
        }
    }
 }

