using System.Linq;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung
{
    public interface IAuNguoiDungService :IBaseService<AU_NGUOIDUNG>
    {
        AU_NGUOIDUNG FindUser(string username, string password);

    }
    public class AuNguoiDungService : BaseService<AU_NGUOIDUNG>, IAuNguoiDungService
    {
        private readonly IRepositoryAsync<AU_NGUOIDUNG> _repository;

        public AuNguoiDungService(IRepositoryAsync<AU_NGUOIDUNG> repository) : base(repository)
        {
            _repository = repository;
        }

        public AU_NGUOIDUNG FindUser(string username, string password)
        {
            return _repository.Queryable().FirstOrDefault(x =>
                x.USERNAME.Equals(username) && x.PASSWORD.Equals(password) && x.TRANGTHAI == 1);
        }
    }
}
