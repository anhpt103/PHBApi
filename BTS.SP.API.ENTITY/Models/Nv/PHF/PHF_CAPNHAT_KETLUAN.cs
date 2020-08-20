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
    [Table("PHF_CAPNHAT_KETLUAN")]
    public class PHF_CAPNHAT_KETLUAN : DataInfoEntityPHF
    {
        [Column("MA_KLKT")]
        [StringLength(50)]
        [Description("Mã kết luận kiểm tra")]
        public string MA_KLKT { get; set; }

        [Column("NGAY_LAP")]
        public DateTime? NGAY_LAP { get; set; }

        [Column("NGUOI_LAP")]
        [StringLength(500)]
        [Description("Người lập")]
        public string NGUOI_LAP { get; set; }

        [Column("MA_DOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MA_DOITUONG { get; set; }

        [Column("TEN_DOITUONG")]
        [StringLength(500)]
        [Description("Tên đối tượng")]
        public string TEN_DOITUONG { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        [StringLength(50)]
        public string NAM { get; set; }

        [Column("TRUONG_DOAN")]
        [StringLength(500)]
        [Description("Trưởng đoàn")]
        public string TRUONG_DOAN { get; set; }

        [Column("PHONG_TT")]
        [StringLength(500)]
        public string PHONG_TT { get; set; }

        [Column("SO_KLKT")]
        [StringLength(200)]
        public string SO_KLKT { get; set; }

        [Column("NGAY_KLKT")]
        public DateTime? NGAY_KLKT { get; set; }

        [Column("DINH_KEMFILE")]
        [StringLength(200)]
        [Description("Đính kèm file")]
        public string DINH_KEMFILE { get; set; }
    }
}
