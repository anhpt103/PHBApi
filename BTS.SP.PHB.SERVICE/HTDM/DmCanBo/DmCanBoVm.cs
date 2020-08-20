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

namespace BTS.SP.PHB.SERVICE.HTDM.DmCanBo
{
    public class DmCanBoVm
    {
        public class ViewModel
        {
            public string MA_CAN_BO { get; set; }
            public string TEN_CAN_BO { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_CAN_BO { get; set; }
            public string TEN_CAN_BO { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_CAN_BO = summary;
                TEN_CAN_BO = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_CANBO();

                if (!string.IsNullOrEmpty(this.MA_CAN_BO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_CAN_BO),
                        Value = this.MA_CAN_BO,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_CAN_BO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_CAN_BO),
                        Value = this.TEN_CAN_BO,
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
                get { return "MA_CAN_BO"; }
            }
        }
    }
}
