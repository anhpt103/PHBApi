using BTS.SP.API.ENTITY.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_COQUANTHU")]
    public class PHF_DM_COQUANTHU : DataInfoEntityPHF
    {
        [Column("MA_COQUANTHU")]
        [StringLength(50)]
        [Description("Mã cơ quan thu")]
        public string MA_COQUANTHU { get; set; }

        [Column("TEN_COQUANTHU")]
        [StringLength(500)]
        [Description("Tên cơ quan thu")]
        public string TEN_COQUANTHU { get; set; }
    }
}
