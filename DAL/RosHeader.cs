
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace com.portal.db.DAL
{
	/// <summary>
	/// 数据访问类:RosHeader
	/// </summary>
	public partial class RosHeader
	{
		public RosHeader()
		{}
		#region  BasicMethod

        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        //private readonly DBCommon.SQLiteDBAccess dal = new DBCommon.SQLiteDBAccess(GetAppSetting.GetConnSetting());


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RosHeaderID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RosHeader");
			strSql.Append(" where RosHeaderID=@RosHeaderID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32)			};
			parameters[0].Value = RosHeaderID;

			return dal.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(com.portal.db.Model.RosHeader model)
		{

            string guid = System.Guid.NewGuid().ToString("N");

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RosHeader(");
            strSql.Append("RosHeaderID,RosItemNO,RosDesc,RosModel,RosCategory,RosBuyer,RosAllocationPercent,RosLeadTime,RosStockQty,RosSafeStock,PoNumber,OpenPoQty,messageBodyID,GlodPlanFlag,VmiStock,UpdateFlag,Created,CreateBy,Updated,UpdateBy)");
			strSql.Append(" values (");
            strSql.Append("@RosHeaderID,@RosItemNO,@RosDesc,@RosModel,@RosCategory,@RosBuyer,@RosAllocationPercent,@RosLeadTime,@RosStockQty,@RosSafeStock,@PoNumber,@OpenPoQty,@messageBodyID,@GlodPlanFlag,@VmiStock,@UpdateFlag,@Created,@CreateBy,@Updated,@UpdateBy)");
			SqlParameter[] parameters = {
					new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32),
					new SqlParameter("@RosItemNO", SqlDbType.VarChar,50),
					new SqlParameter("@RosDesc", SqlDbType.VarChar,50),
					new SqlParameter("@RosModel", SqlDbType.VarChar,50),
					new SqlParameter("@RosCategory", SqlDbType.VarChar,50),
					new SqlParameter("@RosBuyer", SqlDbType.VarChar,50),
					new SqlParameter("@RosAllocationPercent", SqlDbType.VarChar,50),
					new SqlParameter("@RosLeadTime", SqlDbType.VarChar,50),
					new SqlParameter("@RosStockQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosSafeStock", SqlDbType.VarChar,50),
					new SqlParameter("@PoNumber", SqlDbType.VarChar,50),
					new SqlParameter("@OpenPoQty", SqlDbType.VarChar,50),
					new SqlParameter("@messageBodyID", SqlDbType.VarChar,32),
                    new SqlParameter("@GlodPlanFlag", SqlDbType.VarChar,10) ,
                    new SqlParameter("@VmiStock", SqlDbType.VarChar,10) ,
                    new SqlParameter("@UpdateFlag", SqlDbType.VarChar,10),
                    new SqlParameter("@Created", SqlDbType.VarChar,20),
                    new SqlParameter("@CreateBy", SqlDbType.VarChar,100),
                    new SqlParameter("@Updated", SqlDbType.VarChar,20),
                    new SqlParameter("@UpdateBy", SqlDbType.VarChar,100) 
                                        };
            parameters[0].Value = guid;
            parameters[1].Value = model.RosItemNO == null ? DBNull.Value.ToString() : model.RosItemNO;
            parameters[2].Value = model.RosDesc == null ? DBNull.Value.ToString() : model.RosDesc;
            parameters[3].Value = model.RosModel == null ? DBNull.Value.ToString() : model.RosModel;
            parameters[4].Value = model.RosCategory == null ? DBNull.Value.ToString() : model.RosCategory;
            parameters[5].Value = model.RosBuyer == null ? DBNull.Value.ToString() : model.RosBuyer;
            parameters[6].Value = model.RosAllocationPercent == null ? DBNull.Value.ToString() : model.RosAllocationPercent;
            parameters[7].Value = model.RosLeadTime == null ? DBNull.Value.ToString() : model.RosLeadTime;
            parameters[8].Value = model.RosStockQty == null ? DBNull.Value.ToString() : model.RosStockQty;
            parameters[9].Value = model.RosSafeStock == null ? DBNull.Value.ToString() : model.RosSafeStock;
            parameters[10].Value = model.PoNumber == null ? DBNull.Value.ToString() : model.PoNumber;
            parameters[11].Value = model.OpenPoQty == null ? DBNull.Value.ToString() : model.OpenPoQty;
            parameters[12].Value = model.MessageBodyID == null ? DBNull.Value.ToString() : model.MessageBodyID;
            parameters[13].Value = model.Glod_plan_flag == null ? DBNull.Value.ToString() : model.Glod_plan_flag;
            parameters[14].Value = model.VmiStock == null ? DBNull.Value.ToString() : model.VmiStock;
            parameters[15].Value = model.UpdateFlag == null ? DBNull.Value.ToString() : model.UpdateFlag;
            parameters[16].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[17].Value = model.CreateBy == null ? DBNull.Value.ToString() : model.CreateBy;
            parameters[18].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[19].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;

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
		public bool Update(com.portal.db.Model.RosHeader model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RosHeader set ");
			strSql.Append("RosItemNO=@RosItemNO,");
			strSql.Append("RosDesc=@RosDesc,");
			strSql.Append("RosModel=@RosModel,");
			strSql.Append("RosCategory=@RosCategory,");
			strSql.Append("RosBuyer=@RosBuyer,");
			strSql.Append("RosAllocationPercent=@RosAllocationPercent,");
			strSql.Append("RosLeadTime=@RosLeadTime,");
			strSql.Append("RosStockQty=@RosStockQty,");
			strSql.Append("RosSafeStock=@RosSafeStock,");
			strSql.Append("PoNumber=@PoNumber,");
			strSql.Append("OpenPoQty=@OpenPoQty,");
            strSql.Append("messageBodyID=@messageBodyID,");
            strSql.Append("GlodPlanFlag=@GlodPlanFlag,");
            strSql.Append("VmiStock=@VmiStock,");
            strSql.Append("UpdateFlag=@UpdateFlag,");
            strSql.Append("Created=@Created,");
            strSql.Append("CreateBy=@CreateBy,");
            strSql.Append("Updated=@Updated,");
            strSql.Append("UpdateBy=@UpdateBy");
			strSql.Append(" where RosHeaderID=@RosHeaderID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosItemNO", SqlDbType.VarChar,50),
					new SqlParameter("@RosDesc", SqlDbType.VarChar,50),
					new SqlParameter("@RosModel", SqlDbType.VarChar,50),
					new SqlParameter("@RosCategory", SqlDbType.VarChar,50),
					new SqlParameter("@RosBuyer", SqlDbType.VarChar,50),
					new SqlParameter("@RosAllocationPercent", SqlDbType.VarChar,50),
					new SqlParameter("@RosLeadTime", SqlDbType.VarChar,50),
					new SqlParameter("@RosStockQty", SqlDbType.VarChar,50),
					new SqlParameter("@RosSafeStock", SqlDbType.VarChar,50),
					new SqlParameter("@PoNumber", SqlDbType.VarChar,50),
					new SqlParameter("@OpenPoQty", SqlDbType.VarChar,50),
					new SqlParameter("@messageBodyID", SqlDbType.VarChar,32),
                    new SqlParameter("@GlodPlanFlag", SqlDbType.VarChar,10), 
                    new SqlParameter("@VmiStock", SqlDbType.VarChar,10),
                    new SqlParameter("@UpdateFlag", SqlDbType.VarChar,10),
                    new SqlParameter("@Created", SqlDbType.VarChar,10),
                    new SqlParameter("@CreateBy", SqlDbType.VarChar,10),
                    new SqlParameter("@Updated", SqlDbType.VarChar,10),
                    new SqlParameter("@Updateby", SqlDbType.VarChar,10),
                    new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32)
                                        };
            parameters[0].Value = model.RosItemNO == null ? DBNull.Value.ToString() : model.RosItemNO;
            parameters[1].Value = model.RosDesc == null ? DBNull.Value.ToString() : model.RosDesc;
            parameters[2].Value = model.RosModel == null ? DBNull.Value.ToString() : model.RosModel;
            parameters[3].Value = model.RosCategory == null ? DBNull.Value.ToString() : model.RosCategory;
            parameters[4].Value = model.RosBuyer == null ? DBNull.Value.ToString() : model.RosBuyer;
            parameters[5].Value = model.RosAllocationPercent == null ? DBNull.Value.ToString() : model.RosAllocationPercent;
            parameters[6].Value = model.RosLeadTime == null ? DBNull.Value.ToString() : model.RosLeadTime;
            parameters[7].Value = model.RosStockQty == null ? DBNull.Value.ToString() : model.RosStockQty;
            parameters[8].Value = model.RosSafeStock == null ? DBNull.Value.ToString() : model.RosSafeStock;
            parameters[9].Value = model.PoNumber == null ? DBNull.Value.ToString() : model.PoNumber;
            parameters[10].Value = model.OpenPoQty == null ? DBNull.Value.ToString() : model.OpenPoQty;
            parameters[11].Value = model.MessageBodyID == null ? DBNull.Value.ToString() : model.MessageBodyID;
            parameters[12].Value = model.Glod_plan_flag == null ? DBNull.Value.ToString() : model.Glod_plan_flag;
            parameters[13].Value = model.VmiStock == null ? DBNull.Value.ToString() : model.VmiStock;
            parameters[14].Value = model.UpdateFlag == null ? "N" : model.UpdateFlag;
            parameters[15].Value = model.Created == null ? DBNull.Value.ToString() : model.Created;
            parameters[16].Value = model.CreateBy == null ? DBNull.Value.ToString() : model.CreateBy;
            parameters[17].Value = model.Updated == null ? DBNull.Value.ToString() : model.Updated;
            parameters[18].Value = model.UpdateBy == null ? DBNull.Value.ToString() : model.UpdateBy;
            parameters[19].Value = model.RosHeaderID == null ? DBNull.Value.ToString() : model.RosHeaderID;
            
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
		public bool Delete(string RosHeaderID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RosHeader ");
			strSql.Append(" where RosHeaderID=@RosHeaderID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32)			};
			parameters[0].Value = RosHeaderID;

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
		public bool DeleteList(string RosHeaderIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RosHeader ");
			strSql.Append(" where RosHeaderID in ("+RosHeaderIDlist + ")  ");
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
		public com.portal.db.Model.RosHeader GetModel(string RosHeaderID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from RosHeader ");
			strSql.Append(" where RosHeaderID=@RosHeaderID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RosHeaderID", SqlDbType.VarChar,32)			};
			parameters[0].Value = RosHeaderID;

			com.portal.db.Model.RosHeader model=new com.portal.db.Model.RosHeader();
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
		public com.portal.db.Model.RosHeader DataRowToModel(DataRow row)
		{
			com.portal.db.Model.RosHeader model=new com.portal.db.Model.RosHeader();
			if (row != null)
			{
				if(row["RosHeaderID"]!=null)
				{
					model.RosHeaderID=row["RosHeaderID"].ToString();
				}
				if(row["RosItemNO"]!=null)
				{
					model.RosItemNO=row["RosItemNO"].ToString();
				}
				if(row["RosDesc"]!=null)
				{
					model.RosDesc=row["RosDesc"].ToString();
				}
				if(row["RosModel"]!=null)
				{
					model.RosModel=row["RosModel"].ToString();
				}
				if(row["RosCategory"]!=null)
				{
					model.RosCategory=row["RosCategory"].ToString();
				}
				if(row["RosBuyer"]!=null)
				{
					model.RosBuyer=row["RosBuyer"].ToString();
				}
				if(row["RosAllocationPercent"]!=null)
				{
					model.RosAllocationPercent=row["RosAllocationPercent"].ToString();
				}
				if(row["RosLeadTime"]!=null)
				{
					model.RosLeadTime=row["RosLeadTime"].ToString();
				}
				if(row["RosStockQty"]!=null)
				{
					model.RosStockQty=row["RosStockQty"].ToString();
				}
				if(row["RosSafeStock"]!=null)
				{
					model.RosSafeStock=row["RosSafeStock"].ToString();
				}
				if(row["PoNumber"]!=null)
				{
					model.PoNumber=row["PoNumber"].ToString();
				}
				if(row["OpenPoQty"]!=null)
				{
					model.OpenPoQty=row["OpenPoQty"].ToString();
				}
				if(row["messageBodyID"]!=null)
				{
                    model.MessageBodyID = row["messageBodyID"].ToString();
				}
                if (row["GlodPlanFlag"] != null)
				{
                    model.Glod_plan_flag = row["GlodPlanFlag"].ToString();
				}
                if (row["VmiStock"] != null)
				{
                    model.VmiStock = row["VmiStock"].ToString();
				}
                if (row["UpdateFlag"] != null)
                {
                    model.UpdateFlag = row["UpdateFlag"].ToString();
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
			strSql.Append(" FROM RosHeader ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM RosHeader ");
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
			strSql.Append("select count(1) FROM RosHeader ");
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
				strSql.Append("order by T.RosHeaderID desc");
			}
			strSql.Append(")AS Row, T.*  from RosHeader T ");
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
			parameters[0].Value = "RosHeader";
			parameters[1].Value = "RosHeaderID";
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

