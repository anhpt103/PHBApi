using System.Collections.Generic;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_01
{
    public class F01_01BCQTModel
    {
        public F01_01BCQTModel()
        {
            F01_01BCQTDetail = new List<F01_01BCQTDetailItem>();
        }
        public ReportHeader ReportHeader { get; set; }
        public List<F01_01BCQTDetailItem> F01_01BCQTDetail { get; set; }
    }
}
