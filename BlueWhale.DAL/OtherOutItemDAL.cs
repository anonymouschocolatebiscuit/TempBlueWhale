using BlueWhale.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueWhale.DAL
{
    public class OtherOutItemDAL
    {
        public OtherOutItemDAL()
        {

        }

        #region Property

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
        /// <summary>
        /// Add 
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into OtherOutItem(pId,goodsId,num,price,sumPrice,ckId,remarks)";
            sql += " values(@pId,@goodsId,@num,@price,@sumPrice,@ckId,@remarks)  ";
            SqlParameter[] param = {
                                       new SqlParameter("@PId",PId),
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

    }
}
