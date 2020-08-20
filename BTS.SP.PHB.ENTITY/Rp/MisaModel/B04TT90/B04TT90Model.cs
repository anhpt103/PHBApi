using System.Collections.Generic;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B04TT90
{
    public class B04TT90Model
    {
        public B04TT90Model()
        {
            B04TT90Detail = new List<B04TT90DetailItem>();
        }
        public ReportHeader ReportHeader { get; set; }
        public List<B04TT90DetailItem> B04TT90Detail { get; set; }
    }
}
