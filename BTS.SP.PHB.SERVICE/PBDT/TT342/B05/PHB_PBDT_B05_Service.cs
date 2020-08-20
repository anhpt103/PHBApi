﻿using BTS.SP.PHB.ENTITY.PBDT.B05;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B05
{
    public interface IPHB_PBDT_B05_Service : IBaseService<PHB_PBDT_B05>
    {
    }

    public class PHB_PBDT_B05_Service : BaseService<PHB_PBDT_B05>, IPHB_PBDT_B05_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B05> _repository;
        public PHB_PBDT_B05_Service(IRepositoryAsync<PHB_PBDT_B05> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
