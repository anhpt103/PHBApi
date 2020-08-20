using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.QLDT.B02
{
    public class Template_ViewModel
    {
        public string Tinh { get; set; }
        public string Huyen { get; set; }
        public string Xa { get; set; }
        public List<PHB_PBDT_QLDT_B02_TEMPLATE> Details { get; set; }
    }
}