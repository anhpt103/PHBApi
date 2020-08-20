using BTS.SP.PHB.ENTITY.PBDT.B111;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B111
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B111 Form { get; set; }
        public List<PHB_PBDT_B111_DETAIL> Details{ get; set; }
    }
}