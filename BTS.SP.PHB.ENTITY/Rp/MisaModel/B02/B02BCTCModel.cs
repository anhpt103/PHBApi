using System.Collections.Generic;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B02
{
    public class B02BCTCModel
    {
        public B02BCTCModel()
        {
            B02BCTCDetail = new List<B02BCTCDetailItem>();
        }
        public ReportHeader ReportHeader { get; set; }
        public List<B02BCTCDetailItem> B02BCTCDetail { get; set; }
    }
}
