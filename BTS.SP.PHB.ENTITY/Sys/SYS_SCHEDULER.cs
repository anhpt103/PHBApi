using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.PHB.ENTITY.Sys
{
    public class SYS_SCHEDULER : BaseEntity
    {
        [Column("TEN_UNGDUNG")]
        [StringLength(30)]
        [Description("Tên ứng dụng: MISA OR MIMOSA... ")]
        public string TEN_UNGDUNG { get; set; }

        [Column("TIME_PERIOD")]
        [Description("Chu kỳ chạy đồng bộ dữ liệu ")]
        public int TIME_PERIOD { get; set; }
    }
}
