using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;

namespace  BTS.SP.AUTHENTICATION.API.Helper
{
    [DataContract]
    public class ChoiceObj : StateInfoObj
    {
        [DataMember]
        public string Parent { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool OldSelected { get; set; }
        [DataMember]
        public string ReferenceDataId { get; set; }
    }
    [DataContract]
    public class ChoiceObj2 : StateInfoObj2
    {
        [DataMember]
        public string Parent { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool OldSelected { get; set; }
        [DataMember]
        public string ReferenceDataId { get; set; }
    }
    [DataContract]
    public class ChoiceObj3 : StateInfoObj3
    {
        [DataMember]
        public string Parent { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool OldSelected { get; set; }
        [DataMember]
        public string ReferenceDataId { get; set; }
    }
    [DataContract]
    public class ChoiceObj4 : StateInfoObj4
    {
        [DataMember]
        public string Parent { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool OldSelected { get; set; }
        [DataMember]
        public string ReferenceDataId { get; set; }
    }
}
