using BTS.SP.PHB.ENTITY.PBDT.B06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B06
{
    public class Detail_ViewModel
    {
        public List<Row_ViewModel> Rows { get; set; }

        public List<PHB_PBDT_B06_DONVI> DonVis { get; set; }

    }
}