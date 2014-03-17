using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace com.portal.db.DAL
{
    /// <summary>
    /// 数据访问类:Site
    /// </summary>
    public partial class Site
    {
        public Site()
        { }
        #region  BasicMethod
        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());
        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());

        
        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SiteID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Site");
            strSql.Append(" where SiteID=@SiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SiteID", SqlDbType.VarChar ,32)			};
            parameters[0].Value = SiteID;

            return dal.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.Site model)
        {
            string guid = System.Guid.NewGuid().ToString("N");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Site(");
            strSql.Append("SiteID,SupplierID,SiteName,SiteNUM)");
            strSql.Append(" values (");
            strSql.Append("@SiteID,@SupplierID,@SiteName,@SiteNUM)");
            SqlParameter[] parameters = {
					new SqlParameter("@SiteID", SqlDbType.VarChar ,32),
					new SqlParameter("@SupplierID", SqlDbType.VarChar ,32),
					new SqlParameter("@SiteName", SqlDbType.VarChar ,50),
                    new SqlParameter("@SiteNUM", SqlDbType.VarChar ,50)};
            parameters[0].Value = guid;
            parameters[1].Value = model.SupplierID;
            parameters[2].Value = model.SiteName;
            parameters[3].Value = model.SiteNUM;

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
        public bool Update(com.portal.db.Model.Site model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Site set ");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("SiteName=@SiteName,");
            strSql.Append("SiteNUM=@SiteNUM");
            strSql.Append(" where SiteID=@SiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar ,32),
					new SqlParameter("@SiteName", SqlDbType.VarChar ,50),
					new SqlParameter("@SiteID", SqlDbType.VarChar ,32),
                    new SqlParameter("@SiteNUM", SqlDbType.VarChar ,50)};
            parameters[0].Value = model.SupplierID;
            parameters[1].Value = model.SiteName;
            parameters[2].Value = model.SiteID;
            parameters[3].Value = model.SiteNUM;

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
        public bool Delete(string SiteID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Site ");
            strSql.Append(" where SiteID=@SiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SiteID", SqlDbType.VarChar,32)			};
            parameters[0].Value = SiteID;

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


        public bool DeleteBySupplierID(string SupplierID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Site ");
            strSql.Append(" where SupplierID=@SupplierID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,32)			};
            parameters[0].Value = SupplierID;

            int rows = dal.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
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
        public bool DeleteList(string SiteIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Site ");
            strSql.Append(" where SiteID in (" + SiteIDlist + ")  ");
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
        public com.portal.db.Model.Site GetModel(string SiteID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SiteID,SupplierID,SiteName,SiteNUM from Site ");
            strSql.Append(" where SiteID=@SiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SiteID", SqlDbType.VarChar ,32)			};
            parameters[0].Value = SiteID;

            com.portal.db.Model.Site model = new com.portal.db.Model.Site();
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
        public com.portal.db.Model.Site DataRowToModel(DataRow row)
        {
            com.portal.db.Model.Site model = new com.portal.db.Model.Site();
            if (row != null)
            {
                if (row["SiteID"] != null)
                {
                    model.SiteID = row["SiteID"].ToString();
                }
                if (row["SupplierID"] != null)
                {
                    model.SupplierID = row["SupplierID"].ToString();
                }
                if (row["SiteName"] != null)
                {
                    model.SiteName = row["SiteName"].ToString();
                }
                if (row["SiteNUM"] != null)
                {
                    model.SiteNUM = row["SiteNUM"].ToString();
                }
                if (row["SiteName/SiteNUM"] != null)
                {
                    model.NameNUM = row["SiteName/SiteNUM"].ToString();
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
            strSql.Append("select SiteID,SupplierID,SiteName,SiteNUM ,SiteName+'/'+SiteNUM as 'SiteName/SiteNUM'");
            strSql.Append(" FROM Site ");
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
            strSql.Append(" SiteID,SupplierID,SiteName,SiteNUM ");
            strSql.Append(" FROM Site ");
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
            strSql.Append("select count(1) FROM Site ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = dal.ExecuteScalar(CommandType.Text, strSql.ToString());
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
                strSql.Append("order by T.SiteID desc");
            }
            strSql.Append(")AS Row, T.*  from Site T ");
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
            parameters[0].Value = "Site";
            parameters[1].Value = "SiteID";
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

