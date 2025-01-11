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
    public class CangkuDAL
    {
        public CangkuDAL()
        {

        }
        #region 成员字段

    

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }


      

        #endregion


        #region  成员方法

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(int shopId,string code)
        {
            bool flag = false;

            string sql = "select * from cangku where code='"+code+"' and shopId='"+shopId+"' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr,CommandType.Text,sql,null);
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
        public bool isExistsCodeEdit(int id,int shopId,string code)
        {
            bool flag = false;

            string sql = "select * from cangku where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";

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
        public bool isExistsNamesAdd(int shopId,string names)
        {
            bool flag = false;

            string sql = "select * from cangku where names='" + names + "' and shopId='" + shopId + "'  ";

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
        public bool isExistsNamesEdit(int id,int shopId, string names)
        {
            bool flag = false;

            string sql = "select * from cangku where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

      
      


        #endregion

        #region 获取所有信息

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList()
        {
            string sql = "select * from cangku order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,code+' '+names CodeName ");
            strSql.Append(" FROM cangku ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by code  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }


        #endregion


        #region 获取所有信息

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList(int shopId)
        {
            string sql = "select * from cangku where shopId='"+shopId+"' order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into cangku(shopId,code,names,flag) values('"+ShopId+"','" + Code + "','" + Names + "',1)";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 修改一条信息
        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql="";

            if (!this.isExistsCodeEdit(Id,ShopId, Code) && !this.isExistsNamesEdit(Id,ShopId, Names))
            {
                sql = "update cangku set Names='" + Names + "',code='" + Code + "' where Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
            


        }
        #endregion

        

        #region 修改状态
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateFlag(int Id)
        {
            string sqls = "if exists(select * from cangku where id='" + Id + "' and flag=1) ";
            sqls += " update cangku set flag=0 where id='" + Id + "'";

            sqls += " else update cangku set flag=1 where id='" + Id + "'";
            
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqls, null);

        }
        #endregion

        #region 删除一条信息
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from cangku where id='" + Id + "' and flag=0 ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 通过名称获取ID
        /// <summary>
        /// 通过名称获取ID
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId,string names)
        {
            int id = 0;
            string sql = "select * from cangku where names='" + names + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"].ToString());
            }

            reader.Close();

            return id;
        }
        #endregion
    }
}
