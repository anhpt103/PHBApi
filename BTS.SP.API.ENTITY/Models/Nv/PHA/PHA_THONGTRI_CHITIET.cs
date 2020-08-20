using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHA
{
    [Table("PHA_THONGTRI_CHITIET")]
    public class PHA_THONGTRI_CHITIET : DataInfoEntity
    {
        [Column("REF_ID")]
        [StringLength(100)]
        public string REF_ID { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(10)]
        public string MA_CHUONG { get; set; }

        [Column("MA_NGANHKT")]
        [StringLength(10)]
        public string MA_NGANHKT { get; set; }

        [Column("MA_NDKT")]
        [StringLength(10)]
        public string MA_NDKT { get; set; }

        [Column("MA_NV")]
        [StringLength(10)]
        public string MA_NV { get; set; }

        [Column("NOI_DUNG")]
        [StringLength(250)]
        public string NOI_DUNG { get; set; }

        [Column("LOAI_TIEN")]
        [StringLength(3)]
        public string LOAI_TIEN { get; set; }

        [Column("SO_TIEN")]
        public decimal SO_TIEN { get; set; }
    }
}
