using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("PHA_NDT_CTMT_CHUONG_DETAIL")]
    public class PHA_NDT_CTMT_CHUONG_DETAIL : DataInfoEntity
    {
        [Column("PHA_NDT_CTMT_CHUONG_REFID")]
        [StringLength(50)]
        public string PHA_NDT_CTMT_CHUONG_REFID { get; set; }

        [Column("STT")]
        [StringLength(10)]
        public string STT { get; set; }

        [Column("STT_SAPXEP_CHA")]
        public int STT_SAPXEP_CHA { get; set; }

        [Column("STT_SAPXEP_CON")]
        public int STT_SAPXEP_CON { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(50)]
        public string MA_CHUONG { get; set; }

        [Column("TEN_CHUONG")]
        [StringLength(500)]
        public string TEN_CHUONG { get; set; }

        [Column("MA_DVQHNS")]
        [StringLength(50)]
        public string MA_DVQHNS { get; set; }

        [Column("TEN_DVQHNS")]
        [StringLength(500)]
        public string TEN_DVQHNS { get; set; }

        [Column("TEN")]
        [StringLength(500)]
        public string TEN { get; set; }

        [Column("MA")]
        [StringLength(50)]
        public string MA { get; set; }

        [Column("MA_CHA")]
        [StringLength(50)]
        public string MA_CHA { get; set; }

        [Column("MA_CON")]
        [StringLength(50)]
        public string MA_CON { get; set; }

        [Column("LOAI_CTMT")]
        [StringLength(20)]
        public string LOAI_CTMT { get; set; }

        [Column("LOAI_DUTOAN")]
        public int LOAI_DUTOAN { get; set; }

        [Column("CDT_20000_MT")]
        public decimal? CDT_20000_MT { get; set; }

        [Column("CTX_34000_MT")]
        public decimal? CTX_34000_MT { get; set; }

        [Column("CTN_40000")]
        public decimal? CTN_40000 { get; set; }

        [Column("CBS_41000")]
        public decimal? CBS_41000 { get; set; }

        [Column("CDP_43000")]
        public decimal? CDP_43000 { get; set; }

        [Column("CTL_44000")]
        public decimal? CTL_44000 { get; set; }

        //CHI CHƯƠNG TRÌNH MTQG
        [Column("GNBV_VDT_45100_DT")]
        public decimal? GNBV_VDT_45100_DT { get; set; }

        [Column("GNBV_VSN_45100_TX")]
        public decimal? GNBV_VSN_45100_TX { get; set; }

        //Chương trình mục tiêu quốc gia giảm nghèo bền vững
        [Column("GNBV_TONG_GN_45100_0010")]
        public decimal? GNBV_TONG_GN_45100_0010 { get; set; }

        [Column("GNBV_VDT_TONG_45100_DT_0010")]
        public decimal? GNBV_VDT_TONG_45100_DT_0010 { get; set; }

        [Column("GNBV_VDT_45100_DT_0010_VTN")]
        public decimal? GNBV_VDT_45100_DT_0010_VTN { get; set; }

        [Column("GNBV_VDT_45100_DT_0010_VNN")]
        public decimal? GNBV_VDT_45100_DT_0010_VNN { get; set; }

        [Column("GNBV_VSN_TONG_45100_TX_0010")]
        public decimal? GNBV_VSN_TONG_45100_TX_0010 { get; set; }

        [Column("GNBV_VSN_45100_TX_0010_VTN")]
        public decimal? GNBV_VSN_45100_TX_0010_VTN { get; set; }

        [Column("GNBV_VSN_45100_TX_0010_VNN")]
        public decimal? GNBV_VSN_45100_TX_0010_VNN { get; set; }

        //Chương trình mục tiêu quốc gia xây dựng nông thôn mới
        [Column("XDNTM_TONG_NTM_45100_0390")]
        public decimal? XDNTM_TONG_NTM_45100_0390 { get; set; }

        [Column("XDNTM_VDT_TONG_45100_DT_0390")]
        public decimal? XDNTM_VDT_TONG_45100_DT_0390 { get; set; }

        [Column("XDNTM_VDT_45100_DT_0390_VTN")]
        public decimal? XDNTM_VDT_45100_DT_0390_VTN { get; set; }

        [Column("XDNTM_VDT_45100_DT_0390_VNN")]
        public decimal? XDNTM_VDT_45100_DT_0390_VNN { get; set; }

        [Column("XDNTM_VSN_TONG_45100_TX_0390")]
        public decimal? XDNTM_VSN_TONG_45100_TX_0390 { get; set; }

        [Column("XDNTM_VSN_45100_DT_0390_VTN")]
        public decimal? XDNTM_VSN_45100_DT_0390_VTN { get; set; }

        [Column("XDNTM_VSN_45100_DT_0390_VNN")]
        public decimal? XDNTM_VSN_45100_DT_0390_VNN { get; set; }

        [Column("CCN_NAMSAU_48000")]
        public decimal? CCN_NAMSAU_48000 { get; set; }
    }

}
