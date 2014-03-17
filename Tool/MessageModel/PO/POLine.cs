using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    [Serializable]
    public class POLine
    {
        public string lineNo { set; get; }
        public string item_no { set; get; }
        public string desc { set; get; }
        public string request_qty { set; get; }
        public string request_delivery_date { set; get; }
        public string comfirm_eta_date { set; get; }
        public string comfirm_etd_date { set; get; }
        public string comfirm_qty { set; get; }
        public string curr { set; get; }
        public string unit_price { set; get; }
        public string price_unit { set; get; }
        public string line_item_tatoal_amount { set; get; }
        public string schedule_delivery_date { set; get; }
        public string schedule_delivery_qty { set; get; }
        public string Schedule_Arrive_Date { set; get; }
    }
}
