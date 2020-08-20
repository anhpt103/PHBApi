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
    [Table("PHF_TT03_BIEU3A")]
    public class PHF_TT03_BIEU3A : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("MS")]
        [StringLength(50)]
        [Description("MS")]
        public string MS { get; set; }

        [Column("NOIDUNG")]
        [StringLength(1000)]
        [Description("Nội dung")]
        public string NOIDUNG { get; set; }

        [Column("DONVI_TINH")]
        [StringLength(50)]
        [Description("Đơn vị tính")]
        public string DONVI_TINH { get; set; }

        [Column("SOLIEU")]
        [Description("Số liệu")]
        public int? SOLIEU { get; set; }

        [Column("ISBOLD")]
        public int? ISBOLD { get; set; }

        [Column("ISITALIC")]
        public int? ISITALIC { get; set; }

        [Column("SAPXEP")]
        public int? SAPXEP { get; set; }
    }
}
