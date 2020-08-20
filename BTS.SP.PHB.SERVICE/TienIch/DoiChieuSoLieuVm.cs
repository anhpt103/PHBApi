using BTS.SP.API.ENTITY.Models.Nv.PHC;
using System;
using System.Collections.Generic;
using BTS.SP.PHB.SERVICE.BuildQuery;
using BTS.SP.PHB.SERVICE.BuildQuery.Implimentations;
using BTS.SP.PHB.SERVICE.BuildQuery.Types;
using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.TOOLS;

namespace BTS.SP.PHB.SERVICE.TienIch
{
    public class DoiChieuSoLieuVm
    {
        public class Search : IDataSearch
        {
            public string MA_DBHC { get; set; }
            public string DVQHNS { get; set; }
            public DateTime TUNGAY { get; set; }
            public DateTime DENNGAY { get; set; }
            public string LOAIDULIEU { get; set; }
            public string KY_BC { get; set; }
            public string NAM { get; set; }
            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new PHC_DOICHIEUSOLIEUHEADER().DVQHNS);
                }
            }

            public List<PHB.SERVICE.BuildQuery.IQueryFilter> GetFilters()
            {
                var result = new List<PHB.SERVICE.BuildQuery.IQueryFilter>();
                var refObj = new PHC_DOICHIEUSOLIEUHEADER();

                if (!string.IsNullOrEmpty(this.MA_DBHC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.DVQHNS),
                        Value = this.MA_DBHC,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.LOAIDULIEU))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.LOAIDULIEU),
                        Value = this.LOAIDULIEU,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.NAM))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.NAM),
                        Value = this.NAM,
                        Method = FilterMethod.Like
                    });
                }
                return result;
            }

            public List<PHB.SERVICE.BuildQuery.IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public void LoadGeneralParam(string summary)
            {
                DVQHNS = summary;
                LOAIDULIEU = summary;
                NAM = summary;
            }

            List<TOOLS.BuildQuery.IQueryFilter> IDataSearch.GetFilters()
            {
                throw new NotImplementedException();
            }

            List<TOOLS.BuildQuery.IQueryFilter> IDataSearch.GetQuickFilters()
            {
                throw new NotImplementedException();
            }
        }
        public class ConvertPeriod
        {
            public DateTime TUNGAY_HIEULUC { get; set; }
            public DateTime DENNGAY_HIEULUC { get; set; }
        }

        public class Data_Header_XML
        {
            public string MADVQHNS_FILE { get; set; }
            public int KY_FILE { get; set; }
            public int NAM_FILE { get; set; }
        }

        public class DoiChieuSoLieuDto
        {
            public DoiChieuSoLieuDto()
            {
                DataDetails = new List<DoiChieuSoLieuDetails>();
            }
            public string DVQHNS { get; set; }
            public string LOAIDULIEU { get; set; }
            public decimal SOTIEN { get; set; }
            public DateTime NGAYPHATSINH { get; set; }
            public DateTime TUNGAY_HIEULUC { get; set; }
            public DateTime DENNGAY_HIEULUC { get; set; }
            public int KY { get; set; }
            public int NAM { get; set; }
            public string TENFILE { get; set; }
            public string DUONGDAN { get; set; }
            public List<DoiChieuSoLieuDetails> DataDetails { get; set; }
        }

        public class DoiChieuSoLieuDetails
        {
            public string DVQHNS { get; set; }
            public string LOAIDULIEU { get; set; }
            public string MAQUY { get; set; }
            public string MATAIKHOAN { get; set; }
            public int CAP { get; set; }
            public string DBHC { get; set; }
            public string CHUONG { get; set; }
            public string LOAI { get; set; }
            public string KHOAN { get; set; }
            public string MUC { get; set; }
            public string TIEUMUC { get; set; }
            public string NHOM { get; set; }
            public string TIEUNHOM { get; set; }
            public string CTMT { get; set; }
            public string KBNN { get; set; }
            public string MANGUONVON { get; set; }
            public string LOAICHUNGTU { get; set; }
            public decimal SOTIEN { get; set; }
            public DateTime NGAYPHATSINH { get; set; }
            public DateTime TUNGAY_HIEULUC { get; set; }
            public DateTime DENNGAY_HIEULUC { get; set; }
            public int KY { get; set; }
            public int NAM { get; set; }
        }
        public class ViewModelB03A_X
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public string LOAIDULIEU { get; set; }
        }
        public class ViewModelB03B_X
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string LOAIDULIEU { get; set; }
        }
    }
}
