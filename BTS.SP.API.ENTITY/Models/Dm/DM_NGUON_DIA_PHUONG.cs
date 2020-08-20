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
    [Table("DM_NGUON_DIA_PHUONG")]
    public class DM_NGUON_DIA_PHUONG : DataInfoEntity
    {
        [Column("MA_NGUON_DIA_PHUONG")]
        [StringLength(50)]
        [Description("Mã du phong ")]
        public string MA_NGUON_DIA_PHUONG { get; set; }

        [Column("TEN_NGUON_DIA_PHUONG")]
        [StringLength(500)]
        [Description("tên du phong ")]
        public string TEN_NGUON_DIA_PHUONG { get; set; }

        [Column("NGAY_HL")]
        [Description("Ngày hiệu lực ")]
        public DateTime NGAY_HL { get; set; }

        [Column("NGAY_HET_HL")]
        [Description("Ngày hết hiệu lực")]
        public Nullable<DateTime> NGAY_HET_HL { get; set; }

        [Column("LOAI_NGUON")]
        [StringLength(50)]
        [Description("Loại nguồn ")]
        public string LOAI_NGUON { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(50)]
        [Description("Trạng Thái ")]
        public string TRANG_THAI { get; set; }

        [Column("MA_NGUON_CHA")]
        [StringLength(50)]
        [Description("Mã nguồn cha ")]
        public string MA_NGUON_CHA { get; set; }


        [Column("GHI_CHU")]
        [StringLength(50)]
        [Description("Ghi Chú ")]
        public string GHI_CHU { get; set; }

        [Column("USER_NAME")]
        [StringLength(50)]
        [Description("user name")]
        public string USER_NAME { get; set; }

        [Column("NGAY_PS")]
        [Description("Ngày ps")]
        public Nullable<DateTime> NGAY_PS { get; set; }

        [Column("NGAY_SD")]
        [Description("Ngày sử dụng")]
        public Nullable<DateTime> NGAY_SD { get; set; }
    }
}

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
