using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Dm
{
    [Table("DM_MALENHNV")]
    public class DM_MALENHNV : DataInfoEntity
    {
        [Column("MA_LNV")]
        [StringLength(100)]
        public string MA_LNV { get; set; }

        [Column("TEN_LNV")]
        [StringLength(100)]
        public string TEN_LNV { get; set; }
        [Column("GHI_CHU")]
        [StringLength(375)]
        public string GHI_CHU { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái sử dụng (A: Đang sử dụng; I: Không sử dụng)")]
        [StringLength(1)]
        public string TRANG_THAI { get; set; }

    }
}
