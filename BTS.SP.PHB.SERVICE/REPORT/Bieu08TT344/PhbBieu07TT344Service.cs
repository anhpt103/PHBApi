using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU08TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU_08TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using BTS.SP.PHB.ENTITY;
using Oracle.ManagedDataAccess.Types;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu08TT344
{
    public interface IPhbBieu08TT344Service : IBaseService<PHB_BIEU08TT344>
    {
        Task<Response<BIEU08TT344Vm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU08TT344Vm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName);
    }

    public class PhbBieu08TT344Service : BaseService<PHB_BIEU08TT344>, IPhbBieu08TT344Service
    {
        private readonly IRepositoryAsync<PHB_BIEU08TT344> _repository;

        public PhbBieu08TT344Service(IRepositoryAsync<PHB_BIEU08TT344> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU08TT344Vm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            Response<BIEU08TT344Vm.ViewModel> response = new Response<BIEU08TT344Vm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU08TT344_SUMREPORT";
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

                        BIEU08TT344Vm.ViewModel data = new BIEU08TT344Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU08TT344_DETAIL> lst = new List<PHB_BIEU08TT344_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU08TT344_DETAIL()
                                {
                                    PHAN = int.Parse(oracleDataReader["PHAN"].ToString()),
                                    NOIDUNG = oracleDataReader["NOIDUNG"]?.ToString() ?? "",
                                    DUTOAN_NSNN = double.Parse(oracleDataReader["DUTOAN_NSNN"].ToString()),
                                    DUTOAN_NSX = double.Parse(oracleDataReader["DUTOAN_NSX"].ToString()),
                                    QUYETTOAN_NSNN = double.Parse(oracleDataReader["QUYETTOAN_NSNN"].ToString()),
                                    QUYETTOAN_NSX = double.Parse(oracleDataReader["QUYETTOAN_NSX"].ToString()),
                                    SOSANH_NSNN = double.Parse(oracleDataReader["SOSANH_NSNN"].ToString()),
                                    SOSANH_NSX = double.Parse(oracleDataReader["SOSANH_NSX"].ToString())
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

        public async Task<Response<BIEU08TT344Vm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName)
        {
            Response<BIEU08TT344Vm.ViewModel> response = new Response<BIEU08TT344Vm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU08TT344_MERGEREPORT";
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

                        BIEU08TT344Vm.ViewModel data = new BIEU08TT344Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU08TT344_DETAIL> lst = new List<PHB_BIEU08TT344_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU08TT344_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    NOIDUNG = oracleDataReader["NOIDUNG"]?.ToString() ?? ""
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
