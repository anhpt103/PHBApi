﻿
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.AUTH.AuNguoiDungQuyen
{
    public class AuNguoiDungQuyenViewModel:BaseEntity
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

        public AuNguoiDungQuyenViewModel()
        {
            ID = 0;
            XEM = false;
            THEM = false;
            SUA = false;
            XOA = false;
            DUYET = false;
        }
    }
}
