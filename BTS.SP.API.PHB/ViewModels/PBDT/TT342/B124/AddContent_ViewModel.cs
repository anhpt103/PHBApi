using BTS.SP.PHB.ENTITY.PBDT.B124;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B124
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B124 forms { get; set; }

        public List<PHB_PBDT_B124_DETAIL> Details { get; set; }
    }
}