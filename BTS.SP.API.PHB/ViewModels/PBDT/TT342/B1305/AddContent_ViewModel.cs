using BTS.SP.PHB.ENTITY.PBDT.B1305;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1305
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1305 Form { get; set; }
        public List<PHB_PBDT_B1305_DETAIL> Details{ get; set; }
    }
}