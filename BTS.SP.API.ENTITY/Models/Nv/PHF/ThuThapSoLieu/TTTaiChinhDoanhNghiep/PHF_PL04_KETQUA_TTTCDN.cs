using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.ThuThapSoLieu.TTTaiChinhDoanhNghiep
{
    [Table("PHF_PL04_KETQUA_TTTCDN")]
    public class PHF_PL04_KETQUA_TTTCDN : DataInfoEntityPHF
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

        [Column("TONG_DOANHTHU")]
        [Description("Tổng doanh thu")]
        public decimal? TONG_DOANHTHU { get; set; }

        [Column("TONG_CHIPHI")]
        [Description("Tổng chi phí")]
        public decimal? TONG_CHIPHI { get; set; }

        [Column("LOINHUAN_THUCHIEN")]
        [Description("Lợi nhuận thực hiện")]
        public decimal? LOINHUAN_THUCHIEN { get; set; }

        [Column("LOINHUAN_SAUTHUE")]
        [Description("Lợi nhuận sau thuế")]
        public decimal? LOINHUAN_SAUTHUE { get; set; }
        
    }
}
