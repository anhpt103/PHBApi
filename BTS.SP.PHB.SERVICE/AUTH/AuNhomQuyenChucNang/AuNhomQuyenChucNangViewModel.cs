using System;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.AUTH.AuNhomQuyenChucNang
{
    public class AuNhomQuyenChucNangViewModel : BaseEntity
    {
        public string MANHOMQUYEN { get; set; }
        public string MACHUCNANG { get; set; }
        public string TENCHUCNANG { get; set; }
        public string STATE { get; set; }
        public string SOTHUTU { get; set; }
        public bool XEM { get; set; }
        public bool THEM { get; set; }
        public bool SUA { get; set; }
        public bool XOA { get; set; }
        public bool DUYET { get; set; }

        public AuNhomQuyenChucNangViewModel()
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
