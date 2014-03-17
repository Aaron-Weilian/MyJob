using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{

   public class MessageContext {

       public Structure structure { set; get; }

       public Table data = new Table();
    }
    
    public class Structure
    {
        public string name { set; get; }
        public bool isRoot { set; get; }
        public string messageType { set; get; }
        public string key { set; get; }

        public List<Structure> Childs = new List<Structure>();

        public List<Column> Field = new List<Column>();
    }

    public class Column {

        public string tablename { set; get; }

        public string xmlname { set; get; }

        public string fieldname { set; get; }
        //是否是系统字段
        public bool isXml { set; get; }
        //是否是XML文件字段
        public bool isSys { set; get; }
        //是否是主键
        public bool iskey { set; get; }
        //是否是外键
        public bool isrefkey { set; get; }
    }

    public class Table
    {
        public string tableName{ set; get; }

        public string xmlname { set; get; }

        public string keyName { set; get; }

        public string keyValue { set; get; }

        public string refkeyName { set; get; }

        public string refkeyValue { set; get; }

        public List<Table> Childs = new List<Table>();

        public List<Table> Brothers = new List<Table>();

        private List<Column> Columns = new List<Column>();

        private Dictionary<string, string> DataValue = new Dictionary<string, string>();
        
        public String getDataValue(string key) {

            return DataValue[key];
        }

        public void addDataValue(string key, string value) {

            DataValue.Add(key, value);
        }
        
        public void removeDataValue(string key) {
            DataValue.Remove(key);

        }

        public void addColumn(Column item) {
            
            Columns.Add(item);
        }

        public void removeColumn(Column item) {
            Columns.Remove(item);
        }

        public Dictionary<string, string> getData() {
            return DataValue;
        }

        public List<Column> getColumns() {
            return Columns;
        }

        public void setColumns(List<Column> value)
        {
            Columns = value;
        }

        public Boolean isNullColumn {

          
            get
            {
                if (Columns.Count == 0) return true;

                return false;
                
            }
        }
    
    }
}
