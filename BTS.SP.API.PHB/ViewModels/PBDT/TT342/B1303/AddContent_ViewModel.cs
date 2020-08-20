using BTS.SP.PHB.ENTITY.PBDT.B1303;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1303
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1303 Form { get; set; }
        public List<PHB_PBDT_B1303_DETAIL> Details{ get; set; }
    }
}