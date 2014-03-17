using System;
namespace com.portal.db.Model
{
	/// <summary>
	/// Supplier:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Supplier
	{
		public Supplier()
		{}
		#region Model
		private string _supplierid;
		private string _suppliername;
        private string _suppliernum;
		private string _contactname;
		private string _countrycode;
		private string _supplieraddress;
		private string _supplieremail;
		private string _supplierphone;
        private string _duns;
        private string _status;
        private string _sitename;
        private string _sitenum;
		private string _created;
		private string _createby;
		private string _updated;
		private string _updateby;
		/// <summary>
		/// 
		/// </summary>
		public string SupplierID
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierName
		{
			set{ _suppliername=value;}
			get{return _suppliername;}
		}
        public string SupplierNUM
        {
            set { _suppliernum = value; }
            get { return _suppliernum; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string ContactName
		{
			set{ _contactname=value;}
			get{return _contactname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CountryCode
		{
			set{ _countrycode=value;}
			get{return _countrycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierAddress
		{
			set{ _supplieraddress=value;}
			get{return _supplieraddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierEmail
		{
			set{ _supplieremail=value;}
			get{return _supplieremail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierPhone
		{
			set{ _supplierphone=value;}
			get{return _supplierphone;}
		}

        public string DUNS
        {
            set { _duns = value; }
            get { return _duns; }
        }

        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string SiteName
        {
            set { _sitename = value; }
            get { return _sitename; }
        }
        public string SiteNUM
        {
            set { _sitenum = value; }
            get { return _sitenum; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string  Created
		{
			set{ _created=value;}
			get{return _created;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateBy
		{
			set{ _createby=value;}
			get{return _createby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string  Updated
		{
			set{ _updated=value;}
			get{return _updated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateBy
		{
			set{ _updateby=value;}
			get{return _updateby;}
		}
		#endregion Model

	}
}

