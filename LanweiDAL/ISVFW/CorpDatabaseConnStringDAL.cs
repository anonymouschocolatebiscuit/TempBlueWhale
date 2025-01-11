using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Common;//Please add references
namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_userweixin
	/// </summary>
	public partial class CorpDatabaseConnStringDAL
	{
        public CorpDatabaseConnStringDAL()
		{}

        #region 成员字段

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private int bizId;
        public int BizId
        {
            get { return bizId; }
            set { bizId = value; }
        }

        private string corpIdQY;
        public string CorpIdQY
        {
            get { return corpIdQY; }
            set { corpIdQY = value; }
        }

        private string corpIdDD;
        public string CorpIdDD
        {
            get { return corpIdDD; }
            set { corpIdDD = value; }
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

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
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


        #endregion

        #region  BasicMethod

        /// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("id", "CorpDatabaseConnString"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from CorpDatabaseConnString");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        #region 判断手机号是否已经注册

        /// <summary>
        /// 判断手机号是否已经注册
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>如果存在、返回true</returns>
        public bool isExists(string phone)
        {
            bool flag = false;

            string sql = "select * from CorpDatabaseConnString where phone='" + phone + "'  ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }



        #endregion


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
                                     //CorpDatabaseConnString
            strSql.Append("insert into CorpDatabaseConnString(");
			strSql.Append("phone,bizId,corpIdQY,corpIdDD,databaseURL,databaseName,makeDate,dateEnd,flag)");
			strSql.Append(" values (");
            strSql.Append("@phone,@bizId,@corpIdQY,@corpIdDD,@databaseURL,@databaseName,@makeDate,@dateEnd,@flag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
		                                    new SqlParameter("@phone", SqlDbType.VarChar,200),
                                            new SqlParameter("@bizId", SqlDbType.Int,4),
					                        new SqlParameter("@corpIdQY", SqlDbType.VarChar,100),
					                        new SqlParameter("@corpIdDD", SqlDbType.VarChar,100),
					                        new SqlParameter("@databaseURL", SqlDbType.VarChar,2000),
					                        new SqlParameter("@databaseName", SqlDbType.VarChar,1000),
                                            new SqlParameter("@makeDate", SqlDbType.DateTime),
					                        new SqlParameter("@dateEnd", SqlDbType.DateTime),
					                        new SqlParameter("@flag", SqlDbType.VarChar,200)
					
                                        };
			parameters[0].Value = Phone;
			parameters[1].Value =BizId;
			parameters[2].Value = CorpIdQY;
			parameters[3].Value = CorpIdDD;
			parameters[4].Value = DatabaseURL;
			parameters[5].Value = DatabaseName;
			parameters[6].Value = MakeDate;
			parameters[7].Value = DateEnd;
			parameters[8].Value = Flag;
		
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
	
        
        /// <summary>
		/// 更新使用期限
		/// </summary>
		public bool Update()
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update CorpDatabaseConnString set ");
			strSql.Append("dateEnd=@dateEnd,");
			strSql.Append("flag=@flag");
            strSql.Append(" where id=@id ");

			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),					
					new SqlParameter("@dateEnd", SqlDbType.DateTime),
					new SqlParameter("@flag", SqlDbType.VarChar,100),
					};

			parameters[0].Value =Id;
			parameters[1].Value = DateEnd;
            parameters[2].Value = Flag;
		

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// 绑定微信企业号corpId
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpIdQY"></param>
        /// <returns></returns>
        public bool UpdateCorpIdQY(int corpId,string corpIdQY)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CorpDatabaseConnString set ");
            strSql.Append("corpIdQY=@corpIdQY ");
         
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),					
					new SqlParameter("@corpIdQY", SqlDbType.VarChar,200)
					};

            parameters[0].Value = Id;
            parameters[1].Value = corpIdQY;


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 绑定阿里钉钉corpId
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpIdQY"></param>
        /// <returns></returns>
        public bool UpdateCorpIdDD(int corpId, string corpIdDD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CorpDatabaseConnString set ");
            strSql.Append("corpIdDD=@corpIdDD ");

            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),					
					new SqlParameter("@corpIdDD", SqlDbType.VarChar,200)
					};

            parameters[0].Value = Id;
            parameters[1].Value = corpIdDD;


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from CorpDatabaseConnString ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from CorpDatabaseConnString ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DataSet GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from CorpDatabaseConnString ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

           // return DbHelperSQL.Query(strSql.ToString());

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), parameters);

		}

        #region 获取成员数据库连接-------通过手机号、密码

        /// <summary>
        /// 获取成员数据库连接-------通过手机号、密码
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public DataSet GetDatabaseURLByPhonePwd(string phone,string pwd)
        {
            string sql = "select top 1 * from corpDatabaseConnString where id =(select top 1 corpId from users where phone=@phone and pwd=@pwd)  ";
            SqlParameter[] param = {
                                      new SqlParameter("@phone",phone),
                                      new SqlParameter("@pwd",pwd)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        #region 获取成员数据库连接-------通过微信企业号的corpId

        /// <summary>
        /// 获取成员数据库连接-------通过微信企业号的corpId
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public DataSet GetDatabaseURLByCorpIdQY(string corpId)
        {
            string sql = "select top 1 * from corpDatabaseConnString where corpIdQY ='" + corpId + "'  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取成员数据库连接-------通过阿里钉钉的corpId

        /// <summary>
        /// 获取成员数据库连接-------通过阿里钉钉的corpId
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public DataSet GetDatabaseURLByCorpIdDD(string corpId)
        {
            string sql = "select top 1 * from corpDatabaseConnString where corpIdDD ='" + corpId + "'  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

	
		#endregion  BasicMethod
		
	}
}

