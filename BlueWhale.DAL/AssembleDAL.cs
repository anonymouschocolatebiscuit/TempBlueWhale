using System;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class AssembleDAL
    {
        public AssembleDAL()
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

        private DateTime bizDate;
        public DateTime BizDate
        {
            get { return bizDate; }
            set { bizDate = value; }
        }

        private decimal fee;
        public decimal Fee
        {
            get { return fee; }
            set { fee = value; }
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

        #region Generate bill number automatically

        /// <summary>
        /// Generate bill number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            SqlParameter[] param = {
                                      new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","SPZZ"),// The bill will start will 4 alphabets
                                       new SqlParameter("@tableName","GoodsClose")// Table name
                                     };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0]?.ToString() ?? string.Empty;
            }

            return string.Empty;

        }

        #endregion

        #region Generate a new record
        /// <summary>
        /// Generate a new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = "INSERT INTO GoodsClose(shopId,number,bizDate,fee,remarks,makeId,makeDate,bizId,flag)";
            sql += " VALUES(@shopId,@number,@bizDate,@fee,@remarks,@makeId,@makeDate,@bizId,@flag)   SELECT @@IDENTITY ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@bizDate",bizDate),
                                       new SqlParameter("@Fee",Fee),
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
            string sql = @" UPDATE GoodsClose
                            SET  shopId=@shopId
                                ,bizDate = @bizDate
                                ,fee = @fee
                                ,remarks = @remarks
                                ,BizId = @BizId
                                ,Flag = @Flag
                            WHERE id=@id";

            SqlParameter[] param = {
                                        new SqlParameter("@ShopId",ShopId),
                                        new SqlParameter("@BizDate",BizDate),
                                        new SqlParameter("@Fee",Fee),
                                        new SqlParameter("@Remarks",Remarks),
                                        new SqlParameter("@BizId",BizId),
                                        new SqlParameter("@Flag",Flag),
                                        new SqlParameter("@Id",Id)
                                    };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete record
        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = @" IF NOT EXISTS (
                                SELECT 1 FROM GoodsClose WITH (NOLOCK) WHERE flag = '审核' AND id = @Id
                            )
                            BEGIN
                                DELETE FROM GoodsCloseItem WHERE pId = @Id;
                                DELETE FROM GoodsClose WHERE Id = @Id;
                            END";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, parameters);
        }

        #endregion

        #region Get all model

        /// <summary>
        /// Get all model
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "SELECT * FROM viewGoodsClose ORDER BY number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Search model 

        /// <summary>
        /// Search model
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end)
        {
            string sql = "SELECT * FROM viewGoodsClose WHERE bizDate>=@start AND bizDate<=@end AND types=1 ";

            if (shopId != 0)
            {
                sql += " AND shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " AND( number like '%" + key + "%' OR remarks LIKE '%" + key + "%' OR remarksItem LIKE '%" + key + "%') ";
            }

            sql += " ORDER BY number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Search model by id 

        /// <summary>
        /// Search model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "SELECT * FROM viewGoodsClose WHERE id='" + id + "' AND types=1 ORDER BY number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Search model by number

        /// <summary>
        /// Search model by number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "SELECT * FROM viewGoodsClose WHERE number='" + number + "'  and types=1 ORDER BY number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update record

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, DateTime checkDate, string flag)
        {
            string sql = " IF NOT EXISTS(SELECT 1 FROM GoodsClose WITH (NOLOCK) WHERE flag='" + flag + "' AND id='" + Id + "') ";
            sql += " BEGIN ";

            sql += " UPADTE GoodsClose WITH (ROWLOCK) SET flag='" + flag + "' ";

            if (flag == "Check")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            else if (flag == "Save")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += " WHERE id = '" + Id + "'";
            sql += " END ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
