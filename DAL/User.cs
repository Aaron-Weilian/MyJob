
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace com.portal.db.DAL
{
	/// <summary>
	/// 数据访问类:User
	/// </summary>
	public partial class User
	{
		public User()
		{}
		#region  BasicMethod
        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());

        
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [User]");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,32)			};
			parameters[0].Value = UserID;

			return dal.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(com.portal.db.Model.User model)
		{
            string guid = System.Guid.NewGuid().ToString("N");

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [User](");
            strSql.Append("UserID,UserName,UserPassword,Supplier,Created,CeateBy,Updated,UpdateBy,[Status],UserType,Phone,Email,Discription,isFirstLogin,RoleNum,RoleName,SupplierNum)");
			strSql.Append(" values (");
            strSql.Append("@UserID,@UserName,@UserPassword,@Supplier,@Created,@CeateBy,@Updated,@UpdateBy,@Status,@UserType,@Phone,@Email,@Discription,@isFirstLogin,@RoleNum,@RoleName,@SupplierNum)");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,32),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserPassword", SqlDbType.VarChar,50),
					new SqlParameter("@Supplier", SqlDbType.VarChar,255),
					new SqlParameter("@Created", SqlDbType.VarChar,20),
					new SqlParameter("@CeateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Updated", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.VarChar,50),
					new SqlParameter("@UserType", SqlDbType.VarChar,32),
					new SqlParameter("@Phone", SqlDbType.VarChar,32),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
                    new SqlParameter("@Discription", SqlDbType.VarChar,255),
                    new SqlParameter("@isFirstLogin", SqlDbType.VarChar,20),
                    new SqlParameter("@RoleNum", SqlDbType.VarChar,20),
                    new SqlParameter("@RoleName", SqlDbType.VarChar,50),
                    new SqlParameter("@SupplierNum", SqlDbType.VarChar,50)                    };
            parameters[0].Value = guid;
            parameters[1].Value = model.UserName == null ? DBNull.Value.ToString() : model.UserName;
            parameters[2].Value = model.UserPassword == null ? DBNull.Value.ToString() : model.UserPassword;
            parameters[3].Value = model.Supplier == null ? DBNull.Value.ToString() : model.Supplier;
            parameters[4].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[5].Value = model.CeateBy == null ? DBNull.Value.ToString() : model.CeateBy;
            parameters[6].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[7].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[8].Value = model.Status == null ? DBNull.Value.ToString() : model.Status;
            parameters[9].Value = model.UserType == null ? DBNull.Value.ToString() : model.UserType;
            parameters[10].Value = model.Phone == null ? DBNull.Value.ToString() : model.Phone;
            parameters[11].Value = model.Email == null ? DBNull.Value.ToString() : model.Email;
            parameters[12].Value = model.Discription == null ? DBNull.Value.ToString() : model.Discription;
            parameters[13].Value = model.isFirstLogin == null ? DBNull.Value.ToString() : model.isFirstLogin;
            parameters[14].Value = model.Role == null ? DBNull.Value.ToString() : model.Role;
            parameters[15].Value = model.RoleName == null ? DBNull.Value.ToString() : model.RoleName;
            parameters[16].Value = model.SupplierNum == null ? DBNull.Value.ToString() : model.SupplierNum;

			int rows=dal.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
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
		public bool Update(com.portal.db.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [User] set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserPassword=@UserPassword,");
			strSql.Append("Supplier=@Supplier,");
			strSql.Append("Created=@Created,");
			strSql.Append("CeateBy=@CeateBy,");
			strSql.Append("Updated=@Updated,");
			strSql.Append("UpdateBy=@UpdateBy,");
			strSql.Append("Status=@Status,");
			strSql.Append("UserType=@UserType,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("Email=@Email,");
            strSql.Append("Discription=@Discription,");
            strSql.Append("isFirstLogin=@isFirstLogin,");
            strSql.Append("RoleNum=@RoleNum,");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("SupplierNum=@SupplierNum");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserPassword", SqlDbType.VarChar,50),
					new SqlParameter("@Supplier", SqlDbType.VarChar,255),
					new SqlParameter("@Created", SqlDbType.VarChar,20),
					new SqlParameter("@CeateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Updated", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.VarChar,50),
					new SqlParameter("@UserType", SqlDbType.VarChar,32),
					new SqlParameter("@Phone", SqlDbType.VarChar,32),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@UserID", SqlDbType.VarChar,32),
                    new SqlParameter("@Discription", SqlDbType.VarChar,255),
                    new SqlParameter("@isFirstLogin", SqlDbType.VarChar,20),
                    new SqlParameter("@RoleNum", SqlDbType.VarChar,20),
                    new SqlParameter("@RoleName", SqlDbType.VarChar,50),
                    new SqlParameter("@SupplierNum", SqlDbType.VarChar,50)                    };
            parameters[0].Value = model.UserName == null ? DBNull.Value.ToString() : model.UserName;
            parameters[1].Value = model.UserPassword == null ? DBNull.Value.ToString() : model.UserPassword;
            parameters[2].Value = model.Supplier == null ? DBNull.Value.ToString() : model.Supplier;
            parameters[3].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[4].Value = model.CeateBy == null ? DBNull.Value.ToString() : model.CeateBy;
            parameters[5].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[6].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[7].Value = model.Status == null ? DBNull.Value.ToString() : model.Status;
            parameters[8].Value = model.UserType == null ? DBNull.Value.ToString() : model.UserType;
            parameters[9].Value = model.Phone == null ? DBNull.Value.ToString() : model.Phone;
            parameters[10].Value = model.Email == null ? DBNull.Value.ToString() : model.Email;
            parameters[11].Value = model.UserID == null ? DBNull.Value.ToString() : model.UserID;
            parameters[12].Value = model.Discription == null ? DBNull.Value.ToString() : model.Discription;
            parameters[13].Value = model.isFirstLogin == null ? DBNull.Value.ToString() : model.isFirstLogin;
            parameters[14].Value = model.Role == null ? DBNull.Value.ToString() : model.Role;
            parameters[15].Value = model.RoleName == null ? DBNull.Value.ToString() : model.RoleName;
            parameters[16].Value = model.SupplierNum == null ? DBNull.Value.ToString() : model.SupplierNum;

			int rows=dal.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
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
		public bool Delete(string UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from [User] ");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,32)			};
			parameters[0].Value = UserID;

			int rows=dal.ExecuteNonQuery(CommandType.Text,strSql.ToString(),parameters);
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
		public bool DeleteList(string UserIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from [User] ");
			strSql.Append(" where UserID in ("+UserIDlist + ")  ");
			int rows=dal.ExecuteNonQuery(CommandType.Text,strSql.ToString());
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
		public com.portal.db.Model.User GetModel(string UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 UserID,UserName,UserPassword,Supplier,SupplierNum,Created,CeateBy,Updated,UpdateBy,[Status],UserType,Phone,Email,Discription,isFirstLogin,RoleNum,RoleName from [User] ");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,32)			};
			parameters[0].Value = UserID;

			com.portal.db.Model.User model=new com.portal.db.Model.User();
			DataSet ds=dal.ExecuteDataSet(CommandType.Text,strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public com.portal.db.Model.User DataRowToModel(DataRow row)
		{
			com.portal.db.Model.User model=new com.portal.db.Model.User();
			if (row != null)
			{
				if(row["UserID"]!=null)
				{
					model.UserID=row["UserID"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["UserPassword"]!=null)
				{
					model.UserPassword=row["UserPassword"].ToString();
				}
				if(row["Supplier"]!=null)
				{
					model.Supplier=row["Supplier"].ToString();
				}
				if(row["Created"]!=null && row["Created"].ToString()!="")
				{
					model.Created=row["Created"].ToString();
				}
				if(row["CeateBy"]!=null)
				{
					model.CeateBy=row["CeateBy"].ToString();
				}
				if(row["Updated"]!=null && row["Updated"].ToString()!="")
				{
					model.Updated=row["Updated"].ToString();
				}
				if(row["UpdateBy"]!=null)
				{
					model.UpdateBy=row["UpdateBy"].ToString();
				}
				if(row["Status"]!=null)
				{
					model.Status=row["Status"].ToString();
				}
				if(row["UserType"]!=null)
				{
					model.UserType=row["UserType"].ToString();
				}
				if(row["Phone"]!=null)
				{
					model.Phone=row["Phone"].ToString();
				}
				if(row["Email"]!=null)
				{
					model.Email=row["Email"].ToString();
				}
                if (row["Discription"] != null)
                {
                    model.Discription = row["Discription"].ToString();
                }
                if (row["isFirstLogin"] != null)
                {
                    model.isFirstLogin = row["isFirstLogin"].ToString();
                }
                if (row["RoleNum"] != null)
                {
                    model.Role = row["RoleNum"].ToString();
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                if (row["SupplierNum"] != null) {
                    model.SupplierNum = row["SupplierNum"].ToString();
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select UserID,UserName,UserPassword,Supplier,SupplierNum,Created,CeateBy,Updated,UpdateBy,[Status],UserType,Phone,Email,Discription,isFirstLogin,RoleNum,RoleName ");
			strSql.Append(" FROM [User] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" UserID,UserName,UserPassword,Supplier,SupplierNum,Created,CeateBy,Updated,UpdateBy,[Status],UserType,Phone,Email,Discription,isFirstLogin,RoleNum,RoleName ");
			strSql.Append(" FROM [User] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
		}

        public com.portal.db.Model.User Login(string username , string password) {

            string sql = "UserName = '" + username + "' and UserPassword = '" + password + "'";
            DataSet ds = GetList(sql);
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
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM [User] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.UserID desc");
			}
			strSql.Append(")AS Row, T.*  from User T ");
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
			parameters[0].Value = "User";
			parameters[1].Value = "UserID";
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

