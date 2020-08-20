using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS
{
    public class DmDVQHNSVm
    {
        public class ExtendModel : PHB_DM_DVQHNS
        {
            public string MA_DBHC { get; set; }
        }
        public class ViewModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }

            public string MA_CHUONG { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_QHNS = summary;
                TEN_QHNS = summary;
                MA_CHUONG = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_DVQHNS();

                if (!string.IsNullOrEmpty(this.MA_QHNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_QHNS),
                        Value = this.MA_QHNS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_QHNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_QHNS),
                        Value = this.TEN_QHNS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MA_CHUONG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_CHUONG),
                        Value = this.MA_CHUONG,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_DVQHNS().MA_QHNS); }
            }
        }
    }
}
