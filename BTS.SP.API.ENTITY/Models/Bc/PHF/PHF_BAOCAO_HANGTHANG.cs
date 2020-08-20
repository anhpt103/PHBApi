using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_BAOCAO_HANGTHANG")]
    public class PHF_BAOCAO_HANGTHANG : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("TEN_BAOCAO")]
        [Description("Tên báo cáo")]
        [StringLength(50)]
        public string TEN_BAOCAO { get; set; }

        [Column("MA_PHONGBAN")]
        [StringLength(50)]
        public string MA_PHONGBAN { get; set; }

        [Column("MA_DOITUONG")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("MA_CANBO")]
        [StringLength(50)]
        public string MA_CANBO { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        [StringLength(50)]
        public string NAM { get; set; }

        [Column("TU_NGAY")]
        [Description("Từ ngày")]
        public DateTime TUNGAY { get; set; }

        [Column("DEN_NGAY")]
        [Description("Đến ngày")]
        public DateTime DENNGAY { get; set; }

        [Column("MA_QUY")]
        [Description("Mã quý")]
        [StringLength(50)]
        public string MA_QUY { get; set; }

        [Column("TEN_QUY")]
        [Description("Tên quý")]
        [StringLength(50)]
        public string TEN_QUY { get; set; }
    }
}
