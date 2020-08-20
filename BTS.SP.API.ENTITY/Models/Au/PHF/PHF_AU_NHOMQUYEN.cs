using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("PHF_AU_NHOMQUYEN")]
    public class PHF_AU_NHOMQUYEN : DataInfoEntityPHF
    {
        [Column("MANHOMQUYEN")]
        [StringLength(50)]
        [Required]
        public string MANHOMQUYEN { get; set; }

        [Column("TENNHOMQUYEN")]
        [StringLength(100)]
        public string TENNHOMQUYEN { get; set; }

        [Column("MOTA")]
        [StringLength(200)]
        public string MOTA { get; set; }

        [Column("TRANGTHAI")]
        [Description("1: sudung | 0 : khongsudung")]
        public int TRANGTHAI { get; set; }
    }
}
