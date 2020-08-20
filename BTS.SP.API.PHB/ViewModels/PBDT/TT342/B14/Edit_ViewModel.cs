using BTS.SP.PHB.ENTITY.PBDT.B14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B14
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }
        public List<PHB_PBDT_B14_DETAIL> Details { get; set; }
    }
}