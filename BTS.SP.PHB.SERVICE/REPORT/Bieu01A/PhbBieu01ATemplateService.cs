﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU01A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu01A
{
    public interface IPhbBieu01ATemplateService : IBaseService<PHB_BIEU01A_TEMPLATE>
    {
        Task<Response<List<PHB_BIEU01A_TEMPLATE>>> GetTemplateForEdit(string refid);
    }
    public class PhbBieu01ATemplateService:BaseService<PHB_BIEU01A_TEMPLATE>, IPhbBieu01ATemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU01A_TEMPLATE> _repository;
        public PhbBieu01ATemplateService(IRepositoryAsync<PHB_BIEU01A_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<PHB_BIEU01A_TEMPLATE>>> GetTemplateForEdit(string refid)
        {
            Response<List<PHB_BIEU01A_TEMPLATE>> response = new Response<List<PHB_BIEU01A_TEMPLATE>>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText =
                            @"SELECT TEMPLATE.LOAI,TEMPLATE.STT_CHI_TIEU,TEMPLATE.MA_NOIDUNGKT,TEMPLATE.TEN_NOIDUNGKT FROM PHB_BIEU01A_TEMPLATE TEMPLATE WHERE MA_NOIDUNGKT NOT IN 
                            (SELECT DISTINCT(PHB_BIEU01A_DETAIL.MA_NOIDUNGKT) FROM PHB_BIEU01A_DETAIL WHERE PHB_BIEU01A_REFID=:refid) ORDER BY TEMPLATE.MA_NOIDUNGKT";
                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 32).Value = refid;
                        using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if(!dataReader.HasRows) return new Response<List<PHB_BIEU01A_TEMPLATE>>() {Error = false,Data = new List<PHB_BIEU01A_TEMPLATE>()};
                            List<PHB_BIEU01A_TEMPLATE> lst =new List<PHB_BIEU01A_TEMPLATE>();
                            while (dataReader.Read())
                            {
                                lst.Add(new PHB_BIEU01A_TEMPLATE()
                                {
                                    LOAI = int.Parse(dataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = dataReader["STT_CHI_TIEU"].ToString(),
                                    MA_NOIDUNGKT = dataReader["MA_NOIDUNGKT"].ToString(),
                                    TEN_NOIDUNGKT = dataReader["TEN_NOIDUNGKT"].ToString()
                                });
                            }
                            response.Error = false;
                            response.Data = lst;
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
    }
}
