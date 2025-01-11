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
    public class UserListDAL
    {
        public UserListDAL()
        {

        }
        #region 成员字段



        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }

        private int deptId;
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string qq;
        public string QQ
        {
            get { return qq; }
            set { qq = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private DateTime brithDay;
        public DateTime BrithDay
        {
            get { return brithDay; }
            set { brithDay = value; }
        }

        private DateTime comeDate;
        public DateTime ComeDate
        {
            get { return comeDate; }
            set { comeDate = value; }
        }

        private string pwd;
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        private string pwds;
        public string Pwds
        {
            get { return pwds; }
            set { pwds = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

      

        #endregion


        #region  成员方法

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsUserName(string UserName)
        {
            bool flag = false;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.VarChar) };

            string sql = "select * from viewUsers where loginName=@UserName ";

            parameters[0].Value = UserName;
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr,CommandType.Text,sql,parameters);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }


        #region  成员方法

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(string code)
        {
            bool flag = false;

            string sql = "select * from users where loginName='" + code + "' ";

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
        public bool isExistsCodeEdit(int id, string code)
        {
            bool flag = false;

            string sql = "select * from users where loginName='" + code + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

       




        #endregion




        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public bool isLoginValidate(string LoginName, string Pwd)
        {
            if (this.isExistsUserName(LoginName))
            {
                Users user = this.getByUserName(LoginName);
                if (Pwd.Equals(user.Pwd))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Users getByUserName(string UserName)
        {
            Users user = new Users();

            string sql = " select * from viewUsers where loginName=@loginName ";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@loginName", SqlDbType.VarChar) };
            parameters[0].Value = UserName;
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr,CommandType.Text,sql, parameters);
            while (reader.Read())
            {
                user.Id = reader.GetInt32(reader.GetOrdinal("ID"));

                user.CorpIdQY = reader.GetString(reader.GetOrdinal("CorpIdQY"));
                user.CorpSecretQY = reader.GetString(reader.GetOrdinal("CorpSecretQY"));
                
                user.ShopName = reader.GetString(reader.GetOrdinal("shopName"));
                //user.LoginName = reader.GetString(reader.GetOrdinal("LoginName"));
                user.DeptName = reader.GetString(reader.GetOrdinal("DeptName"));
                user.RoleName = reader.GetString(reader.GetOrdinal("roleName"));
                user.Pwd = reader.GetString(reader.GetOrdinal("pwd"));
                user.Pwds = reader.GetString(reader.GetOrdinal("pwds"));
                user.Names = reader.GetString(reader.GetOrdinal("names"));
                user.Phone = reader.GetString(reader.GetOrdinal("phone"));
                user.MakeDate = reader.GetDateTime(reader.GetOrdinal("makeDate"));
                user.Flag = reader.GetString(reader.GetOrdinal("Flag"));
              
            }
            reader.Close();
            return user;
        }


        #endregion

        #region 获取所有用户

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllUserList()
        {
            string sql = "select * from viewUsers order by loginName  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有用户

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllUserListByShopId(int shopId)
        {
            string sql = "select * from viewUsers where shopId='" + shopId + "' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有用户

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllUserList(int id)
        {
            string sql = "select * from viewUsers where id='"+id+"' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有用户

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="typeId">1 钉钉用户 2 微信用户 </param>
        /// <returns></returns>
        public DataSet GetAllUserList(string corpId,int typeId)
        {
            string sql = "select * from userList where corpId='" + corpId + "' and typeId='"+typeId+"' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }




        #endregion

        #region 新增用户信息
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @"insert into users(shopId,loginName,names,deptId,roleId,tel,phone,email,qq,address,brithDay,comeDate,pwd,pwds,makeDate,flag) 
                      values(@shopId,@loginName,@names,@deptId,@roleId,@tel,@phone,@email,@qq,@address,@brithDay,@comeDate,@pwd,@pwds,@makeDate,@flag)";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@LoginName",LoginName),
                                       new SqlParameter("@names",Names),
                                       new SqlParameter("@deptId",DeptId),
                                       new SqlParameter("@roleId",RoleId),
                                       new SqlParameter("@tel",Tel),
                                       new SqlParameter("@phone",Phone), 
                                       new SqlParameter("@email",Email),
                                       new SqlParameter("@qq",QQ),
                                       new SqlParameter("@address",Address),
                                       new SqlParameter("@pwd",Pwd),
                                       new SqlParameter("@Pwds",Pwds),
                                       new SqlParameter("@makeDate",MakeDate),
                                       new SqlParameter("@flag",Flag),
                                       new SqlParameter("@brithday",BrithDay),
                                       new SqlParameter("@comeDate",ComeDate)
                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);


          
        }

        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE users
                                   SET loginName = @loginName
                                      ,names = @names
                                      ,shopId = @shopId
                                      ,deptId = @deptId
                                      ,roleId = @roleId
                                      ,tel = @tel
                                      ,phone = @phone
                                      ,email = @email
                                      ,qq = @qq
                                      ,address = @address
                                      ,brithDay = @brithDay
                                      ,comeDate = @comeDate
                                      ,flag = @flag

                                 WHERE id=@id ";

            SqlParameter[] param = {
                                       
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@LoginName",LoginName),
                                       new SqlParameter("@names",Names),
                                       new SqlParameter("@deptId",DeptId),
                                       new SqlParameter("@roleId",RoleId),
                                       new SqlParameter("@tel",Tel),
                                       new SqlParameter("@phone",Phone), 
                                       new SqlParameter("@email",Email),
                                       new SqlParameter("@qq",QQ),
                                       new SqlParameter("@address",Address),
                                       new SqlParameter("@flag",Flag),
                                       new SqlParameter("@brithday",BrithDay),
                                       new SqlParameter("@comeDate",ComeDate),
                                        new SqlParameter("@Id",Id)

                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

           

        }
        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <param name="pwds"></param>
        /// <returns></returns>
        public int UpdatePwd(int Id, string pwd,string pwds)
        {
            string sql = "update users set pwd='" + pwd + "',pwds='"+pwds+"' where Id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 修改用户状态
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateFlag(int Id)
        {
            string sqls = "if exists(select * from users where id='"+Id+"' and flag=1) ";
            sqls += " update users set flag=0 where id='"+Id+"'";

            sqls += " else update users set flag=1 where id='"+Id+"'";

            //string sql = "update users set flag='" + flag + "' where Id='" + Id + "'";
            
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqls, null);

        }
        #endregion

        #region 删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from users where id='" + Id + "' and flag<>'启用'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion



        #region 根据员工编号权限获取页面
        /// <summary>
        /// 根据员工编号权限获取页面
        /// </summary>
        /// <param name="ParentTypeId"></param>
        /// <returns></returns>
        public List<PageInfo> GetPageListByUserId(int userId, int parentId)
        {
            string SQL_TYPE_NAME = "select * from  [usersPageList] where  userId='" + userId + "' and parentId='" + parentId + "'  order by pageId  ";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, SQL_TYPE_NAME, null))
            {
                List<PageInfo> list = new List<PageInfo>();
                while (read.Read())
                {
                    PageInfo s = new PageInfo();
                    s.Id = Convert.ToInt32(read["pageId"].ToString());
                    s.Url = read["url"].ToString();
                    s.Title = read["title"].ToString();
                    s.ParentId =Convert.ToInt32(read["parentId"].ToString());
                  
                    list.Add(s);
                }
                return list;
            }
        }
        #endregion

        #region 判断员工是否有页面或操作的权限
        /// <summary>
        /// 判断员工是否有页面或操作的权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckPowerByUserIdAndUrl(int id,string url)
        {
            bool yes = false;
            string sql = "select * from usersPageList where userId='" + id + "' and url='"+url+"' and url<>'' ";

            DataSet ds= SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                yes = true;
            }

            return yes;

        }
        #endregion

        #region 在插入权限前，先将原来的权限全部删除掉
        /// <summary>
        /// 在插入权限前，先将原来的权限全部删除掉
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int DeletePageListByUserId(int id)
        {
            string sql = "delete from usersPageList where userId='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 新增用户权限
        /// <summary>
        /// 新增用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="parentId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public int AddUsersPageList(int userId, int pageId,string url, string title, int parentId, int seq)
        {
            string sql = "insert into usersPageList(userId,pageId,url,title,parentId,seq) values(@userId,@pageId,@url,@title,@parentId,@seq)";
            SqlParameter[] param = {
                                       new SqlParameter("@userId",userId),
                                       new SqlParameter("@pageId",pageId),
                                       new SqlParameter("@url",url),
                                       new SqlParameter("@title",title),
                                       new SqlParameter("@parentId",parentId),
                                       new SqlParameter("@seq",seq)
                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text,sql, param);

        }
        #endregion

        #region 初始化密码
        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int ChangPassword(int id)
        {
            string sqlstr = "update users set pwd='123456',pwds='123456' where id='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqlstr, null);
        }

        #endregion



        #region 初始化权限表

        /// <summary>
        /// 初始化权限表
        /// </summary>
        /// <returns></returns>
        public int TruncateTablePageList()
        {
            string sql = "truncate table pageList ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql,null);
        }

        #endregion

        #region 新增插入所有权限到表
        /// <summary>
        /// 新增插入所有权限到表
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="parentId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public int AddPageList(int pageId, string url, string title, int parentId, int seq)
        {
            string sql = "insert into PageList(pageId,url,title,parentId,seq) values(@pageId,@url,@title,@parentId,@seq)";
            SqlParameter[] param = {
                                      
                                       new SqlParameter("@pageId",pageId),
                                       new SqlParameter("@url",url),
                                       new SqlParameter("@title",title),
                                       new SqlParameter("@parentId",parentId),
                                       new SqlParameter("@seq",seq)
                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion

        #region 查询所有页面
        /// <summary>
        /// 查询所有页面
        /// </summary>
        /// <param name="ParentTypeId"></param>
        /// <returns></returns>
        public List<PageInfo> GetAllPages(string ParentTypeId)
        {
            string SQL_TYPE_NAME = "select * from pageList where ParentId=" + ParentTypeId + " order by id ";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, SQL_TYPE_NAME, null))
            {
                List<PageInfo> list = new List<PageInfo>();
                while (read.Read())
                {
                    PageInfo s = new PageInfo();

                    s.Id = read.GetInt32(1);
                    s.Url = read.GetString(2);
                    s.Title = read.GetString(3);
                    s.ParentId = read.GetInt32(4);
                    s.Seq = read.GetInt32(5);
                    list.Add(s);
                }
                return list;
            }
        }
        #endregion


        #region 获取所有用户

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllGoodsList()
        {
            string sql = "select * from tempGoodsST ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllGoodsListAAA()
        {
            string sql = "select * from numTemp201501 ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllGoodsListBBB()
        {
            string sql = "select * from numTemp201502 ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }




        #endregion


        #region 新增插入所有权限到表
        /// <summary>
        /// 新增插入所有权限到表
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="parentId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public int AddGoodsList(string code, string barcode, string names, string spec, string num, string price)
        {
            string sql = "insert into numTemp201502(code,barcode,names,spec,num,price) values(@code,@barcode,@names,@spec,@num,@price)";
            SqlParameter[] param = {
                                      
                                       new SqlParameter("@code",code),
                                       new SqlParameter("@barcode",barcode),
                                       new SqlParameter("@names",names),
                                       new SqlParameter("@spec",spec),
                                       new SqlParameter("@num",num),
                                       new SqlParameter("@price",price)
                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion


        #region 通过dbId获取客户使用系统的数据库连接信息

        /// <summary>
        /// 通过dbId获取客户使用系统的数据库连接信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="SystemName"></param>
        /// <returns></returns>
        public string GetDatabaseURLByDBId(int dbId)
        {
            
            string url = "server=112.74.207.231,1433;database=LanweiERP_YY;User ID=sa;pwd=lanweisoft@163.com;Max Pool Size=512";


            string ConStr = "server=112.74.207.231,1433;database=LanweiWeb;User ID=sa;pwd=lanweisoft@163.com;Max Pool Size=512";

            string sql = "select * from  orderlist where id='" + dbId + "' ";

            DataSet ds = SQLHelper.SqlDataAdapter(ConStr, CommandType.Text, sql, null);

            if (ds.Tables[0].Rows.Count > 0)
            {
                url = ds.Tables[0].Rows[0]["databaseURL"].ToString();
            }


            return url;



        }



        #endregion


        #region 判断用户是否绑定

        /// <summary>
        /// 判断用户是否绑定
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool CheckWeixinBind(string openId)
        {
            bool isBind = false;

            string sql = "select id from users where openId='" + openId + "' ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion


        #region 获取成员-------通过微信号

        /// <summary>
        /// 获取成员-------通过微信号
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByWeixinOpenId(string openId)
        {
            string sql = " select * from users where  openId='" + openId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 修改用户绑定微信openId 情况
        /// <summary>
        /// 修改用户绑定微信openId 情况
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <param name="openId">绑定有值、解绑未空值</param>
        /// <returns></returns>
        public int UpdateUserOpenId(int Id,string openId)
        {
            string sqls = " update users set openId='"+openId+"' where id='" + Id + "' ";
          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqls, null);

        }
        #endregion


    }
}
