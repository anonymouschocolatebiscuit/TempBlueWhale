using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlueWhale.DAL
{
    public class LogsDAL
    {

        public LogsDAL()
        {

        }

        #region member properties

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

        private string events;
        public string Events
        {
            get { return events; }
            set { events = value; }
        }

        private string users;
        public string Users
        {
            get { return users; }
            set { users = value; }
        }

        private string ip;
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }


        #endregion

        #region Insert log information
        /// <summary>
        /// Insert log information
        /// </summary>
        /// <param name="events"></param>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int Add()
        {
            int temp = 0;
            string sql = "insert into logs(shopId,events,users,ip) values('" + ShopId + "','" + Events + "','" + Users + "','" + Ip + "') ";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion

        #region Delete log information
        /// <summary>
        /// Delete log information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string idString)
        {
            int temp = 0;
            string sql = "delete from  logs where id in(" + idString + ")";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion

        #region Delete log information
        /// <summary>
        /// Delete log information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int temp = 0;
            string sql = "delete from  logs where id='" + id + "' ";
            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }
        #endregion


        #region Search log information
        /// <summary>
        /// Search log information
        /// </summary>
        /// <param name="events"></param>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataSet GetLogsInfo(int shopId, string keys, DateTime start, DateTime end)
        {

            string sql = "select * from  logs where convert(varchar(100),date,23) between  @start  and  @end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (keys != "")
            {
                sql += " and (Users like '%" + keys + "%' or events like '%" + keys + "%' or ip like '%" + keys + "%' )";
            }


            sql += " order by id desc ";
            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

    }
}
