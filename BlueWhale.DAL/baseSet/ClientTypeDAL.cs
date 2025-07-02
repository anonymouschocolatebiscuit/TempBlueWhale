using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL.BaseSet
{
    public class ClientTypeDAL
    {
        public ClientTypeDAL()
        {
        }

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

        public bool IsExistsNamesAdd(int shopId, string names)
        {
            bool flag = false;

            string sql = "SELECT * FROM clientType WITH (NOLOCK) WHERE names='" + names + "' AND shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        public bool IsExistsNamesEdit(int id, int shopId, string names)
        {
            bool flag = false;

            string sql = "SELECT * FROM clientType WITH (NOLOCK) WHERE names='" + names + "' AND id<>'" + id + "' AND shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM viewClientType WITH (NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }

            strSql.Append(" ORDER BY flag  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }
        public int Add()
        {
            string sql = "INSERT INTO clientType(shopId,names,flag) VALUES('" + ShopId + "','" + Names + "','" + Flag + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int Update()
        {
            if (!this.IsExistsNamesEdit(Id, ShopId, Names))
            {
                string sql = "UPDATE clientType WITH (ROWLOCK) SET ShopId='" + ShopId + "',Names='" + Names + "',Flag='" + Flag + "' WHERE Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int Id)
        {
            string sql = "DELETE FROM clientType WHERE id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int GetIdByName(int shopId, string names)
        {
            int id = 0;
            string sql = "select * from clientType where names='" + names + "' and shopId='" + shopId + "' ";

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }
            return id;
        }
    }
}
