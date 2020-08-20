using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.ThuThapSoLieu.TTTaiChinhDoanhNghiep
{
    [Table("PHF_PL03_DAUTU_TTTCDN")]
    public class PHF_PL03_DAUTU_TTTCDN : DataInfoEntityPHF
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

        [Column("DONVI")]
        [StringLength(500)]
        [Description("Đơn vị")]
        public string DONVI { get; set; }

        [Column("GIATRI_DAUTU")]
        [Description("Giá trị đầu tư")]
        public decimal? GIATRI_DAUTU { get; set; }

        [Column("VON_DIEULE")]
        [Description("Vốn điều lệ")]
        public decimal? VON_DIEULE { get; set; }

        [Column("LNCT_CHIA")]
        [Description("LN, CT chia năm")]
        public decimal? LNCT_CHIA { get; set; }
        
    }
}
