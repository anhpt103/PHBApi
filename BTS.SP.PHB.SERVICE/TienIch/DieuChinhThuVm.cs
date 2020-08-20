using BTS.SP.PHB.ENTITY.PHC;
using BTS.SP.PHB.SERVICE.BuildQuery;
using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHC.SERVICE.TienIch
{
    public class DieuChinhThuVm
    {
        public class Search : IDataSearch
        {
            public DateTime? TUNGAY_HL { get; set; }
            public DateTime? DENNGAY_HL { get; set; }
            public int ISNO { get; set; }
            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new PHC_CHUNGTUHEADER().MA_CHUNGTU);
                }
            }
            public List<PHB.SERVICE.BuildQuery.IQueryFilter> GetFilters()
            {
                var result = new List<PHB.SERVICE.BuildQuery.IQueryFilter>();
                var refObj = new PHC_CHUNGTUHEADER();

                return result;
            }
            public List<PHB.SERVICE.BuildQuery.IQueryFilter> GetQuickFilters()
            {
                return null;
            }
            public void LoadGeneralParam(string summary)
            {
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

        public class InputParam
        {
            public DateTime? TUNGAY_HL { get; set; }
            public DateTime? DENNGAY_HL { get; set; }
            public int ISNO { get; set; }
        }

        public class DTO
        {
            public string SO_CHUNGTU { get; set; }
            public string MA_NGHIEPVU { get; set; }
            public string MACTMT { get; set; }
            public string TENCTMT { get; set; }
            public Nullable<DateTime> NGAY_CT { get; set; }
            public string DIENGIAI { get; set; }
            public string TAIKHOAN_NO { get; set; }
            public string NO_NB { get; set; }
            public string TAIKHOAN_CO { get; set; }
            public string CO_NB { get; set; }
            public string NGUONNO { get; set; }
        }
    }
}
