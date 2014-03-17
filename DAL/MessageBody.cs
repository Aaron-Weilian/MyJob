using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
//using Maticsoft.DBUtility;//Please add references
namespace com.portal.db.DAL
{
    /// <summary>
    /// 数据访问类:MessageBody
    /// </summary>
    public partial class MessageBody
    {
        public MessageBody()
        { }
        #region  BasicMethod

        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());
        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());



        public object SqlNull(object obj)
        {
            if (obj == null)
                return DBNull.Value;

            return obj;
        }


        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// 
        public bool Exists(string key, string MessageType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Message");
            strSql.Append(" where [key]=@key and MessageType = @MessageType");
            SqlParameter[] parameters = {
					new SqlParameter("@key", SqlDbType.VarChar,32),
                    new SqlParameter("@MessageType", SqlDbType.VarChar,50)};

            parameters[0].Value = key;
            parameters[1].Value = MessageType;

            return dal.Exists(strSql.ToString(), parameters);
        }

        //public bool ExistRefs(string key, string MessageType)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from Message");
        //    strSql.Append(" where [key]=@key and MessageType = @MessageType");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@key", SqlDbType.VarChar,32),
        //            new SqlParameter("@MessageType", SqlDbType.VarChar,50)};

        //    parameters[0].Value = key;
        //    parameters[1].Value = MessageType;

        //    return dal.ExecuteDataSet(strSql.ToString(), parameters);
        //}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.MessageBody model)
        {
            string guid = System.Guid.NewGuid().ToString("N");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Message(");
            strSql.Append("messageID,messageName,[key],creationDateTime,edi_location_code,ship_from,messageType,vender_name,vender_num,vender_site,vender_site_num,duns_num,contact_name,email,phone,address,street,city,postal_code,segment1,segment2,segment3,segment4,segment5,segment6,segment7,segment8,segment9,segment10,status,notes,confirmDateTime,messageAlias,referenceId)");
            strSql.Append(" values (");
            strSql.Append("@messageID,@messageName,@key,@creationDateTime,@edi_location_code,@ship_from,@messageType,@vender_name,@vender_num,@vender_site,@vender_site_num,@duns_num,@contact_name,@email,@phone,@address,@street,@city,@postal_code,@segment1,@segment2,@segment3,@segment4,@segment5,@segment6,@segment7,@segment8,@segment9,@segment10,@status,@notes,@confirmDateTime,@messageAlias,@referenceId)");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.VarChar,32),
					new SqlParameter("@messageName", SqlDbType.VarChar,255),
					new SqlParameter("@key", SqlDbType.VarChar,50),
					new SqlParameter("@creationDateTime", SqlDbType.DateTime),
					new SqlParameter("@edi_location_code", SqlDbType.VarChar,50),
					new SqlParameter("@ship_from", SqlDbType.VarChar,50),
					new SqlParameter("@messageType", SqlDbType.VarChar,50),
					new SqlParameter("@vender_name", SqlDbType.VarChar,255),
					new SqlParameter("@vender_num", SqlDbType.VarChar,50),
					new SqlParameter("@vender_site", SqlDbType.VarChar,255),
					new SqlParameter("@vender_site_num", SqlDbType.VarChar,50),
					new SqlParameter("@duns_num", SqlDbType.VarChar,50),
					new SqlParameter("@contact_name", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,255),
					new SqlParameter("@phone", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,255),
					new SqlParameter("@street", SqlDbType.VarChar,50),
					new SqlParameter("@city", SqlDbType.VarChar,50),
					new SqlParameter("@postal_code", SqlDbType.VarChar,50),
					new SqlParameter("@segment1", SqlDbType.VarChar,50),
					new SqlParameter("@segment2", SqlDbType.VarChar,50),
					new SqlParameter("@segment3", SqlDbType.VarChar,50),
					new SqlParameter("@segment4", SqlDbType.VarChar,50),
					new SqlParameter("@segment5", SqlDbType.VarChar,50),
					new SqlParameter("@segment6", SqlDbType.VarChar,50),
					new SqlParameter("@segment7", SqlDbType.VarChar,50),
					new SqlParameter("@segment8", SqlDbType.VarChar,50),
					new SqlParameter("@segment9", SqlDbType.VarChar,50),
					new SqlParameter("@segment10", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@notes", SqlDbType.VarChar,50),
                    new SqlParameter("@confirmDateTime", SqlDbType.DateTime),
                    new SqlParameter("@messageAlias", SqlDbType.VarChar,50),
                    new SqlParameter("@referenceId", SqlDbType.VarChar,20)                    
                                        };
            parameters[0].Value = guid;
            parameters[1].Value = SqlNull(model.messageName);
            parameters[2].Value = SqlNull(model.key);
            parameters[3].Value = SqlNull(model.creationDateTime);
            parameters[4].Value = SqlNull(model.edi_location_code);
            parameters[5].Value = SqlNull(model.ship_from);
            parameters[6].Value = SqlNull(model.messageType);
            parameters[7].Value = SqlNull(model.vender_name);
            parameters[8].Value = SqlNull(model.vender_num);
            parameters[9].Value = SqlNull(model.vender_site);
            parameters[10].Value = SqlNull(model.vender_site_num);
            parameters[11].Value = SqlNull(model.duns_num);
            parameters[12].Value = SqlNull(model.contact_name);
            parameters[13].Value = SqlNull(model.email);
            parameters[14].Value = SqlNull(model.phone);
            parameters[15].Value = SqlNull(model.address);
            parameters[16].Value = SqlNull(model.street);
            parameters[17].Value = SqlNull(model.city);
            parameters[18].Value = SqlNull(model.postal_code);
            parameters[19].Value = SqlNull(model.segment1);
            parameters[20].Value = SqlNull(model.segment2);
            parameters[21].Value = SqlNull(model.segment3);
            parameters[22].Value = SqlNull(model.segment4);
            parameters[23].Value = SqlNull(model.segment5);
            parameters[24].Value = SqlNull(model.segment6);
            parameters[25].Value = SqlNull(model.segment7);
            parameters[26].Value = SqlNull(model.segment8);
            parameters[27].Value = SqlNull(model.segment9);
            parameters[28].Value = SqlNull(model.segment10);
            parameters[29].Value = SqlNull(model.status);
            parameters[30].Value = SqlNull(model.notes);
            parameters[31].Value = SqlNull(model.confirmDateTime);
            parameters[32].Value = SqlNull(model.messageAlias);
            parameters[33].Value = SqlNull(model.referenceid);

            int rows = dal.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(com.portal.db.Model.MessageBody model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Message set ");
            strSql.Append("messageName=@messageName,");
            strSql.Append("[key]=@key,");
            strSql.Append("creationDateTime=@creationDateTime,");
            strSql.Append("edi_location_code=@edi_location_code,");
            strSql.Append("ship_from=@ship_from,");
            strSql.Append("messageType=@messageType,");
            strSql.Append("vender_name=@vender_name,");
            strSql.Append("vender_num=@vender_num,");
            strSql.Append("vender_site=@vender_site,");
            strSql.Append("vender_site_num=@vender_site_num,");
            strSql.Append("duns_num=@duns_num,");
            strSql.Append("contact_name=@contact_name,");
            strSql.Append("email=@email,");
            strSql.Append("phone=@phone,");
            strSql.Append("address=@address,");
            strSql.Append("street=@street,");
            strSql.Append("city=@city,");
            strSql.Append("postal_code=@postal_code,");
            strSql.Append("segment1=@segment1,");
            strSql.Append("segment2=@segment2,");
            strSql.Append("segment3=@segment3,");
            strSql.Append("segment4=@segment4,");
            strSql.Append("segment5=@segment5,");
            strSql.Append("segment6=@segment6,");
            strSql.Append("segment7=@segment7,");
            strSql.Append("segment8=@segment8,");
            strSql.Append("segment9=@segment9,");
            strSql.Append("segment10=@segment10,");
            strSql.Append("status=@status,");
            strSql.Append("notes=@notes,");
            strSql.Append("confirmDateTime=@confirmDateTime,");
            strSql.Append("messageAlias=@messageAlias,");
            strSql.Append("referenceId=@referenceid");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageName", SqlDbType.VarChar,255),
					new SqlParameter("@key", SqlDbType.VarChar,50),
					new SqlParameter("@creationDateTime", SqlDbType.DateTime),
					new SqlParameter("@edi_location_code", SqlDbType.VarChar,50),
					new SqlParameter("@ship_from", SqlDbType.VarChar,50),
					new SqlParameter("@messageType", SqlDbType.VarChar,50),
					new SqlParameter("@vender_name", SqlDbType.VarChar,255),
					new SqlParameter("@vender_num", SqlDbType.VarChar,50),
					new SqlParameter("@vender_site", SqlDbType.VarChar,255),
					new SqlParameter("@vender_site_num", SqlDbType.VarChar,50),
					new SqlParameter("@duns_num", SqlDbType.VarChar,50),
					new SqlParameter("@contact_name", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,255),
					new SqlParameter("@phone", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,255),
					new SqlParameter("@street", SqlDbType.VarChar,50),
					new SqlParameter("@city", SqlDbType.VarChar,50),
					new SqlParameter("@postal_code", SqlDbType.VarChar,50),
					new SqlParameter("@segment1", SqlDbType.VarChar,50),
					new SqlParameter("@segment2", SqlDbType.VarChar,50),
					new SqlParameter("@segment3", SqlDbType.VarChar,50),
					new SqlParameter("@segment4", SqlDbType.VarChar,50),
					new SqlParameter("@segment5", SqlDbType.VarChar,50),
					new SqlParameter("@segment6", SqlDbType.VarChar,50),
					new SqlParameter("@segment7", SqlDbType.VarChar,50),
					new SqlParameter("@segment8", SqlDbType.VarChar,50),
					new SqlParameter("@segment9", SqlDbType.VarChar,50),
					new SqlParameter("@segment10", SqlDbType.VarChar,50),
					new SqlParameter("@messageID", SqlDbType.VarChar,32),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@notes", SqlDbType.VarChar,50),
                    new SqlParameter("@confirmDateTime", SqlDbType.DateTime),
                    new SqlParameter("@messageAlias", SqlDbType.VarChar,50),
                    new SqlParameter("@referenceid", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = SqlNull(model.messageName);
            parameters[1].Value = SqlNull(model.key);
            parameters[2].Value = SqlNull(model.creationDateTime);
            parameters[3].Value = SqlNull(model.edi_location_code);
            parameters[4].Value = SqlNull(model.ship_from);
            parameters[5].Value = SqlNull(model.messageType);
            parameters[6].Value = SqlNull(model.vender_name);
            parameters[7].Value = SqlNull(model.vender_num);
            parameters[8].Value = SqlNull(model.vender_site);
            parameters[9].Value = SqlNull(model.vender_site_num);
            parameters[10].Value = SqlNull(model.duns_num);
            parameters[11].Value = SqlNull(model.contact_name);
            parameters[12].Value = SqlNull(model.email);
            parameters[13].Value = SqlNull(model.phone);
            parameters[14].Value = SqlNull(model.address);
            parameters[15].Value = SqlNull(model.street);
            parameters[16].Value = SqlNull(model.city);
            parameters[17].Value = SqlNull(model.postal_code);
            parameters[18].Value = SqlNull(model.segment1);
            parameters[19].Value = SqlNull(model.segment2);
            parameters[20].Value = SqlNull(model.segment3);
            parameters[21].Value = SqlNull(model.segment4);
            parameters[22].Value = SqlNull(model.segment5);
            parameters[23].Value = SqlNull(model.segment6);
            parameters[24].Value = SqlNull(model.segment7);
            parameters[25].Value = SqlNull(model.segment8);
            parameters[26].Value = SqlNull(model.segment9);
            parameters[27].Value = SqlNull(model.segment10);
            parameters[28].Value = SqlNull(model.messageID);
            parameters[29].Value = SqlNull(model.status);
            parameters[30].Value = SqlNull(model.notes);
            parameters[31].Value = SqlNull(model.confirmDateTime);
            parameters[32].Value = SqlNull(model.messageAlias);
            parameters[33].Value = SqlNull(model.referenceid);

            int rows = dal.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string messageID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Message ");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.VarChar,32)			};
            parameters[0].Value = messageID;

            int rows = dal.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string messageIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Message ");
            strSql.Append(" where messageID in (" + messageIDlist + ")  ");
            int rows = dal.ExecuteNonQuery(CommandType.Text,strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public com.portal.db.Model.MessageBody GetModel(string messageID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Message ");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.VarChar,32)			};
            parameters[0].Value = messageID;

            com.portal.db.Model.MessageBody model = new com.portal.db.Model.MessageBody();
            DataSet ds = dal.ExecuteDataSet(CommandType.Text,strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public com.portal.db.Model.MessageBody DataRowToModel(DataRow row)
        {
            com.portal.db.Model.MessageBody model = new com.portal.db.Model.MessageBody();
            if (row != null)
            {
                if (row["messageID"] != null)
                {
                    model.messageID = row["messageID"].ToString();
                }
                if (row["messageName"] != null)
                {
                    model.messageName = row["messageName"].ToString();
                }
                if (row["key"] != null)
                {
                    model.key = row["key"].ToString();
                }
                if (row["creationDateTime"] != null && row["creationDateTime"].ToString() != "")
                {
                    model.creationDateTime = DateTime.Parse(row["creationDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (row["confirmDateTime"] != null && row["confirmDateTime"].ToString() != "")
                {
                    model.confirmDateTime = DateTime.Parse(row["confirmDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (row["edi_location_code"] != null)
                {
                    model.edi_location_code = row["edi_location_code"].ToString();
                }
                if (row["ship_from"] != null)
                {
                    model.ship_from = row["ship_from"].ToString();
                }
                if (row["messageType"] != null)
                {
                    model.messageType = row["messageType"].ToString();
                }
                if (row["vender_name"] != null)
                {
                    model.vender_name = row["vender_name"].ToString();
                }
                if (row["vender_num"] != null)
                {
                    model.vender_num = row["vender_num"].ToString();
                }
                if (row["vender_site"] != null)
                {
                    model.vender_site = row["vender_site"].ToString();
                }
                if (row["vender_site_num"] != null)
                {
                    model.vender_site_num = row["vender_site_num"].ToString();
                }
                if (row["duns_num"] != null)
                {
                    model.duns_num = row["duns_num"].ToString();
                }
                if (row["contact_name"] != null)
                {
                    model.contact_name = row["contact_name"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["phone"] != null)
                {
                    model.phone = row["phone"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["street"] != null)
                {
                    model.street = row["street"].ToString();
                }
                if (row["city"] != null)
                {
                    model.city = row["city"].ToString();
                }
                if (row["postal_code"] != null)
                {
                    model.postal_code = row["postal_code"].ToString();
                }
                if (row["segment1"] != null)
                {
                    model.segment1 = row["segment1"].ToString();
                }
                if (row["segment2"] != null)
                {
                    model.segment2 = row["segment2"].ToString();
                }
                if (row["segment3"] != null)
                {
                    model.segment3 = row["segment3"].ToString();
                }
                if (row["segment4"] != null)
                {
                    model.segment4 = row["segment4"].ToString();
                }
                if (row["segment5"] != null)
                {
                    model.segment5 = row["segment5"].ToString();
                }
                if (row["segment6"] != null)
                {
                    model.segment6 = row["segment6"].ToString();
                }
                if (row["segment7"] != null)
                {
                    model.segment7 = row["segment7"].ToString();
                }
                if (row["segment8"] != null)
                {
                    model.segment8 = row["segment8"].ToString();
                }
                if (row["segment9"] != null)
                {
                    model.segment9 = row["segment9"].ToString();
                }
                if (row["segment10"] != null)
                {
                    model.segment10 = row["segment10"].ToString();
                }
                if (row["status"] != null)
                {
                    model.status = row["status"].ToString();
                }
                if (row["referenceId"] != null)
                {
                    model.referenceid = row["referenceId"].ToString();
                }
                if (row["notes"] != null)
                {
                    model.notes = row["notes"].ToString();
                }
                if (row["messageAlias"] != null)
                {
                    model.messageAlias = row["messageAlias"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY Status desc ,creationDateTime desc");
            return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = dal.ExecuteScalar(CommandType.Text,strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.messageID desc");
            }
            strSql.Append(")AS Row, T.*  from Message T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "MessageBody";
            parameters[1].Value = "messageID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

