using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Auth;

namespace BTS.SP.PHB.SERVICE.AUTH.AuNguoiDungNhomQuyen
{
    public class AuNguoiDungNhomQuyenConfigModel
    {
        public string USERNAME { get; set; }
        public List<AU_NGUOIDUNG_NHOMQUYEN> LstAdd { get; set; }
        public List<AU_NGUOIDUNG_NHOMQUYEN> LstDelete { get; set; }
    }
}
