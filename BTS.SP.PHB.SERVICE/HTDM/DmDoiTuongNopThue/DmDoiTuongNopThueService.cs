using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmDoiTuongNopThue
{
    public interface IDmDoiTuongNopThueService : IBaseService<PHB_DM_DOITUONGNOPTHUE>
    {
    }
    public class DmDoiTuongNopThueService : BaseService<PHB_DM_DOITUONGNOPTHUE>, IDmDoiTuongNopThueService
    {
        private readonly IRepositoryAsync<PHB_DM_DOITUONGNOPTHUE> _repository;
        public DmDoiTuongNopThueService(IRepositoryAsync<PHB_DM_DOITUONGNOPTHUE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
