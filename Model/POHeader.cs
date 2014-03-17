using System;
namespace com.portal.db.Model
{
    /// <summary>
    /// POHeader:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class POHeader : BaseModel
    {
        public POHeader()
        { }
        #region Model
        private string _poheaderid;
        private string _messagebodyid;
        private string _buyer;
        private string _po_date;
        private string _po_number;
        private string _po_type;
        private string _order_type;
        private string _your_reference;
        private string _desc;
        private string _delivery_location;
        private string _delivery_address;
        private string _currency;
        private string _terms_of_delivery;
        private string _terms_of_payment;
        private string _means_of_transport;
        private string _supplier;
        private string _supplier_site;
        private string _supplier_address;
        private string _bonded;

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
        public string messageBodyID
        {
            set { _messagebodyid = value; }
            get { return _messagebodyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string buyer
        {
            set { _buyer = value; }
            get { return _buyer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string po_date
        {
            set { _po_date = value; }
            get { return _po_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string po_number
        {
            set { _po_number = value; }
            get { return _po_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string po_type
        {
            set { _po_type = value; }
            get { return _po_type; }
        }

        public string order_type
        {
            set { _order_type = value; }
            get { return _order_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string your_reference
        {
            set { _your_reference = value; }
            get { return _your_reference; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string delivery_location
        {
            set { _delivery_location = value; }
            get { return _delivery_location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string currency
        {
            set { _currency = value; }
            get { return _currency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string terms_of_delivery
        {
            set { _terms_of_delivery = value; }
            get { return _terms_of_delivery; }
        }
        public string means_of_transport
        {
            set { _means_of_transport = value; }
            get { return _means_of_transport; }
        }
        public string supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        public string terms_of_payment
        {
            set { _terms_of_payment = value; }
            get { return _terms_of_payment; }
        }
      
        public string bonded
        {
            set { _bonded = value; }
            get { return _bonded; }
        }
        public string supplier_site
        {
            set { _supplier_site = value; }
            get { return _supplier_site; }
        }
        public string supplier_address
        {
            set { _supplier_address = value; }
            get { return _supplier_address; }
        }
        public string delivery_address
        {
            set { _delivery_address = value; }
            get { return _delivery_address; }
        }

        #endregion Model

    }
}

