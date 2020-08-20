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
    [Table("PHF_BM_06TT_TCDN")]
    public class PHF_BM_06TT_TCDN : DataInfoEntityPHF
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

        [Column("QUYETDINH_PHEDUYET")]
        [StringLength(200)]
        [Description("Quyết định phê duyệt dự án đầu tư")]
        public string QUYETDINH_PHEDUYET { get; set; }

        [Column("TMDTPD_TONGMUC")]
        [StringLength(50)]
        [Description("Tổng mức")]
        public string TMDTPD_TONGMUC { get; set; }

        [Column("TMDTPD_TUCO")]
        [StringLength(50)]
        [Description("Tự có")]
        public string TMDTPD_TUCO { get; set; }


        [Column("TMDTPD_NSCAP")]
        [StringLength(50)]
        [Description("NS cấp")]
        public string TMDTPD_NSCAP { get; set; }


        [Column("TMDTPD_VAYUUDAI")]
        [StringLength(50)]
        [Description("Vay ưu đãi")]
        public string TMDTPD_VAYUUDAI { get; set; }


        [Column("TMDTPD_VAYNGANHANG")]
        [StringLength(50)]
        [Description("Vay ngân hàng")]
        public string TMDTPD_VAYNGANHANG { get; set; }


        [Column("TMDTPD_KHAC")]
        [StringLength(50)]
        [Description("Khác")]
        public string TMDTPD_KHAC { get; set; }

        [Column("GIATRI_KLNT")]
        [StringLength(50)]
        [Description("Giá trị khối lượng nghiệm thu")]
        public string GIATRI_KLNT { get; set; }

        [Column("THGN__TONGSO")]
        [StringLength(50)]
        [Description("Tổng số")]
        public string THGN__TONGSO { get; set; }

        [Column("THGN__TAMUNG_CHUDAUTU")]
        [StringLength(50)]
        [Description("Chủ đầu tư")]
        public string THGN__TAMUNG_CHUDAUTU { get; set; }

        [Column("THGN__TAMUNG_NHATHAU")]
        [StringLength(50)]
        [Description("Nhà thầu")]
        public string THGN__TAMUNG_NHATHAU { get; set; }

        [Column("THGN_THANHTOAN_TUCO")]
        [StringLength(50)]
        [Description("Tự có")]
        public string THGN_THANHTOAN_TUCO { get; set; }

        [Column("THGN_THANHTOAN_NSCAP")]
        [StringLength(50)]
        [Description("NS cấp")]
        public string THGN_THANHTOAN_NSCAP { get; set; }

        [Column("THGN_THANHTOAN_VAYNGANHANG")]
        [StringLength(50)]
        [Description("Vay ngân hàng")]
        public string THGN_THANHTOAN_VAYNGANHANG { get; set; }

        [Column("THGN_THANHTOAN_KHAC")]
        [StringLength(50)]
        [Description("Khác")]
        public string THGN_THANHTOAN_KHAC { get; set; }

        [Column("TDTHDA_PHEDUYET_KHOICONG")]
        [StringLength(50)]
        [Description("Khởi công")]
        public string TDTHDA_PHEDUYET_KHOICONG { get; set; }

        [Column("TDTHDA_PHEDUYET_HOANTHANH")]
        [StringLength(50)]
        [Description("Hoàn thành")]
        public string TDTHDA_PHEDUYET_HOANTHANH { get; set; }

        [Column("TDTHDA_THUCTE_KHOICONG")]
        [StringLength(50)]
        [Description("Khởi công")]
        public string TDTHDA_THUCTE_KHOICONG { get; set; }

        [Column("TDTHDA_THUCTE_HOANTHANH")]
        [StringLength(50)]
        [Description("Hoàn thành")]
        public string TDTHDA_THUCTE_HOANTHANH { get; set; }

        [Column("GT_KHOILUONG_DODANG")]
        [StringLength(50)]
        [Description("Giá trị khối lượng dở dang")]
        public string GT_KHOILUONG_DODANG { get; set; }

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
    }
}
