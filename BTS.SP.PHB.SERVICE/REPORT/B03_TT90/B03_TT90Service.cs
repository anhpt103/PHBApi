using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B03TT90;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT.B03_TT90
{
    public interface IPhbB03_TT90Service : IBaseService<PHB_B03_TT90>
    {
        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(B03TT90Model model, PHBContext context);
    }
    public class B03_TT90Service : BaseService<PHB_B03_TT90>, IPhbB03_TT90Service
    {
        private readonly IRepositoryAsync<PHB_B03_TT90> _repository;
        public B03_TT90Service(IRepositoryAsync<PHB_B03_TT90> repository) : base(repository)
        {
            _repository = repository;
        }

        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.PHB_B03_TT90s.Any(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpB03_TT90 = context.PHB_B03_TT90s.FirstOrDefault(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
                    rpB03_TT90.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.PHB_B03_TT90s.Remove(rpB03_TT90);

                    var exitstRpDetail = context.PHB_B03_TT90_DETAILs.Any(rpD => rpD.PHB_B03_TT90_REFID == rpB03_TT90.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.PHB_B03_TT90_DETAILs.Where(rpD => rpD.PHB_B03_TT90_REFID == rpB03_TT90.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.PHB_B03_TT90_DETAILs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            return "";
        }

        public string InsertData(B03TT90Model model, PHBContext context)
        {
            if (model == null) return "B03TT90Model model is null";

            var rpB03_TT90_BCTC = new PHB_B03_TT90
            {
                NAM_BC = model.ReportHeader.ReportYear,
                MA_QHNS = model.ReportHeader.CompanyID,
                NGAY_TAO = DateTime.Now,
                TRANG_THAI = 0,
                NGUOI_TAO = "ServiceMISA",
                REFID = model.ReportHeader.RefID,
                KY_BC = model.ReportHeader.ReportPeriod,
                TRANG_THAI_GUI = 1,
                MA_CHUONG = model.ReportHeader.BudgetChapterID,
                ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
            };
            try
            {
                context.PHB_B03_TT90s.Add(rpB03_TT90_BCTC);

                List<PHB_B03_TT90_DETAIL> lstRpB03_TT90_DETAIL = new List<PHB_B03_TT90_DETAIL>();

                foreach (var rpDt in model.B03TT90Detail)
                {
                    var rpBCTC_DETAIL = new PHB_B03_TT90_DETAIL
                    {
                        PHB_B03_TT90_REFID = model.ReportHeader.RefID,
                        SAP_XEP = rpDt.ReportItemIndex,
                        TEN_CHI_TIEU = rpDt.ReportItemName,
                        STT_CHI_TIEU = rpDt.ReportItemAlias,
                        MA_CHI_TIEU = rpDt.ReportItemCode,
                        DU_TOAN_NAM = rpDt.EstimateAmount,
                        UOC_THUC_HIEN = rpDt.ApprovedAmount,
                        UOC_THUC_HIEN_DU_TOAN = rpDt.OtherAmount,
                        INDAM = 0,
                        INNGHIENG = 0,
                        UOC_THUC_HIEN_CUNG_KY = 0,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpB03_TT90_DETAIL.Add(rpBCTC_DETAIL);
                }

                if (lstRpB03_TT90_DETAIL.Count == 0) return "lstRpB03_TT90_DETAIL trong B03_TT90Service.InsertData có độ dài = 0";
                context.PHB_B03_TT90_DETAILs.AddRange(lstRpB03_TT90_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
