﻿using BTS.SP.PHB.ENTITY.PBDT.B05;
using BTS.SP.PHB.ENTITY.PBDT.B1502;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1502
{
    public interface IPHB_PBDT_B1502_ROW_Service : IBaseService<PHB_PBDT_B1502_ROW>
    {
    }

    public class PHB_PBDT_B1502_ROW_Service : BaseService<PHB_PBDT_B1502_ROW>, IPHB_PBDT_B1502_ROW_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1502_ROW> _repository;
        public PHB_PBDT_B1502_ROW_Service(IRepositoryAsync<PHB_PBDT_B1502_ROW> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
