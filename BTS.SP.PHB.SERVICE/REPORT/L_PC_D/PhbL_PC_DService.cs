using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.L_PC_D;
using BTS.SP.PHB.SERVICE.Models.L_PC_D;

namespace BTS.SP.PHB.SERVICE.REPORT.L_PC_D
{
    public interface IPhbL_PC_DService : IBaseService<PHB_L_PC_D>
    {
        Task<Response<PHB_L_PC_DVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbL_PC_DService : BaseService<PHB_L_PC_D>, IPhbL_PC_DService
    {
        private readonly IRepositoryAsync<PHB_L_PC_D> _repository;
        public PhbL_PC_DService(IRepositoryAsync<PHB_L_PC_D> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<PHB_L_PC_DVm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<PHB_L_PC_DVm.ViewModel> response = new Response<PHB_L_PC_DVm.ViewModel>();
            try
            {
                using (OracleConnection connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_L_PC_UB_SUMREPORT";
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

                        PHB_L_PC_DVm.ViewModel data = new PHB_L_PC_DVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_L_PC_D_DETAIL> lstTrongBang = new List<PHB_L_PC_D_DETAIL>();
                            List<PHB_L_PC_D_DETAIL> lstNgoaiBang = new List<PHB_L_PC_D_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                int loai = int.Parse(oracleDataReader["LOAI"].ToString());
                                if (loai == 1)
                                {
                                    lstTrongBang.Add(new PHB_L_PC_D_DETAIL()
                                    {
                                        STT = oracleDataReader["STT"].ToString(),
                                        HO_VATEN = oracleDataReader["HO_VATEN"].ToString(),
                                        CHUC_DANH = oracleDataReader["CHUC_DANH"].ToString(),
                                        LOAI = loai,
                                        HE_SOLUONG = decimal.Parse(oracleDataReader["HE_SOLUONG"].ToString()),
                                        PC_KV = decimal.Parse(oracleDataReader["PC_KV"].ToString()),
                                        PC_CHUCVU = decimal.Parse(oracleDataReader["PC_CHUCVU"].ToString()),
                                        PC_THAMNIEN = decimal.Parse(oracleDataReader["PC_THAMNIEN"].ToString()),
                                        PC_TRACHNHIEM = decimal.Parse(oracleDataReader["PC_TRACHNHIEM"].ToString()),
                                        PC_CONGVU = decimal.Parse(oracleDataReader["PC_CONGVU"].ToString()),
                                        PC_LOAIXA = decimal.Parse(oracleDataReader["PC_LOAIXA"].ToString()),
                                        PC_LAUNAM = decimal.Parse(oracleDataReader["PC_LAUNAM"].ToString()),
                                        PC_THUHUT = decimal.Parse(oracleDataReader["PC_THUHUT"].ToString()),
                                        CKPT_BHXH = decimal.Parse(oracleDataReader["CKPT_BHXH"].ToString()),
                                        CKPT_BHYT = decimal.Parse(oracleDataReader["CKPT_BHYT"].ToString()),
                                    });
                                }
                                else
                                {
                                    lstNgoaiBang.Add(new PHB_L_PC_D_DETAIL()
                                    {
                                        STT = oracleDataReader["STT"].ToString(),
                                        HO_VATEN = oracleDataReader["HO_VATEN"].ToString(),
                                        CHUC_DANH = oracleDataReader["CHUC_DANH"].ToString(),
                                        LOAI = loai,
                                        HE_SOLUONG = decimal.Parse(oracleDataReader["HE_SOLUONG"].ToString()),
                                        PC_KV = decimal.Parse(oracleDataReader["PC_KV"].ToString()),
                                        PC_CHUCVU = decimal.Parse(oracleDataReader["PC_CHUCVU"].ToString()),
                                        PC_THAMNIEN = decimal.Parse(oracleDataReader["PC_THAMNIEN"].ToString()),
                                        PC_TRACHNHIEM = decimal.Parse(oracleDataReader["PC_TRACHNHIEM"].ToString()),
                                        PC_CONGVU = decimal.Parse(oracleDataReader["PC_CONGVU"].ToString()),
                                        PC_LOAIXA = decimal.Parse(oracleDataReader["PC_LOAIXA"].ToString()),
                                        PC_LAUNAM = decimal.Parse(oracleDataReader["PC_LAUNAM"].ToString()),
                                        PC_THUHUT = decimal.Parse(oracleDataReader["PC_THUHUT"].ToString()),
                                        CKPT_BHXH = decimal.Parse(oracleDataReader["CKPT_BHXH"].ToString()),
                                        CKPT_BHYT = decimal.Parse(oracleDataReader["CKPT_BHYT"].ToString()),
                                    });
                                }
                            }
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
    }
}
