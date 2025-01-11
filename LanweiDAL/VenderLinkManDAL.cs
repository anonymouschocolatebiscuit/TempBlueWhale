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
    public class VenderLinkManDAL
    {
        public VenderLinkManDAL()
        {

        }
        #region 成员字段

    

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        /// <summary>
        /// 供应商编号
        /// </summary>
        private int pId;
        public int PId
        {
            get { return pId; }
            set { pId = value; }
        }


     
        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string qq;
        public string QQ
        {
            get { return qq; }
            set { qq = value; }
        }

        private int moren;
        public int Moren
        {
            get { return moren; }
            set { moren = value; }
        }


      

        #endregion


        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNames(int pId,string names)
        {
            bool flag = false;

            string sql = "select * from venderLinkMan where pId='" + pId + "' and names='" + names + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

      
        #region 获取所有联系人------通过Code 供应商编号

        /// <summary>
        /// 获取所有联系人------通过Code 供应商编号
        /// </summary>
        /// <returns></returns>
        public DataSet GetMolderByPId()
        {
            string sql = "select * from venderLinkMan where pId='"+PId+"' order by id ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 新增联系人信息
        /// <summary>
        /// 新增联系人信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "";
            if (Moren == 1)
            {
                sql += " update venderLinkMan set moren=0 where pid='"+PId+"' ";
            }
            sql += " insert into venderLinkMan(pid,names,phone,tel,qq,moren) values('" + PId + "','" + Names + "','" + Phone + "','" + Tel + "','" + QQ + "','" + Moren + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 修改联系人信息
        /// <summary>
        /// 修改联系人信息
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = "";
            if (Moren == 1)
            {
                sql += " update venderLinkMan set moren=0  where pid='" + PId + "' ";
            }

             sql += " update venderLinkMan set Names='" + Names + "',Phone='" + Phone + "',Tel='" + Tel + "',QQ='" + QQ + "',Moren='" + Moren + "' where Id='" + Id + "'";
            
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion


        #region 删除联系人信息
        /// <summary>
        /// 删除联系人信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from venderLinkMan where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

    }
}
