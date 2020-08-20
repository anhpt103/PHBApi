using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU68NS;
using BTS.SP.PHB.SERVICE.Models.BIEU68NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu68NS
{
    public interface IPhbBieu68NsService : IBaseService<PHB_BIEU68NS>
    {
        Task<Response<BIEU68NSVm.DetailModel>> SumReport(string madbhc,string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu68NsService:BaseService<PHB_BIEU68NS>, IPhbBieu68NsService
    {
        private readonly IRepositoryAsync<PHB_BIEU68NS> _repository;
        public PhbBieu68NsService(IRepositoryAsync<PHB_BIEU68NS> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU68NSVm.DetailModel>> SumReport(string madbhc,string machuong, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU68NSVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU68NS_SUMREPORT";
                        command.Parameters.Clear();
                        OracleParameter pMachuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
                        pMachuong.Direction = ParameterDirection.Input;
                        pMachuong.Size = 50;
                        pMachuong.Value = machuong;

                        OracleParameter pMadbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pMadbhc.Direction = ParameterDirection.Input;
                        pMadbhc.Size = 50;
                        pMadbhc.Value = madbhc;

                        OracleParameter pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        OracleParameter pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        OracleParameter pLstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        pLstdvqhns.Direction = ParameterDirection.Input;
                        pLstdvqhns.Size = 2000;
                        pLstdvqhns.Value = lstdvqhns;

                        OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        var data = new BIEU68NSVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor) pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU68NSVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new BIEU68NSVm.DetailModel.Item()
                                {
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    TONG_SO = double.Parse(oracleDataReader["TONG_SO"].ToString()),
                                    TANG_THU = double.Parse(oracleDataReader["TANG_THU"].ToString()),
                                    DU_PHONG = double.Parse(oracleDataReader["DU_PHONG"].ToString()),
                                    THUONG_VUOT_DTTHU = double.Parse(oracleDataReader["THUONG_VUOT_DTTHU"].ToString()),
                                    INDAM = int.Parse(oracleDataReader["INDAM"].ToString()),
                                    INNGHIENG = int.Parse(oracleDataReader["INNGHIENG"].ToString()),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString())
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
