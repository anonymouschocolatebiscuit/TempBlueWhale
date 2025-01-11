using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Lanwei.Weixin.DBUtility
{
    public class SQLHelper
    {

        #region 连接数据库字符串
        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        public static string ConStr = ConfigurationSettings.AppSettings["ConStr"].ToString();

       

        /// <summary>
        /// 通过微信获取openId、唯一
        /// </summary>
        public static string openId = "";



        /// <summary>
        /// 通过微信获取fromOpenId、唯一
        /// </summary>
        public static string fromOpenId = "";


        /// <summary>
        /// 用于存储微信登录后的客户ID
        /// </summary>
        public static int clientId = 0;

        public static int clientTypeId = 0;

        public static string clientName = "游客";

        public static string clientTypeName = "默认级别";

        public static string clientPwd = "";

        
        #region 演示系统的用户数据采集

        /// <summary>
        /// 进入演示系统的userId、用于统计分析
        /// </summary>
        public static string demoUserId = "";

        /// <summary>
        /// 手机号码
        /// </summary>
        public static string demoUserMobile = "";

        /// <summary>
        /// 姓名
        /// </summary>
        public static string demoUserName = "";

        #endregion

        /// <summary>
        /// 用于存储微信登录后的用户ID
        /// </summary>
        public static int userId = 0;

        public static int shopId = 0;

        public static string userName = "管理员";

        public static string userPwd = "";


        /// <summary>
        /// app平台Id、用于切换企业数据库
        /// </summary>
        public static int appId = 0;

        public static int corpId = 0;

        /// <summary>
        /// app平台名称
        /// </summary>
        public static string appName = "蓝微.云订货";


        /// <summary>
        /// 行业判断
        /// </summary>
        public static string trade = "1";

        /// <summary>
        /// 微信企业号corpId
        /// </summary>
        public static string corpIdQY = string.Empty;

        /// <summary>
        /// 微信企业号corpSecret
        /// </summary>
        public static string corpSecretQY = string.Empty;

        /// <summary>
        /// 微信企业号的企业永久授权码
        /// </summary>
        public static string permanent_codeQY = string.Empty;


        /// <summary>
        /// 阿里钉钉corpId
        /// </summary>
        public static string corpIdDD = string.Empty;

        /// <summary>
        /// 阿里钉钉corpSecret
        /// </summary>
        public static string corpSecretDD = string.Empty;

        /// <summary>
        /// 阿里钉钉的企业永久授权码
        /// </summary>
        public static string permanent_codeDD = string.Empty;


        #endregion

        #region 执行增,删,改------返回影响行数
        /// <summary>
        /// 执行增,删,改------返回影响行数
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
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
                    //conn.Close();//自己加的程序
                }
            }
        }
        #endregion

        #region 执行查询---返回一行记录
        /// <summary>
        /// 执行查询---返回一行记录
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            #region 原来的写法
            //SqlCommand cmd = new SqlCommand();
            //SqlConnection conn = new SqlConnection(connectionString);          


            //try
            //{
            //    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            //    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    cmd.Parameters.Clear();
            //    return rdr;

            //}
            //catch (SqlException E)
            //{
            //    conn.Close();
            //    throw new Exception(E.Message);
                
            //}
            //finally
            //{
            //    cmd.Dispose();
            //    //conn.Close();//自己加的程序
            //}

            #endregion

            SqlConnection connection = new SqlConnection(connectionString);          

            if (connection == null) throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            // 创建命令  
            SqlCommand cmd = new SqlCommand();
            try
            {
               
              
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                // 创建数据阅读器  
                SqlDataReader dataReader;
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // 清除参数,以便再次使用..  
                // HACK: There is a problem here, the output parameter values are fletched   
                // when the reader is closed, so if the parameters are detached from the command  
                // then the SqlReader can磘 set its values.   
                // When this happen, the parameters can磘 be used again in other command.  
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

        #endregion

        #region 执行查询--返回数据集
        /// <summary>
        /// 执行查询--返回数据集
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
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
                conn.Close();//自己加的程序
            }

        }

        #endregion


        #region 执行查询--返回数据集
        /// <summary>
        /// 执行查询--返回数据集
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet SqlDataAdapter(int page1,int page2,string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {


                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds,page1,page2,"product");
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
                conn.Close();//自己加的程序
            }

        }

        #endregion


        #region 执行查询
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static string ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
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
                return cmd.ExecuteScalar().ToString();
            }
            catch (SqlException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();//自己加的程序
            }
        }

        #endregion

        #region 为SqlCommand命令设置参数
        /// <summary>
        /// 为SqlCommand命令设置参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
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
