using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    [Serializable]
    public class Message<T> where T : Header
    {
        public string filename { set; get; }
        public MessageSender sender { set; get; }
        public MessagePartner partner { set; get; }
        public List<T> headers { set; get; }

        public MessageContext messagebody { set; get; }

    }

    [Serializable]
    public class XMLMessage
    {
        public string filename { set; get; }
        public MessageSender sender { set; get; }
        public MessagePartner partner { set; get; }

        public MessageContext messagebody { set; get; }

    }
}
