using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BTS.SP.TOOLS.BuildQuery.Message
{
    [Serializable]
    [DataContract]
    public class MessageObj
    {
        public MessageObj()
        {

            isUseCustomMessage = true;
        }

        public MessageObj(string _message)
        {

            if (!string.IsNullOrEmpty(_message))
            {
                Message = _message;
            }
        }

        public MessageObj(CodeObj _code)
        {

            if (_code != null)
            {
                Language = _code.Language;
                Code = _code.Code;
            }
        }

        private string _code;

        /// <summary>
        /// Message Code
        /// </summary>
        [DataMember]
        public string Code
        {
            get { return _code; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    isUseCustomMessage = false;
                    _code = value;
                }
                else
                {
                    isUseCustomMessage = true;
                }
            }
        }

        /// <summary>
        /// Message Language
        /// </summary>
        [DataMember]
        public string Language { get; set; }

        private bool isUseCustomMessage;

        private string _message;

        /// <summary>
        /// Messsage Details 
        /// User can set it manual for set by Message Code
        /// </summary>
        [DataMember]
        public string Message
        {
            get
            {
                var result = _message;
                if (!isUseCustomMessage && !string.IsNullOrEmpty(Code))
                {

                    result = Code;

                }
                return result;
            }
            set
            {
                isUseCustomMessage = true;
                _message = value;
            }
        }

        /// <summary>
        /// Message Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Message Extension Data
        /// </summary>
        [XmlIgnore, IgnoreDataMember]
        public dynamic ExtData { get; set; }

        [DataMember]
        public string ShortDetails
        {
            get
            {
                string result = string.IsNullOrEmpty(Message) || Message == Code
                    ? string.Format("Message Code:【{0}】", Code)
                    : string.Format("【{0}】 {1}", Code, Message);
                return result;
            }
        }

        [DataMember]
        public string LongDetails
        {
            get
            {
                string result = string.Format("{0}-{1}", ShortDetails, Description);
                return result;
            }
        }

        public MessageObj clone()
        {
            var result = new MessageObj
            {
                Code = Code,
                Language = Language,
                Message = Message,
                Description = Description,
                ExtData = ExtData,
            };
            return result;
        }

        public MessageObj copyTo(MessageObj _destination)
        {
            if (_destination != null)
            {
                _destination.Code = Code;
                _destination.Language = Language;
                _destination.Message = Message;
                _destination.Description = Description;
                _destination.ExtData = ExtData;
            }
            else
            {
                _destination = clone();
            }
            return _destination;
        }

        #region Overrides of Object

        public override string ToString()
        {
            var result = string.Format("【{0}】{1}-{2}-{3}",
                Code,
                Message,
                Description,
                ExtData == null ? "" : string.Format("{0}", ExtData));
            return result;
        }

        #endregion

        [Serializable]
        [DataContract]
        public class CodeObj
        {
            /// <summary>
            /// Message Code
            /// </summary>
            [DataMember]
            public string Code { get; set; }

            /// <summary>
            /// Message Language
            /// </summary>
            [DataMember]
            public string Language { get; set; }

            /// <summary>
            /// Message Details
            /// </summary>
            [DataMember]
            public string Message { get; set; }

            internal string FullCode
            {
                get
                {
                    var result = "";
                    if (!string.IsNullOrEmpty(Code))
                    {
                        result = Code + (string.IsNullOrEmpty(Language) ? "" : string.Format("_{0}", Language));
                    }
                    return result;
                }
            }
        }
    }
}
