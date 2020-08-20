using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.PHB.ENTITY.Sys
{
    public class SYS_DVQHNS_QUANLY : BaseEntity
    {
        [Column("MA_DVQHNS")]
        [StringLength(20)]
        [Description("Mã đơn vị quan hệ ngân sách ")]
        public string MA_DVQHNS { get; set; }

        [Column("TEN_DVQHNS")]
        [StringLength(240)]
        [Description("Tên đơn vị quan hệ ngân sách ")]
        public string TEN_DVQHNS { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(3)]
        [Description("Mã chương ")]
        public string MA_CHUONG { get; set; }

        [Column("MA_NGANHKT")]
        [StringLength(10)]
        [Description("Mã nganh KT ")]
        public string MA_NGANHKT { get; set; }

        [Column("MA_SUNGHIEP")]
        [StringLength(10)]
        [Description("Mã su nghiep ")]
        public string MA_SUNGHIEP { get; set; }

        [Column("TEN_SUNGHIEP")]
        [StringLength(100)]
        [Description("Ten su nghiep ")]
        public string TEN_SUNGHIEP { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(1)]
        [Description("Trạng thái sử dụng (A: Đang sử dụng; I: Không sử dụng) ")]
        public string TRANG_THAI { get; set; }

        [Column("NOI_MO_TK")]
        [StringLength(120)]
        public string NOI_MO_TK { get; set; }

        [Column("SO_TK")]
        [StringLength(120)]
        public string SO_TK { get; set; }

        [Column("MA_DBHC")]
        [StringLength(10)]
        public string MA_DBHC { get; set; }

        [Column("MA_DVQHNS_CHA")]
        [StringLength(20)]
        public string MA_DVQHNS_CHA { get; set; }
    }
}
