using BTS.SP.PHB.ENTITY.PBDT.B1501;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B1501
{
    public class Detail_ViewModel
    {
        public PHB_PBDT_B1501_ROW Row { get; set; }
        public List<PHB_PBDT_B1501_DATA> Datas { get; set; }
    }
}