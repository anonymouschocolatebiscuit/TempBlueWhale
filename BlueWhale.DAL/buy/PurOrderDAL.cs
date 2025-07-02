using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class PurOrderDAL
    {
        public PurOrderDAL()
        {

        }

        #region Attribute

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

        private DateTime sendDate;
        public DateTime SendDate
        {
            get { return sendDate; }
            set { sendDate = value; }
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

        #region Auto Generate Bill Number

        /// <summary>
        /// Auto Generate Bill Number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                new SqlParameter("@shopId",shopId),
                new SqlParameter("@NumberHeader","CGDD"), // Bill Number First 4 Characters                               
                new SqlParameter("@tableName","purOrder") // Table
            };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// Create New Record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = "insert into purOrder(shopId,number,wlId,bizDate,sendDate,remarks,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@sendDate,@remarks,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
            SqlParameter[] param = {
                new SqlParameter("@ShopId",ShopId),
                new SqlParameter("@number",Number),
                new SqlParameter("@wlId",WlId),
                new SqlParameter("@bizDate",BizDate),
                new SqlParameter("@SendDate",SendDate),
                new SqlParameter("@remarks",Remarks),
                new SqlParameter("@makeId",MakeId),
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

        #region Update Record

        /// <summary>
        /// Update Record
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @" 
                            UPDATE purOrder
                            SET 
                            wlId = @wlId,
                            bizDate = @bizDate,
                            sendDate = @sendDate,
                            remarks = @remarks,
                            BizId = @BizId,
                            Flag = @Flag
                            WHERE id=@id and flag<>'Review'
                         ";
            SqlParameter[] param = {
                new SqlParameter("@WlId",WlId),
                new SqlParameter("@BizDate",BizDate),
                new SqlParameter("@SendDate",SendDate),
                new SqlParameter("@Remarks",Remarks),
                new SqlParameter("@BizId",BizId),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Id",Id)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete Member Info

        /// <summary>
        /// Delete Member Info
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += " select * from purOrder where flag='Review' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from purOrderItem  where pId='" + Id + "' delete from purOrder where Id='" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Member

        /// <summary>
        /// Get All Member
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from viewPurOrderList order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Search Purchase Order

        /// <summary>
        /// Search Purchase Order
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int shopId)
        {
            string sql = "select * from viewPurOrderList  where bizDate>=@start and bizDate<=@end  ";

            if (key != "")
            {
                sql += " and ( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' )";
            }
            if (shopId >= 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }
            sql += " order by number ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Search Purchase Order ----- With Id

        /// <summary>
        /// Search Purchase Order ----- With Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewPurOrderList where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Review Record

        /// <summary>
        /// Review Record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from purOrder where flag='" + flag + "' and id='" + Id + "') ";

            sql += " begin ";

            sql += " update purOrder set flag='" + flag + "' ";

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

        #region Search Purchase Order ----- Table

        /// <summary>
        /// Search Purchase Order ----- Table
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListReport(DateTime start, DateTime end, DateTime sendStart, DateTime sendEnd, int wlId, int goodsId, int types)
        {
            string sql = "select * from viewPurOrderListReport  where bizDate>=@start and bizDate<=@end  and sendDate>=@sendStart and sendDate<=@sendEnd ";

            if (wlId != 0)
            {
                sql += " and wlId = '" + wlId + "' ";
            }

            if (goodsId != 0)
            {
                sql += " and goodsId = '" + goodsId + "' ";
            }

            if (types != 0) // 0 All 1 Not In 2 Partially In 3 All In
            {
                if (types == 1) // not In
                {
                    sql += " and getNumNo = num ";
                }

                if (types == 2) // Partially in
                {
                    sql += " and getNum>0 and num>getNum ";
                }

                if (types == 3) //All In
                {
                    sql += " and getNumNo <= 0 ";
                }
            }
            sql += " order by code,number,wlId ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end),
                new SqlParameter("@sendStart",sendStart),
                new SqlParameter("@sendEnd",sendEnd)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Search Purchase Order-----Table----NewUI

        /// <summary>
        /// Search Purchase Order-----Table----NewUI
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListReport(int shopId, DateTime start, DateTime end, DateTime sendStart, DateTime sendEnd, string wlId, string code, string types)
        {

            string sql = @" 
                            select * from viewPurOrderListReport  
                            where bizDate>=@start and bizDate<=@end  and sendDate>=@sendStart and sendDate<=@sendEnd 
                         ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlId in (select id from vender where code in (" + wlId + ") and shopId='" + shopId + "' )";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            if (types != "") //0 All 1 Not In 2 Partially In 3 All In
            {
                sql += " and ( ";
                if (types.Contains("Not In"))
                {
                    sql += "  getNumNo = num ";
                }

                if (types.Contains("Not In") && types.Contains("Partially In"))
                {
                    sql += " or (getNum>0 and num>getNum )";
                }

                if (!types.Contains("Not In") && types.Contains("Partially In"))
                {
                    sql += " (getNum>0 and num>getNum )";
                }

                if (types.Contains("All In") && !types.Contains("Not In") && !types.Contains("Partially In"))
                {
                    sql += " getNumNo <= 0 ";
                }

                if (types.Contains("All In") && (types.Contains("Not In") || types.Contains("Partially In")))
                {
                    sql += " or getNumNo <= 0 ";
                }

                sql += " )";

                //if (types == 1) // Not In
                //{
                //    sql += " and getNumNo = num ";
                //}

                //if (types == 2) // Partially In
                //{
                //    sql += " and getNum>0 and num>getNum ";
                //}

                //if (types == 3) // All In
                //{
                //    sql += " and getNumNo <= 0 ";
                //}
            }
            sql += " order by code,number,wlId ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end),
                new SqlParameter("@sendStart",sendStart),
                new SqlParameter("@sendEnd",sendEnd)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion
    }
}