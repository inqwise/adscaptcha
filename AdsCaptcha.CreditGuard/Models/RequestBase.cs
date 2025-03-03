using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CreditGuard
{
    public abstract class RequestBase
    {
        public const string Command = "doDeal";
        public string RequestID { get; set; }
        public string Version { get; set; }
        public bool Debug { get; set; }

        public RequestBase(string requestID, string version, bool debug)
        {
            this.RequestID = requestID;
            this.Version = version;
            this.Debug = debug;
        }       
    }
}
