using BTS.SP.PHB.ENTITY.PBDT.B07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B07
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B07 Form { get; set; }
        public List<PHB_PBDT_B07_DETAIL> Details{ get; set; }
    }
}