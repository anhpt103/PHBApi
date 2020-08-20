using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_NHAPBAOCAO_CHITIET")]
    public class PHF_NHAPBAOCAO_CHITIET : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã báo cáo cha")]
        public string MA_FILE_PK { get; set; }

        [Column("MACOT")]
        [StringLength(50)]
        [Required]
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
        [Description("Số thứ tự hiển thị")]
        public string SOTHUTU_HIENTHI { get; set; }

        [Column("DINH_DANG")]
        [StringLength(50)]
        [Description("Định dạng")]
        public string DINH_DANG { get; set; }

        [Column("NOI_DUNG_STRING")]
        [StringLength(1000)]
        [Description("Nội dung string")]
        public string NOI_DUNG_STRING { get; set; }

        [Column("NOI_DUNG_INT")]
        [Description("Nội dung int")]
        public int? NOI_DUNG_INT { get; set; }
    }
}
