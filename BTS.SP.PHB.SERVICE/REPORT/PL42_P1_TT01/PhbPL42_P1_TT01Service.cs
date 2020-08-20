using BTS.SP.PHB.ENTITY.Rp.PHB.PL42_P1_TT01;
using BTS.SP.PHB.SERVICE.Models.PL42_P1_TT01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.REPORT.PL42_P1_TT01
{
    public interface IPhbPL42_P1_TT01Service : IBaseService<PHB_PL42_P1_TT01>
    {
        Task<Response<PL42_P1_TT01Vm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<PL42_P1_TT01Vm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName);
        Task<Response<PL42_P1_TT01Vm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbPL42_P1_TT01Service : BaseService<PHB_PL42_P1_TT01>, IPhbPL42_P1_TT01Service
    {
        private readonly IRepositoryAsync<PHB_PL42_P1_TT01> _repository;
        public PhbPL42_P1_TT01Service(IRepositoryAsync<PHB_PL42_P1_TT01> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PL42_P1_TT01Vm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<PL42_P1_TT01Vm.DetailModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL42_P1_TT01_SUMREPORT";
                        command.Parameters.Clear();

                        var pMachuong = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMachuong.Direction = ParameterDirection.Input;
                        pMachuong.Size = 50;
                        pMachuong.Value = username;

                        var pLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pLoaibc.Direction = ParameterDirection.Input;
                        pLoaibc.Value = loaibc;

                        var pChiTiet = command.Parameters.Add("CHITIET", OracleDbType.Int32);
                        pChiTiet.Direction = ParameterDirection.Input;
                        pChiTiet.Value = Convert.ToInt32(chitiet);

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pLstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        pLstdvqhns.Direction = ParameterDirection.Input;
                        pLstdvqhns.Size = 2000;
                        pLstdvqhns.Value = lstdvqhns;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        var data = new PL42_P1_TT01Vm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<PL42_P1_TT01Vm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                decimal? SO_TIEN;
                                //var isSO_BAOCAO = double.TryParse(oracleDataReader["SO_BAOCAO"].ToString(), out SO_BAOCAO);
                                //var isSO_XETDUYET = double.TryParse(oracleDataReader["SO_XETDUYET"].ToString(), out SO_XETDUYET);
                                //var isCHENH_LECH = double.TryParse(oracleDataReader["CHENH_LECH"].ToString(), out CHENH_LECH);
                                if (!string.IsNullOrEmpty(oracleDataReader["SO_TIEN"].ToString()))
                                {
                                    SO_TIEN = decimal.Parse(oracleDataReader["SO_TIEN"].ToString());
                                }
                                else
                                {
                                    SO_TIEN = null;
                                }
                                lst.Add(new PL42_P1_TT01Vm.DetailModel.Item()
                                {
                                    MASO = oracleDataReader["MASO"]?.ToString(),
                                    LOAI_KHOAN = oracleDataReader["LOAI_KHOAN"]?.ToString(),
                                    DON_VI = oracleDataReader["DON_VI"].ToString(),
                                    CHITIEU = oracleDataReader["CHITIEU"].ToString(),
                                    SO_TIEN = SO_TIEN.Value
                                });
                            }
                            data.DETAIL = lst;
                        }
                        if (data.DETAIL.Count > 0)
                        {
                            response.Error = false;
                            response.Data = data;
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = ErrorMessage.EMPTY_DATA;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }

        public async Task<Response<PL42_P1_TT01Vm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<PL42_P1_TT01Vm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL41_SUMREPORT_HTML";
                        command.Parameters.Clear();

                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 50;
                        pMahuyen.Value = username;

                        var pKLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pKLoaibc.Direction = ParameterDirection.Input;
                        pKLoaibc.Value = loaibc;

                        var pChitiet = command.Parameters.Add("CHITIET", OracleDbType.Int32);
                        pChitiet.Direction = ParameterDirection.Input;
                        pChitiet.Value = Convert.ToInt32(chitiet);

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pLstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        pLstdvqhns.Direction = ParameterDirection.Input;
                        pLstdvqhns.Size = 2000;
                        pLstdvqhns.Value = lstdvqhns;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        var data = new PL42_P1_TT01Vm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<PL42_P1_TT01Vm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                decimal SO_TIEN;
                                var isSO_TIEN = decimal.TryParse(oracleDataReader["SO_TIEN"].ToString(), out SO_TIEN);
                                lst.Add(new PL42_P1_TT01Vm.DetailModel.Item()
                                {
                                    LOAI_KHOAN = oracleDataReader["LOAI_KHOAN"]?.ToString(),
                                    DON_VI = oracleDataReader["DON_VI"]?.ToString(),
                                    CHITIEU = oracleDataReader["CHITIEU"].ToString(),
                                    SO_TIEN = isSO_TIEN ? SO_TIEN : 0,
                                });
                            }
                            data.DETAIL = lst;
                        }
                        if (data.DETAIL.Count > 0)
                        {
                            response.Error = false;
                            response.Data = data;
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = ErrorMessage.EMPTY_DATA;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }
        public async Task<Response<PL42_P1_TT01Vm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName)
        {
            Response<PL42_P1_TT01Vm.ViewModel> response = new Response<PL42_P1_TT01Vm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL41_MERGEREPORT";
                        command.Parameters.Clear();
                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 50;
                        pMahuyen.Value = username;

                        var pKLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pKLoaibc.Direction = ParameterDirection.Input;
                        pKLoaibc.Value = loaibc;

                        var pChiTiet = command.Parameters.Add("CHITIET", OracleDbType.Int32);
                        pChiTiet.Direction = ParameterDirection.Input;
                        pChiTiet.Value = Convert.ToInt32(chitiet);

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pLstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        pLstdvqhns.Direction = ParameterDirection.Input;
                        pLstdvqhns.Size = 2000;
                        pLstdvqhns.Value = lstdvqhns;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;
                        await command.ExecuteNonQueryAsync();

                        PL42_P1_TT01Vm.ViewModel data = new PL42_P1_TT01Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_PL42_P1_TT01_DETAIL> lst = new List<PHB_PL42_P1_TT01_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_PL42_P1_TT01_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    CHITIEU = oracleDataReader["CHITIEU"]?.ToString() ?? ""
                                });
                            }
                            //data.DETAIL = lst;
                        }
                        response.Error = false;
                        response.Data = data;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }
    }
}
