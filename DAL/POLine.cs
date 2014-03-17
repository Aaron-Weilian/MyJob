using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
//using Maticsoft.DBUtility;//Please add references
namespace com.portal.db.DAL
{
    /// <summary>
    /// 数据访问类:POLine
    /// </summary>
    public partial class POLine
    {
        public POLine()
        { }
        #region  BasicMethod

        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string POLineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from POLine");
            strSql.Append(" where POLineID=@POLineID ");
            SqlParameter[] parameters = {
					new SqlParameter("@POLineID", SqlDbType.VarChar,32)			};
            parameters[0].Value = POLineID;

            return dal.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.POLine model)
        {
            string guid = System.Guid.NewGuid().ToString("N");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into POLine(");
            strSql.Append("POLineID,POHeaderID,[lineNo],item_no,request_qty,request_delivery_date,unit_price,[desc],price_unit,line_item_tatoal_amount,schedule_delivery_date,schedule_delivery_qty,curr,Schedule_Arrive_Date)");
            strSql.Append(" values (");
            strSql.Append("@POLineID,@POHeaderID,@lineNo,@item_no,@request_qty,@request_delivery_date,@unit_price,@desc,@price_unit,@line_item_tatoal_amount,@schedule_delivery_date,@schedule_delivery_qty,@curr,@Schedule_Arrive_Date)");
            SqlParameter[] parameters = {
					new SqlParameter("@POLineID", SqlDbType.VarChar,32),
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32),
					new SqlParameter("@lineNo", SqlDbType.VarChar,50),
					new SqlParameter("@item_no", SqlDbType.VarChar,50),
					new SqlParameter("@request_qty", SqlDbType.VarChar,50),
					new SqlParameter("@request_delivery_date", SqlDbType.VarChar,20),
					new SqlParameter("@unit_price", SqlDbType.VarChar,50),
					new SqlParameter("@desc", SqlDbType.VarChar,50),
					new SqlParameter("@price_unit", SqlDbType.VarChar,50),
					new SqlParameter("@line_item_tatoal_amount", SqlDbType.VarChar,50),
					new SqlParameter("@schedule_delivery_date", SqlDbType.VarChar,20),
					new SqlParameter("@schedule_delivery_qty", SqlDbType.VarChar,50),
                    new SqlParameter("@curr", SqlDbType.VarChar,50),
                    new SqlParameter("@Schedule_Arrive_Date", SqlDbType.VarChar,50)};
            parameters[0].Value = guid;
            parameters[1].Value = model.POHeaderID;
            parameters[2].Value = model.lineNo == null ? DBNull.Value.ToString() : model.lineNo;
            parameters[3].Value = model.item_no == null ? DBNull.Value.ToString() : model.item_no;
            parameters[4].Value = model.request_qty == null ? DBNull.Value.ToString() : model.request_qty;
            parameters[5].Value = model.request_delivery_date == null ? DBNull.Value.ToString() : model.request_delivery_date.ToString();
            parameters[6].Value = model.unit_price == null ? DBNull.Value.ToString() : model.unit_price;
            parameters[7].Value = model.desc == null ? DBNull.Value.ToString() : model.desc;
            parameters[8].Value = model.price_unit == null ? DBNull.Value.ToString() : model.price_unit;
            parameters[9].Value = model.line_item_tatoal_amount == null ? DBNull.Value.ToString() : model.line_item_tatoal_amount;
            parameters[10].Value = model.schedule_delivery_date == null ? DBNull.Value.ToString() : model.schedule_delivery_date.ToString();
            parameters[11].Value = model.schedule_delivery_qty == null ? DBNull.Value.ToString() : model.schedule_delivery_qty;
            parameters[12].Value = model.curr == null ? DBNull.Value.ToString() : model.curr;
            parameters[13].Value = model.Schedule_Arrive_Date == null ? DBNull.Value.ToString() : model.Schedule_Arrive_Date.ToString(); 
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
        public bool Update(com.portal.db.Model.POLine model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update POLine set ");
            strSql.Append("POHeaderID=@POHeaderID,");
            strSql.Append("[lineNo]=@lineNo,");
            strSql.Append("item_no=@item_no,");
            strSql.Append("request_qty=@request_qty,");
            strSql.Append("request_delivery_date=@request_delivery_date,");
            strSql.Append("unit_price=@unit_price,");
            strSql.Append("[desc]=@desc,");
            strSql.Append("price_unit=@price_unit,");
            strSql.Append("line_item_tatoal_amount=@line_item_tatoal_amount,");
            strSql.Append("schedule_delivery_date=@schedule_delivery_date,");
            strSql.Append("schedule_delivery_qty=@schedule_delivery_qty,");
            strSql.Append("curr=@curr,");
            strSql.Append("Schedule_Arrive_Date=@Schedule_Arrive_Date");
            strSql.Append(" where POLineID=@POLineID ");
            SqlParameter[] parameters = {
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32),
					new SqlParameter("@lineNo", SqlDbType.VarChar,50),
					new SqlParameter("@item_no", SqlDbType.VarChar,50),
					new SqlParameter("@request_qty", SqlDbType.VarChar,50),
					new SqlParameter("@request_delivery_date", SqlDbType.VarChar,20),
					new SqlParameter("@unit_price", SqlDbType.VarChar,50),
					new SqlParameter("@desc", SqlDbType.VarChar,50),
					new SqlParameter("@price_unit", SqlDbType.VarChar,50),
					new SqlParameter("@line_item_tatoal_amount", SqlDbType.VarChar,50),
					new SqlParameter("@schedule_delivery_date", SqlDbType.VarChar,20),
					new SqlParameter("@schedule_delivery_qty", SqlDbType.VarChar,50),
                    new SqlParameter("@curr", SqlDbType.VarChar,50),
					new SqlParameter("@POLineID", SqlDbType.VarChar,32),
                    new SqlParameter("@Schedule_Arrive_Date", SqlDbType.VarChar,50)};
            parameters[0].Value = model.POHeaderID;
            parameters[1].Value = model.lineNo == null ? DBNull.Value.ToString() : model.lineNo;
            parameters[2].Value = model.item_no == null ? DBNull.Value.ToString() : model.item_no;
            parameters[3].Value = model.request_qty == null ? DBNull.Value.ToString() : model.request_qty;
            parameters[4].Value = model.request_delivery_date == null ? DBNull.Value.ToString() : model.request_delivery_date.ToString();
            parameters[5].Value = model.unit_price == null ? DBNull.Value.ToString() : model.unit_price;
            parameters[6].Value = model.desc == null ? DBNull.Value.ToString() : model.desc;
            parameters[7].Value = model.price_unit == null ? DBNull.Value.ToString() : model.price_unit;
            parameters[8].Value = model.line_item_tatoal_amount == null ? DBNull.Value.ToString() : model.line_item_tatoal_amount;
            parameters[9].Value = model.schedule_delivery_date == null ? DBNull.Value.ToString() : model.schedule_delivery_date.ToString();
            parameters[10].Value = model.schedule_delivery_qty == null ? DBNull.Value.ToString() : model.schedule_delivery_qty;
            parameters[11].Value = model.curr == null ? DBNull.Value.ToString() : model.curr;
            parameters[12].Value = model.POLineID;
            parameters[13].Value = model.Schedule_Arrive_Date == null ? DBNull.Value.ToString() : model.Schedule_Arrive_Date.ToString(); 
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
        public bool Delete(string POLineID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from POLine ");
            strSql.Append(" where POLineID=@POLineID ");
            SqlParameter[] parameters = {
					new SqlParameter("@POLineID", SqlDbType.VarChar,32)			};
            parameters[0].Value = POLineID;

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
        public bool DeleteList(string POLineIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from POLine ");
            strSql.Append(" where POLineID in (" + POLineIDlist + ")  ");
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
        public com.portal.db.Model.POLine GetModel(string POLineID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from POLine ");
            strSql.Append(" where POLineID=@POLineID ");
            strSql.Append(" ORDER BY [lineNO]");
            SqlParameter[] parameters = {
					new SqlParameter("@POLineID", SqlDbType.VarChar,32)			};
            parameters[0].Value = POLineID;

            com.portal.db.Model.POLine model = new com.portal.db.Model.POLine();
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
        public com.portal.db.Model.POLine DataRowToModel(DataRow row)
        {
            com.portal.db.Model.POLine model = new com.portal.db.Model.POLine();
            if (row != null)
            {
                if (row["POLineID"] != null)
                {
                    model.POLineID = row["POLineID"].ToString();
                }
                if (row["POHeaderID"] != null)
                {
                    model.POHeaderID = row["POHeaderID"].ToString();
                }
                if (row["lineNo"] != null)
                {
                    model.lineNo = row["lineNo"].ToString();
                }
                if (row["item_no"] != null)
                {
                    model.item_no = row["item_no"].ToString();
                }
                if (row["request_qty"] != null)
                {
                    model.request_qty = row["request_qty"].ToString();
                }
                if (row["request_delivery_date"] != null && row["request_delivery_date"].ToString() != "")
                {
                    model.request_delivery_date = row["request_delivery_date"].ToString();
                }
                if (row["unit_price"] != null)
                {
                    model.unit_price = row["unit_price"].ToString();
                }
                if (row["desc"] != null)
                {
                    model.desc = row["desc"].ToString();
                }
                if (row["curr"] != null)
                {
                    model.curr = row["curr"].ToString();
                }
                if (row["price_unit"] != null)
                {
                    model.price_unit = row["price_unit"].ToString();
                }
                if (row["line_item_tatoal_amount"] != null)
                {
                    model.line_item_tatoal_amount = row["line_item_tatoal_amount"].ToString();
                }
                if (row["schedule_delivery_date"] != null && row["schedule_delivery_date"].ToString() != "")
                {
                    model.schedule_delivery_date = row["schedule_delivery_date"].ToString();
                }
                if (row["schedule_delivery_qty"] != null)
                {
                    model.schedule_delivery_qty = row["schedule_delivery_qty"].ToString();
                }
                if (row["Schedule_Arrive_Date"] != null)
                {
                    model.Schedule_Arrive_Date = row["Schedule_Arrive_Date"].ToString();
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
            strSql.Append(" FROM POLine ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY [lineNO]");
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
            strSql.Append(" FROM POLine ");
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
            strSql.Append("select count(1) FROM POLine ");
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
                strSql.Append("order by T.POLineID desc");
            }
            strSql.Append(")AS Row, T.*  from POLine T ");
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
            parameters[0].Value = "POLine";
            parameters[1].Value = "POLineID";
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

