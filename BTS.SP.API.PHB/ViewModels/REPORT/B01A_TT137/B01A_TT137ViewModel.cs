using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.REPORT.B01A_TT137
{
    public class B01A_TT137ViewModel
    {
        public class AddModel
        {
            public PHB_B01A_TT137 data { get; set; }
            public List<PHB_B01A_TT137_DETAIL> datadetail { get; set; }
        }

        public class EditModel
        {
            public string refid { get; set; }
            public List<PHB_B01A_TT137_DETAIL> datadetail { get; set; }
        }

    }
}