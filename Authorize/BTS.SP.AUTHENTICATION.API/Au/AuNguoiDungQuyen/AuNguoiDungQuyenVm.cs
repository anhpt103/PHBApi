using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungQuyen
{
    public class AuNguoiDungQuyenVm
    {
        public class ViewModel:DataInfoEntity
        {
            public string PHANHE { get; set; }
            public string USERNAME { get; set; }
            public string MACHUCNANG { get; set; }
            public string TENCHUCNANG { get; set; }
            public string SOTHUTU { get; set; }
            public bool XEM { get; set; }
            public bool THEM { get; set; }
            public bool SUA { get; set; }
            public bool XOA { get; set; }
            public bool DUYET { get; set; }

            public ViewModel()
            {
                //ID = 0;
                XEM = false;
                THEM = false;
                SUA = false;
                XOA = false;
                DUYET = false;
            }
        }

        public class ConfigModel
        {
            public string USERNAME { get; set; }
            public List<ViewModel> LstAdd { get; set; }
            public List<ViewModel> LstEdit { get; set; }
            public List<ViewModel> LstDelete { get; set; }
        }
    }

}
