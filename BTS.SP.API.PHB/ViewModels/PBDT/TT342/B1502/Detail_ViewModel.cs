using BTS.SP.PHB.ENTITY.PBDT.B1502;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1502
{
    public class Detail_ViewModel
    {
        public PHB_PBDT_B1502_ROW Row { get; set; }
        public List<PHB_PBDT_B1502_DATA> Datas { get; set; }
    }
}