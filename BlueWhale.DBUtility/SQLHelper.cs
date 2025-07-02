using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DBUtility
{
    public class SQLHelper
    {
        public static string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();

        public static int corpId = 0;

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

        #region Excecute Query---Return 1 Record

        /// <summary>
        /// Excecute Query---Return 1 Record
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            if (connection == null) throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            // Create command  
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                // Create SQL Data Reader  
                SqlDataReader dataReader;
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // when the reader is closed, so if the parameters are detached from the command  
                // then the SqlReader can set its values.   
                // When this happen, the parameters can be used again in other command.  
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        public static DataSet SqlDataAdapter(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
            catch (SqlException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
            }
        }

        #endregion

        #region Execute Query -- Return DataSet

        /// <summary>
        /// Execute query -- Return DataSet
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="cmdType">Command type (e.g., Text, Stored Procedure)</param>
        /// <param name="cmdText">SQL query or stored procedure name</param>
        /// <param name="commandParameters">SQL parameters</param>
        /// <returns>Returns a DataSet containing the query results</returns>
        public static DataSet SqlDataAdapter(int page1, int page2, string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, page1, page2, "product");
                cmd.Parameters.Clear();
                return ds;
            }
            catch (SqlException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        #endregion

        #region Set Parameters for SqlCommand

        /// <summary>
        /// Set parameters for SqlCommand
        /// </summary>
        /// <param name="cmd">The SqlCommand object</param>
        /// <param name="conn">The SqlConnection object</param>
        /// <param name="trans">The SqlTransaction object (if any)</param>
        /// <param name="cmdType">The type of the command (e.g., Text, Stored Procedure)</param>
        /// <param name="cmdText">The SQL query or stored procedure name</param>
        /// <param name="cmdParms">The parameters for the SQL command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion
    }
}