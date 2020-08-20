
using BTS.SP.PHB.ENTITY.Rp.BM16_TT344;
using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.REPORT.BM16_TT344
{
    public class AddContent_ViewModel
    {
        public PHB_BM16_TT344 Form { get; set; }
        public List<PHB_BM16_TT344_DETAIL> Details { get; set; }
    }
}