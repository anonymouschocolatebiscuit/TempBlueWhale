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
    public class IsvSuiteTicketDingdingDAL
    {
        public IsvSuiteTicketDingdingDAL()
        {

        }

        #region 成员字段

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string suiteKey;
        public string SuiteKey
        {
            get { return suiteKey; }
            set { suiteKey = value; }
        }

        private string suiteToken;
        public string SuiteToken
        {
            get { return suiteToken; }
            set { suiteToken = value; }
        }

        private string encodingAESKey;
        public string EncodingAESKey
        {
            get { return encodingAESKey; }
            set { encodingAESKey = value; }
        }

        private string token;
        public string Token
        {
            get { return token; }
            set { token = value; }
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

        private string suiteAccessToken;
        public string SuiteAccessToken
        {
            get { return suiteAccessToken; }
            set { suiteAccessToken = value; }
        }

        private DateTime lastupatetime;
        public DateTime Lastupatetime
        {
            get { return lastupatetime; }
            set { lastupatetime = value; }
        }


        private string timestamp;
        public string TimeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }


        private DateTime suiteTokenExpires;
        public DateTime SuiteTokenExpires
        {
            get { return suiteTokenExpires; }
            set { suiteTokenExpires = value; }
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
        public bool isExistsCodeAdd(string suiteKey)
        {
            bool flag = false;

            string sql = "select * from IsvSuiteKeyInfoDingding where suiteKey='" + suiteKey + "' ";

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
            string sql = @"insert into IsvSuiteKeyInfoDingding
                                                   (SuiteKey
                                                   ,SuiteSecret
                                                   ,SuiteTicket
                                                   ,Token
                                                   ,EncodingAESKey
                                                   ,TimeStamp
                                                   ,SuiteAccessToken
                                                   ,SuiteTokenExpires
                                                   ,lastupatetime
                                                   ,updateNum)
                                             VALUES
                                                   (@SuiteKey
                                                   ,@SuiteSecret
                                                   ,@SuiteTicket
                                                   ,@Token
                                                   ,@EncodingAESKey
                                                   ,@TimeStamp
                                                   ,@SuiteAccessToken
                                                   ,@SuiteTokenExpires
                                                   ,@lastupatetime
                                                   ,@updateNum) ";
            SqlParameter[] param = {
                                       new SqlParameter("@SuiteKey",SuiteKey),
                                       new SqlParameter("@SuiteSecret",SuiteSecret),
                                       new SqlParameter("@SuiteTicket",SuiteTicket),
                                       new SqlParameter("@Token",Token),
                                       new SqlParameter("@EncodingAESKey",EncodingAESKey),
                                       new SqlParameter("@TimeStamp",TimeStamp),
                                       new SqlParameter("@SuiteAccessToken",SuiteAccessToken),
                                       new SqlParameter("@SuiteTokenExpires",SuiteTokenExpires),
                                       new SqlParameter("@LastUpdateTime",Lastupatetime),
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
            string sql = @"UPDATE IsvSuiteKeyInfoDingding
                                   SET suiteTicket = @suiteTicket
                                      ,TimeStamp = @TimeStamp
                                      ,LastUpdateTime = @LastUpdateTime
                                      ,suiteAccessToken=@suiteAccessToken
                                      ,SuiteTokenExpires=@SuiteTokenExpires
                                      ,updateNum =updateNum+1
                                  WHERE SuiteKey=@SuiteKey ";// 

            SqlParameter[] param = {
                                       new SqlParameter("@SuiteKey",SuiteKey),                                     
                                       new SqlParameter("@SuiteTicket",SuiteTicket),                                    
                                       new SqlParameter("@TimeStamp",TimeStamp),
                                       new SqlParameter("@SuiteAccessToken",SuiteAccessToken),
                                       new SqlParameter("@SuiteTokenExpires",SuiteTokenExpires),
                                       new SqlParameter("@LastUpdateTime",Lastupatetime),
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
        public DataSet GetSuiteTicketBySuiteId(string suiteKey)
        {
            string sql = "select top 1  * from IsvSuiteKeyInfoDingding where suiteKey='" + suiteKey + "' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

    }
}
