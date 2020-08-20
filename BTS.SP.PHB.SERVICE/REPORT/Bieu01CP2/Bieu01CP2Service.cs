using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU01CP2;
using BTS.SP.PHB.SERVICE.Models.BIEU01CP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU01CP2
{
    public interface IBIEU01CP2Service:IBaseService<PHB_BIEU01CP2>
    {
        Task<Response<BIEU01CP2Vm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class BIEU01CP2Service:BaseService<PHB_BIEU01CP2>, IBIEU01CP2Service
    {
        private readonly IRepositoryAsync<PHB_BIEU01CP2> _repository;
        public BIEU01CP2Service(IRepositoryAsync<PHB_BIEU01CP2> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU01CP2Vm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            Response<BIEU01CP2Vm.ViewModel> response =new Response<BIEU01CP2Vm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01CP2_SUMREPORT";
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

                        BIEU01CP2Vm.ViewModel data = new BIEU01CP2Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU01CP2_DETAIL> lst = new List<PHB_BIEU01CP2_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU01CP2_DETAIL()
                                {
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    NOI_DUNG_CHI = oracleDataReader["NOI_DUNG_CHI"]?.ToString() ?? "",
                                    MA_LOAI = oracleDataReader["MA_LOAI"]?.ToString() ?? "",
                                    MA_KHOAN = oracleDataReader["MA_KHOAN"]?.ToString() ?? "",
                                    MA_MUC = oracleDataReader["MA_MUC"]?.ToString() ?? "",
                                    MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"]?.ToString() ?? "",
                                    TS_SOBAOCAO = double.Parse(oracleDataReader["TS_SOBAOCAO"].ToString()),
                                    TS_SOXDTD = double.Parse(oracleDataReader["TS_SOXDTD"].ToString()),
                                    NSTN_SOBAOCAO = double.Parse(oracleDataReader["NSTN_SOBAOCAO"].ToString()),
                                    NSTN_SOXDTD = double.Parse(oracleDataReader["NSTN_SOXDTD"].ToString()),
                                    VT_SOBAOCAO = double.Parse(oracleDataReader["VT_SOBAOCAO"].ToString()),
                                    VT_SOXDTD = double.Parse(oracleDataReader["VT_SOXDTD"].ToString()),
                                    VNNN_SOBAOCAO = double.Parse(oracleDataReader["VNNN_SOBAOCAO"].ToString()),
                                    VNNN_SOXDTD = double.Parse(oracleDataReader["VNNN_SOXDTD"].ToString()),
                                    PDKTL_SOBAOCAO = double.Parse(oracleDataReader["PDKTL_SOBAOCAO"].ToString()),
                                    PDKTL_SOXDTD = double.Parse(oracleDataReader["PDKTL_SOXDTD"].ToString()),
                                    NHDKDL_SOBAOCAO = double.Parse(oracleDataReader["NHDKDL_SOBAOCAO"].ToString()),
                                    NHDKDL_SOXDTD = double.Parse(oracleDataReader["NHDKDL_SOXDTD"].ToString()),
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
