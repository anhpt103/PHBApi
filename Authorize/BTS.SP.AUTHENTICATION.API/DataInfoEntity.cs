using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.AUTHENTICATION.API
{
    public class DataInfoEntity : EntityBase
    {
        [Key]
        public string Id { get; set; }
    }
}
