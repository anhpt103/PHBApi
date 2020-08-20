using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHA
{
    [Table("PHA_TELERIK_REPORT")]
    public class PHA_TELERIK_REPORT : DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("NAME_STORE")]
        [Required]
        [Description("Tên store")]
        [StringLength(250)]
        public string NAME_STORE { get; set; }

        [Column("NAME_TELERIK")]
        [Required]
        [Description("Tên telerik")]
        [StringLength(250)]
        public string NAME_TELERIK { get; set; }

        [Column("NGAY_TAO")]
        public DateTime? NGAY_TAO { get; set; }

        [Column("NGUOI_TAO")]
        [StringLength(150)]
        public string NGUOI_TAO { get; set; }
    }
}
