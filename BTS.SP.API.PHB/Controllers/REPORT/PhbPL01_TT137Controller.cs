using BTS.SP.API.ENTITY.Models.Bc.PHB.BIEU01C;
using BTS.SP.API.ENTITY.Models.Sys;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU01C;
using BTS.SP.PHB.ENTITY.Rp.PL1_TT137;
using BTS.SP.PHB.SERVICE.REPORT.B01B_TT137;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01C;
using BTS.SP.PHB.SERVICE.REPORT.PL01_TT137;
using BTS.SP.PHB.SERVICE.SERVICES;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/dm/pl1_tt137")]
    [Route("{id?}")]
    public class PL1_TT137Controller : ApiController
    {
        private readonly IPL1_TT137Service _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PL1_TT137Controller(IPL1_TT137Service service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetListPL1_137")]
        [HttpPost]
        public async Task<IHttpActionResult> GetListPL1_137(JObject jsonData)
        {

            var response = new Response<PagedObj<PL1_TT137>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<PL1_TT137Vm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PL1_TT137Vm.ViewModel>>();
            var query = new QueryBuilder
            {
                Take = paged.totalItems,
                Skip = 0
            };
            try
            {
                var filterResult = await _service.FilterAsync(filtered, query);
                if (filterResult.Value != null)
                {
                    response.Error = false;
                    response.Message = "Thành công";
                    response.Data = filterResult.Value;
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

        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(PL1_TT137 model)
        {
            Response<PL1_TT137> response = new Response<PL1_TT137>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    if (identity != null && !string.IsNullOrEmpty(identity.Name))
                    {
                        model.NguoiTao = identity.Name;
                    }
                    model.NgayTao = DateTime.Now;
                    _service.Insert(model);
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = "Thêm mới thành công!";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Lỗi khi thêm mới!";
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
            Response<PL1_TT137> response = new Response<PL1_TT137>();
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

        [Route("PHB_PL1_TT137_SUMREPORT")]
        [HttpPost]
        public async Task<IHttpActionResult> PHB_PL1_TT137_SUMREPORT(PL1_TT137ReportViewModel model)
        {
            var response = new Response<PL1_TT137ReportViewModel>();
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


        private PL1_TT137ReportViewModel LoadStoredPHB_B01A_TT137_SUMREPORT(PL1_TT137ReportViewModel model)
        {
            var obj = new PL1_TT137ReportViewModel();
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
                        command.Parameters.Add("NAM_BC", OracleDbType.Varchar2).Value = model.NAM_BC;
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
                        command.Parameters.Add("NAM_BC", OracleDbType.Varchar2).Value = model.NAM_BC;
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
                        command.Parameters.Add("NAM_BC", OracleDbType.Varchar2).Value = model.NAM_BC;
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
