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

namespace BTS.SP.PHB.SERVICE.HTDM.DmTKKhoBac
{
    public class DmTKKhoBacVm
    {
        public class ViewModel
        {
            public string MA { get; set; }
            public string TEN { get; set; }
        }

        public class Search : IDataSearch
        {
            public string MA { get; set; }
            public string TEN { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA = summary;
                TEN = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new DM_TKKHOBAC();

                if (!string.IsNullOrEmpty(this.MA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DBHC),
                        Value = this.MA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN),
                        Value = this.TEN,
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
                get { return "MA"; }
            }
        }
    }

}

