using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.SERVICE.Models.BIEU03;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu03
{
    public interface IPhbBieu03DetailService : IBaseService<PHB_BIEU03_DETAIL>
    {
        Task<Response<List<BIEU03Vm.DetailModel.Item>>> GetDetailByRefId(string refid);
    }
    public class PhbBieu03DetailService:BaseService<PHB_BIEU03_DETAIL>,IPhbBieu03DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU03_DETAIL> _repository;
        public PhbBieu03DetailService(IRepositoryAsync<PHB_BIEU03_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<BIEU03Vm.DetailModel.Item>>> GetDetailByRefId(string refid)
        {
            var response =new Response<List<BIEU03Vm.DetailModel.Item>>();
            try
            {
                var lst = new List<BIEU03Vm.DetailModel.Item>();
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "Select dt.ID,dt.STT_CHI_TIEU,dt.MA_CHI_TIEU,dt.TEN_CHI_TIEU,dt.LOAI,dt.SAPXEP,dt.DU_TOAN_NAM_TRUOC,dt.DU_TOAN_DUOC_GIAO,dt.DU_TOAN_DUOC_SU_DUNG," +
                                             "dt.QUYET_TOAN_NAM,NVL(tpl.INDAM,0) as INDAM,NVL(tpl.INNGHIENG,1) as INNGHIENG from PHB_BIEU03_DETAIL dt LEFT JOIN PHB_BIEU03_TEMPLATE tpl " +
                                             "ON tpl.MA_CHI_TIEU = dt.MA_CHI_TIEU Where dt.PHB_BIEU03_REFID = '"+refid+"' " +
                                             "ORDER by dt.SAPXEP,dt.MA_CHI_TIEU";
                        using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                var item = new BIEU03Vm.DetailModel.Item();
                                item.ID = int.Parse(reader["ID"].ToString());
                                item.STT_CHI_TIEU = reader["STT_CHI_TIEU"]?.ToString();
                                item.MA_CHI_TIEU = reader["MA_CHI_TIEU"].ToString();
                                item.TEN_CHI_TIEU = reader["TEN_CHI_TIEU"].ToString();
                                item.LOAI = int.Parse(reader["LOAI"].ToString());
                                item.SAPXEP = int.Parse(reader["SAPXEP"].ToString());
                                item.DU_TOAN_NAM_TRUOC = double.Parse(reader["DU_TOAN_NAM_TRUOC"].ToString());
                                item.DU_TOAN_DUOC_GIAO = double.Parse(reader["DU_TOAN_DUOC_GIAO"].ToString());
                                item.DU_TOAN_DUOC_SU_DUNG = double.Parse(reader["DU_TOAN_DUOC_SU_DUNG"].ToString());
                                item.QUYET_TOAN_NAM = double.Parse(reader["QUYET_TOAN_NAM"].ToString());
                                item.INDAM = int.Parse(reader["INDAM"].ToString());
                                item.INNGHIENG = int.Parse(reader["INNGHIENG"].ToString());
                                lst.Add(item);
                            }
                        }
                        response.Error = false;
                        response.Data = lst;
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
