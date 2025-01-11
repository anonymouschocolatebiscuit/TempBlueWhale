using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class KaoqinDAL
    {
        public KaoqinDAL()
        {

        }
        #region 成员字段

    

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string locationX;
        public string LocationX
        {
            get { return locationX; }
            set { locationX = value; }
        }

        private string locationY;
        public string LocationY
        {
            get { return locationY; }
            set { locationY = value; }
        }

        private string distance;
        public string Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private string ip;
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        #endregion


        #region  成员方法

     
        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(string names)
        {
            bool flag = false;

            string sql = "select * from kaoqinList where names='" + names + "' ";

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
        public bool isExistsNamesEdit(int id, string names)
        {
            bool flag = false;

            string sql = "select * from kaoqinList where names='" + names + "' and id<>'" + id + "' ";

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
            string sql = "select * from viewKaoqinList order by id  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM viewKaoqinList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by makeDate desc  ");

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
            string sql = "insert into kaoqinList(typeName,userId,makeDate,location,locationX,locationY,Distance,remarks,ip)  ";
            sql += "values('"+TypeName+"','" + UserId + "','" + MakeDate + "','" + Location + "','" + LocationX + "','" + LocationY + "','" + Distance + "','" + Remarks + "','" + IP + "') ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
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
            string sql = "delete from kaoqinList where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

    }
}
