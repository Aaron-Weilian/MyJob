
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;  
namespace com.portal.db.DAL
{
    /// <summary>
    /// 数据访问类:Role
    /// </summary>
    public partial class Role
    {
        public Role()
        { }
        #region  BasicMethod
        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Role");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,32)			};
            parameters[0].Value = RoleID;

            return dal.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Role(");
            strSql.Append("RoleID,RoleName,RoleNUM,Created,CreateBy,Updated,UpdateBy,Discription)");
            strSql.Append(" values (");
            strSql.Append("@RoleID,@RoleName,@RoleNUM,@Created,@CreateBy,@Updated,@UpdateBy,@Discription)");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,32),
					new SqlParameter("@RoleName", SqlDbType.VarChar,50),
					new SqlParameter("@RoleNUM", SqlDbType.VarChar,50),
					new SqlParameter("@Created", SqlDbType.VarChar,20),
					new SqlParameter("@CreateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Updated", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Discription", SqlDbType.VarChar,255)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.RoleNUM;
            parameters[3].Value = model.Created;
            parameters[4].Value = model.CreateBy;
            parameters[5].Value = model.Updated;
            parameters[6].Value = model.UpdateBy;
            parameters[7].Value = model.Discription;

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
        public bool Update(com.portal.db.Model.Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Role set ");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("RoleNUM=@RoleNUM,");
            strSql.Append("Created=@Created,");
            strSql.Append("CreateBy=@CreateBy,");
            strSql.Append("Updated=@Updated,");
            strSql.Append("UpdateBy=@UpdateBy,");
            strSql.Append("Discription=@Discription");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.VarChar,50),
					new SqlParameter("@RoleNUM", SqlDbType.VarChar,50),
					new SqlParameter("@Created", SqlDbType.VarChar,20),
					new SqlParameter("@CreateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Updated", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Discription", SqlDbType.VarChar,255),
					new SqlParameter("@RoleID", SqlDbType.VarChar,32)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.RoleNUM;
            parameters[2].Value = model.Created;
            parameters[3].Value = model.CreateBy;
            parameters[4].Value = model.Updated;
            parameters[5].Value = model.UpdateBy;
            parameters[6].Value = model.Discription;
            parameters[7].Value = model.RoleID;

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
        public bool Delete(string RoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Role ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,32)			};
            parameters[0].Value = RoleID;

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
        public bool DeleteList(string RoleIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Role ");
            strSql.Append(" where RoleID in (" + RoleIDlist + ")  ");
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
        public com.portal.db.Model.Role GetModel(string RoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RoleID,RoleName,RoleNUM,Created,CreateBy,Updated,UpdateBy,Discription from Role ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,32)			};
            parameters[0].Value = RoleID;

            com.portal.db.Model.Role model = new com.portal.db.Model.Role();
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
        public com.portal.db.Model.Role DataRowToModel(DataRow row)
        {
            com.portal.db.Model.Role model = new com.portal.db.Model.Role();
            if (row != null)
            {
                if (row["RoleID"] != null)
                {
                    model.RoleID = row["RoleID"].ToString();
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                if (row["RoleNUM"] != null)
                {
                    model.RoleNUM = row["RoleNUM"].ToString();
                }
                
                if (row["Created"] != null)
                {
                    model.Created = row["Created"].ToString();
                }
                if (row["CreateBy"] != null)
                {
                    model.CreateBy = row["CreateBy"].ToString();
                }
                if (row["Updated"] != null)
                {
                    model.Updated = row["Updated"].ToString();
                }
                if (row["UpdateBy"] != null)
                {
                    model.UpdateBy = row["UpdateBy"].ToString();
                }
                if (row["Discription"] != null)
                {
                    model.Discription = row["Discription"].ToString();
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
            strSql.Append("select RoleID,RoleName,RoleNUM,Created,CreateBy,Updated,UpdateBy,Discription ");
            strSql.Append(" FROM Role ");
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
            strSql.Append(" RoleID,RoleName,RoleNUM,Created,CreateBy,Updated,UpdateBy,Discription ");
            strSql.Append(" FROM Role ");
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
            strSql.Append("select count(1) FROM Role ");
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
                strSql.Append("order by T.RoleID desc");
            }
            strSql.Append(")AS Row, T.*  from Role T ");
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
            parameters[0].Value = "Role";
            parameters[1].Value = "RoleID";
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

