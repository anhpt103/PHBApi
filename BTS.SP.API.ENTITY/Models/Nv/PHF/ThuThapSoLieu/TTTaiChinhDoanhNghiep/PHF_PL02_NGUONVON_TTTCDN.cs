using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.ThuThapSoLieu.TTTaiChinhDoanhNghiep
{
    [Table("PHF_PL02_NGUONVON_TTTCDN")]
    public class PHF_PL02_NGUONVON_TTTCDN : DataInfoEntityPHF
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

        [Column("NO_TONGCONG")]
        [Description("Nợ tổng cộng")]
        public decimal? NO_TONGCONG { get; set; }

        [Column("NO_NGANHAN")]
        [Description("Nợ ngắn hạn")]
        public decimal? NO_NGANHAN { get; set; }

        [Column("NO_DAIHAN")]
        [Description("Nợ dài hạn")]
        public decimal? NO_DAIHAN { get; set; }

        [Column("NO_KHOANKHAC")]
        [Description("Nợ khoản khác")]
        public decimal? NO_KHOANKHAC { get; set; }

        [Column("VON_TONGCONG")]
        [Description("Vốn chủ sở hữu - Tổng cộng")]
        public decimal? VON_TONGCONG { get; set; }

        [Column("VON_CSH")]
        [Description("Vốn chủ sở hữu - Vốn đầu tư của CSH")]
        public decimal? VON_CSH { get; set; }

        [Column("VON_DTPT")]
        [Description("Vốn chủ sở hữu - Vốn DTPT")]
        public decimal? VON_DTPT { get; set; }

        [Column("VON_CHUAPHANPHOI")]
        [Description("Vốn chủ sở hữu - Vốn chưa phân phối")]
        public decimal? VON_CHUAPHANPHOI { get; set; }

        [Column("VON_KHOANKHAC")]
        [Description("Vốn khoản khác")]
        public decimal? VON_KHOANKHAC { get; set; }

        [Column("TONGCONG")]
        [Description("Tổng cộng")]
        public decimal? TONGCONG { get; set; }
    }
}
