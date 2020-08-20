using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU11TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU11TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using BTS.SP.PHB.ENTITY;
using Oracle.ManagedDataAccess.Types;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu11TT344
{
    public interface IPhbBieu11TT344Service : IBaseService<PHB_BIEU11TT344>
    {
        Task<Response<BIEU11TT344Vm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns);
    }

    public class PhbBieu11TT344Service : BaseService<PHB_BIEU11TT344>, IPhbBieu11TT344Service
    {
        private readonly IRepositoryAsync<PHB_BIEU11TT344> _repository;

        public PhbBieu11TT344Service(IRepositoryAsync<PHB_BIEU11TT344> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU11TT344Vm.ViewModel>> SumReport(string madbhc,string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<BIEU11TT344Vm.ViewModel> response = new Response<BIEU11TT344Vm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU11TT344_SUMREPORT";
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

                        BIEU11TT344Vm.ViewModel data = new BIEU11TT344Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU11TT344_DETAIL> lst = new List<PHB_BIEU11TT344_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU11TT344_DETAIL()
                                {
                                    MA_CHUONG = oracleDataReader["MA_CHUONG"].ToString(),
                                    MA_LOAI = oracleDataReader["MA_LOAI"].ToString(),
                                    MA_KHOAN = oracleDataReader["MA_KHOAN"]?.ToString(),
                                    MA_MUC = oracleDataReader["MA_MUC"].ToString(),
                                    MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"].ToString(),
                                    DIEN_GIAI = oracleDataReader["DIEN_GIAI"].ToString(),
                                    QUYET_TOAN = double.Parse(oracleDataReader["QUYET_TOAN"].ToString()),
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
