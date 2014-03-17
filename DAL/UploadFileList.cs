using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace com.portal.db.DAL
{
    public partial class UploadFileList
    {

        #region  BasicMethod
        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.UploadFileList model)
        {
            string guid = System.Guid.NewGuid().ToString("N");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UploadFileList(");
            strSql.Append("UploadFileListID,FileName,FilePath,FileStream,Status,MessageType,ConfirmDate,UploadBy)");
            strSql.Append(" values (");
            strSql.Append("@UploadFileListID,@FileName,@FilePath,@FileStream,@Status,@MessageType,@ConfirmDate,@UploadBy)");
            SqlParameter[] parameters = {
					new SqlParameter("@UploadFileListID", SqlDbType.VarChar,32),
					new SqlParameter("@FileName", SqlDbType.VarChar,100),
					new SqlParameter("@FilePath", SqlDbType.VarChar,200),
					new SqlParameter("@FileStream", SqlDbType.VarBinary),
					new SqlParameter("@Status", SqlDbType.VarChar,10),
					new SqlParameter("@MessageType", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmDate", SqlDbType.VarChar,20),
                    new SqlParameter("@UploadBy", SqlDbType.VarChar,50)                    };
            parameters[0].Value = guid;
            parameters[1].Value = model.FileName == null ? DBNull.Value.ToString() : model.FileName;
            parameters[2].Value = model.FilePath == null ? DBNull.Value.ToString() : model.FilePath;
            parameters[3].Value = model.FileStream ;
            parameters[4].Value = model.Status == null ? DBNull.Value.ToString() : model.Status;
            parameters[5].Value = model.MessageType == null ? DBNull.Value.ToString() : model.MessageType;
            parameters[6].Value = model.ConfirmDate == null ? DBNull.Value.ToString() : model.ConfirmDate;
            parameters[7].Value = model.UploadBy == null ? DBNull.Value.ToString() : model.UploadBy;
           

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
        /// 更新一条数据
        /// </summary>
        public bool Update(com.portal.db.Model.UploadFileList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UploadFileList set ");
            strSql.Append("FileName=@FileName,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("FileStream=@FileStream,");
            strSql.Append("Status=@Status,");
            strSql.Append("MessageType=@MessageType,");
            strSql.Append("ConfirmDate=@ConfirmDate,");
            strSql.Append("UploadBy=@UploadBy");
            strSql.Append(" where UploadFileListID=@UploadFileListID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UploadFileListID", SqlDbType.VarChar,32),
					new SqlParameter("@FileName", SqlDbType.VarChar,100),
					new SqlParameter("@FilePath", SqlDbType.VarChar,200),
					new SqlParameter("@FileStream", SqlDbType.VarBinary),
					new SqlParameter("@Status", SqlDbType.VarChar,10),
					new SqlParameter("@MessageType", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmDate", SqlDbType.VarChar,20),
                    new SqlParameter("@UploadBy", SqlDbType.VarChar,50)                    };
            parameters[0].Value = model.UploadFileListID == null ? DBNull.Value.ToString() : model.UploadFileListID;
            parameters[1].Value = model.FileName == null ? DBNull.Value.ToString() : model.FileName;
            parameters[2].Value = model.FilePath == null ? DBNull.Value.ToString() : model.FilePath;
            parameters[3].Value = model.FileStream ;
            parameters[4].Value = model.Status == null ? DBNull.Value.ToString() : model.Status;
            parameters[5].Value = model.MessageType == null ? DBNull.Value.ToString() : model.MessageType;
            parameters[6].Value = model.ConfirmDate == null ? DBNull.Value.ToString() : model.ConfirmDate;
            parameters[7].Value = model.UploadBy == null ? DBNull.Value.ToString() : model.UploadBy;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UploadFileListID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UploadFileList ");
            strSql.Append(" where UploadFileListID=@UploadFileListID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UploadFileListID", SqlDbType.VarChar,32)			};
            parameters[0].Value = UploadFileListID;

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
        /// 得到一个对象实体
        /// </summary>
        public com.portal.db.Model.UploadFileList DataRowToModel(DataRow row)
        {
            com.portal.db.Model.UploadFileList model = new com.portal.db.Model.UploadFileList();
            if (row != null)
            {
                if (row["UploadFileListID"] != null)
                {
                    model.UploadFileListID = row["UploadFileListID"].ToString();
                }
                if (row["FileName"] != null)
                {
                    model.FileName = row["FileName"].ToString();
                }
                if (row["FilePath"] != null)
                {
                    model.FilePath = row["FilePath"].ToString();
                
                }
                if (row["FileStream"] != null)
                {
                    model.FileStream = (byte[])row["FileStream"];
                }
                if (row["MessageType"] != null)
                {
                    model.MessageType = row["MessageType"].ToString();
                }
                if (row["Status"] != null)
                {
                    model.Status = row["Status"].ToString();
                }
                if (row["UploadBy"] != null)
                {
                    model.UploadBy = row["UploadBy"].ToString();
                }
                if (row["ConfirmDate"] != null && row["ConfirmDate"].ToString() != "")
                {
                    model.ConfirmDate = row["ConfirmDate"].ToString();
                }
                
            }
            return model;
        }


        public com.portal.db.Model.UploadFileList DataRowToModelNOStream(DataRow row)
        {
            com.portal.db.Model.UploadFileList model = new com.portal.db.Model.UploadFileList();
            if (row != null)
            {
                if (row["UploadFileListID"] != null)
                {
                    model.UploadFileListID = row["UploadFileListID"].ToString();
                }
                if (row["FileName"] != null)
                {
                    model.FileName = row["FileName"].ToString();
                }
                if (row["FilePath"] != null)
                {
                    model.FilePath = row["FilePath"].ToString();

                }
                //if (row["FileStream"] != null)
                //{
                //    model.FileStream = (byte[])row["FileStream"];
                //}
                if (row["MessageType"] != null)
                {
                    model.MessageType = row["MessageType"].ToString();
                }
                if (row["Status"] != null)
                {
                    model.Status = row["Status"].ToString();
                }
                if (row["UploadBy"] != null)
                {
                    model.UploadBy = row["UploadBy"].ToString();
                }
                if (row["ConfirmDate"] != null && row["ConfirmDate"].ToString() != "")
                {
                    model.ConfirmDate = row["ConfirmDate"].ToString();
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
            strSql.Append("select UploadFileListID,FileName,FilePath,FileStream,MessageType,Status,UploadBy,ConfirmDate");
            strSql.Append(" FROM UploadFileList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append("order by ConfirmDate desc");
            return dal.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFileList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UploadFileListID,FileName,FilePath,MessageType,Status,UploadBy,ConfirmDate");
            strSql.Append(" FROM UploadFileList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append("order by ConfirmDate desc");
            return dal.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }


        #endregion BasicMethod
    }
}
