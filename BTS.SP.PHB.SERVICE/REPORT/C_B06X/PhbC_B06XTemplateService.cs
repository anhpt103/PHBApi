using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.C_B06X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B06X
{
    public interface IPhbC_B06XTemplateService:IBaseService<PHB_C_B06X_TEMPLATE>
    {
        Task<Response<List<PHB_C_B06X_TEMPLATE>>> GetTemplateForEdit(string refid);
    }
    public class PhbC_B06XTemplateService:BaseService<PHB_C_B06X_TEMPLATE>, IPhbC_B06XTemplateService
    {
        private readonly IRepositoryAsync<PHB_C_B06X_TEMPLATE> _repository;
        public PhbC_B06XTemplateService(IRepositoryAsync<PHB_C_B06X_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<PHB_C_B06X_TEMPLATE>>> GetTemplateForEdit(string refid)
        {
            Response<List<PHB_C_B06X_TEMPLATE>> response = new Response<List<PHB_C_B06X_TEMPLATE>>();
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
                            @"SELECT TEMPLATE.LOAI,TEMPLATE.MA_CHITIEU,TEMPLATE.TEN_CHITIEU FROM PHB_C_B06X_TEMPLATE TEMPLATE WHERE MA_CHITIEU NOT IN 
                            (SELECT DISTINCT(PHB_C_B06X_DETAIL.MA_CHITIEU) FROM PHB_C_B06X_DETAIL WHERE PHB_C_B06X_REFID=:refid) ORDER BY TEMPLATE.MA_CHITIEU";
                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 32).Value = refid;
                        using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (!dataReader.HasRows) return new Response<List<PHB_C_B06X_TEMPLATE>>() { Error = false, Data = new List<PHB_C_B06X_TEMPLATE>() };
                            List<PHB_C_B06X_TEMPLATE> lst = new List<PHB_C_B06X_TEMPLATE>();
                            while (dataReader.Read())
                            {
                                lst.Add(new PHB_C_B06X_TEMPLATE()
                                {
                                    LOAI = int.Parse(dataReader["LOAI"].ToString()),
                                    MA_CHITIEU = dataReader["MA_CHITIEU"].ToString(),
                                    TEN_CHITIEU = dataReader["TEN_CHITIEU"].ToString()
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
