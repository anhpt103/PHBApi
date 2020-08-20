﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHE
{
    [Table("PHE_CHITIET_GIAO_KEHOACH_VON")]
    public class PHE_CHITIET_GIAO_KEHOACH_VON : DataInfoEntity
    {
        [Column("SO_CHUNGTU")]
        [StringLength(50)]
        public string SO_CHUNGTU { get; set; }

        [Column("GOI_THAU")]
        [StringLength(50)]
        public string GOI_THAU { get; set; }

        [Column("MA_HANGMUC")]
        [StringLength(50)]
        public string MA_HANGMUC { get; set; }

        [Column("MA_CHIPHI")]
        [StringLength(50)]
        public string MA_CHIPHI { get; set; }

        [Column("NGUON_VON")]
        [StringLength(50)]
        public string NGUON_VON { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(50)]
        public string MA_CHUONG { get; set; }

        [Column("MA_TIEUMUC")]
        [StringLength(50)]
        public string MA_TIEUMUC { get; set; }

        [Column("DOITUONG_VON")]
        [StringLength(50)]
        public string DOITUONG_VON { get; set; }

        [Column("SOTIEN")]
        public decimal SOTIEN { get; set; }

        [Column("TEP_DINHKEM")]
        [StringLength(1000)]
        public string TEP_DINHKEM { get; set; }
    }
}
