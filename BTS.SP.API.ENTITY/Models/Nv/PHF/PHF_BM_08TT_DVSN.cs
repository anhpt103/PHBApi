using BTS.SP.API.ENTITY;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_08TT_DVSN")]
    public class PHF_BM_08TT_DVSN : DataInfoEntityPHF
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

        [Column("NOIDUNG_CHI")]
        [StringLength(1000)]
        [Description("Nội dung chi")]
        public string NOIDUNG_CHI { get; set; }

        [Column("SOTIEN")]
        [Description("Số tiền")]
        public decimal? SOTIEN { get; set; }

        [Column("NGUYENNHAN_TRICH_CAOHON")]
        [StringLength(2000)]
        [Description("Nguyên nhân Trích cao hơn quy định")]
        public string NGUYENNHAN_TRICH_CAOHON { get; set; }

        [Column("NGUYENNHAN_TRICH_SAINGUON")]
        [StringLength(2000)]
        [Description("Nguyên nhân Trích không đúng nguồn")]
        public string NGUYENNHAN_TRICH_SAINGUON { get; set; }

        [Column("NGUYENNHAN_TRICH_SAI_TYLE")]
        [StringLength(2000)]
        [Description("Nguyên nhân Trích sai tỷ lệ")]
        public string NGUYENNHAN_TRICH_SAI_TYLE { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
