using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class SalesOrderItemDAL
    {
        public SalesOrderItemDAL()
        {

        }

        #region 成员属性

     

        private int pId;
        public int PId
        {
            get { return pId; }
            set { pId = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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
        /// 折扣金额
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


        

        #region 新增一条记录
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            #region 插入列
            string sql = @" insert into salesOrderItem
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

            #region 插入值

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

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";


            sql += " delete from salesOrderItem  where pId='" + PId + "' ";

   

          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 删除成员信息--------单条
        /// <summary>
        /// 删除成员信息--------单条
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteItem(int id)
        {
            string sql = " ";

            sql += " delete from salesOrderItem  where Id='" + id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewSalesOrderItem where pId='"+ pId +"' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取销售订单的商品列表，关联商品信息，获取原单、ItemId,以及采购价格等等

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(string ItemIdString)
        {
            string sql = @"select item.id,goodsId,item.num,
                                    g.names,
                                    g.spec,
                                    g.unitName,
                                    g.code,
                                    g.barcode,
                                    g.priceCost,
                                    p.number
                                    from
                                    salesOrderItem item
                                    left join viewGoods g on item.goodsId=g.id
                                    left join salesOrder p on item.pId=p.id 
                                    where item.id in (" + ItemIdString + ")";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 查询订货明细

        /// <summary>
        /// 查询订货明细
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end,int clientId)
        {
            string sql = "select * from viewsalesOrderDetail  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
            if (key != "")
            {
                sql += " and (goodsName like '%" + key + "%' or spec  like '%" + key + "%'  or code  like '%" + key + "%' ) ";
            }
           
            sql += " order by code ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询订货汇总

        /// <summary>
        /// 查询订货汇总
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetOrderListSum(string key, DateTime start, DateTime end, int clientId)
        {
            string sql = "select barcode,goodsId,code,spec,goodsName,unitName,sum(Num) sumNum,sum(sumPriceAll) sumPriceAll from viewsalesOrderDetail  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
            if (key != "")
            {
                sql += " and (goodsName like '%" + key + "%' or spec  like '%" + key + "%'  or code  like '%" + key + "%' ) ";
            }

            sql += @" group by barcode,goodsId,code,spec,goodsName,unitName order by code ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 获取销售订单的商品列表，关联商品信息，获取原单、ItemId,以及采购价格等等

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(string isWhere)
        {
            string sql = @"
                            select * from viewSalesOrderListSelect
                                    ";

            if (isWhere != "")
            {
                sql +=" where "+ isWhere;
            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
