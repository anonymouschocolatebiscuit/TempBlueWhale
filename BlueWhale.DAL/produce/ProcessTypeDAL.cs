using BlueWhale.DBUtility;
using System;
using System.Data.SqlClient;
using System.Data;

namespace BlueWhale.DAL.produce
{
    public class ProcessTypeDAL
    {
        public ProcessTypeDAL()
        {
        }

        #region Member Field
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

        #region  Member Method
        /// <summary>
        /// isExistsNamesAdd
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExistsNamesAdd(int shopId, string names)
        {
            bool flag = false;

            string sql = "SELECT * FROM processType WHERE names='" + names + "' AND shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// IsExistsNamesEdit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExistsNamesEdit(int id, int shopId, string names)
        {
            bool flag = false;

            string sql = "SELECT * FROM processType WHERE names='" + names + "' AND id<>'" + id + "' AND shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// CheckBeUsed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckBeUsed(int id)
        {
            bool flag = false;

            string sql = "SELECT * FROM processType WHERE typeId='" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        #region GetIdByName
        /// <summary>
        /// GetIdByName
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId, string names)
        {
            int id = 0;
            string sql = "SELECT * FROM processType WHERE names='" + names + "' AND shopId='" + shopId + "'  ";

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }


            return id;
        }
        #endregion
        #endregion

        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "INSERT INTO processType(shopId,names,seq) VALUES('" + ShopId + "','" + Names + "','" + Seq + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = "";

            if (!this.IsExistsNamesEdit(Id, ShopId, Names))
            {
                sql = "UPDATE processType SET ShopId='" + ShopId + "',Names='" + Names + "',Seq='" + Seq + "' where Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " DELETE FROM processType WHERE id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }
        #endregion

        /// <summary>
        /// GetList
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            string sql = @"SELECT a.*,isnull(num,0) num
                            FROM processType a
                            LEFT JOIN
                            (
                            SELECT typeId,count(*) num
                            FROM processList
                            GROUP BY typeId
                            ) b
                            ON a.id=b.typeId ";

            if (strWhere != "")
            {
                sql += " WHERE " + strWhere;
            }

            sql += "  ORDER BY seq ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql.ToString(), null);
        }

        #region GetModelById
        /// <summary>
        /// GetModelById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetModelById(int Id)
        {
            string sql = "SELECT * FROM processType WHERE Id='" + Id + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion
    }
}
