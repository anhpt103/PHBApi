using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.Models.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu70NS
{
    public interface IPhbBieu70NsService : IBaseService<PHB_BIEU70NS>
    {
        Task<Response<BIEU70NSVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu70NsService:BaseService<PHB_BIEU70NS>, IPhbBieu70NsService
    {
        private readonly IRepositoryAsync<PHB_BIEU70NS> _repository;
        public PhbBieu70NsService(IRepositoryAsync<PHB_BIEU70NS> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU70NSVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU70NSVm.ViewModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU70NS_SUMREPORT";
                        command.Parameters.Clear();
                        OracleParameter pMachuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
                        pMachuong.Direction = ParameterDirection.Input;
                        pMachuong.Size = 50;
                        pMachuong.Value = machuong;

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

                        var data = new BIEU70NSVm.ViewModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<PHB_BIEU70NS_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU70NS_DETAIL()
                                {
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_TIEN_NBC = double.Parse(oracleDataReader["SO_TIEN_NBC"].ToString()),
                                    SO_TIEN_NT = double.Parse(oracleDataReader["SO_TIEN_NT"].ToString())
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
