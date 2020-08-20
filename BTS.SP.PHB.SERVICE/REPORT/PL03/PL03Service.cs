using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.SERVICE.Models.PL03;
using static BTS.SP.PHB.SERVICE.Models.PL03.PL03Vm;

namespace BTS.SP.PHB.SERVICE.REPORT.PL03
{
    public interface IPL03Service
    {
        Task<Response<PL03Vm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PL03Service : IPL03Service
    {
        public async Task<Response<PL03Vm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<PL03Vm.ViewModel> response =new Response<PL03Vm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL03";
                        command.Parameters.Clear();
                        OracleParameter p_machuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
                        p_machuong.Direction = ParameterDirection.Input;
                        p_machuong.Size = 50;
                        p_machuong.Value = machuong;

                        OracleParameter p_nambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        p_nambc.Direction = ParameterDirection.Input;
                        p_nambc.Value = nambc;

                        OracleParameter p_kybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        p_kybc.Direction = ParameterDirection.Input;
                        p_kybc.Value = kybc;

                        OracleParameter p_lstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        p_lstdvqhns.Direction = ParameterDirection.Input;
                        p_lstdvqhns.Size = 2000;
                        p_lstdvqhns.Value = lstdvqhns;

                        OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        PL03Vm.ViewModel data = new PL03Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PL03DTO> lst = new List<PL03DTO>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PL03DTO()
                                {
                                    SAPXEP = oracleDataReader["SAPXEP"].ToString(),
                                    CT = oracleDataReader["CT"].ToString(),
                                    CAP = int.Parse(oracleDataReader["CAP"].ToString()),
                                    INDAM = int.Parse(oracleDataReader["INDAM"].ToString()),
                                    INGHIENG = int.Parse(oracleDataReader["INGHIENG"].ToString()),
                                    HIENTHI = oracleDataReader["HIENTHI"].ToString(),
                                    TRANGTHAI = oracleDataReader["TRANGTHAI"].ToString(),
                                    LOAI_CHITIEU = int.Parse(oracleDataReader["LOAI_CHITIEU"].ToString()),
                                    MA_CHITIEU = oracleDataReader["MA_CHITIEU"].ToString(),
                                    MA_CHITIEU_2 = oracleDataReader["MA_CHITIEU_2"].ToString(),
                                    TEN_CHITIEU = oracleDataReader["TEN_CHITIEU"].ToString(),
                                    QT = double.Parse(oracleDataReader["QT"].ToString()),
                                    DT = double.Parse(oracleDataReader["DT"].ToString()),
                                    STT = oracleDataReader["STT"].ToString(),
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
