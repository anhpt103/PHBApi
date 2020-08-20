using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU01C;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU01C
{
    public interface IPhbBIEU01CService : IBaseService<PHB_BIEU01C>
    {
        Task<Response<PHB_BIEU01CVm.ViewModel2>> SumReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBIEU01CService:BaseService<PHB_BIEU01C>, IPhbBIEU01CService {
        private readonly IRepositoryAsync<PHB_BIEU01C> _repository;

        public PhbBIEU01CService(IRepositoryAsync<PHB_BIEU01C> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PHB_BIEU01CVm.ViewModel2>> SumReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            Response<PHB_BIEU01CVm.ViewModel2> response = new Response<PHB_BIEU01CVm.ViewModel2>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01C_SUMALLREPORT";
                        command.Parameters.Clear();
                        var mMaDbhc = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        mMaDbhc.Direction = ParameterDirection.Input;
                        mMaDbhc.Size = 50;
                        mMaDbhc.Value = madbhc;

                        var pLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pLoaibc.Direction = ParameterDirection.Input;
                        pLoaibc.Value = loaibc;

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

                        PHB_BIEU01CVm.ViewModel2 data = new PHB_BIEU01CVm.ViewModel2();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU01CVm.DetailModel2> lst = new List<PHB_BIEU01CVm.DetailModel2>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU01CVm.DetailModel2()
                                {
                                    MA_LOAI = oracleDataReader["MA_LOAI"]?.ToString(),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    MA_SO = oracleDataReader["MA_SO"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"].ToString()),
                                    GIA_TRI_BC = Decimal.Parse(oracleDataReader["GIA_TRI_BC"].ToString()),
                                    GIA_TRI_DUYET = Decimal.Parse(oracleDataReader["GIA_TRI_DUYET"].ToString()),
                                });
                            }
                            data.DETAILS = lst;
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
