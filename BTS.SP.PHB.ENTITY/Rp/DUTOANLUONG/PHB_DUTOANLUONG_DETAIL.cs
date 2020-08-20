
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.DUTOANLUONG
{
    public class PHB_DUTOANLUONG_DETAIL : BaseEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_DUTOANLUONG")]
        [StringLength(50)]
        public string PHB_DUTOANLUONG_REFID { get; set; }

        [Description("STT")]
        public int STT { get; set; }


        [Description("Số thứ tự chỉ tiêu")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }

        [Description("Mã chỉ tiêu")]
        [StringLength(50)]
        public string MA_CHI_TIEU { get; set; }

        [Description("Tên chỉ tiêu")]
        [StringLength(1000)]
        public string TEN_CHI_TIEU { get; set; }

        [Description("biến chế Được cấp có thẩm quyền giao")]
        public double BC_DUOC_CAP { get; set; }

        [Description("biến chế Có mặt ")]
        public double BC_CO_MAT { get; set; }

        [Description("Mã ngạch ")]
        [StringLength(200)]
        public string MA_NGACH { get; set; }

        [Description("Hệ số lương ")]
        public double HS_LUONG { get; set; }

        [Description("Hệ số HS_PCCV ")]
        public double HS_PC_CV { get; set; }

        [Description("Hệ số HS_PC_KV ")]
        public double HS_PC_KV{ get; set; }

        [Description("Hệ số PHU CAP THU HUT ")]
        public double HS_PC_TH{ get; set; }

        [Description("Hệ số LAM THEM ")]
        public double HS_PC_LT { get; set; }

        [Description("Hệ số NANG NHOC DOC HAI ")]
        public double HS_PC_NN_DH { get; set; }

        [Description("Hệ số hoạt động phí đại biểu Quốc hội, đại biểu HĐND ")]
        public double HS_HD_DBQH_DBND { get; set; }

        [Description("Hệ số phụ cấp ưu đãi nghề ")]
        public double HS_PC_UDN { get; set; }

        [Description("Hệ số phụ cấp thâm niên nghề ")]
        public double HS_PC_TNN { get; set; }

        [Description("Hệ số phụ cấp trách nhiệm theo nghề, theo công việc ")]
        public double HS_PC_TN_NGHE_CV { get; set; }

        [Description("Hệ số phụ cấp trực ")]
        public double HS_PC_TRUC { get; set; }

        [Description("Hệ số phụ cấp Thâm niên vượt khung ")]
        public double HS_PC_TN_VUOT_KHUNG { get; set; }

        [Description("Hệ số phụ cấp đặc biệt khác của ngành ")]
        public double HS_PC_DB_KHAC { get; set; }

        [Description("Hệ số phụ cấp công tác lâu năm ở vùng có điều kiện kinh tế - xã hội đặc biệt khó khăn ")]
        public double HS_PC_CT_LN { get; set; }

        [Description("Hệ số phụ cấp theo loại xã ")]
        public double HS_PC_LX { get; set; }

        [Description("Hệ số phụ cấp công tác Đảng, Đoàn thể chính trị - xã hội ")]
        public double HS_PC_CT_D_DTCT_XH { get; set; }

        [Description("Hệ số phụ cấp công vụ ")]
        public double HS_PC_CVU { get; set; }

        [Description("Hệ số phụ cấp khác ")]
        public double HS_PC_KHAC { get; set; }

        [Description("Cộng hệ số ")]
        public double CONG_HS { get; set; }

        [Description("Tiền lương tháng ")]
        public double TIEN_LUONG_THANG { get; set; }

        [Description("Trích vào CP ")]
        public double BHXH_CP { get; set; }

        [Description("Trừ vào lương ")]
        public double BHXH_LUONG { get; set; }

        [Description("Trích vào CP ")]
        public double BHYT_CP { get; set; }

        [Description("Trừ vào lương ")]
        public double BHYT_LUONG { get; set; }

        [Description("Trích vào CP ")]
        public double BHTN_CP { get; set; }

        [Description("Trừ vào lương ")]
        public double BHTN_LUONG { get; set; }

        [Description("Trích vào CP ")]
        public double BH_TNLD_BNN_CP { get; set; }

        [Description("Trừ vào lương ")]
        public double BH_TNLD_BNN_LUONG { get; set; }

        [Description("Trích vào CP ")]
        public double KPCD_CP { get; set; }

        [Description("Trừ vào lương ")]
        public double KPCD_LUONG { get; set; }

        [Description("Số phải nộp công đoàn cấp trên ")]
        public double KPCD_NOP_CD { get; set; }

        [Description("Số để lại chi đơn vị ")]
        public double KPCD_DE_LAI_DV { get; set; }

        [Description("Thuế thu nhập cá nhân ")]
        public double THUE_TNCN { get; set; }

        [Description("Giảm trừ gia cảnh ")]
        public double GT_DC { get; set; }

        [Description("Số thực lĩnh ")]
        public double SO_THUC_LINH { get; set; }

        [Description("GHI_CHI")]
        [StringLength(500)]
        public string GHI_CHI { get; set; }



    }
}
