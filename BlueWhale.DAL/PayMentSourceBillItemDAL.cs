using BlueWhale.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class PayMentSourceBillItemDAL
    {
        public PayMentSourceBillItemDAL()
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

        private string sourceNumber;
        public string SourceNumber
        {
            get { return sourceNumber; }
            set { sourceNumber = value; }
        }

        private decimal priceCheckNow;
        public decimal PriceCheckNow
        {
            get { return priceCheckNow; }
            set { priceCheckNow = value; }
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

            string sql = "insert into PayMentSourceBillItem(pId,sourceNumber,priceCheckNow,flag)";
            sql += " values(@pId,@sourceNumber,@priceCheckNow,@flag)  ";
            SqlParameter[] param = {

                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@SourceNumber",SourceNumber),
                                       new SqlParameter("@PriceCheckNow",PriceCheckNow),
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
            sql += " delete from PayMentSourceBillItem  where pId='" + PId + "' ";

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
            string sql = "select * from viewPayMentSourceBillItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
