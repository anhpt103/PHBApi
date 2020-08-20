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
    [Table("PHF_TH_THUCHIEN_KEHOACH_TT")]
    public class PHF_TH_THUCHIEN_KEHOACH_TT : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public string STT { get; set; }

        [Column("TEN_DOITUONG")]
        [StringLength(500)]
        public string TEN_DOITUONG { get; set; }

        [Column("TONGSO_CUOC_THANHTRA")]
        [StringLength(500)]
        public string TONGSO_CUOC_THANHTRA { get; set; }

        [Column("TONGSO_DOAN_THANHTRA")]
        [StringLength(500)]
        public string TONGSO_DOAN_THANHTRA { get; set; }

        [Column("TIENDO_DANG_THUCHIEN")]
        [StringLength(500)]
        public string TIENDO_DANG_THUCHIEN { get; set; }

        [Column("TIENDO_DANG_DUTHAO")]
        [StringLength(500)]
        public string TIENDO_DANG_DUTHAO { get; set; }

        [Column("TIENDO_DA_CONGBO_DUTHAO")]
        [StringLength(500)]
        public string TIENDO_DA_CONGBO_DUTHAO { get; set; }

        [Column("TIENDO_DANGTRINH_LANHDAO")]
        [StringLength(500)]
        public string TIENDO_DANGTRINH_LANHDAO { get; set; }

        [Column("TIENDO_DA_LUUHANH")]
        [StringLength(500)]
        public string TIENDO_DA_LUUHANH { get; set; }

        [Column("GHI_CHU")]
        [StringLength(500)]
        public string GHI_CHU { get; set; }
    }
}
