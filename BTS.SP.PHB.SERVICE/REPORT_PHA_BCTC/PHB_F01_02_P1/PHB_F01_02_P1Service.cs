using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_02;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.PHB_F01_02_P1
{
    public interface IPhbF01_02_P1Service : IBaseService<ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1>
    {
        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(F01_02_P1BCQTModel model, PHBContext context);
    }

    public class PhbF01_02_P1Service : BaseService<ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1>, IPhbF01_02_P1Service
    {
        private readonly IRepositoryAsync<ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1> _repository;

        public PhbF01_02_P1Service(IRepositoryAsync<ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1> repository) : base(repository)
        {
            _repository = repository;
        }

        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.PHB_F01_02_P1s.Any(rp => rp.MA_DONVI == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpB02 = context.PHB_F01_02_P1s.FirstOrDefault(rp => rp.MA_DONVI == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM == ReportYear);
                    rpB02.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.PHB_F01_02_P1s.Remove(rpB02);

                    var exitstRpDetail = context.PHB_F01_02_P1_DETAILs.Any(rpD => rpD.PHB_F01_02_P1_REFID == rpB02.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.PHB_F01_02_P1_DETAILs.Where(rpD => rpD.PHB_F01_02_P1_REFID == rpB02.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.PHB_F01_02_P1_DETAILs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public string InsertData(F01_02_P1BCQTModel model, PHBContext context)
        {
            if (model == null) return "F01_02_P1BCQTModel model is null";

            var rpF01_02_P1 = new ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1
            {
                NAM = model.ReportHeader.ReportYear,
                MA_DONVI = model.ReportHeader.CompanyID,
                NGAY_TAO = DateTime.Now,
                TRANG_THAI = 0,
                NGUOI_TAO = "ServiceMISA",
                REFID = model.ReportHeader.RefID,
                KY_BC = model.ReportHeader.ReportPeriod,
                TRANG_THAI_GUI = 1,
                ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
            };
            try
            {
                context.PHB_F01_02_P1s.Add(rpF01_02_P1);

                List<PHB_F01_02_P1_DETAIL> lstRpF01_02_P1_DETAIL = new List<PHB_F01_02_P1_DETAIL>();

                foreach (var rpDt in model.F01_02_P1BCQTDetail)
                {
                    var rpBCTC_DETAIL = new PHB_F01_02_P1_DETAIL
                    {
                        PHB_F01_02_P1_REFID = model.ReportHeader.RefID,
                        STT_SAPXEP = rpDt.ReportItemIndex,
                        CHI_TIEU = rpDt.ReportItemName,
                        STT = rpDt.ReportItemAlias,
                        MA_SO = rpDt.ReportItemCode,
                        GIA_TRI = rpDt.Amount,
                        MA_LOAI = rpDt.BudgetKindItemID,
                        MA_KHOAN = rpDt.BudgetSubKindItemID,
                        MA_NGUON = rpDt.BudgetSourceID,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpF01_02_P1_DETAIL.Add(rpBCTC_DETAIL);
                }

                if (lstRpF01_02_P1_DETAIL.Count == 0) return "lstRpF01_02_P1_DETAIL trong PhbF01_02_P1Service.InsertData có độ dài = 0";
                context.PHB_F01_02_P1_DETAILs.AddRange(lstRpF01_02_P1_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
