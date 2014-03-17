using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    [Serializable]
    public class MessagePartner
    {
        public string confirmDateTime { set; get; }
        public string vender_name { set; get; }
        public string vender_num { set; get; }
        public string vender_site { set; get; }
        public string vender_site_num { set; get; }
        public string duns_num { set; get; }
        public string contact_name { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public string address { set; get; }
        public string street { set; get; }
        public string city { set; get; }
        public string postal_code { set; get; }

    }
}
