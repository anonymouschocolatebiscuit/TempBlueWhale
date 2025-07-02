using BlueWhale.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace BlueWhale.DAL.produce
{
    public class ProduceListItemBomDAL
    {
        public ProduceListItemBomDAL()
        {

        }

        #region Attribute

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

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private int goodsId;
        public int GoodsId
        {
            get { return goodsId; }
            set { goodsId = value; }
        }

        private float numBom;
        public float NumBom
        {
            get { return numBom; }
            set { numBom = value; }
        }

        private float rate;
        public float Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        private float num;
        public float Num
        {
            get { return num; }
            set { num = value; }
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

        #region Create new record
        /// <summary>
        /// Create new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into ProduceListItemBom(pId,itemId,goodsId,numBom,rate,num,remarks)";
            sql += " values(@pId,@itemId,@goodsId,@numBom,@rate,@num,@remarks)  ";
            SqlParameter[] param = {
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@ItemId",ItemId),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@NumBom",NumBom),
                                       new SqlParameter("@Rate",Rate),
                                       new SqlParameter("@Num",Num),
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
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";

            sql += " delete from ProduceListItemBom  where pId='" + PId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region Get All Model

        /// <summary>
        /// Get All Model
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewProduceListItemBom where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Member

        /// <summary>
        /// Get All Member
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(string isWhere)
        {
            string sql = "select * from viewProduceListItemBom  ";

            if (isWhere != "")
            {
                sql += " where " + isWhere;

            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
