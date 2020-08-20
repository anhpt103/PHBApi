
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTKKhoBac
{
    public interface IDmTKKhoBacService : IBaseService<DM_TKKHOBAC>
    {

    }
    public class DmTKKhoBacService : BaseService<DM_TKKHOBAC>, IDmTKKhoBacService
    {
        private readonly IRepositoryAsync<DM_TKKHOBAC> _repository;
        public DmTKKhoBacService(IRepositoryAsync<DM_TKKHOBAC> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
