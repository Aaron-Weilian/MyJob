using System;
namespace com.portal.db.Model
{
    /// <summary>
    /// MessageBody:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class MessageBody : BaseModel
    {
        public MessageBody()
        { }
        #region Model
        private string _messageid;
        //private string _messagebodyid;
        private string _messagename;
        private string _key;
        private string _referenceid;
        private string _messageAlias;
        private string _confirmdatetime;
        private string _creationdatetime;
        private string _edi_location_code;
        private string _ship_from;
        private string _messagetype;
        private string _vender_name;
        private string _vender_num;
        private string _vender_site;
        private string _vender_site_num;
        private string _duns_num;
        private string _contact_name;
        private string _email;
        private string _phone;
        private string _address;
        private string _street;
        private string _city;
        private string _postal_code;
        private string _status;
        private string _notes;
        private string _segment1;
        private string _segment2;
        private string _segment3;
        private string _segment4;
        private string _segment5;
        private string _segment6;
        private string _segment7;
        private string _segment8;
        private string _segment9;
        private string _segment10;
        /// <summary>
        /// 
        /// </summary>
        public string messageID
        {
            set { _messageid = value; }
            get { return _messageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        //public string messageBodyID
        //{
        //    set { _messagebodyid = value; }
        //    get { return _messagebodyid; }
        //}
        /// <summary>
        /// 
        /// </summary>
        public string messageName
        {
            set { _messagename = value; }
            get { return _messagename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string messageAlias
        {
            set { _messageAlias = value; }
            get { return _messageAlias; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string referenceid
        {
            set { _referenceid = value; }
            get { return _referenceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string key
        {
            set { _key = value; }
            get { return _key; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string creationDateTime
        {
            set { _creationdatetime = value; }
            get { return _creationdatetime; }
        }

        public string confirmDateTime
        {
            set { _confirmdatetime = value; }
            get { return _confirmdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string edi_location_code
        {
            set { _edi_location_code = value; }
            get { return _edi_location_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ship_from
        {
            set { _ship_from = value; }
            get { return _ship_from; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string messageType
        {
            set { _messagetype = value; }
            get { return _messagetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vender_name
        {
            set { _vender_name = value; }
            get { return _vender_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vender_num
        {
            set { _vender_num = value; }
            get { return _vender_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vender_site
        {
            set { _vender_site = value; }
            get { return _vender_site; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vender_site_num
        {
            set { _vender_site_num = value; }
            get { return _vender_site_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string duns_num
        {
            set { _duns_num = value; }
            get { return _duns_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_name
        {
            set { _contact_name = value; }
            get { return _contact_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string street
        {
            set { _street = value; }
            get { return _street; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postal_code
        {
            set { _postal_code = value; }
            get { return _postal_code; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string segment1
        {
            set { _segment1 = value; }
            get { return _segment1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment2
        {
            set { _segment2 = value; }
            get { return _segment2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment3
        {
            set { _segment3 = value; }
            get { return _segment3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment4
        {
            set { _segment4 = value; }
            get { return _segment4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment5
        {
            set { _segment5 = value; }
            get { return _segment5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment6
        {
            set { _segment6 = value; }
            get { return _segment6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment7
        {
            set { _segment7 = value; }
            get { return _segment7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment8
        {
            set { _segment8 = value; }
            get { return _segment8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment9
        {
            set { _segment9 = value; }
            get { return _segment9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string segment10
        {
            set { _segment10 = value; }
            get { return _segment10; }
        }
        #endregion Model

    }
}

