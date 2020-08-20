using BTS.SP.PHB.ENTITY.Rp.DoiChieuTABMIS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System.Collections.Generic;

namespace BTS.SP.PHB.SERVICE.TienIch
{
    public interface IDoiChieuSoLieuService : IBaseService<PHC_DOICHIEUSOLIEUHEADER>
    {
        List<PHC_DOICHIEUSOLIEUDETAILS> InsertData(List<DoiChieuSoLieuVm.DoiChieuSoLieuDetails> instance);
        List<PHC_DOICHIEUSOLIEUHEADER> InsertDto(List<DoiChieuSoLieuVm.DoiChieuSoLieuDto> instance);
    }
    public class DoiChieuSoLieuService : BaseService<PHC_DOICHIEUSOLIEUHEADER>, IDoiChieuSoLieuService
    {
        private readonly IRepositoryAsync<PHC_DOICHIEUSOLIEUHEADER> _repository;

        public DoiChieuSoLieuService(IRepositoryAsync<PHC_DOICHIEUSOLIEUHEADER> repository) : base(repository)
        {
            _repository = repository;
        }

        public List<PHC_DOICHIEUSOLIEUDETAILS> InsertData(List<DoiChieuSoLieuVm.DoiChieuSoLieuDetails> instance)
        {
            //var detailData = AutoMapper.Mapper.Map<List<DoiChieuSoLieuVm.DoiChieuSoLieuDetails>, List<PHC_DOICHIEUSOLIEUDETAILS>>(instance);
            var detailData = new List<PHC_DOICHIEUSOLIEUDETAILS>();
            detailData.ForEach(x => {        
            });
            _repository.GetRepository<PHC_DOICHIEUSOLIEUDETAILS>().InsertRange(detailData);
            return detailData;
        }

        public List<PHC_DOICHIEUSOLIEUHEADER> InsertDto(List<DoiChieuSoLieuVm.DoiChieuSoLieuDto> instance)
        {
            //var dataDto = AutoMapper.Mapper.Map<List<DoiChieuSoLieuVm.DoiChieuSoLieuDto>, List<PHC_DOICHIEUSOLIEUHEADER>>(instance);
            var dataDto = new List<PHC_DOICHIEUSOLIEUHEADER>();

            dataDto.ForEach(x => {
            });
            _repository.GetRepository<PHC_DOICHIEUSOLIEUHEADER>().InsertRange(dataDto);
            return dataDto;
        }
    }
}

   
