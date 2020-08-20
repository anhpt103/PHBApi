using BTS.SP.AUTHENTICATION.API.BuildQuery;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.Dm.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.AUTHENTICATION.API.ServiceFunc.DmDBHC
{
    public class DM_DBHCVm
    {
        public class DM_DBHCs
        {
            public string MA_DBHC { get; set; }
            public string TEN_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public DateTime NGAY_HL { get; set; }

        }
        public class Search : IDataSearch
        {
            public string MA_DBHC { get; set; }
            public string TEN_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string MA_T { get; set; }
            public string MA_H { get; set; }
            public string MA_X { get; set; }
            public Nullable<DateTime> NGAY_PS { get; set; }
            public Nullable<DateTime> NGAY_SD { get; set; }
            public Nullable<DateTime> NGAY_HL { get; set; }
            public Nullable<int> LOAI { get; set; }
            public string USER_NAME { get; set; }
            public string MA_THAMCHIEU { get; set; }
            public string CAN_CU { get; set; }
            public Nullable<int> VALID { get; set; }

            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new DM_DBHC().MA_DBHC);
                }
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
                if (!string.IsNullOrEmpty(this.MA_DBHC_CHA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DBHC_CHA),
                        Value = this.MA_DBHC_CHA,
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
                MA_DBHC = summary;
                TEN_DBHC = summary;
            }
        }
    }
}
