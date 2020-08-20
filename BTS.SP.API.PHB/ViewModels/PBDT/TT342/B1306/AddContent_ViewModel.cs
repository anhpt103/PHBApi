using BTS.SP.PHB.ENTITY.PBDT.B1306;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1306
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1306 Form { get; set; }
        public List<PHB_PBDT_B1306_DETAIL> Details{ get; set; }
    }
}