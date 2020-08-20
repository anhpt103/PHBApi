using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.C_B02AX
{
    public class PHB_C_B02AXVm
    {

        public class DataRes
        {
            public DataRes()
            {
                Body = new List<PHB_C_B02AX_DETAIL>();                
            }
            public List<PHB_C_B02AX_DETAIL> Body { get; set; }
            public int NAM_BC { get; set; }
        }
        
        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B02AX_DETAIL> LstAdd { get; set; }
            public List<PHB_C_B02AX_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
