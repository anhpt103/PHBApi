using BTS.SP.PHB.ENTITY.Rp.PL01_TT137;
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

namespace BTS.SP.PHB.SERVICE.REPORT.PL01_TT137
{
    public class PHB_PL01_TT137Vm
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
            public string MA_QHNS { get; set; }
            public string NAM_BC { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_QHNS = summary;
                NAM_BC = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_PL01_TT137();

                if (!string.IsNullOrEmpty(this.MA_QHNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_QHNS),
                        Value = this.MA_QHNS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.NAM_BC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.NAM_BC),
                        Value = this.NAM_BC,
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
                get { return "Ky"; }
            }
        }
    }
}
