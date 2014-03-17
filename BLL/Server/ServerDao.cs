using System;
using System.Data;
using System.Collections.Generic;
using com.portal.db.Model;

namespace com.portal.db.BLL
{

    public interface ServerDao{

        bool AddModel(string table, Dictionary<string, string> DataValue);

        //bool UpdateModel(string table, Object model);

        List<Object> GetModelList(string table, Type type, Dictionary<string, string> param);
        
    }

    public class ServerImpl : ServerDao {

        private readonly com.portal.db.DAL.CommomDAL dal = new com.portal.db.DAL.CommomDAL();
        
        public Object GetModel(string table, Type type, Dictionary<string, string> param)
        {
            return GetModelList(table, type, param)[0];
        }


        public bool Exits(string table, Dictionary<string, string> param)
        {
            DataSet ds = dal.GetDataList(table, param);
            if (ds.Tables.Count > 0) return true;

            return false;
        
        }

        public DataRow GetRow(string table, Dictionary<string, string> param)
        {
            DataSet ds = dal.GetDataList(table, param);
            if (ds.Tables.Count > 0) return ds.Tables[0].Rows[0];

            return null;

        }

        public DataRowCollection GetRows(string table, Dictionary<string, string> param)
        {
            DataSet ds = dal.GetDataList(table, param);
            if (ds.Tables.Count > 0) return ds.Tables[0].Rows;

            return null;

        }
        
        
        
        public List<Object> GetModelList(string table, Type type, Dictionary<string, string> param)
        {
            DataSet ds = dal.GetDataList(table, param);

            return Data2List(ds.Tables[0], type);
        }

        private List<Object> Data2List(DataTable dt, Type type)
        {
            List<Object> modelList = new List<Object>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    modelList.Add(dal.Data2Model(dt.Rows[n], type));
                    
                }
            }
            return modelList;
        }

        public int Execute(string CommandText)
        {
            return dal.Execute(CommandText);
        }


        public bool AddModel(string table, Dictionary<string, string> DataValue)
        {


            return true;
        }


        public Boolean saveData(Tool.XMLMessage xmlModel, string messageType, string flag, string filename)
        {
            try
            {
                BLL.MessageBody MessageServer = new BLL.MessageBody();
                
                Model.MessageBody model = new Model.MessageBody();


                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;

                model.segment1 = filename;
                model.messageType = messageType;

                if ("OUT" == flag)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("IN" == flag)
                {
                    model.status = "Unread";
                    model.segment2 = "IN";
                    model.notes = "Creation";
                }


                MessageServer.Add(model);
                model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];



                saveData(xmlModel.messagebody.data,model.messageID);


                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public Boolean updateData(Tool.XMLMessage xmlModel, string messageType, string flag, string filename)
        {
            try
            {
                BLL.MessageBody MessageServer = new BLL.MessageBody();

                Model.MessageBody model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];


                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;

                model.segment1 = filename;
                model.messageType = messageType;

                if ("OUT" == flag)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("IN" == flag)
                {
                    model.status = "Unread";
                    model.segment2 = "IN";
                    model.notes = "Creation";
                }


                MessageServer.Add(model);
                model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];



                updateData(xmlModel.messagebody.data, model.messageID);


                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        public Boolean mergeData(Tool.XMLMessage xmlModel, string messageType, string flag, string filename)
        {
            try
            {
                Model.MessageBody model = new Model.MessageBody();

                BLL.MessageBody MessageServer = new BLL.MessageBody();

                List<Model.MessageBody> list = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'");

                if (list.Count > 0) model = list[0];


                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"):xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;

                model.segment1 = filename;
                model.segment4 = "F";
                model.messageType = messageType;

               

                if (list.Count > 0)
                {
                    MessageServer.Update(model);
                    updateData(xmlModel.messagebody.data, model.messageID);
                }
                else {
                    MessageServer.Add(model);
                    model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];
                    saveData(xmlModel.messagebody.data, model.messageID);
                }

                if ("OUT" == flag)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                    model.segment4 = "T";
                }
                if ("IN" == flag)
                {
                    model.status = "Unread";
                    model.segment2 = "IN";
                    model.notes = "Creation";
                    model.segment4 = "T";
                }

                MessageServer.Update(model);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

       // public Tool.PMessage




        void saveData(Tool.Table bodyData,string refkey)
        {
            //生成根主键
            string key = null;

            //保存字段
            if (!bodyData.isNullColumn)
            {
                key = Guid.NewGuid().ToString("N");

                foreach (Tool.Column field in bodyData.getColumns()) {

                    //绑定主键
                    if (field.iskey ) bodyData.addDataValue(field.fieldname, key);
                    //绑定外键
                    if (field.isrefkey ) bodyData.addDataValue(field.fieldname, refkey);
                }
                
               //保存记录
               dal.AddModel(bodyData.tableName, bodyData.getData());
               
            }

            if (key == null) key = refkey;

            //迭代同级
            foreach (Tool.Table mb in bodyData.Brothers)
            {
                saveData(mb, refkey);
            }
            //迭代子集
            foreach (Tool.Table mb in bodyData.Childs)
            {
                saveData(mb, key);
            }
        }

        void updateData(Tool.Table bodyData, string refkey)
        {
            string key = null;

            if (!bodyData.isNullColumn)
            {
                key = Guid.NewGuid().ToString("N");
                bool flag = false;
                string param = null;
                string value = null;
                foreach (Tool.Column field in bodyData.getColumns())
                {
                    if (field.isrefkey ) bodyData.addDataValue(field.fieldname, refkey);
                    
                    //处理主键
                    if (field.iskey ) {
                        //Dictionary<string,string> dic = new Dictionary<string,string>();
                        //dic.Add(field.fieldname,bodyData.GetDataValue(field.fieldname));

                        if (Exits(field.tablename, bodyData.getData()))
                        {
                            param = field.fieldname;
                            value = GetRow(bodyData.tableName, bodyData.getData())[field.fieldname].ToString();
                            //bodyData.FieldValue.Add(field.fieldname, GetRows(bodyData.nodename, dic)[field.fieldname].ToString());
                            key = value;
                            flag = true;
                        }
                        else
                        {
                            bodyData.addDataValue(field.fieldname, key);
                        }
                    }
                }

                if (flag)
                    dal.UpdateModel(bodyData.tableName, bodyData.getData(), param, value);
                else dal.AddModel(bodyData.tableName, bodyData.getData());

            }

            if (key == null) key = refkey;

            foreach (Tool.Table mb in bodyData.Brothers)
            {
                updateData(mb,refkey);
            }
            foreach (Tool.Table mb in bodyData.Childs)
            {
                updateData(mb,key);
            }
        }



    
    }

    public class ConvertModel : ServerImpl
    {

        public BodyStructure getRoot(string type)
        {

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("MessageType", type);
            param.Add("Name", "MESSAGEBODY");

            return (BodyStructure)GetModel("Data_Structure", typeof(BodyStructure), param);
        }

        //迭代获取消息结构
        public Tool.Structure Convert(string type, bool isRoot, BodyStructure body)
        {
            Tool.Structure mbody = new Tool.Structure();
            mbody.key = body.ID;
            mbody.name = body.Name.ToUpper();
            mbody.messageType = type;
            mbody.isRoot = isRoot;

            Dictionary<string, string> param1 = new Dictionary<string, string>();
            param1.Add("SOBJECTNO", body.ObjectNO);

            foreach (DataField field in GetModelList("Data_Field", typeof(DataField), param1))
            {
                Tool.Column model = new Tool.Column();

                model.tablename = field.STABLE;
                model.fieldname = field.SFIELDAS;
                model.xmlname = field.SFIELD.ToUpper();
                model.isSys = false;
                model.isXml = false;
                model.isrefkey = false;
                model.iskey = false;



                if (field.LSYS == "1") model.isSys = true;//系统字段

                if (field.SXML == "1") model.isXml = true;//XML文件字段

                if (field.SKEY == "1") model.iskey = true; //主键

                if (field.SREFKEY == "1") model.isrefkey = true;// 外键


                mbody.Field.Add(model);
            }


            Dictionary<string, string> param2 = new Dictionary<string, string>();
            param2.Add("MessageType", type);
            param2.Add("Parent", body.ID);

            List<Object> b = GetModelList("Data_Structure", typeof(BodyStructure), param2);

            foreach (BodyStructure bs in GetModelList("Data_Structure", typeof(BodyStructure), param2))
            {
                mbody.Childs.Add(Convert(type, false, bs));
            }

            return mbody;
        }

        //获取结构数据
        public Tool.Table GetTableData(Tool.Structure structure, string refkey)
        {

            Tool.Table table = new Tool.Table();
            table.xmlname = structure.name;
            
            string key = refkey;

            string tableName = null;
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (structure.Field.Count>0)
            {
               

                //获取外键ID
                foreach (Tool.Column column in structure.Field)
                {
                    tableName = column.tablename;

                    if (column.isrefkey)
                    {
                        param.Add(column.fieldname, refkey);
                    }
                }
            }

            if (param.Count > 0)
            {
                //使用外键查询出记录

                foreach (DataRow row in GetRows(tableName, param))
                {
                    Tool.Table brother = new Tool.Table();
                    brother.xmlname = structure.name;

                    foreach (Tool.Column column in structure.Field)
                    {

                        if (column.iskey)
                        {
                            key = row[column.fieldname].ToString();
                        }

                        brother.addDataValue(column.fieldname, row[column.fieldname].ToString());

                    }

                    brother.setColumns(structure.Field);

                    foreach (Tool.Structure strut in structure.Childs)
                    {
                        brother.Childs.Add(GetTableData(strut, key));
                    }

                    table.Brothers.Add(brother);

                }
            }
                

            //foreach (Tool.Structure strut in structure.Childs)
            //{
            //    table.Childs.Add(Convert2Data(strut, key));
            //}

            return table;

        }

        public Tool.Table Convert2Data(Tool.Structure structure, string refkey) {

            Tool.Table table = new Tool.Table();
            table.xmlname = structure.name;

            if (structure.name == "MESSAGEBODY")
            {
                foreach (Tool.Structure strut in structure.Childs)
                {
                    table.Childs.Add(GetTableData(strut, refkey));
                }
            }
            else { 
               table = GetTableData(structure, refkey);
            }

            return table;
        }

        public Tool.XMLMessage Convert2Data(Model.MessageBody model, Tool.Structure structure)
        {
            try
            {
                Tool.XMLMessage pm = new Tool.XMLMessage();

                Tool.MessageSender sender = new Tool.MessageSender();
                Tool.MessagePartner partner = new Tool.MessagePartner();

                sender.edi_location_code = model.edi_location_code;
                sender.messageAlias = model.messageAlias;
                sender.messageName = model.messageName;
                sender.referenceID = model.referenceid;
                sender.key = model.key;
                sender.creationDateTime = model.creationDateTime.ToString();

                partner.vender_name = model.vender_name;
                partner.vender_site = model.vender_site;
                partner.vender_site_num = model.vender_site_num;
                partner.vender_num = model.vender_num;
                partner.address = model.address;
                partner.city = model.city;
                partner.contact_name = model.contact_name;
                partner.duns_num = model.duns_num;
                partner.email = model.email;
                partner.phone = model.phone;
                partner.postal_code = model.postal_code;
                partner.street = model.street;


                pm.sender = sender;
                pm.partner = partner;

                Tool.MessageContext body = new Tool.MessageContext();

                body.data = Convert2Data(structure, model.messageID);

                pm.messagebody = body;

                return pm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    
    }
}
