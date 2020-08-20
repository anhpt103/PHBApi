using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("PHA_NHAPDUTOAN_CHUONG")]
    public class PHA_NHAPDUTOAN_CHUONG : DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("NAM")]
        public int NAM { get; set; }

        [Column("LOAI_CHITIEU")]
        [StringLength(50)]
        public string LOAI_CHITIEU { get; set; }
               
        [Column("MA_DBHC")]
        [StringLength(20)]
        public string MA_DBHC { get; set; }

        [Column("TRANG_THAI")]
        [Description("10: Đã duyệt | 0:chưa duyệt")]
        public int TRANG_THAI { get; set; }

        [Column("USER_NHAP")]
        [StringLength(50)]
        public string USER_NHAP { get; set; }

        [Column("NGAY_NHAP")]
        public DateTime? NGAY_NHAP { get; set; }

        [Column("USER_SUA")]
        [StringLength(50)]
        public string USER_SUA { get; set; }

        [Column("NGAY_SUA")]
        public DateTime? NGAY_SUA { get; set; }

        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("THOIGIAN_NHAP")]
        [Description("Thời gian nhận file DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN_NHAP { get; set; }

        [Column("THOIGIAN_SUA")]
        [Description("Thời gian sửa file DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN_SUA { get; set; }
    }
}
