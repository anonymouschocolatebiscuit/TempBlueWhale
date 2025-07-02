using BlueWhale.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace BlueWhale.DAL
{
    public class AssembleItemDAL
    {
        public AssembleItemDAL()
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

        private int types;
        public int Types
        {
            get { return types; }
            set { types = value; }
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

        private int ckId;
        public int CkId
        {
            get { return ckId; }
            set { ckId = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        #endregion

        #region Add
        public int Add()
        {
            string sql = "insert into GoodsCloseItem(pId,types,goodsId,num,price,sumPrice,ckId,remarks)";
            sql += " values(@pId,@types,@goodsId,@num,@price,@sumPrice,@ckId,@remarks)  ";
            SqlParameter[] param = {
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@Types",Types),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@SumPrice",SumPrice),
                                       new SqlParameter("@CkId",CkId),
                                       new SqlParameter("@Remarks",Remarks)
                                     };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="PId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";

            sql += " DELETE FROM GoodsCloseItem WHERE pId='" + PId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion

        #region GetAllModel

        /// <summary>
        /// GetAllModel
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "SELECT * FROM viewGoodsCloseItem WHERE pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetAllModel

        /// <summary>
        /// GetAllModel
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId, int types)
        {
            string sql = "SELECT * FROM viewGoodsCloseItem WHERE pId='" + pId + "' ";

            if (types != 0)
            {
                sql += " AND types='" + types + "'";
            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
