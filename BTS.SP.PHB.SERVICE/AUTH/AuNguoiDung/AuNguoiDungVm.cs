using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung
{
    public class AuNguoiDungVm
    {
        public class ViewModel
        {
            public string USERNAME { get; set; }
            public string EMAIL { get; set; }
            public string FULLNAME { get; set; }
        }
        public class Search : IDataSearch
        {
            public string USERNAME { get; set; }
            public string EMAIL { get; set; }
            public string FULLNAME { get; set; }

            public void LoadGeneralParam(string summary)
            {
                USERNAME = summary;
                EMAIL = summary;
                FULLNAME = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new AU_NGUOIDUNG();

                if (!string.IsNullOrEmpty(this.USERNAME))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.USERNAME),
                        Value = this.USERNAME,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.FULLNAME))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.FULLNAME),
                        Value = this.FULLNAME,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.EMAIL))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.EMAIL),
                        Value = this.EMAIL,
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
                get { return "USERNAME"; }
            }
        }
    }
}
