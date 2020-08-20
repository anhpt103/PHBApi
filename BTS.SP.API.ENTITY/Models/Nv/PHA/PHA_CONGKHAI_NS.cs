using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHA
{
    [Table("PHA_CONGKHAI_NS")]
    public class PHA_CONGKHAI_NS : DataInfoEntity
    {
        [Column("MABAOCAO")]
        [StringLength(20)]
        public string MABAOCAO { get; set; }

        [Column("TENBAOCAO")]
        [StringLength(150)]
        public string TENBAOCAO { get; set; }

        [Column("LINHVUC")]
        [StringLength(250)]
        public string LINHVUC { get; set; }

        [Column("TUNGAY_BC")]
        public DateTime? TUNGAY_BC { get; set; }

        [Column("DENGAY_BC")]
        public DateTime? DENGAY_BC { get; set; }

        [Column("SO_QUYETDINH")]
        [StringLength(50)]
        public string SO_QUYETDINH { get; set; }

        [Column("NGAY_CONGBO")]
        public DateTime? NGAY_CONGBO { get; set; }

        [Column("NGAY_HIEULUC")]
        public DateTime? NGAY_HIEULUC { get; set; }

        [Column("TRICHYEU_NOIDUNG")]
        [StringLength(500)]
        public string TRICHYEU_NOIDUNG { get; set; }

        [Column("NGUOI_KYDUYET")]
        [StringLength(150)]
        public string NGUOI_KYDUYET { get; set; }

        [Column("DUONGDAN_FILE")]
        [StringLength(250)]
        public string DUONGDAN_FILE { get; set; }

        [Column("FILEDINHKEM")]
        [StringLength(250)]
        public string FILEDINHKEM { get; set; }

        [Column("TRANG_THAI")]
        [Description("A: Đã duyệt | I:chưa duyệt")]
        public string TRANG_THAI { get; set; }

    }
}
