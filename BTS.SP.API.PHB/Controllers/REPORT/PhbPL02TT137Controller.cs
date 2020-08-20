using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.PL02_TT137;
using BTS.SP.PHB.SERVICE.REPORT.PL02_TT137;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/PhuLuc/pl02_TT137")]
    [Route("{id?}")]
    public class PhbPL02TT137Controller : ApiController
    {
        private readonly IPHB_PL02_TT137Service _service;
        private readonly IPHB_PL02_TT137DetailService _serviceDetail;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISysDvqhnsService _sysDVQHNSService;

        public PhbPL02TT137Controller(IPHB_PL02_TT137Service service, IPHB_PL02_TT137DetailService serviceDetail, ISysDvqhnsService sysDvqhnsService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _serviceDetail = serviceDetail;
            _unitOfWorkAsync = unitOfWorkAsync;
            _sysDVQHNSService = sysDvqhnsService;
        }

        #region thêm mới
        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(PL02_TT137ViewModel.AddModel model)
        {
            var response = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == model.data.MA_QHNS);
            var data = new PHB_PL02_TT137
            {
                REFID = Guid.NewGuid().ToString("n"),
                MA_CHUONG = model.data.MA_CHUONG,
                MA_QHNS = model.data.MA_QHNS,
                TEN_QHNS = model.data.TEN_QHNS,
                MA_QHNS_CHA = tempDVSDNS.MA_DVQHNS_CHA,
                NAM_BC = model.data.NAM_BC,
                KY_BC = model.data.KY_BC,
                TRANG_THAI = 0,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = identity.Name,
                NGUOI_SUA = null,
                NGAY_SUA = null
            };
            _service.Insert(data);

            var checkExits = _service.Queryable().Where(x => x.MA_QHNS == data.MA_QHNS && x.NAM_BC == data.NAM_BC && x.KY_BC == data.KY_BC).Count();
            if (checkExits > 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EXITS_REPORT;
                return Ok(response);
            }

            var dataDetail = new PHB_PL02_TT137_DETAIL
            {
                PHB_PL02_TT137_REFID = data.REFID,
                CANBO_I = model.dataDetail.CANBO_I,
                CHUCVU_I = model.dataDetail.CHUCVU_I,
                CANBO_II = model.dataDetail.CANBO_II,
                CHUCVU_II = model.dataDetail.CHUCVU_II,
                QT_NGANSACH_NAM = model.dataDetail.QT_NGANSACH_NAM,
                QT_KHONGVON = model.dataDetail.QT_KHONGVON,
                DT_GIAO_DAUNAM = model.dataDetail.DT_GIAO_DAUNAM,
                DT_BOSUNG_TRONGNAM = model.dataDetail.DT_BOSUNG_TRONGNAM,
                TSKP_PHAINOP_NSNN = model.dataDetail.TSKP_PHAINOP_NSNN,
                TSKP_DANOP_NSNN = model.dataDetail.TSKP_DANOP_NSNN,
                TSKP_CONPHAINOP_NSNN = model.dataDetail.TSKP_CONPHAINOP_NSNN,
                THUYET_MINH = model.dataDetail.THUYET_MINH,
                NHAN_XET = model.dataDetail.NHAN_XET,
                KIEN_NGHI = model.dataDetail.KIEN_NGHI,
                DONVI_CHA = model.dataDetail.DONVI_CHA,
                DIA_CHI = model.dataDetail.DIA_CHI,
            };
            _serviceDetail.Insert(dataDetail);

            try
            {
                _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }
        #endregion

        #region edit
        [Route("Update")]
        [HttpPost]
        public async Task<IHttpActionResult> Update(PL02_TT137ViewModel.EditModel model)
        {
            var response = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            var data = _service.Queryable().FirstOrDefault(x => x.REFID == model.data.REFID);
            var dataDetail = _serviceDetail.Queryable().FirstOrDefault(x => x.PHB_PL02_TT137_REFID == data.REFID);
            if (data != null)
            {
                data.NGUOI_SUA = identity.Name;
                data.NGAY_SUA = DateTime.Now;
                data.ObjectState = ObjectState.Modified;
                _service.Update(data);
            }
            if (dataDetail != null)
            {
                if (string.IsNullOrEmpty(model.MA_BAO_CAO) || string.IsNullOrEmpty(model.MA_BAO_CAO)) return BadRequest();
                model.MA_BAO_CAO = model.MA_BAO_CAO.ToUpper();
                model.dataDetail.PHB_PL02_TT137_REFID = model.data.REFID;
                try
                {
                    using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                    {

                        connection.Open();
                        var command = connection.CreateCommand();
                        OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        try
                        {
                            if (model.MA_BAO_CAO != null)
                            {
                                command.CommandText = "DELETE FROM PHB_" + model.MA_BAO_CAO + "_DETAIL WHERE PHB_" + model.MA_BAO_CAO + "_REFID='" + model.dataDetail.PHB_PL02_TT137_REFID + "'";
                                command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            WriteLogs.LogError(ex);
                        }
                    }
                }
                catch (Exception e)
                {
                    WriteLogs.LogError(e);
                    return InternalServerError();
                }
            }
            if (model.dataDetail != null)
            {
                dataDetail.PHB_PL02_TT137_REFID = data.REFID;
                dataDetail.CANBO_I = model.dataDetail.CANBO_I;
                dataDetail.CHUCVU_I = model.dataDetail.CHUCVU_I;
                dataDetail.CANBO_II = model.dataDetail.CANBO_II;
                dataDetail.CHUCVU_II = model.dataDetail.CHUCVU_II;
                dataDetail.QT_NGANSACH_NAM = model.dataDetail.QT_NGANSACH_NAM;
                dataDetail.QT_KHONGVON = model.dataDetail.QT_KHONGVON;
                dataDetail.DT_GIAO_DAUNAM = model.dataDetail.DT_GIAO_DAUNAM;
                dataDetail.DT_BOSUNG_TRONGNAM = model.dataDetail.DT_BOSUNG_TRONGNAM;
                dataDetail.TSKP_PHAINOP_NSNN = model.dataDetail.TSKP_PHAINOP_NSNN;
                dataDetail.TSKP_DANOP_NSNN = model.dataDetail.TSKP_DANOP_NSNN;
                dataDetail.TSKP_CONPHAINOP_NSNN = model.dataDetail.TSKP_CONPHAINOP_NSNN;
                dataDetail.THUYET_MINH = model.dataDetail.THUYET_MINH;
                dataDetail.NHAN_XET = model.dataDetail.NHAN_XET;
                dataDetail.KIEN_NGHI = model.dataDetail.KIEN_NGHI;
                dataDetail.DONVI_CHA = model.dataDetail.DONVI_CHA;
                dataDetail.DIA_CHI = model.dataDetail.DIA_CHI;
                dataDetail.ObjectState = ObjectState.Added;
                _serviceDetail.Insert(dataDetail);
            }

            try
            {
                var kq = _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            return Ok(response);
        }
        #endregion

        [Route("GetbyREFID")]
        [HttpGet]
        public async Task<IHttpActionResult> GetbyREFID(int id)
        {
            Response<PHB_PL02_TT137_DETAIL> response = new Response<PHB_PL02_TT137_DETAIL>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var obj = _service.FindById(id);
                    var objDetail = _serviceDetail.Queryable().FirstOrDefault(x => x.PHB_PL02_TT137_REFID == obj.REFID);

                    if (obj != null)
                    {
                        response.Error = false;
                        response.Message = "Có dữ liệu!";
                        response.Data = objDetail;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Không có dữ liệu!";
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ex.InnerException.Message;
                }
                return Ok(response);
            }
        }

        [Route("GetbyId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetbyId(int id)
        {
            Response<PHB_PL02_TT137> response = new Response<PHB_PL02_TT137>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var obj = _service.FindById(id);

                    if (obj != null)
                    {
                        response.Error = false;
                        response.Message = "Có dữ liệu!";
                        response.Data = obj;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Không có dữ liệu!";
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ex.InnerException.Message;
                }
                return Ok(response);
            }
        }

        [Route("PHB_PL02_TT137_SUMREPORT")]
        [HttpPost]
        public async Task<IHttpActionResult> PHB_PL02_TT137_SUMREPORT(PL02_TT137ReportViewModel model)
        {
            var response = new Response<PL02_TT137ReportViewModel>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var obj = LoadStoredPHB_B01A_TT137_SUMREPORT(model);
                try
                {
                    if (obj != null)
                    {
                        response.Error = false;
                        response.Message = "Có dữ liệu!";
                        response.Data = obj;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Không có dữ liệu!";
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ex.InnerException.Message;
                }
                return Ok(response);
            }
        }


        private PL02_TT137ReportViewModel LoadStoredPHB_B01A_TT137_SUMREPORT(PL02_TT137ReportViewModel model)
        {
            var obj = new PL02_TT137ReportViewModel();
            try
            {
                using (OracleConnection connect = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    connect.OpenAsync();
                    using (var command = connect.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B01A_TT137_SUMREPORT";
                        command.Parameters.Clear();
                        command.Parameters.Add("LST_DVQHNS", OracleDbType.Clob).Value = model.LST_DVQHNS;
                        command.Parameters.Add("DONVI_TIEN", OracleDbType.Varchar2).Value = model.DONVI_TIEN;
                        command.Parameters.Add("NAM_BC", OracleDbType.Varchar2).Value = "2018";
                        command.Parameters.Add("KY_BC", OracleDbType.Varchar2).Value = model.KY_BC;
                        command.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = model.USERNAME;
                        command.Parameters.Add("CUR1", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        command.ExecuteNonQueryAsync();
                    };
                    using (var command = connect.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL1_TT137";
                        command.Parameters.Clear();
                        command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        command.ExecuteNonQueryAsync();
                        using (var reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader())
                        {
                            while (reader.Read())
                            {
                                obj.TongSoThu = reader["TongSoThu"].ToString() == "" ? 0 : int.Parse(reader["TongSoThu"].ToString());
                                obj.TongSoTienPhaiNop = reader["TongSoTienPhaiNop"].ToString() == "" ? 0 : int.Parse(reader["TongSoTienPhaiNop"].ToString());
                                obj.TongSoDuocKhauTru = reader["TongSoDuocKhauTru"].ToString() == "" ? 0 : int.Parse(reader["TongSoDuocKhauTru"].ToString());
                            }

                        }
                    };
                    using (var command = connect.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL1_TT137_AND_1C";
                        command.Parameters.Clear();
                        command.Parameters.Add("DSDVQHNS", OracleDbType.Varchar2).Value = model.DSDVQHNS;
                        command.Parameters.Add("NAM_BC", OracleDbType.Varchar2).Value = "2018";
                        command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        command.ExecuteNonQueryAsync();
                        using (var reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader())
                        {
                            //int Ma01, Ma36, Ma44, Ma61, Ma79, Ma08, Ma37, Ma47, Ma64, Ma82, Ma14, Ma17, Ma42, Ma52, Ma73, Ma91, Ma20, Ma53, Ma29, Ma43, Ma57, Ma76, Ma94, Ma31, Ma34, Ma35, Ma59;
                            List<fillMaSo> listMa = new List<fillMaSo>();
                            List<string> MaSoDuKinhPhiNamTruoc = new List<string>() { "1", "36", "44", "61", "79" };
                            List<string> MaDuToanDuocGiaoTrongNam = new List<string>() { "08", "37", "47", "64", "82" };
                            List<string> MaKinhPhiQuyetToan = new List<string>() { "17", "42", "52", "73", "91" };
                            List<string> MaSoDuKinhPhiNamSau = new List<string>() { "29", "43", "57", "73", "94" };
                            while (reader.Read())
                            {
                                string Maso = reader["MA_SO"].ToString();
                                if (Maso != null && Maso != "")
                                {
                                    listMa.Add(new fillMaSo()
                                    {
                                        MaSo = Maso,
                                        Value = reader["GIA_TRI_BC"].ToString() == "" ? 0 : int.Parse(reader["GIA_TRI_BC"].ToString())
                                    });
                                }
                            }
                            foreach (var item in listMa)
                            {
                                if (MaSoDuKinhPhiNamTruoc.Contains(item.MaSo))
                                {
                                    obj.SoDuKinhPhiNamTruoc += item.Value;
                                }
                                else if (MaDuToanDuocGiaoTrongNam.Contains(item.MaSo))
                                {
                                    obj.DuToanDuocGiaoTrongNam += item.Value;
                                }
                                else if (item.MaSo == "14")
                                {
                                    obj.KinhPhiThucNhanTrongNam = item.Value;
                                }
                                else if (MaKinhPhiQuyetToan.Contains(item.MaSo))
                                {
                                    obj.KinhPhiQuyetToan += item.Value;
                                }
                                else if (item.MaSo == "20" || item.MaSo == "53")
                                {
                                    obj.KinhPhiGiamTrongNam += item.Value;
                                }
                                else if (MaSoDuKinhPhiNamSau.Contains(item.MaSo))
                                {
                                    obj.SoDuKinhPhiNamSau += item.Value;
                                }
                                else if (item.MaSo == "31" || item.MaSo == "34")
                                {
                                    obj.KinhPhiDaNhan += item.Value;
                                }
                                else if (item.MaSo == "35" || item.MaSo == "59")
                                {
                                    obj.DuToanConDu += item.Value;
                                }
                            }
                        }
                    };
                    using (var command = connect.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL1_TT137_AND_1B";
                        command.Parameters.Clear();
                        command.Parameters.Add("DSDVQHNS", OracleDbType.Varchar2).Value = model.DSDVQHNS;
                        command.Parameters.Add("NAM_BC", OracleDbType.Varchar2).Value = "2018";
                        command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        command.ExecuteNonQueryAsync();
                        using (var reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader())
                        {
                            while (reader.Read())
                            {
                                obj.KetQuaChenhLech = reader["KetQuaChenhLech"].ToString() == "" ? 0 : int.Parse(reader["KetQuaChenhLech"].ToString());
                                obj.KinhPhiTietKiem = reader["KinhPhiTietKiem"].ToString() == "" ? 0 : int.Parse(reader["KinhPhiTietKiem"].ToString());
                                obj.TrichLapCacQuy = reader["TrichLapCacQuy"].ToString() == "" ? 0 : int.Parse(reader["TrichLapCacQuy"].ToString());
                                obj.KinhPhiCaiCachTienLuong = reader["KinhPhiCaiCachTienLuong"].ToString() == "" ? 0 : int.Parse(reader["KinhPhiCaiCachTienLuong"].ToString());
                            }

                        }
                    };
                    connect.Close();
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
            }
            return obj;
        }

        private string getParam(ReportInput paramJson, string input)
        {
            for (var i = 0; i < paramJson.param.Count; i++)
            {
                if (paramJson.param[i].name.Equals(input))
                {
                    return paramJson.param[i].value;
                }
            }
            return "";
        }

        private class fillMaSo
        {
            public string MaSo { get; set; }
            public int Value { get; set; }
        }
    }
}
