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
    public class ReceivableSourceBillItemDAL
    {
        public ReceivableSourceBillItemDAL()
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

        private string sourceNumber;
        public string SourceNumber
        {
            get { return sourceNumber; }
            set { sourceNumber = value; }
        }

        private decimal priceCheckNow;
        public decimal PriceCheckNow
        {
            get { return priceCheckNow; }
            set { priceCheckNow = value; }
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

            string sql = "insert into ReceivableSourceBillItem(pId,sourceNumber,priceCheckNow,flag)";
            sql += " values(@pId,@sourceNumber,@priceCheckNow,@flag)  ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@SourceNumber",SourceNumber),
                                       new SqlParameter("@PriceCheckNow",PriceCheckNow),
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


            sql += " delete from ReceivableSourceBillItem  where pId='" + PId + "' ";

   

          

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
            string sql = "select * from viewReceivableSourceBillItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
