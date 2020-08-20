using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
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
            public int?ReportPeriod { get; set; }
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
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class B02BCQTDetailItem
        {
            public decimal? PreviousPeriodAmount { get; set; }
            public decimal? CurrentPeriodAmount { get; set; }
            public int? BudgetKindItemID { get; set; }
            public int? BudgetSourceID { get; set; }
            public int? BudgetSubKindItemID { get; set; }
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class B03BCQTDetailItem
        {
            public decimal? PrevAmount { get; set; }
            public decimal? Amount { get; set; }
            public int? BudgetKindItemID { get; set; }
            public int? BudgetSourceID { get; set; }
            public int? BudgetSubKindItemID { get; set; }
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class B04BCQTDetailItem
        {
            public string ReportItemName { get; set; }
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ContentString { get; set; }
            public string GroupType { get; set; }
            public decimal? Amount1 { get; set; }
            public decimal? Amount2 { get; set; }
            public decimal? Amount3 { get; set; }
            public decimal? Amount4 { get; set; }
            public decimal? Amount5 { get; set; }
            public decimal? Amount6 { get; set; }
            public decimal? Amount7 { get; set; }
            public decimal? Amount8 { get; set; }
            public decimal? Amount9 { get; set; }
            public decimal? Amount10 { get; set; }
            public decimal? Amount11 { get; set; }
            public decimal? Amount12 { get; set; }
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
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
        }
        public class B02BCTCDetail
        {
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemName { get; set; }
            public int? ReportItemIndex { get; set; }
            public string ReportItemCode { get; set; }
            public string ReportItemDescription { get; set; }
            public int?PreviousPeriodAmount { get; set; }
            public int?CurrentPeriodAmount { get; set; }
        }
        public class B03BCQTDetailReceipt
        {
            public string RefID { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
            public decimal? TotalReceiptAmount { get; set; }
            public decimal?PaymentAmount { get; set; }
            public decimal? DeductAmount { get; set; }
            public string Description { get; set; }
            public string ReportItemAlias { get; set; }
            public decimal? BudgetExpenseType { get; set; }
            public string DetailID { get; set; }
            public bool IsBold { get; set; }
            public bool IsItalic { get; set; }
            public int?StateRow { get; set; }
            public int?ReportItemIndexRoot { get; set; }
        }

        public class B03BCQTDetailUseSource
        {
            public string RefID { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
            public decimal? TotalAmount { get; set; }
            public decimal? BudgetSourceAmount { get; set; }
            public decimal? DeductAmount { get; set; }
            public decimal? ServiceAmount { get; set; }
            public decimal? OtherAmount { get; set; }
            public string ReportItemAlias { get; set; }
            public string DetailID { get; set; }
        }
        public class B03BCQTDetailGeneral
        {
            public string RefID { get; set; }
            public string Code { get; set; }
            public string Value { get; set; }
            public string DetailID { get; set; }
        }
        public class B03BCQTDetailBudget
        {
            public string RefID { get; set; }
            public int?Type { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemName { get; set; }
            public decimal? Amount { get; set; }
            public decimal? PersonNumber { get; set; }
            public string ReportItemContent { get; set; }
            public string DetailID { get; set; }
            public int?IsBold { get; set; }
            public int?IsItalic { get; set; }
            public int?ReportItemIndexRoot { get; set; }
        }
        public class F0102BCQTP1Detail
        {
            public string RefID { get; set; }
            public int?ProjectID { get; set; }
            public int?MethodDistributeID { get; set; }
            public int?BudgetSourceID { get; set; }
            public int?BudgetKindItemID { get; set; }
            public int?BudgetSubKindItemID { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemName { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemCode { get; set; }
            public decimal? TotalAmountApproved { get; set; }
            public decimal? Amount { get; set; }
            public decimal? AccAmount { get; set; }
        }

        public class B05BCTCDetailGeneral
        {
            public string RefID { get; set; }
            public string Code { get; set; }
            public string Value { get; set; }
        }

        public class B05BCTCDetailBudget
        {
            public string RefID { get; set; }
            public int?Type { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemName { get; set; }
            public int?ReportItemIndex { get; set; }
            public decimal? Amount1 { get; set; }
            public decimal? Amount2 { get; set; }
        }

        public class B03bBCTCDetail
        {
            public string RefID { get; set; }
            public string ReportItemName { get; set; }
            public string ReportItemCode { get; set; }
            public string ReportItemAlias { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemDescription { get; set; }
            public decimal?PrevAmount { get; set; }
            public decimal? Amount { get; set; }
        }

        public class B01BSTTDetail
        {
            public string RefID { get; set; }
            public string ReportItemName { get; set; }
            public string ReportItemCode { get; set; }
            public string ReportItemAlias { get; set; }
            public int?ReportItemIndex { get; set; }
            public decimal?Amount { get; set; }
            public decimal?MediateUnit2Amount { get; set; }
            public decimal?MediateUnit1Amount { get; set; }
            public decimal?EstimateUnit1Amount { get; set; }
            public decimal?EstimateUnitOut1Amount { get; set; }
            public decimal?OherEstimateUnitOut1Amount { get; set; }
            public decimal?NationalOutAmount { get; set; }
        }

        public class B01BSTTDetailP2
        {
            public string RefID { get; set; }
            public string ReportItemName { get; set; }
            public string ReportItemCode { get; set; }
            public string ReportItemAlias { get; set; }
            public int?ReportItemIndex { get; set; }
            public decimal?Amount { get; set; }
        }

        public class B04BCTCDetail
        {
            public string RefID { get; set; }
            public string ReportItemName { get; set; }
            public int?ReportItemIndex { get; set; }
            public string ReportItemCode { get; set; }
            public string ContentString { get; set; }
            public string GroupType { get; set; }
            public decimal? Amount1 { get; set; }
            public decimal? Amount2 { get; set; }
            public decimal? Amount3 { get; set; }
            public decimal?Amount4 { get; set; }
            public decimal? Amount5 { get; set; }
            public decimal?Amount6 { get; set; }
            public decimal?Amount7 { get; set; }
            public decimal?Amount8 { get; set; }
            public decimal?Amount9 { get; set; }
            public decimal?Amount10 { get; set; }
            public decimal?Amount11 { get; set; }
            public decimal? Amount12 { get; set; }
        }

        public class B04TT90Detail
        {
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemName { get; set; }
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public decimal?EstimateAmount { get; set; }
            public decimal?ApprovedAmount { get; set; }
        }

        public class B03TT90Detail
        {
            public string RefID { get; set; }
            public string ReportItemAlias { get; set; }
            public string ReportItemName { get; set; }
            public string ReportItemCode { get; set; }
            public int?ReportItemIndex { get; set; }
            public decimal?EstimateAmount { get; set; }
            public decimal?ApprovedAmount { get; set; }
            public decimal?OtherAmount { get; set; }
        }
        public class F0101BCQTDetail
        {
            public string RefID { get; set; }
            public int? BudgetSourceID { get; set; }
            public int? ExpenseType { get; set; }
            public int? BudgetKindItemID { get; set; }
            public string BudgetKindItemName { get; set; }
            public int? BudgetSubKindItemID { get; set; }
            public string BudgetSubKindItemName { get; set; }
            public int? BudgetItemID { get; set; }
            public string BudgetItemName { get; set; }
            public int? BudgetSubItemID { get; set; }
            public string BudgetSubItemName { get; set; }
            public decimal? BudgetSourceAmount { get; set; }
            public decimal? AidAmount { get; set; }
            public decimal? DebitAmount { get; set; }
            public decimal? DeductAmount { get; set; }
            public decimal? OtherAmount { get; set; }
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
        public class InsertObj
        {
            public ReportHeader ReportHeader { get; set; }
            public List<B01BCQTDetailItem> B01BCQTDetail { get; set; }
            public List<B02BCQTDetailItem> B02BCTCDetail { get; set; }
            public List<B03BCQTDetailItem> B03bBCTCDetail { get; set; }
            public List<B04BCQTDetailItem> B04BCTCDetail { get; set; }
            public List<F0102P1BCQTDetailItem> F0102BCQTP1Detail { get; set; }
            public List<ProjectItem> Project { get; set; }
            public string xmlns { get; set; }
        }

        public class Company
        {
            public string CompanyID { get; set; }
            public string CompanyName { get; set; }
            public string CompanyAddress { get; set; }
            public int?EstimateGrade { get; set; }
            public string Telephone { get; set; }
            public string Fax { get; set; }
            public bool InActive { get; set; }
            public int?ParticularID { get; set; }
        }

        public class XmlTotalObj
        {
            public List<ReportHeader> ReportHeader { get; set; }
            public Company Company { get; set; }
            public List<ProjectItem> Project { get; set; }
            public List<B01BCQTDetailItem> B01BCQTDetail { get; set; }
            public List<B02BCQTDetailItem> B02BCTCDetail { get; set; }
            public List<B03BCQTDetailItem> B03bBCTCDetail { get; set; }
            public List<B04BCQTDetailItem> B04BCTCDetail { get; set; }
            public List<F0102P1BCQTDetailItem> F0102BCQTP1Detail { get; set; }
            public List<B03BCQTDetailGeneral> B03BCQTDetailGeneral { get; set; }
            public List<B03BCQTDetailBudget> B03BCQTDetailBudget { get; set; }
            public List<B03BCQTDetailReceipt> B03BCQTDetailReceipt { get; set; }
            public List<B03BCQTDetailUseSource> B03BCQTDetailUseSource { get; set; }
            public List<B05BCTCDetailGeneral> B05BCTCDetailGeneral { get; set; }
            public List<B05BCTCDetailBudget> B05BCTCDetailBudget { get; set; }
            public List<B01BSTTDetail> B01BSTTDetail { get; set; }
            public List<B01BSTTDetailP2> B01BSTTDetailP2 { get; set; }
            public List<B04TT90Detail> B04TT90Detail { get; set; }
            public List<B03TT90Detail> B03TT90Detail { get; set; }
            public List<F0101BCQTDetail> F0101BCQTDetail { get; set; }
        }
    }
}