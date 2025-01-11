using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class CorpDAL
    {
        public CorpDAL()
        {

        }

        #region 成员字段

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string suiteId;
        public string SuiteId
        {
            get { return suiteId; }
            set { suiteId = value; }
        }

        private string token;
        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        private string encodingAESKey;
        public string EncodingAESKey
        {
            get { return encodingAESKey; }
            set { encodingAESKey = value; }
        }

        private string suiteSecret;
        public string SuiteSecret
        {
            get { return suiteSecret; }
            set { suiteSecret = value; }
        }

        private string suiteTicket;
        public string SuiteTicket
        {
            get { return suiteTicket; }
            set { suiteTicket = value; }
        }

        private DateTime lastupatetime;
        public DateTime Lastupatetime
        {
            get { return lastupatetime; }
            set { lastupatetime = value; }
        }

        private int updateNum;
        public int UpdateNum
        {
            get { return updateNum; }
            set { updateNum = value; }
        }


        #endregion

        #region  成员方法

      

       

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(string suiteId)
        {
            bool flag = false;

            string sql = "select * from GetSuiteTicket where suiteId='" + suiteId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

     



     
        #endregion

        #region 新增SuiteTicket信息
        /// <summary>
        /// 新增SuiteTicket信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @"insert into GetSuiteTicket
                                                   (suiteId
                                                   ,token
                                                   ,encodingAESKey
                                                   ,suiteSecret
                                                   ,suiteTicket
                                                   ,lastupatetime
                                                   ,updateNum)
                                             VALUES
                                                   (@suiteId
                                                   ,@token
                                                   ,@encodingAESKey
                                                   ,@suiteSecret
                                                   ,@suiteTicket
                                                   ,@lastupatetime
                                                   ,@updateNum) ";
            SqlParameter[] param = {
                                       new SqlParameter("@suiteId",SuiteId),
                                       new SqlParameter("@token",Token),
                                       new SqlParameter("@encodingAESKey",EncodingAESKey),
                                       new SqlParameter("@suiteSecret",SuiteSecret),
                                       new SqlParameter("@suiteTicket",SuiteTicket),
                                       new SqlParameter("@lastupatetime",Lastupatetime),
                                       new SqlParameter("@updateNum",UpdateNum)

                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);


          
        }

        #endregion

        #region 修改SuiteTicket信息
        /// <summary>
        /// 修改SuiteTicket信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE GetSuiteTicket
                                   SET suiteTicket = @suiteTicket
                                      ,lastupatetime = @lastupatetime
                                      ,updateNum =updateNum+1

                                 WHERE suiteId=@suiteId ";

            SqlParameter[] param = {
                                       
                                       new SqlParameter("@suiteId",SuiteId),                                      
                                       new SqlParameter("@suiteTicket",SuiteTicket),
                                       new SqlParameter("@lastupatetime",Lastupatetime),
                                       new SqlParameter("@updateNum",UpdateNum)

                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

           

        }
        #endregion

        #region 获取所有SuiteTicket

        /// <summary>
        /// 获取所有SuiteTicket
        /// </summary>
        /// <returns></returns>
        public DataSet GetSuiteTicketBySuiteId(string suiteId)
        {
            string sql = "select top 1  * from GetSuiteTicket where suiteId='" + suiteId + "' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

    }
}
