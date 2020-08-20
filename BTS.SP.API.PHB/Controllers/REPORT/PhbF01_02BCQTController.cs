using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_02;
using BTS.SP.PHB.ENTITY.Rp.PHB_F01_02BCQT;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT;
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
using System.Data.Entity.Validation;
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
    [RoutePrefix("api/report/phbF01_02BCQT")]
    [Route("{id?}")]
    public class PhbF01_02BCQTController : ApiController
    {
        private readonly IDmLoaiKhoanService _LoaiKhoanservice;
        private readonly IPhbF01_02BCQTService _F01_02BCQTService;
        private readonly IPhbF01_02BCQTTemplateService _F01_02BCQTTemplateService;
        private readonly IPhbF01_02BCQTDetailService _F01_02BCQTDetailService;
        private readonly ISysDvqhnsService _SysDvqhnsService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IDmChuongService _dmChuong;

        public PhbF01_02BCQTController(IPhbF01_02BCQTService F01_02BCQTService, IPhbF01_02BCQTTemplateService F01_02BCQTTemplateService,
            IPhbF01_02BCQTDetailService F01_02BCQTDetailService, IDmLoaiKhoanService LoaiKhoanservice, ISysDvqhnsService SysDvqhnsService, IUnitOfWorkAsync unitOfWorkAsync, IDmChuongService dmChuong, IDM_NGANHKTService dmNganhktService)
        {
            _LoaiKhoanservice = LoaiKhoanservice;
            _F01_02BCQTService = F01_02BCQTService;
            _F01_02BCQTTemplateService = F01_02BCQTTemplateService;
            _F01_02BCQTDetailService = F01_02BCQTDetailService;
            _SysDvqhnsService = SysDvqhnsService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
            _dmNganhktService = dmNganhktService;
        }

        #region UploadData 
        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var httpRequest = HttpContext.Current.Request;
            Response<string> response = new Response<string>();
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_F01_02BCQT F01_02BCQT = new PHB_F01_02BCQT()
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
                            F01_02BCQT.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            F01_02BCQT.MA_QHNS = httpRequest["MA_QHNS"];
                            F01_02BCQT.TEN_QHNS = httpRequest["TEN_QHNS"];
                            F01_02BCQT.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            F01_02BCQT.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            F01_02BCQT.KY_BC = int.Parse(httpRequest["KY_BC"]);
                            var checkReport = await _F01_02BCQTService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(F01_02BCQT.MA_CHUONG) && x.MA_QHNS.Equals(F01_02BCQT.MA_QHNS) &&
                                x.NAM_BC == F01_02BCQT.NAM_BC && x.KY_BC == F01_02BCQT.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _F01_02BCQTService.Insert(F01_02BCQT);


                            var IlstLK = _LoaiKhoanservice.GetAll();

                            int start_Row = 13;
                            int end_Row = 58;
                            int start_Col = 4;
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
                                            var obj = new PHB_F01_02BCQT_DETAIL() { PHB_F01_02BCQT_REFID = F01_02BCQT.REFID, ObjectState = ObjectState.Added };
                                            obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Text;
                                            obj.MA_CHI_TIEU = count.ToString();
                                            obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;
                                            obj.MA_SO = workSheet.Cells[r, 3].Text;
                                            var Khoan = tmpKhoan1.Substring(6, tmpKhoan1.Length - 6);
                                            obj.MA_KHOAN = Khoan;
                                            obj.MA_LOAI = IlstLK.FirstOrDefault(x => x.MA.Equals(Khoan)).MA_CHA;
                                            obj.GIA_TRI_PS = workSheet.Cells[r, c1].Value != null ? decimal.Parse(workSheet.Cells[r, c1].Value.ToString()) : 0;
                                            obj.GIA_TRI_LK = workSheet.Cells[r, c2].Value != null ? decimal.Parse(workSheet.Cells[r, c2].Value.ToString()) : 0;
                                            _F01_02BCQTDetailService.Insert(obj);
                                        }
                                    }

                                }
                                count++;
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = F01_02BCQT.REFID;
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
            var response = new Response<PHB_F01_02BCQTVm.DataRes>();
            try
            {
                // lay nam BC
                var templst = _F01_02BCQTService.GetAll();
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


        private PHB_F01_02BCQTVm.DataRes GetContentDetail(string refid)
        {
            var result = new PHB_F01_02BCQTVm.DataRes();


            List<string> MaLoai = new List<string>();
            List<PHB_F01_02BCQTVm.LoaiKhoanItem> lstLK = new List<PHB_F01_02BCQTVm.LoaiKhoanItem>();
            decimal sumTotal = 0;
            var indextotal = new List<int>();
            var valuetotal = new List<decimal?>();
            int index = 0;

            var tmpls = _F01_02BCQTDetailService.GetAll();
            var tmplstfilter = tmpls.Where(x => x.PHB_F01_02BCQT_REFID.Equals(refid)).ToList();
            MaLoai = tmpls.Where(x => x.PHB_F01_02BCQT_REFID.Equals(refid)).Select(x => x.MA_LOAI).Distinct().OrderBy(x => x).ToList();

            foreach (var item in MaLoai)
            {
                var obj = new PHB_F01_02BCQTVm.LoaiKhoanItem();
                obj.LoaiItem = item;
                obj.KhoanItem = tmplstfilter.Where(x => x.MA_LOAI.Equals(item)).Select(x => x.MA_KHOAN).Distinct().ToList();
                obj.KhoanItem.Sort();
                obj.KhoanItem.Insert(0, "000");
                lstLK.Add(obj);
            }

            List<PHB_F01_02BCQTVm.BaoCaoDetail> lstbc = new List<PHB_F01_02BCQTVm.BaoCaoDetail>();

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_F01_02BCQT_GET_DETAIL";
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
                                var obj = new PHB_F01_02BCQTVm.BaoCaoDetail();
                                obj.MA_SO = oracleDataReader["MA_SO"]?.ToString();
                                obj.SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"]?.ToString());
                                obj.STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString();
                                obj.TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString();

                                foreach (var l in lstLK)
                                {
                                    decimal sumKPS = 0;
                                    decimal sumKLK = 0;
                                    foreach (var k in l.KhoanItem)
                                    {
                                        if (!k.Equals("000"))
                                        {

                                            var valuePS = oracleDataReader["'" + k + "'" + "_PS"]?.ToString();
                                            var valueLK = oracleDataReader["'" + k + "'" + "_LK"]?.ToString();
                                            var gt = new PHB_F01_02BCQTVm.ColObject(k, decimal.Parse(valuePS), decimal.Parse(valueLK));
                                            obj.lstCOL.Add(gt);
                                            sumKLK += decimal.Parse(valueLK);
                                            sumKPS += decimal.Parse(valuePS);

                                        }
                                        else
                                        {
                                            var gt = new PHB_F01_02BCQTVm.ColObject("T" + l.LoaiItem, 0, 0);
                                            obj.lstCOL.Add(gt);
                                        }

                                    }
                                    int idx = obj.lstCOL.FindIndex(x => x.KHOAN.Equals("T" + l.LoaiItem));
                                    obj.lstCOL[idx].SO_PS = sumKPS;
                                    obj.lstCOL[idx].SO_LK = sumKLK;
                                }
                                lstbc.Add(obj);
                            }

                        }

                        lstbc.ForEach(x =>
                        {
                            decimal sumPS = 0;
                            decimal sumLK = 0;
                            x.lstCOL.ForEach(u =>
                            {
                                if (!u.KHOAN.StartsWith("T"))
                                {
                                    sumPS += u.SO_PS;
                                    sumLK += u.SO_LK;
                                }

                            });
                            x.TONG_SOPS = sumPS;
                            x.TONG_SOLK = sumLK;
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
        public async Task<IHttpActionResult> GetEditByRefId(PHB_F01_02BCQTVm.InputPara para)
        {
            var response = new Response<List<PHB_F01_02BCQT_DETAIL>>();
            try
            {

                var rawData = _F01_02BCQTDetailService.GetAll().Where(x => x.PHB_F01_02BCQT_REFID.Equals(para.REFID)).ToList();


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
        [Route("Update")]
        public async Task<IHttpActionResult> Update(PHB_F01_02BCQTVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var F01_02BCQT =
                    await _F01_02BCQTService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (F01_02BCQT == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete theo loại
                if (model.LstLoaiDelete != null && model.LstLoaiDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var maloai in model.LstLoaiDelete)
                    {
                        var details = await _F01_02BCQTDetailService.Queryable().Where(x => x.MA_LOAI.Equals(maloai)).ToListAsync();
                        foreach (var item in details)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _F01_02BCQTDetailService.Delete(item);
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
                        var details = await _F01_02BCQTDetailService.Queryable().Where(x => x.MA_KHOAN.Equals(makhoan)).ToListAsync();
                        foreach (var item in details)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _F01_02BCQTDetailService.Delete(item);
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
                        item.PHB_F01_02BCQT_REFID = F01_02BCQT.REFID;
                        _F01_02BCQTDetailService.Insert(item);
                    }
                }
                #endregion

                #region edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _F01_02BCQTDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            hasValue = true;
                            detail.ObjectState = ObjectState.Modified;
                            detail.GIA_TRI_PS = item.GIA_TRI_PS;
                            detail.GIA_TRI_LK = item.GIA_TRI_LK;
                            _F01_02BCQTDetailService.Update(detail);
                        }
                    }
                }
                #endregion
                if (hasValue)
                {
                    F01_02BCQT.NGAY_SUA = DateTime.Now;
                    F01_02BCQT.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _F01_02BCQTService.Update(F01_02BCQT);
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

        #region Add Content
        //[Route("AddContent")]
        //[HttpPost]
        //public async Task<IHttpActionResult> AddContent(PHB_F01_02BCQTVm.ContentData item)
        //{
        //    var response = new Response<PHB_F01_02BCQTVm.ContentData>();
        //    try
        //    {
        //        item.lstDetail.ForEach(x =>
        //        {
        //            x.MA_LOAI = item.MA_LOAI;
        //            x.MA_KHOAN = item.MA_KHOAN;
        //        });
        //        var data = item.lstDetail.ToList();

        //        foreach (var v in data)
        //        {
        //            v.ObjectState = ObjectState.Added;
        //            _F01_02BCQTDetailService.Insert(v);
        //        }



        //        if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
        //        {
        //            response.Error = false;
        //            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //        }
        //        else
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.ERROR_UPDATE_DATA;
        //        }
        //        response.Error = false;
        //        response.Data = item;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}
        #endregion

        #region Sum Report
        [Route("GetSumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSumReport(PHB_F01_02BCQTVm.SumPara para)
        {
            var response = new Response<PHB_F01_02BCQTVm.DataRes>();
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


        private PHB_F01_02BCQTVm.DataRes SumRpData(PHB_F01_02BCQTVm.SumPara para)
        {
            var result = new PHB_F01_02BCQTVm.DataRes();


            //============================================================================
            var lstLK = new List<PHB_F01_02BCQTVm.LoaiKhoanItem>();

            var MaLoai = new List<string>();
            try
            {
                var tmplk = new List<PHB_F01_02BCQT>();
                if (para.LOAI_BC.Equals("2"))
                {
                    var sysdvqhs = _SysDvqhnsService.GetAll().Where(x => x.MA_DVQHNS_CHA == para.DSDVQHNS || x.MA_DVQHNS.Equals(para.DSDVQHNS)).Select(x => x.MA_DVQHNS).ToList();
                    var tmplkbc = _F01_02BCQTService.GetAll().Where(x => x.KY_BC == para.KY_BC && x.NAM_BC == para.NAM_BC).ToList();

                    foreach (var item in sysdvqhs)
                    {
                        var obj = tmplkbc.SingleOrDefault(x => x.MA_QHNS.Equals(item));
                        if (obj != null) tmplk.Add(obj);
                    }

                }
                else if (para.LOAI_BC.Equals("3"))
                {
                    tmplk = _F01_02BCQTService.GetAll().Where(x => x.KY_BC == para.KY_BC && x.NAM_BC == para.NAM_BC && x.MA_QHNS.Equals(para.DSDVQHNS)).ToList();
                }



                var lstfilter = new List<PHB_F01_02BCQT_DETAIL>();
                tmplk.ForEach(x =>
                {
                    var tmp = _F01_02BCQTDetailService.GetAll().Where(u => u.PHB_F01_02BCQT_REFID.Equals(x.REFID)).ToList();
                    lstfilter.AddRange(tmp);
                });


                MaLoai = lstfilter.Select(x => x.MA_LOAI).Distinct().OrderBy(x => x).ToList();

                foreach (var item in MaLoai)
                {
                    var obj = new PHB_F01_02BCQTVm.LoaiKhoanItem();
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

            List<PHB_F01_02BCQTVm.BaoCaoDetail> lstbc = new List<PHB_F01_02BCQTVm.BaoCaoDetail>();

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_F01_02BCQT_SUMREPORT";
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
                                var obj = new PHB_F01_02BCQTVm.BaoCaoDetail();
                                obj.MA_SO = oracleDataReader["MA_SO"]?.ToString();
                                obj.SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"]?.ToString());
                                obj.STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString();
                                obj.TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString();
                                obj.IS_BOLD = Int32.Parse(oracleDataReader["IS_BOLD"]?.ToString());
                                obj.IS_ITALIC = Int32.Parse(oracleDataReader["IS_ITALIC"]?.ToString());

                                foreach (var l in lstLK)
                                {
                                    decimal sumKPS = 0;
                                    decimal sumKLK = 0;
                                    foreach (var k in l.KhoanItem)
                                    {
                                        if (!k.Equals("000"))
                                        {

                                            var valuePS = oracleDataReader["'" + k + "'" + "_PS"]?.ToString();
                                            var valueLK = oracleDataReader["'" + k + "'" + "_LK"]?.ToString();
                                            var gt = new PHB_F01_02BCQTVm.ColObject(k, decimal.Parse(valuePS), decimal.Parse(valueLK));
                                            obj.lstCOL.Add(gt);
                                            sumKLK += decimal.Parse(valueLK);
                                            sumKPS += decimal.Parse(valuePS);

                                        }
                                        else
                                        {
                                            var gt = new PHB_F01_02BCQTVm.ColObject("T" + l.LoaiItem, 0, 0);
                                            obj.lstCOL.Add(gt);
                                        }

                                    }
                                    int idx = obj.lstCOL.FindIndex(x => x.KHOAN.Equals("T" + l.LoaiItem));
                                    obj.lstCOL[idx].SO_PS = sumKPS;
                                    obj.lstCOL[idx].SO_LK = sumKLK;
                                }
                                lstbc.Add(obj);
                            }

                        }

                        lstbc.ForEach(x =>
                        {
                            decimal sumPS = 0;
                            decimal sumLK = 0;
                            x.lstCOL.ForEach(u =>
                            {
                                if (!u.KHOAN.StartsWith("T"))
                                {
                                    sumPS += u.SO_PS;
                                    sumLK += u.SO_LK;
                                }

                            });
                            x.TONG_SOPS = sumPS;
                            x.TONG_SOLK = sumLK;
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
            var fileNameInPut = "F01-02_PI.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/F01_02BCQT/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInPut;
            //var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/API_PHB/Template/BIEU3BP1/template3b_PI.xlsx";
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {
                List<DM_CHITIEU_BAOCAO> items = new List<DM_CHITIEU_BAOCAO>();
                OracleCommand cmd = new OracleCommand();
                OracleDataReader dr;
                cmd.CommandText = @"SELECT MA_CHITIEU,SAPXEP,TEN_CHITIEU,STT,CONGTHUC_WHERE,INDAM,INNGHIENG,MA_DONG FROM DM_CHITIEU_BAOCAO WHERE MA_BAOCAO = 'F01_02BCQT'";
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
                        MA_DONG = dr["MA_DONG"].ToString(),
                    };
                    items.Add(dt);
                }
                dr.Close();
                cmd.Connection.Close();
                List<string> dt04 = new List<string>(new[] { "13", "39" });
                List<string> chi = new List<string>(new[] { "7", "8", "19", "22", "23", "31", "32", "33", "34" });
                var itemsF = items.Where(x => x.STT != null).OrderBy(y => int.Parse(y.STT)).ToList();
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
                                cmdt.CommandText += "  (MA_DIABAN IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC IN (SELECT MA_DBHC FROM DM_DBHC Where MA_DBHC = '" + para.MA_DBHC + "' or MA_DBHC_CHA = '" + para.MA_DBHC +
                                   "') or MA_DBHC_CHA IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '" + para.MA_DBHC + "' or MA_DBHC_CHA = '" + para.MA_DBHC + "'))) AND ";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(para.MA_KBNN))
                            {
                                cmdt.CommandText += " MA_KBNN like '" + para.MA_KBNN + "' AND ";
                            }
                        }
                        if (!string.IsNullOrEmpty(para.MA_CTMTQG))
                        {
                            cmdt.CommandText += "MA_CTMTQG IN ('" + para.MA_CTMTQG + "') AND ";
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

                List<PHA_HACHTOAN_CHI> result1 = new List<PHA_HACHTOAN_CHI>();
                foreach (var t in itemsF)
                {
                    if (!string.IsNullOrEmpty(t.CONGTHUC_WHERE))
                    {

                        var tbl = "PHA_TH_MLNS";
                        var gtht = "GIA_TRI_HACH_TOAN";
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
                            cmdt.CommandText += "MA_DVQHNS IN ('" + para.MA_DVQHNS + "') AND ";
                        }
                        if (chi.Contains(t.STT))
                        {
                            if (!string.IsNullOrEmpty(para.MA_DBHC))
                            {
                                cmdt.CommandText += "  (MA_DIABAN IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC IN (SELECT MA_DBHC FROM DM_DBHC Where MA_DBHC = '" + para.MA_DBHC + "' or MA_DBHC_CHA = '" + para.MA_DBHC +
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
                        if (!string.IsNullOrEmpty(para.MA_CTMTQG))
                        {
                            cmdt.CommandText += "MA_CTMTQG IN ('" + para.MA_CTMTQG + "') AND ";
                        }


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
                            result1.Add(tmpItem);
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
                        _BindingDataToExcel1C(workSheet, result, result1, itemsF, para);
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


        public void _BindingDataToExcel1C(ExcelWorksheet ws, List<PHA_HACHTOAN_CHI> result, List<PHA_HACHTOAN_CHI> result1, List<DM_CHITIEU_BAOCAO> items, ExportParams para)
        {
            var starRow = 13;
            var starCol = 4;
            var STT = 1;
            var nextSTT = 0;
            var khoanCount = 0;
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

            }
            //Thêm Năm Nay
            //Thêm Động loại
            var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();



            for (int j = 0; j < loaiS.Count; j++)
            {
                var khoanS = result.Where(x => x.MA_LOAI == loaiS[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
                // Thêm mã dòng loại

                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Merge = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.Font.Bold = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.WrapText = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, starCol].Value = "LOẠI " + loaiS[j];
                ws.Cells[11, starCol].Merge = true;
                ws.Cells[11, starCol].Style.Font.Bold = true;
                ws.Cells[11, starCol].Style.WrapText = true;
                ws.Cells[11, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[11, starCol].Value = "TỔNG SỐ ";
                ws.Cells[12, starCol].Value = STT;
                ws.Cells[12, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                    ws.Cells[11, nextCol].Value = "KHOẢN " + khoanS[k];
                    ws.Cells[11, nextCol].Style.Font.Bold = true;
                    ws.Cells[11, nextCol].Style.WrapText = true;
                    ws.Cells[11, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[12, nextCol].Value = STT + khoanS.Count;
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
                khoanCount += khoanS.Count;
            }

            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Merge = true;
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Value = "Năm Nay";
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.Font.Bold = true;
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.WrapText = true;
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // END Năm Nay

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

            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Merge = true;
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Value = "Luỹ kế từ khi khởi đầu";
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.Font.Bold = true;
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.WrapText = true;
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //Thêm Luỹ kế
            var loaitmp1 = result1.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS1 = loaitmp.Select(x => x.MA_LOAI).ToList();

            for (int j = 0; j < loaiS1.Count; j++)
            {

                var khoanS1 = result1.Where(x => x.MA_LOAI == loaiS1[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
                // Thêm mã dòng loại

                ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Merge = true;
                ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Style.Font.Bold = true;
                ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Style.WrapText = true;
                ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, starCol].Value = "Loại" + loaiS[j];
                ws.Cells[11, starCol].Merge = true;
                ws.Cells[11, starCol].Style.Font.Bold = true;
                ws.Cells[11, starCol].Style.WrapText = true;
                ws.Cells[11, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[11, starCol].Value = "Tổng Số";
                ws.Cells[12, starCol].Value = STT;
                ws.Cells[12, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                for (int k = 0; k < khoanS1.Count; k++)
                {
                    var nextCol = starCol + k + 1;
                    ws.Cells[11, nextCol].Value = "Khoản" + khoanS1[k];
                    ws.Cells[11, nextCol].Style.Font.Bold = true;
                    ws.Cells[11, nextCol].Style.WrapText = true;
                    ws.Cells[11, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[12, nextCol].Value = STT + khoanS1.Count;
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                        {
                            ws.Cells[starRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS1[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }
                }
                //Gán số cột bằng số cột hiện tại
                starCol += khoanS1.Count + 1;
                STT += khoanS1.Count + 1;
                nextSTT = STT;
                khoanCount += khoanS1.Count;
            }


            //End Luỹ kế

            var DVBAOCAO = _dmChuong.Queryable().FirstOrDefault(x => x.MA_CHUONG == para.MA_CHUONG).TEN_CHUONG;
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Đơn vị báo cáo :" + DVBAOCAO;
            ws.Cells[3, 1].Value = "Từ ngày hiệu lực :" + para.TUNGAY_HIEULUC.ToString("d") + "đến ngày hiệu lực" + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[4, 1].Value = "Từ ngày kết sổ :" + para.TUNGAY_KETSO.ToString("d") + "đến ngày kết sổ:" + para.DENNGAY_KETSO.ToString("d");

            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHB_F01_02BCQT_TEMPLATE>> response = new Response<List<PHB_F01_02BCQT_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _F01_02BCQTTemplateService.Queryable()
                    .OrderBy(x => x.SAP_XEP)
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

        public class InsertObject_F0102P1
        {
            public PHB_F01_02BCQT PHB_F01_02BCQT { get; set; }
            public List<PHB_F01_02BCQT_DETAIL> DETAILS { get; set; }
        }

        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(InsertObject_F0102P1 instance)
        {
            var response = new Response<string>();
            try
            {
                PHB_F01_02BCQT model = new PHB_F01_02BCQT();

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                model.MA_QHNS = instance.PHB_F01_02BCQT.MA_QHNS;
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM_BC = instance.PHB_F01_02BCQT.NAM_BC;
                model.REFID = Guid.NewGuid().ToString();
                model.TEN_QHNS = instance.PHB_F01_02BCQT.TEN_QHNS;
                model.KY_BC = instance.PHB_F01_02BCQT.KY_BC;
                model.MA_CHUONG = instance.PHB_F01_02BCQT.MA_CHUONG;
                //check đã có báo cáo chưa
                var reportCount = _F01_02BCQTService
                    .Queryable()
                    .Count(report => report.MA_QHNS == model.MA_QHNS && report.NAM_BC == model.NAM_BC);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _F01_02BCQTService.Insert(model);

                foreach (var item in instance.DETAILS)
                {
                    item.PHB_F01_02BCQT_REFID = model.REFID;
                    _F01_02BCQTDetailService.Insert(item);
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

        [Route("Detail/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(string refid)
        {
            var response = new Response<List<PHB_F01_02BCQT_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _F01_02BCQTDetailService.Queryable()
                    .Where(detail => detail.PHB_F01_02BCQT_REFID == refid)
                    .ToListAsync();
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

        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(List<PHB_F01_02BCQT_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_F01_02BCQT();
            try
            {
                var refid = model.First().PHB_F01_02BCQT_REFID;
                report = await _F01_02BCQTService.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();

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
                _F01_02BCQTService.Update(report);

                var lstDetail = await _F01_02BCQTDetailService.Queryable().Where(detail => detail.PHB_F01_02BCQT_REFID == report.REFID).ToListAsync();

                //loop to edit each detail
                foreach (var detail in lstDetail)
                {
                    PHB_F01_02BCQT_DETAIL first = null;
                    foreach (var e in model)
                    {
                        if (e.MA_SO == detail.MA_SO && e.MA_LOAI == detail.MA_LOAI && e.MA_KHOAN == detail.MA_KHOAN)
                        {
                            first = e;
                            detail.GIA_TRI_LK = first.GIA_TRI_LK;
                            detail.GIA_TRI_PS = first.GIA_TRI_PS;
                            detail.ObjectState = ObjectState.Modified;
                            _F01_02BCQTDetailService.Update(detail);
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

        [Route("Delete/{refid}")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(string refid)
        {
            var response = new Response<string>();

            //get report by refid
            var report = new PHB_F01_02BCQT();
            try
            {
                report = await _F01_02BCQTService.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
                if (report == null)
                {
                    response.Message = ErrorMessage.EMPTY_DATA;
                    response.Error = true;
                    return Ok(response);
                }

                //check if report is already censored or not
                if (report.TRANG_THAI == 1)
                {
                    response.Message = "Báo cáo đã được duyệt, không thể xóa!";
                    response.Error = true;
                    return Ok(response);
                }

                //get list details by refid
                var lstDetail = new List<PHB_F01_02BCQT_DETAIL>();
                lstDetail = await _F01_02BCQTDetailService.Queryable().Where(detail => detail.PHB_F01_02BCQT_REFID == refid).ToListAsync();
                _F01_02BCQTService.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _F01_02BCQTDetailService.Delete(detail);
                }

                await _unitOfWorkAsync.SaveChangesAsync();
                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            return Ok();
        }

        [Route("ImportXML")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportXML(XmlViewModel.InsertObj model)
        {
            var response = new Response<string>();
            try
            {
                var project = model.Project.Where(x => x.IsDetail == true);
                foreach (var p in project)
                {
                    var bc = new PHB_F01_02BCQT
                    {
                        NAM_BC = model.ReportHeader.ReportYear,
                        MA_QHNS = model.ReportHeader.CompanyID,
                        NGAY_TAO = DateTime.Now,
                        TRANG_THAI = 0,
                        NGUOI_TAO = RequestContext.Principal.Identity.Name,
                        REFID = Guid.NewGuid().ToString()
                    };

                    //check đã có báo cáo chưa
                    var reportCount = _F01_02BCQTService
                        .Queryable()
                        .Count(report => report.MA_QHNS == model.ReportHeader.CompanyID && report.NAM_BC == model.ReportHeader.ReportYear);

                    if (reportCount > 0)
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.EXITS_REPORT;
                        return Ok(response);
                    }

                    _F01_02BCQTService.Insert(bc);

                    var details = model.F0102BCQTP1Detail.Where(x => x.ProjectID == p.ProjectID);
                    foreach (var t in details)
                    {
                        var itemNN = new PHB_F01_02BCQT_DETAIL
                        {
                            PHB_F01_02BCQT_REFID = bc.REFID,
                            TEN_CHI_TIEU = t.ReportItemName,
                            MA_SO = t.ReportItemCode.ToString(),
                            GIA_TRI_LK = t.AccAmount.GetValueOrDefault(0),
                            GIA_TRI_PS = t.Amount.GetValueOrDefault(0),
                            MA_KHOAN = t.BudgetSubKindItemID.Replace("K", ""),
                            MA_LOAI = t.BudgetKindItemID.Replace("L", ""),
                        };
                        _F01_02BCQTDetailService.Insert(itemNN);

                        var itemLK = new PHB_F01_02BCQT_DETAIL
                        {
                            PHB_F01_02BCQT_REFID = bc.REFID,
                            TEN_CHI_TIEU = t.ReportItemName,
                            MA_SO = t.ReportItemCode.ToString(),
                            GIA_TRI_LK = t.AccAmount.GetValueOrDefault(0),
                            GIA_TRI_PS = t.Amount.GetValueOrDefault(0),
                            MA_KHOAN = t.BudgetSubKindItemID.Replace("K", ""),
                            MA_LOAI = t.BudgetKindItemID.Replace("L", ""),
                        };
                        _F01_02BCQTDetailService.Insert(itemLK);
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

        public class BAOCAO_PARAM
        {
            public int NAM_BC { get; set; }
            public string lst_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public int KY_BC { get; set; }
            public int DonViTien { get; set; }
            public string MA_CHUONG { get; set; }
        }

        [Route("BAOCAODAURA")]

        public HttpResponseMessage BAOCAODAURA(BAOCAO_PARAM para)
        {
            HttpResponseMessage result = null;
            string file = null;

            file = _CreateExcelFileBaoCao(para);
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

        public string _CreateExcelFileBaoCao(BAOCAO_PARAM para)
        {
            var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var fileNameInPut = "F01-02_PI.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/F01_02BCQT/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInPut;
            //var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/API_PHB/Template/BIEU3BP1/template3b_PI.xlsx";
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {
                string[] refid = _F01_02BCQTService.Queryable().Where(x => x.NAM_BC == para.NAM_BC && x.KY_BC == para.KY_BC && para.lst_DVQHNS.Contains(x.MA_QHNS)).Select(y => y.REFID).ToArray();
                List<PHB_F01_02BCQT_DETAIL> result = _F01_02BCQTDetailService.Queryable().Where(x => refid.Contains(x.PHB_F01_02BCQT_REFID)).ToList();
                List<PHB_F01_02BCQT_TEMPLATE> template = _F01_02BCQTTemplateService.Queryable().ToList();
                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        _BindingDataToExcelDauRa(workSheet, result, para, template);
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

        public void _BindingDataToExcelDauRa(ExcelWorksheet ws, List<PHB_F01_02BCQT_DETAIL> result, BAOCAO_PARAM para, List<PHB_F01_02BCQT_TEMPLATE> template)
        {
            var starRow = 13;
            var starCol = 4;
            var STT = 1;
            var nextSTT = 0;
            var khoanCount = 0;
            for (int i = 0; i < template.Count; i++)
            {
                ws.Cells[starRow + i, 1].Value = template[i].STT_CHI_TIEU;
                ws.Cells[starRow + i, 2].Value = template[i].TEN_CHI_TIEU;
                ws.Cells[starRow + i, 3].Value = template[i].MA_SO;
                ws.Cells[starRow + i, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                if (template[i].IS_BOLD == 1)
                {
                    ws.Cells[starRow + i, 1].Style.Font.Bold = true;
                    ws.Cells[starRow + i, 2].Style.Font.Bold = true;
                }
            }
            //Thêm động loại
            var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();

            for (int i = 0; i < loaiS.Count; i++)
            {
                var khoanS = result.Where(x => x.MA_LOAI == loaiS[i] && x.MA_KHOAN != "TOTAL").Select(x => x.MA_KHOAN).Distinct().ToList();
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Merge = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.Font.Bold = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.WrapText = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[11, starCol].Value = "LOẠI " + loaiS[i];
                ws.Cells[11, starCol].Merge = true;
                ws.Cells[11, starCol].Style.Font.Bold = true;
                ws.Cells[11, starCol].Style.WrapText = true;
                ws.Cells[11, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[11, starCol].Value = "TỔNG SỐ ";
                //ws.Cells[12, starCol].Value = STT;
                ws.Cells[12, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Thêm Tiền Tổng Loại
                for (int j = 0; j < template.Count; j++)
                {
                    ws.Cells[starRow + j, starCol].Value = result.Where(x => x.MA_LOAI == loaiS[i] && x.MA_SO == template[j].MA_SO && x.MA_KHOAN != "TOTAL").Sum(y => y.GIA_TRI_PS);
                    //ws.Cells[starRow + j, starCol ].Value = 1;
                    ws.Cells[starRow + j, starCol].Style.Numberformat.Format = "###,###,###,###,###";
                }
                //Thêm Khoản
                for (int k = 0; k < khoanS.Count; k++)
                {
                    var nextCol = starCol + k + 1;
                    ws.Cells[11, nextCol].Value = "KHOẢN " + khoanS[k];
                    ws.Cells[11, nextCol].Style.Font.Bold = true;
                    ws.Cells[11, nextCol].Style.WrapText = true;
                    ws.Cells[11, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //ws.Cells[12, nextCol].Value = STT + khoanS.Count;

                    for (int m = 0; m < template.Count; m++)
                    {
                        if (khoanS[k] != "TOTAL")
                        {
                            ws.Cells[starRow + m, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[i] && x.MA_KHOAN == khoanS[k] && x.MA_SO == template[m].MA_SO).Sum(y => y.GIA_TRI_PS);
                            ws.Cells[starRow + m, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }
                }
                starCol += khoanS.Count + 1;
                STT += khoanS.Count + 1;
                nextSTT = STT;
                khoanCount += khoanS.Count;
            }
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Merge = true;
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Value = "Năm Nay";
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.Font.Bold = true;
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.WrapText = true;
            ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            // END Năm Nay
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Merge = true;
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Value = "Luỹ kế từ khi khởi đầu";
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.Font.Bold = true;
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.WrapText = true;
            ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //Thêm Luỹ kế
            for (int j = 0; j < loaiS.Count; j++)
            {
                var khoanS = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_KHOAN != "TOTAL").Select(y => y.MA_KHOAN).Distinct().ToList();
                //var maSo = result.Where(x => x.MA_LOAI == loaiS[j]).Select(x => x.MA_SO).Distinct().ToList();
                // Thêm mã dòng loại
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Merge = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.Font.Bold = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.WrapText = true;
                ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, starCol].Value = "Loại" + loaiS[j];
                ws.Cells[11, starCol].Merge = true;
                ws.Cells[11, starCol].Style.Font.Bold = true;
                ws.Cells[11, starCol].Style.WrapText = true;
                ws.Cells[11, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[11, starCol].Value = "Tổng Số";
                //ws.Cells[12, starCol].Value = STT;
                ws.Cells[12, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Thêm Tiền Tổng Loại
                for (int k = 0; k < template.Count; k++)
                {
                    ws.Cells[starRow + k, starCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_SO == template[k].MA_SO && x.MA_KHOAN != "TOTAL").Sum(y => y.GIA_TRI_LK);
                    ws.Cells[starRow + k, starCol].Style.Numberformat.Format = "###,###,###,###,###";
                }
                //Thêm Khoản
                for (int m = 0; m < khoanS.Count; m++)
                {
                    var nextCol = starCol + m + 1;
                    ws.Cells[11, nextCol].Value = "Khoản" + khoanS[m];
                    ws.Cells[11, nextCol].Style.Font.Bold = true;
                    ws.Cells[11, nextCol].Style.WrapText = true;
                    ws.Cells[11, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //ws.Cells[12, nextCol].Value = STT + khoanS.Count;
                    for (int n = 0; n < template.Count; n++)
                    {
                        if (khoanS[m] != "TOTAL")
                        {
                            ws.Cells[starRow + n, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_KHOAN == khoanS[m] && x.MA_SO == template[n].MA_SO).Sum(y => y.GIA_TRI_LK);
                            ws.Cells[starRow + n, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }
                }
                //Gán số cột bằng số cột hiện tại
                starCol += khoanS.Count + 1;
                STT += khoanS.Count + 1;
                nextSTT = STT;
                khoanCount += khoanS.Count;
            }
            ws.Cells[1, 1].Value = "Mã Chương: " + para.MA_CHUONG;
            ws.Cells[2, 1].Value = "Mã Đơn Vị Báo Cáo: " + para.lst_DVQHNS;
            ws.Cells[3, 1].Value = "Tên Đơn Vị Báo Cáo: " + para.TEN_DVQHNS;
            var dodai = 42;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + dodai, starCol - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + dodai, starCol - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + dodai, starCol - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[10, 1, starRow + dodai, starCol - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        [Route("ReceiveDataFromService")]
        [HttpPost]
        public async Task<IHttpActionResult> ReceiveDataFromService(List<F01_02_P2BCQTModel> model)
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
                        foreach (var rpF01_02_P2 in model)
                        {
                            string msg = _F01_02BCQTService.IfExistsRpPeriodThenDelete(rpF01_02_P2.ReportHeader.CompanyID, rpF01_02_P2.ReportHeader.ReportPeriod, rpF01_02_P2.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _F01_02BCQTService.InsertData(rpF01_02_P2, context);
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