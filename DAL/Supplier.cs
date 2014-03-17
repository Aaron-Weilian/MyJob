using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace com.portal.db.DAL
{
	/// <summary>
	/// 数据访问类:Supplier
	/// </summary>
	public partial class Supplier
	{
		public Supplier()
		{}
        
		#region  BasicMethod
        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());

        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SupplierID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Supplier");
			strSql.Append(" where SupplierID=@SupplierID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,32)			};
			parameters[0].Value = SupplierID;

			return dal.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(com.portal.db.Model.Supplier model)
		{
            string guid = System.Guid.NewGuid().ToString("N");

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Supplier(");
            strSql.Append("SupplierID,SupplierName,ContactName,CountryCode,SupplierAddress,SupplierEmail,SupplierPhone,Created,CreateBy,Updated,UpdateBy,DUNS,[Status],SupplierNUM)");
			strSql.Append(" values (");
            strSql.Append("@SupplierID,@SupplierName,@ContactName,@CountryCode,@SupplierAddress,@SupplierEmail,@SupplierPhone,@Created,@CreateBy,@Updated,@UpdateBy,@DUNS,@Status,@SupplierNUM)");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,32),
					new SqlParameter("@SupplierName", SqlDbType.VarChar,50),
					new SqlParameter("@ContactName", SqlDbType.VarChar,50),
					new SqlParameter("@CountryCode", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierAddress", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierEmail", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierPhone", SqlDbType.VarChar,50),
					new SqlParameter("@Created", SqlDbType.VarChar,20),
					new SqlParameter("@CreateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Updated", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
                    new SqlParameter("@DUNS", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                    new SqlParameter("@SupplierNUM", SqlDbType.VarChar,50)};
            parameters[0].Value = guid;
            parameters[1].Value = model.SupplierName == null ? DBNull.Value.ToString() : model.SupplierName;
            parameters[2].Value = model.ContactName == null ? DBNull.Value.ToString() : model.ContactName;
            parameters[3].Value = model.CountryCode == null ? DBNull.Value.ToString() : model.CountryCode;
            parameters[4].Value = model.SupplierAddress == null ? DBNull.Value.ToString() : model.SupplierAddress;
            parameters[5].Value = model.SupplierEmail == null ? DBNull.Value.ToString() : model.SupplierEmail;
            parameters[6].Value = model.SupplierPhone == null ? DBNull.Value.ToString() : model.SupplierPhone;
            parameters[7].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[8].Value = model.CreateBy == null ? DBNull.Value.ToString() : model.CreateBy;
            parameters[9].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[10].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[11].Value = model.DUNS == null ? DBNull.Value.ToString() : model.DUNS;
            parameters[12].Value = model.Status == null ? DBNull.Value.ToString() : model.Status;
            parameters[13].Value = model.SupplierNUM == null ? DBNull.Value.ToString() : model.SupplierNUM;

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
		public bool Update(com.portal.db.Model.Supplier model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Supplier set ");
			strSql.Append("SupplierName=@SupplierName,");
			strSql.Append("ContactName=@ContactName,");
			strSql.Append("CountryCode=@CountryCode,");
			strSql.Append("SupplierAddress=@SupplierAddress,");
			strSql.Append("SupplierEmail=@SupplierEmail,");
			strSql.Append("SupplierPhone=@SupplierPhone,");
			strSql.Append("Created=@Created,");
			strSql.Append("CreateBy=@CreateBy,");
			strSql.Append("Updated=@Updated,");
			strSql.Append("UpdateBy=@UpdateBy,");
            strSql.Append("DUNS=@DUNS,");
            strSql.Append("[Status]=@Status,");
            strSql.Append("SupplierNUM=@SupplierNUM");
			strSql.Append(" where SupplierID=@SupplierID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierName", SqlDbType.VarChar,50),
					new SqlParameter("@ContactName", SqlDbType.VarChar,50),
					new SqlParameter("@CountryCode", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierAddress", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierEmail", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierPhone", SqlDbType.VarChar,50),
					new SqlParameter("@Created", SqlDbType.VarChar,20),
					new SqlParameter("@CreateBy", SqlDbType.VarChar,50),
					new SqlParameter("@Updated", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@SupplierID", SqlDbType.VarChar,32),
                    new SqlParameter("@DUNS", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                    new SqlParameter("@SupplierNUM", SqlDbType.VarChar,50)};
            parameters[0].Value = model.SupplierName == null ? DBNull.Value.ToString() : model.SupplierName;
            parameters[1].Value = model.ContactName == null ? DBNull.Value.ToString() : model.ContactName;
            parameters[2].Value = model.CountryCode == null ? DBNull.Value.ToString() : model.CountryCode;
            parameters[3].Value = model.SupplierAddress == null ? DBNull.Value.ToString() : model.SupplierAddress;
            parameters[4].Value = model.SupplierEmail == null ? DBNull.Value.ToString() : model.SupplierEmail;
            parameters[5].Value = model.SupplierPhone == null ? DBNull.Value.ToString() : model.SupplierPhone;
            parameters[6].Value = model.Created == null ? DBNull.Value.ToString() : model.Created; ;
            parameters[7].Value = model.CreateBy == null ? DBNull.Value.ToString() : model.CreateBy;
            parameters[8].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated; ;
            parameters[9].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[10].Value = model.SupplierID == null ? DBNull.Value.ToString() : model.SupplierID;
            parameters[11].Value = model.DUNS == null ? DBNull.Value.ToString() : model.DUNS;
            parameters[12].Value = model.Status == null ? DBNull.Value.ToString() : model.Status;
            parameters[13].Value = model.SupplierNUM == null ? DBNull.Value.ToString() : model.SupplierNUM; 

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
		public bool Delete(string SupplierID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Supplier ");
			strSql.Append(" where SupplierID=@SupplierID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,32)			};
			parameters[0].Value = SupplierID;

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
		public bool DeleteList(string SupplierIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Supplier ");
			strSql.Append(" where SupplierID in ("+SupplierIDlist + ")  ");
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
		public com.portal.db.Model.Supplier GetModel(string SupplierID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from Supplier ");
			strSql.Append(" where SupplierID=@SupplierID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,32)			};
			parameters[0].Value = SupplierID;

			com.portal.db.Model.Supplier model=new com.portal.db.Model.Supplier();
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
		public com.portal.db.Model.Supplier DataRowToModel(DataRow row)
		{
			com.portal.db.Model.Supplier model=new com.portal.db.Model.Supplier();
			if (row != null)
			{
                
				if(row["SupplierID"]!=null)
				{
					model.SupplierID=row["SupplierID"].ToString();
				}
				if(row["SupplierName"]!=null)
				{
					model.SupplierName=row["SupplierName"].ToString();
				}
				if(row["ContactName"]!=null)
				{
					model.ContactName=row["ContactName"].ToString();
				}
				if(row["CountryCode"]!=null)
				{
					model.CountryCode=row["CountryCode"].ToString();
				}
				if(row["SupplierAddress"]!=null)
				{
					model.SupplierAddress=row["SupplierAddress"].ToString();
				}
				if(row["SupplierEmail"]!=null)
				{
					model.SupplierEmail=row["SupplierEmail"].ToString();
				}
				if(row["SupplierPhone"]!=null)
				{
					model.SupplierPhone=row["SupplierPhone"].ToString();
				}
				if(row["Created"]!=null && row["Created"].ToString()!="")
				{
					model.Created=row["Created"].ToString();
				}
				if(row["CreateBy"]!=null)
				{
					model.CreateBy=row["CreateBy"].ToString();
				}
				if(row["Updated"]!=null && row["Updated"].ToString()!="")
				{
					model.Updated=row["Updated"].ToString();
				}
				if(row["UpdateBy"]!=null)
				{
					model.UpdateBy=row["UpdateBy"].ToString();
				}
                if (row["DUNS"] != null && row["DUNS"].ToString() != "")
                {
                    model.DUNS = row["DUNS"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = row["Status"].ToString();
                }
                if (row["SupplierNUM"] != null && row["SupplierNUM"].ToString() != "")
                {
                    model.SupplierNUM = row["SupplierNUM"].ToString();
                }
			}
			return model;
		}

        public com.portal.db.Model.Supplier DataRowForSite(DataRow row)
        {
            com.portal.db.Model.Supplier model = new com.portal.db.Model.Supplier();
            if (row != null)
            {
                if (row["SiteName"] != null && row["SiteName"].ToString() != "")
                {
                    model.SiteName = row["SiteName"].ToString();
                }
                if (row["SiteNUM"] != null && row["SiteNUM"].ToString() != "")
                {
                    model.SiteNUM = row["SiteNUM"].ToString();
                }
                if (row["SupplierID"] != null)
                {
                    model.SupplierID = row["SupplierID"].ToString();
                }
                if (row["SupplierName"] != null)
                {
                    model.SupplierName = row["SupplierName"].ToString();
                }
                if (row["ContactName"] != null)
                {
                    model.ContactName = row["ContactName"].ToString();
                }
                if (row["CountryCode"] != null)
                {
                    model.CountryCode = row["CountryCode"].ToString();
                }
                if (row["SupplierAddress"] != null)
                {
                    model.SupplierAddress = row["SupplierAddress"].ToString();
                }
                if (row["SupplierEmail"] != null)
                {
                    model.SupplierEmail = row["SupplierEmail"].ToString();
                }
                if (row["SupplierPhone"] != null)
                {
                    model.SupplierPhone = row["SupplierPhone"].ToString();
                }
                if (row["Created"] != null && row["Created"].ToString() != "")
                {
                    model.Created = row["Created"].ToString();
                }
                if (row["CreateBy"] != null)
                {
                    model.CreateBy = row["CreateBy"].ToString();
                }
                if (row["Updated"] != null && row["Updated"].ToString() != "")
                {
                    model.Updated = row["Updated"].ToString();
                }
                if (row["UpdateBy"] != null)
                {
                    model.UpdateBy = row["UpdateBy"].ToString();
                }
                if (row["DUNS"] != null && row["DUNS"].ToString() != "")
                {
                    model.DUNS = row["DUNS"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = row["Status"].ToString();
                }
                if (row["SupplierNUM"] != null && row["SupplierNUM"].ToString() != "")
                {
                    model.SupplierNUM = row["SupplierNUM"].ToString();
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
            strSql.Append("select SupplierID,SupplierName,SupplierNUM,ContactName,CountryCode,SupplierAddress,SupplierEmail,SupplierPhone,Created,CreateBy,Updated,UpdateBy,DUNS,[Status] ");
			strSql.Append(" FROM Supplier ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
		}

        public DataSet GetSiteList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select site.SiteName,site.SiteNUM, supplier.SupplierID,SupplierName,SupplierNUM,ContactName,CountryCode,SupplierAddress,SupplierEmail,SupplierPhone,Created,CreateBy,Updated,UpdateBy,DUNS,[Status] ");
            strSql.Append("FROM Supplier as supplier ");
            strSql.Append("LEFT JOIN SITE AS SITE ");
            strSql.Append("ON supplier.SupplierID=site.SupplierID   ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return dal.ExecuteDataSet(CommandType.Text, strSql.ToString());
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
            strSql.Append(" SupplierID,SupplierName,ContactName,CountryCode,SupplierAddress,SupplierEmail,SupplierPhone,Created,CreateBy,Updated,UpdateBy,DUNS,[Status] ");
			strSql.Append(" FROM Supplier ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return dal.ExecuteDataSet(CommandType.Text,strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Supplier ");
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
				strSql.Append("order by T.SupplierID desc");
			}
			strSql.Append(")AS Row, T.*  from Supplier T ");
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
			parameters[0].Value = "Supplier";
			parameters[1].Value = "SupplierID";
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

