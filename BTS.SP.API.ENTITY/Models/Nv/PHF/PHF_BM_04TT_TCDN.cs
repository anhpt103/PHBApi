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
    [Table("PHF_BM_04TT_TCDN")]
    public class PHF_BM_04TT_TCDN : DataInfoEntityPHF
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

        [Column("TEN_SOHUU")]
        [StringLength(1000)]
        [Description("Tên chủ sở hữu")]
        public string TEN_SOHUU { get; set; }

        [Column("SOVON")]
        [StringLength(1000)]
        [Description("Số vốn được góp")]
        public string SOVON { get; set; }

        [Column("SOVONTHUC")]
        [StringLength(1000)]
        [Description("Số thực góp đến 31/12/…")]
        public string SOVONTHUC { get; set; }

        [Column("SOVONTHIEU")]
        [StringLength(1000)]
        [Description("Số vốn góp còn thiếu")]
        public string SOVONTHIEU { get; set; }

        [Column("NGUYENNHAN")]
        [StringLength(1000)]
        [Description("Nguyên nhân")]
        public string NGUYENNHAN { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
