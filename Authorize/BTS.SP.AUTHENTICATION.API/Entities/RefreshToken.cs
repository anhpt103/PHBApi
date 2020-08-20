using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTS.SP.AUTHENTICATION.API.Entities
{
    public class RefreshToken : DataInfoEntity
    {

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        public string ProtectedTicket { get; set; }
    }
}