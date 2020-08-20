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
    [Table("PHF_CAPNHAT_BAOCAO_CHITIET")]
    public class PHF_CAPNHAT_BAOCAO_CHITIET : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("INDEX")]
        public int INDEX { get; set; }

        [Column("NOIDUNG_CHITIET")]
        [StringLength(1000)]
        public string NOIDUNG_CHITIET { get; set; }

        [Column("KETQUA_THANHTRA")]
        [StringLength(1000)]
        public string KETQUA_THANHTRA { get; set; }

    }
}
