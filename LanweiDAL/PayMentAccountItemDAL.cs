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
    public class PayMentAccountItemDAL
    {
        public PayMentAccountItemDAL()
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

        private int bkId;
        public int BkId
        {
            get { return bkId; }
            set { bkId = value; }
        }

        private decimal payPrice;
        public decimal PayPrice
        {
            get { return payPrice; }
            set { payPrice = value; }
        }

        private int payTypeId;
        public int PayTypeId
        {
            get { return payTypeId; }
            set { payTypeId = value; }
        }

        private string payNumber;
        public string PayNumber
        {
            get { return payNumber; }
            set { payNumber = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
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

            string sql = "insert into PayMentAccountItem(pId,bkId,payPrice,payTypeId,payNumber,remarks,flag)";
            sql += " values(@pId,@bkId,@payPrice,@payTypeId,@payNumber,@remarks,@flag)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@BkId",BkId),
                                       new SqlParameter("@PayPrice",PayPrice),
                                       new SqlParameter("@PayTypeId",PayTypeId),
                                       new SqlParameter("@PayNumber",PayNumber),
                                       new SqlParameter("@Remarks",Remarks),
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


            sql += " delete from PayMentAccountItem  where pId='" + PId + "' ";

   

          

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
            string sql = "select * from viewPayMentAccountItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
