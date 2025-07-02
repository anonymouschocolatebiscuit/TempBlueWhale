using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class VenderTypeDAL
    {
        public VenderTypeDAL() { }

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

        #region Member Methods

        /// <summary>
        /// Check if name exists (for add)
        /// </summary>
        public bool isExistsNamesAdd(int shopId, string names)
        {
            bool flag = false;
            string sql = "select * from venderType where names='" + names + "' and shopId='" + shopId + "' ";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);

            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Check if name exists (for edit)
        /// </summary>
        public bool isExistsNamesEdit(int id, int shopId, string names)
        {
            bool flag = false;
            string sql = "select * from venderType where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "' ";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);

            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        #endregion

        #region Get All Records

        /// <summary>
        /// Get all records
        /// </summary>
        public DataSet GetALLModelList()
        {
            string sql = "select * from venderType order by id";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// Get filtered data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM viewVenderType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by flag");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #endregion

        #region Add Record

        /// <summary>
        /// Add a new record
        /// </summary>
        public int Add()
        {
            string sql = "insert into venderType(shopId,names,flag) values('" + ShopId + "','" + Names + "','" + Flag + "')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update Record

        /// <summary>
        /// Update a record
        /// </summary>
        public int Update()
        {
            string sql = "";

            if (!this.isExistsNamesEdit(Id, ShopId, Names))
            {
                sql = "update venderType set ShopId='" + ShopId + "',Names='" + Names + "',Flag='" + Flag + "' where Id='" + Id + "'";
                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Delete Record

        /// <summary>
        /// Delete a record
        /// </summary>
        public int Delete(int Id)
        {
            string sql = "delete from venderType where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get ID by Name

        /// <summary>
        /// Get ID by name
        /// </summary>
        public int GetIdByName(int shopId, string names)
        {
            int id = 0;
            string sql = "select * from venderType where names='" + names + "' and shopId='" + shopId + "' ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

            if (ds.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }

            return id;
        }

        #endregion
    }
}
