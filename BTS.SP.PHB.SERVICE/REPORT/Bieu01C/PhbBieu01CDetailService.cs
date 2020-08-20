using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU01C;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU01C
{
    public interface IPhbBIEU01CDetailService : IBaseService<PHB_BIEU01C_DETAIL>
    {
        
    }
    public class PhbBIEU01CDetailService:BaseService<PHB_BIEU01C_DETAIL>,IPhbBIEU01CDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU01C_DETAIL> _repository;
        public PhbBIEU01CDetailService(IRepositoryAsync<PHB_BIEU01C_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
