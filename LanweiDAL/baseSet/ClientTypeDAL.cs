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
    public class ClientTypeDAL
    {
        public ClientTypeDAL()
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

        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
      

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

     
      

        #endregion


        #region  成员方法

     
        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(int shopId,string names)
        {
            bool flag = false;

            string sql = "select * from clientType where names='" + names + "' and shopId='"+shopId+"' ";

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

            string sql = "select * from clientType where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

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
            string sql = "select * from viewClientType order by flag desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM viewClientType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by flag  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #endregion

        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into clientType(shopId,names,flag) values('" + ShopId + "','" + Names + "','" + Flag + "')";

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

            if (!this.isExistsNamesEdit(Id,ShopId, Names))
            {
                sql = "update clientType set ShopId='" + ShopId + "',Names='" + Names + "',Flag='" + Flag + "' where Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
            


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
            string sql = "delete from clientType where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 通过名称获取编号

        /// <summary>
        /// 通过名称获取ID
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId,string names)
        {
            int id = 0;
            string sql = "select * from clientType where names='" + names + "' and shopId='"+shopId+"' ";

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }


            return id;
        }

        #endregion



        #region 删除--客户类别折扣

        /// <summary>
        /// 删除--客户类别折扣
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public int DeleteClientTypeDis(int typeId)
        {
            string sql = " delete from clientTypeDis where typeId='" + typeId + "' ";


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }


        #endregion

        #region 设置客户类别折扣

        /// <summary>
        /// 设置客户类别折扣
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="dis"></param>
        /// <returns></returns>
        public int SetClientTypeDis(int typeId, decimal dis)
        {
            string sql = " delete from clientTypeDis where typeId='" + typeId + "' ";
            sql += " insert into clientTypeDis(typeId,dis) values('" + typeId + "','" + dis + "') ";



            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }


        /// <summary>
        /// 设置客户类别折扣
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="dis"></param>
        /// <returns></returns>
        public int UpdateClientTypeDis(int typeId, decimal dis)
        {



            string sql = " update clientType set dis='"+dis+"' where id='" + typeId + "' ";
         


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }


        #endregion

    }
}
