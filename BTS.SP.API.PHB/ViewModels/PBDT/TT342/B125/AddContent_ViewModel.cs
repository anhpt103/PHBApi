using BTS.SP.PHB.ENTITY.PBDT.B125;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B125
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B125 forms { get; set; }

        public List<PHB_PBDT_B125_DETAIL> Details { get; set; }
    }
}