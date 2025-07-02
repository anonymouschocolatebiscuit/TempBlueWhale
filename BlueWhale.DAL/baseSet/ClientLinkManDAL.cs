using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class ClientLinkManDAL
    {
        public ClientLinkManDAL()
        {

        }
        #region

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

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

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int moren;
        public int Moren
        {
            get { return moren; }
            set { moren = value; }
        }

        #endregion

        public bool isExistsNames(int id, string names)
        {
            bool flag = false;

            string sql = "select * from clientLinkMan where names='" + names + "' and pId='" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        #region 

        public DataSet GetMolderByPId()
        {
            string sql = "select * from clientLinkMan where pId='" + PId + "' order by id ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 

        public int Add()
        {
            string sql = "";
            if (Moren == 1)
            {
                sql += " update clientLinkMan set moren=0 where pId='" + PId + "'";
            }
            sql += " insert into clientLinkMan(pId,names,phone,tel,moren,address) values('" + PId + "','" + Names + "','" + Phone + "','" + Tel + "','" + Moren + "','" + Address + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 

        public int Update()
        {
            string sql = "";
            if (Moren == 1)
            {
                sql += " update clientLinkMan set moren=0  where PId='" + PId + "' ";
            }

            sql += " update clientLinkMan set Names='" + Names + "',Phone='" + Phone + "',Tel='" + Tel + "',Moren='" + Moren + "',Address='" + Address + "' where Id='" + Id + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 

        public int Delete(int Id)
        {
            string sql = "delete from clientLinkMan where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
