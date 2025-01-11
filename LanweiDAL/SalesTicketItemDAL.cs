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
    public class SalesTicketItemDAL
    {
        public SalesTicketItemDAL()
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

        private decimal dis;
        public decimal Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        private decimal disPrice;
        public decimal DisPrice
        {
            get { return disPrice; }
            set { disPrice = value; }
        }

        private int tax;
        public int Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        private int taxPrice;
        public int TaxPrice
        {
            get { return taxPrice; }
            set { taxPrice = value; }
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
          
            string sql = "insert into SalesTicketItem(pId,goodsId,num,price,dis,disPrice,sumPrice,tax,taxPrice,sumPriceAll,ckId,remarks,sourceNumber)";
            sql += " values(@pId,@goodsId,@num,@price,@dis,@disPrice,@sumPrice,@tax,@taxPrice,@sumPriceAll,@ckId,@remarks,@sourceNumber)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@Dis",Dis),
                                       new SqlParameter("@DisPrice",DisPrice),
                                       new SqlParameter("@SumPrice",SumPrice),
                                       new SqlParameter("@Tax",Tax),
                                       new SqlParameter("@TaxPrice",TaxPrice),
                                       new SqlParameter("@SumPriceAll",SumPriceAll),
                                       new SqlParameter("@CkId",CkId),
                                       new SqlParameter("@Remarks",Remarks),
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


            sql += " delete from SalesTicketItem  where pId='" + PId + "' ";

   

          

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
            string sql = "select * from viewSalesTicketItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
