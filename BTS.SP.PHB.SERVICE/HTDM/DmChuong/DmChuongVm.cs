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

namespace BTS.SP.PHB.SERVICE.HTDM.DmChuong
{
    public class DmChuongVm
    {
        public class ViewModel
        {
            public string MA_CHUONG { get; set; }
            public string TEN_CHUONG { get; set; }
            public string Value { get; set; }
            public string Text { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_CHUONG { get; set; }
            public string TEN_CHUONG { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_CHUONG = summary;
                TEN_CHUONG = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new DM_CHUONG();

                if (!string.IsNullOrEmpty(this.MA_CHUONG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_CHUONG),
                        Value = this.MA_CHUONG,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_CHUONG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_CHUONG),
                        Value = this.TEN_CHUONG,
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
                get { return "MA_CHUONG"; }
            }
        }
    }
}
