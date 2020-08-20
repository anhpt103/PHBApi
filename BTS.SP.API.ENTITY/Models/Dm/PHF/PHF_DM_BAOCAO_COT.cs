using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_BAOCAO_COT")]
    public class PHF_DM_BAOCAO_COT : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(50)]
        [Required]
        public string MABAOCAO { get; set; }

        [Column("MACOT")]
        [StringLength(50)]
        [Required]
        public string MACOT { get; set; }

        [Column("TENCOT")]
        [StringLength(1000)]
        [Required]
        public string TENCOT { get; set; }

        [Column("KIEUDULIEU")]
        [Description("Kiểu dữ liệu")]
        [StringLength(30)]
        public string KIEUDULIEU { get; set; }

        [Column("DODAI")]
        [Description("Độ dài cột")]
        public int DODAI { get; set; }

        [Column("SOTHUTU")]
        [Description("Số thứ tự")]
        public int SOTHUTU { get; set; }
    }
}
