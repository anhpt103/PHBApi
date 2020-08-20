using BTS.SP.PHB.ENTITY.PBDT.B1301;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1301
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }
        public List<PHB_PBDT_B1301_DETAIL> Details { get; set; }
    }
}