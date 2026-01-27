using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class OtherPayDAL
    {
        public OtherPayDAL()
        {

        }

        #region Member attributes

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

        private int bkId;

        public int BkId
        {
            get { return bkId; }
            set { bkId = value; }
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

        #region Automatically generate document numbers

        /// <summary>
        /// Generate document number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                                      new SqlParameter("@shopId",shopId),
                                      new SqlParameter("@NumberHeader","QTFK"),
                                      new SqlParameter("@tableName","OtherPay")
                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Add a new record

        /// <summary>
        /// Add a new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = "insert into OtherPay(shopId,number,wlId,bizDate,bkId,remarks,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@bkId,@remarks,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@bizDate",bizDate),
                                       new SqlParameter("@bkId",BkId),
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@BizId",BizId),
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

        #region Edit a record

        /// <summary>
        /// Edit a record
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE OtherPay
                                        SET 
                                           wlId = @wlId                                       
                                          ,shopId = @shopId
                                          ,bizDate = @bizDate                                                                
                                          ,bkId = @bkId                                       
                                          ,remarks = @remarks                                    
                                          ,BizId = @BizId                                     
                                          ,Flag = @Flag    
                                        WHERE id=@id";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                           new SqlParameter("@WlId",WlId),
                                           new SqlParameter("@BizDate",BizDate),
                                           new SqlParameter("@bkId",BkId),
                                           new SqlParameter("@Remarks",Remarks),
                                           new SqlParameter("@BizId",BizId),
                                           new SqlParameter("@Flag",Flag),
                                           new SqlParameter("@Id",Id)
                                       };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete member information

        /// <summary>
        /// Delete member information
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += " select * from OtherPay where flag='审核' and id='" + Id + "' )";
            sql += " begin ";
            sql += " delete from OtherPayItem  where pId='" + Id + "' delete from OtherPay where Id='" + Id + "'";
            sql += " end ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get all members

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from viewOtherPay order by number ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Check other payment orders

        /// <summary>
        /// Check other payment orders
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end, int bkId)
        {
            string sql = "select * from viewOtherPay where bizDate>=@start and bizDate<=@end  ";

            if (shopId >= 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%') ";
            }
            if (bkId != 0)
            {
                sql += " and bkId = '" + bkId + "' ";
            }
            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        /// <summary>
        /// Get data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *   ");
            strSql.Append(" FROM viewOtherPay ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id  ");
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// Get data list
        /// </summary>
        public DataSet GetListSum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select bkName,sum(sumPrice) sumPrice   ");
            strSql.Append(" FROM viewOtherPay ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" group by bkName  ");
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #region Query other payment orders-----by number

        /// <summary>
        /// Query other payment orders-----by number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewOtherPay where id='" + id + "' order by number ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Query other payment orders-----by number

        /// <summary>
        /// Query other payment orders-----by number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewOtherPay where number='" + number + "' order by number ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Review a record

        /// <summary>
        /// Review a record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from OtherPay where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update OtherPay set flag='" + flag + "' ";
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
    }
}
