using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B04;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107
{
    public interface IBc04BCTCTT107Service : IBaseService<ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107>
    {
        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(B04BCTCModel model, PHBContext context);
    }

    public class Bc04BCTCTT107Service : BaseService<ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107>, IBc04BCTCTT107Service
    {
        private readonly IRepositoryAsync<ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107> _repository;

        public Bc04BCTCTT107Service(IRepositoryAsync<ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107> repository) : base(repository)
        {
            _repository = repository;
        }

        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.BC04_BCTC_TT107s.Any(rp => rp.MA_DONVI == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpB04_TT107 = context.BC04_BCTC_TT107s.FirstOrDefault(rp => rp.MA_DONVI == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM == ReportYear);
                    rpB04_TT107.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.BC04_BCTC_TT107s.Remove(rpB04_TT107);

                    var exitstRpDetail = context.BC04_BCTC_TT107_DETAILSs.Any(rpD => rpD.BC04_BCTC_TT107_REFID == rpB04_TT107.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.BC04_BCTC_TT107_DETAILSs.Where(rpD => rpD.BC04_BCTC_TT107_REFID == rpB04_TT107.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.BC04_BCTC_TT107_DETAILSs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public string InsertData(B04BCTCModel model, PHBContext context)
        {
            if (model == null) return "B04BCTCModel model is null";

            var rpB02_BCTC = new ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107
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
                context.BC04_BCTC_TT107s.Add(rpB02_BCTC);

                List<BC04_BCTC_TT107_DETAILS> lstRpB04_BCTC_DETAIL = new List<BC04_BCTC_TT107_DETAILS>();

                foreach (var rpDt in model.B04BCTCDetail)
                {
                    var rpBCTC_DETAIL = new BC04_BCTC_TT107_DETAILS
                    {
                        BC04_BCTC_TT107_REFID = model.ReportHeader.RefID,
                        CHI_TIEU = rpDt.ReportItemName,
                        MA_SO = rpDt.ReportItemCode,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpB04_BCTC_DETAIL.Add(rpBCTC_DETAIL);
                }

                if (lstRpB04_BCTC_DETAIL.Count == 0) return "lstRpB04_BCTC_DETAIL trong Bc04BCTCTT107Service.InsertData có độ dài = 0";
                context.BC04_BCTC_TT107_DETAILSs.AddRange(lstRpB04_BCTC_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
