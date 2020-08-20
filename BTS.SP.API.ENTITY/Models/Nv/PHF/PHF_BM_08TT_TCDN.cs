using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_08TT_TCDN")]
    public class PHF_BM_08TT_TCDN :DataInfoEntityPHF
    {

        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }

        [Column("DOITUONG_NO")]
        [StringLength(200)]
        [Description("Đối tượng nợ")]
        public string DOITUONG_NO { get; set; }

        [Column("TONGNO_PHAITHU")]
        [StringLength(200)]
        [Description("Tổng nợ phải thu tại 31 / 12 /…")]
        public string TONGNO_PHAITHU { get; set; }

        [Column("TREN_6THANG")]
        [StringLength(200)]
        [Description("Trên 6 tháng- dưới 1 năm")]
        public string TREN_6THANG { get; set; }

        [Column("TREN_1NAM")]
        [StringLength(200)]
        [Description("Trên 1 năm- dưới 2 năm")]
        public string TREN_1NAM { get; set; }

        [Column("TU_2NAM")]
        [StringLength(200)]
        [Description("Từ 2 năm - dưới 3 năm")]
        public string TU_2NAM { get; set; }

        [Column("TREN_3NAM")]
        [StringLength(200)]
        [Description("Trên 3 năm")]
        public string TREN_3NAM { get; set; }

        [Column("TONGCONG")]
        [StringLength(200)]
        [Description("Tổng cộng")]
        public string TONGCONG { get; set; }

        [Column("TRICHLAP_DUPHONG")]
        [StringLength(200)]
        [Description("Trích lập dự phòng")]
        public string TRICHLAP_DUPHONG { get; set; }

        [Column("XULY_NO")]
        [StringLength(200)]
        [Description("Xử lý nợ")]
        public string XULY_NO { get; set; }        

    }
}
