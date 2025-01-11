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
    public class SalesOrderLockDAL
    {
        public SalesOrderLockDAL()
        {

        }

        #region 成员属性

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private decimal num;
        public decimal Num
        {
            get { return num; }
            set { num = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private int makeId;
        public int MakeId
        {
            get { return makeId; }
            set { makeId = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }


        #endregion


        #region 新增一条记录
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
          
            string sql = "insert into salesOrderLock(itemId,goodsId,num,makeId,makeDate,flag)";
            sql += " values(@itemId,@goodsId,@num,@makeId,@makeDate,@flag)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@ItemId",ItemId),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@MakeId",MakeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@Flag",Flag)

                                                                  
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


            sql += " delete from salesOrderLock  where itemId='" + PId + "' ";

   

          

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

            sql += " delete from salesOrderLock  where Id='" + id + "' ";

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
            string sql = "select * from viewSalesOrderLock where itemId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        


    }
}
