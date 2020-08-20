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
    [Table("PHF_BM_07TT_TCDN")]
    public class PHF_BM_07TT_TCDN :DataInfoEntityPHF
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

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }


        [Column("TOCHUC_CANHAN")]
        [StringLength(200)]
        [Description("Tên tổ chức, cá nhân đầu tư")]
        public string TOCHUC_CANHAN { get; set; }

        [Column("QDPD_SO")]
        [StringLength(200)]
        [Description("Số")]
        public string QDPD_SO { get; set; }

        [Column("QDPD_NGAYTHANG")]
        [StringLength(200)]
        [Description("Ngày tháng")]
        public string QDPD_NGAYTHANG { get; set; }

        [Column("QDPD_SOTIEN")]
        [StringLength(200)]
        [Description("Số tiền")]
        public string QDPD_SOTIEN { get; set; }

        [Column("HOPDONG_SO")]
        [StringLength(200)]
        [Description("Số")]
        public string HOPDONG_SO { get; set; }

        [Column("HOPDONG_NGAYTHANG")]
        [StringLength(200)]
        [Description("Ngày tháng")]
        public string HOPDONG_NGAYTHANG { get; set; }

        [Column("HOPDONG_SOTIEN")]
        [StringLength(200)]
        [Description("Số tiền")]
        public string HOPDONG_SOTIEN { get; set; }

        [Column("THOIGIAN_GOP")]
        [StringLength(200)]
        [Description("Thời gian góp")]
        public string THOIGIAN_GOP { get; set; }

        [Column("THOIGIAN_GOPDEN")]
        [StringLength(200)]
        [Description("Thực góp đến 31/12/…")]
        public string THOIGIAN_GOPDEN { get; set; }

        [Column("TYLE_SHVGOP")]
        [StringLength(200)]
        [Description("Tỷ lệ sở hữu vốn góp")]
        public string TYLE_SHVGOP { get; set; }

        [Column("NGVG_COPHANHOA")]
        [StringLength(200)]
        [Description("Cổ phần hóa")]
        public string NGVG_COPHANHOA { get; set; }

        [Column("NGVG_THANHLAPMOI")]
        [StringLength(200)]
        [Description("Thành lập mới")]
        public string NGVG_THANHLAPMOI { get; set; }

        [Column("NGVG_KHAC")]
        [StringLength(200)]
        [Description("Khác")]
        public string NGVG_KHAC { get; set; }

        [Column("DOANHTHU_THUNHAP")]
        [StringLength(200)]
        [Description("Doanh thu & thu nhập khác năm….")]
        public string DOANHTHU_THUNHAP { get; set; }

        [Column("LOINHUAN_THUCHIEN")]
        [StringLength(200)]
        [Description("Lợi nhuận thực hiện năm….")]
        public string LOINHUAN_THUCHIEN { get; set; }

        [Column("CTLN_NAM")]
        [StringLength(200)]
        [Description("Năm…")]
        public string CTLN_NAM { get; set; }

        [Column("CTLN_LUYKE")]
        [StringLength(200)]
        [Description("Lũy kế")]
        public string CTLN_LUYKE { get; set; }

        [Column("DUPHONG_GIAMGIA")]
        [StringLength(200)]
        [Description("Lập dự phòng giảm giá đầu tư")]
        public string DUPHONG_GIAMGIA { get; set; }

    }
}
