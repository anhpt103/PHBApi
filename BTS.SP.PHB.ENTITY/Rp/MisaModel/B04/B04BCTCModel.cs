using System.Collections.Generic;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B04
{
    public class B04BCTCModel
    {
        public B04BCTCModel()
        {
            B04BCTCDetail = new List<B04BCTCDetailItem>();
        }
        public ReportHeader ReportHeader { get; set; }
        public List<B04BCTCDetailItem> B04BCTCDetail { get; set; }
    }
}
