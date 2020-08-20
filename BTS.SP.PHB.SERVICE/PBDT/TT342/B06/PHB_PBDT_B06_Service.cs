﻿using BTS.SP.PHB.ENTITY.PBDT.B06;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B06
{
    public interface IPHB_PBDT_B06_Service : IBaseService<PHB_PBDT_B06>
    {
    }

    public class PHB_PBDT_B06_Service : BaseService<PHB_PBDT_B06>, IPHB_PBDT_B06_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B06> _repository;
        public PHB_PBDT_B06_Service(IRepositoryAsync<PHB_PBDT_B06> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}