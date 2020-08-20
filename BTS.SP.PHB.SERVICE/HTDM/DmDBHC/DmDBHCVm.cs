using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmDBHC
{
    public class DmDBHCVm
    {
        public class ViewModel
        {
            public string MA_DBHC { get; set; }
            public string TEN_DBHC { get; set; }
        }

        public class Search : IDataSearch
        {
            public string MA_DBHC { get; set; }
            public string TEN_DBHC { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_DBHC = summary;
                TEN_DBHC = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new DM_DBHC();

                if (!string.IsNullOrEmpty(this.MA_DBHC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DBHC),
                        Value = this.MA_DBHC,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_DBHC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_DBHC),
                        Value = this.TEN_DBHC,
                        Method = FilterMethod.Like
                    });
                }
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public string DefaultOrder
            {
                get { return "MA_DBHC"; }
            }
        }
    }
}
