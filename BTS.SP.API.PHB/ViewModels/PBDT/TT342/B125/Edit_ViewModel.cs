using BTS.SP.PHB.ENTITY.PBDT.B125;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B125
{
    public class Edit_ViewModel
    {
        public int FormId { get; set; }

        public List<PHB_PBDT_B125_DETAIL> DETAILS { get; set; }
    }
}