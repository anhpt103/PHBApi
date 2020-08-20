using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.SYS.SysDvqhns
{
    public class SysDvqhnsVm
    {
        public class ViewModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
        }

        public class ViewModel_Model
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_DBHC { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_DVQHNS = summary;
                TEN_DVQHNS = summary;
                MA_DVQHNS_CHA = summary;
                MA_CHUONG = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new SYS_DVQHNS();

                if (!string.IsNullOrEmpty(this.MA_DVQHNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DVQHNS),
                        Value = this.MA_DVQHNS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_DVQHNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_DVQHNS),
                        Value = this.TEN_DVQHNS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MA_DVQHNS_CHA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DVQHNS_CHA),
                        Value = this.MA_DVQHNS_CHA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MA_CHUONG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_CHUONG),
                        Value = this.MA_CHUONG,
                        Method = FilterMethod.EqualTo
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
                get { return ClassHelper.GetPropertyName(() => new SYS_DVQHNS().MA_DVQHNS); }
            }
        }
    }
}
