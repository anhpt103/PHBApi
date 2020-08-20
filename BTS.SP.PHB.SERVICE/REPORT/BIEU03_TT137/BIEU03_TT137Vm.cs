using BTS.SP.PHB.ENTITY.Rp.BIEU03_TT137;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU03_TT137
{
    public class BIEU03_TT137Vm
    {
        public class SumPara
        {
            public string DSDVQHNS { get; set; }
            public string NAM_BC { get; set; }
        }

        public class AddModel
        {
            public PHB_BIEU03_TT137 data { get; set; }
            public List<PHB_BIEU03_TT137_DETAIL> datadetail { get; set; }
        }

        public class EditModel
        {
            public EditModel()
            {
                datadetail = new List<PHB_BIEU03_TT137_DETAIL>();
                data = new PHB_BIEU03_TT137();
            }
            public PHB_BIEU03_TT137 data { get; set; }
            public List<PHB_BIEU03_TT137_DETAIL> datadetail { get; set; }
        }
    }
}
