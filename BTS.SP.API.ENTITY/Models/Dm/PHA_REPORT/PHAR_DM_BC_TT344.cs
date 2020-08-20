using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Dm.PHA_REPORT
{
    [Table("PHAR_DM_BC_TT344")]
    public class PHAR_DM_BC_TT344 : DataInfoEntity
    {
        [Column("MA_BAOCAO")]
        [StringLength(100)]
        public string MA_BAOCAO { get; set; }

        [Column("TEN_BAOCAO")]
        [StringLength(200)]
        public string TEN_BAOCAO { get; set; }

        [Column("TRANGTHAI")]
        [Description("Trạng thái sử dụng (1: Đang sử dụng; 0: Không sử dụng)")]
        public int TRANGTHAI { get; set; }

        [Column("NAMBC")]
        [StringLength(20)]
        public string NAMBC { get; set; }

        [Column("BIEU_MAU")]
        [StringLength(200)]
        public string BIEU_MAU { get; set; }

        [Column("SO_QD")]
        [StringLength(100)]
        public string SO_QD { get; set; }

        [Column("NGAY_CB")]
        public Nullable<DateTime> NGAY_CB { get; set; }

        [Column("URL")]
        [StringLength(250)]
        public string URL { get; set; }

        [Column("FILENAME")]
        [StringLength(250)]
        public string FILENAME { get; set; }

        [Column("L_BAOCAO")]
        [StringLength(20)]
        public string L_BAOCAO { get; set; }

    }
}
