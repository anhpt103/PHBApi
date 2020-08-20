using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.B03BBCTC;

namespace BTS.SP.PHB.SERVICE.REPORT.B03BCTC
{
    public interface IPhbB03BctcService : IBaseService<PHB_B03BBCTC>
    {
        Task<Response<PhbB03BctcVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }

    public class PhbB03BctcService : BaseService<PHB_B03BBCTC>, IPhbB03BctcService
    {
        private readonly IRepositoryAsync<PHB_B03BBCTC> _repository;
        public PhbB03BctcService(IRepositoryAsync<PHB_B03BBCTC> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PhbB03BctcVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<PhbB03BctcVm.ViewModel> response = new Response<PhbB03BctcVm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B03BCTC_SUMREPORT";
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

                        PhbB03BctcVm.ViewModel data = new PhbB03BctcVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_B03BBCTC_DETAIL> lst = new List<PHB_B03BBCTC_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_B03BBCTC_DETAIL()
                                {
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    THUYET_MINH = double.Parse(oracleDataReader["THUYET_MINH"].ToString()),
                                    NAM_NAY = double.Parse(oracleDataReader["NAM_NAY"].ToString()),
                                    NAM_TRUOC = double.Parse(oracleDataReader["NAM_TRUOC"].ToString()),
                                    PHAN = int.Parse(oracleDataReader["PHAN"].ToString())
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
