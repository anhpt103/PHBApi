using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU01B;
using BTS.SP.PHB.SERVICE.Models.BIEU01B;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu01B
{
    public interface IPhbBieu01BService : IBaseService<PHB_BIEU01B>
    {
        Task<Response<BIEU01BVm.DetailModel>> SumReport(string username, int loaibc,bool chitiet,int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU01BVm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU01BVm.DetailModel>> SumAllReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU01BVm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName,int phan,int cap);
    }
    public class PhbBieu01BService:BaseService<PHB_BIEU01B>, IPhbBieu01BService
    {
        private readonly IRepositoryAsync<PHB_BIEU01B> _repository;
        public PhbBieu01BService(IRepositoryAsync<PHB_BIEU01B> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<BIEU01BVm.DetailModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response =new Response<BIEU01BVm.DetailModel>();
            try
            {
                using (var connection =new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01B_SUMREPORT";
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

                        var data =new BIEU01BVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor) pCur.Value).GetDataReader())
                        {
                            var lst =new List<BIEU01BVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int PHAN, CAP, LOAI;
                                double SO_BC, SO_DCKT;
                                var isPHAN = int.TryParse(oracleDataReader["PHAN"].ToString(), out PHAN);
                                var isCAP = int.TryParse(oracleDataReader["CAP"].ToString(), out CAP);
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isSO_BC = double.TryParse(oracleDataReader["SO_BC"].ToString(), out SO_BC);
                                var isSO_DCKT = double.TryParse(oracleDataReader["SO_DCKT"].ToString(), out SO_DCKT);
                                lst.Add(new BIEU01BVm.DetailModel.Item()
                                {
                                    PHAN = isPHAN ? PHAN : 0,
                                    CAP = isCAP ? CAP : 0,
                                    LOAI = isLOAI ? LOAI : 0,
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_BC = isSO_BC ? SO_BC : 0,
                                    SO_DCKT = isSO_DCKT ? SO_DCKT : 0,
                                    MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                    TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString(),
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
        public async Task<Response<BIEU01BVm.DetailModel>> SumReport_HTML(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU01BVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01B_SUMREPORT_HTML";
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

                        var data = new BIEU01BVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU01BVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int PHAN, CAP, LOAI;
                                double SO_BC, SO_DCKT;
                                var isPHAN = int.TryParse(oracleDataReader["PHAN"].ToString(), out PHAN);
                                var isCAP = int.TryParse(oracleDataReader["CAP"].ToString(), out CAP);
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isSO_BC = double.TryParse(oracleDataReader["SO_BC"].ToString(), out SO_BC);
                                var isSO_DCKT = double.TryParse(oracleDataReader["SO_DCKT"].ToString(), out SO_DCKT);
                                lst.Add(new BIEU01BVm.DetailModel.Item()
                                {
                                    PHAN = isPHAN ? PHAN : 0,
                                    CAP = isCAP ? CAP : 0,
                                    LOAI = isLOAI ? LOAI : 0,
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_BC = isSO_BC ? SO_BC : 0,
                                    SO_DCKT = isSO_DCKT ? SO_DCKT : 0
                                    //MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                    //TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString(),
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

        public async Task<Response<BIEU01BVm.DetailModel>> SumAllReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU01BVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01B_SUMALLREPORT";
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

                        var data = new BIEU01BVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU01BVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                int PHAN, LOAI;
                                double SO_BC, SO_DCKT;
                                var isPHAN = int.TryParse(oracleDataReader["PHAN"].ToString(), out PHAN);
                                var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                var isSO_BC = double.TryParse(oracleDataReader["NAM_NAY"].ToString(), out SO_BC);
                                var isSO_DCKT = double.TryParse(oracleDataReader["NAM_NAY"].ToString(), out SO_DCKT);
                                lst.Add(new BIEU01BVm.DetailModel.Item()
                                {
                                    PHAN = isPHAN ? PHAN : 0,
                                    LOAI = isLOAI ? LOAI : 0,
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
                                    MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
                                    SO_BC = isSO_BC ? SO_BC : 0,
                                    SO_DCKT = isSO_DCKT ? SO_DCKT : 0
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

        public async Task<Response<BIEU01BVm.ViewModel>> MergeReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns, List<string> changeList, string newName, int phan,int cap)
        {
            Response<BIEU01BVm.ViewModel> response = new Response<BIEU01BVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01B_MERGEREPORT";
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

                        BIEU01BVm.ViewModel data = new BIEU01BVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU01B_DETAIL> lst = new List<PHB_BIEU01B_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                int PHAN, CAP;
                                var isPHAN = int.TryParse(oracleDataReader["PHAN"].ToString(), out PHAN);
                                var isCAP = int.TryParse(oracleDataReader["CAP"].ToString(), out CAP);
                                //var isLOAI = int.TryParse(oracleDataReader["LOAI"].ToString(), out LOAI);
                                lst.Add(new PHB_BIEU01B_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString() ?? "",
                                    PHAN = isPHAN ? PHAN : 0,
                                    CAP = isCAP ? CAP : 0
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
