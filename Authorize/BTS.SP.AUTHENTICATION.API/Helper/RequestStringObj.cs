using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace  BTS.SP.AUTHENTICATION.API.Helper
{
    [DataContract]
    public class RequestStringObj
    {
        [DataMember]
        public string RequestData { get; set; }
    }
}
