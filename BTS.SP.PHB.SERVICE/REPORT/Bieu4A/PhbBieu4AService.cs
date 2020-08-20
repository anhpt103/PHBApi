using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.SERVICE.Models.BIEU4A;
using BTS.SP.PHB.SERVICE.Models.BIEU4A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4A
{
    public interface IPhbBieu4AService : IBaseService<PHB_BIEU4A>
    {
        Task<Response<BIEU4AVm.DetailModel>> SumReport(string username,int loaibc,bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU4AVm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName);
        Task<Response<BIEU4AVm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu4AService:BaseService<PHB_BIEU4A>, IPhbBieu4AService
    {
        private readonly IRepositoryAsync<PHB_BIEU4A> _repository;
        public PhbBieu4AService(IRepositoryAsync<PHB_BIEU4A> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU4AVm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU4AVm.DetailModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU4A_SUMREPORT";
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

                        var data = new BIEU4AVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU4AVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new BIEU4AVm.DetailModel.Item()
                                {
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_DUTOAN = double.Parse(oracleDataReader["SO_DUTOAN"].ToString()),
                                    SO_THUCHIEN = double.Parse(oracleDataReader["SO_THUCHIEN"].ToString()),
                                    MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                    TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString()
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
                response.Message= ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }

        public async Task<Response<BIEU4AVm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU4AVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU4A_SUMREPORT_HTML";
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

                        var data = new BIEU4AVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU4AVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int LOAI;
                                double SO_DUTOAN, SO_THUCHIEN;
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isSO_DUTOAN = double.TryParse(oracleDataReader["SO_DUTOAN"].ToString(), out SO_DUTOAN);
                                var isSO_THUCHIEN = double.TryParse(oracleDataReader["SO_THUCHIEN"].ToString(), out SO_THUCHIEN);
                                lst.Add(new BIEU4AVm.DetailModel.Item()
                                {
                                    LOAI = isLOAI ? LOAI : 0,
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_DUTOAN = isSO_DUTOAN ? SO_DUTOAN : 0,
                                    SO_THUCHIEN = isSO_THUCHIEN ? SO_THUCHIEN : 0
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
        public async Task<Response<BIEU4AVm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName)
        {
            Response<BIEU4AVm.ViewModel> response = new Response<BIEU4AVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU4A_MERGEREPORT";
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

                        BIEU4AVm.ViewModel data = new BIEU4AVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU4A_DETAIL> lst = new List<PHB_BIEU4A_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU4A_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString() ?? ""
                                });
                            }
                            data.DETAIL = lst;
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
