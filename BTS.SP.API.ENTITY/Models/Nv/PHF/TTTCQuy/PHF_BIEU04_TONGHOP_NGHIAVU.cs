using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BIEU04_TONGHOP_NGHIAVU")]
    public class PHF_BIEU04_TONGHOP_NGHIAVU : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TRICH_YEU")]
        [StringLength(500)]
        public string TRICH_YEU { get; set; }

        [Column("NGHIAVU_NSNN_TONDONG")]
        [StringLength(200)]
        public string NGHIAVU_NSNN_TONDONG { get; set; }

        [Column("PHATHIEN_THANHTRA")]
        [StringLength(100)]
        public string PHATHIEN_THANHTRA { get; set; }

        [Column("NGHIAVU_NSNN_PHAINOP")]
        [StringLength(200)]
        public string NGHIAVU_NSNN_PHAINOP { get; set; }

        [Column("GHICHU")]
        [StringLength(500)]
        public string GHICHU { get; set; }

    }
}
