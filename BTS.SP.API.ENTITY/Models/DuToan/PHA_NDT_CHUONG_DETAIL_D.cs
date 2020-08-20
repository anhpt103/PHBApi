using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("PHA_NDT_CHUONG_DETAIL_D")]
    public class PHA_NDT_CHUONG_DETAIL_D : DataInfoEntity
    {
        [Column("PHA_NDT_CHUONG_REFID")]
        [StringLength(50)]
        public string PHA_NDT_CHUONG_REFID { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(50)]
        public string MA_CHUONG { get; set; }

        [Column("TEN_CHUONG")]
        [StringLength(500)]
        public string TEN_CHUONG { get; set; }

        [Column("MA_DVQHNS")]
        [StringLength(50)]
        public string MA_DVQHNS { get; set; }

        [Column("STT")]
        [StringLength(10)]
        public string STT { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("TEN_DVQHNS")]
        [StringLength(500)]
        public string TEN_DVQHNS { get; set; }

        [Column("LOAI_CHITIEU")]
        [StringLength(50)]
        public string LOAI_CHITIEU { get; set; }

        [Column("TONG")]
        public decimal? TONG { get; set; }
        // Chi đầu tư
        [Column("TONG_CDT")]
        public decimal? TONG_CDT { get; set; }

        [Column("CDT_QP_30110")]
        public decimal? CDT_QP_30110 { get; set; }

        [Column("CDT_AN_30120")]
        public decimal? CDT_AN_30120 { get; set; }

        [Column("CDT_GD_30130")]
        public decimal? CDT_GD_30130 { get; set; }

        [Column("CDT_KHCN_30140")]
        public decimal? CDT_KHCN_30140 { get; set; }

        [Column("CDT_YT_30150")]
        public decimal? CDT_YT_30150 { get; set; }

        [Column("CDT_VH_30160")]
        public decimal? CDT_VH_30160 { get; set; }

        [Column("CDT_PTTH_30170")]
        public decimal? CDT_PTTH_30170 { get; set; }

        [Column("CDT_TDTT_30180")]
        public decimal? CDT_TDTT_30180 { get; set; }

        [Column("CDT_BVMT_30190")]
        public decimal? CDT_BVMT_30190 { get; set; }

        [Column("CDT_KT_30200")]
        public decimal? CDT_KT_30200 { get; set; }

        [Column("CDT_GT_30203")]
        public decimal? CDT_GT_30203 { get; set; }

        [Column("CDT_NLNN_30205")]
        public decimal? CDT_NLNN_30205 { get; set; }
        [Description("Chi nông, lâm, ngư nghiệp, thủy sản")]

        [Column("CDT_HDNN_30210")]
        public decimal? CDT_HDNN_30210 { get; set; }
        [Description("Chi hoạt động của cơ quan quản lý nhà nước, đảng, đoàn thể")]

        [Column("CDT_DBXH_30220")]
        public decimal? CDT_DBXH_30220 { get; set; }
        [Description("Chi đảm bảo xã hội")]

        [Column("CDT_K_30230")]
        public decimal? CDT_K_30230 { get; set; }
        [Description("Chi đầu tư khác")]

        //Chi thường xuyên
        [Column("TONG_CTX")]
        public decimal? TONG_CTX { get; set; }

        [Column("CTX_QP_34020")]
        public decimal? CTX_QP_34020 { get; set; }

        [Column("CTX_AN_34030")]
        public decimal? CTX_AN_34030 { get; set; }

        [Column("CTX_GD_30130")]
        public decimal? CTX_GD_30130 { get; set; }

        [Column("CTX_KHCN_34080")]
        public decimal? CTX_KHCN_34080 { get; set; }

        [Column("CTX_YT_34090")]
        public decimal? CTX_YT_34090 { get; set; }

        [Column("CTX_VH_34300")]
        public decimal? CTX_VH_34300 { get; set; }

        [Column("CTX_PTTH_34400")]
        public decimal? CTX_PTTH_34400 { get; set; }

        [Column("CTX_TDTT_34500")]
        public decimal? CTX_TDTT_34500 { get; set; }

        [Column("CTX_BVMT_34600")]
        public decimal? CTX_BVMT_34600 { get; set; }

        [Column("CTX_KT_34700")]
        public decimal? CTX_KT_34700 { get; set; }

        [Column("CTX_GT_34740")]
        public decimal? CTX_GT_34740 { get; set; }

        [Column("CTX_NLNN_34760")]
        public decimal? CTX_NLNN_34760 { get; set; }
        [Description("Chi nông, lâm, ngư nghiệp, thủy sản")]

        [Column("CTX_HDNN_34800")]
        public decimal? CTX_HDNN_34800 { get; set; }
        [Description("Chi hoạt động của cơ quan quản lý nhà nước, đảng, đoàn thể")]

        [Column("CTX_DBXH_34900")]
        public decimal? CTX_DBXH_34900 { get; set; }

        [Column("CTX_K_35020")]
        public decimal? CTX_K_35020 { get; set; }
    }
}
