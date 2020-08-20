using BTS.SP.PHB.ENTITY.PBDT.B121;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B121
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B121 Form { get; set; }

        public List<PHB_PBDT_B121_DETAIL> Details { get; set; }
    }
}