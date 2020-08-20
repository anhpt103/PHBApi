using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU2CP2
{
    public class PHB_BIEU2CP2Vm
    {

        public class DataRes
        {
            public DataRes()
            {
                Body = new List<PHB_BIEU2CP2_DETAIL>();                
            }
            public List<PHB_BIEU2CP2_DETAIL> Body { get; set; }
            public int NAM_BC { get; set; }
        }
        
        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU2CP2_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU2CP2_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
