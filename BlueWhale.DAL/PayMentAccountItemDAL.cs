using BlueWhale.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class PayMentAccountItemDAL
    {
        public PayMentAccountItemDAL()
        {

        }

        #region Attributes

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

        private int bkId;
        public int BkId
        {
            get { return bkId; }
            set { bkId = value; }
        }

        private decimal payPrice;
        public decimal PayPrice
        {
            get { return payPrice; }
            set { payPrice = value; }
        }

        private int payTypeId;
        public int PayTypeId
        {
            get { return payTypeId; }
            set { payTypeId = value; }
        }

        private string payNumber;
        public string PayNumber
        {
            get { return payNumber; }
            set { payNumber = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        #endregion

        #region Create a record

        /// <summary>
        /// Create a record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {

            string sql = "insert into PayMentAccountItem(pId,bkId,payPrice,payTypeId,payNumber,remarks,flag)";
            sql += " values(@pId,@bkId,@payPrice,@payTypeId,@payNumber,@remarks,@flag)  ";
            SqlParameter[] param = {

                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@BkId",BkId),
                                       new SqlParameter("@PayPrice",PayPrice),
                                       new SqlParameter("@PayTypeId",PayTypeId),
                                       new SqlParameter("@PayNumber",PayNumber),
                                       new SqlParameter("@Remarks",Remarks),
                                       new SqlParameter("@Flag",Flag)
                                     };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";
            sql += " delete from PayMentAccountItem  where pId='" + PId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetList

        /// <summary>
        /// GetList
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewPayMentAccountItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
