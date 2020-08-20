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
    [Table("PHF_BM_10TT_TCDN")]
    public class PHF_BM_10TT_TCDN : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int? STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int? STT_CHA { get; set; }

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

        [Column("CACKHOAN_PHAINOP")]
        [StringLength(200)]
        [Description("Các khoản phải nộp")]
        public string CACKHOAN_PHAINOP { get; set; }

        [Column("SO_PHAINOP")]
        [StringLength(200)]
        [Description("Số phải nộp đầu kỳ ")]
        public string SO_PHAINOP { get; set; }

        [Column("SO_PHATSINH")]
        [StringLength(200)]
        [Description("Số phát sinh trong kỳ")]
        public string SO_PHATSINH { get; set; }

        [Column("SO_DANOP")]
        [StringLength(200)]
        [Description("Số đã nộp trong kỳ")]
        public string SO_DANOP { get; set; }

        [Column("SCPN_TONGSO")]
        [StringLength(200)]
        [Description("Tổng số")]
        public string SCPN_TONGSO { get; set; }

        [Column("SCPN_SOCHAMNOP")]
        [StringLength(200)]
        [Description("Trong đó: số chậm nộp")]
        public string SCPN_SOCHAMNOP { get; set; }

        [Column("SCPN_NGUYENNHAN")]
        [StringLength(200)]
        [Description("Nguyên nhân")]
        public string SCPN_NGUYENNHAN { get; set; }
       
    }
}
