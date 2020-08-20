using BTS.SP.PHB.ENTITY.PBDT.B06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B06
{
    public class Row_ViewModel
    {
        public PHB_PBDT_B06_DETAIL Detail { get; set; }

        public List<PHB_PBDT_B06_DATA> Datas { get; set; }
    }
}