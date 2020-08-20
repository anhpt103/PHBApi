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

namespace BTS.SP.PHB.SERVICE.HTDM.DmBaoCao
{
    public class DmBaoCaoVm
    {
        public class ViewModel
        {
            public string MA_BAO_CAO { get; set; }
            public string TEN_BAO_CAO { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_BAO_CAO { get; set; }
            public string TEN_BAO_CAO { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_BAO_CAO = summary;
                TEN_BAO_CAO = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_BAOCAO();

                if (!string.IsNullOrEmpty(this.MA_BAO_CAO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_BAO_CAO),
                        Value = this.MA_BAO_CAO,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_BAO_CAO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_BAO_CAO),
                        Value = this.TEN_BAO_CAO,
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
                get { return "MA_BAO_CAO"; }
            }
        }
    }
}
