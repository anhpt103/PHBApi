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
    [Table("PHF_BM_10_THUYETMINH_CQHC")]
    public class PHF_BM_10_THUYETMINH_CQHC: DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_HANGHOA")]
        [StringLength(1000)]
        [Description("Tên hàng, mô tả hàng hoá theo khai báo")]
        public string TEN_HANGHOA { get; set; }

        [Column("MASO_CHUGIAI_HS_KHAIBAO")]
        [StringLength(1000)]
        [Description("Mã số HS khai báo (thuế suất thuế NK) Chú giải HS của mã số HS khai báo")]
        public string MASO_CHUGIAI_HS_KHAIBAO { get; set; }

        [Column("MASO_CHUGIAI_HS_PHUHOP")]
        [StringLength(1000)]
        [Description("Mã số HS phù hợp (thuế suất thuế NK) và Chú giải HS ")]
        public string MASO_CHUGIAI_HS_PHUHOP { get; set; }
    }
}
