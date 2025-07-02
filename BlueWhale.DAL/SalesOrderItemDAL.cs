using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class SalesOrderItemDAL
    {
        public SalesOrderItemDAL()
        {
        }

        #region Member attributes

        private int pId;
        public int PId { get { return pId; } set { pId = value; } }

        private int id;
        public int Id { get { return id; } set { id = value; } }

        private int goodsId;
        public int GoodsId { get { return goodsId; } set { goodsId = value; } }

        private decimal num;
        public decimal Num { get { return num; } set { num = value; } }

        private decimal price;
        public decimal Price { get { return price; } set { price = value; } }

        private decimal dis;
        public decimal Dis { get { return dis; } set { dis = value; } }

        /// <summary>
        /// Discount amount
        /// </summary>
        private decimal sumPriceDis;
        public decimal SumPriceDis { get { return sumPriceDis; } set { sumPriceDis = value; } }

        private decimal priceNow;
        public decimal PriceNow { get { return priceNow; } set { priceNow = value; } }

        private decimal sumPriceNow;
        public decimal SumPriceNow { get { return sumPriceNow; } set { sumPriceNow = value; } }

        private int tax;
        public int Tax { get { return tax; } set { tax = value; } }

        private decimal priceTax;
        public decimal PriceTax { get { return priceTax; } set { priceTax = value; } }

        private decimal sumPriceTax;
        public decimal SumPriceTax { get { return sumPriceTax; } set { sumPriceTax = value; } }

        private decimal sumPriceAll;
        public decimal SumPriceAll { get { return sumPriceAll; } set { sumPriceAll = value; } }

        private int ckId;
        public int CkId { get { return ckId; } set { ckId = value; } }

        private string remarks;
        public string Remarks { get { return remarks; } set { remarks = value; } }

        private int itemId;
        public int ItemId { get { return itemId; } set { itemId = value; } }

        private string sourceNumber;
        public string SourceNumber { get { return sourceNumber; } set { sourceNumber = value; } }

        #endregion

        #region Add new record

        public int Add()
        {
            string sql = @"INSERT into salesOrderItem (
                pId, goodsId, num, price, dis, sumPriceDis, priceNow, sumPriceNow,
                tax, priceTax, sumPriceTax, sumPriceAll, ckId, remarks, itemId, sourceNumber)
                VALUES (
                @pId, @goodsId, @num, @price, @dis, @sumPriceDis, @priceNow, @sumPriceNow,
                @tax, @priceTax, @sumPriceTax, @sumPriceAll, @ckId, @remarks, @itemId, @sourceNumber)";

            SqlParameter[] param = {
                new SqlParameter("@PId", PId),
                new SqlParameter("@GoodsId", GoodsId),
                new SqlParameter("@Num", Num),
                new SqlParameter("@Price", Price),
                new SqlParameter("@Dis", Dis),
                new SqlParameter("@SumPriceDis", SumPriceDis),
                new SqlParameter("@PriceNow", PriceNow),
                new SqlParameter("@SumPriceNow", SumPriceNow),
                new SqlParameter("@Tax", Tax),
                new SqlParameter("@PriceTax", PriceTax),
                new SqlParameter("@SumPriceTax", SumPriceTax),
                new SqlParameter("@SumPriceAll", SumPriceAll),
                new SqlParameter("@CkId", CkId),
                new SqlParameter("@Remarks", Remarks),
                new SqlParameter("@ItemId", ItemId),
                new SqlParameter("@SourceNumber", SourceNumber)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete Records

        public int Delete(int pId)
        {
            string sql = "DELETE FROM salesOrderItem WHERE pId='" + pId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int DeleteItem(int id)
        {
            string sql = "DELETE FROM salesOrderItem WHERE Id='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get all members

        public DataSet GetAllModel(int pId)
        {
            string sql = "SELECT * FROM viewSalesOrderItem WHERE pId='" + pId + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public DataSet GetAllModel(string ItemIdString)
        {
            string sql = @"SELECT item.id, goodsId, item.num, g.names, g.spec, g.unitName, g.code, g.barcode, g.priceCost, p.number
                            FROM salesOrderItem item
                            LEFT JOIN viewGoods g ON item.goodsId = g.id
                            LEFT JOIN salesOrder p ON item.pId = p.id
                            WHERE item.id IN (" + ItemIdString + ")";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public DataSet GetAllModel(string key, DateTime start, DateTime end, int clientId)
        {
            string sql = "SELECT * FROM viewsalesOrderDetail WHERE bizDate >= @start AND bizDate <= @end AND wlId='" + clientId + "'";
            if (key != "")
            {
                sql += " AND (goodsName LIKE '%" + key + "%' OR spec LIKE '%" + key + "%' OR code LIKE '%" + key + "%')";
            }
            sql += " ORDER BY code";

            SqlParameter[] param = {
                new SqlParameter("@start", start),
                new SqlParameter("@end", end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        public DataSet GetOrderListSum(string key, DateTime start, DateTime end, int clientId)
        {
            string sql = "SELECT barcode, goodsId, code, spec, goodsName, unitName, SUM(Num) sumNum, SUM(sumPriceAll) sumPriceAll FROM viewsalesOrderDetail WHERE bizDate >= @start AND bizDate <= @end AND wlId='" + clientId + "'";
            if (key != "")
            {
                sql += " AND (goodsName LIKE '%" + key + "%' OR spec LIKE '%" + key + "%' OR code LIKE '%" + key + "%')";
            }
            sql += " GROUP BY barcode, goodsId, code, spec, goodsName, unitName ORDER BY code";

            SqlParameter[] param = {
                new SqlParameter("@start", start),
                new SqlParameter("@end", end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get item list

        public DataSet GetList(string isWhere)
        {
            string sql = "SELECT * FROM viewSalesOrderListSelect WITH (NOLOCK)";
            if (isWhere != "")
            {
                sql += " WHERE " + isWhere;
            }
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}