﻿using BTS.SP.PHB.ENTITY.PBDT.B1310;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1310
{
    public interface IPHB_PBDT_B1310_Service : IBaseService<PHB_PBDT_B1310>
    {
    }

    public class PHB_PBDT_B1310_Service : BaseService<PHB_PBDT_B1310>, IPHB_PBDT_B1310_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1310> _repository;
        public PHB_PBDT_B1310_Service(IRepositoryAsync<PHB_PBDT_B1310> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
