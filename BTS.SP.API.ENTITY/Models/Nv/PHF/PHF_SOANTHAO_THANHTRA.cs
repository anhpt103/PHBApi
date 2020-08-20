using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_SOANTHAO_THANHTRA")]
    public class PHF_SOANTHAO_THANHTRA : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }

        [Column("NAM")]
        public int NAM { get; set; }

        [Column("MA_PHONGBAN")]
        [StringLength(50)]
        public string MA_PHONGBAN { get; set; }

        [Column("NOI_DUNG")]
        [StringLength(500)]
        public string NOI_DUNG { get; set; }

        [Column("MA_DOITUONG")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime DENNGAY { get; set; }

        [Column("QUY")]
        [Description("Quý")]
        [StringLength(50)]
        public string QUY { get; set; }

        [Column("TENQUY")]
        [Description("Tên quý")]
        [StringLength(50)]
        public string TENQUY { get; set; }

        [Column("DINHKEMFILE")]
        [StringLength(200)]
        [Description("File đính kèm")]
        public string DINHKEMFILE { get; set; }

        [Column("URL")]
        [StringLength(250)]
        [Description("Đường dẫn để lưu tệp upload")]
        public string URL { get; set; }
    }
}
