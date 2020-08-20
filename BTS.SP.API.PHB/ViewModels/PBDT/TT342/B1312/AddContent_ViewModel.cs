using BTS.SP.PHB.ENTITY.PBDT.B1312;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1312
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1312 Form { get; set; }
        public List<PHB_PBDT_B1312_DETAIL> Details{ get; set; }
    }
}