using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Entities;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungNhomQuyen
{
    public class AuNguoiDungNhomQuyenVm
    {
        public class ViewModel:DataInfoEntity
        {
            public string USERNAME { get; set; }
            public string MANHOMQUYEN { get; set; }
            public string TENNHOMQUYEN { get; set; }
        }
        public class ConfigModel
        {
            public string USERNAME { get; set; }
            public List<AU_NGUOIDUNG_NHOMQUYEN> LstAdd { get; set; }
            public List<AU_NGUOIDUNG_NHOMQUYEN> LstDelete { get; set; }
        }
    }
}
