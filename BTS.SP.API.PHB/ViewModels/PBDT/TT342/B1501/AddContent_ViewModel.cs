using BTS.SP.PHB.ENTITY.PBDT.B1501;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1501
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_B1501 Form { get; set; }
        public List<Detail_ViewModel> Detail_ViewModels{ get; set; }
    }
}