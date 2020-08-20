using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;


namespace BTS.SP.PHB.SERVICE.REPORT.PHB_BM14_TT134
{
    public interface IkekhaichungtuService : IBaseService<KEKHAICHUNGTU>
    {

    }
    public class kekhaichungtuService : BaseService<KEKHAICHUNGTU>, IkekhaichungtuService
    {
        private readonly IRepositoryAsync<KEKHAICHUNGTU> _repository;
        public kekhaichungtuService(IRepositoryAsync<KEKHAICHUNGTU> repository) : base(repository)
        {
            _repository = repository;
        }
    }
   
}
