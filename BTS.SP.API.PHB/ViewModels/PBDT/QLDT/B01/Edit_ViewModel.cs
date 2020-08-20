using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.QLDT.B01
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }
        public List<PHB_PBDT_QLDT_B01_DETAIL> Details { get; set; }
    }
}