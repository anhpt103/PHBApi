using BTS.SP.API.ENTITY.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHC
{
    [Table("DM_TEST_DANHMUC")]
    public class DM_TEST_DANHMUC : DataInfoEntity
    {
        [Column("MA")]
        [StringLength(20)]
        [ExportExcel("Mã test")]
        public string MA { get; set; }

        [Column("TEN")]
        [StringLength(500)]
        [ExportExcel("Tên test")]
        public string TEN { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái sử dụng (A: Ðang sử dụng)")]
        [StringLength(1)]
        public string TRANG_THAI { get; set; }
    }
}
