using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace  BTS.SP.AUTHENTICATION.API.Helper
{
    [DataContract]
    public class StateInfoObj
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string ExtendValue { get; set; }
        [DataMember]
        public int ExtendValue2 { get; set; }
        [DataMember]
        public string ExtendValue3 { get; set; }
        [DataMember]
        public int? ExtendValue4 { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public bool Selected { get; set; }
        
    }

    [DataContract]
    public class StateInfoObj2
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ma_DBHC_Cha { get; set; }
        [DataMember]
        public string Ma_H { get; set; }
        [DataMember]
        public string ExtendValue { get; set; }
        [DataMember]
        public string Ten_DBHC { get; set; }
        [DataMember]
        public bool Selected { get; set; }

    }
    [DataContract]
    public class StateInfoObj3
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ma_H { get; set; }
        [DataMember]
        public string Ma_X { get; set; }
        [DataMember]
        public string ExtendValue { get; set; }
        [DataMember]
        public string Ten_DBHC { get; set; }
        [DataMember]
        public bool Selected { get; set; }

    }
    [DataContract]
    public class StateInfoObj4
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ma_DV { get; set; }
        [DataMember]
        public string Ma_H { get; set; }
        [DataMember]
        public string ExtendValue { get; set; }
        [DataMember]
        public string Ten_DV { get; set; }
        [DataMember]
        public bool Selected { get; set; }

    }
}
