﻿using System.Collections.Generic;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B03TT90
{
    public class B03TT90Model
    {
        public B03TT90Model()
        {
            B03TT90Detail = new List<B03TT90DetailItem>();
        }
        public ReportHeader ReportHeader { get; set; }
        public List<B03TT90DetailItem> B03TT90Detail { get; set; }
    }
}
