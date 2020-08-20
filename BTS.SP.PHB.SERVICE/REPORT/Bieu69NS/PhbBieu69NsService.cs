using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU69NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.Models.BIEU69NS;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu69NS
{
    public interface IPhbBieu69NsService : IBaseService<PHB_BIEU69NS>
    {
        Task<Response<BIEU69NSVm.ViewModel>> SumReport(string maDbhc, int loaibc, int nambc, int kybc, string lstdvqhns, bool chitiet);
    }
    public class PhbBieu69NsService:BaseService<PHB_BIEU69NS>,IPhbBieu69NsService
    {
        private readonly IRepositoryAsync<PHB_BIEU69NS> _repository;
        public PhbBieu69NsService(IRepositoryAsync<PHB_BIEU69NS> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<BIEU69NSVm.ViewModel>> SumReport(string username, int loaibc, int nambc, int kybc, string lstdvqhns ,bool chitiet)
        {
            Response<BIEU69NSVm.ViewModel> response = new Response<BIEU69NSVm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU69NS_SUMREPORT";
                        command.Parameters.Clear();
                        //OracleParameter p_machuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
                        //p_machuong.Direction = ParameterDirection.Input;
                        //p_machuong.Size = 50;
                        //p_machuong.Value = machuong;
                        OracleParameter pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 50;
                        pMahuyen.Value = username;

                        OracleParameter pKLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pKLoaibc.Direction = ParameterDirection.Input;
                        pKLoaibc.Value = loaibc;

                        OracleParameter pChitiet = command.Parameters.Add("CHITIET", OracleDbType.Int32);
                        pChitiet.Direction = ParameterDirection.Input;
                        pChitiet.Value = Convert.ToInt32(chitiet);

                        OracleParameter p_nambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        p_nambc.Direction = ParameterDirection.Input;
                        p_nambc.Value = nambc;

                        OracleParameter p_kybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        p_kybc.Direction = ParameterDirection.Input;
                        p_kybc.Value = kybc;

                        OracleParameter p_lstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        p_lstdvqhns.Direction = ParameterDirection.Input;
                        p_lstdvqhns.Size = 2000;
                        p_lstdvqhns.Value = null;

                        OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        BIEU69NSVm.ViewModel data = new BIEU69NSVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU69NS_DETAIL> lst = new List<PHB_BIEU69NS_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                double skn_thanhtra,skn_kiemtoan, SXL_KIEMTOAN, SXL_THANHTRA, SCXL_THANHTRA, SCXL_KIEMTOAN;
                                var isSkn_thanhtra = double.TryParse(oracleDataReader["SKN_THANHTRA"].ToString(), out skn_thanhtra);
                                var isskn_kiemtoan = double.TryParse(oracleDataReader["SKN_KIEMTOAN"].ToString(), out skn_kiemtoan);
                                var isSXL_KIEMTOAN = double.TryParse(oracleDataReader["SXL_KIEMTOAN"].ToString(), out SXL_KIEMTOAN);
                                var isSXL_THANHTRA = double.TryParse(oracleDataReader["SXL_THANHTRA"].ToString(), out SXL_THANHTRA);
                                var isSCXL_THANHTRA = double.TryParse(oracleDataReader["SCXL_THANHTRA"].ToString(), out SCXL_THANHTRA);
                                var isSCXL_KIEMTOAN = double.TryParse(oracleDataReader["SCXL_KIEMTOAN"].ToString(), out SCXL_KIEMTOAN);
                                lst.Add(new PHB_BIEU69NS_DETAIL()
                                {

                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    SKN_THANHTRA =  isSkn_thanhtra ? skn_thanhtra : 0,
                                    SKN_KIEMTOAN = isskn_kiemtoan ? skn_kiemtoan : 0,
                                    SCXL_THANHTRA = isSXL_KIEMTOAN ? SXL_KIEMTOAN : 0,
                                    SCXL_KIEMTOAN = isSXL_THANHTRA ? SXL_THANHTRA : 0,
                                    SXL_THANHTRA = isSCXL_THANHTRA ? SCXL_THANHTRA : 0,
                                    SXL_KIEMTOAN = isSCXL_KIEMTOAN ? SCXL_KIEMTOAN :0,
                                    //LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    PHAN = oracleDataReader["PHAN"].ToString(),
                                    MA_CHI_TIEU_CHA = oracleDataReader["MA_CHI_TIEU_CHA"].ToString()
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
