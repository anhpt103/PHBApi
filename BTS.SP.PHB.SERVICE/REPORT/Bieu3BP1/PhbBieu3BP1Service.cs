using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP1;
using BTS.SP.PHB.SERVICE.Models.BIEU3BP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3BP1
{
    public interface IPhbBieu3BP1Service : IBaseService<PHB_BIEU3BP1>
    {
        Task<Response<BIEU3BP1Vm.DetailModel>> SumReport(string madbhc,int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu3BP1Service:BaseService<PHB_BIEU3BP1>, IPhbBieu3BP1Service
    {
        private readonly IRepositoryAsync<PHB_BIEU3BP1> _repository;
        public PhbBieu3BP1Service(IRepositoryAsync<PHB_BIEU3BP1> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU3BP1Vm.DetailModel>> SumReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response =new Response<BIEU3BP1Vm.DetailModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU3BP1_SUMREPORT";
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
                        var data =new BIEU3BP1Vm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU3BP1Vm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new BIEU3BP1Vm.DetailModel.Item()
                                {
                                    PHAN = int.Parse(oracleDataReader["PHAN"].ToString()),
                                    MA_LOAI = oracleDataReader["MA_LOAI"].ToString(),
                                    MA_KHOAN = oracleDataReader["MA_KHOAN"].ToString(),
                                    MA_MUC = oracleDataReader["MA_MUC"]?.ToString(),
                                    MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    CAP = int.Parse(oracleDataReader["CAP"].ToString()),
                                    INDAM = int.Parse(oracleDataReader["INDAM"].ToString()),
                                    INNGHIENG = int.Parse(oracleDataReader["INNGHIENG"].ToString()),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_BC = double.Parse(oracleDataReader["SO_BC"].ToString()),
                                    SO_XDTD = double.Parse(oracleDataReader["SO_XDTD"].ToString()),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                    TEN_QHNS = oracleDataReader["TEN_QHNS"]?.ToString()
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
