using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanwei.Weixin.DBUtility;
using System.Data;
using System.Data.SqlClient;


namespace Lanwei.Weixin.DAL
{
    public class NewsDAL
    {

        public NewsDAL()
        {

        }

        #region 成员属性

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

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private string contents;
        public string Contents
        {
            get { return contents; }
            set { contents = value; }
        }


        #endregion


        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="events"></param>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int Add()
        {
            int temp = 0;
            string sql = "insert into newsList(shopId,title,typeName,contents,makeDate) values('"+ShopId+"','" + Title + "','" + TypeName + "','" + Contents + "',getdate()) ";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion


        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="events"></param>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int Update()
        {
            int temp = 0;
            string sql = " update newsList set title='"+Title+"',typeName='"+TypeName+"',contents='"+Contents+"' where id='"+Id+"' ";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion



        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int temp = 0;
            string sql = "delete from  newsList where id='" + id + "' ";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion


        #region 删除信息-----------批量
        /// <summary>
        /// 删除信息-----------批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string idString)
        {
            int temp = 0;
            string sql = "delete from  newsList where id in(" + idString + ")";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion


        #region 查询新闻信息
        /// <summary>
        /// 查询新闻信息
        /// </summary>
        /// <param name="events"></param>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataSet GetQuestionInfo(int shopId,string keys, DateTime start, DateTime end,string flag)
        {

            string sql = "select * from  newsList where convert(varchar(100),makeDate,23) between  @start  and  @end and shopId='"+shopId+"' ";

            if (flag != "全部")
            {
                sql += " and typeName = '" + flag + "' ";
            }

          
            sql += " order by id desc ";
            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region 查询查询新闻
        /// <summary>
        /// 查询查询新闻
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public DataSet GetNewsList(string keys ,string typeName)
        {

            string sql = "select * from  newsList where 1=1  ";

            if (keys != "")
            {
                sql += " and title like '%" + keys + "%' ";
            }


            if (typeName != "全部")
            {
                sql += " and typeName='" + typeName + "' ";
            }


            sql += " order by id desc ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *   ");
            strSql.Append(" FROM newsList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id desc ");


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

      
        #region 查询查询新闻
        /// <summary>
        /// 查询查询新闻
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public DataSet GetNewsList(string typeName)
        {
             
            string sql = "select * from  newsList where 1=1  ";

            if (typeName != "全部")
            {
                sql += " and typeName='" + typeName + "' ";
            }
         

            sql += " order by id desc ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion


        #region 查询查询新闻
        /// <summary>
        /// 查询查询新闻
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public DataSet GetNewsList(string typeName,int topNum)
        {

            string sql = "select top "+topNum.ToString()+" * from  newsList where typeName='" + typeName + "'";


            sql += " order by id desc ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion


        #region 查询查询新闻
        /// <summary>
        /// 查询查询新闻
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public DataSet GetNewsList(int id)
        {

            string sql = "select * from  newsList where id='" + id + "'";


            sql += " order by id desc ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion


        #region 更新新闻阅读次数

        public int UpdateHot(int id)
        {
            int temp = 0;
            string sql = "update newsList set hot=hot+1 where id='"+id+"' ";

            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }

        #endregion

    }
}
