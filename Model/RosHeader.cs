
using System;
namespace com.portal.db.Model
{
	/// <summary>
	/// RosHeader:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class RosHeader : BaseModel
	{
		public RosHeader()
		{}
		#region Model
		private string _rosheaderid;
        private string _glod_plan_flag;
		private string _rositemno;
		private string _rosdesc;
		private string _rosmodel;
		private string _roscategory;
		private string _rosbuyer;
		private string _rosallocationpercent;
		private string _rosleadtime;
		private string _rosstockqty;
		private string _rossafestock;
		private string _ponumber;
		private string _openpoqty;
		private string _messagebodyid;
        private string _vmistock;
        private string _updateflag;

		/// <summary>
		/// 
		/// </summary>
		public string RosHeaderID
		{
			set{ _rosheaderid=value;}
			get{return _rosheaderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosItemNO
		{
			set{ _rositemno=value;}
			get{return _rositemno;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string Glod_plan_flag
        {
            set { _glod_plan_flag = value; }
            get { return _glod_plan_flag; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string RosDesc
		{
			set{ _rosdesc=value;}
			get{return _rosdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosModel
		{
			set{ _rosmodel=value;}
			get{return _rosmodel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosCategory
		{
			set{ _roscategory=value;}
			get{return _roscategory;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosBuyer
		{
			set{ _rosbuyer=value;}
			get{return _rosbuyer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosAllocationPercent
		{
			set{ _rosallocationpercent=value;}
			get{return _rosallocationpercent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosLeadTime
		{
			set{ _rosleadtime=value;}
			get{return _rosleadtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosStockQty
		{
			set{ _rosstockqty=value;}
			get{return _rosstockqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosSafeStock
		{
			set{ _rossafestock=value;}
			get{return _rossafestock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PoNumber
		{
			set{ _ponumber=value;}
			get{return _ponumber;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string VmiStock
        {
            set { _vmistock = value; }
            get { return _vmistock; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string OpenPoQty
		{
			set{ _openpoqty=value;}
			get{return _openpoqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MessageBodyID
		{
			set{ _messagebodyid=value;}
            get { return _messagebodyid; }
		}
        /// <summary>
        /// 
        /// </summary>
        public string UpdateFlag
        {
            set { _updateflag = value; }
            get { return _updateflag; }
        }

		#endregion Model

	}
}

