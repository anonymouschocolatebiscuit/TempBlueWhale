using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class PayMentDAL
    {
        public PayMentDAL()
        {

        }

        #region Attributes

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

        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int wlId;

        public int WlId
        {
            get { return wlId; }
            set { wlId = value; }
        }

        private DateTime bizDate;

        public DateTime BizDate
        {
            get { return bizDate; }
            set { bizDate = value; }
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

        private decimal disPrice;

        public decimal DisPrice
        {
            get { return disPrice; }
            set { disPrice = value; }
        }

        private decimal payPriceNowMore;

        public decimal PayPriceNowMore
        {
            get { return payPriceNowMore; }
            set { payPriceNowMore = value; }
        }

        private string flag;

        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        #endregion

        #region Search Payment Order

        /// <summary>
        /// Search Payment Order
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int wlId, DateTime start, DateTime end, string number)
        {
            string sql = "select *,'付款' types from viewPayMent  where bizDate>=@start and bizDate<=@end and wlId='" + wlId + "'  and (isnull(payPriceSum,0)-isnull(priceCheckNowSum,0))> 0 ";
            if (number != "")
            {
                sql += " and  number like '%" + number + "%' ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Search Payment Order By Id

        /// <summary>
        /// Search Payment Order By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewPayMent where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region Auto Generate Receipt Number

        /// <summary>
        /// Generate Receipt Number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                                       new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","CGFK"),
                                       new SqlParameter("@tableName","PayMent")
                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Add a record

        /// <summary>
        /// Add a record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = "insert into PayMent(shopId,number,wlId,bizDate,makeId,makeDate,disPrice,PayPriceNowMore,flag,remarks)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@makeId,@makeDate,@disPrice,@PayPriceNowMore,@flag,@remarks)   select @@identity ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@bizDate",bizDate),

                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@disPrice",DisPrice),
                                       new SqlParameter("@PayPriceNowMore",PayPriceNowMore),

                                       new SqlParameter("@Flag",Flag),
                                       new SqlParameter("@Remarks",Remarks)
                                     };

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, param);
            if (sdr.Read())
            {
                id = sdr[0].ToString();
            }

            return int.Parse(id);
        }

        #endregion

        #region Edit a record
        /// <summary>
        /// Edit a record
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE PayMent
                                       SET 
                                           shopId = @shopId
                                          ,wlId = @wlId                                       
                                          ,bizDate = @bizDate                                      
                                          ,makeId = @makeId                                                                                         
                                          ,makeDate = @makeDate
                                          ,DisPrice = @DisPrice                                                                                         
                                          ,PayPriceNowMore = @PayPriceNowMore
                                          ,Flag = @Flag
                                          ,remarks = @remarks
                                WHERE id=@id";
            SqlParameter[] param = {
                new SqlParameter("@ShopId",ShopId),
                new SqlParameter("@wlId",WlId),
                new SqlParameter("@bizDate",bizDate),
                new SqlParameter("@makeId",makeId),
                new SqlParameter("@MakeDate",MakeDate),
                new SqlParameter("@DisPrice",DisPrice),
                new SqlParameter("@PayPriceNowMore",PayPriceNowMore),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Remarks",Remarks),
                 new SqlParameter("@Id",Id)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Check payment order


        /// <summary>
        /// Check payment order
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewPayMent  where bizDate>=@start and bizDate<=@end  ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and (wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%') ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region Audit a record

        /// <summary>
        /// Audit a record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from PayMent where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update PayMent set flag='" + flag + "' ";
            if (flag == "Review")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "Save")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete a record

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += "               select * from PayMent where flag='Review' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from PayMentAccountItem  where pId='" + Id + "' delete from PayMentSourceBillItem  where pId='" + Id + "' delete from PayMent where Id='" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
