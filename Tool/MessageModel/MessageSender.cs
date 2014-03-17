using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    [Serializable]
    public class MessageSender
    {
        public string key { set; get; }
        public string messageName { set; get; }
        public string messageAlias { set; get; }
        public string referenceID { set; get; }
        public string creationDateTime { set; get; }
        public string edi_location_code { set; get; }
        public string ship_from { set; get; }

    }
}
