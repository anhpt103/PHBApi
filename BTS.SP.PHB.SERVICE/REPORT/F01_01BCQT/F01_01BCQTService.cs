using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.F01_01BCQT;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_01;
using BTS.SP.PHB.SERVICE.Models.F01_01BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_01BCQT
{
    public interface IF01_01BCQTService : IBaseService<PHB_F01_01BCQT>
    {
        Task<Response<F01_01BCQTVm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns);

        string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context);

        string InsertData(F01_01BCQTModel model, PHBContext context);
    }
    public class F01_01BCQTService : BaseService<PHB_F01_01BCQT>, IF01_01BCQTService
    {
        private readonly IRepositoryAsync<PHB_F01_01BCQT> _repository;
        public F01_01BCQTService(IRepositoryAsync<PHB_F01_01BCQT> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Response<F01_01BCQTVm.ViewModel>> SumReport(string username, int loaibc, bool chitiet, int nambc, int kybc, string lstdvqhns)
        {
            Response<F01_01BCQTVm.ViewModel> response = new Response<F01_01BCQTVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_F01_01BCQT_SUMREPORT";
                        command.Parameters.Clear();
                        var pMahuyen = command.Parameters.Add("USER_NAME", OracleDbType.NVarchar2);
                        pMahuyen.Direction = ParameterDirection.Input;
                        pMahuyen.Size = 50;
                        pMahuyen.Value = username;

                        var pKLoaibc = command.Parameters.Add("LOAIBC", OracleDbType.Int32);
                        pKLoaibc.Direction = ParameterDirection.Input;
                        pKLoaibc.Value = loaibc;

                        var pChiTiet = command.Parameters.Add("CHITIET", OracleDbType.Int32);
                        pChiTiet.Direction = ParameterDirection.Input;
                        pChiTiet.Value = Convert.ToInt32(chitiet);

                        var pNambc = command.Parameters.Add("NAMBC", OracleDbType.Int32);
                        pNambc.Direction = ParameterDirection.Input;
                        pNambc.Value = nambc;

                        var pKybc = command.Parameters.Add("KYBC", OracleDbType.Int32);
                        pKybc.Direction = ParameterDirection.Input;
                        pKybc.Value = kybc;

                        var pLstdvqhns = command.Parameters.Add("DSDVQHNS", OracleDbType.NVarchar2);
                        pLstdvqhns.Direction = ParameterDirection.Input;
                        pLstdvqhns.Size = 2000;
                        pLstdvqhns.Value = lstdvqhns;

                        var pCur = command.Parameters.Add("CUR", OracleDbType.RefCursor);
                        pCur.Direction = ParameterDirection.Output;
                        await command.ExecuteNonQueryAsync();

                        F01_01BCQTVm.ViewModel data = new F01_01BCQTVm.ViewModel();
                        using (OracleDataReader oracleDataReader = ((OracleRefCursor)pCur.Value).GetDataReader())
                        {
                            List<PHB_F01_01BCQT_DETAIL> lst = new List<PHB_F01_01BCQT_DETAIL>();
                            while (oracleDataReader.Read())
                            {
                                lst.Add(new PHB_F01_01BCQT_DETAIL()
                                {
                                    LOAI = int.Parse(oracleDataReader["LOAI"].ToString()),
                                    MA_LOAI = oracleDataReader["MA_LOAI"]?.ToString() ?? "",
                                    MA_KHOAN = oracleDataReader["MA_KHOAN"]?.ToString() ?? "",
                                    MA_MUC = oracleDataReader["MA_MUC"]?.ToString() ?? "",
                                    MA_TIEU_MUC = oracleDataReader["MA_TIEU_MUC"]?.ToString() ?? "",
                                    NOI_DUNG_CHI = oracleDataReader["NOI_DUNG_CHI"]?.ToString() ?? "",
                                    TONG_SO = decimal.Parse(oracleDataReader["TONG_SO"].ToString()),
                                    NSNN_TRONGNUOC = decimal.Parse(oracleDataReader["NSNN_TRONGNUOC"].ToString()),
                                    VIEN_TRO = decimal.Parse(oracleDataReader["VIEN_TRO"].ToString()),
                                    VAYNO_NUOCNGOAI = decimal.Parse(oracleDataReader["VAYNO_NUOCNGOAI"].ToString()),
                                    NP_DELAI = decimal.Parse(oracleDataReader["NP_DELAI"].ToString()),
                                    NHD_DELAI = decimal.Parse(oracleDataReader["NHD_DELAI"].ToString())
                                });
                            }
                            data.DETAIL = lst;
                        }
                        response.Error = false;
                        response.Data = data;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }

        public string IfExistsRpPeriodThenDelete(string CompanyID, int ReportPeriod, int ReportYear, PHBContext context)
        {
            var exitstRp = context.PHB_F01_01BCQTs.Any(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
            if (exitstRp)
            {
                try
                {
                    var rpF01_01 = context.PHB_F01_01BCQTs.FirstOrDefault(rp => rp.MA_QHNS == CompanyID && rp.KY_BC == ReportPeriod && rp.NAM_BC == ReportYear);
                    rpF01_01.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                    context.PHB_F01_01BCQTs.Remove(rpF01_01);

                    var exitstRpDetail = context.PHB_F01_01BCQT_DETAILs.Any(rpD => rpD.PHB_F01_01BCQT_REFID == rpF01_01.REFID);
                    if (exitstRpDetail)
                    {
                        var listRpExists = context.PHB_F01_01BCQT_DETAILs.Where(rpD => rpD.PHB_F01_01BCQT_REFID == rpF01_01.REFID);
                        foreach (var rpDel in listRpExists) rpDel.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                        context.PHB_F01_01BCQT_DETAILs.RemoveRange(listRpExists);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "";
        }

        public string InsertData(F01_01BCQTModel model, PHBContext context)
        {
            if (model == null) return "F01_01BCQTModel model is null";

            var rpF01_02_P1 = new PHB_F01_01BCQT
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
                context.PHB_F01_01BCQTs.Add(rpF01_02_P1);

                List<PHB_F01_01BCQT_DETAIL> lstRpF01_01_DETAIL = new List<PHB_F01_01BCQT_DETAIL>();

                foreach (var rpDt in model.F01_01BCQTDetail)
                {
                    var rpBCTC_DETAIL = new PHB_F01_01BCQT_DETAIL
                    {
                        PHB_F01_01BCQT_REFID = model.ReportHeader.RefID,
                        NOI_DUNG_CHI = rpDt.BudgetSubItemName,
                        MA_LOAI = rpDt.BudgetKindItemID,
                        MA_KHOAN = rpDt.BudgetSubKindItemID,
                        MA_MUC = rpDt.BudgetItemID,
                        MA_TIEU_MUC = rpDt.BudgetSubItemID,
                        NSNN_TRONGNUOC = rpDt.BudgetSourceAmount,
                        VIEN_TRO = rpDt.AidAmount,
                        VAYNO_NUOCNGOAI = rpDt.DebitAmount,
                        NP_DELAI = rpDt.DeductAmount,
                        NHD_DELAI = rpDt.OtherAmount,
                        ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added
                    };
                    lstRpF01_01_DETAIL.Add(rpBCTC_DETAIL);
                }

                if (lstRpF01_01_DETAIL.Count == 0) return "lstRpF01_01_DETAIL trong F01_01BCQTService.InsertData có độ dài = 0";
                context.PHB_F01_01BCQT_DETAILs.AddRange(lstRpF01_01_DETAIL);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
