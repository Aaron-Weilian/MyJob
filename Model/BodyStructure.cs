using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.portal.db.Model
{
    public class BodyStructure
    {
        private string _id;
        private string _messageType;
        private string _parent;
        private string _name;
        private string _tablename;
        private string _objectno;

        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        public string MessageType
        {
            set { _messageType = value; }
            get { return _messageType; }
        }

        public string Parent
        {
            set { _parent = value; }
            get { return _parent; }
        }

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public string ObjectNO
        {
            set { _objectno = value; }
            get { return _objectno; }
        }
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }

       
    }

    //public class BodyData {

    //    private MessageBody body;

    //    private MessageTable table;
    
    
    
    //}

    //public class MessageTable {

    //    public List<MessageTable> Childs = new List<MessageTable>();
    //    public List<MessageTable> Brothers = new List<MessageTable>();
    //    public List<DataField> DataField = new List<DataField>();
    //    public Dictionary<string, string> FieldValue = new Dictionary<string, string>();
    //}
}
