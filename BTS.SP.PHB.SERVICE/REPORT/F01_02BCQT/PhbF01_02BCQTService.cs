using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_02;
using BTS.SP.PHB.ENTITY.Rp.PHB_F01_02BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT
{
    public interface IPhbF01_02BCQTService : IBaseService<PHB_F01_02BCQT>
    {
        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(F01_02_P2BCQTModel model, PHBContext context);
    }
    public class PhbF01_02BCQTService : BaseService<PHB_F01_02BCQT>, IPhbF01_02BCQTService
    {
        private readonly IRepositoryAsync<PHB_F01_02BCQT> _repository;

        public PhbF01_02BCQTService(IRepositoryAsync<PHB_F01_02BCQT> repository) : base(repository)
        {
            _repository = repository;
        }

        //public async Task<Response<PHB_F01_02BCQTVm.ViewModel>> SumReport(string MACHUONG, int NAMBC, int KYBC, string[] DSDVQHNS)
        //{
        //    Response<PHB_F01_02BCQTVm.ViewModel> response = new Response<PHB_F01_02BCQTVm.ViewModel>();
        //    try
        //    {
        //        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (OracleCommand command = connection.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "STC_PHB_2017.PHB_F01_02BCQT_SUMREPORT";
        //                command.Parameters.Clear();
        //                OracleParameter p_machuong=command.Parameters.Add("MACHUONG", OracleDbType.NVarchar2);
        //                p_machuong.Direction = ParameterDirection.Input;
        //                p_machuong.Size = 50;
        //                p_machuong.Value = "421";

        //                OracleParameter p_nambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
        //                p_nambc.Direction = ParameterDirection.Input;
        //                p_nambc.Value = NAMBC;

        //                OracleParameter p_kybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
        //                p_kybc.Direction = ParameterDirection.Input;
        //                p_kybc.Value = KYBC;

        //                //OracleParameter p_dsdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.);
        //                //p_dsdvqhns.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
        //                //p_dsdvqhns.Direction = ParameterDirection.Input;
        //                //p_dsdvqhns.Size = DSDVQHNS.Length;
        //                //p_machuong.Value = DSDVQHNS;

        //                OracleParameter pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
        //                pCur.Direction =ParameterDirection.Output;

        //                await command.ExecuteNonQueryAsync();

        //                PHB_F01_02BCQTVm.ViewModel data = new PHB_F01_02BCQTVm.ViewModel();
        //                using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
        //                {
        //                    List<PHB_F01_02BCQTVm.DetailModel> lst = new List<PHB_F01_02BCQTVm.DetailModel>();
        //                    while (oracleDataReader.Read())
        //                    {
        //                        lst.Add(new PHB_F01_02BCQTVm.DetailModel()
        //                        {
        //                            LOAI_CHI_TIEU = Int32.Parse(oracleDataReader["LOAI_CHI_TIEU"].ToString()),
        //                            MA_LOAI = oracleDataReader["MA_LOAI"]?.ToString(),
        //                            STT_CHI_TIEU = oracleDataReader["STT_CHI_TIEU"]?.ToString(),
        //                            MA_CHI_TIEU = oracleDataReader["MA_CHI_TIEU"].ToString(),
        //                            TEN_CHI_TIEU = oracleDataReader["TEN_CHI_TIEU"].ToString(),
        //                            SAP_XEP = Int32.Parse(oracleDataReader["SAP_XEP"].ToString()),
        //                            QUYET_TOAN_NAM = Double.Parse(oracleDataReader["QUYET_TOAN_NAM"].ToString()),
        //                            DU_TOAN_DUOC_GIAO = Double.Parse(oracleDataReader["DU_TOAN_DUOC_GIAO"].ToString()),
        //                        });
        //                    }
        //                    data.DETAILS = lst;
        //                }
        //                response.Error = false;
        //                response.Data = data;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return response;
        //}

        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.PHB_F01_02BCQTs.Any(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpF01_02_P2 = context.PHB_F01_02BCQTs.FirstOrDefault(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
                    rpF01_02_P2.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.PHB_F01_02BCQTs.Remove(rpF01_02_P2);

                    var exitstRpDetail = context.PHB_F01_02BCQT_DETAILs.Any(rpD => rpD.PHB_F01_02BCQT_REFID == rpF01_02_P2.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.PHB_F01_02BCQT_DETAILs.Where(rpD => rpD.PHB_F01_02BCQT_REFID == rpF01_02_P2.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.PHB_F01_02BCQT_DETAILs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public string InsertData(F01_02_P2BCQTModel model, PHBContext context)
        {
            if (model == null) return "F01_02_P2BCQTModel model is null";

            var rpF01_02_P2 = new PHB_F01_02BCQT
            {
                NAM_BC = model.ReportHeader.ReportYear,
                MA_QHNS = model.ReportHeader.CompanyID,
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
                context.PHB_F01_02BCQTs.Add(rpF01_02_P2);

                List<PHB_F01_02BCQT_DETAIL> lstRpF01_02_P2_DETAIL = new List<PHB_F01_02BCQT_DETAIL>();

                foreach (var rpDt in model.F01_02_P2BCQTDetail)
                {
                    var rpF01_02_P2_DETAIL = new PHB_F01_02BCQT_DETAIL
                    {
                        PHB_F01_02BCQT_REFID = model.ReportHeader.RefID,
                        MA_LOAI = rpDt.BudgetKindItemID,
                        MA_KHOAN = rpDt.BudgetSubKindItemID,
                        GIA_TRI_LK = rpDt.AccAmount,
                        GIA_TRI_PS = rpDt.Amount,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpF01_02_P2_DETAIL.Add(rpF01_02_P2_DETAIL);
                }

                if (lstRpF01_02_P2_DETAIL.Count == 0) return "lstRpF01_02_P2_DETAIL trong PhbF01_02BCQTService.InsertData có độ dài = 0";
                context.PHB_F01_02BCQT_DETAILs.AddRange(lstRpF01_02_P2_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
