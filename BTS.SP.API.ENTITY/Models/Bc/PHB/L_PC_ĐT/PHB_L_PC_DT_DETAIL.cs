using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.L_PC_DT
{
    [Table("PHB_L_PC_DT_DETAIL")]
    public class PHB_L_PC_DT_DETAIL : DataInfoEntity
    {
        [Column("MA_BAOCAO_CHA")]
        [StringLength(50)]
        public string MA_BAOCAO_CHA { get; set; }

        [Description("Guid ID trong PHB_L_PC_DT")]
        [StringLength(50)]
        public string PHB_L_PC_DT_REFID { get; set; }

        [Description("Loại tài khoản: 1 TRONG BẢNG , 2 NGOÀI BẢNG")]
        public int LOAI { get; set; }

        [Column("STT")]
        [Description("Số thứ tự")]
        [StringLength(5)]
        public string STT { get; set; }

        [Description("Họ và tên")]
        [Required]
        [StringLength(50)]
        public string HO_VATEN { get; set; }

        [Description("Chức danh")]
        [Required]
        [StringLength(250)]
        public string CHUC_DANH { get; set; }

        [Description("Hệ số lương")]
        public decimal? HE_SOLUONG { get; set; }

        [Description("Phụ cấp khu vực")]
        public decimal? PC_KV { get; set; }

        [Description("Phụ cấp chức vụ")]
        public decimal? PC_CHUCVU { get; set; }

        [Description("Phụ cấp thâm niên")]
        public decimal? PC_THAMNIEN { get; set; }

        [Description("Phụ cấp trách nhiệm")]
        public decimal? PC_TRACHNHIEM { get; set; }

        [Description("Phụ cấp công vụ 25%")]
        public decimal? PC_CONGVU { get; set; }

        [Description("Phụ cấp loại xã 10%")]
        public decimal? PC_LOAIXA { get; set; }

        [Description("Phụ cấp lâu năm")]
        public decimal? PC_LAUNAM { get; set; }

        [Description("Phụ cấp thu hút 70%")]
        public decimal? PC_THUHUT { get; set; }

        [Description("Các khoản phải trừ 8% BHXH")]
        public decimal? CKPT_BHXH { get; set; }

        [Description("Các khoản phải trừ 8% BHYT")]
        public decimal? CKPT_BHYT { get; set; }
    }
}
