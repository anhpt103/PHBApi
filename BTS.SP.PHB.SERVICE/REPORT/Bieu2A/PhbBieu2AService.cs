using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU2A;
using BTS.SP.PHB.SERVICE.Models.BIEU2A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu2A
{
    public interface IPhbBieu2AService : IBaseService<PHB_BIEU2A>
    {
        Task<Response<BIEU2AVm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU2AVm.DetaiRplModel>> SumAllReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu2AService:BaseService<PHB_BIEU2A>, IPhbBieu2AService {
        private readonly IRepositoryAsync<PHB_BIEU2A> _repository;

        public PhbBieu2AService(IRepositoryAsync<PHB_BIEU2A> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU2AVm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU2AVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU2A_SUMREPORT";
                        command.Parameters.Clear();


                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 50;
                        pMahuyen.Value = username;

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

                        var data = new BIEU2AVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU2AVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new BIEU2AVm.DetailModel.Item()
                                {
                                    MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                    TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU = int.Parse(oracleDataReader["MA_CHI_TIEU"].ToString()),
                                    DU_TOAN = double.Parse(oracleDataReader["DU_TOAN"].ToString()),
                                    THUC_HIEN = double.Parse(oracleDataReader["THUC_HIEN"].ToString()),
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
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }

        public async Task<Response<BIEU2AVm.DetaiRplModel>> SumAllReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU2AVm.DetaiRplModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU2A_SUMALLREPORT";
                        command.Parameters.Clear();

                        var pMahuyen = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 10;
                        pMahuyen.Value = madbhc;

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

                        var data = new BIEU2AVm.DetaiRplModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU2AVm.DetaiRplModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new BIEU2AVm.DetaiRplModel.Item()
                                {
                                    MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                    TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    DU_TOAN = double.Parse(oracleDataReader["DU_TOAN"].ToString()),
                                    THUC_HIEN = double.Parse(oracleDataReader["THUC_HIEN"].ToString()),
                                    TONG_THU = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                    TIEN_KHAUTRU = double.Parse(oracleDataReader["TIEN_KHAUTRU"].ToString()),
                                    TIEN_NSNN = double.Parse(oracleDataReader["TIEN_NSNN"].ToString())
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
