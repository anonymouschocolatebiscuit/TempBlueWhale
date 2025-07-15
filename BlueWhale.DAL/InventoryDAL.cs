using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class InventoryDAL
    {
        public InventoryDAL()
        {

        }

        #region Member Fields

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

        #region  Member Methods

        /// <summary>
        /// is exists code--When Add
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from cangku where code='" + code + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// is exists code--When Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id, int shopId, string code)
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
        /// is name exists
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(int shopId, string names)
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
        /// is name exists--When Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int id, int shopId, string names)
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

        #region GetData

        /// <summary>
        /// Get All Info
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList()
        {
            string sql = "select * from cangku order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// Get Data List
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

        #region GetALLModelList

        /// <summary>
        /// Get ALL Model List
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList(int shopId)
        {
            string sql = "select * from cangku where shopId='" + shopId + "' order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add

        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into cangku(shopId,code,names,flag) values('" + ShopId + "','" + Code + "','" + Names + "',1)";

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

            if (!this.isExistsCodeEdit(Id, ShopId, Code) && !this.isExistsNamesEdit(Id, ShopId, Names))
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

        #region UpdateFlag

        /// <summary>
        /// Update Flag
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

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from cangku where id='" + Id + "' and flag=0 ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetIdByName

        /// <summary>
        /// Get Id By Name
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId, string names)
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