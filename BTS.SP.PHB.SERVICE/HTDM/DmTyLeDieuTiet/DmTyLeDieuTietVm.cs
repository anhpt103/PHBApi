using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTyLeDieuTiet
{
    public class DmTyLeDieuTietVm 
    {
        public class TCS_TYLEDIEUTIET
        {
            public string MA { get; set; }
            public string TYLE_DIEUTIET { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA { get; set; }
            public string TYLE_DIEUTIET { get; set; }
            public string TRANG_THAI { get; set; }
            public string StrKey { get; set; }
            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new PHB_DM_TYLEDIEUTIET().MA);
                }
            }
            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new TCS_TYLEDIEUTIET();
                if (!string.IsNullOrEmpty(this.MA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA),
                        Value = this.MA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TYLE_DIEUTIET))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TYLE_DIEUTIET),
                        Value = this.TYLE_DIEUTIET,
                        Method = FilterMethod.Like
                    });
                }
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public void LoadGeneralParam(string summary)
            {
                MA = summary;
                TYLE_DIEUTIET = summary;
            }
        }
    }
}
