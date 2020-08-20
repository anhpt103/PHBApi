using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("PHA_NHAPDUTOAN_DUAN_DETAIL")]
    public class PHA_NHAPDUTOAN_DUAN_DETAIL : DataInfoEntity
    {
        [Column("PHA_NHAPDUTOAN_DUAN_REFID")]
        [StringLength(50)]
        public string PHA_NHAPDUTOAN_DUAN_REFID { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(50)]
        public string MA_CHUONG { get; set; }

        [Column("TEN_CHUONG")]
        [StringLength(500)]
        public string TEN_CHUONG { get; set; }

        [Column("MA_DVQHNS")]
        [StringLength(50)]
        public string MA_DVQHNS { get; set; }

        [Column("STT")]
        [StringLength(10)]
        public string STT { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("TEN_DVQHNS")]
        [StringLength(500)]
        public string TEN_DVQHNS { get; set; }

        [Column("LOAI_CHITIEU")]
        [StringLength(50)]
        public string LOAI_CHITIEU { get; set; }

        [Column("TONG_GT")]
        public decimal? TONG_GT { get; set; }

        [Column("NGANSACH_GT")]
        public decimal? NGANSACH_GT { get; set; }

        [Column("NSDP_GT")]
        public decimal? NSDP_GT { get; set; }

        [Column("TONG_LK")]
        public decimal? TONG_LK { get; set; }

        [Column("NGANSACH_LK")]
        public decimal? NGANSACH_LK { get; set; }

        [Column("NSDP_LK")]
        public decimal? NSDP_LK { get; set; }

        [Column("TONG_KH")]
        public decimal? TONG_KH { get; set; }

        [Column("NGANSACH_KH")]
        public decimal? NGANSACH_KH { get; set; }

        [Column("NSDP_KH")]
        public decimal? NSDP_KH { get; set; }
    }
}
