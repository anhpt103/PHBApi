﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.SERVICE.AUTH.AuNguoiDungNhomQuyen
{
    public class AuNguoiDungNhomQuyenViewModel:BaseEntity
    {
        public string PHANHE { get; set; }
        public string USERNAME { get; set; }
        public string MANHOMQUYEN { get; set; }
        public string TENNHOMQUYEN { get; set; }
    }
}
