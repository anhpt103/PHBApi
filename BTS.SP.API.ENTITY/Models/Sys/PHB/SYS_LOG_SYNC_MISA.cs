using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Sys.PHB
{
    public class SYS_LOG_SYNC_MISA : DataInfoEntity
    {
        [Column("THOIGIAN_LOG")]
        [Description("Thời gian ghi log ")]
        public DateTime THOIGIAN_LOG { get; set; }

        [Column("TEN_UNGDUNG")]
        [StringLength(30)]
        [Description("Tên ứng dụng: MISA OR MIMOSA... ")]
        public string TEN_UNGDUNG { get; set; }

        [Column("NOIDUNG_LOG")]
        [StringLength(4000)]
        [Description("Nội dung log ")]
        public string NOIDUNG_LOG { get; set; }

        [Column("TYPE_ERROR")]
        [StringLength(30)]
        [Description("Loại lỗi")]
        public string TYPE_ERROR { get; set; }
    }
}
