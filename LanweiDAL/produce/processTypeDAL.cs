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
    public class processTypeDAL
    {
        public processTypeDAL()
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

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

     
        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
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

            string sql = "select * from processType where names='" + names + "' and shopId='" + shopId + "' ";

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

            string sql = "select * from processType where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// 判断是否有被引用，能否删除？
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckBeUsed(int id)
        {
            bool flag = false;

            string sql = "select * from processType where typeId='" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }





        #region 通过名称获取编号

        /// <summary>
        /// 通过名称获取ID
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId,string names)
        {
            int id = 0;
            string sql = "select * from processType where names='" + names + "' and shopId='" + shopId + "'  ";

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr,CommandType.Text,sql,null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }

           
            return id;
        }

        #endregion




        #endregion

        

        

        




        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into processType(shopId,names,seq) values('" + ShopId + "','" + Names + "','" + Seq + "')";

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
                sql = "update processType set ShopId='" + ShopId + "',Names='" + Names + "',Seq='" + Seq + "' where Id='" + Id + "'";

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
            string sql = " delete from processType where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {

            string sql = @"select a.*,isnull(num,0) num
                            from processType a
                            left join
                            (
                            select typeId,count(*) num
                            from processList
                            group by typeId
                            ) b
                            on a.id=b.typeId ";

            if (strWhere != "")
            {
                sql += " where " + strWhere;
            }

            sql += "  order by seq ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql.ToString(), null);
        }

        


        #region 查询类别信息---通过编号
        /// <summary>
        /// 查询类别信息---通过编号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetModelById(int Id)
        {
            string sql = "select * from processType where Id='" + Id + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
