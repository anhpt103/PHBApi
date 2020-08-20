using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.BuildQuery;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.Services;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNguoiDung
{
    public class AuNguoiDungVm
    {
        public class Search : IDataSearch
        {
            public string USERNAME { get; set; }
            public string FULLNAME { get; set; }
            public string EMAIL { get; set; }

            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new AU_NGUOIDUNG().USERNAME);
                }
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

            public void LoadGeneralParam(string summary)
            {
                EMAIL = summary;
                USERNAME = summary;
                FULLNAME = summary;
            }
        }
    }
}
