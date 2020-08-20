using BTS.SP.PHB.ENTITY.PBDT.B122;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B122
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }

        public List<PHB_PBDT_B122_DETAIL> Details { get; set; }
    }
}