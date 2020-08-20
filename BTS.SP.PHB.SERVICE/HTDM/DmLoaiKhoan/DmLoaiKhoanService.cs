using System;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan
{
    public interface IDmLoaiKhoanService:IBaseService<PHB_DM_LOAIKHOAN>
    {
    }
    public class DmLoaiKhoanService : BaseService<PHB_DM_LOAIKHOAN>,IDmLoaiKhoanService
    {
        private readonly IRepositoryAsync<PHB_DM_LOAIKHOAN> _repository;

        public DmLoaiKhoanService(IRepositoryAsync<PHB_DM_LOAIKHOAN> repository) : base(repository)
        {
            _repository = repository;
            
        }
    }
}
