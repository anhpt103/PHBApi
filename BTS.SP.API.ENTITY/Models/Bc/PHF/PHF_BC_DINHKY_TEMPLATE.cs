using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_BC_DINHKY_TEMPLATE")]
    public class PHF_BC_DINHKY_TEMPLATE : DataInfoEntityPHF
    {
        [Column("STT")]
        [StringLength(50)]
        [Description("STT")]
        public string STT { get; set; }

        [Column("TIEU_CHI")]
        [StringLength(200)]
        [Description("Các Tiêu Chí")]
        public string TIEU_CHI { get; set; }

        [Column("NOI_DUNG")]
        [StringLength(300)]
        [Description("Nội Dung")]
        public string NOI_DUNG { get; set; }

        [Column("DINH_DANG")]
        [StringLength(500)]
        [Description("Định Dạng")]
        public string DINH_DANG { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime DENNGAY { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        [StringLength(50)]
        public string NAM { get; set; }

        [Column("QUY")]
        [Description("Quý")]
        [StringLength(50)]
        public string QUY { get; set; }

        [Column("TENQUY")]
        [Description("Tên quý")]
        [StringLength(50)]
        public string TENQUY { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("THOIGIAN")]
        [Description("Thời gian nhận file DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN { get; set; }
    }
}
