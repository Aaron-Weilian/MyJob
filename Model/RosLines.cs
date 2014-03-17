using System;
namespace com.portal.db.Model
{
	/// <summary>
	/// RosLines:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class RosLines : BaseModel
	{
		public RosLines()
		{}
		#region Model
		private string _roslineid;
        private string _rosdemanddate;
		private string _rosdemandquantity;
		private string _rosetaqty;
		private string _rosetdqty;
		private string _rosdio;
        private string _roscumeta;
		private string _rosshortageqty;
		private string _rosheaderid;
        private string _po_no;
		/// <summary>
		/// 
		/// </summary>
		public string RosLineID
		{
			set{ _roslineid=value;}
			get{return _roslineid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosDemandDate
		{
			set{ _rosdemanddate=value;}
			get{return _rosdemanddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosDemandQuantity
		{
			set{ _rosdemandquantity=value;}
			get{return _rosdemandquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosEtaQty
		{
			set{ _rosetaqty=value;}
			get{return _rosetaqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosEtdQty
		{
			set{ _rosetdqty=value;}
			get{return _rosetdqty;}
		}

        public string RosCumEta {

            set { _roscumeta = value; }
            get { return _roscumeta; }
        
        }

		/// <summary>
		/// 
		/// </summary>
		public string RosDio
		{
			set{ _rosdio=value;}
			get{return _rosdio;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosShortageQty
		{
			set{ _rosshortageqty=value;}
			get{return _rosshortageqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RosHeaderID
		{
			set{ _rosheaderid=value;}
			get{return _rosheaderid;}
		}

        public string PO_NO
        {
            set { _po_no = value; }
            get { return _po_no; }
        }

		#endregion Model

	}
}

