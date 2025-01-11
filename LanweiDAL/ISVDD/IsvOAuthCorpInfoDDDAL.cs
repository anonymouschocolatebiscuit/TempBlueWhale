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
    public class IsvOAuthCorpInfoDDDAL
    {
        public IsvOAuthCorpInfoDDDAL()
        {

        }

        #region 成员字段

        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string suiteKey;
        public string SuiteKey
        {
            get { return suiteKey; }
            set { suiteKey = value; }
        }

        private string tmpAuthCode;
        public string TmpAuthCode
        {
            get { return tmpAuthCode; }
            set { tmpAuthCode = value; }
        }

        private string permanentCode;
        public string PermanentCode
        {
            get { return permanentCode; }
            set { permanentCode = value; }
        }

        private string corpid;
        public string Corpid
        {
            get { return corpid; }
            set { corpid = value; }
        }

        private string corpName;
        public string CorpName
        {
            get { return corpName; }
            set { corpName = value; }
        }

        private string mobile;
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string corpLogoUrl;
        public string CorpLogoUrl
        {
            get { return corpLogoUrl; }
            set { corpLogoUrl = value; }
        }

        private string accessToken;
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        private DateTime tokenExpires;
        public DateTime TokenExpires
        {
            get { return tokenExpires; }
            set { tokenExpires = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private DateTime dateStart;
        public DateTime DateStart
        {
            get { return dateStart; }
            set { dateStart = value; }
        }

        private DateTime dateEnd;
        public DateTime DateEnd
        {
            get { return dateEnd; }
            set { dateEnd = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private int bizId;
        public int BizId
        {
            get { return bizId; }
            set { bizId = value; }
        }

        private string databaseName;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private string databaseURL;
        public string DatabaseURL
        {
            get { return databaseURL; }
            set { databaseURL = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string userid;
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }



        #endregion


        #region 成员方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="suiteKey"></param>
        /// <returns></returns>
        public bool isExistsCorpIdAddSuiteKey(string corpid, string suiteKey)
        {
            bool flag = false;

            string sql = "select * from IsvSuiteCorpInfoDingding where corpid='" + corpid + "' and suiteKey='" + suiteKey + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

      

        #endregion



        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from IsvSuiteCorpInfoDingding order by id ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(string corpid)
        {
            string sql = "select * from IsvSuiteCorpInfoDingding where corpid='" + corpid + "' order by id ";

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
            string sql = @"  INSERT INTO IsvSuiteCorpInfoDingding
                                           (SuiteKey
                                           ,TmpAuthCode
                                           ,PermanentCode
                                           ,Corpid
                                           ,CorpName
                                           ,Mobile
                                           ,Name
                                           ,CorpLogoUrl
                                           ,suiteAccessToken
                                           ,TokenExpires
                                           ,makeDate
                                           ,dateStart
                                           ,dateEnd
                                           ,flag
                                           ,bizId
                                           ,databaseName
                                           ,databaseURL
                                           ,email
                                           ,phone
                                           ,userid) 
                                    VALUES
                                           (@SuiteKey
                                           ,@TmpAuthCode
                                           ,@PermanentCode
                                           ,@Corpid
                                           ,@CorpName
                                           ,@Mobile
                                           ,@Name
                                           ,@CorpLogoUrl
                                           ,@suiteAccessToken
                                           ,@TokenExpires
                                           ,@makeDate
                                           ,@dateStart
                                           ,@dateEnd
                                           ,@flag
                                           ,@bizId
                                           ,@databaseName
                                           ,@databaseURL
                                           ,@email
                                           ,@phone
                                           ,@userid) ";
            SqlParameter[] param = {
                                       new SqlParameter("@SuiteKey",SuiteKey),
                                       new SqlParameter("@TmpAuthCode",TmpAuthCode),
                                       new SqlParameter("@PermanentCode",PermanentCode),
                                       new SqlParameter("@Corpid",Corpid),
                                       new SqlParameter("@CorpName",CorpName),
                                       new SqlParameter("@Mobile",Mobile),
                                       new SqlParameter("@Name",Name),
                                       new SqlParameter("@CorpLogoUrl",CorpLogoUrl),
                                       new SqlParameter("@suiteAccessToken",AccessToken),
                                       new SqlParameter("@TokenExpires",TokenExpires),
                                       new SqlParameter("@makeDate",makeDate),
                                       new SqlParameter("@dateStart",dateStart),
                                       new SqlParameter("@dateEnd",dateEnd),
                                       new SqlParameter("@flag",flag),
                                       new SqlParameter("@bizId",bizId),
                                       new SqlParameter("@databaseName",databaseName),
                                       new SqlParameter("@databaseURL",databaseURL),
                                       new SqlParameter("@email",email),
                                       new SqlParameter("@Phone",Phone),
                                       new SqlParameter("@userid",userid)
                                                  
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region 新增授权应用信息
        /// <summary>
        /// 新增授权应用信息
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="agentid"></param>
        /// <param name="name"></param>
        /// <param name="round_logo_url"></param>
        /// <param name="square_logo_url"></param>
        /// <param name="appId"></param>
        /// <param name="api_group"></param>
        /// <returns></returns>
        public int AddGent(string suitKey, string corpid, int agentid, string agentName, string logoUrl, int appId, string description, int isClose)
        {
            string sql = " insert into IsvSuiteCorpAgentDingding(suiteKey,corpid,agentId,agentName,logoUrl,appId,description,isClose)";
            sql += " values(@suiteKey,@corpid,@agentId,@agentName,@logoUrl,@appId,@description,@isClose) ";

            SqlParameter[] param = {
                                       new SqlParameter("@suitKey",suitKey),
                                       new SqlParameter("@corpid",corpid),
                                       new SqlParameter("@agentid",agentid),
                                       new SqlParameter("@agentName",agentName),
                                       new SqlParameter("@logoUrl",logoUrl),
                                       new SqlParameter("@description",description),
                                       new SqlParameter("@appId",appId),
                                       new SqlParameter("@isClose",isClose)

                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion







        #region 生成数据库

        #region 数据库备份

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int DataBackup(string database, string path)
        {
            int num = 0;
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@dataBase", database),
                new SqlParameter("@path", path)
            };
            DataSet set = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "RestoreDatabase", parameterArray);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0][0].ToString() == path)
                {
                    num = 1;
                }
            }

            return num;
        }



        /// <summary>
        /// 数据库还原
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int DataRestore(string names, string path)
        {

            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@dataBase", names),
                new SqlParameter("@path", path)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.StoredProcedure, "RestoreDatabase", parameterArray); ;
        }



        /// <summary>
        /// 数据库备份判断是否成功？
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int DataBackupCom(string path)
        {
            int num = 0;
            string str = "select top 1 physical_device_name from msdb..backupmediafamily order by media_set_id desc";
            DataSet set = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, str, null);
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                if (row[0].ToString() == path)
                {
                    num = 1;
                }
            }
            return num;
        }



        #endregion

        #endregion

        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE IsvSuiteCorpInfoDingding
                             SET 
                               TmpAuthCode = @TmpAuthCode
                              ,PermanentCode = @PermanentCode
                              ,CorpName = @CorpName
                              ,Mobile = @Mobile
                              ,Name = @Name
                              ,CorpLogoUrl = @CorpLogoUrl
                              ,suiteAccessToken = @suiteAccessToken
                              ,TokenExpires = @TokenExpires
                              ,makeDate = @makeDate
                              ,dateStart = @dateStart
                              ,dateEnd = @dateEnd
                              ,flag = @flag
                              ,bizId = @bizId
                              ,databaseName = @databaseName
                              ,databaseURL = @databaseURL
                              ,email = @email
                              ,phone = @phone
                              ,userid = @userid

                            where suiteKey=@suiteKey and corpId=@corpId     ";


            SqlParameter[] param = {
                                       new SqlParameter("@SuiteKey",SuiteKey),
                                       new SqlParameter("@TmpAuthCode",TmpAuthCode),
                                       new SqlParameter("@PermanentCode",PermanentCode),
                                       new SqlParameter("@Corpid",Corpid),
                                       new SqlParameter("@CorpName",CorpName),
                                       new SqlParameter("@Mobile",Mobile),
                                       new SqlParameter("@Name",Name),
                                       new SqlParameter("@CorpLogoUrl",CorpLogoUrl),
                                       new SqlParameter("@AccessToken",AccessToken),
                                       new SqlParameter("@TokenExpires",TokenExpires),
                                       new SqlParameter("@makeDate",makeDate),
                                       new SqlParameter("@dateStart",dateStart),
                                       new SqlParameter("@dateEnd",dateEnd),
                                       new SqlParameter("@flag",flag),
                                       new SqlParameter("@bizId",bizId),
                                       new SqlParameter("@databaseName",databaseName),
                                       new SqlParameter("@databaseURL",databaseURL),
                                       new SqlParameter("@email",email),
                                       new SqlParameter("@phone",phone),
                                       new SqlParameter("@userid",userid)
                                                  
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
                

        }
        #endregion

        #region 删除授权企业信息
        /// <summary>
        /// 删除授权企业信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(string corpid)
        {
            string sql = "delete from IsvSuiteCorpInfoDingding where corpId='" + corpid + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        

        #region 删除授权企业应用信息
        /// <summary>
        /// 删除授权企业应用信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteAgent(string corpid)
        {
            string sql = "delete from IsvSuiteCorpAgentDingding where corpId='" + corpid + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        




    }
}
