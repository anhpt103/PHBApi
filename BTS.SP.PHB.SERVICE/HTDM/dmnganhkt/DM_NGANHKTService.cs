using BTS.SP.PHB.ENTITY.Dm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BTS.SP.API.ENTITY.Models.Sys;
using BTS.SP.API.ENTITY.Models.Dm.PHA;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNganhKT
{
    public interface IDM_NGANHKTService : IBaseService<DM_NGANHKT>
    {
        //Add function here
    }
    public class DM_NGANHKTService : BaseService<DM_NGANHKT>, IDM_NGANHKTService
    {
        private readonly IRepositoryAsync<DM_NGANHKT> _repository;
        public DM_NGANHKTService(IRepositoryAsync<DM_NGANHKT> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
