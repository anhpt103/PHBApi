using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.C_B02AX;
using BTS.SP.PHB.SERVICE.Models.C_B02A_X;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B02aX
{
    public interface IB02aXService : IBaseService<PHB_C_B02AX>
    {
        Task<Response<C_B02A_XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName);
        Task<Response<C_B02A_XVm.DetailModel>> SumReport_HTML(string madbhc, string madbhc_cha, int nambc, int kybc);
    }
    public class B02aXService:BaseService<PHB_C_B02AX>, IB02aXService
    {
        private readonly IRepositoryAsync<PHB_C_B02AX> _repository;

        public B02aXService(IRepositoryAsync<PHB_C_B02AX> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<C_B02A_XVm.DetailModel>> SumReport_HTML(string madbhc, string madbhc_cha, int nambc, int kybc)
        {
            var response = new Response<C_B02A_XVm.DetailModel>();

            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B02A_X_SUMREPORT_HTML";
                        command.Parameters.Clear();

                        var pmadbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pmadbhc.Direction = ParameterDirection.Input;
                        pmadbhc.Size = 5;
                        pmadbhc.Value = madbhc;

                        var pmadbhc_cha = command.Parameters.Add("MADBHC_CHA", OracleDbType.NVarchar2);
                        pmadbhc_cha.Direction = ParameterDirection.Input;
                        pmadbhc_cha.Size = 5;
                        pmadbhc_cha.Value = madbhc_cha;

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        var data = new C_B02A_XVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<C_B02A_XVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int DU_TOAN, TH_TRONG_THANG, TH_LUYKE_DN, SO_SANH;                                                
                                var isDU_TOAN = int.TryParse(oracleDataReader["DUTOANNAM"].ToString(), out DU_TOAN);
                                var isTH_TRONG_THANG = int.TryParse(oracleDataReader["TRONGTHANG"].ToString(), out TH_TRONG_THANG);
                                var isTH_LUYKE_DN = int.TryParse(oracleDataReader["LUYKE"].ToString(), out TH_LUYKE_DN);
                                var isSO_SANH = int.TryParse(oracleDataReader["SOSANH"].ToString(), out SO_SANH);
                                lst.Add(new C_B02A_XVm.DetailModel.Item()
                                {
                                    NOI_DUNG = oracleDataReader["NOI_DUNG"].ToString(),
                                    DU_TOAN = isDU_TOAN ? DU_TOAN : 0,
                                    TH_TRONG_THANG = isTH_TRONG_THANG ? TH_TRONG_THANG : 0,
                                    TH_LUYKE_DN = isTH_LUYKE_DN ? TH_LUYKE_DN : 0,
                                    SO_SANH = isSO_SANH ? SO_SANH : 0
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
        public async Task<Response<C_B02A_XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName)
        {
            Response<C_B02A_XVm.ViewModel> response = new Response<C_B02A_XVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B02A_X_MERGEREPORT";
                        command.Parameters.Clear();
                        var pmadbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pmadbhc.Direction = ParameterDirection.Input;
                        pmadbhc.Size = 5;
                        pmadbhc.Value = madbhc;

                        var pmadbhc_cha = command.Parameters.Add("MADBHC_CHA", OracleDbType.NVarchar2);
                        pmadbhc_cha.Direction = ParameterDirection.Input;
                        pmadbhc_cha.Size = 5;
                        pmadbhc_cha.Value = madbhc_cha;

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        C_B02A_XVm.ViewModel data = new C_B02A_XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B02AX_DETAIL> lst = new List<PHB_C_B02AX_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_C_B02AX_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    NOI_DUNG = oracleDataReader["NOI_DUNG"]?.ToString() ?? ""
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
