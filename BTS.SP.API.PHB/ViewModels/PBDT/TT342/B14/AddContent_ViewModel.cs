using BTS.SP.PHB.ENTITY.PBDT.B14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B14
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B14 Form { get; set; }
        public List<PHB_PBDT_B14_DETAIL> Details{ get; set; }
    }
}