using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.C_B06X;
using BTS.SP.PHB.SERVICE.Models.C_B06X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B06X
{
    public interface IPhbC_B06XService : IBaseService<PHB_C_B06X>
    {
        Task<Response<PHB_C_B06XVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
        Task<Response<PHB_C_B06XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName);
        Task<Response<PHB_C_B06XVm.DetailModel>> SumReport_HTML(string madbhc, string madbhc_cha, int nambc, int kybc);
    }
    public class PhbC_B06XService : BaseService<PHB_C_B06X>, IPhbC_B06XService
    {
        private readonly IRepositoryAsync<PHB_C_B06X> _repository;
        public PhbC_B06XService(IRepositoryAsync<PHB_C_B06X> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PHB_C_B06XVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<PHB_C_B06XVm.ViewModel> response = new Response<PHB_C_B06XVm.ViewModel>();
            try
            {
                using (OracleConnection connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B06X_SUMREPORT";
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

                        PHB_C_B06XVm.ViewModel data = new PHB_C_B06XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            //List<PHB_C_B06X_DETAIL> lstTrongBang = new List<PHB_C_B06X_DETAIL>();
                            //List<PHB_C_B06X_DETAIL> lstNgoaiBang = new List<PHB_C_B06X_DETAIL>();
                            //while (oracleDataReader.Read())
                            //{
                            //    int loai = int.Parse(oracleDataReader["LOAI"].ToString());
                                //if (loai == 1)
                                //{
                                //    lstTrongBang.Add(new PHB_C_B06X_DETAIL()
                                //    {
                                //        MA_TAIKHOAN = oracleDataReader["MA_TAIKHOAN"].ToString(),
                                //        TEN_TAIKHOAN = oracleDataReader["TEN_TAIKHOAN"].ToString(),
                                //        LOAI = loai,
                                //        SDDK_NO = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                //        SDDK_CO = double.Parse(oracleDataReader["SDDK_CO"].ToString()),
                                //        PSTK_NO = double.Parse(oracleDataReader["PSTK_NO"].ToString()),
                                //        PSTK_CO = double.Parse(oracleDataReader["PSTK_CO"].ToString()),
                                //        LUYKE_NO = double.Parse(oracleDataReader["LUYKE_NO"].ToString()),
                                //        LUYKE_CO = double.Parse(oracleDataReader["LUYKE_CO"].ToString()),
                                //        SDCK_NO = double.Parse(oracleDataReader["SDCK_NO"].ToString()),
                                //    });
                                //}
                                //else
                                //{
                                //    lstNgoaiBang.Add(new PHB_C_B06X_DETAIL()
                                //    {
                                //        MA_TAIKHOAN = oracleDataReader["MA_TAIKHOAN"].ToString(),
                                //        TEN_TAIKHOAN = oracleDataReader["TEN_TAIKHOAN"].ToString(),
                                //        LOAI = loai,
                                //        SDDK_NO = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                //        SDDK_CO = double.Parse(oracleDataReader["SDDK_CO"].ToString()),
                                //        PSTK_NO = double.Parse(oracleDataReader["PSTK_NO"].ToString()),
                                //        PSTK_CO = double.Parse(oracleDataReader["PSTK_CO"].ToString()),
                                //        LUYKE_NO = double.Parse(oracleDataReader["LUYKE_NO"].ToString()),
                                //        LUYKE_CO = double.Parse(oracleDataReader["LUYKE_CO"].ToString()),
                                //        SDCK_NO = double.Parse(oracleDataReader["SDCK_NO"].ToString()),
                                //    });
                                //}

                            //}
                            //data.DETAIL_TRONGBANG = lstTrongBang;
                            //data.DETAIL_NGOAIBANG = lstNgoaiBang;
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

        public async Task<Response<PHB_C_B06XVm.DetailModel>> SumReport_HTML(string madbhc, string madbhc_cha, int nambc, int kybc)
        {
            var response = new Response<PHB_C_B06XVm.DetailModel>();

            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B06X_SUMREPORT_HTML";
                        command.Parameters.Clear();

                        var pmadbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pmadbhc.Direction = ParameterDirection.Input;
                        pmadbhc.Size = 5;
                        pmadbhc.Value = madbhc;

                        var pmadbhc_cha = command.Parameters.Add("MADBHC_CHA", OracleDbType.NVarchar2);
                        pmadbhc_cha.Direction = ParameterDirection.Input;
                        pmadbhc_cha.Size = 5;
                        pmadbhc_cha.Value = madbhc_cha;

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        var data = new PHB_C_B06XVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<PHB_C_B06XVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                double SDDK, TONG_THU, TONG_CHI, CON_LAI;
                                var isSDDK = double.TryParse(oracleDataReader["SDDK"].ToString(), out SDDK);
                                var isTONG_THU = double.TryParse(oracleDataReader["TONG_THU"].ToString(), out TONG_THU);
                                var isTONG_CHI = double.TryParse(oracleDataReader["TONG_CHI"].ToString(), out TONG_CHI);
                                var isCON_LAI = double.TryParse(oracleDataReader["CON_LAI"].ToString(), out CON_LAI);
                                lst.Add(new PHB_C_B06XVm.DetailModel.Item()
                                {
                                    TEN_CHITIEU = oracleDataReader["TEN_CHITIEU"].ToString(),
                                    SDDK = isSDDK ? SDDK : 0,
                                    TONG_THU = isTONG_THU ? TONG_THU : 0,
                                    TONG_CHI = isTONG_CHI ? TONG_CHI : 0,
                                    CON_LAI = isCON_LAI ? CON_LAI : 0

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
        public async Task<Response<PHB_C_B06XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName)
        {
            Response<PHB_C_B06XVm.ViewModel> response = new Response<PHB_C_B06XVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B06X_MERGEREPORT";
                        command.Parameters.Clear();
                        var pmadbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pmadbhc.Direction = ParameterDirection.Input;
                        pmadbhc.Size = 5;
                        pmadbhc.Value = madbhc;

                        var pmadbhc_cha = command.Parameters.Add("MADBHC_CHA", OracleDbType.NVarchar2);
                        pmadbhc_cha.Direction = ParameterDirection.Input;
                        pmadbhc_cha.Size = 5;
                        pmadbhc_cha.Value = madbhc_cha;

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        PHB_C_B06XVm.ViewModel data = new PHB_C_B06XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B06X_DETAIL> lst = new List<PHB_C_B06X_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_C_B06X_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    TEN_CHITIEU = oracleDataReader["TEN_CHITIEU"]?.ToString() ?? ""
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
