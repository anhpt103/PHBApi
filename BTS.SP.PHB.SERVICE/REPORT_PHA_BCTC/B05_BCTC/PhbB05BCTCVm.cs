using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC
{
    public class PhbB05BCTCVm
    {
        public class AddConten
        {
            public List<PHB_B05_BCTC_DETAIL> dataTable { get; set; }
            public PHB_B05_BCTC_WORK dataWork { get; set; }
            public PHB_B05_BCTC data { get; set; }
        }

        public class EditConten
        {
            public string Formid { get; set; }
            public List<PHB_B05_BCTC_DETAIL> dataTable { get; set; } 
            public PHB_B05_BCTC_WORK dataWork { get; set; }
        }


        public class Detail
        {
            public List<PHB_B05_BCTC_DETAIL> dataTable { get; set; }
                public PHB_B05_BCTC_WORK dataWork { get; set; }
        }
    }
}

