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

namespace BTS.SP.PHB.SERVICE.HTDM.DmDuAn
{
    public class DmDuAnVm
    {
        public class ViewModel
        {
            public string MA_DA { get; set; }
            public string SOHIEU_DA { get; set; }
            public string TEN_DA { get; set; }
            public string TEN_EN_DUAN { get; set; }
            public string TEN_CTMT { get; set; }
            public string DONVI_THUCHIEN { get; set; }
            public string MO_TA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_DA { get; set; }
            public string SOHIEU_DA { get; set; }
            public string TEN_DA { get; set; }
            public string TEN_EN_DUAN { get; set; }
            public string TEN_CTMT { get; set; }
            public string DONVI_THUCHIEN { get; set; }
            public string MO_TA { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_DA = summary;
                SOHIEU_DA = summary;
                TEN_DA = summary;
                TEN_EN_DUAN = summary;
                DONVI_THUCHIEN = summary;
                MO_TA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_DUAN();

                if (!string.IsNullOrEmpty(this.MA_DA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DA),
                        Value = this.MA_DA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.SOHIEU_DA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.SOHIEU_DA),
                        Value = this.SOHIEU_DA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_DA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_DA),
                        Value = this.TEN_DA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_EN_DUAN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_EN_DUAN),
                        Value = this.TEN_EN_DUAN,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_CTMT))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_CTMT),
                        Value = this.TEN_CTMT,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.DONVI_THUCHIEN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.DONVI_THUCHIEN),
                        Value = this.DONVI_THUCHIEN,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MO_TA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MO_TA),
                        Value = this.MO_TA,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_DUAN().MA_DA); }
            }
        }
    }
}
