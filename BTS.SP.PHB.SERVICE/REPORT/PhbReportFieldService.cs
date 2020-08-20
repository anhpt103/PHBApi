using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT
{
    public interface IPhbReportFieldService:IBaseService<PHB_REPORT_FIELD>
    {
    }
    public class PhbReportFieldService:BaseService<PHB_REPORT_FIELD>, IPhbReportFieldService
    {
        private readonly IRepositoryAsync<PHB_REPORT_FIELD> _repository;

        public PhbReportFieldService(IRepositoryAsync<PHB_REPORT_FIELD> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
