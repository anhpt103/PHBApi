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
using BTS.SP.PHB.ENTITY.Rp.PL3_1;
using BTS.SP.PHB.SERVICE.Models.PL31;

namespace BTS.SP.PHB.SERVICE.REPORT.PL31
{
    public interface IPhbPL31Service : IBaseService<PHB_PL31>
    {
        Task<Response<PL31Vm.DetailModel>> SumReport(string username,int loaibc,bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<PL31Vm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName);
        Task<Response<PL31Vm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbPL31Service:BaseService<PHB_PL31>, IPhbPL31Service
    {
        private readonly IRepositoryAsync<PHB_PL31> _repository;
        public PhbPL31Service(IRepositoryAsync<PHB_PL31> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PL31Vm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
           var response = new Response<PL31Vm.DetailModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL31_SUMREPORT";
                        command.Parameters.Clear();
                        var pMaDbHc = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMaDbHc.Direction = ParameterDirection.Input;
                        pMaDbHc.Size = 50;
                        pMaDbHc.Value = username;

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

                        var data = new PL31Vm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<PL31Vm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PL31Vm.DetailModel.Item()
                                {
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    DT_SOBC = double.Parse(oracleDataReader["DT_SOBC"].ToString()),
                                    DT_SODCKT = double.Parse(oracleDataReader["DT_SODCKT"].ToString()),
                                    TH_SOBC = double.Parse(oracleDataReader["TH_SOBC"].ToString()),
                                    TH_SODCKT = double.Parse(oracleDataReader["TH_SODCKT"].ToString()),
                                    MA_QHNS = oracleDataReader["MA_QHNS"]?.ToString(),
                                    TEN_QHNS = oracleDataReader["TEN_QHNS"]?.ToString(),
                                    INDAM = int.Parse(oracleDataReader["INDAM"].ToString()),
                                    INNGHIENG = int.Parse(oracleDataReader["INNGHIENG"].ToString())
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

        public async Task<Response<PL31Vm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<PL31Vm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL31_SUMREPORT_HTML";
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

                        var data = new PL31Vm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<PL31Vm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int LOAI;
                                double DT_SOBC, DT_SODCKT, TH_SOBC, TH_SODCKT;
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isDT_SOBC = double.TryParse(oracleDataReader["DT_SOBC"].ToString(), out DT_SOBC);
                                var isDT_SODCKT = double.TryParse(oracleDataReader["DT_SODCKT"].ToString(), out DT_SODCKT);
                                var isTH_SOBC = double.TryParse(oracleDataReader["TH_SOBC"].ToString(), out TH_SOBC);
                                var isTH_SODCKT = double.TryParse(oracleDataReader["TH_SODCKT"].ToString(), out TH_SODCKT);
                                lst.Add(new PL31Vm.DetailModel.Item()
                                {
                                    LOAI = isLOAI ? LOAI : 0,
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    DT_SOBC = isDT_SOBC ? DT_SOBC : 0,
                                    DT_SODCKT = isDT_SODCKT ? DT_SODCKT : 0,
                                    TH_SOBC = isTH_SOBC ? TH_SOBC : 0,
                                    TH_SODCKT = isTH_SODCKT ? TH_SODCKT : 0
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
        public async Task<Response<PL31Vm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName)
        {
            Response<PL31Vm.ViewModel> response = new Response<PL31Vm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_PL31_MERGEREPORT";
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

                        PL31Vm.ViewModel data = new PL31Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_PL31_DETAIL> lst = new List<PHB_PL31_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_PL31_DETAIL()
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
