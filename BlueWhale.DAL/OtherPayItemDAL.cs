using BlueWhale.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class OtherPayItemDAL
    {
        public OtherPayItemDAL()
        {

        }

        #region Member attributes

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

        private int typeId;
        public int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        #endregion

        #region Add a new record
        /// <summary>
        /// Add a new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into OtherPayItem(pId,typeId,price,remarks)";
            sql += " values(@pId,@typeId,@price,@remarks)  ";
            SqlParameter[] param = {
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@TypeId",TypeId),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@Remarks",Remarks)
                                     };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete member information
        /// <summary>
        /// Delete member information
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";
            sql += " delete from OtherPayItem  where pId='" + PId + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get all members

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewOtherPayItem where pId='" + pId + "' ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
