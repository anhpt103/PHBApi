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
    [Table("PHF_HUONGDAN_CHIDAO")]
    public class PHF_HUONGDAN_CHIDAO : DataInfoEntityPHF
    {
        [Column("MA_VANBAN")]
        [StringLength(200)]
        [Description("Mã văn bản")]
        public string MA_VANBAN { get; set; }

        [Column("TEN_VANBAN")]
        [StringLength(500)]
        [Description("Tên văn bản")]
        public string TEN_VANBAN { get; set; }

        [Column("FILEDINHKEM")]
        [StringLength(1000)]
        [Description("File đính kèm")]
        public string FILEDINHKEM { get; set; }

        [Column("URL")]
        [Description("URL")]
        [StringLength(250)]
        public string URL { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime DENNGAY { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        public int NAM { get; set; }

        [Column("NGAY_HIEULUC")]
        [Description("Ngày hiệu lực văn bản")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime? NGAY_HIEULUC { get; set; }

        [Column("THOIGIAN_CAPNHAT")]
        [Description("Thời gian cập nhật văn bản DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN_CAPNHAT { get; set; }

        [Column("NGUOI_CAPNHAT")]
        [StringLength(500)]
        [Description("Người cập nhật văn bản")]
        public string NGUOI_CAPNHAT { get; set; }
    }
}
