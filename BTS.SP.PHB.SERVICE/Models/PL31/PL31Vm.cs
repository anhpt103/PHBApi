using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.PL3_1;

namespace BTS.SP.PHB.SERVICE.Models.PL31
{
    public class PL31Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_PL31_DETAIL> DETAIL { get; set; }
        }

        public class DetailModel
        {
            public string REFID { get; set; }
            public class Item: PHB_PL31_DETAIL
            {
                public string MA_QHNS { get; set; }
                public string TEN_QHNS { get; set; }
                public int INDAM { get; set; }
                public int INNGHIENG { get; set; }
            }
            public List<Item> DETAIL { get; set; }
        }
        public class GroupExcel
        {
            public GroupExcel()
            {
                lstData = new List<DetailModel.Item>();
            }
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public List<DetailModel.Item> lstData { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_PL31_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_PL31_DETAIL> LstAdd { get; set; }
            public List<PHB_PL31_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
