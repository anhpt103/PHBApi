using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_BAOCAO_DONG")]
    public class PHF_DM_BAOCAO_DONG : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(50)]
        [Required]
        public string MABAOCAO { get; set; }

        [Column("MACOT")]
        [StringLength(50)]
        public string MACOT { get; set; }

        [Column("MADONG")]
        [StringLength(500)]
        public string MADONG { get; set; }

        [Column("TENDONG")]
        [StringLength(2000)]
        public string TENDONG { get; set; }

        [Column("SOTHUTU")]
        [Description("Số thứ tự")]
        public int SOTHUTU { get; set; }

        [Column("SOTHUTU_HIENTHI")]
        [StringLength(100)]
        [Description("Số thứ tự hiển thị")]
        public string SOTHUTU_HIENTHI { get; set; }

        [Column("DINH_DANG")]
        [StringLength(50)]
        [Description("Định dạng")]
        public string DINH_DANG { get; set; }


    }
}
