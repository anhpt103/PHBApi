using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.F01_02BCQT_PII;
using BTS.SP.PHB.SERVICE.Models.F01_02BCQT_PII;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT_PII
{
    public interface IF01_02BCQT_PIIService:IBaseService<PHB_F01_02BCQT_PII>
    {
        Task<Response<F01_02BCQT_PIIVm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class F01_02BCQT_PIIService:BaseService<PHB_F01_02BCQT_PII>, IF01_02BCQT_PIIService
    {
        private readonly IRepositoryAsync<PHB_F01_02BCQT_PII> _repository;
        public F01_02BCQT_PIIService(IRepositoryAsync<PHB_F01_02BCQT_PII> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<F01_02BCQT_PIIVm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<F01_02BCQT_PIIVm.ViewModel> response =new Response<F01_02BCQT_PIIVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_F01_02BCQT_PII_SUMREPORT";
                        command.Parameters.Clear();
                        OracleParameter p_machuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
                        p_machuong.Direction = ParameterDirection.Input;
                        p_machuong.Size = 50;
                        p_machuong.Value = machuong;

                        OracleParameter p_nambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        p_nambc.Direction = ParameterDirection.Input;
                        p_nambc.Value = nambc;

                        OracleParameter p_kybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        p_kybc.Direction = ParameterDirection.Input;
                        p_kybc.Value = kybc;

                        OracleParameter p_lstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        p_lstdvqhns.Direction = ParameterDirection.Input;
                        p_lstdvqhns.Size = 2000;
                        p_lstdvqhns.Value = lstdvqhns;

                        OracleParameter p_madbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        p_madbhc.Direction = ParameterDirection.Input;
                        p_madbhc.Size = 50;
                        p_madbhc.Value = madbhc;

                        OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        F01_02BCQT_PIIVm.ViewModel data = new F01_02BCQT_PIIVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_F01_02BCQT_PII_DETAIL> lst = new List<PHB_F01_02BCQT_PII_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_F01_02BCQT_PII_DETAIL()
                                {
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    MA_LOAI = oracleDataReader["MA_LOAI"]?.ToString() ?? "",
                                    MA_KHOAN = oracleDataReader["MA_KHOAN"]?.ToString() ?? "",
                                    MA_MUC = oracleDataReader["MA_MUC"]?.ToString() ?? "",
                                    MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"]?.ToString() ?? "",
                                    NOI_DUNG_CHI = oracleDataReader["NOI_DUNG_CHI"]?.ToString() ?? "",
                                    TONG_SO_NAM_NAY = double.Parse(oracleDataReader["TONG_SO_NAM_NAY"].ToString()),
                                    NSNN_TRONGNUOC_NAM_NAY = double.Parse(oracleDataReader["NSNN_TRONGNUOC_NAM_NAY"].ToString()),
                                    VIEN_TRO_NAM_NAY = double.Parse(oracleDataReader["VIEN_TRO_NAM_NAY"].ToString()),
                                    VAYNO_NUOCNGOAI_NAM_NAY = double.Parse(oracleDataReader["VAYNO_NUOCNGOAI_NAM_NAY"].ToString()),
                                    TONG_SO_LUY_KE = double.Parse(oracleDataReader["TONG_SO_LUY_KE"].ToString()),
                                    NSNN_TRONGNUOC_LUY_KE = double.Parse(oracleDataReader["NSNN_TRONGNUOC_LUY_KE"].ToString()),
                                    VIEN_TRO_LUY_KE = double.Parse(oracleDataReader["VIEN_TRO_LUY_KE"].ToString()),
                                    VAYNO_NUOCNGOAI_LUY_KE = double.Parse(oracleDataReader["VAYNO_NUOCNGOAI_LUY_KE"].ToString()),
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
