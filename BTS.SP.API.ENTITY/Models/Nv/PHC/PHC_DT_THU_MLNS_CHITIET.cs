using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHC
{
    [Table("PHC_DT_THU_MLNS_CHITIET")]
    public class PHC_DT_THU_MLNS_CHITIET : DataInfoEntity
    {
        [Required]
        [Column("MADUTOAN")]
        [StringLength(50)]
        [Description("Mã dự toán")]
        public string MADUTOAN { get; set; }

        [Column("SO_QD")]
        [Description("Số quyết định")]
        public Nullable<int> SO_QD { get; set; }

        [Column("TYLE_XAHUONG")]
        [Description("Tỷ lệ xã hưởng %")]
        public Nullable<int> TYLE_XAHUONG { get; set; }

        [Column("MANGUON")]
        [StringLength(20)]
        [Description("Mã nguồn")]
        public string MANGUON { get; set; }

        [Column("MACHUONG")]
        [StringLength(3)]
        [Description("Mã Chương")]
        public string MACHUONG { get; set; }

        [Column("MAKHOAN")]
        [StringLength(6)]
        [Description("Mã khoản")]
        public string MAKHOAN { get; set; }

        [Column("MAMUC")]
        [StringLength(6)]
        [Description("Mã mục")]
        public string MAMUC { get; set; }

        [Column("MATIEUMUC")]
        [StringLength(6)]
        [Description("Mã tiểu mục")]
        public string MATIEUMUC { get; set; }

        [Column("NSNN")]
        [Description("ngan sach nha nuoc")]
        public Nullable<decimal> NSNN { get; set; }

        [Column("NSNN_DN")]
        [Description("Ngân sách nhà nước đầu năm")]
        public Nullable<decimal> NSNN_DN { get; set; }

        [Column("NSNN_BS")]
        [Description("Ngân sách nhà nước bổ sung")]
        public Nullable<decimal> NSNN_BS { get; set; }

        [Column("NSNN_DC")]
        [Description("Ngân sách nhà nước điểu chỉnh")]
        public Nullable<decimal> NSNN_DC { get; set; }

        [Column("NSX")]
        [Description("ngan sach xa")]
        public Nullable<decimal> NSX { get; set; }

        [Column("NSX_DN")]
        [Description("Ngân sách xã đầu năm")]
        public Nullable<decimal> NSX_DN { get; set; }

        [Column("NSX_BS")]
        [Description("Ngân sách xã bổ sung")]
        public Nullable<decimal> NSX_BS { get; set; }

        [Column("NSX_DC")]
        [Description("Ngân sách xã điều chỉnh")]
        public Nullable<decimal> NSX_DC { get; set; }

    }
}
