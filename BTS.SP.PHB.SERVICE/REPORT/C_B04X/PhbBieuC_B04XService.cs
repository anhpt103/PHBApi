
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
using BTS.SP.PHB.ENTITY;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.ENTITY.Rp.C_B03B_X;
using BTS.SP.PHB.ENTITY.Rp.C_B04X;
using BTS.SP.PHB.SERVICE.Models.C_B03D_X;
using BTS.SP.PHB.SERVICE.Models.C_B04X;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B04X
{
    public interface IPhbBieuC_B04XService : IBaseService<PHB_C_B04X>
    {
        Task<Response<C_B04XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName);
        Task<Response<C_B04XVm.DetailModel>> SumReport_HTML(string madbhc, string madbhc_cha, int nambc, int kybc);
    }
    public class PhbBieuC_B04XService : BaseService<PHB_C_B04X>, IPhbBieuC_B04XService
    {
        private readonly IRepositoryAsync<PHB_C_B04X> _repository;

        public PhbBieuC_B04XService(IRepositoryAsync<PHB_C_B04X> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<C_B04XVm.DetailModel>> SumReport_HTML(string madbhc, string madbhc_cha, int nambc, int kybc)
        {
            var response = new Response<C_B04XVm.DetailModel>();

            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B04X_SUMREPORT_HTML";
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

                        var data = new C_B04XVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<C_B04XVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int SO_DAUNAM, PHATSINH_TANG,PHATSINH_GIAM, CUOINAM;
                                var isSO_DAUNAM = int.TryParse(oracleDataReader["SO_DAUNAM"].ToString(), out SO_DAUNAM);
                                var isPHATSINH_TANG = int.TryParse(oracleDataReader["PHATSINH_TANG"].ToString(), out PHATSINH_TANG);
                                var isPHATSINH_GIAM = int.TryParse(oracleDataReader["PHATSINH_GIAM"].ToString(), out PHATSINH_GIAM);
                                var isCUOINAM = int.TryParse(oracleDataReader["CUOINAM"].ToString(), out CUOINAM);
                                lst.Add(new C_B04XVm.DetailModel.Item()
                                {
                                    CHI_TIEU = oracleDataReader["CHI_TIEU"].ToString(),
                                    SO_DAUNAM = isSO_DAUNAM ? SO_DAUNAM : 0,
                                    PHATSINH_TANG = isPHATSINH_TANG ? PHATSINH_TANG : 0,
                                    PHATSINH_GIAM = isPHATSINH_GIAM ? PHATSINH_GIAM : 0,
                                    CUOINAM = isCUOINAM ? CUOINAM : 0
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
        public async Task<Response<C_B04XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName)
        {
            Response<C_B04XVm.ViewModel> response = new Response<C_B04XVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B04X_MERGEREPORT";
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

                        C_B04XVm.ViewModel data = new C_B04XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B04X_DETAIL> lst = new List<PHB_C_B04X_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_C_B04X_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    CHI_TIEU = oracleDataReader["CHI_TIEU"]?.ToString() ?? ""
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
