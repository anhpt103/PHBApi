﻿using System.Collections.Generic;

namespace BTS.SP.PHB.ENTITY.Rp.MisaModel.B03
{
    public class B03bBCTCModel
    {
        public B03bBCTCModel()
        {
            B03bBCTCDetail = new List<B03bBCTCDetailItem>();
        }
        public ReportHeader ReportHeader { get; set; }
        public List<B03bBCTCDetailItem> B03bBCTCDetail { get; set; }
    }
}
