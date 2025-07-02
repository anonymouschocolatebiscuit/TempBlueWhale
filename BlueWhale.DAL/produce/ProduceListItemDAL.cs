using BlueWhale.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace BlueWhale.DAL.produce
{
    public class ProduceListItemDAL
    {
        public ProduceListItemDAL()
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

        private int processId;
        public int ProcessId
        {
            get { return processId; }
            set { processId = value; }
        }

        private decimal num;
        public decimal Num
        {
            get { return num; }
            set { num = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private decimal sumPrice;
        public decimal SumPrice
        {
            get { return sumPrice; }
            set { sumPrice = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        #endregion

        #region Create ProduceListItem

        /// <summary>
        /// Create ProduceListItem
        /// </summary>
        /// <returns></returns>

        public int Add()
        {
            string sql = "insert into ProduceListItem(pId,ProcessId,num,price,sumPrice,remarks)";
            sql += " values(@pId,@ProcessId,@num,@price,@sumPrice,@remarks)  ";

            SqlParameter[] param = {
                new SqlParameter("@PId",PId),
                new SqlParameter("@ProcessId",ProcessId),
                new SqlParameter("@Num",Num),
                new SqlParameter("@Price",Price),
                new SqlParameter("@SumPrice",SumPrice),
                new SqlParameter("@Remarks",Remarks)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete ProduceListItem

        /// <summary>
        /// Delete ProduceListItem
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>

        public int Delete(int PId)
        {
            string sql = " ";

            sql += " delete from ProduceListItem  where pId='" + PId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get ProduceListItem By ProduceList Id

        /// <summary>
        /// Get ProduceListItem By ProduceList Id
        /// </summary>
        /// <returns></returns>

        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewProduceListItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get ProduceListItem List

        /// <summary>
        /// Get ProduceListItem List
        /// </summary>
        /// <returns></returns>

        public DataSet GetList(string isWhere)
        {
            string sql = "select * from viewProduceListItem  ";

            if (isWhere != "")
            {
                sql += " where " + isWhere;

            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
