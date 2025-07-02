using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class ProduceInItemDAL
    {
        public ProduceInItemDAL()
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

        private decimal dis;
        public decimal Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        /// <summary>
        /// Discount Amount
        /// </summary>
        private decimal sumPriceDis;
        public decimal SumPriceDis
        {
            get { return sumPriceDis; }
            set { sumPriceDis = value; }
        }

        private decimal priceNow;
        public decimal PriceNow
        {
            get { return priceNow; }
            set { priceNow = value; }
        }

        private decimal sumPriceNow;
        public decimal SumPriceNow
        {
            get { return sumPriceNow; }
            set { sumPriceNow = value; }
        }

        private int tax;
        public int Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        private decimal priceTax;
        public decimal PriceTax
        {
            get { return priceTax; }
            set { priceTax = value; }
        }

        private decimal sumPriceTax;
        public decimal SumPriceTax
        {
            get { return sumPriceTax; }
            set { sumPriceTax = value; }
        }

        private decimal sumPriceAll;
        public decimal SumPriceAll
        {
            get { return sumPriceAll; }
            set { sumPriceAll = value; }
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

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private string sourceNumber;
        public string SourceNumber
        {
            get { return sourceNumber; }
            set { sourceNumber = value; }
        }



        #endregion


        #region Insert Data
        /// <summary>
        /// Insert new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            #region Insert row
            string sql = @" insert into ProduceInItem
                              ( 
                                pId
                                ,goodsId
                                ,num
                                ,price
                                ,dis
                                ,sumPriceDis
                                ,priceNow
                                ,sumPriceNow
                                ,tax
                                ,priceTax
                                ,sumPriceTax
                                ,sumPriceAll
                                ,ckId
                                ,remarks
                                ,itemId
                                ,sourceNumber
                                )";

            #endregion

            #region Insert value

            sql += @" values(
                                @pId
                                ,@goodsId
                                ,@num
                                ,@price
                                ,@dis
                                ,@sumPriceDis
                                ,@priceNow
                                ,@sumPriceNow
                                ,@tax
                                ,@priceTax
                                ,@sumPriceTax
                                ,@sumPriceAll
                                ,@ckId
                                ,@remarks
                                ,@itemId
                                ,@sourceNumber

               )";

            #endregion

            SqlParameter[] param = {
                                       
                new SqlParameter("@PId",PId),
                new SqlParameter("@GoodsId",GoodsId),
                new SqlParameter("@Num",Num),
                new SqlParameter("@Price",Price),
                new SqlParameter("@Dis",Dis),
                new SqlParameter("@SumPriceDis",SumPriceDis),
                new SqlParameter("@PriceNow",PriceNow),
                new SqlParameter("@SumPriceNow",SumPriceNow),
                new SqlParameter("@Tax",Tax),
                new SqlParameter("@PriceTax",PriceTax),
                new SqlParameter("@SumPriceTax",SumPriceTax),
                new SqlParameter("@SumPriceAll",SumPriceAll),
                new SqlParameter("@CkId",CkId),
                new SqlParameter("@Remarks",Remarks),
                new SqlParameter("@ItemId",ItemId),
                new SqlParameter("@SourceNumber",SourceNumber)

                                                                  
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


            sql += " delete from ProduceInItem  where pId='" + PId + "' ";

   

          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region Get

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewProduceInItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region Get

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(string isWhere)
        {
            string sql = "select * from viewProduceInItem  ";
            if (isWhere != "")
            {
                sql += " where " + isWhere;
            }


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
