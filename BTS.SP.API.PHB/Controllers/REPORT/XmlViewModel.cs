using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    public class XmlViewModel
    {
        public class ReportHeader
        {
            public string AccountSystem { get; set; }
            public int? BudgetChapterID { get; set; }
            public string CompanyID { get; set; }
            public string CompanyName { get; set; }
            public string ExportDate { get; set; }
            public int? ExportVersion { get; set; }
            public int? ParticularID { get; set; }
            public string ProductID { get; set; }
            public string RefID { get; set; }
            public string ReportID { get; set; }
            public int ReportPeriod { get; set; }
            public int ReportYear { get; set; }
            public string Version { get; set; }
        }
        public class B01BCQTDetailItem
        {
            public decimal? Amount { get; set; }
            public int? BudgetKindItemID { get; set; }
            public int? BudgetSourceID { get; set; }
            public int? BudgetSubKindItemID { get; set; }
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public int ReportItemCode { get; set; }
            public int ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class F0101BCQTDetailItem
        {
            public decimal? BudgetSourceAmount { get; set; }
            public decimal? AidAmount { get; set; }
            public decimal? DebitAmount { get; set; }
            public decimal? DeductAmount { get; set; }
            public decimal? OtherAmount { get; set; }

            public int? BudgetSourceID { get; set; }

            public int? BudgetKindItemID { get; set; }
            public int? BudgetKindItemName { get; set; }

            public int? BudgetSubKindItemID { get; set; }
            public string BudgetSubKindItemName { get; set; }

            public int? BudgetItemID { get; set; }
            public string BudgetItemName { get; set; }

            public int? BudgetSubItemID { get; set; }
            public string BudgetSubItemName { get; set; }

            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public int ReportItemCode { get; set; }
            public int ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class F0102P1BCQTDetailItem
        {
            public int? ProjectID { get; set; }
            public decimal? TotalAmountApproved { get; set; }
            public decimal? AccAmount { get; set; }
            public decimal? Amount { get; set; }
            public string BudgetKindItemID { get; set; }
            public string BudgetSourceID { get; set; }
            public string BudgetSubKindItemID { get; set; }
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public int ReportItemCode { get; set; }
            public int ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class ProjectItem
        {
            public int? ProjectID { get; set; }
            public string ProjectName { get; set; }
            public string ProgramName { get; set; }
            public int? ParentID { get; set; }
            public int? ObjectType { get; set; }
            public bool? IsDetail { get; set; }
        }
        public class B03TT90DetailItem
        {
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public int ReportItemCode { get; set; }
            public int ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
            public decimal EstimateAmount { get; set; }
            public decimal ApprovedAmount { get; set; }
            public decimal OtherAmount { get; set; }
        }

        public class B04TT90DetailItem
        {
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public int ReportItemCode { get; set; }
            public int ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
            public decimal EstimateAmount { get; set; }
            public decimal ApprovedAmount { get; set; }
            public decimal OtherAmount { get; set; }
        }
        public class InsertObj
        {
            public ReportHeader ReportHeader { get; set; }
            public List<B01BCQTDetailItem> B01BCQTDetail { get; set; }
            public List<F0101BCQTDetailItem> F0101BCQTDetail { get; set; }
            public List<F0102P1BCQTDetailItem> F0102BCQTP1Detail { get; set; }
            public List<B03TT90DetailItem> B03TT90Detail { get; set; }
            public List<B04TT90DetailItem> B04TT90Detail { get; set; }
            public List<ProjectItem> Project { get; set; }
            public string xmlns { get; set; }
        }

    }
}