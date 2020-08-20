using BTS.SP.PHB.ENTITY.Rp.L_PC_D;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;
using System;
using System.Collections.Generic;

namespace BTS.SP.PHB.SERVICE.Models.L_PC_D
{
    public class PHB_L_PC_DVm
    {
        public class Search : IDataSearch
        {
            public string MA_BAO_BAO { get; set; }
            public int KY_BC { get; set; }
            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new PHB_L_PC_D().REFID);
                }
            }
            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_L_PC_D();
              
                if (!string.IsNullOrEmpty(this.MA_BAO_BAO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.REFID),
                        Value = this.MA_BAO_BAO,
                        Method = FilterMethod.Like
                    });
                }
                if (this.KY_BC != 0)
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.KY_BC),
                        Value = this.KY_BC,
                        Method = FilterMethod.EqualTo
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
                MA_BAO_BAO = summary;
            }
        }
            public class ViewModel
        {
            public string REFID { get; set; }
        }
        public class DetailModel
        {
            public int LOAI { get; set; }
            public string STT { get; set; }
            public string HO_VATEN { get; set; }
            public string CHUC_DANH { get; set; }
            public double HE_SOLUONG { get; set; }
            public double PC_KV { get; set; }
            public double PC_CHUCVU { get; set; }
            public double PC_THAMNIEN { get; set; }
            public double PC_TRACHNHIEM { get; set; }
            public double PC_CONGVU { get; set; }
            public double PC_LOAIXA { get; set; }
            public double PC_LAUNAM { get; set; }
            public double PC_THUHUT { get; set; }
            public double CKPT_BHXH { get; set; }
            public double CKPT_BHYT { get; set; }
        }
        public class InsertModel
        {
            public int ID { get; set; }
            public string REFID { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_DBHC  { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public string MA_DBHC_CHA { get; set; }
        }

        public class DTO
        {
            public DTO()
            {
                DataDetails = new List<PHB_L_PC_D_DETAIL>();
                listDeleteDetails = new List<PHB_L_PC_D_DETAIL>();
            }
            public int ID { get; set; }
            public bool Is_MA_QHNS_Wrong { get; set; }
            public int KY_BC { get; set; }
            public string MA_BAO_CAO { get; set; }
            public string MA_BAOCAO_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DVQHNS { get; set; }
            public int NAM_BC { get; set; }
            public string TEN_DVQHNS { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public List<PHB_L_PC_D_DETAIL> DataDetails { get; set; }
            public List<PHB_L_PC_D_DETAIL> listDeleteDetails { get; set; }
        }

        public class EditModel
        {
            public int KY_BC { get; set; }
            public string REFID { get; set; }
            public List<PHB_L_PC_D_DETAIL> LstAdd { get; set; }
            public List<PHB_L_PC_D_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
