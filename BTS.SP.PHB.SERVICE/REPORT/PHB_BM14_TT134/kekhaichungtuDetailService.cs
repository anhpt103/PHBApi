
using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.PHB_BM14_TT134
{
    public interface IkekhaichungtuDetailService : IBaseService<KEKHAICHUNGTUDETAIL>
    {

    }

    public class kekhaichungtuDetailService : BaseService<KEKHAICHUNGTUDETAIL>, IkekhaichungtuDetailService
    {
        private readonly IRepositoryAsync<KEKHAICHUNGTUDETAIL> _repository;
        public kekhaichungtuDetailService(IRepositoryAsync<KEKHAICHUNGTUDETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
