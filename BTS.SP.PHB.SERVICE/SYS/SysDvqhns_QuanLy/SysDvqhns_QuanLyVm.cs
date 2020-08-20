using System;
using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy
{
    public class SysDvqhns_QuanLyVm
    {
        public class ViewModel
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string TEN_CHUONG { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_DBHC { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_DVQHNS = summary;
                TEN_DVQHNS = summary;
                MA_CHUONG = summary;
                MA_DBHC = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new SYS_DVQHNS_QUANLY();

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
                if (!string.IsNullOrEmpty(this.MA_CHUONG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_CHUONG),
                        Value = this.MA_CHUONG,
                        Method = FilterMethod.EqualTo
                    });
                }
                if (!string.IsNullOrEmpty(this.MA_DBHC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_DBHC),
                        Value = this.MA_DBHC,
                        Method = FilterMethod.Like
                    });
                }
                return result;
            }

            List<TOOLS.BuildQuery.IQueryFilter> IDataSearch.GetFilters()
            {
                throw new NotImplementedException();
            }

            public List<TOOLS.BuildQuery.IQueryFilter> GetQuickFilters()
            {
                throw new NotImplementedException();
            }

            public string DefaultOrder
            {
                get { return ClassHelper.GetPropertyName(() => new SYS_DVQHNS_QUANLY().MA_DVQHNS); }
            }
        }
    }
}
