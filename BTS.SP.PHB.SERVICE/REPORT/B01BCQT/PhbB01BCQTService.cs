using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B01;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT.B01BCQT
{
    public interface IPhbB01BCQTService : IBaseService<PHB_B01BCQT>
    {
        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(B01BCQTModel model, PHBContext context);
    }
    public class PhbB01BCQTService : BaseService<PHB_B01BCQT>, IPhbB01BCQTService
    {
        private readonly IRepositoryAsync<PHB_B01BCQT> _repository;

        public PhbB01BCQTService(IRepositoryAsync<PHB_B01BCQT> repository) : base(repository)
        {
            _repository = repository;
        }


        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.PHB_B01BCQTs.Any(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpB01_BCQT = context.PHB_B01BCQTs.FirstOrDefault(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
                    rpB01_BCQT.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.PHB_B01BCQTs.Remove(rpB01_BCQT);

                    var exitstRpDetail = context.PHB_B01BCQT_DETAILs.Any(rpD => rpD.PHB_B01BCQT_REFID == rpB01_BCQT.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.PHB_B01BCQT_DETAILs.Where(rpD => rpD.PHB_B01BCQT_REFID == rpB01_BCQT.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.PHB_B01BCQT_DETAILs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public string InsertData(B01BCQTModel model, PHBContext context)
        {
            if (model == null) return "B01BCQTModel model is null";

            var rpB01_BCQT = new PHB_B01BCQT
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
                context.PHB_B01BCQTs.Add(rpB01_BCQT);

                List<PHB_B01BCQT_DETAIL> lstRpB01_BCQT_DETAIL = new List<PHB_B01BCQT_DETAIL>();

                foreach (var rpDt in model.B01BCQTDetail)
                {
                    var rpB01_BCQT_DETAIL = new PHB_B01BCQT_DETAIL
                    {
                        PHB_B01BCQT_REFID = model.ReportHeader.RefID,
                        TEN_CHI_TIEU = rpDt.ReportItemName,
                        MA_CHI_TIEU = rpDt.ReportItemCode,
                        STT_CHI_TIEU = rpDt.ReportItemAlias,
                        SAP_XEP = rpDt.ReportItemIndex,
                        MA_SO = rpDt.ReportItemCode,
                        GIA_TRI = rpDt.Amount,
                        MA_LOAI = rpDt.BudgetKindItemID,
                        MA_KHOAN = rpDt.BudgetSubKindItemID,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpB01_BCQT_DETAIL.Add(rpB01_BCQT_DETAIL);
                }

                if (lstRpB01_BCQT_DETAIL.Count == 0) return "lstRpB01_BCQT_DETAIL trong PhbB01BCQTService.InsertData có độ dài = 0";
                context.PHB_B01BCQT_DETAILs.AddRange(lstRpB01_BCQT_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
