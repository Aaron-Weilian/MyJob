using System;
namespace com.portal.db.Model
{
    /// <summary>
    /// POLine:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class POLine : BaseModel
    {
        public POLine()
        { }
        #region Model
        private string _polineid;
        private string _poheaderid;
        private string _lineno;
        private string _item_no;
        private string _request_qty;
        private string _request_delivery_date;
        private string _unit_price;
        private string _desc;
        private string _curr;
        private string _price_unit;
        private string _line_item_tatoal_amount;
        private string _schedule_delivery_date;
        private string _schedule_delivery_qty;
        private string _schedule_arrive_date;
        /// <summary>
        /// 
        /// </summary>
        public string POLineID
        {
            set { _polineid = value; }
            get { return _polineid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string POHeaderID
        {
            set { _poheaderid = value; }
            get { return _poheaderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lineNo
        {
            set { _lineno = value; }
            get { return _lineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string item_no
        {
            set { _item_no = value; }
            get { return _item_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string request_qty
        {
            set { _request_qty = value; }
            get { return _request_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string request_delivery_date
        {
            set { _request_delivery_date = value; }
            get { return _request_delivery_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string unit_price
        {
            set { _unit_price = value; }
            get { return _unit_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }

        public string curr
        {
            set { _curr = value; }
            get { return _curr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string price_unit
        {
            set { _price_unit = value; }
            get { return _price_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string line_item_tatoal_amount
        {
            set { _line_item_tatoal_amount = value; }
            get { return _line_item_tatoal_amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string schedule_delivery_date
        {
            set { _schedule_delivery_date = value; }
            get { return _schedule_delivery_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string schedule_delivery_qty
        {
            set { _schedule_delivery_qty = value; }
            get { return _schedule_delivery_qty; }
        }


        public string Schedule_Arrive_Date
        {
            set { _schedule_arrive_date = value; }
            get { return _schedule_arrive_date; }
        }
     
        #endregion Model

    }
}

