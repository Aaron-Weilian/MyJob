
using System;
namespace com.portal.db.Model
{
	/// <summary>
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
		#region Model
		private string _userid;
		private string _username;
		private string _userpassword;
		private string _supplier;
        private string _suppliernum;
		private string _created;
		private string _ceateby;
		private string _updated;
		private string _updateby;
		private string _status;
		private string _usertype;
		private string _phone;
		private string _email;
        private string _discription;
        private string _isfirstlogin;
        private string _role;
        private string _rolename;
		/// <summary>
		/// 
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserPassword
		{
			set{ _userpassword=value;}
			get{return _userpassword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier
		{
			set{ _supplier=value;}
			get{return _supplier;}
		}
        public string SupplierNum
        {
            set { _suppliernum = value; }
            get { return _suppliernum; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string Created
		{
			set{ _created=value;}
			get{return _created;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CeateBy
		{
			set{ _ceateby=value;}
			get{return _ceateby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Updated
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
		/// <summary>
		/// 
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
        public string Discription
        {
            set { _discription = value; }
            get { return _discription; }
        }
        public string isFirstLogin
        {
            set { _isfirstlogin = value; }
            get { return _isfirstlogin; }
        }
        public string Role
        {
            set { _role = value; }
            get { return _role; }
        }
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
		#endregion Model

	}
}

