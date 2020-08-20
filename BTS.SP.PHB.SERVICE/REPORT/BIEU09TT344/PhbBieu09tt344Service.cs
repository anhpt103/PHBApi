using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU09TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.SERVICE.Models.BIEU09TT344;
using BTS.SP.PHB.ENTITY.Helper;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU09TT344
{
    public interface IPhbBieu09tt344Service : IBaseService<PHB_BIEU09TT344>
    {
        Task<Response<BIEU09TT344Vm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu09tt344Service : BaseService<PHB_BIEU09TT344>, IPhbBieu09tt344Service
    {
        private readonly IRepositoryAsync<PHB_BIEU09TT344> _repository;

        public PhbBieu09tt344Service(IRepositoryAsync<PHB_BIEU09TT344> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<BIEU09TT344Vm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<BIEU09TT344Vm.ViewModel> response = new Response<BIEU09TT344Vm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU09TT344_SUMREPORT";
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

                        BIEU09TT344Vm.ViewModel data = new BIEU09TT344Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU09TT344_DETAIL> lst = new List<PHB_BIEU09TT344_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU09TT344_DETAIL()
                                {
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    DT_TONGSO = double.Parse(oracleDataReader["DT_TONGSO"].ToString()),
                                    DT_DTPT = double.Parse(oracleDataReader["DT_DTPT"].ToString()),
                                    DT_TX = double.Parse(oracleDataReader["DT_TX"].ToString()),
                                    QT_TONGSO = double.Parse(oracleDataReader["QT_TONGSO"].ToString()),
                                    QT_DTPT = double.Parse(oracleDataReader["QT_DTPT"].ToString()),
                                    QT_TX = double.Parse(oracleDataReader["QT_TX"].ToString()),
                                    SS_TONGSO = double.Parse(oracleDataReader["SS_TONGSO"].ToString()),
                                    SS_DTPT = double.Parse(oracleDataReader["SS_DTPT"].ToString()),
                                    SS_TX = double.Parse(oracleDataReader["SS_TX"].ToString()),
                                    IN_DAM = int.Parse(oracleDataReader["IN_DAM"].ToString())
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
