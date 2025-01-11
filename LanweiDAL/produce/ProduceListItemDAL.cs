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
    public class ProduceListItemDAL
    {
        public ProduceListItemDAL()
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

        private int processId;
        public int ProcessId
        {
            get { return processId; }
            set { processId = value; }
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

            string sql = "insert into ProduceListItem(pId,ProcessId,num,price,sumPrice,remarks)";
            sql += " values(@pId,@ProcessId,@num,@price,@sumPrice,@remarks)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@ProcessId",ProcessId),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@SumPrice",SumPrice),                                     
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


            sql += " delete from ProduceListItem  where pId='" + PId + "' ";

   

          

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
            string sql = "select * from viewProduceListItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(string isWhere)
        {
            string sql = "select * from viewProduceListItem  ";

            if (isWhere != "")
            {
                sql += " where " + isWhere;

            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
