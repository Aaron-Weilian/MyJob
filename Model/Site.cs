using System;
namespace com.portal.db.Model
{
    /// <summary>
    /// Site:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Site
    {
        public Site()
        { }
        #region Model
        private string _siteid;
        private string _supplierid;
        private string _sitename;
        private string _sitenum;
        private string _namenum;
        /// <summary>
        /// 
        /// </summary>
        public string SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
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

        public string NameNUM
        {
            set { _namenum = value; }
            get { return _namenum; }
        }

        #endregion Model

    }
}

