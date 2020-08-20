using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B02BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.SERVICE.Models.B02BCQT;
using BTS.SP.PHB.ENTITY.Helper;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.ENTITY.Rp.PHB.B02BCQT;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.REPORT.B02BCQT
{
    public interface IPhbB02BcqtService : IBaseService<PHB_B02BCQT>
    {
        Task<Response<B02BCQTVm.ViewModel>> Sumreport(string machuong, int nambc, int kybc, string lstdvqhns, string madbhc);
        Task<Response<B02BCQTVm.DetailModel>>SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<B02BCQTVm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName,string phan);
    }
    public class PhbB02BcqtService : BaseService<PHB_B02BCQT>, IPhbB02BcqtService
    {
        private readonly IRepositoryAsync<PHB_B02BCQT> _repository;

        public PhbB02BcqtService(IRepositoryAsync<PHB_B02BCQT> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<B02BCQTVm.ViewModel>> Sumreport(string machuong, int nambc, int kybc, string lstdvqhns, string madbhc)
        {
            Response<B02BCQTVm.ViewModel> response = new Response<B02BCQTVm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B02BCQT_SUMREPORT";
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

                        OracleParameter p_madbhc = command.Parameters.Add("MA_DBHC", OracleDbType.NVarchar2);
                        p_madbhc.Direction = ParameterDirection.Input;
                        p_madbhc.Size = 50;
                        p_madbhc.Value = madbhc;

                        OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        B02BCQTVm.ViewModel data = new B02BCQTVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_B02BCQT_DETAIL> lst = new List<PHB_B02BCQT_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                int PHAN,LOAI;
                                double SKN_TONGSO, SKN_THANHTRA, SKN_KIEMTOAN,SKN_TAICHINH, SDXL_TONGSO, SDXL_THANHTRA, SDXL_KIEMTOAN, SDXL_TAICHINH, SCXL_TONGSO, SCXL_THANHTRA, SCXL_KIEMTOAN, SCXL_TAICHINH;
                                var isPHAN = int.TryParse(oracleDataReader["PHAN"].ToString(), out PHAN);
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isSKN_TONGSO = double.TryParse(oracleDataReader["SKN_TONGSO"].ToString(), out SKN_TONGSO);
                                var isSKN_THANHTRA = double.TryParse(oracleDataReader["SKN_THANHTRA"].ToString(), out SKN_THANHTRA);
                                var isSKN_KIEMTOAN = double.TryParse(oracleDataReader["SKN_KIEMTOAN"].ToString(), out SKN_KIEMTOAN);
                                var isSKN_TAICHINH = double.TryParse(oracleDataReader["SKN_TAICHINH"].ToString(), out SKN_TAICHINH);
                                var isSDXL_TONGSO = double.TryParse(oracleDataReader["SDXL_TONGSO"].ToString(), out SDXL_TONGSO);
                                var isSDXL_THANHTRA = double.TryParse(oracleDataReader["SDXL_THANHTRA"].ToString(), out SDXL_THANHTRA);
                                var isSDXL_KIEMTOAN = double.TryParse(oracleDataReader["SDXL_KIEMTOAN"].ToString(), out SDXL_KIEMTOAN);
                                var isSDXL_TAICHINH = double.TryParse(oracleDataReader["SDXL_TAICHINH"].ToString(), out SDXL_TAICHINH);
                                var isSCXL_TONGSO = double.TryParse(oracleDataReader["SCXL_TONGSO"].ToString(), out SCXL_TONGSO);
                                var isSCXL_THANHTRA = double.TryParse(oracleDataReader["SCXL_THANHTRA"].ToString(), out SCXL_THANHTRA);
                                var isSCXL_KIEMTOAN = double.TryParse(oracleDataReader["SCXL_KIEMTOAN"].ToString(), out SCXL_KIEMTOAN);
                                var isSCXL_TAICHINH = double.TryParse(oracleDataReader["SCXL_TAICHINH"].ToString(), out SCXL_TAICHINH);
                                lst.Add(new PHB_B02BCQT_DETAIL()
                                {
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU_CHA = oracleDataReader["MA_CHI_TIEU_CHA"].ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    PHAN = oracleDataReader["PHAN"].ToString(),
                                    LOAI = isLOAI ? LOAI : 0,
                                    SKN_TONGSO = isSKN_TONGSO ? SKN_TONGSO : 0,
                                    SKN_THANHTRA = isSKN_THANHTRA ? SKN_THANHTRA : 0,
                                    SKN_KIEMTOAN = isSKN_KIEMTOAN ? SKN_KIEMTOAN : 0,
                                    SKN_TAICHINH = isSKN_TAICHINH ? SKN_TAICHINH : 0,
                                    SDXL_TONGSO = isSDXL_TONGSO ? SDXL_TONGSO : 0,
                                    SDXL_THANHTRA = isSDXL_THANHTRA ? SDXL_THANHTRA : 0,
                                    SDXL_KIEMTOAN = isSDXL_KIEMTOAN ? SDXL_KIEMTOAN : 0,
                                    SDXL_TAICHINH = isSDXL_TAICHINH ? SDXL_TAICHINH : 0,
                                    SCXL_TONGSO = isSCXL_TONGSO ? SCXL_TONGSO : 0,
                                    SCXL_THANHTRA = isSCXL_THANHTRA ? SCXL_THANHTRA : 0,
                                    SCXL_KIEMTOAN = isSCXL_KIEMTOAN ? SCXL_KIEMTOAN : 0,
                                    SCXL_TAICHINH = isSCXL_TAICHINH ? SCXL_TAICHINH : 0,

                                    //STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    //TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    //MA_CHI_TIEU_CHA = oracleDataReader["MA_CHI_TIEU_CHA"].ToString(),
                                    //MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    //PHAN = oracleDataReader["PHAN"].ToString(),
                                    //LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    //SKN_TONGSO = double.Parse(oracleDataReader["SKN_TONGSO"].ToString()),
                                    //SKN_THANHTRA = double.Parse(oracleDataReader["SKN_THANHTRA"].ToString()),
                                    //SKN_KIEMTOAN = double.Parse(oracleDataReader["SKN_KIEMTOAN"].ToString()),
                                    //SKN_TAICHINH = double.Parse(oracleDataReader["SKN_TAICHINH"].ToString()),
                                    //SDXL_TONGSO = double.Parse(oracleDataReader["SDXL_TONGSO"].ToString()),
                                    //SDXL_THANHTRA = double.Parse(oracleDataReader["SDXL_THANHTRA"].ToString()),
                                    //SDXL_KIEMTOAN = double.Parse(oracleDataReader["SDXL_KIEMTOAN"].ToString()),
                                    //SDXL_TAICHINH = double.Parse(oracleDataReader["SDXL_TAICHINH"].ToString()),
                                    //SCXL_TONGSO = double.Parse(oracleDataReader["SCXL_TONGSO"].ToString()),
                                    //SCXL_THANHTRA = double.Parse(oracleDataReader["SCXL_THANHTRA"].ToString()),
                                    //SCXL_KIEMTOAN = double.Parse(oracleDataReader["SCXL_KIEMTOAN"].ToString()),
                                    //SCXL_TAICHINH = double.Parse(oracleDataReader["SCXL_TAICHINH"].ToString()),
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

        public async Task<Response<B02BCQTVm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<B02BCQTVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B02BCQT_SUMREPORT_HTML";
                        command.Parameters.Clear();

                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 50;
                        pMahuyen.Value = username;

                        var pKLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pKLoaibc.Direction = ParameterDirection.Input;
                        pKLoaibc.Value = loaibc;

                        var pChitiet = command.Parameters.Add("CHITIET", OracleDbType.Int32);
                        pChitiet.Direction = ParameterDirection.Input;
                        pChitiet.Value = Convert.ToInt32(chitiet);

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

                        var data = new B02BCQTVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<B02BCQTVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int PHAN, LOAI;
                                double SKN_TONGSO, SKN_THANHTRA, SKN_KIEMTOAN, SKN_TAICHINH, SDXL_TONGSO, SDXL_THANHTRA, SDXL_KIEMTOAN, SDXL_TAICHINH, SCXL_TONGSO, SCXL_THANHTRA, SCXL_KIEMTOAN, SCXL_TAICHINH;
                                var isPHAN = int.TryParse(oracleDataReader["PHAN"].ToString(), out PHAN);
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isSKN_TONGSO = double.TryParse(oracleDataReader["SKN_TONGSO"].ToString(), out SKN_TONGSO);
                                var isSKN_THANHTRA = double.TryParse(oracleDataReader["SKN_THANHTRA"].ToString(), out SKN_THANHTRA);
                                var isSKN_KIEMTOAN = double.TryParse(oracleDataReader["SKN_KIEMTOAN"].ToString(), out SKN_KIEMTOAN);
                                var isSKN_TAICHINH = double.TryParse(oracleDataReader["SKN_TAICHINH"].ToString(), out SKN_TAICHINH);
                                var isSDXL_TONGSO = double.TryParse(oracleDataReader["SDXL_TONGSO"].ToString(), out SDXL_TONGSO);
                                var isSDXL_THANHTRA = double.TryParse(oracleDataReader["SDXL_THANHTRA"].ToString(), out SDXL_THANHTRA);
                                var isSDXL_KIEMTOAN = double.TryParse(oracleDataReader["SDXL_KIEMTOAN"].ToString(), out SDXL_KIEMTOAN);
                                var isSDXL_TAICHINH = double.TryParse(oracleDataReader["SDXL_TAICHINH"].ToString(), out SDXL_TAICHINH);
                                var isSCXL_TONGSO = double.TryParse(oracleDataReader["SCXL_TONGSO"].ToString(), out SCXL_TONGSO);
                                var isSCXL_THANHTRA = double.TryParse(oracleDataReader["SCXL_THANHTRA"].ToString(), out SCXL_THANHTRA);
                                var isSCXL_KIEMTOAN = double.TryParse(oracleDataReader["SCXL_KIEMTOAN"].ToString(), out SCXL_KIEMTOAN);
                                var isSCXL_TAICHINH = double.TryParse(oracleDataReader["SCXL_TAICHINH"].ToString(), out SCXL_TAICHINH);
                                lst.Add(new B02BCQTVm.DetailModel.Item()
                                {
                                    //MA_CHI_TIEU_CHA = oracleDataReader["MA_CHI_TIEU_CHA"].ToString(),
                                    PHAN = oracleDataReader["PHAN"].ToString(),
                                    LOAI = isLOAI ? LOAI : 0,
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SKN_TONGSO = isSKN_TONGSO ? SKN_TONGSO : 0,
                                    SKN_THANHTRA = isSKN_THANHTRA ? SKN_THANHTRA : 0,
                                    SKN_KIEMTOAN = isSKN_KIEMTOAN ? SKN_KIEMTOAN : 0,
                                    SKN_TAICHINH = isSKN_TAICHINH ? SKN_TAICHINH : 0,
                                    SDXL_TONGSO = isSDXL_TONGSO ? SDXL_TONGSO : 0,
                                    SDXL_THANHTRA = isSDXL_THANHTRA ? SDXL_THANHTRA : 0,
                                    SDXL_KIEMTOAN = isSDXL_KIEMTOAN ? SDXL_KIEMTOAN : 0,
                                    SDXL_TAICHINH = isSDXL_TAICHINH ? SDXL_TAICHINH : 0,
                                    SCXL_TONGSO = isSCXL_TONGSO ? SCXL_TONGSO : 0,
                                    SCXL_THANHTRA = isSCXL_THANHTRA ? SCXL_THANHTRA : 0,
                                    SCXL_KIEMTOAN = isSCXL_KIEMTOAN ? SCXL_KIEMTOAN : 0,
                                    SCXL_TAICHINH = isSCXL_TAICHINH ? SCXL_TAICHINH : 0
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

        public async Task<Response<B02BCQTVm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName, string phan)
        {
            Response<B02BCQTVm.ViewModel> response = new Response<B02BCQTVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_B02BCQT_MERGEREPORT";
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

                        B02BCQTVm.ViewModel data = new B02BCQTVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_B02BCQT_DETAIL> lst = new List<PHB_B02BCQT_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_B02BCQT_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString() ?? "",
                                    PHAN = oracleDataReader["PHAN"]?.ToString() ?? ""
                                    //MA_CHI_TIEU_CHA = oracleDataReader["MA_CHI_TIEU_CHA"]?.ToString() ?? ""
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
