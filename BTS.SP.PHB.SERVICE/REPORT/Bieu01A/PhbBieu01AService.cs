using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU01A;
using BTS.SP.PHB.SERVICE.Models.BIEU01A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu01A
{
    public interface IPhbBieu01AService : IBaseService<PHB_BIEU01A>
    {
        // Task<Response<BIEU01AVm.ViewModel>> SumReport(string mahuyen,int loaibc,string machuong, int nambc, int kybc, string lstdvqhns);

        Task<Response<BIEU01AVm.DetailModel>> SumReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
        Task<Response<BIEU01AVm.DetaiRplModel>> SumAllReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu01AService : BaseService<PHB_BIEU01A>, IPhbBieu01AService
    {
        private readonly IRepositoryAsync<PHB_BIEU01A> _repository;

        public PhbBieu01AService(IRepositoryAsync<PHB_BIEU01A> repository) : base(repository)
        {
            _repository = repository;
        }

        //public async Task<Response<BIEU01AVm.ViewModel>> SumReport(string mahuyen, int loaibc, string machuong, int nambc, int kybc, string lstdvqhns)
        //{
        //    var response = new Response<BIEU01AVm.ViewModel>();
        //    try
        //    {
        //        using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "PHB_BIEU01A_SUMREPORT";
        //                command.Parameters.Clear();
        //                var pMahuyen = command.Parameters.Add("MAHUYEN", OracleDbType.NVarchar2);
        //                pMahuyen.Direction = ParameterDirection.Input;
        //                pMahuyen.Size = 3;
        //                pMahuyen.Value = mahuyen;
        //                var pMachuong = command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
        //                pMachuong.Direction = ParameterDirection.Input;
        //                pMachuong.Size = 50;
        //                pMachuong.Value = machuong;
        //                var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
        //                pNambc.Direction = ParameterDirection.Input;
        //                pNambc.Value = nambc;
        //                var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
        //                pKybc.Direction = ParameterDirection.Input;
        //                pKybc.Value = kybc;
        //                var pKLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
        //                pKLoaibc.Direction = ParameterDirection.Input;
        //                pKLoaibc.Value = loaibc;
        //                var pLstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
        //                pLstdvqhns.Direction = ParameterDirection.Input;
        //                pLstdvqhns.Size = 2000;
        //                pLstdvqhns.Value = lstdvqhns;
        //                var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
        //                pCur.Direction = ParameterDirection.Output;
        //                await command.ExecuteNonQueryAsync();
        //                var data = new BIEU01AVm.ViewModel();
        //                using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
        //                {
        //                    var lstPhi = new List<PHB_BIEU01A_DETAIL>();
        //                    var lstLephi = new List<PHB_BIEU01A_DETAIL>();
        //                    while (oracleDataReader.Read())
        //                    {
        //                        var loai = int.Parse(oracleDataReader["LOAI"].ToString());
        //                        if (loai == 1)
        //                        {
        //                            lstPhi.Add(new PHB_BIEU01A_DETAIL()
        //                            {
        //                                MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
        //                                TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
        //                                LOAI = loai,
        //                                STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
        //                                MA_CHI_TIEU = int.Parse(oracleDataReader["MA_CHI_TIEU"].ToString()),
        //                                DT_SOBAOCAO = double.Parse(oracleDataReader["DT_SOBAOCAO"].ToString()),
        //                                DT_SOXDTD = double.Parse(oracleDataReader["DT_SOXDTD"].ToString()),
        //                                TH_SOBAOCAO = double.Parse(oracleDataReader["TH_SOBAOCAO"].ToString()),
        //                                TH_SOXDTD = double.Parse(oracleDataReader["TH_SOXDTD"].ToString())
        //                            });
        //                        }
        //                        else
        //                        {
        //                            lstLephi.Add(new PHB_BIEU01A_DETAIL()
        //                            {
        //                                MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
        //                                TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
        //                                LOAI = loai,
        //                                STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
        //                                MA_CHI_TIEU = int.Parse(oracleDataReader["MA_CHI_TIEU"].ToString()),
        //                                DT_SOBAOCAO = double.Parse(oracleDataReader["DT_SOBAOCAO"].ToString()),
        //                                DT_SOXDTD = double.Parse(oracleDataReader["DT_SOXDTD"].ToString()),
        //                                TH_SOBAOCAO = double.Parse(oracleDataReader["TH_SOBAOCAO"].ToString()),
        //                                TH_SOXDTD = double.Parse(oracleDataReader["TH_SOXDTD"].ToString())
        //                            });
        //                        }
        //                    }
        //                    data.DETAIL_PHI = lstPhi;
        //                    data.DETAIL_LEPHI = lstLephi;
        //                }
        //                if (data.DETAIL_PHI.Count > 0 || data.DETAIL_LEPHI.Count > 0)
        //                {
        //                    response.Error = false;
        //                    response.Data = data;
        //                }
        //                else
        //                {
        //                    response.Error = true;
        //                    response.Message = ErrorMessage.EMPTY_DATA;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return response;
        //}

        public async Task<Response<BIEU01AVm.DetailModel>> SumReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU01AVm.DetailModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01A_SUMREPORT";
                        command.Parameters.Clear();

                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 10;
                        pMahuyen.Value = madbhc;

                        var pLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pLoaibc.Direction = ParameterDirection.Input;
                        pLoaibc.Value = loaibc;

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

                        var data = new BIEU01AVm.DetailModel();
                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            var lst = new List<BIEU01AVm.DetailModel.Item>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new BIEU01AVm.DetailModel.Item()
                                {
                                    MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                    TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU = int.Parse(oracleDataReader["MA_CHI_TIEU"].ToString()),
                                    DT_SOBAOCAO = double.Parse(oracleDataReader["DT_SOBAOCAO"].ToString()),
                                    DT_SOXDTD = double.Parse(oracleDataReader["DT_SOXDTD"].ToString()),
                                    TH_SOBAOCAO = double.Parse(oracleDataReader["TH_SOBAOCAO"].ToString()),
                                    TH_SOXDTD = double.Parse(oracleDataReader["TH_SOXDTD"].ToString()),
                                    MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                    TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString()
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

        public async Task<Response<BIEU01AVm.DetaiRplModel>> SumAllReport(string madbhc, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            var response = new Response<BIEU01AVm.DetaiRplModel>();
            var lstDetail = new List<BIEU01AVm.DetaiRplModel.Item>();
            var lstSum = new List<BIEU01AVm.DetaiRplModel.Item>();
            var lstSumDetail = new List<BIEU01AVm.DetaiRplModel.Item>();
            var data = new BIEU01AVm.DetaiRplModel();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01A_SUMREPORT";
                        command.Parameters.Clear();

                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 10;
                        pMahuyen.Value = madbhc;

                        var pLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pLoaibc.Direction = ParameterDirection.Input;
                        pLoaibc.Value = loaibc;

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

                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            while (oracleDataReader.Read())
                            {
                                lstDetail.Add(new BIEU01AVm.DetaiRplModel.Item()
                                {
                                    MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                    TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU = int.Parse(oracleDataReader["MA_CHI_TIEU"].ToString()),
                                    DT_SOBAOCAO = double.Parse(oracleDataReader["DT_SOBAOCAO"].ToString()),
                                    DT_SOXDTD = double.Parse(oracleDataReader["DT_SOXDTD"].ToString()),
                                    TH_SOBAOCAO = double.Parse(oracleDataReader["TH_SOBAOCAO"].ToString()),
                                    TH_SOXDTD = double.Parse(oracleDataReader["TH_SOXDTD"].ToString()),
                                    MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                    TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString()
                                });
                            }
                        }
                    }
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU01A_SUMALLREPORT";
                        command.Parameters.Clear();

                        var pMahuyen = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 10;
                        pMahuyen.Value = madbhc;

                        var pLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pLoaibc.Direction = ParameterDirection.Input;
                        pLoaibc.Value = loaibc;

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

                        using (var oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            while (oracleDataReader.Read())
                            {
                                lstSum.Add(new BIEU01AVm.DetaiRplModel.Item()
                                {
                                    MA_NOIDUNGKT = oracleDataReader["MA_NOIDUNGKT"].ToString(),
                                    TEN_NOIDUNGKT = oracleDataReader["TEN_NOIDUNGKT"].ToString(),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"].ToString(),
                                    DT_SOBAOCAO = double.Parse(oracleDataReader["DT_SOBAOCAO"].ToString()),
                                    DT_SOXDTD = double.Parse(oracleDataReader["DT_SOXDTD"].ToString()),
                                    TH_SOBAOCAO = double.Parse(oracleDataReader["TH_SOBAOCAO"].ToString()),
                                    TH_SOXDTD = double.Parse(oracleDataReader["TH_SOXDTD"].ToString()),
                                    TONG_THU = double.Parse(oracleDataReader["TONG_THU"].ToString()),
                                    TIEN_KHAUTRU = double.Parse(oracleDataReader["TIEN_KHAUTRU"].ToString()),
                                    TIEN_NSNN = double.Parse(oracleDataReader["TIEN_NSNN"].ToString())
                                });
                            }
                            //data.DETAIL = lstSum;
                        }

                    }
                    foreach (var item in lstSum)
                    {
                        var lstObj = new List<BIEU01AVm.DetaiRplModel.Item>();
                        foreach (var itemDetail in lstDetail)
                        {
                            if (itemDetail.MA_NOIDUNGKT == item.MA_NOIDUNGKT)
                            {
                                lstObj.Add(itemDetail);
                            }
                        }
                        if (lstObj != null)
                        {
                            lstSumDetail.Add(new BIEU01AVm.DetaiRplModel.Item()
                            {
                                MA_NOIDUNGKT = item.MA_NOIDUNGKT,
                                TEN_NOIDUNGKT = item.TEN_NOIDUNGKT,
                                LOAI = item.LOAI,
                                STT_CHI_TIEU = item.STT_CHI_TIEU,
                                DT_SOBAOCAO = item.DT_SOBAOCAO,
                                DT_SOXDTD = item.DT_SOXDTD,
                                TH_SOBAOCAO = item.TH_SOBAOCAO,
                                TH_SOXDTD = item.TH_SOXDTD,
                                TONG_THU = item.TONG_THU + lstObj[0].TH_SOBAOCAO,
                                TIEN_KHAUTRU = item.TIEN_KHAUTRU + lstObj[1].TH_SOBAOCAO,
                                TIEN_NSNN = item.TIEN_NSNN + lstObj[2].TH_SOBAOCAO
                            });
                        }
                        else
                        {
                            lstSumDetail.Add(new BIEU01AVm.DetaiRplModel.Item()
                            {
                                MA_NOIDUNGKT = item.MA_NOIDUNGKT,
                                TEN_NOIDUNGKT = item.TEN_NOIDUNGKT,
                                LOAI = item.LOAI,
                                STT_CHI_TIEU = item.STT_CHI_TIEU,
                                DT_SOBAOCAO = item.DT_SOBAOCAO,
                                DT_SOXDTD = item.DT_SOXDTD,
                                TH_SOBAOCAO = item.TH_SOBAOCAO,
                                TH_SOXDTD = item.TH_SOXDTD,
                                TONG_THU = item.TONG_THU,
                                TIEN_KHAUTRU = item.TIEN_KHAUTRU,
                                TIEN_NSNN = item.TIEN_NSNN
                            });
                        }
                    }
                    if (loaibc == 2 && chitiet == true)
                    {
                        data.DETAIL = lstSumDetail;
                    }
                    else
                    {
                        data.DETAIL = lstSum;
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
