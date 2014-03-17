using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool
{
    [Serializable]
    public class ROSHeader : Header
    {

        public string item_no { set; get; }

        public string description { set; get; }

        public string model { set; get; }

        public string category { set; get; }

        public string glod_plan_flag { set; get; }

        public string vendor_name { set; get; }

        public string vendor_site { set; get; }

        public string buyer { set; get; }

        public string allocation_percent { set; get; }

        public string lead_time { set; get; }

        public string stock_qty { set; get; }

        public string safe_stock { set; get; }

        public string po_number { set; get; }

        public string open_po_qty { set; get; }

        public string vmi_stock { set; get; }

        public string updateflag { set; get; }

        public List<ROSLine> lines = new List<ROSLine>();

    }
}