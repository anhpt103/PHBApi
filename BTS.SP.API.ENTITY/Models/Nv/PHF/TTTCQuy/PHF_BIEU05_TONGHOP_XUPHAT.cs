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
    [Table("PHF_BIEU05_TONGHOP_XUPHAT")]
    public class PHF_BIEU05_TONGHOP_XUPHAT : DataInfoEntityPHF
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

        [Column("NOIDUNG_VIPHAM")]
        [StringLength(500)]
        public string NOIDUNG_VIPHAM { get; set; }

        [Column("HINHTHUC_PHAT")]
        [StringLength(200)]
        public string HINHTHUC_PHAT { get; set; }

        [Column("MUC_PHAT")]
        [StringLength(100)]
        public string MUC_PHAT { get; set; }

        [Column("BIENPHAP_KHACPHUC")]
        [StringLength(100)]
        public string BIENPHAP_KHACPHUC { get; set; }

        [Column("GHICHU")]
        [StringLength(500)]
        public string GHICHU { get; set; }

    }
}
