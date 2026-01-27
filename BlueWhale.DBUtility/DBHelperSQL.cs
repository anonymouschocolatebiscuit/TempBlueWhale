using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DBUtility
{
    public abstract class DbHelperSQL
    {
        // Database connection string (configured in web.config),
        // supports dynamic changes to connectionString for multiple databases.
#pragma warning disable CS0618 // Type or member is obsolete
        public static string connectionString = ConfigurationSettings.AppSettings["ConStr"].ToString();
#pragma warning restore CS0618 // Type or member is obsolete

        public DbHelperSQL()
        {
        }

        #region Public Methods

        /// <summary>
        /// Check whether a column exists in a table
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="columnName">Column name</param>
        /// <returns>True if exists, otherwise false</returns>
        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object result = GetSingle(sql);

            if (result == null)
            {
                return false;
            }

            return Convert.ToInt32(result) > 0;
        }

        public static int GetMinID(string fieldName, string tableName)
        {
            string sql = "select min(" + fieldName + ") from " + tableName;
            object result = GetSingle(sql);

            if (result == null)
            {
                return 0;
            }

            return int.Parse(result.ToString());
        }

        public static int GetMaxID(string fieldName, string tableName)
        {
            string sql = "select max(" + fieldName + ")+1 from " + tableName;
            object result = GetSingle(sql);

            if (result == null)
            {
                return 1;
            }

            return int.Parse(result.ToString());
        }

        public static bool Exists(string sql)
        {
            object result = GetSingle(sql);
            int count;

            if (Equals(result, null) || Equals(result, DBNull.Value))
            {
                count = 0;
            }
            else
            {
                count = int.Parse(result.ToString());
            }

            return count != 0;
        }

        /// <summary>
        /// Check whether a table exists
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <returns>True if exists, otherwise false</returns>
        public static bool TabExists(string tableName)
        {
            string sql = "select count(*) from sysobjects where id = object_id(N'[" + tableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            // Alternative:
            // SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tableName + "]') AND type in (N'U')

            object result = GetSingle(sql);
            int count;

            if (Equals(result, null) || Equals(result, DBNull.Value))
            {
                count = 0;
            }
            else
            {
                count = int.Parse(result.ToString());
            }

            return count != 0;
        }

        public static bool Exists(string sql, params SqlParameter[] parameters)
        {
            object result = GetSingle(sql, parameters);
            int count;

            if (Equals(result, null) || Equals(result, DBNull.Value))
            {
                count = 0;
            }
            else
            {
                count = int.Parse(result.ToString());
            }

            return count != 0;
        }

        #endregion

        #region Execute SQL Query

        /// <summary>
        /// Execute SQL query and return affected row count
        /// </summary>
        /// <param name="SQLString">SQL query</param>
        /// <returns>Affected row count</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Overload to execute SQL and return affected row count
        /// </summary>
        /// <param name="connection">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="SQLString">SQL query</param>
        /// <returns>Affected row count</returns>
        public static int ExecuteSql(SqlConnection connection, SqlTransaction trans, string SQLString)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    cmd.Connection = connection;
                    cmd.Transaction = trans;
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    cmd.CommandTimeout = Times;
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Executes a mixed transaction of SQL and Oracle
        /// </summary>
        /// <param name="list">SQL command list</param>
        /// <param name="oracleCmdSqlList">Oracle command list</param>
        /// <returns>
        /// 0 - SQL transaction failed  
        /// -1 - Oracle transaction failed  
        /// 1 - Transaction completed successfully
        /// </returns>
        public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn
                };

                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;

                try
                {
                    foreach (CommandInfo myDE in list)
                    {
                        string cmdText = myDE.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;

                        PrepareCommand(cmd, conn, tx, cmdText, cmdParms);

                        if (myDE.EffentNextType == EffentNextType.SolicitationEvent)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                tx.Rollback();
                                throw new Exception("Exception " + myDE.CommandText + " must follow the 'select count(...)' format");
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;

                            if (!(obj == null || obj == DBNull.Value))
                            {
                                isHave = Convert.ToInt32(obj) > 0;
                            }

                            if (isHave)
                            {
                                myDE.OnSolicitationEvent();
                            }
                        }

                        if (myDE.EffentNextType == EffentNextType.WhenHaveContine ||
                            myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                tx.Rollback();
                                throw new Exception("SQL Exception " + myDE.CommandText + " must follow the 'select count(...)' format");
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;

                            if (!(obj == null || obj == DBNull.Value))
                            {
                                isHave = Convert.ToInt32(obj) > 0;
                            }

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                            {
                                tx.Rollback();
                                throw new Exception("SQL Exception " + myDE.CommandText + " returned value must be greater than 0");
                            }

                            if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                            {
                                tx.Rollback();
                                throw new Exception("SQL Exception " + myDE.CommandText + " returned value must be greater than 0");
                            }

                            continue;
                        }

                        int val = cmd.ExecuteNonQuery();

                        if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                        {
                            tx.Rollback();
                            throw new Exception("SQL Exception " + myDE.CommandText + " must affect rows");
                        }

                        cmd.Parameters.Clear();
                    }

                    tx.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Execute multiple SQL queries within a transaction
        /// </summary>
        /// <param name="SQLStringList">SQL query list</param>
        /// <returns>Total affected rows</returns>
        public static int ExecuteSqlTran(List<string> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn
                };

                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;

                try
                {
                    int count = 0;

                    for (int i = 0; i < SQLStringList.Count; i++)
                    {
                        string sql = SQLStringList[i];

                        if (sql.Trim().Length > 1)
                        {
                            cmd.CommandText = sql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// Execute SQL with a single parameter
        /// </summary>
        /// <param name="SQLString">SQL query</param>
        /// <param name="content">Parameter content</param>
        /// <returns>Affected row count</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                SqlParameter parameter = new SqlParameter("@content", SqlDbType.NText)
                {
                    Value = content
                };

                cmd.Parameters.Add(parameter);

                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Execute SQL with a single parameter and return a scalar value
        /// </summary>
        /// <param name="SQLString">SQL query</param>
        /// <param name="content">Parameter content</param>
        /// <returns>Query result</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                SqlParameter parameter = new SqlParameter("@content", SqlDbType.NText)
                {
                    Value = content
                };

                cmd.Parameters.Add(parameter);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (Equals(result, null) || Equals(result, DBNull.Value))
                    {
                        return null;
                    }

                    return result;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Insert image data into the database
        /// </summary>
        /// <param name="strSQL">SQL statement</param>
        /// <param name="fs">Image byte array</param>
        /// <returns>Affected row count</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                SqlParameter parameter = new SqlParameter("@fs", SqlDbType.Image)
                {
                    Value = fs
                };

                cmd.Parameters.Add(parameter);

                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Execute scalar SQL query
        /// </summary>
        /// <param name="SQLString">SQL query</param>
        /// <returns>Query result</returns>
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (Equals(result, null) || Equals(result, DBNull.Value))
                    {
                        return null;
                    }

                    return result;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }

        public static object GetSingle(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    cmd.CommandTimeout = Times;
                    object result = cmd.ExecuteScalar();

                    if (Equals(result, null) || Equals(result, DBNull.Value))
                    {
                        return null;
                    }

                    return result;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Execute query and return SqlDataReader
        /// </summary>
        /// <param name="strSQL">SQL query</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Execute query and return DataSet
        /// </summary>
        /// <param name="SQLString">SQL query</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(SQLString, connection);
                    adapter.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(SQLString, connection);
                    adapter.SelectCommand.CommandTimeout = Times;
                    adapter.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        /// <summary>
        /// Execute query with transaction and return DataSet
        /// </summary>
        /// <param name="connection">SqlConnection</param>
        /// <param name="trans">SqlTransaction</param>
        /// <param name="SQLString">SQL query</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(SqlConnection connection, SqlTransaction trans, string SQLString)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(SQLString, connection);
                adapter.SelectCommand.Transaction = trans;
                adapter.Fill(ds, "ds");
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        #endregion

        #region Execute a parameterized SQL statement

        /// <summary>
        /// Executes an SQL statement and returns the number of affected records.
        /// </summary>
        /// <param name="SQLString">SQL statement</param>
        /// <returns>Number of affected records</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Execute an SQL statement within a transaction and return the number of affected records.
        /// </summary>
        /// <param name="connection">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="SQLString">SQL statement</param>
        /// <returns>Number of affected records</returns>
        public static int ExecuteSql(
            SqlConnection connection,
            SqlTransaction trans,
            string SQLString,
            params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Executes multiple SQL statements to implement a database transaction.
        /// </summary>
        /// <param name="SQLStringList">
        /// Hashtable of SQL statements (key is SQL statement, value is SqlParameter[])
        /// </param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();

                    try
                    {
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])entry.Value;

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Executes multiple SQL statements to implement a database transaction.
        /// </summary>
        /// <param name="cmdList">CommandInfo list</param>
        /// <returns>Total affected rows</returns>
        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();

                    try
                    {
                        int count = 0;

                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine ||
                                myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (cmdText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;

                                if (!(obj == null || obj == DBNull.Value))
                                {
                                    isHave = Convert.ToInt32(obj) > 0;
                                }

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                continue;
                            }

                            int val = cmd.ExecuteNonQuery();
                            count += val;

                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Executes multiple SQL statements with identity return.
        /// </summary>
        /// <param name="SQLStringList">CommandInfo list</param>
        public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();

                    try
                    {
                        int identity = 0;

                        foreach (CommandInfo myDE in SQLStringList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;

                            foreach (SqlParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = identity;
                                }
                            }

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();

                            foreach (SqlParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.Output)
                                {
                                    identity = Convert.ToInt32(parameter.Value);
                                }
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Executes multiple SQL statements with identity return.
        /// </summary>
        /// <param name="SQLStringList">
        /// Hashtable of SQL statements (key is SQL statement, value is SqlParameter[])
        /// </param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();

                    try
                    {
                        int identity = 0;

                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])entry.Value;

                            foreach (SqlParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = identity;
                                }
                            }

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();

                            foreach (SqlParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.Output)
                                {
                                    identity = Convert.ToInt32(parameter.Value);
                                }
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Executes a scalar query with parameters.
        /// </summary>
        /// <param name="SQLString">SQL statement</param>
        /// <returns>Query result</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object result = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    if (Equals(result, null) || Equals(result, DBNull.Value))
                    {
                        return null;
                    }

                    return result;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Executes a scalar query within a transaction.
        /// </summary>
        public static object GetSingle(
            SqlConnection connection,
            SqlTransaction trans,
            string SQLString,
            params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
                    object result = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    if (Equals(result, null) || Equals(result, DBNull.Value))
                    {
                        return null;
                    }

                    return result;
                }
                catch (SqlException e)
                {
                    trans.Rollback();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Executes a query and returns a SqlDataReader.
        /// </summary>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return reader;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Executes a query and returns a DataSet.
        /// </summary>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        adapter.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }
            }
        }

        /// <summary>
        /// Executes a query within a transaction and returns a DataSet.
        /// </summary>
        public static DataSet Query(
            SqlConnection connection,
            SqlTransaction trans,
            string SQLString,
            params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, trans, SQLString, cmdParms);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                try
                {
                    adapter.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (SqlException ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        private static void PrepareCommand(
            SqlCommand cmd,
            SqlConnection conn,
            SqlTransaction trans,
            string cmdText,
            SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.Input ||
                         parameter.Direction == ParameterDirection.InputOutput) &&
                        parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region Stored Procedure Operations

        /// <summary>
        /// Executes a stored procedure and returns a SqlDataReader
        /// (Note: After calling this method, be sure to close the SqlDataReader)
        /// </summary>
        /// <param name="storedProcName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// Executes a stored procedure and returns a DataSet
        /// </summary>
        /// <param name="storedProcName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <param name="tableName">Result table name</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    SelectCommand = BuildQueryCommand(connection, storedProcName, parameters)
                };

                adapter.Fill(dataSet, tableName);
                connection.Close();

                return dataSet;
            }
        }

        /// <summary>
        /// Executes a stored procedure with timeout and returns a DataSet
        /// </summary>
        /// <param name="storedProcName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <param name="tableName">Result table name</param>
        /// <param name="Times">Command timeout</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(
            string storedProcName,
            IDataParameter[] parameters,
            string tableName,
            int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    SelectCommand = BuildQueryCommand(connection, storedProcName, parameters)
                };

                adapter.SelectCommand.CommandTimeout = Times;
                adapter.Fill(dataSet, tableName);
                connection.Close();

                return dataSet;
            }
        }

        /// <summary>
        /// Builds a SqlCommand object (used to return result sets)
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="storedProcName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(
            SqlConnection connection,
            string storedProcName,
            IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // Assign DBNull.Value to unassigned input/output parameters
                    if ((parameter.Direction == ParameterDirection.Input ||
                         parameter.Direction == ParameterDirection.InputOutput) &&
                        parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }

                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// Executes a stored procedure and returns the affected row count
        /// </summary>
        /// <param name="storedProcName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <param name="rowsAffected">Affected rows</param>
        /// <returns>Return value</returns>
        public static int RunProcedure(
            string storedProcName,
            IDataParameter[] parameters,
            out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();

                int result = (int)command.Parameters["ReturnValue"].Value;
                return result;
            }
        }

        /// <summary>
        /// Builds a SqlCommand object that returns an integer value
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="storedProcName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildIntCommand(
            SqlConnection connection,
            string storedProcName,
            IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);

            SqlParameter returnParameter = new SqlParameter(
                "ReturnValue",
                SqlDbType.Int,
                4,
                ParameterDirection.ReturnValue,
                false,
                0,
                0,
                string.Empty,
                DataRowVersion.Default,
                null);

            command.Parameters.Add(returnParameter);
            return command;
        }

        #endregion
    }
}
