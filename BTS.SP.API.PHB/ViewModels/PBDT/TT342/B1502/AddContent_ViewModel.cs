using BTS.SP.PHB.ENTITY.PBDT.B1502;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1502
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1502 Form { get; set; }
        public List<Detail_ViewModel> Detail_ViewModels{ get; set; }
    }
}