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
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu01B
{
    public interface IPhbBieu01BTemplateService:IBaseService<PHB_BIEU01B_TEMPLATE>
    {
        Task<Response<List<PHB_BIEU01B_TEMPLATE>>> GetTemplateForEdit(string refid);
    }
    public class PhbBieu01BTemplateService:BaseService<PHB_BIEU01B_TEMPLATE>, IPhbBieu01BTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU01B_TEMPLATE> _repository;
        public PhbBieu01BTemplateService(IRepositoryAsync<PHB_BIEU01B_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<PHB_BIEU01B_TEMPLATE>>> GetTemplateForEdit(string refid)
        {
            Response<List<PHB_BIEU01B_TEMPLATE>> response = new Response<List<PHB_BIEU01B_TEMPLATE>>();
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
                            @"SELECT TEMPLATE.PHAN,TEMPLATE.CAP,TEMPLATE.LOAI,TEMPLATE.STT_CHI_TIEU,TEMPLATE.MA_CHI_TIEU,TEMPLATE.TEN_CHI_TIEU FROM PHB_BIEU01B_TEMPLATE TEMPLATE WHERE MA_CHI_TIEU NOT IN 
                            (SELECT DISTINCT(PHB_BIEU01B_DETAIL.MA_CHI_TIEU) FROM PHB_BIEU01B_DETAIL WHERE PHB_BIEU01B_REFID=:refid) ORDER BY TEMPLATE.MA_CHI_TIEU";
                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 32).Value = refid;
                        using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (!dataReader.HasRows) return new Response<List<PHB_BIEU01B_TEMPLATE>>() { Error = false, Data = new List<PHB_BIEU01B_TEMPLATE>() };
                            List<PHB_BIEU01B_TEMPLATE> lst = new List<PHB_BIEU01B_TEMPLATE>();
                            while (dataReader.Read())
                            {
                                lst.Add(new PHB_BIEU01B_TEMPLATE()
                                {
                                    PHAN = int.Parse(dataReader["PHAN"].ToString()),
                                    CAP = int.Parse(dataReader["CAP"].ToString()),
                                    LOAI = int.Parse(dataReader["LOAI"].ToString()),
                                    STT_CHI_TIEU = dataReader["STT_CHI_TIEU"].ToString(),
                                    MA_CHI_TIEU = dataReader["MA_CHI_TIEU"].ToString(),
                                    TEN_CHI_TIEU = dataReader["TEN_CHI_TIEU"].ToString()
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
