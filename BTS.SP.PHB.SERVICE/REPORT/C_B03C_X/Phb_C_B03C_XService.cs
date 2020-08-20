﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.Models.C_B03C_X;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.SERVICES;
using BTS.SP.PHB.ENTITY.Rp.C_B03C_X;
using Repository.Pattern.Repositories;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B03C_X
{
    public interface IPhb_C_B03C_XService : IBaseService<PHB_C_B03C_X>  
    {  
        Task<Response<C_B03C_XVm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns);
        Task<Response<C_B03C_XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName);
    }
    public class Phb_C_B03C_XService : BaseService<PHB_C_B03C_X>, IPhb_C_B03C_XService
    {
        private readonly IRepositoryAsync<PHB_C_B03C_X> _repository;
        public Phb_C_B03C_XService(IRepositoryAsync<PHB_C_B03C_X> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<C_B03C_XVm.ViewModel>> MergeReport(string madbhc, string madbhc_cha, int nambc, int kybc, List<string> changeList, string newName)
        {
            Response<C_B03C_XVm.ViewModel> response = new Response<C_B03C_XVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B03C_X_MERGEREPORT";
                        command.Parameters.Clear();
                        var pmadbhc = command.Parameters.Add("MADBHC", OracleDbType.NVarchar2);
                        pmadbhc.Direction = ParameterDirection.Input;
                        pmadbhc.Size = 5;
                        pmadbhc.Value = madbhc;

                        var pmadbhc_cha = command.Parameters.Add("MADBHC_CHA", OracleDbType.NVarchar2);
                        pmadbhc_cha.Direction = ParameterDirection.Input;
                        pmadbhc_cha.Size = 5;
                        pmadbhc_cha.Value = madbhc_cha;

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;

                        await command.ExecuteNonQueryAsync();

                        C_B03C_XVm.ViewModel data = new C_B03C_XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B03C_X_DETAIL> lst = new List<PHB_C_B03C_X_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_C_B03C_X_DETAIL()
                                {
                                    ID = int.Parse(oracleDataReader["ID"].ToString()),
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"]?.ToString() ?? ""
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

        public async Task<Response<C_B03C_XVm.ViewModel>> SumReport(string madbhc, string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<C_B03C_XVm.ViewModel> response = new Response<C_B03C_XVm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_C_B03C_X_SUMREPORT";
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

                        C_B03C_XVm.ViewModel data = new C_B03C_XVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_C_B03C_X_DETAIL> lst = new List<PHB_C_B03C_X_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_C_B03C_X_DETAIL()
                                {
                                    PHAN = int.Parse(oracleDataReader["PHAN"].ToString()),
                                    TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
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
