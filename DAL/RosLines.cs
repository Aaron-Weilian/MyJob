
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace com.portal.db.DAL
{
	/// <summary>
	/// 数据访问类:RosLines
	/// </summary>
	public partial class RosLines
	{
		public RosLines()
		{}
		#region  BasicMethod

        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());
        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RosLineID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RosLines");
			strSql.Append(" where RosLineID=@RosLineID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosLineID", SqlDbType.VarChar,32)			};
			parameters[0].Value = RosLineID;

			return dal.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(com.portal.db.Model.RosLines model)
		{
            string guid = System.Guid.NewGuid().ToString("N");

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RosLines(");
            strSql.Append("RosLineID,RosDemandDate,RosDemandQuantity,RosEtaQty,RosEtdQty,RosDio,RosShortageQty,RosHeaderID,RosCumEta,Created,CreateBy,Updated,UpdateBy,PO_NO)");
			strSql.Append(" values (");
            strSql.Append("@RosLineID,@RosDemandDate,@RosDemandQuantity,@RosEtaQty,@RosEtdQty,@RosDio,@RosShortageQty,@RosHeaderID,@RosCumEta,@Created,@CreateBy,@Updated,@UpdateBy,@PO_NO)");
			SqlParameter[] parameters = {
					new SqlParameter("@RosLineID", SqlDbType.VarChar,32),
					new SqlParameter("@RosDemandDate", SqlDbType.VarChar,20),
					new SqlParameter("@RosDemandQuantity", SqlDbType.VarChar,50),
					new SqlParameter("@RosEtaQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosEtdQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosDio", SqlDbType.VarChar,50),
					new SqlParameter("@RosShortageQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32),
                    new SqlParameter("@RosCumEta", SqlDbType.VarChar,50),
                    new SqlParameter("@Created", SqlDbType.VarChar,20),                    
                    new SqlParameter("@CreateBy", SqlDbType.VarChar,100),                    
                    new SqlParameter("@Updated", SqlDbType.VarChar,20),
                    new SqlParameter("@UpdateBy", SqlDbType.VarChar,100),
                    new SqlParameter("@PO_NO", SqlDbType.VarChar,100)     
                                        };
            parameters[0].Value = guid;
            parameters[1].Value = model.RosDemandDate == null ? DBNull.Value.ToString() : model.RosDemandDate;
            parameters[2].Value = model.RosDemandQuantity == null ? DBNull.Value.ToString() : model.RosDemandQuantity;
            parameters[3].Value = model.RosEtaQty == null ? DBNull.Value.ToString() : model.RosEtaQty;
            parameters[4].Value = model.RosEtdQty == null ? DBNull.Value.ToString() : model.RosEtdQty;
            parameters[5].Value = model.RosDio == null ? DBNull.Value.ToString() : model.RosDio;
            parameters[6].Value = model.RosShortageQty == null ? DBNull.Value.ToString() : model.RosShortageQty;
            parameters[7].Value = model.RosHeaderID == null ? DBNull.Value.ToString() : model.RosHeaderID;
            parameters[8].Value = model.RosCumEta == null ? DBNull.Value.ToString() : model.RosCumEta;
            parameters[9].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[10].Value = model.CreateBy == null ? DBNull.Value.ToString() : model.CreateBy;
            parameters[11].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[12].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[13].Value = model.PO_NO == null ? DBNull.Value.ToString() : model.PO_NO;

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
		public bool Update(com.portal.db.Model.RosLines model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RosLines set ");
			strSql.Append("RosDemandDate=@RosDemandDate,");
			strSql.Append("RosDemandQuantity=@RosDemandQuantity,");
			strSql.Append("RosEtaQty=@RosEtaQty,");
			strSql.Append("RosEtdQty=@RosEtdQty,");
			strSql.Append("RosDio=@RosDio,");
			strSql.Append("RosShortageQty=@RosShortageQty,");
			strSql.Append("RosHeaderID=@RosHeaderID,");
            strSql.Append("RosCumEta=@RosCumEta,");
            strSql.Append("Created=@Created,");
            strSql.Append("CreateBy=@CreateBy,");
            strSql.Append("Updated=@Updated,");
            strSql.Append("UpdateBy=@UpdateBy,");
            strSql.Append("PO_NO=@PO_NO");
			strSql.Append(" where RosLineID=@RosLineID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosDemandDate", SqlDbType.VarChar,20),
					new SqlParameter("@RosDemandQuantity", SqlDbType.VarChar,50),
					new SqlParameter("@RosEtaQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosEtdQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosDio", SqlDbType.VarChar,50),
					new SqlParameter("@RosShortageQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32),
                    new SqlParameter("@RosCumEta", SqlDbType.VarChar,50),
                    new SqlParameter("@Created", SqlDbType.VarChar,50),                    
                    new SqlParameter("@CreateBy", SqlDbType.VarChar,50),                    
                    new SqlParameter("@Updated", SqlDbType.VarChar,50),
                    new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
                    new SqlParameter("@PO_NO", SqlDbType.VarChar,50),
                    new SqlParameter("@RosLineID", SqlDbType.VarChar,32)
                                        };
            parameters[0].Value = model.RosDemandDate == null ? DBNull.Value.ToString() : model.RosDemandDate;
            parameters[1].Value = model.RosDemandQuantity == null ? DBNull.Value.ToString() : model.RosDemandQuantity;
            parameters[2].Value = model.RosEtaQty == null ? DBNull.Value.ToString() : model.RosEtaQty;
            parameters[3].Value = model.RosEtdQty == null ? DBNull.Value.ToString() : model.RosEtdQty;
            parameters[4].Value = model.RosDio == null ? DBNull.Value.ToString() : model.RosDio;
            parameters[5].Value = model.RosShortageQty == null ? DBNull.Value.ToString() : model.RosShortageQty;
            parameters[6].Value = model.RosHeaderID == null ? DBNull.Value.ToString() : model.RosHeaderID;
            parameters[7].Value = model.RosCumEta == null ? DBNull.Value.ToString() : model.RosCumEta;
            parameters[8].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[9].Value = model.CreateBy == null ? DBNull.Value.ToString() : model.CreateBy;
            parameters[10].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[11].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[12].Value = model.PO_NO == null ? DBNull.Value.ToString() : model.PO_NO;
            parameters[13].Value = model.RosLineID == null ? DBNull.Value.ToString() : model.RosLineID;
            
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
		public bool Delete(string RosLineID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RosLines ");
			strSql.Append(" where RosLineID=@RosLineID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosLineID", SqlDbType.VarChar,32)			};
			parameters[0].Value = RosLineID;

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
		public bool DeleteList(string RosLineIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RosLines ");
			strSql.Append(" where RosLineID in ("+RosLineIDlist + ")  ");
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
		public com.portal.db.Model.RosLines GetModel(string RosLineID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from RosLines ");
			strSql.Append(" where RosLineID=@RosLineID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosLineID", SqlDbType.VarChar,32)			};
			parameters[0].Value = RosLineID;

			com.portal.db.Model.RosLines model=new com.portal.db.Model.RosLines();
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
		public com.portal.db.Model.RosLines DataRowToModel(DataRow row)
		{
			com.portal.db.Model.RosLines model=new com.portal.db.Model.RosLines();
			if (row != null)
			{
				if(row["RosLineID"]!=null)
				{
					model.RosLineID=row["RosLineID"].ToString();
				}
				if(row["RosDemandDate"]!=null && row["RosDemandDate"].ToString()!="")
				{
					model.RosDemandDate=row["RosDemandDate"].ToString();
				}
				if(row["RosDemandQuantity"]!=null)
				{
					model.RosDemandQuantity=row["RosDemandQuantity"].ToString();
				}
				if(row["RosEtaQty"]!=null)
				{
					model.RosEtaQty=row["RosEtaQty"].ToString();
				}
				if(row["RosEtdQty"]!=null)
				{
					model.RosEtdQty=row["RosEtdQty"].ToString();
				}
				if(row["RosDio"]!=null)
				{
					model.RosDio=row["RosDio"].ToString();
				}
				if(row["RosShortageQty"]!=null)
				{
					model.RosShortageQty=row["RosShortageQty"].ToString();
				}
				if(row["RosHeaderID"]!=null)
				{
					model.RosHeaderID=row["RosHeaderID"].ToString();
				}
                if (row["RosCumEta"] != null)
				{
                    model.RosCumEta = row["RosCumEta"].ToString();
				}
                if (row["PO_NO"] != null)
                {
                    model.PO_NO = row["PO_NO"].ToString();
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
            strSql.Append("select * ");
			strSql.Append(" FROM RosLines ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" ORDER BY RosDemandDate");
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
            strSql.Append(" * ");
			strSql.Append(" FROM RosLines ");
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
			strSql.Append("select count(1) FROM RosLines ");
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
				strSql.Append("order by T.RosLineID desc");
			}
			strSql.Append(")AS Row, T.*  from RosLines T ");
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
			parameters[0].Value = "RosLines";
			parameters[1].Value = "RosLineID";
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

