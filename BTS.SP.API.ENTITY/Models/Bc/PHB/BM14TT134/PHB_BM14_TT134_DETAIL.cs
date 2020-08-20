using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
   
namespace BTS.SP.API.ENTITY.Models.Bc.PHB.BM14TT134
{
    [Table("PHB_BM14_TT134_DETAIL")]
    public class PHB_BM14_TT134_DETAIL : DataInfoEntity
    {
        [Column("PHB_BM14_TT134_REFID")]
        [Required]
        [Description("Guid ID trong  PHB_BM14_TT134")]
        [StringLength(50)]   
        public string PHB_BM14_TT134_REFID { get; set; }


        [Column("SO_CTU")]
        [StringLength(50)]
        public string SO_CTU { get; set; }

        [Column("NGAY_THANG")]
        [Required]
        public DateTime NGAY_THANG { get; set; }

        [Column("NOIDUNG")]         
        [Required]
        public string NOIDUNG { get; set; }

        [Column("SO_TIEN")]
        [Required]        
        public decimal SO_TIEN { get; set; }

    }
}
