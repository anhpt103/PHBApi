using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.B04_TT90;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B04TT90;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT.B04_TT90
{
    public interface IPhbB04_TT90Service : IBaseService<PHB_B04_TT90>
    {
        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(B04TT90Model model, PHBContext context);
    }
    public class B04_TT90Service : BaseService<PHB_B04_TT90>, IPhbB04_TT90Service
    {
        private readonly IRepositoryAsync<PHB_B04_TT90> _repository;
        public B04_TT90Service(IRepositoryAsync<PHB_B04_TT90> repository) : base(repository)
        {
            _repository = repository;
        }

        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.PHB_B04_TT90s.Any(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpB04_TT90 = context.PHB_B04_TT90s.FirstOrDefault(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
                    rpB04_TT90.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.PHB_B04_TT90s.Remove(rpB04_TT90);

                    var exitstRpDetail = context.PHB_B04_TT90_DETAILs.Any(rpD => rpD.PHB_B04_TT90_REFID == rpB04_TT90.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.PHB_B04_TT90_DETAILs.Where(rpD => rpD.PHB_B04_TT90_REFID == rpB04_TT90.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.PHB_B04_TT90_DETAILs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public string InsertData(B04TT90Model model, PHBContext context)
        {
            if (model == null) return "B04TT90Model model is null";

            var rpB04_TT90 = new PHB_B04_TT90
            {
                NAM_BC = model.ReportHeader.ReportYear,
                MA_QHNS = model.ReportHeader.CompanyID,
                NGAY_TAO = DateTime.Now,
                TRANG_THAI = 0,
                NGUOI_TAO = "ServiceMISA",
                REFID = model.ReportHeader.RefID,
                KY_BC = model.ReportHeader.ReportPeriod,
                TRANG_THAI_GUI = 1,
                MA_CHUONG = model.ReportHeader.BudgetChapterCode,
                ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
            };
            try
            {
                context.PHB_B04_TT90s.Add(rpB04_TT90);

                List<PHB_B04_TT90_DETAIL> lstRpB04_TT90_DETAIL = new List<PHB_B04_TT90_DETAIL>();

                foreach (var rpDt in model.B04TT90Detail)
                {
                    var rpB04_TT90_DETAIL = new PHB_B04_TT90_DETAIL
                    {
                        PHB_B04_TT90_REFID = model.ReportHeader.RefID,
                        TEN_CHI_TIEU = rpDt.ReportItemName,
                        STT_CHI_TIEU = rpDt.ReportItemAlias,
                        SAP_XEP = rpDt.ReportItemIndex,
                        MA_CHI_TIEU = rpDt.ReportItemCode,
                        TONGSOLIEU_BCQT = rpDt.EstimateAmount,
                        TONGSOLIEU_QT_DUOCDUYET = rpDt.ApprovedAmount,
                        INDAM = 0,
                        INNGHIENG = 0,
                        SOQUYETTOAN_DUOCDUYET = 0,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpB04_TT90_DETAIL.Add(rpB04_TT90_DETAIL);
                }

                if (lstRpB04_TT90_DETAIL.Count == 0) return "lstRpB04_TT90_DETAIL trong B04_TT90Service.InsertData có độ dài = 0";
                context.PHB_B04_TT90_DETAILs.AddRange(lstRpB04_TT90_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
