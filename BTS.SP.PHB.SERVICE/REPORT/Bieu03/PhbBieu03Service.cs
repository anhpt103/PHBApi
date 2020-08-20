using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.SERVICE.Models.BIEU03;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu03
{
    public interface IPhbBieu03Service : IBaseService<PHB_BIEU03>
    {
        Task<Response<BIEU03Vm.DetailModel>> SumReport(string username, int loaibc,bool chitiet,int nambc, int kybc,string lstdvqhns);
    }
    public class PhbBieu03Service:BaseService<PHB_BIEU03>, IPhbBieu03Service {
        private readonly IRepositoryAsync<PHB_BIEU03> _repository;

        public PhbBieu03Service(IRepositoryAsync<PHB_BIEU03> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU03Vm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU03Vm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU03_SUMREPORT";
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

                        var data = new BIEU03Vm.DetailModel();
                        using (var reader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU03Vm.DetailModel.Item>();
                            while (reader.Read())
                            {
                                lst.Add(new BIEU03Vm.DetailModel.Item()
                                {
                                    STT_CHI_TIEU = reader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = reader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = reader["TEN_CHI_TIEU"].ToString(),
                                    LOAI = int.Parse(reader["LOAI"].ToString()),
                                    SAPXEP = int.Parse(reader["SAPXEP"].ToString()),
                                    DU_TOAN_NAM_TRUOC = double.Parse(reader["DU_TOAN_NAM_TRUOC"].ToString()),
                                    DU_TOAN_DUOC_GIAO = double.Parse(reader["DU_TOAN_DUOC_GIAO"].ToString()),
                                    DU_TOAN_DUOC_SU_DUNG = double.Parse(reader["DU_TOAN_DUOC_SU_DUNG"].ToString()),
                                    QUYET_TOAN_NAM = double.Parse(reader["QUYET_TOAN_NAM"].ToString()),
                                    INDAM = int.Parse(reader["INDAM"].ToString()),
                                    INNGHIENG =int.Parse(reader["INNGHIENG"].ToString()),
                                    MA_QHNS = reader["MA_QHNS"].ToString(),
                                    TEN_QHNS = reader["TEN_QHNS"].ToString()
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
    }
}
