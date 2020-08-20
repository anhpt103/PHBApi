using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B03X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.SERVICE.Models.C_B03X;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B03X
{
    public interface IPhbcB03xService : IBaseService<PHB_C_B03X>
    {
        Task<Response<C_B03XVm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbcB03xService : BaseService<PHB_C_B03X>, IPhbcB03xService
    {
        private readonly IRepositoryAsync<PHB_C_B03X> _repository;

        public PhbcB03xService(IRepositoryAsync<PHB_C_B03X> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<C_B03XVm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<C_B03XVm.ViewModel> response = new Response<C_B03XVm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B03X_SUMREPORT";
                        command.Parameters.Clear();
                        OracleParameter p_machuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
                        p_machuong.Direction = ParameterDirection.Input;
                        p_machuong.Size = 50;
                        p_machuong.Value = machuong;

                        OracleParameter p_madbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        p_madbhc.Direction = ParameterDirection.Input;
                        p_madbhc.Size = 50;
                        p_madbhc.Value = madbhc;

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

                        C_B03XVm.ViewModel data = new C_B03XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B03X_DETAIL> lst = new List<PHB_C_B03X_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_C_B03X_DETAIL()
                                {
                                    // = oracleDataReader["MA_CHUONG"].ToString(),
                                    //MA_KHOAN = oracleDataReader["MA_KHOAN"]?.ToString(),
                                    //MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"].ToString(),
                                    //NOI_DUNG_CHI = oracleDataReader["NOI_DUNG_CHI"].ToString(),
                                    //SO_TIEN = double.Parse(oracleDataReader["SO_TIEN"].ToString()),
                                    //LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    //PHAN = oracleDataReader["PHAN"].ToString()
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
