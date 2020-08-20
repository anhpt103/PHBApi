using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU01C;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01C;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU01C")]
    [Route("{id?}")]
    public class PhbBIEU01CController : ApiController
    {
        private readonly IDmLoaiKhoanService _LoaiKhoanservice;
        private readonly IPhbBIEU01CService _bieu01CService;
        private readonly IPhbBIEU01CTemplateService _BIEU01CTemplateService;
        private readonly IPhbBIEU01CDetailService _BIEU01CDetailService;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBIEU01CController(IPhbBIEU01CService BIEU01CService, IPhbBIEU01CTemplateService BIEU01CTemplateService,
            IPhbBIEU01CDetailService BIEU01CDetailService, IDmLoaiKhoanService LoaiKhoanservice,IDM_NGANHKTService dmNganhktService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _LoaiKhoanservice = LoaiKhoanservice;
            _bieu01CService = BIEU01CService;
            _BIEU01CTemplateService = BIEU01CTemplateService;
            _BIEU01CDetailService = BIEU01CDetailService;
            _dmNganhktService = dmNganhktService;
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
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var bieu01C = new PHB_BIEU01C()
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
                            bieu01C.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu01C.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu01C.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu01C.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu01C.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu01C.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu01CService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu01C.MA_CHUONG) && x.MA_QHNS.Equals(bieu01C.MA_QHNS) &&
                                x.NAM_BC == bieu01C.NAM_BC && x.KY_BC == bieu01C.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu01CService.Insert(bieu01C);

                            var IlstLK = _LoaiKhoanservice.GetAll();
                            int start_Row = 6;
                            int end_Row = 110;
                            int start_Col = 7;
                            int count = 1;


                            //define range specific data

                            bool isPS = false;
                            bool isLK = false;
                            int colPS_Start = 0, colPS_End = 0, colLK_Start = 0, colLK_End = 0;
                            string value = null;

                            for (int r = start_Row; r <= end_Row; r++)
                            {
                                for (int c = start_Col; c <= workSheet.Dimension.End.Column; c++)
                                {
                                    if (workSheet.Cells[r, c].Value != null)
                                    {
                                        value = workSheet.Cells[r, c].Value.ToString();
                                    }

                                    if (isPS == false && isLK == false && value.Equals("Năm nay"))
                                    {
                                        isPS = true;
                                        colPS_Start = c;
                                    }
                                    else if (isPS == true && isLK == false && value.StartsWith("Lũy kế"))
                                    {
                                        isPS = false;
                                        isLK = true;
                                        colPS_End = c - 1;
                                        colLK_Start = c;

                                    }
                                }
                            }

                            colLK_End = colLK_Start + (colPS_End - colPS_Start);

                            if (colPS_Start <= 0 || colPS_End <= 0 || colLK_Start <= 0 || colLK_End <= 0)
                            {
                                response.Error = true;
                                response.Message = "Lỗi định dạng báo cáo.";
                                return Ok(response);
                            }


                            // read data each cell
                            for (int r = start_Row + 4; r <= end_Row; r++)
                            {

                                for (int c1 = colPS_Start, c2 = colLK_Start; c1 <= colPS_End && c2 <= colLK_End; c1++, c2++)
                                {
                                    var tmpKhoan1 = workSheet.Cells[start_Row + 2, c1].Text;
                                    var tmpKhoan2 = workSheet.Cells[start_Row + 2, c2].Text;
                                    if (tmpKhoan1.StartsWith("K") && tmpKhoan2.StartsWith("K"))
                                    {
                                        if (tmpKhoan1.Equals(tmpKhoan2))
                                        {
                                            var obj = new PHB_BIEU01C_DETAIL()
                                            {
                                                PHB_BIEU01C_REFID = bieu01C.REFID,
                                                ObjectState = ObjectState.Added
                                            };
                                            obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Text;
                                            obj.MA_CHI_TIEU = count.ToString();
                                            obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;
                                            obj.MA_SO = workSheet.Cells[r, 3].Text;
                                            var Khoan = tmpKhoan1.Substring(6, tmpKhoan1.Length - 6);
                                            obj.MA_KHOAN = Khoan;
                                            obj.MA_LOAI = IlstLK.FirstOrDefault(x => x.MA.Equals(Khoan)).MA_CHA;
                                            obj.GIA_TRI_BC = workSheet.Cells[r, c1].Value != null ? decimal.Parse(workSheet.Cells[r, c1].Value.ToString()) : 0;
                                            obj.GIA_TRI_DUYET = workSheet.Cells[r, c2].Value != null ? decimal.Parse(workSheet.Cells[r, c2].Value.ToString()) : 0;
                                            _BIEU01CDetailService.Insert(obj);
                                        }

                                    }

                                }
                                count++;
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = bieu01C.REFID;
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
            var response = new Response<PHB_BIEU01CVm.DataRes>();
            try
            {               
                // lay nam BC
                var templst = _bieu01CService.GetAll();
                var ins = templst.FirstOrDefault(x => x.REFID.Equals(refid));
                var res = GetContentDetail(refid);
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
        

        private PHB_BIEU01CVm.DataRes GetContentDetail(string refid)
        {
            var result = new PHB_BIEU01CVm.DataRes();


            List<string> MaLoai = new List<string>();
            List<PHB_BIEU01CVm.LoaiKhoanItem> lstLK = new List<PHB_BIEU01CVm.LoaiKhoanItem>();
            decimal sumTotal = 0;
            var indextotal = new List<int>();
            var valuetotal = new List<decimal?>();
            int index = 0;

            var tmpls = _BIEU01CDetailService.GetAll();
            var tmplstfilter = tmpls.Where(x => x.PHB_BIEU01C_REFID.Equals(refid)).ToList();
            MaLoai = tmpls.Where(x => x.PHB_BIEU01C_REFID.Equals(refid)).Select(x => x.MA_LOAI).Distinct().OrderBy(x => x).ToList();

            foreach (var item in MaLoai)
            {
                var obj = new PHB_BIEU01CVm.LoaiKhoanItem();
                obj.LoaiItem = item;
                obj.KhoanItem = tmplstfilter.Where(x => x.MA_LOAI.Equals(item)).Select(x => x.MA_KHOAN).Distinct().ToList();
                obj.KhoanItem.Sort();
                obj.KhoanItem.Insert(0, "000");
                lstLK.Add(obj);
            }

            List<PHB_BIEU01CVm.BaoCaoDetail> lstbc = new List<PHB_BIEU01CVm.BaoCaoDetail>();

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                     connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01C_GET_DETAIL";
                        command.Parameters.Clear();

                        OracleParameter rID = command.Parameters.Add("REF_ID", OracleDbType.Varchar2);
                        rID.Size = 200;
                        rID.Direction = ParameterDirection.Input;
                        rID.Value = refid;


                        OracleParameter p_cur = command.Parameters.Add("cur", OracleDbType.RefCursor);
                        p_cur.Direction = ParameterDirection.Output;
                         command.ExecuteNonQueryAsync();

                        using (var oracleDataReader = ((OracleRefCursor)p_cur.Value).GetDataReader())
                        {

                            while (oracleDataReader.Read())
                            {
                                var obj = new PHB_BIEU01CVm.BaoCaoDetail();
                                obj.MA_SO = oracleDataReader["MA_SO"]?.ToString();
                                obj.SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"]?.ToString());
                                obj.STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString();
                                obj.TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString();

                                foreach (var l in lstLK)
                                {
                                    decimal sumKBC = 0;
                                    decimal sumKD = 0;
                                    foreach (var k in l.KhoanItem)
                                    {
                                        if (!k.Equals("000"))
                                        {

                                            var valueBC = oracleDataReader["'" + k+ "'" + "_BC"]?.ToString();
                                            var valueD = oracleDataReader["'" + k + "'"+ "_DUYET"]?.ToString();
                                            decimal tmpBC = 0;
                                            decimal tmpD = 0;
                                            decimal.TryParse(valueBC, out tmpBC);
                                            decimal.TryParse(valueD, out tmpD);
                                            var gt = new PHB_BIEU01CVm.ColObject(k, tmpBC, tmpD, tmpD - tmpBC);
                                            obj.lstCOL.Add(gt);
                                            sumKBC += tmpBC;
                                            sumKD += tmpD;

                                        }
                                        else
                                        {
                                            var gt = new PHB_BIEU01CVm.ColObject("T" + l.LoaiItem, 0, 0, 0);
                                            obj.lstCOL.Add(gt);
                                        }

                                    }
                                    int idx = obj.lstCOL.FindIndex(x => x.KHOAN.Equals("T" + l.LoaiItem));
                                    obj.lstCOL[idx].SO_BC = sumKBC;
                                    obj.lstCOL[idx].SO_DUYET = sumKD;
                                    obj.lstCOL[idx].CHENH_LECH = sumKD - sumKBC;
                                }
                                lstbc.Add(obj);
                            }

                        }

                        lstbc.ForEach(x =>
                        {
                            decimal sumBC = 0;
                            decimal sumD = 0;
                            x.lstCOL.ForEach(u =>
                            {
                                if (!u.KHOAN.StartsWith("T"))
                                {
                                    sumBC += u.SO_BC;
                                    sumD += u.SO_DUYET;
                                }

                            });
                            x.TONG_SOBC = sumBC;
                            x.TONG_SODUYET = sumD;
                            x.TONG_CHENHLECH = sumD - sumBC;
                        });

                        result.Header = lstLK;
                        result.Body = lstbc;
                        return result;
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }
        #endregion

        #region Get Edit
        [Route("GetEditByRefId")]
        [HttpPost]
        public async Task<IHttpActionResult> GetEditByRefId(PHB_BIEU01CVm.InputPara para)
        {
            var response = new Response<List<PHB_BIEU01C_DETAIL>>();
            try
            {
               
                var rawData = _BIEU01CDetailService.GetAll().Where(x=>x.PHB_BIEU01C_REFID.Equals(para.REFID)).ToList();
                    
               
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
        public async Task<IHttpActionResult> Edit(List<PHB_BIEU01C_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_BIEU01C();
            try
            {
                var refid = model.First().PHB_BIEU01C_REFID;
                report = await _bieu01CService.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();

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
                _bieu01CService.Update(report);

                var lstDetail = await _BIEU01CDetailService.Queryable().Where(detail => detail.PHB_BIEU01C_REFID == report.REFID).ToListAsync();

                //loop to edit each detail
                foreach (var detail in lstDetail)
                {
                    PHB_BIEU01C_DETAIL first = null;
                    foreach (var e in model)
                    {
                        if (e.MA_SO == detail.MA_SO && e.MA_LOAI == detail.MA_LOAI && e.MA_KHOAN == detail.MA_KHOAN)
                        {
                            first = e;
                            detail.GIA_TRI_BC = first.GIA_TRI_BC;
                            detail.GIA_TRI_DUYET = first.GIA_TRI_DUYET;
                            detail.GIA_TRI_CHENHLECH = first.GIA_TRI_CHENHLECH;
                            detail.ObjectState = ObjectState.Modified;
                            _BIEU01CDetailService.Update(detail);
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

        #region get Template
        [Route("GetTemplate")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_BIEU01C_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _BIEU01CTemplateService.Queryable().OrderBy(x => x.SAP_XEP).ToListAsync();
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

        #region Add Content

        public class InsertObject
        {
            public PHB_BIEU01C PHB_BIEU01C { get; set; }
            public List<PHB_BIEU01C_DETAIL> DETAILS { get; set; }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IHttpActionResult> Post(InsertObject model)
        {
            var response = new Response<string>();
            try
            {
                PHB_BIEU01C itemData = new PHB_BIEU01C();
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                itemData.MA_QHNS = model.PHB_BIEU01C.MA_QHNS;
                itemData.TEN_QHNS = model.PHB_BIEU01C.TEN_QHNS;
                itemData.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                itemData.NGAY_TAO = DateTime.Now;
                itemData.NAM_BC = model.PHB_BIEU01C.NAM_BC;
                itemData.KY_BC = model.PHB_BIEU01C.KY_BC;
                itemData.MA_CHUONG = model.PHB_BIEU01C.MA_CHUONG;
                itemData.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _bieu01CService.Queryable().Count(report => report.MA_QHNS == itemData.MA_QHNS && report.NAM_BC == itemData.NAM_BC);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _bieu01CService.Insert(itemData);

                foreach (var item in model.DETAILS)
                {
                    item.PHB_BIEU01C_REFID = itemData.REFID;
                    _BIEU01CDetailService.Insert(item);
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
    }
}