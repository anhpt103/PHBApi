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
    [Table("PHF_BM_05TT_TCDN")]
    public class PHF_BM_05TT_TCDN : DataInfoEntityPHF
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

        [Column("NOIDUNG")]
        [StringLength(1000)]
        [Description("Nội dung")]
        public string NOIDUNG { get; set; }

        [Column("SOTIEN")]
        [StringLength(1000)]
        [Description("Số tiền vay")]
        public string SOTIEN { get; set; }

        [Column("THOIHAN")]
        [StringLength(1000)]
        [Description("Thời hạn trả")]
        public string THOIHAN { get; set; }

        [Column("NGUON_KHAUHAO")]
        [StringLength(1000)]
        [Description("Nguồn trả nợ Khấu hao")]
        public string NGUON_KHAUHAO { get; set; }

        [Column("NGUON_LOINHUAN")]
        [StringLength(1000)]
        [Description("Nguồn trả nợ Lợi nhuận")]
        public string NGUON_LOINHUAN { get; set; }

        [Column("NGUONKHAC")]
        [StringLength(1000)]
        [Description("Nguồn trả nợ Lợi nhuận")]
        public string NGUONKHAC { get; set; }

        [Column("THIEUNGUON_KHAUHAO")]
        [StringLength(1000)]
        [Description("Thiếu nguồn trả nợ Khấu hao")]
        public string THIEUNGUON_KHAUHAO { get; set; }

        [Column("THIEUNGUON_LOINHUAN")]
        [StringLength(1000)]
        [Description("Thiếu nguồn trả nợ Lợi nhuận")]
        public string THIEUNGUON_LOINHUAN { get; set; }

        [Column("THIEUNGUON_KHAC")]
        [StringLength(1000)]
        [Description("Thiếu nguồn trả nợ Khác")]
        public string THIEUNGUON_KHAC { get; set; }

        [Column("CHENHLECH")]
        [StringLength(1000)]
        [Description("Chênh lệch")]
        public string CHENHLECH { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
