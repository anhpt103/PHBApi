using System;
using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Rp.DOICHIEUSOLIEU;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.Models.DOICHIEUSOLIEU
{
    public class DOICHIEUSOLIEUVm
    {
        public class Search : IDataSearch
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
            public string CAP_DUTOAN { get; set; }
            public string LOAI_DULIEU { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_DVQHNS = summary;
                TEN_DVQHNS = summary;
                MA_DVQHNS_CHA = summary;
                CAP_DUTOAN = summary;
                LOAI_DULIEU = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DOICHIEUSOLIEU();

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
                if (!string.IsNullOrEmpty(this.CAP_DUTOAN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.CAP_DUTOAN),
                        Value = this.CAP_DUTOAN,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.LOAI_DULIEU))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.LOAI_DULIEU),
                        Value = this.LOAI_DULIEU,
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
                get { return "MA_DVQHNS"; }
            }
        }

        public class ViewModel
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
            public string CAP_DUTOAN { get; set; }
            public string LOAI_DULIEU { get; set; }
            public int NAM { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string MA_HUYEN { get; set; }
        }
        public class InsertModel
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
            public string CAP_DUTOAN { get; set; }
            public string LOAI_DULIEU { get; set; }
            public int NAM { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string MA_HUYEN { get; set; }
            public decimal SOTIEN_DENGHI { get; set; }
            public decimal SOTIEN_TABMIS { get; set; }
        }

        public class EditModel
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
            public string CAP_DUTOAN { get; set; }
            public string LOAI_DULIEU { get; set; }
            public int NAM { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string MA_HUYEN { get; set; }
            public decimal SOTIEN_DENGHI { get; set; }
            public decimal SOTIEN_TABMIS { get; set; }
        }

        public class TemplateExcel
        {
            public string MA_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public string MA_DVQHNS_CHA { get; set; }
            public string CAP_DUTOAN { get; set; }
            public string LOAI_DULIEU { get; set; }
            public int NAM { get; set; }
            public string MA_HUYEN { get; set; }
            public decimal SOTIEN_DENGHI { get; set; }
        }

        public class DeleteItem
        {
            public string MA_DVQHNS { get; set; }
            public string CAP_DUTOAN { get; set; }
            public string LOAI_DULIEU { get; set; }
            public int NAM { get; set; }
        }
    }
}
