using BTS.SP.PHB.ENTITY.Rp.B02_TT137;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.REPORT.B02_TT137
{
    public class B02TT137_ViewModel
    {
        public class SumPara
        {
            public string DSDVQHNS { get; set; }
            public string NAM_BC { get; set; } 
        }

        public class AddModel
        {
            public PHB_B02_TT137 data { get; set; }
            public List<PHB_B02_TT137_DETAIL> datadetail { get; set; }
        }

        public class EditModel
        {
            public string refid { get; set; }
            public List<PHB_B02_TT137_DETAIL> datadetail { get; set; }
        }
    }
}