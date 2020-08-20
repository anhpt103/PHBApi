using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations;

namespace BTS.SP.PHB.ENTITY
{
    public class BaseEntity:Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
