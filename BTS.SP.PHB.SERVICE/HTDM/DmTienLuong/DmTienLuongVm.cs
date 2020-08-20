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

namespace BTS.SP.PHB.SERVICE.HTDM.DmTienLuong
{
    public class DmTienLuongVm
    {
        public class ViewModel
        {
            public string ID { get; set; }
        }
        public class Search : IDataSearch
        {
            public string ID { get; set; }
            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new PHB_DM_TIENLUONG().ID);

                }
            }

            public void LoadGeneralParam(string summary)
            {
                ID = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_TIENLUONG();

                //if (!string.IsNullOrEmpty(this.MA_CAN_BO))
                //{
                //    result.Add(new QueryFilterLinQ
                //    {
                //        Property = ClassHelper.GetProperty(() => refObj.),
                //        Value = this.MA_CAN_BO,
                //        Method = FilterMethod.Like
                //    });
                //}
              
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }
 
        }
    }
}
