using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_DINHKEMFILE3")]
    public class PHF_DINHKEMFILE3: DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("TENBAOCAO")]
        [StringLength(500)]
        [Description("Tên báo cáo")]
        public string TENBAOCAO { get; set; }

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
