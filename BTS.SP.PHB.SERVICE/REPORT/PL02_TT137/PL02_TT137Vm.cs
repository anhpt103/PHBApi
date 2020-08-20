using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.PL02_TT137
{
    public class PL02_TT137Vm
    {
        public class ViewModel
        {
            public string Ky { get; set; }
            public string Nam { get; set; }
            public string Chuong { get; set; }
            public string DonVi { get; set; }
            public string CanBoI { get; set; }
            public string CanBoII { get; set; }
            public string ChucVuI { get; set; }
            public string ChucVuII { get; set; }
            public string QuyetToanNganSachNam { get; set; }
            public string KhongBaoGomQuyetToanVon { get; set; }
            public double DuToanGiaoDauNam { get; set; }
            public double DuToanBoSungTrongNam { get; set; }
            public double TSKPPhaiNopNSNN { get; set; }
            public double TSKPDaNopNSNN { get; set; }
            public double TSKPConPhaiNopNSNN { get; set; }
            public string ThuyetMinh { get; set; }
            public string NhanXet { get; set; }
            public string KienNghi { get; set; }
            public string NgayTao { get; set; }
            public string NguoiTao { get; set; }
            public string DV_Cha { get; set; }
            public string DiaChi { get; set; }
           
        }
        public class Search : IDataSearch
        {
            public string Ky { get; set; }
            public string Nam { get; set; }
            public string DV_Cha { get; set; }

            public void LoadGeneralParam(string summary)
            {
                Ky = summary;
                Nam = summary;
                DV_Cha = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                //var refObj = new PL1_TT137();

                //if (!string.IsNullOrEmpty(this.Ky))
                //{
                //    result.Add(new QueryFilterLinQ
                //    {
                //        Property = ClassHelper.GetProperty(() => refObj.Ky),
                //        Value = this.Ky,
                //        Method = FilterMethod.Like
                //    });
                //}
                //if (!string.IsNullOrEmpty(this.Nam))
                //{
                //    result.Add(new QueryFilterLinQ
                //    {
                //        Property = ClassHelper.GetProperty(() => refObj.Nam),
                //        Value = this.Nam,
                //        Method = FilterMethod.Like
                //    });
                //}
                //if (!string.IsNullOrEmpty(this.DV_Cha))
                //{
                //    result.Add(new QueryFilterLinQ
                //    {
                //        Property = ClassHelper.GetProperty(() => refObj.DV_Cha),
                //        Value = this.DV_Cha,
                //        Method = FilterMethod.Like
                //    });
                //}
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public string DefaultOrder
            {
                get { return "Ky"; }
            }
        }
    }
}
