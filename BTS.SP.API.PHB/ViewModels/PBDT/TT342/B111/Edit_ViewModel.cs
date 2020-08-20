using BTS.SP.PHB.ENTITY.PBDT.B05;
using BTS.SP.PHB.ENTITY.PBDT.B111;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B111
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }
        public List<PHB_PBDT_B111_DETAIL> Details { get; set; }
    }
}