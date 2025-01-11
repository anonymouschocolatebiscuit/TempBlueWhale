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
    public class IsvOAuthCorpInfoQYDAL
    {
        public IsvOAuthCorpInfoQYDAL()
        {

        }

        #region 成员字段

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string access_token;
        public string Access_token
        {
            get { return access_token; }
            set { access_token = value; }
        }

        private int expires_in;
        public int Expires_in
        {
            get { return expires_in; }
            set { expires_in = value; }
        }

        private string permanent_code;
        public string Permanent_code
        {
            get { return permanent_code; }
            set { permanent_code = value; }
        }

        private string corpid;
        public string Corpid
        {
            get { return corpid; }
            set { corpid = value; }
        }

        private string corp_name;
        public string Corp_name
        {
            get { return corp_name; }
            set { corp_name = value; }
        }

        private string corp_type;
        public string Corp_type
        {
            get { return corp_type; }
            set { corp_type = value; }
        }

        private string corp_round_logo_url;
        public string Corp_round_logo_url
        {
            get { return corp_round_logo_url; }
            set { corp_round_logo_url = value; }
        }

        private string corp_square_logo_url;
        public string Corp_square_logo_url
        {
            get { return corp_square_logo_url; }
            set { corp_square_logo_url = value; }
        }

        private int corp_user_max;
        public int Corp_user_max
        {
            get { return corp_user_max; }
            set { corp_user_max = value; }
        }

        private int corp_agent_max;
        public int Corp_agent_max
        {
            get { return corp_agent_max; }
            set { corp_agent_max = value; }
        }

        private string corp_full_name;
        public string Corp_full_name
        {
            get { return corp_full_name; }
            set { corp_full_name = value; }
        }

        private DateTime verified_end_time;
        public DateTime Verified_end_time
        {
            get { return verified_end_time; }
            set { verified_end_time = value; }
        }

        private int subject_type;
        public int Subject_type
        {
            get { return subject_type; }
            set { subject_type = value; }
        }

        private string corp_wxqrcode;
        public string Corp_wxqrcode
        {
            get { return corp_wxqrcode; }
            set { corp_wxqrcode = value; }
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

        private DateTime lastUpdateTime;
        public DateTime LastUpdateTime
        {
            get { return lastUpdateTime; }
            set { lastUpdateTime = value; }
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

        private string databaseURL;
        public string DatabaseURL
        {
            get { return databaseURL; }
            set { databaseURL = value; }
        }

        private string databaseName;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string mobile;
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
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
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(string corpid)
        {
            bool flag = false;

            string sql = "select * from IsvOAuthCorpInfoQY where corpid='" + corpid + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 编号是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id, string corpid)
        {
            bool flag = false;

            string sql = "select * from IsvOAuthCorpInfoQY where corpid='" + corpid + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(string name)
        {
            bool flag = false;

            string sql = "select * from IsvOAuthCorpInfoQY where name='" + name + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 名称是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int id, string name)
        {
            bool flag = false;

            string sql = "select * from IsvOAuthCorpInfoQY where name='" + name + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// 判断是否已经存在corpid，能否删除？
        /// </summary>
        /// <param name="corpid"></param>
        /// <returns></returns>
        public bool CheckCorpid(string corpid)
        {
            bool flag = false;

            string sql = " select * from IsvOAuthCorpInfoQY where corpid='" + corpid + "' ";

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
            string sql = "select * from IsvOAuthCorpInfoQY order by id ";

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
            string sql = "select * from IsvOAuthCorpInfoQY where corpid='" + corpid + "' order by id ";

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
            string sql = @" if not exists(select * from IsvOAuthCorpInfoQY where corpId=@corpId ) INSERT INTO IsvOAuthCorpInfoQY
                                           (access_token
                                           ,expires_in
                                           ,permanent_code
                                           ,corpid
                                           ,corp_name
                                           ,corp_type
                                           ,corp_round_logo_url
                                           ,corp_square_logo_url
                                           ,corp_user_max
                                           ,corp_agent_max
                                           ,corp_full_name
                                           ,verified_end_time
                                           ,subject_type
                                           ,corp_wxqrcode
                                           ,makeDate
                                           ,dateStart
                                           ,lastUpdateTime
                                           ,dateEnd
                                           ,flag
                                           ,bizId
                                           ,databaseName
                                           ,databaseURL
                                           ,email
                                           ,mobile
                                           ,userid) 
                                    VALUES
                                           (@access_token
                                           ,@expires_in
                                           ,@permanent_code
                                           ,@corpid
                                           ,@corp_name
                                           ,@corp_type
                                           ,@corp_round_logo_url
                                           ,@corp_square_logo_url
                                           ,@corp_user_max
                                           ,@corp_agent_max
                                           ,@corp_full_name
                                           ,@verified_end_time
                                           ,@subject_type
                                           ,@corp_wxqrcode
                                           ,@makeDate
                                           ,@dateStart
                                           ,@lastUpdateTime
                                           ,@dateEnd
                                           ,@flag
                                           ,@bizId
                                           ,@databaseName
                                           ,@databaseURL
                                           ,@email
                                           ,@mobile
                                           ,@userid) ";
            SqlParameter[] param = {
                                       new SqlParameter("@access_token",access_token),
                                       new SqlParameter("@expires_in",expires_in),
                                       new SqlParameter("@permanent_code",permanent_code),
                                       new SqlParameter("@corpid",corpid),
                                       new SqlParameter("@corp_name",corp_name),
                                       new SqlParameter("@corp_type",corp_type),
                                       new SqlParameter("@corp_round_logo_url",corp_round_logo_url),
                                       new SqlParameter("@corp_square_logo_url",corp_square_logo_url),
                                       new SqlParameter("@corp_user_max",corp_user_max),
                                       new SqlParameter("@corp_agent_max",corp_agent_max),
                                       new SqlParameter("@corp_full_name",corp_full_name),
                                       new SqlParameter("@verified_end_time",verified_end_time),
                                       new SqlParameter("@subject_type",subject_type),
                                       new SqlParameter("@corp_wxqrcode",corp_wxqrcode),
                                       new SqlParameter("@makeDate",makeDate),
                                       new SqlParameter("@dateStart",dateStart),
                                       new SqlParameter("@lastUpdateTime",lastUpdateTime),
                                       new SqlParameter("@dateEnd",dateEnd),
                                       new SqlParameter("@flag",flag),
                                       new SqlParameter("@bizId",bizId),
                                       new SqlParameter("@databaseName",databaseName),
                                       new SqlParameter("@databaseURL",databaseURL),
                                       new SqlParameter("@email",email),
                                       new SqlParameter("@mobile",mobile),
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
        public int AddGent(string corpid, int agentid, string name, string round_logo_url, string square_logo_url, int appId, string api_group)
        {
            string sql = " insert into IsvOAuthAgentInfoQY(corpid,agentid,name,round_logo_url,square_logo_url,appId, api_group)";
            sql += " values(@corpid,@agentid,@name,@round_logo_url,@square_logo_url,@appId, @api_group) ";

            SqlParameter[] param = {
                                       new SqlParameter("@corpid",corpid),
                                       new SqlParameter("@agentid",agentid),
                                       new SqlParameter("@name",name),
                                       new SqlParameter("@round_logo_url",round_logo_url),
                                       new SqlParameter("@square_logo_url",square_logo_url),
                                       new SqlParameter("@appId",appId),
                                       new SqlParameter("@api_group",api_group)

                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion


        #region 新增授权通讯录部门信息
        /// <summary>
        /// 新增授权通讯录部门信息
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="parentid"></param>
        /// <param name="writable"></param>
        /// <returns></returns>
        public int AddDept(string corpid, int id, string name, int parentid, int orderNum)
        {
            string sql = " insert into IsvOAuthDeptInfoQY(corpid,id,name,parentid,orderNum)";
            sql += " values(@corpid,@id,@name,@parentid,@orderNum) ";

            SqlParameter[] param = {
                                       new SqlParameter("@corpid",corpid),
                                       new SqlParameter("@id",id),
                                       new SqlParameter("@name",name),
                                       new SqlParameter("@parentid",parentid),
                                       new SqlParameter("@orderNum",orderNum)

                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion


        #region 新增部门人员信息
        /// <summary>
        /// 新增部门人员信息
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="postion"></param>
        /// <param name="mobile"></param>
        /// <param name="gender"></param>
        /// <param name="email"></param>
        /// <param name="weixinid"></param>
        /// <param name="avatar"></param>
        /// <param name="status"></param>
        /// <param name="extattr"></param>
        /// <returns></returns>
        public int AddMember(string corpid, string userid, string name, string department, string postion,
            string mobile,string gender,string email,string weixinid,string avatar,string status,string extattr)
        {
            string sql = " insert into IsvOAuthMemberInfoQY(corpid,userid,name,department,postion,mobile,gender,email,weixinid,avatar,status,extattr,makeDate)";
            sql += " values(@corpid,@userid,@name,@department,@postion,@mobile,@gender,@email,@weixinid,@avatar,@status,@extattr,getdate()) ";

            SqlParameter[] param = {
                                       new SqlParameter("@corpid",corpid),
                                       new SqlParameter("@userid",userid),
                                       new SqlParameter("@name",name),
                                       new SqlParameter("@department",department),
                                       new SqlParameter("@postion",postion),
                                       new SqlParameter("@mobile",mobile),
                                       new SqlParameter("@gender",gender),
                                       new SqlParameter("@email",email),
                                       new SqlParameter("@weixinid",weixinid),
                                       new SqlParameter("@avatar",avatar),
                                       new SqlParameter("@status",status),
                                       new SqlParameter("@extattr",extattr)

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
            //string sql="";
            //if (!this.isExistsCodeEdit(Id, Code) && !this.isExistsNamesEdit(Id, Names))
            //{
            //    sql = "update account set Names='" + Names + "',Code='" + Code + "',YueDate='" + YueDate + "',YuePrice='" + YuePrice + "',Types='" + Types + "' where Id='" + Id + "'";

            //    return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            //}
            //else
            //{
                return 0;
            //}
                

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
            string sql = "delete from IsvOAuthCorpInfoQY where corpId='" + corpid + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 删除授权企业部门信息
        /// <summary>
        /// 删除授权企业部门信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteDept(string corpid)
        {
            string sql = "delete from IsvOAuthDeptInfoQY where corpId='" + corpid + "'";
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
            string sql = "delete from IsvOAuthAgentInfoQY where corpId='" + corpid + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 删除授权企业成员信息
        /// <summary>
        /// 删除授权企业成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteMember(string corpid)
        {
            string sql = "delete from IsvOAuthMemberInfoQY where corpId='" + corpid + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

    }
}
