using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;
using BTS.SP.PHB.SERVICE.Models.C_B01X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B01X
{
    public interface IPhbC_B01XService : IBaseService<PHB_C_B01X>
    {
        Task<Response<PHB_C_B01XVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbC_B01XService : BaseService<PHB_C_B01X>, IPhbC_B01XService
    {
        private readonly IRepositoryAsync<PHB_C_B01X> _repository;
        public PhbC_B01XService(IRepositoryAsync<PHB_C_B01X> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PHB_C_B01XVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<PHB_C_B01XVm.ViewModel> response = new Response<PHB_C_B01XVm.ViewModel>();
            try
            {
                using (OracleConnection connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B01X_SUMREPORT";
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

                        OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        PHB_C_B01XVm.ViewModel data = new PHB_C_B01XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B01X_DETAIL> lstTrongBang = new List<PHB_C_B01X_DETAIL>();
                            List<PHB_C_B01X_DETAIL> lstNgoaiBang = new List<PHB_C_B01X_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                int loai = int.Parse(oracleDataReader["LOAI"].ToString());
                                if (loai == 1)
                                {
                                    lstTrongBang.Add(new PHB_C_B01X_DETAIL()
                                    {
                                        MA_TAIKHOAN = oracleDataReader["MA_TAIKHOAN"].ToString(),
                                        TEN_TAIKHOAN = oracleDataReader["TEN_TAIKHOAN"].ToString(),
                                        LOAI = loai,
                                        SDDK_NO = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                        SDDK_CO = double.Parse(oracleDataReader["SDDK_CO"].ToString()),
                                        PSTK_NO = double.Parse(oracleDataReader["PSTK_NO"].ToString()),
                                        PSTK_CO = double.Parse(oracleDataReader["PSTK_CO"].ToString()),
                                        LUYKE_NO = double.Parse(oracleDataReader["LUYKE_NO"].ToString()),
                                        LUYKE_CO = double.Parse(oracleDataReader["LUYKE_CO"].ToString()),
                                        SDCK_NO = double.Parse(oracleDataReader["SDCK_NO"].ToString()),
                                    });
                                }
                                else
                                {
                                    lstNgoaiBang.Add(new PHB_C_B01X_DETAIL()
                                    {
                                        MA_TAIKHOAN = oracleDataReader["MA_TAIKHOAN"].ToString(),
                                        TEN_TAIKHOAN = oracleDataReader["TEN_TAIKHOAN"].ToString(),
                                        LOAI = loai,
                                        SDDK_NO = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                        SDDK_CO = double.Parse(oracleDataReader["SDDK_CO"].ToString()),
                                        PSTK_NO = double.Parse(oracleDataReader["PSTK_NO"].ToString()),
                                        PSTK_CO = double.Parse(oracleDataReader["PSTK_CO"].ToString()),
                                        LUYKE_NO = double.Parse(oracleDataReader["LUYKE_NO"].ToString()),
                                        LUYKE_CO = double.Parse(oracleDataReader["LUYKE_CO"].ToString()),
                                        SDCK_NO = double.Parse(oracleDataReader["SDCK_NO"].ToString()),
                                    });
                                }

                            }
                            data.DETAIL_TRONGBANG = lstTrongBang;
                            data.DETAIL_NGOAIBANG = lstNgoaiBang;
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
