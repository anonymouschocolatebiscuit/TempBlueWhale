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
    public class SalesQuoteItemDAL
    {
        public SalesQuoteItemDAL()
        {

        }

        #region 成员属性

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

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }



        #endregion


        #region 新增一条记录
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
          
            string sql = "insert into salesQuoteItem(pId,goodsId,num,price,sumPrice,tax,priceTax,sumPriceTax,sumPriceAll,remarks)";
            sql += " values(@pId,@goodsId,@num,@price,@sumPrice,@tax,@priceTax,@sumPriceTax,@sumPriceAll,@remarks)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@GoodsId",GoodsId),

                                    
                                      
                                       new SqlParameter("@Num",Num),

                                    

                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@SumPrice",SumPrice),
  
                                       new SqlParameter("@Tax",Tax),
                                       new SqlParameter("@PriceTax",PriceTax),

                                       new SqlParameter("@SumPriceTax",SumPriceTax),
                                       new SqlParameter("@SumPriceAll",SumPriceAll),
                                     
                                       new SqlParameter("@Remarks",Remarks)

                                                                  
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


            sql += " delete from salesQuoteItem  where pId='" + PId + "' ";

   

          

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

            sql += " delete from salesQuoteItem  where Id='" + id + "' ";

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
            string sql = "select * from viewSalesQuoteItem where pId='" + pId + "' ";

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
            string sql = "select * from viewSalesQuoteDetail  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
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
            string sql = "select goodsId,code,spec,goodsName,unitName,sum(Num) sumNum,sum(sumPriceAll) sumPriceAll from viewSalesQuoteItemDetail  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
            if (key != "")
            {
                sql += " and (goodsName like '%" + key + "%' or spec  like '%" + key + "%'  or code  like '%" + key + "%' ) ";
            }

            sql += @" group by goodsId,code,spec,goodsName,unitName order by code ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


    }
}
