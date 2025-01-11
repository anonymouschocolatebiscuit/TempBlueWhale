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
    public class ProduceProcessListDAL
    {
        /*
         * 生产计划的生产工序记录表
         * 
         * 
         * 
         * 
        */
        public ProduceProcessListDAL()
        {

        }

        #region 成员属性

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
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

        private int makeId;
        public int MakeId
        {
            get { return makeId; }
            set { makeId = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private int bizId;
        public int BizId
        {
            get { return bizId; }
            set { bizId = value; }
        }

        private DateTime bizDate;
        public DateTime BizDate
        {
            get { return bizDate; }
            set { bizDate = value; }
        }


        private int checkId;
        public int CheckId
        {
            get { return checkId; }
            set { checkId = value; }
        }

        private DateTime checkDate;
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
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
            string id = "0";

            string sql = @"insert into produceProcessList(
                                    shopId,
                                    pId,
                                    processId,
                                    num,
                                    price,
                                    sumPrice,   
                                    remarks,   
                                    makeId,
                                    makeDate,   
                                    bizId,
                                    bizDate,
                                    flag
                                           )";
                                      
            sql += @" values(
                                    @shopId,
                                    @pId,
                                    @processId,
                                    @num,
                                    @price,
                                    @sumPrice,   
                                    @remarks,   
                                    @makeId,
                                    @makeDate,   
                                    @bizId,
                                    @bizDate,
                                    @flag
                                     )  
                                          select @@identity ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@ProcessId",ProcessId),
                                       new SqlParameter("@Num",Num),                                       
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@SumPrice",SumPrice),
                                       new SqlParameter("@Remarks",Remarks),
                                       new SqlParameter("@BizId",BizId),
                                       new SqlParameter("@BizDate",BizDate),                                    
                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),                                      
                                       new SqlParameter("@Flag",Flag)
                                                                  
                                     };

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, param);
            if (sdr.Read())
            {
                id = sdr[0].ToString();

            }

            return int.Parse(id);
        }
        #endregion




        

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += "               select * from produceProcessList where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += "  delete from produceProcessList where Id='" + Id + "'";

            sql += " end ";

          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

       

        #region 审核一条记录

        /// <summary>
        /// 审核一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId,DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from produceProcessList where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update produceProcessList set flag='" + flag + "' ";
            if (flag == "审核")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "保存")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion


        



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *   ");
            strSql.Append(" FROM viewProduceProcessList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListSum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select bizId,bizName,typeId,typeName,processId,processName,unitName,sum(Num) sumNum,sum(sumPrice)  sumPrice ");
            strSql.Append(" FROM viewProduceProcessList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" group by bizId,bizName,typeId,typeName,processId,processName,unitName ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }





    }
}
