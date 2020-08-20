using BTS.SP.PHB.ENTITY.PBDT.B1310;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1310
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1310 Form { get; set; }
        public List<PHB_PBDT_B1310_DETAIL> Details{ get; set; }
    }
}