using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("IDENTITY_REFRESH_TOKEN")]
    public partial class IDENTITY_REFRESH_TOKEN : DataInfoEntityPHF
    {
        public string Id { get; set; }

        [StringLength(50)]
        public string Identity { get; set; }

        [StringLength(128)]
        public string ClientId { get; set; }

        public DateTime? IssuedUtc { get; set; }

        public DateTime? ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }
    }
}
