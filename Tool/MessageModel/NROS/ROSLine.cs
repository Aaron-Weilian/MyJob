using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool
{
    [Serializable]
    public class ROSLine
    {
        public string demand_date { set; get; }

        public string demand_quantity { set; get; }

        public string eta_qty { set; get; }

        public string etd_qty { set; get; }

        public string dio { set; get; }

        public string cum_eta { set; get; }

        public string balance { set; get; }

        public string po_no { set; get; }

        public string shortage_qty { set; get; }
    }
}