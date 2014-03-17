using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;



namespace DBCommon
{
    public class DBAccess
    {
        public DBAccess(string Connstr)
        {
            _connstr = Connstr;
        }

        public  string _connstr = string.Empty;
        public  SqlConnection _conn = null;
       
        #region CONNECTION


        /// <summary>
        /// This method returns MySqlConnection object.
        /// </summary>for SQLServer2008
        /// <returns>MySqlConnection</returns>
        public SqlConnection GetConnection()
        {
            try
            {
                if (_conn == null)
                {
                   // if (_connstr == null || _connstr == "")
                       // _connstr = GetAppSetting.GetConnSetting();
                    _conn = new SqlConnection(_connstr);
                }

                return _conn;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

       

        #endregion


        #region EXECUTE DATASET

        public DataSet ExecuteDataSet(CommandType cmdType, string CommandText)
        {
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                myDataAdapter.SelectCommand = myCommand;
                myDataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }
        }


        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="cmdType">Command type</param>
        /// <param name="cmdText">Command text</param>
        /// <param name="mysqlParams">MySqlParameters</param>
        /// <returns>Data in the form of Dataset.</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string CommandText, SqlParameter[] mysqlParams)
        {
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = GetConnection();
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;

                for (int i = 0; i < mysqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(mysqlParams[i]);
                }

                myDataAdapter.SelectCommand = myCommand;
                myDataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

       
        #endregion


        #region EXECUTE SCALAR
                     

        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="CommandText">CommandText</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(CommandType cmdType, string CommandText)
        {
            object objValue;
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                mySqlConnection.Open();
                objValue = myCommand.ExecuteScalar();
                return objValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }

        }


        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="CommandText">CommandText</param>
        /// <param name="mysqlParams">MySqlParameter</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(CommandType cmdType, string CommandText, SqlParameter[] mysqlParams)
        {
            object objValue;
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                for (int i = 0; i < mysqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(mysqlParams[i]);
                }
                mySqlConnection.Open();
                objValue = myCommand.ExecuteScalar();
                return objValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }

        }

               

        #endregion


        #region EXECUTE NONQUERY

        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="CommandText">CommandText</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType cmdType, string CommandText)
        {
            int intRValue;
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                
                mySqlConnection.Open();
                SqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                intRValue = myCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();


                return intRValue;

            }
            catch (SqlException Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }

        }

        public int Execute(CommandType cmdType, string CommandText)
        {
            int intRValue;
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                mySqlConnection.Open();
                SqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                myCommand.Transaction = mySqlTransaction;

                intRValue = myCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();

                return intRValue;

            }
            catch (SqlException Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }

        }

        /// <summary>
        /// /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="CommandText">CommandText</param>
        /// <param name="mysqlParams">MySqlParameters</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType cmdType, string CommandText, SqlParameter[] mysqlParams)
        {
            int intRValue;
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                mySqlConnection.Open();
                SqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                myCommand.Transaction = mySqlTransaction;

                for (int i = 0; i < mysqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(mysqlParams[i]);
                }


                intRValue = myCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();

                return intRValue;

            }
            catch (Exception Ex)
            {
                throw Ex ;
            }
            finally
            {
                mySqlConnection.Close();
            }

        }

        #endregion

        public int InsertDataSet(SqlCommand insertCommand, DataTable dataTable){
            using (SqlConnection conn = GetConnection()) {
                SqlDataAdapter da = new SqlDataAdapter(null, conn);
                insertCommand.Connection = conn;
                da.InsertCommand = insertCommand;
                da.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                da.UpdateBatchSize = 0;
                return da.Update(dataTable);
            }
        }

        public int UpdateDataSet(SqlCommand updateCommand, DataTable dataTable){
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(null, conn);
                updateCommand.Connection = conn;
                da.UpdateCommand = updateCommand;
                da.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                da.UpdateBatchSize = 0;
                return da.Update(dataTable);
            }
        }


        #region EXECUTE READER

        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="cmdType">Command Type</param>
        /// <param name="CommandText">Command Text</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(CommandType cmdType, string CommandText)
        {
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                SqlDataReader mySqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                mySqlConnection.Open();
                mySqlDataReader = myCommand.ExecuteReader();
                return mySqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
      
        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="cmdType">Command Type</param>
        /// <param name="CommandText">Command Text</param>
        /// <param name="mysqlParams">MYSQLPARAMETERS</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(CommandType cmdType, string CommandText, SqlParameter[] mysqlParams)
        {
            SqlConnection mySqlConnection = GetConnection();
            try
            {
                SqlDataReader mySqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                for (int i = 0; i < mysqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(mysqlParams[i]);
                }

                mySqlConnection.Open();
                mySqlDataReader = myCommand.ExecuteReader();
                return mySqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }
        }


        public bool Exists(string CommandText, SqlParameter[] mysqlParams)
        {
            SqlConnection mySqlConnection = GetConnection();
            bool exist = false;
            try
            {
                SqlDataReader mySqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                for (int i = 0; i < mysqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(mysqlParams[i]);
                }

                mySqlConnection.Open();
                mySqlDataReader = myCommand.ExecuteReader();
                exist = mySqlDataReader.Read() ? Convert.ToInt32(mySqlDataReader.GetValue(0)) > 0 : false; ;
                return exist;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }
        }


        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="cmdType">Command Type</param>
        /// <param name="CommandText">Command Text</param>
        /// <param name="mysqlParams">MYSQLPARAMETERS</param>
        /// <returns></returns>
        public void ExecuteTrascationReader(CommandType cmdType, string CommandText)//, MySqlParameter[] mysqlParams)
        {
            bool success = true;
            GetConnection();
            SqlTransaction txn = _conn.BeginTransaction();
            try
            {
                SqlDataReader mySqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = _conn;
                myCommand.CommandType = cmdType;

                //for (int i = 0; i < mysqlParams.Length; i++)
                //{
                //    myCommand.Parameters.Add(mysqlParams[i]);
                //}
                string selectstr = string.Format("select count(*) from test_insert2");

                myCommand.CommandText = selectstr;
                mySqlDataReader = myCommand.ExecuteReader();
                bool exist = mySqlDataReader.Read() ? Convert.ToInt32(mySqlDataReader.GetValue(0)) > 4 : false;

                mySqlDataReader.Close();
                if (exist)
                {
                    myCommand.CommandText = string.Format("update test_insert1 set Name= '{0}'where Name =2", "10");
                    myCommand.CommandText = string.Format("delete from test_insert1 where Name= '{0}'", "2");
                }
                else
                {
                    myCommand.CommandText =
                        string.Format("Insert into test_insert1(Name,DateTime,ChineseName) values({0},'{1}','{2}')",
                                      "2", DateTime.Now.ToString(), "中国字111");//EncodeTraslate("中国字"));
                }
                //myCommand.CommandText = CommandText;

                myCommand.ExecuteNonQuery();
                //return mySqlDataReader;

                myCommand.CommandText = string.Format("Insert into test_insert2(Name,DateTime,Selected) values({0},'{1}',1)",
                                                        "7", DateTime.Now.ToString());

                myCommand.CommandText = string.Format("Insert into test_insert1(Name,DateTime,ChineseName) values({0},'{1}','{2}')",
                                                        "2", DateTime.Now.ToString(), "中国字222");
                myCommand.ExecuteNonQuery();
                //int i = Convert.ToInt32("da");
            }
            catch (Exception Ex)
            {
                success = false;
                //throw Ex;
            }
            finally
            {
                if (success)
                {
                    txn.Commit();
                }
                else
                {
                    txn.Rollback();
                }
            }
        }

        public string EncodeTraslate(string content)
        {
            
            Encoding iso88591 = Encoding.GetEncoding("iso8859-1");
            Encoding df = Encoding.Default;

            byte[] gb2312bytes = df.GetBytes(content);
            //byte[] asciiBytes = Encoding.Convert(df, iso88591, gb2312bytes);
            string str = iso88591.GetString(gb2312bytes);
            return str;

        }

        #endregion
    }
}
