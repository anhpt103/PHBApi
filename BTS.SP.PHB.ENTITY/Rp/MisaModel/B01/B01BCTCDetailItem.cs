using System;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B01
{
    public class B01BCTCDetailItem
    {
        public Guid RefID { get; set; }
        public string ReportItemName { get; set; }
        public string ReportItemCode { get; set; }
        public string ReportItemAlias { get; set; }
        public int ReportItemIndex { get; set; }
        public string Description { get; set; }
        public decimal PrevAmount { get; set; }
        public decimal Amount { get; set; }
    }
}
