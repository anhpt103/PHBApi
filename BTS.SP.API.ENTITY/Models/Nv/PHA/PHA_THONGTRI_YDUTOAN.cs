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
    [Table("PHA_THONGTRI_YDUTOAN")]
    public class PHA_THONGTRI_YDUTOAN : DataInfoEntity
    {
        [Column("REF_ID")]
        [StringLength(100)]
        public string REF_ID { get; set; }

        [Column("SO_TT")]
        [StringLength(10)]
        public string SO_TT { get; set; }

        [Column("NGAY_TT")]
        public DateTime? NGAY_TT { get; set; }

        [Column("MA_LNV")]
        [StringLength(10)]
        public string MA_LNV { get; set; }

        [Column("TEN_LNV")]
        [StringLength(100)]
        public string TEN_LNV { get; set; }

        [Column("MA_CQTC")]
        [StringLength(10)]
        public string MA_CQTC { get; set; }

        [Column("TEN_CQTC")]
        [StringLength(100)]
        public string TEN_CQTC { get; set; }

        [Column("MA_DVSDNS")]
        [StringLength(10)]
        public string MA_DVSDNS { get; set; }

        [Column("TEN_DVSDNS")]
        [StringLength(100)]
        public string TEN_DVSDNS { get; set; }

        [Column("MA_CTMT")]
        [StringLength(10)]
        public string MA_CTMT { get; set; }

        [Column("TEN_CTMT")]
        [StringLength(100)]
        public string TEN_CTMT { get; set; }

        [Column("MA_DBHC")]
        [StringLength(10)]
        public string MA_DBHC { get; set; }

        [Column("NOI_MO_TK")]
        [StringLength(250)]
        public string NOI_MO_TK { get; set; }

        [Column("SO_TK")]
        [StringLength(100)]
        public string SO_TK { get; set; }

        [Column("TRONG_DUTOAN")]
        [StringLength(1)]
        public string TRONG_DUTOAN { get; set; }

        [Column("NIEN_DO")]
        [StringLength(5)]
        public string NIEN_DO { get; set; }

        [Column("MAU_THONGTRI")]
        [StringLength(3)]
        public string MAU_THONGTRI { get; set; }

        [Column("NOI_DUNG")]
        [StringLength(250)]
        public string NOI_DUNG { get; set; }

        [Column("CHI_DAN")]
        [StringLength(500)]
        public string CHI_DAN { get; set; }

        [Column("DONVI")]
        [StringLength(200)]
        public string DONVI { get; set; }

        [Column("NGAY_TAO")]
        public DateTime? NGAY_TAO { get; set; }

        [Column("NGUOI_TAO")]
        [StringLength(200)]
        public string NGUOI_TAO { get; set; }

        [Column("TRANG_THAI")]
        public int TRANG_THAI { get; set; }

        [Column("TONG_TIEN")]
        public decimal TONG_TIEN { get; set; }

        [Column("CHUYEN_VIEN")]
        [StringLength(250)]
        public string CHUYEN_VIEN { get; set; }

        [Column("TRUONG_PHONG")]
        [StringLength(250)]
        public string TRUONG_PHONG { get; set; }

        [Column("TRUONG_PHONG_NS")]
        [StringLength(250)]
        public string TRUONG_PHONG_NS { get; set; }

        [Column("GIAM_DOC")]
        [StringLength(250)]
        public string GIAM_DOC { get; set; }

        [Column("CHUONG")]
        [StringLength(5)]
        public string CHUONG { get; set; }

        [Column("KHOAN")]
        [StringLength(5)]
        public string KHOAN { get; set; }

        [Column("TIEU_MUC")]
        [StringLength(5)]
        public string TIEU_MUC { get; set; }
    }
}
