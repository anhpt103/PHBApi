using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHA_BCTC.B04_BCTC
{
    [Table("PHA_B04_BCTC_TEMPLATE")]
    public class PHA_B04_BCTC_TEMPLATE : DataInfoEntity
    {
        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(250)]
        public string CHI_TIEU { get; set; }

        [Column("MA_SO")]
        [StringLength(250)]
        public string MA_SO { get; set; }

        [Column("MA_SO_CHA")]
        [StringLength(250)]
        public string MA_SO_CHA { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        public string XML_PARENT_FIELD_NAME_1 { get; set; }

        public string XML_PARENT_FIELD_NAME_2 { get; set; }

        public int? IS_INCLUDED_SO_CUOI_NAM { get; set; }
        
        public int? IS_INCLUDED_SO_DAU_NAM { get; set; }

        public int? IS_INCLUDED_TONG_CONG { get; set; }

        public int? IS_INCLUDED_TSCD_HUU_HINH { get; set; }

        public int? IS_INCLUDED_TSCD_VO_HINH { get; set; }

        public int? IS_INCLUDED_NGUON_VON_KD { get; set; }

        public int? IS_INCLUDED_CHENH_LECH_TY_GIA { get; set; }

        public int? IS_INCLUDED_THANG_DU_LUY_KE { get; set; }

        public int? IS_INCLUDED_CAC_QUY { get; set; }

        public int? IS_INCLUDED_CAI_CACH_TIEN_LUON { get; set; }

        public int? IS_INCLUDED_KHAC { get; set; }

        public int? IS_INCLUDED_CONG { get; set; }

        public int? IS_INCLUDED_NAM_NAY { get; set; }

        public int? IS_INCLUDED_NAM_TRUOC { get; set; }

        public string MA_SO_IMPORT_XML_BCTC_107_GT { get; set; }
    }
}
