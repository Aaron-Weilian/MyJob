using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace DBCommon
{
    public class SQLiteDBAccess
    {
        public SQLiteDBAccess(string Connstr)
        {

            _connstr = Connstr;
        }

        public  string _connstr = string.Empty;
        public SQLiteConnection  _conn = null;
       
        #region CONNECTION
  
        /// <summary>
        /// This method gets the connection string.
        /// </summary>
        /// <returns>Connection String</returns>
        public string GetConnectionString()
        {
            try
            {
                string strReturnConnectionString;

                string server = "10.155.36.139";
                string database = "portal";
                string uid = "sa";
                string password = "Emex1234";

                strReturnConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                /* This code takes connection string from the web.config file.*/

                //strReturnConnectionString = _connstr;//ConfigurationSettings ConnectionStrings["MySqlConnectionString"].ConnectionString.ToString();        

                return strReturnConnectionString;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        



        /// <summary>
        /// This method returns MySqlConnection object.
        /// </summary>for SQLServer2008
        /// <returns>MySqlConnection</returns>
      

        //For SQLite
        public SQLiteConnection GetConnection()
        {
            try
            {
                if (_connstr == string.Empty)
                {
                    // _connstr = GetConnectionString();

                }
                if (_conn == null)
                {
                    _conn = new SQLiteConnection(_connstr);
                }
                if (_conn.State != ConnectionState.Open)
                {
                    //_conn.Open();
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
            //SqlConnection mySqlConnection = GetConnection();
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                DataSet dsData = new DataSet();
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter();
                //SqlCommand myCommand = new SqlCommand();

                SQLiteCommand cmd = new SQLiteCommand();


                cmd.Connection = mySqlConnection;
                cmd.CommandType = cmdType;
                cmd.CommandText = CommandText;
                myDataAdapter.SelectCommand = cmd;
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
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                DataSet dsData = new DataSet();
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter();
                SQLiteCommand myCommand = new SQLiteCommand();
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
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                SQLiteCommand myCommand = new SQLiteCommand();
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
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                SQLiteCommand myCommand = new SQLiteCommand();
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
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                SQLiteCommand myCommand = new SQLiteCommand();
                myCommand.Connection = mySqlConnection;
                myCommand.CommandType = cmdType;
                myCommand.CommandText = CommandText;
                mySqlConnection.Open();
                SQLiteTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                intRValue = myCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();
                return intRValue;

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
        /// /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="CommandText">CommandText</param>
        /// <param name="mysqlParams">MySqlParameters</param>
        /// <returns></returns>


        public int ExecuteNonQuery(CommandType cmdType, string CommandText, SqlParameter[] mysqlParams)
        {
            int intRValue;
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                mySqlConnection.Open();
                SQLiteTransaction mySqlTransaction = mySqlConnection.BeginTransaction();

                SQLiteCommand myCommand = new SQLiteCommand();
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
                throw Ex;
            }
            finally
            {
                mySqlConnection.Close();
            }

        }


        #endregion


        #region EXECUTE READER

        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="cmdType">Command Type</param>
        /// <param name="CommandText">Command Text</param>
        /// <returns></returns>
       
        public SQLiteDataReader ExecuteReader(CommandType cmdType, string CommandText)
        {
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                SQLiteDataReader mySqlDataReader;
                SQLiteCommand myCommand = new SQLiteCommand();
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

        public SQLiteDataReader ExecuteReader(CommandType cmdType, string CommandText, SqlParameter[] mysqlParams)
        {
            SQLiteConnection mySqlConnection = GetConnection();
            try
            {
                SQLiteDataReader mySqlDataReader;
                SQLiteCommand myCommand = new SQLiteCommand();
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
            SQLiteConnection mySqlConnection = GetConnection();
            bool exist = false;
            try
            {
                SQLiteDataReader mySqlDataReader;
                SQLiteCommand myCommand = new SQLiteCommand();
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
