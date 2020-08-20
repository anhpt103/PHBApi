using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_TIENDO_TUAN_CHITIET")]
    public class PHF_TIENDO_TUAN_CHITIET : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("TUAN")]
        [Description("Tuần")]
        public int? TUAN { get; set; }

        [Column("NOIDUNG_CHITIET")]
        [StringLength(1000)]
        public string NOIDUNG_CHITIET { get; set; }

        [Column("TIENDO")]
        [StringLength(1000)]
        public string TIENDO { get; set; }

        [Column("CONGVIEC_TUANTOI")]
        [StringLength(1000)]
        public string CONGVIEC_TUANTOI { get; set; }

        [Column("CONGVIEC_DUKIEN")]
        [StringLength(1000)]
        public string CONGVIEC_DUKIEN { get; set; }
    }
}
