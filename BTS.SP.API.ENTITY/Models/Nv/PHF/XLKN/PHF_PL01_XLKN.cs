using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL01_XLKN")]
    public class PHF_PL01_XLKN : DataInfoEntityPHF
    {
        [Column("MA_BAOCAO")]
        [StringLength(100)]
        [Description("Mã báo cáo")]
        public string MA_BAOCAO { get; set; }


        [Column("NAM")]
        [Description("năm")]
        public int NAM { get; set; }

        [Column("PHONG_THANHTRA")]
        [StringLength(500)]
        [Description("Ghi chú")]
        public string PHONG_THANHTRA { get; set; }

        [Column("DOT_BAOCAO")]
        [StringLength(500)]
        [Description("đợt báo cáo")]
        public string DOT_BAOCAO { get; set; }

    }
}
