using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_BAOCAO")]
    public class PHF_DM_BAOCAO : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(50)]
        [Required]
        public string MABAOCAO { get; set; }

        [Column("TENBAOCAO")]
        [StringLength(250)]
        [Required]
        public string TEN_BAO_CAO { get; set; }

        [Column("MOTA")]
        [StringLength(500)]
        public string MOTA { get; set; }

        [Column("NAM")]
        public int NAM { get; set; }

        [Column("TRANGTHAI")]
        [Description("Trạng thái")]
        public Nullable<int> TRANGTHAI { get; set; }

        [Column("STATUS")]
        [Description("Trạng thái")]
        public Nullable<int> STATUS { get; set; }

        [Column("MA_PHONGBAN")]
        [Description("Mã phòng ban")]
        [StringLength(50)]
        public string MA_PHONGBAN { get; set; }
    }
}
