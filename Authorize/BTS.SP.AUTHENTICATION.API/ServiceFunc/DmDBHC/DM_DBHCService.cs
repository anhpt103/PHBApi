using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BTS.SP.AUTHENTICATION.API.Services;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Dm.Entities;

namespace BTS.SP.AUTHENTICATION.API.ServiceFunc.DmDBHC
{
    public interface IDM_DBHCService : IDataInfoService<DM_DBHC>
    {
        //Add function here
        List<string> GetListDbhc(string madiaban);
    }
    public class DM_DBHCService : DataInfoServiceBase<DM_DBHC>, IDM_DBHCService
    {
        public DM_DBHCService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        protected override Expression<Func<DM_DBHC, bool>> GetKeyFilter(DM_DBHC instance)
        {
            return x => x.MA_DBHC == instance.MA_DBHC;
        }

        public List<string> GetListDbhc(string madiaban)
        {
            try
            {
               
                var a = UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.MA_T == madiaban).Select(x => x.MA_DBHC).ToList();
                
                  
                return a;
            }
            catch (Exception ex)
            {
                 
                return null;
            }
        }
    }
}
