using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.QLDT.B03
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }
        public List<PHB_PBDT_QLDT_B03_DETAIL> Details { get; set; }
    }
}