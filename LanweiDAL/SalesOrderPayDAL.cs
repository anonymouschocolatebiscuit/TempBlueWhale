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
    public class SalesOrderPayDAL
    {
        public SalesOrderPayDAL()
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

        private int payTypeId;
        public int PayTypeId
        {
            get { return payTypeId; }
            set { payTypeId = value; }
        }

        private decimal payPrice;
        public decimal PayPrice
        {
            get { return payPrice; }
            set { payPrice = value; }
        }

      
        private string payNumber;
        public string PayNumber
        {
            get { return payNumber; }
            set { payNumber = value; }
        }



        #endregion


        #region 新增一条记录
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
          
            string sql = "insert into salesOrderPay(pId,payTypeId,payNumber,payPrice)";
            sql += " values(@pId,@payTypeId,@payNumber,@payPrice)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@PayTypeId",PayTypeId),
                                     
                                       new SqlParameter("@PayPrice",PayPrice),
                                    
                                       new SqlParameter("@PayNumber",PayNumber)

                                                                  
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


            sql += " delete from salesOrderPay  where pId='" + PId + "' ";

   

          

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

            sql += " delete from salesOrderPay  where Id='" + id + "' ";

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
            string sql = "select * from viewSalesOrderPay where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 查询订货付款明细

        /// <summary>
        /// 查询订货付款明细
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end,int clientId)
        {
            string sql = "select * from viewSalesOrderPayDetail  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
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


        #region 查询订货付款汇总

        /// <summary>
        /// 查询订货付款汇总
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetOrderListSum(string key, DateTime start, DateTime end, int clientId)
        {
            string sql = "select goodsId,code,spec,goodsName,unitName,sum(Num) sumNum,sum(sumPriceAll) sumPriceAll from viewSalesOrderPayDetail  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
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
