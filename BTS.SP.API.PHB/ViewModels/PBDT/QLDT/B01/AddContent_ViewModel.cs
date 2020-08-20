using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using System.Collections.Generic;

namespace BTS.SP.API.PHB.ViewModels.PBDT.QLDT.B01
{
    public class AddContent_ViewModel
    {
        public PHB_PBDT_QLDT_B01 Form { get; set; }
        public List<PHB_PBDT_QLDT_B01_DETAIL> Details{ get; set; }
    }
}