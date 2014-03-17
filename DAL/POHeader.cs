using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
//using Maticsoft.DBUtility;//Please add references
namespace com.portal.db.DAL
{
    /// <summary>
    /// 数据访问类:POHeader
    /// </summary>
    public partial class POHeader
    {
        public POHeader()
        { }
        #region  BasicMethod

        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string POHeaderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from POHeader");
            strSql.Append(" where POHeaderID=@POHeaderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32)			};
            parameters[0].Value = POHeaderID;

            return dal.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.POHeader model)
        {
            string guid = System.Guid.NewGuid().ToString("N");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into POHeader(");
            strSql.Append("POHeaderID,messageBodyID,buyer,po_date,po_number,po_type,your_reference,[desc],delivery_location,currency,terms_of_delivery,terms_of_payment,means_of_transport,supplier,supplier_site,supplier_address,bonded,delivery_address)");
            strSql.Append(" values (");
            strSql.Append("@POHeaderID,@messageBodyID,@buyer,@po_date,@po_number,@po_type,@your_reference,@desc,@delivery_location,@currency,@terms_of_delivery,@terms_of_payment,@meas_of_transport,@supplier,@supplier_site,@supplier_address,@bonded,@delivery_address)");
            SqlParameter[] parameters = {
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32),
					new SqlParameter("@messageBodyID", SqlDbType.VarChar,32),
					new SqlParameter("@buyer", SqlDbType.VarChar,255),
					new SqlParameter("@po_date", SqlDbType.VarChar,20),
					new SqlParameter("@po_number", SqlDbType.VarChar,50),
					new SqlParameter("@po_type", SqlDbType.VarChar,50),
					new SqlParameter("@your_reference", SqlDbType.VarChar,50),
					new SqlParameter("@desc", SqlDbType.VarChar,50),
					new SqlParameter("@delivery_location", SqlDbType.VarChar,50),
					new SqlParameter("@currency", SqlDbType.VarChar,50),
					new SqlParameter("@terms_of_delivery", SqlDbType.VarChar,50),
                    new SqlParameter("@terms_of_payment", SqlDbType.VarChar,50),
                    new SqlParameter("@meas_of_transport", SqlDbType.VarChar,50),                    
                    new SqlParameter("@supplier", SqlDbType.VarChar,255),
                    new SqlParameter("@supplier_site", SqlDbType.VarChar,255),
                    new SqlParameter("@supplier_address", SqlDbType.VarChar,255),
                    new SqlParameter("@bonded", SqlDbType.VarChar,50),
                    new SqlParameter("@delivery_address", SqlDbType.VarChar,255)                    };
            parameters[0].Value = guid;
            parameters[1].Value = model.messageBodyID == null ? DBNull.Value.ToString() : model.messageBodyID;
            parameters[2].Value = model.buyer == null ? DBNull.Value.ToString() : model.buyer;
            parameters[3].Value = model.po_date == null ? DBNull.Value.ToString() : model.po_date;
            parameters[4].Value = model.po_number == null ? DBNull.Value.ToString() : model.po_number;
            parameters[5].Value = model.po_type == null ? DBNull.Value.ToString() : model.po_type;
            parameters[6].Value = model.your_reference == null ? DBNull.Value.ToString() : model.your_reference;
            parameters[7].Value = model.desc == null ? DBNull.Value.ToString() : model.desc;
            parameters[8].Value = model.delivery_location == null ? DBNull.Value.ToString() : model.delivery_location;
            parameters[9].Value = model.currency == null ? DBNull.Value.ToString() : model.currency;
            parameters[10].Value = model.terms_of_delivery == null ? DBNull.Value.ToString() : model.terms_of_delivery;
            parameters[11].Value = model.terms_of_payment == null ? DBNull.Value.ToString() : model.terms_of_payment;
            parameters[12].Value = model.means_of_transport == null ? DBNull.Value.ToString() : model.means_of_transport;
            parameters[13].Value = model.supplier == null ? DBNull.Value.ToString() : model.supplier;
            parameters[14].Value = model.supplier_site == null ? DBNull.Value.ToString() : model.supplier_site;
            parameters[15].Value = model.supplier_address == null ? DBNull.Value.ToString() : model.supplier_address;
            parameters[16].Value = model.bonded == null ? DBNull.Value.ToString() : model.bonded;
            parameters[17].Value = model.delivery_address == null ? DBNull.Value.ToString() : model.delivery_address;




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
        public bool Update(com.portal.db.Model.POHeader model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update POHeader set ");
            strSql.Append("messageBodyID=@messageBodyID,");
            strSql.Append("buyer=@buyer,");
            strSql.Append("po_date=@po_date,");
            strSql.Append("po_number=@po_number,");
            strSql.Append("po_type=@po_type,");
            strSql.Append("your_reference=@your_reference,");
            strSql.Append("[desc]=@desc,");
            strSql.Append("delivery_location=@delivery_location,");
            strSql.Append("currency=@currency,");
            strSql.Append("terms_of_delivery=@terms_of_delivery,");
            strSql.Append("terms_of_payment=@terms_of_payment,");
            strSql.Append("means_of_transport=@meas_of_transport,");
            strSql.Append("supplier=@supplier,");
            strSql.Append("supplier_site=@supplier_site,");
            strSql.Append("supplier_address=@supplier_address,");
            strSql.Append("bonded=@bonded,");
            strSql.Append("delivery_address=@delivery_address");
            strSql.Append(" where POHeaderID=@POHeaderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageBodyID", SqlDbType.VarChar,32),
					new SqlParameter("@buyer", SqlDbType.VarChar,50),
					new SqlParameter("@po_date", SqlDbType.VarChar,20),
					new SqlParameter("@po_number", SqlDbType.VarChar,50),
					new SqlParameter("@po_type", SqlDbType.VarChar,50),
					new SqlParameter("@your_reference", SqlDbType.VarChar,50),
					new SqlParameter("@desc", SqlDbType.VarChar,50),
					new SqlParameter("@delivery_location", SqlDbType.VarChar,50),
					new SqlParameter("@currency", SqlDbType.VarChar,50),
					new SqlParameter("@terms_of_delivery", SqlDbType.VarChar,50),
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32),
                    new SqlParameter("@terms_of_payment", SqlDbType.VarChar,50),
                    new SqlParameter("@meas_of_transport", SqlDbType.VarChar,50),                    
                    new SqlParameter("@supplier", SqlDbType.VarChar,255),
                    new SqlParameter("@supplier_site", SqlDbType.VarChar,255),
                    new SqlParameter("@supplier_address", SqlDbType.VarChar,255),
                    new SqlParameter("@bonded", SqlDbType.VarChar,50),
                    new SqlParameter("@delivery_address", SqlDbType.VarChar,255) };
            parameters[0].Value = model.messageBodyID;
            parameters[1].Value = model.buyer == null ? DBNull.Value.ToString() : model.buyer;
            parameters[2].Value = model.po_date == null ? DBNull.Value.ToString() : model.po_date;
            parameters[3].Value = model.po_number == null ? DBNull.Value.ToString() : model.po_number;
            parameters[4].Value = model.po_type == null ? DBNull.Value.ToString() : model.po_type;
            parameters[5].Value = model.your_reference == null ? DBNull.Value.ToString() : model.your_reference;
            parameters[6].Value = model.desc == null ? DBNull.Value.ToString() : model.desc;
            parameters[7].Value = model.delivery_location == null ? DBNull.Value.ToString() : model.delivery_location;
            parameters[8].Value = model.currency == null ? DBNull.Value.ToString() : model.currency;
            parameters[9].Value = model.terms_of_delivery == null ? DBNull.Value.ToString() : model.terms_of_delivery;
            parameters[10].Value = model.POHeaderID;
            parameters[11].Value = model.terms_of_payment == null ? DBNull.Value.ToString() : model.terms_of_payment;
            parameters[12].Value = model.means_of_transport == null ? DBNull.Value.ToString() : model.means_of_transport;
            parameters[13].Value = model.supplier == null ? DBNull.Value.ToString() : model.supplier;
            parameters[14].Value = model.supplier_site == null ? DBNull.Value.ToString() : model.supplier_site;
            parameters[15].Value = model.supplier_address == null ? DBNull.Value.ToString() : model.supplier_address;
            parameters[16].Value = model.bonded == null ? DBNull.Value.ToString() : model.bonded;
            parameters[17].Value = model.delivery_address == null ? DBNull.Value.ToString() : model.delivery_address;

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
        public bool Delete(string POHeaderID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from POHeader ");
            strSql.Append(" where POHeaderID=@POHeaderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32)			};
            parameters[0].Value = POHeaderID;

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
        public bool DeleteList(string POHeaderIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from POHeader ");
            strSql.Append(" where POHeaderID in (" + POHeaderIDlist + ")  ");
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
        public com.portal.db.Model.POHeader GetModel(string POHeaderID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from POHeader ");
            strSql.Append(" where POHeaderID=@POHeaderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@POHeaderID", SqlDbType.VarChar,32)			};
            parameters[0].Value = POHeaderID;

            com.portal.db.Model.POHeader model = new com.portal.db.Model.POHeader();
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
        public com.portal.db.Model.POHeader DataRowToModel(DataRow row)
        {
            com.portal.db.Model.POHeader model = new com.portal.db.Model.POHeader();
            if (row != null)
            {
                if (row["POHeaderID"] != null)
                {
                    model.POHeaderID = row["POHeaderID"].ToString();
                }
                if (row["messageBodyID"] != null)
                {
                    model.messageBodyID = row["messageBodyID"].ToString();
                }
                if (row["buyer"] != null)
                {
                    model.buyer = row["buyer"].ToString();
                }
                if (row["po_date"] != null && row["po_date"].ToString() != "")
                {
                    model.po_date = row["po_date"].ToString();
                }
                if (row["po_number"] != null)
                {
                    model.po_number = row["po_number"].ToString();
                }
                if (row["po_type"] != null)
                {
                    model.po_type = row["po_type"].ToString();
                }
                if (row["your_reference"] != null)
                {
                    model.your_reference = row["your_reference"].ToString();
                }
                if (row["desc"] != null)
                {
                    model.desc = row["desc"].ToString();
                }
                if (row["delivery_location"] != null)
                {
                    model.delivery_location = row["delivery_location"].ToString();
                }
                if (row["currency"] != null)
                {
                    model.currency = row["currency"].ToString();
                }
                if (row["terms_of_delivery"] != null)
                {
                    model.terms_of_delivery = row["terms_of_delivery"].ToString();
                }
                if (row["means_of_transport"] != null)
                {
                    model.means_of_transport = row["means_of_transport"].ToString();
                }
                if (row["terms_of_payment"] != null)
                {
                    model.terms_of_payment = row["terms_of_payment"].ToString();
                }
                if (row["supplier"] != null)
                {
                    model.supplier = row["supplier"].ToString();
                }
                if (row["supplier_site"] != null)
                {
                    model.supplier_site = row["supplier_site"].ToString();
                }
                if (row["supplier_address"] != null)
                {
                    model.supplier_address = row["supplier_address"].ToString();
                }
                if (row["bonded"] != null)
                {
                    model.bonded = row["bonded"].ToString();
                }
                if (row["delivery_address"] != null)
                {
                    model.delivery_address = row["delivery_address"].ToString();
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
            strSql.Append(" FROM POHeader ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" *");
            strSql.Append(" FROM POHeader ");
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
            strSql.Append("select count(1) FROM POHeader ");
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
                strSql.Append("order by T.POHeaderID desc");
            }
            strSql.Append(")AS Row, T.*  from POHeader T ");
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
            parameters[0].Value = "POHeader";
            parameters[1].Value = "POHeaderID";
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

