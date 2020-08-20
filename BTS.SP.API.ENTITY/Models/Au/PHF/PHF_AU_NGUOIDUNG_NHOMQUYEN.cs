using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("PHF_AU_NGUOIDUNG_NHOMQUYEN")]
    public class PHF_AU_NGUOIDUNG_NHOMQUYEN : DataInfoEntityPHF
    {
        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(50)]
        public string MANHOMQUYEN { get; set; }
    }
}
