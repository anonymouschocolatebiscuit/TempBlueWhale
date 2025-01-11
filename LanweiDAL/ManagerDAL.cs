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
    public class ManagerDAL
    {
        public ManagerDAL()
        {

        }

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

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private int corpId;
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
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

        private string openId;
        public string OpenId
        {
            get { return openId; }
            set { openId = value; }
        }


        #endregion



        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select *  from users order by makeDate desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取成员-------通过电话号码

        /// <summary>
        /// 获取成员-------通过电话号码
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByPhone()
        {
            string sql = "select * from users where phone='" + Phone + "' ";

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

            string sql = @"insert into users(CorpId,names,deptId,roleId,tel,phone,email,qq,address,brithDay,comeDate,pwd,pwds,makeDate,flag,openId) 
                      values(@CorpId,@names,@deptId,@roleId,@tel,@phone,@email,@qq,@address,@brithDay,@comeDate,@pwd,@pwds,@makeDate,@flag,'')";
            SqlParameter[] param = {
                                       new SqlParameter("@CorpId",CorpId),
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


        

        #region 修改客户密码信息
        /// <summary>
        /// 修改客户密码信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdateMemberPwd(int Id, string pwd,string pwds)
        {
            string sql = "update users set pwd='" + pwd + "',pwds='" + pwds + "' where Id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from users where id='" + Id + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        


      
        //以下是微信部分功能

        #region 判断用户是否绑定

        /// <summary>
        /// 判断用户是否绑定
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool CheckWeixinBind(string openId)
        {
            bool isBind = false;

            string sql = "select id from users where openId='" + openId + "'";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion


        #region 判断用户手机是否存在------微信授权的时候

        /// <summary>
        /// 判断用户手机是否存在------微信授权的时候
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckPhonePwd(string phone, string pwd)
        {
            bool isBind = false;

            string sql = "select id from users where pwd='" + pwd + "' and phone='" + phone + "' ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion




        #region 绑定用户微信--------微信授权

        /// <summary>
        /// 绑定用户微信--------微信授权
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public int WeixinBindByPhonePwd(string openId, string phone, string pwd)
        {


            string sql = "update users set openId='" + openId + "' where phone='" + phone + "' and pwd='" + pwd + "' ";


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }



        #endregion


        #region 绑定用户微信--------取消微信授权

        /// <summary>
        /// 绑定用户微信--------取消微信授权
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public int WeixinBindByPhonePwdCancel(string openId, string phone)
        {


            string sql = "update users set openId='' where phone='" + phone + "' and openId='" + openId + "' ";


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }



        #endregion


        #region 通过微信OpenId获取用户信息

        /// <summary>
        /// 通过微信OpenId获取客户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public DataSet GetUserByOpenId(string openId)
        {

            string sql = "select * from  users where openId='" + openId + "' ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);



        }



        #endregion



        //以下网站部分功能

        #region 判断用户手机是否存在

        /// <summary>
        /// 判断用户手机是否存在
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckPhoneReg(string phone)
        {
            bool isBind = false;

            string sql = "select id from corpDatabaseConnString where phone='" + phone + "'  ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion


    }
}
