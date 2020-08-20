using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;
using BTS.SP.PHB.ENTITY.Rp.C_B02AX;
using BTS.SP.PHB.ENTITY.Rp.C_B02B_X;

namespace BTS.SP.API.PHB.Utils
{
    public class B01XImportModel:PHB_C_B01X_DETAIL
    {
        public string MA_QHNS { get; set; }
        public int KY_BC { get; set; }
        public int NAM_BC { get; set; }
    }

    public class B02AXImportModel : PHB_C_B02AX_DETAIL
    {
        public string MA_QHNS { get; set; }
        public int KY_BC { get; set; }
        public int NAM_BC { get; set; }
    }
    public class B02BXImportModel : PHB_C_B02B_X_DETAIL
    {
        public string MA_QHNS { get; set; }
        public int KY_BC { get; set; }
        public int NAM_BC { get; set; }
    }
}