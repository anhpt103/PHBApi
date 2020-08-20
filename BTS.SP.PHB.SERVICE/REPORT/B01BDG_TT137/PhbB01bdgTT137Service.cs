﻿using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.B01BDG_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01BDG_TT137
{
    public interface IPhbB01bdgTT137Service : IBaseService<PHB_B01BDG_TT137>
    {
    }
    public class PhbB01bdgTT137Service : BaseService<PHB_B01BDG_TT137>, IPhbB01bdgTT137Service
    {
        private readonly IRepositoryAsync<PHB_B01BDG_TT137> _repository;
        public PhbB01bdgTT137Service(IRepositoryAsync<PHB_B01BDG_TT137> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}