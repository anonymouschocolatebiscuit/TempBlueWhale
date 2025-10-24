using BlueWhale.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class InventoryTransferDetailsDAL
    {
        public InventoryTransferDetailsDAL()
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

        private int goodsId;
        public int GoodsId
        {
            get { return goodsId; }
            set { goodsId = value; }
        }

        private decimal num;
        public decimal Num
        {
            get { return num; }
            set { num = value; }
        }

        private int ckIdOut;
        public int CkIdOut
        {
            get { return ckIdOut; }
            set { ckIdOut = value; }
        }

        private int ckIdIn;
        public int CkIdIn
        {
            get { return ckIdIn; }
            set { ckIdIn = value; }
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
            string sql = "insert into inventoryTransferDetails(pId,goodsId,num,ckIdIn,ckIdOut,remarks)";
            sql += " values(@pId,@goodsId,@num,@ckIdIn,@ckIdOut,@remarks)  ";
            SqlParameter[] param = {
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@CkIdIn",CkIdIn),
                                       new SqlParameter("@CkIdOut",CkIdOut),
                                       new SqlParameter("@Remarks",Remarks)
                                     };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete a record
        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";
            sql += " delete from inventoryTransferDetails where pId='" + PId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Retrieve all records

        /// <summary>
        /// Retrieve all records
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewInventoryTransferDetails where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}

