using BTS.SP.PHB.ENTITY.PBDT.B123;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B123
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B123 Form { get; set; }

        public List<PHB_PBDT_B123_DETAIL> Details { get; set; }
    }
}