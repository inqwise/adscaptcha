using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public abstract class ResponseBase
    {
        public string RequestID { get; set; }
        public int TransID { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
        public string AdditionalInfo { get; set; }
        public string Version { get; set; }
        public string Language { get; set; }
    }
}
