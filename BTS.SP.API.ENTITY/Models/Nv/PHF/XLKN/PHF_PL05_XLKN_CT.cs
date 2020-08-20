using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL05_XLKN_CT")]
    public class PHF_PL05_XLKN_CT : DataInfoEntityPHF
    {
        [Column("MA_FILE")]
        [StringLength(100)]
        [Description("Mã báo cáo")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("STT")]
        [Description("Số thứ tự")]
        [StringLength(200)]
        public string STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        [StringLength(200)]
        public string STT_CHA { get; set; }

        [Column("THOIGIAN_DKTK")]
        [StringLength(500)]
        [Description("Thời gian dự kiến triển khai")]
        public string THOIGIAN_DKTK { get; set; }

        [Column("DOITUONG")]
        [StringLength(500)]
        [Description("Đối tượng thanh tra")]
        public string DOITUONG { get; set; }

        [Column("NOIDUNG")]
        [StringLength(500)]
        [Description("Nội dung thanh tra")]
        public string NOIDUNG { get; set; }

        [Column("TONGSONGUOI_DK")]
        [StringLength(500)]
        [Description("Dự kiến số người tham gia - Tổng số (người)")]
        public string TONGSONGUOI_DK { get; set; }

        [Column("CANBOPHONG_DK")]
        [StringLength(500)]
        [Description("Dự kiến số người tham gia - Trong đó cán bộ của phòng (người)")]
        public string CANBOPHONG_DK { get; set; }

        [Column("GHI_CHU")]
        [StringLength(500)]
        [Description("Ghi chú")]
        public string GHI_CHU { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
