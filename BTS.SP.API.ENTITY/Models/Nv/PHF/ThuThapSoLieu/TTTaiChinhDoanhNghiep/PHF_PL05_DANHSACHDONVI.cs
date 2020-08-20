using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Nv.PHF.ThuThapSoLieu.TTTaiChinhDoanhNghiep
{
    [Table("PHF_PL05_DANHSACHDONVI")]
    public class PHF_PL05_DANHSACHDONVI : DataInfoEntityPHF
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

        [Column("NGANHNGHE_KD")]
        [StringLength(500)]
        public string NGANHNGHE_KD { get; set; }

        [Column("VON_DL")]
        public int? VON_DL { get; set; }

        [Column("DIA_CHI")]
        [StringLength(500)]
        public string DIA_CHI { get; set; }

        [Column("TONG_TAISAN")]
        public decimal? TONG_TAISAN { get; set; }

        [Column("VON_CHUSOHUU")]
        public decimal? VON_CHUSOHUU { get; set; }

        [Column("TONG_DT_TN")]
        public decimal? TONG_DT_TN { get; set; }

        [Column("TONG_CHIPHI")]
        public decimal? TONG_CHIPHI { get; set; }

        [Column("TONG_LOINHUAN_TT")]
        public decimal? TONG_LOINHUAN_TT { get; set; }


    }
}
