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
    public class IsvReceiveDingdingDAL
    {
        public IsvReceiveDingdingDAL()
        {

        }

        #region 成员字段

        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string signature;
        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        private string timestamp;
        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        private string nonce;
        public string Nonce
        {
            get { return nonce; }
            set { nonce = value; }
        }

        private string encrypt;
        public string Encrypt
        {
            get { return encrypt; }
            set { encrypt = value; }
        }

        private string echoString;
        public string EchoString
        {
            get { return echoString; }
            set { echoString = value; }
        }

        private DateTime createTime;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }



        #endregion


        



        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from IsvReceiveDingding order by id ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        

        #region 新增成员信息
        /// <summary>
        /// 新增成员信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @"  INSERT INTO IsvReceiveDingding
                                           (Signature
           ,Timestamp
           ,Nonce
           ,Encrypt
           ,EchoString
           ,CreateTime) 
                                    VALUES
                                           (@Signature
           ,@Timestamp
           ,@Nonce
           ,@Encrypt
           ,@EchoString
           ,@CreateTime) ";
            SqlParameter[] param = {
                                       new SqlParameter("@Signature",Signature),
                                       new SqlParameter("@Timestamp",Timestamp),
                                       new SqlParameter("@Nonce",Nonce),
                                       new SqlParameter("@Encrypt",Encrypt),
                                       new SqlParameter("@EchoString",EchoString),
                                       new SqlParameter("@CreateTime",CreateTime)
                                                  
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


     
        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete from isvSuiteEventLogs where id='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region 新增推送日志
      
        /// <summary>
        /// 新增推送日志
        /// </summary>
        /// <param name="AuthCorpId"></param>
        /// <param name="EventType"></param>
        /// <param name="SuiteKey"></param>
        /// <param name="TimeStamp"></param>
        /// <param name="remarks"></param>
        /// <param name="CreateTime"></param>
        /// <returns></returns>
        public int AddSuiteEventLogs(string AuthCorpId, string EventType, string SuiteKey, string TimeStamp, string remarks, DateTime CreateTime)
        {
            string sql = @"insert into isvSuiteEventLogs(AuthCorpId,EventType,SuiteKey,TimeStamp,remarks,creattime)
                                       values(@AuthCorpId,@EventType,@SuiteKey,@TimeStamp,@remarks,@CreateTime)";

            SqlParameter[] param = {
                                       new SqlParameter("@AuthCorpId",AuthCorpId),
                                       new SqlParameter("@EventType",EventType),
                                       new SqlParameter("@SuiteKey",SuiteKey),
                                       new SqlParameter("@TimeStamp",TimeStamp),
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@CreateTime",CreateTime)
                                                  
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


    }
}
