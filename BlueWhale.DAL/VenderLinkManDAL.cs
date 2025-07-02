using BlueWhale.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class VenderLinkManDAL
    {
        public VenderLinkManDAL()
        {
        }

        #region Attributes

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Vender Number
        /// </summary>
        private int pId;
        public int PId
        {
            get { return pId; }
            set { pId = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private int defaults;
        public int Defaults
        {
            get { return defaults; }
            set { defaults = value; }
        }

        #endregion

        /// <summary>
        /// Check username existance
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNames(int pId, string names)
        {
            bool flag = false;

            string sql = "select * from venderLinkMan where pId='" + pId + "' and names='" + names + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        #region Get All Contact

        /// <summary>
        /// Get All Contact------With Code Vender Number
        /// </summary>
        /// <returns></returns>
        public DataSet GetMolderByPId()
        {
            string sql = "select * from venderLinkMan where pId='" + PId + "' order by id ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Contact
        /// <summary>
        /// Add Contact
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "";

            if (Defaults == 1)
            {
                sql += " update venderLinkMan set default=0 where pid='" + PId + "' ";
            }
            sql += " insert into venderLinkMan(pid,names,phone,tel,default) values('" + PId + "','" + Names + "','" + Phone + "','" + Tel + "','" + Defaults + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Edit Contact Details
        /// <summary>
        /// Edit Contact Details
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = "";
            if (Defaults == 1)
            {
                sql += " update venderLinkMan set default=0  where pid='" + PId + "' ";
            }

            sql += " update venderLinkMan set Names='" + Names + "',Phone='" + Phone + "',Tel='" + Tel + "',Default='" + Defaults + "' where Id='" + Id + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion


        #region Delete Contact
        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from venderLinkMan where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}