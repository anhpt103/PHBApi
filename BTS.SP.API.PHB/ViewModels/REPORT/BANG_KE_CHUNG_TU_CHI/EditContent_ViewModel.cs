using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.REPORT.BANG_KE_CHUNG_TU_CHI
{
    public class EditContent_ViewModel
    {
        public KEKHAICHUNGTU Form { get; set; }
        public List<KEKHAICHUNGTUDETAIL> Details { get; set; }
    }
}