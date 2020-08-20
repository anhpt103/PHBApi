﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU10TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU10TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU10TT344
{
    public interface IPhbBieu10tt344Service : IBaseService<PHB_BIEU10TT344>
    {
        Task<Response<BIEU10TT344Vm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns);
    }
    public class PhbBieu10tt344Service : BaseService<PHB_BIEU10TT344>, IPhbBieu10tt344Service
    {
        private readonly IRepositoryAsync<PHB_BIEU10TT344> _repository;

        public PhbBieu10tt344Service(IRepositoryAsync<PHB_BIEU10TT344> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<Response<BIEU10TT344Vm.ViewModel>> SumReport(string machuong, int nambc, int kybc, string lstdvqhns)
        {
            Response<BIEU10TT344Vm.ViewModel> response = new Response<BIEU10TT344Vm.ViewModel>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_BIEU10TT344_SUMREPORT";
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

                        BIEU10TT344Vm.ViewModel data = new BIEU10TT344Vm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_BIEU10TT344_DETAIL> lst = new List<PHB_BIEU10TT344_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_BIEU10TT344_DETAIL()
                                {
                                    MA_CHUONG = oracleDataReader["MA_CHUONG"].ToString(),
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