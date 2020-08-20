using BTS.SP.PHB.ENTITY.PBDT.TT344;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.TT344.B04
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }
        public List<PHB_PBDT_TT344_B04_DETAIL> Details { get; set; }
    }
}