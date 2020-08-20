using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;

using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;
using BTS.SP.TOOLS;

namespace BTS.SP.PHB.SERVICE.HTDM.DmChiTieuBaoCao
{
    public class DM_CHITIEU_BAOCAOVm
    {
        public class ViewModel
        {
            public string MA_BAOCAO { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_BAOCAO { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_BAOCAO = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new DM_CHITIEU_BAOCAO();

                if (!string.IsNullOrEmpty(this.MA_BAOCAO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_BAOCAO),
                        Value = this.MA_BAOCAO,
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
                get { return "MA_BAOCAO"; }
            }
        }
    }
}
