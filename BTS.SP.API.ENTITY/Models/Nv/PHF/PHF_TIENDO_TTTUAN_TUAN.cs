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
    [Table("PHF_TIENDO_TTTUAN_TUAN")]
    public class PHF_TIENDO_TTTUAN_TUAN : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_TUAN")]
        [StringLength(50)]
        public string MA_TUAN { get; set; }

        [Column("TUAN")]
        [Description("Tuần")]
        public int? TUAN { get; set; }

        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }   
    }
}
