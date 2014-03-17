using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using System.Data.SqlClient;

namespace com.portal.db.DAL
{
    public partial class CommomDAL
    {
        private readonly DBCommon.DBAccess dal = new DBCommon.DBAccess(GetAppSetting.GetConnSetting());

        #region  BasicMethod
        public bool NonQueryByStoredProcedure(string StoredProcedureName)
        {
            int rows = dal.Execute(System.Data.CommandType.StoredProcedure, StoredProcedureName);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet QuerySQL(string sql){
            return dal.ExecuteDataSet(CommandType.Text, sql);
        }

        public int Execute(string CommandText)
        {
            return dal.Execute(CommandType.Text, CommandText);
        }
        #endregion  BasicMethod

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Data_Structure ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return dal.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

            //}
        //public com.portal.db.Model.BodyStructure GetModel(string strWhere)
        //{
        //    DataSet ds = GetList(strWhere);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return DataRowToModel(ds.Tables[0].Rows[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public com.portal.db.Model.BodyStructure DataRowToModel(DataRow row)
        //{
        //    com.portal.db.Model.BodyStructure model = new com.portal.db.Model.BodyStructure();
        //    if (row != null)
        //    {
        //        if (row["ID"] != null)
        //        {
        //            model.ID = row["ID"].ToString();
        //        }
        //        if (row["MessageType"] != null)
        //        {
        //            model.MessageType = row["MessageType"].ToString();
        //        }
        //        if (row["Parent"] != null)
        //        {
        //            model.Parent = row["Parent"].ToString();
        //        }
        //        if (row["Name"] != null)
        //        {
        //            model.Name = row["Name"].ToString();
        //        }
        //        if (row["ObjectNO"] != null)
        //        {
        //            model.ObjectNO = row["ObjectNO"].ToString();
        //        }
                

        //    }
        //    return model;
        //}

        public DataSet GetFieldList(string strWhere) {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Data_Field ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return dal.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        public DataSet GetDataList(string table,Dictionary<string,string> param)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ").Append(table)
                  .Append(" where 1=1 ");

            int n = 0;
            foreach(string key in param.Keys) {
                if (param[key] != "")
                {
                    strSql.Append(" and [" + key + "]= @" + key);
                    n++;
                }
            }

            if (n > 0)
            {
                SqlParameter[] sqlparam = new SqlParameter[n];
                int i = 0;
                foreach (string key in param.Keys)
                {
                    if (param[key] != "")
                    {
                        sqlparam[i] = new SqlParameter("@" + key + "", param[key]);
                        i++;
                    }
                }

                return dal.ExecuteDataSet(CommandType.Text, strSql.ToString(), sqlparam);
            }
            else {
                return dal.ExecuteDataSet(CommandType.Text, strSql.ToString());
            }
        }

        public void CreateInstanc(string className, ref Object obj) { 
            obj = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(className, false);
        }

        public Object Data2Model(DataRow row, Type type)
        {
            Object obj = type.Assembly.CreateInstance(type.FullName);

            if (row != null)
            {
                foreach (System.Reflection.PropertyInfo property in type.GetProperties()) {
                    string methodName = "set_" + property.Name;
                    System.Reflection.MethodInfo method = type.GetMethod(methodName);

                    if (method.GetParameters().Length > 0)
                    {
                        if (row[method.Name.Substring(4)] != null)
                        {
                            object[] parameters = new object[] { 
                                row[method.Name.Substring(4)].ToString() 
                            };

                            method.Invoke(obj, parameters);
                        }
                    }

                }
                
            }

            return obj;
        }

        public bool AddModel(string table,Object model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ").Append(table);
            foreach (System.Reflection.PropertyInfo property in model.GetType().GetProperties())
            { 

                string methodName = "get_" + property.Name;
                System.Reflection.MethodInfo method = model.GetType().GetMethod(methodName);

                strSql.Append(" values (")
                      .Append(method.Invoke(model,null)).Append(",");
                
            }

            int rows = dal.ExecuteNonQuery(CommandType.Text, strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddModel(string table, Dictionary<string, string> DataValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ").Append(table).Append(" ( ");
            foreach (string property in DataValue.Keys)
            {
                strSql.Append("[").Append(property).Append("],");
            }
            //去除末尾逗号
            strSql.Remove(strSql.Length-1, 1);
            strSql.Append(") ").Append("values(");
            foreach (string property in DataValue.Keys)
            {
                strSql.Append("@").Append(property).Append(",");

            }
            //去除末尾逗号
            strSql.Remove(strSql.Length-1, 1);
            strSql.Append(") ");

            SqlParameter[] sqlparam = new SqlParameter[DataValue.Count];
            int i = 0;
            foreach (string key in DataValue.Keys)
            {
                sqlparam[i] = new SqlParameter("@" + key + "", DataValue[key]);
                i++;
            }

            int rows = dal.ExecuteNonQuery(CommandType.Text, strSql.ToString(), sqlparam);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateModel(string table, Dictionary<string, string> DataValue,string key,string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ").Append(table).Append(" set ");
            foreach (string property in DataValue.Keys)
            {
                strSql.Append("[").Append(property).Append("]=@").Append(property).Append(",");
            }
            //去除末尾逗号
            strSql.Remove(strSql.Length-1, 1);
            strSql.Append(" where ").Append("[").Append(key).Append("]=@").Append(key);

            SqlParameter[] sqlparam = new SqlParameter[DataValue.Count+1];
            int i = 0;
            foreach (string property in DataValue.Keys)
            {
                sqlparam[i] = new SqlParameter("@" + property + "", DataValue[property]);
                i++;
            }
            sqlparam[i] = new SqlParameter("@" + key + "", value);

            int rows = dal.ExecuteNonQuery(CommandType.Text, strSql.ToString(), sqlparam);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        //public com.portal.db.Model.DataField FieldDataRowToModel(DataRow row)
        //{
        //    com.portal.db.Model.DataField model = new com.portal.db.Model.DataField();
        //    if (row != null)
        //    {
        //        if (row["ID"] != null)
        //        {
        //            model.ID = row["ID"].ToString();
        //        }
        //        if (row["SOBJECT"] != null)
        //        {
        //            model.SOBJECT = row["SOBJECT"].ToString();
        //        }
        //        if (row["STABLE"] != null)
        //        {
        //            model.STABLE = row["STABLE"].ToString();
        //        }
        //        if (row["SFIELDAS"] != null)
        //        {
        //            model.SFIELDAS = row["SFIELDAS"].ToString();
        //        }
        //        if (row["SFIELD"] != null)
        //        {
        //            model.SFIELD = row["SFIELD"].ToString();
        //        }
        //        if (row["STITLE"] != null)
        //        {
        //            model.STITLE = row["STITLE"].ToString();
        //        }
        //        if (row["SDATATYPE"] != null)
        //        {
        //            model.SDATATYPE = row["SDATATYPE"].ToString();
        //        }

        //    }
        //    return model;
        //}
    }
}
