using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTyLeDieuTiet
{
    public interface IDmTyLeDieuTietService : IBaseService<PHB_DM_TYLEDIEUTIET>
    {
    }
    public class DmTyLeDieuTietService : BaseService<PHB_DM_TYLEDIEUTIET>, IDmTyLeDieuTietService
    {
        private readonly IRepositoryAsync<PHB_DM_TYLEDIEUTIET> _repository;
        public DmTyLeDieuTietService(IRepositoryAsync<PHB_DM_TYLEDIEUTIET> repository) : base(repository)
        {
            _repository = repository; 
        }
    }
}
