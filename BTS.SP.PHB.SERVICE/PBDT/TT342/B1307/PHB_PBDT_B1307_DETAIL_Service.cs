﻿using BTS.SP.PHB.ENTITY.PBDT.B1307;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1307
{
    public interface IPHB_PBDT_B1307_DETAIL_Service : IBaseService<PHB_PBDT_B1307_DETAIL>
    {
    }

    public class PHB_PBDT_B1307_DETAIL_Service : BaseService<PHB_PBDT_B1307_DETAIL>, IPHB_PBDT_B1307_DETAIL_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1307_DETAIL> _repository;
        public PHB_PBDT_B1307_DETAIL_Service(IRepositoryAsync<PHB_PBDT_B1307_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
