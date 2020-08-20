using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B01X
{
    public interface IPhbC_B01XTemplateService:IBaseService<PHB_C_B01X_TEMPLATE>
    {
        Task<Response<List<PHB_C_B01X_TEMPLATE>>> GetTemplateForEdit(string refid);
    }
    public class PhbC_B01XTemplateService:BaseService<PHB_C_B01X_TEMPLATE>, IPhbC_B01XTemplateService
    {
        private readonly IRepositoryAsync<PHB_C_B01X_TEMPLATE> _repository;
        public PhbC_B01XTemplateService(IRepositoryAsync<PHB_C_B01X_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<PHB_C_B01X_TEMPLATE>>> GetTemplateForEdit(string refid)
        {
            Response<List<PHB_C_B01X_TEMPLATE>> response = new Response<List<PHB_C_B01X_TEMPLATE>>();
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
                            @"SELECT TEMPLATE.LOAI,TEMPLATE.MA_TAIKHOAN,TEMPLATE.TEN_TAIKHOAN FROM PHB_C_B01X_TEMPLATE TEMPLATE WHERE MA_TAIKHOAN NOT IN 
                            (SELECT DISTINCT(PHB_C_B01X_DETAIL.MA_TAIKHOAN) FROM PHB_C_B01X_DETAIL WHERE PHB_C_B01X_REFID=:refid) ORDER BY TEMPLATE.MA_TAIKHOAN";
                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 32).Value = refid;
                        using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (!dataReader.HasRows) return new Response<List<PHB_C_B01X_TEMPLATE>>() { Error = false, Data = new List<PHB_C_B01X_TEMPLATE>() };
                            List<PHB_C_B01X_TEMPLATE> lst = new List<PHB_C_B01X_TEMPLATE>();
                            while (dataReader.Read())
                            {
                                lst.Add(new PHB_C_B01X_TEMPLATE()
                                {
                                    LOAI = int.Parse(dataReader["LOAI"].ToString()),
                                    MA_TAIKHOAN = dataReader["MA_TAIKHOAN"].ToString(),
                                    TEN_TAIKHOAN = dataReader["TEN_TAIKHOAN"].ToString()
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
