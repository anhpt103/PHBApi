namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B01
{
    public class B01BCQTDetailItem
    {
        public string RefID { get; set; }
        public string ReportItemName { get; set; }
        public string ReportItemCode { get; set; }
        public string ReportItemAlias { get; set; }
        public int ReportItemIndex { get; set; }
        public decimal Amount { get; set; }

        public string BudgetSourceID { get; set; }
        public string BudgetKindItemID { get; set; }
        public string BudgetSubKindItemID { get; set; }
    }
}
