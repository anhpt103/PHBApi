using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;
using BTS.SP.PHB.ENTITY.Rp.DUTOANLUONG;

namespace BTS.SP.PHB.SERVICE.REPORT.DUTOANLUONG
{
    public interface IPhbDuToanLuongDetailService : IBaseService<PHB_DUTOANLUONG_DETAIL>
    {

    }
    public class PhbDuToanLuongDetailService : BaseService<PHB_DUTOANLUONG_DETAIL>, IPhbDuToanLuongDetailService
    {
        private readonly IRepositoryAsync<PHB_DUTOANLUONG_DETAIL> _repository;
        public PhbDuToanLuongDetailService(IRepositoryAsync<PHB_DUTOANLUONG_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
