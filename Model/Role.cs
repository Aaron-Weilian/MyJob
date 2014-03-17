using System;
namespace com.portal.db.Model
{
    /// <summary>
    /// Role:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Role
    {
        public Role()
        { }
        #region Model
        private string _roleid;
        private string _rolename;
        private string _rolenum;
        private string _created;
        private string _createby;
        private string _updated;
        private string _updateby;
        private string _discription;
        /// <summary>
        /// 
        /// </summary>
        public string RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleNUM
        {
            set { _rolenum = value; }
            get { return _rolenum; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public string Created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateBy
        {
            set { _createby = value; }
            get { return _createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Updated
        {
            set { _updated = value; }
            get { return _updated; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateBy
        {
            set { _updateby = value; }
            get { return _updateby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Discription
        {
            set { _discription = value; }
            get { return _discription; }
        }
        #endregion Model

    }
}

