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
    [Table("PHF_BIEU01_TONGHOP_NGUONTHU")]
    public class PHF_BIEU01_TONGHOP_NGUONTHU : DataInfoEntityPHF
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

        [Column("NGUONTHU")]
        [StringLength(500)]
        public string NGUONTHU { get; set; }

        [Column("TONGSO_THU")]
        public decimal? TONGSO_THU { get; set; }

        [Column("THEOCD_KEHOACH")]
        public decimal? THEOCD_KEHOACH { get; set; }

        [Column("THEOCD_THUCHIEN")]
        public decimal? THEOCD_THUCHIEN { get; set; }


        [Column("THEOCD_TYLETH")]
        public decimal? THEOCD_TYLETH { get; set; }

        [Column("SAICD_TONGSO")]
        public decimal? SAICD_TONGSO { get; set; }

        [Column("SAICD_NGOAISO")]
        public decimal? SAICD_NGOAISO { get; set; }

        [Column("GHICHU")]
        [StringLength(500)]
        public string GHICHU { get; set; }
    }
}
