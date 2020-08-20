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
    [Table("PHF_BIEU03_CONGTY_TCDN")]
    public class PHF_BIEU03_CONGTY_TCDN : DataInfoEntityPHF
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

        [Column("TEN_DONVI")]
        [StringLength(1000)]
        [Description("Tên đơn vị")]
        public string TEN_DONVI { get; set; }

        [Column("MASO_THUE")]
        [StringLength(1000)]
        [Description("Mã số thuế")]
        public string MASO_THUE { get; set; }

        [Column("GIAM_DOC")]
        [StringLength(1000)]
        [Description("Giám đốc")]
        public string GIAM_DOC { get; set; }

        [Column("KETOAN_TRUONG")]
        [StringLength(1000)]
        [Description("Kế toán trưởng")]
        public string KETOAN_TRUONG { get; set; }

        [Column("DIEN_THOAI")]
        [StringLength(1000)]
        [Description("DIEN_THOAI")]
        public string NOIDUNGSAI { get; set; }

        [Column("DIA_CHI")]
        [StringLength(1000)]
        [Description("Địa chỉ")]
        public string DIA_CHI { get; set; }

        [Column("NGANHNGHE_KINHDOANH")]
        [StringLength(1000)]
        [Description("Ngành nghề kinh doanh")]
        public string NGANHNGHE_KINHDOANH { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
