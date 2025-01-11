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
    public class OtherInItemDAL
    {
        public OtherInItemDAL()
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


        #region 新增一条记录
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <returns></returns>
        public int Add()
        {

            string sql = "insert into OtherInItem(pId,goodsId,num,price,sumPrice,ckId,remarks)";
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

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = " ";


            sql += " delete from OtherInItem  where pId='" + PId + "' ";

   

          

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
            string sql = "select * from viewOtherInItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
