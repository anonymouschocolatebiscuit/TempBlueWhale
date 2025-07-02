using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL.produce
{
    public class ProcessListDAL
    {
        public ProcessListDAL()
        {
        }

        #region Class Method

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

        private int typeId;
        public int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private int unitId;
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        #endregion

        public DataSet GetList(string strWhere)
        {
            string sql = @"SELECT *
                            FROM viewProcessList WITH(NOLOCK)";

            if (strWhere != "")
            {
                sql += " WHERE " + strWhere;
            }

            sql += " ORDER BY SEQ ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql.ToString(), null);
        }

        public int Delete(int Id)
        {
            string sql = " DELETE FROM processList WHERE id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public bool IsExistsNamesAdd(int shopId, string names)
        {
            bool flag = false;

            string sql = "SELECT * FROM processList WHERE names='" + names + "' AND shopId='" + shopId + "' ";

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

            string sql = "SELECT * FROM processList WHERE names='" + names + "' and id<>'" + id + "' AND shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        public int Add()
        {
            string sql = "INSERT INTO processList(shopId,typeId,names,unitId,price,seq) VALUES" +
                "('" + ShopId + "','" + TypeId + "','" + Names + "','" + UnitId + "','" + Price + "','" + Seq + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int Update()
        {
            if (!IsExistsNamesEdit(Id, ShopId, Names))
            {
                string sql = "UPDATE processList SET ShopId='" + ShopId + "',TypeId='" + TypeId + "',Names='" + Names + "',unitId='" + UnitId + "',price='" + Price + "',Seq='" + Seq + "' WHERE Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }
    }
}
