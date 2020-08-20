using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP1;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.REPORT.BIEU2CP1;
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
    [RoutePrefix("api/report/phbBIEU2CP1")]
    [Route("{id?}")]
    public class PhbBieu2CP1Controller : ApiController
    {
        private readonly IDmLoaiKhoanService _LoaiKhoanservice;
        private readonly IPhbBIEU2CP1Service _BIEU2CP1Service;
        private readonly IPhbBIEU2CP1TemplateService _BIEU2CP1TemplateService;
        private readonly IPhbBIEU2CP1DetailService _BIEU2CP1DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu2CP1Controller(IPhbBIEU2CP1Service BIEU2CP1Service, IPhbBIEU2CP1TemplateService BIEU2CP1TemplateService,
            IPhbBIEU2CP1DetailService BIEU2CP1DetailService, IDmLoaiKhoanService LoaiKhoanservice, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _LoaiKhoanservice = LoaiKhoanservice;
            _BIEU2CP1Service = BIEU2CP1Service;
            _BIEU2CP1TemplateService = BIEU2CP1TemplateService;
            _BIEU2CP1DetailService = BIEU2CP1DetailService;
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
                        var bieu2Cp1 = new PHB_BIEU2CP1()
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
                            bieu2Cp1.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu2Cp1.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu2Cp1.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu2Cp1.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu2Cp1.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu2Cp1.KY_BC = int.Parse(httpRequest["KY_BC"]);
                            var checkReport = await _BIEU2CP1Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu2Cp1.MA_CHUONG) && x.MA_QHNS.Equals(bieu2Cp1.MA_QHNS) &&
                                x.NAM_BC == bieu2Cp1.NAM_BC && x.KY_BC == bieu2Cp1.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _BIEU2CP1Service.Insert(bieu2Cp1);

                            var IlstLK = _LoaiKhoanservice.GetAll();
                            int start_Row = 10;
                            int end_Row = 114;
                            int start_Col = 5;
                            int count = 1;

                            for (int r = start_Row + 3; r <= end_Row; r++)
                            {

                                for (int c = start_Col; c <= workSheet.Dimension.End.Column; c++)
                                {
                                    var tmpKhoan = workSheet.Cells[start_Row + 1, c].Text;
                                    if (tmpKhoan.StartsWith("K"))
                                    {         
                                            var obj = new PHB_BIEU2CP1_DETAIL() { PHB_BIEU2CP1_REFID = bieu2Cp1.REFID, ObjectState = ObjectState.Added };
                                            obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Text;
                                            obj.MA_CHI_TIEU = count.ToString();
                                            obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;
                                           
                                            var Khoan = tmpKhoan.Substring(6, tmpKhoan.Length - 6);
                                            obj.MA_KHOAN = Khoan;
                                            obj.MA_LOAI = IlstLK.FirstOrDefault(x => x.MA.Equals(Khoan)).MA_CHA;
                                            obj.GIA_TRI = workSheet.Cells[r, c].Value != null ? decimal.Parse(workSheet.Cells[r, c].Value.ToString()) : 0;                                            
                                            _BIEU2CP1DetailService.Insert(obj);
                                    }

                                }
                                count++;
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = bieu2Cp1.REFID;
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
            var response = new Response<PHB_BIEU2CP1Vm.DataRes>();
            try
            {
                // lay nam BC
                var templst = _BIEU2CP1Service.GetAll();
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



        private PHB_BIEU2CP1Vm.DataRes GetContentDetail(string refid)
        {
            var result = new PHB_BIEU2CP1Vm.DataRes();



            List<string> MaLoai = new List<string>();
            List<PHB_BIEU2CP1Vm.LoaiKhoanItem> lstLK = new List<PHB_BIEU2CP1Vm.LoaiKhoanItem>();
            var indextotal = new List<int>();
            var valuetotal = new List<decimal?>();

            var tmpls = _BIEU2CP1DetailService.GetAll();
            var tmplstfilter = tmpls.Where(x => x.PHB_BIEU2CP1_REFID.Equals(refid)).ToList();
            MaLoai = tmpls.Where(x => x.PHB_BIEU2CP1_REFID.Equals(refid)).Select(x => x.MA_LOAI).Distinct().OrderBy(x => x).ToList();

            foreach (var item in MaLoai)
            {
                var obj = new PHB_BIEU2CP1Vm.LoaiKhoanItem();
                obj.LoaiItem = item;
                obj.KhoanItem = tmplstfilter.Where(x => x.MA_LOAI.Equals(item)).Select(x => x.MA_KHOAN).Distinct().ToList();
                obj.KhoanItem.Sort();
                obj.KhoanItem.Insert(0, "000");
                lstLK.Add(obj);
            }

            List<PHB_BIEU2CP1Vm.BaoCaoDetail> lstbc = new List<PHB_BIEU2CP1Vm.BaoCaoDetail>();

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU2C_GET_DETAIL";
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
                                var obj = new PHB_BIEU2CP1Vm.BaoCaoDetail();
                                obj.SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"]?.ToString());
                                obj.STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString();
                                obj.TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString();

                                foreach (var l in lstLK)
                                {
                                    decimal sumK = 0;
                                    foreach (var k in l.KhoanItem)
                                    {
                                        if (!k.Equals("000"))
                                        {                                            
                                            var value = oracleDataReader["'" + k + "'"]?.ToString();
                                            var gt = new PHB_BIEU2CP1Vm.ColObject(k, decimal.Parse(value));
                                            obj.lstCOL.Add(gt);
                                            sumK += decimal.Parse(value);

                                        }
                                        else
                                        {
                                            var gt = new PHB_BIEU2CP1Vm.ColObject("T" + l.LoaiItem, 0);
                                            obj.lstCOL.Add(gt);
                                        }

                                    }
                                    int idx = obj.lstCOL.FindIndex(x => x.KHOAN.Equals("T" + l.LoaiItem));
                                    obj.lstCOL[idx].GIA_TRI = sumK;
                                    
                                }
                                lstbc.Add(obj);
                            }

                        }

                        lstbc.ForEach(x =>
                        {
                            decimal sum = 0;
                            x.lstCOL.ForEach(u =>
                            {
                                if (!u.KHOAN.StartsWith("T"))
                                {
                                    sum += u.GIA_TRI;
                                }
                            });
                            x.TONG = sum;
                        });

                        result.Header = lstLK;
                        result.Body = lstbc;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion


        #region Get Edit
        [Route("GetEditByRefId")]
        [HttpPost]
        public async Task<IHttpActionResult> GetEditByRefId(PHB_BIEU2CP1Vm.InputPara para)
        {
            var response = new Response<List<PHB_BIEU2CP1_DETAIL>>();
            try
            {
                var rawData = _BIEU2CP1DetailService.GetAll().Where(x => x.PHB_BIEU2CP1_REFID.Equals(para.REFID)).ToList();
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
        [HttpPut]
        public async Task<IHttpActionResult> Put(PHB_BIEU2CP1Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu2cp1 =
                    await _BIEU2CP1Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu2cp1 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete theo loại
                if (model.LstLoaiDelete != null && model.LstLoaiDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var maloai in model.LstLoaiDelete)
                    {
                        var details = await _BIEU2CP1DetailService.Queryable().Where(x => x.MA_LOAI.Equals(maloai)).ToListAsync();
                        foreach (var item in details)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _BIEU2CP1DetailService.Delete(item);
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
                        var details = await _BIEU2CP1DetailService.Queryable().Where(x => x.MA_KHOAN.Equals(makhoan)).ToListAsync();
                        foreach (var item in details)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _BIEU2CP1DetailService.Delete(item);
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
                        item.PHB_BIEU2CP1_REFID = bieu2cp1.REFID;
                        _BIEU2CP1DetailService.Insert(item);
                    }
                }
                #endregion

                #region edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _BIEU2CP1DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            hasValue = true;
                            detail.ObjectState = ObjectState.Modified;
                            detail.GIA_TRI = item.GIA_TRI;
                            _BIEU2CP1DetailService.Update(detail);
                        }
                    }
                }
                #endregion
                if (hasValue)
                {
                    bieu2cp1.NGAY_SUA = DateTime.Now;
                    bieu2cp1.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _BIEU2CP1Service.Update(bieu2cp1);
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

        #region Get Template
        [Route("GetTemplate")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_BIEU2CP1_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _BIEU2CP1TemplateService.Queryable().OrderBy(x => x.SAP_XEP).ToListAsync();
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
        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(PHB_BIEU2CP1Vm.ContentData item)
        {
            var response = new Response<PHB_BIEU2CP1Vm.ContentData>();
            try
            {
                item.lstDetail.ForEach(x =>
                {
                    x.MA_LOAI = item.MA_LOAI;
                    x.MA_KHOAN = item.MA_KHOAN;
                });
                var data = item.lstDetail.ToList();

                foreach (var v in data)
                {
                    v.ObjectState = ObjectState.Added;
                    _BIEU2CP1DetailService.Insert(v);
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
                response.Data = item;
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