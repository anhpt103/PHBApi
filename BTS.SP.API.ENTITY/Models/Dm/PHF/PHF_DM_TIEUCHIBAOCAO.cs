using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_TIEUCHIBAOCAO")]
    public class PHF_DM_TIEUCHIBAOCAO : DataInfoEntityPHF
    {
        [Column("MA_TIEUCHI")]
        [StringLength(50)]
        [Description("Mã tiêu chí")]
        public string MA_TIEUCHI { get; set; }

        [Required]
        [Column("TEN_TIEUCHI")]
        [StringLength(2000)]
        [Description("Tên tiêu chí")]
        public string TEN_TIEUCHI { get; set; }

        [Column("MA_CHA")]
        [StringLength(50)]
        [Description("Mã tiêu chí cha")]
        public string MA_CHA { get; set; }

        [Column("TEN_BAOCAO")]
        [StringLength(50)]
        [Description("Tên báo cáo")]
        public string TEN_BAOCAO { get; set; }

        [Column("SAPXEP")]
        [StringLength(50)]
        [Description("Sắp xếp")]
        public string SAPXEP { get; set; }

        [Column("INDAM")]
        public Nullable<int> INDAM { get; set; }

        [Column("INNGHIENG")]
        public Nullable<int> INNGHIENG { get; set; }

        [Column("TRANGTHAI")]
        [Description("Trạng thái")]
        public Nullable<int> TRANGTHAI { get; set; }
    }
}