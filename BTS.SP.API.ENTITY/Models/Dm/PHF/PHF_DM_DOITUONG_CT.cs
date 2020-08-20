using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_DOITUONG_CT")]
    public class PHF_DM_DOITUONG_CT : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_DOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MA_DOITUONG { get; set; }

        [Required]
        [Column("MA_DOITUONG_LQ")]
        [StringLength(50)]
        [Description("Mã đối tượng liên quan")]
        public string MA_DOITUONG_LQ { get; set; }

        [Required]
        [Column("TEN_DOITUONG_LQ")]
        [StringLength(500)]
        [Description("Tên đối tượng liên quan")]
        public string TEN_DOITUONG_LQ { get; set; }
    }
}
