using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    [Serializable]
    public class POHeader : Header
    {
        public string buyer { set; get; }
        public string po_date { set; get; }
        public string po_number { set; get; }
        public string po_type { set; get; }
        public string your_reference { set; get; }
        public string rev { set; get; }
        public string desc { set; get; }
        public string order_class { set; get; }
        public string delivery_location { set; get; }
        public string delivery_address { set; get; }
        public string currency { set; get; }
        public string means_of_transport { set; get; }
        public string terms_of_delivery { set; get; }
        public string supplier { set; get; }
        public string supplier_site { set; get; }
        public string supplier_address { set; get; }
        public string bonded { set; get; }
        public string term_of_payment { set; get; }


        public List<POLine> polines = new List<POLine>();
    }
}
