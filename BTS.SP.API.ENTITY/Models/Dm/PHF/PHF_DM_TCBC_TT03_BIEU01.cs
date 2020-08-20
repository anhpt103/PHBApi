using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_TCBC_TT03_BIEU01")]
    public class PHF_DM_TCBC_TT03_BIEU01 : DataInfoEntityPHF
    {
        [Column("STT")]
        [StringLength(50)]
        [Description("Số thứ tự")]
        public string STT { get; set; }

        [Column("NOIDUNG")]
        [StringLength(2000)]
        [Description("Tên tiêu chí")]
        public string NOIDUNG { get; set; }

        [Column("TEN_BAOCAO")]
        [StringLength(50)]
        [Description("Tên báo cáo")]
        public string TEN_BAOCAO { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(50)]
        [Description("Trạng thái")]
        public string TRANG_THAI { get; set; }

        [Column("SAPXEP")]
        [Description("Sắp xếp")]
        public int SAPXEP { get; set; }

        [Column("MADONG")]
        [StringLength(50)]
        public string MADONG { get; set; }

        [Column("INDAM")]
        public Nullable<int> INDAM { get; set; }

        [Column("INNGHIENG")]
        public Nullable<int> INNGHIENG { get; set; }
    }
}
