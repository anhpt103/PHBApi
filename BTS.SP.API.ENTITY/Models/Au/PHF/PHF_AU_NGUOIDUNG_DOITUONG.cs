using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("PHF_AU_NGUOIDUNG_DOITUONG")]
    public class PHF_AU_NGUOIDUNG_DOITUONG : DataInfoEntityPHF
    {
        [Required]
        [Column("USERNAME")]
        [StringLength(30)]
        public string USERNAME { get; set; }

        [Required]
        [Column("MA_DOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MA_DOITUONG { get; set; }
       
    }
}
