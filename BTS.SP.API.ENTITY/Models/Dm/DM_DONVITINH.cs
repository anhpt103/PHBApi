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
    [Table("DM_DONVITINH")]
    public class DM_DONVITINH:DataInfoEntity
    {
        [Column("MA_DVT")]
        [StringLength(10)]
        public string MA_DVT { get; set; }
       
        [Column("TEN_DVT")]
        [StringLength(500)]
        public string TEN_DVT { get; set; }
       
        [Column("USER_NAME")]
        [StringLength(20)]
        public string USER_NAME { get; set; }

        [Column("NGAY_HL")]
        public DateTime NGAY_HL { get; set; }

        [Column("NGAY_HET_HL")]
        [Description("Ngày hết hiệu lực")]
        public Nullable<DateTime> NGAY_HET_HL { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái sử dụng (A: Đang sử dụng; I: Không sử dụng)")]
        [StringLength(1)]
        public string TRANG_THAI { get; set; }
    }
}
