using BTS.SP.PHB.ENTITY.Rp.B02H_II;
using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Helper;
using Repository.Pattern.Repositories;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.SERVICE.Models.B02H_II;

namespace BTS.SP.PHB.SERVICE.REPORT.B02H_II
{
    public interface IB02H_IIService : IBaseService<PHB_B02H_II>
    {
        Task<Response<B02H_IIVm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class B02H_IIService : BaseService<PHB_B02H_II>, IB02H_IIService
    {
        private readonly IRepositoryAsync<PHB_B02H_II> _repository;
        public B02H_IIService(IRepositoryAsync<PHB_B02H_II> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<B02H_IIVm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            Response<B02H_IIVm.ViewModel> response = new Response<B02H_IIVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B02H_II_SUMREPORT";
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

                        B02H_IIVm.ViewModel data = new B02H_IIVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_B02H_II_DETAIL> lst = new List<PHB_B02H_II_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_B02H_II_DETAIL()
                                {
                                    MA_SO = int.Parse(oracleDataReader["MA_SO"].ToString()),
                                    NOI_DUNG_CHI = oracleDataReader["NOI_DUNG_CHI"]?.ToString() ?? "",
                                    MA_LOAI = oracleDataReader["MA_LOAI"]?.ToString() ?? "",
                                    MA_KHOAN = oracleDataReader["MA_KHOAN"]?.ToString() ?? "",
                                    MA_MUC = oracleDataReader["MA_MUC"]?.ToString() ?? "",
                                    MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"]?.ToString() ?? "",
                                    TONG_SO = double.Parse(oracleDataReader["TONG_SO"].ToString()),
                                    NSNN_TONG_SO = double.Parse(oracleDataReader["NSNN_TONG_SO"].ToString()),
                                    NSNN_NSNN_GIAO = double.Parse(oracleDataReader["NSNN_NSNN_GIAO"].ToString()),
                                    NSNN_PHI_LEPHI = double.Parse(oracleDataReader["NSNN_PHI_LEPHI"].ToString()),
                                    NSNN_VIEN_TRO = double.Parse(oracleDataReader["NSNN_VIEN_TRO"].ToString()),
                                    NGUON_KHAC = double.Parse(oracleDataReader["NGUON_KHAC"].ToString())
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
