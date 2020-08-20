using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.B03BCQT_BII1;
using BTS.SP.PHB.SERVICE.Models.B03BCQT_BII1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B03BCQT_BII1
{
    public interface IPhbB03BCQT_BII1Service : IBaseService<PHB_B03BCQT_BII1>
    {
        Task<Response<B03BCQT_BII1Vm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbB03BCQT_BII1Service:BaseService<PHB_B03BCQT_BII1>, IPhbB03BCQT_BII1Service
    {
        private readonly IRepositoryAsync<PHB_B03BCQT_BII1> _repository;
        public PhbB03BCQT_BII1Service(IRepositoryAsync<PHB_B03BCQT_BII1> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<B03BCQT_BII1Vm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            Response<B03BCQT_BII1Vm.ViewModel> response =new Response<B03BCQT_BII1Vm.ViewModel>();
            try
            {
                using (OracleConnection connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B03BCQT_BII1_SUMREPORT";
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

                        B03BCQT_BII1Vm.ViewModel data =new B03BCQT_BII1Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_B03BCQT_BII1_DETAIL> lst_phi = new List<PHB_B03BCQT_BII1_DETAIL>();
                            List<PHB_B03BCQT_BII1_DETAIL> lst_lephi = new List<PHB_B03BCQT_BII1_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                int loai = int.Parse(oracleDataReader["LOAI"].ToString());
                                if (loai == 1)
                                {
                                    lst_phi.Add(new PHB_B03BCQT_BII1_DETAIL()
                                    {
                                        MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                        TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                        LOAI = loai,
                                        STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                        TONG_THU = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                        TIEN_NSNN = double.Parse(oracleDataReader["TIEN_NSNN"].ToString()),
                                        TIEN_KHAUTRU = double.Parse(oracleDataReader["TIEN_KHAUTRU"].ToString()),
                                        GHI_CHU = oracleDataReader["GHI_CHU"].ToString()
                                    });
                                }
                                else
                                {
                                    lst_lephi.Add(new PHB_B03BCQT_BII1_DETAIL()
                                    {
                                        MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                        TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                        LOAI = loai,
                                        STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                        TONG_THU = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                        TIEN_NSNN = double.Parse(oracleDataReader["TIEN_NSNN"].ToString()),
                                        TIEN_KHAUTRU = double.Parse(oracleDataReader["TIEN_KHAUTRU"].ToString()),
                                        GHI_CHU = oracleDataReader["GHI_CHU"].ToString()
                                    });
                                }

                            }
                            data.DETAIL_PHI = lst_phi;
                            data.DETAIL_LEPHI = lst_lephi;
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
