using System;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class CheckBillDAL
    {
        public CheckBillDAL()
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

        private int clientIdA;
        public int ClientIdA
        {
            get { return clientIdA; }
            set { clientIdA = value; }
        }

        private int venderIdA;
        public int VenderIdA
        {
            get { return venderIdA; }
            set { venderIdA = value; }
        }

        private int clientIdB;
        public int ClientIdB
        {
            get { return clientIdB; }
            set { clientIdB = value; }
        }

        private int venderIdB;
        public int VenderIdB
        {
            get { return venderIdB; }
            set { venderIdB = value; }
        }

        private int bizType;
        public int BizType
        {
            get { return bizType; }
            set { bizType = value; }
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

        private decimal checkPrice;
        public decimal CheckPrice
        {
            get { return checkPrice; }
            set { checkPrice = value; }
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

        #region Automatically Generate Bill Number

        /// <summary>
        /// Generate Bill Number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {

                                      new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","DJHX"),//Bill code prefix (4 letters)
                                       new SqlParameter("@tableName","CheckBill")//Table name
                                       
                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Add a Record
        /// <summary>
        /// Add a Record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = "insert into CheckBill(shopId,number,clientIdA,venderIdA,clientIdB,venderIdB,bizType,bizDate,makeId,makeDate,CheckPrice,flag,remarks)";
            sql += " values(@shopId,@number,@clientIdA,@venderIdA,@clientIdB,@venderIdB,@bizType,@bizDate,@makeId,@makeDate,@CheckPrice,@flag,@remarks)   select @@identity ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@ClientIdA",ClientIdA),
                                       new SqlParameter("@VenderIdA",VenderIdA),

                                       new SqlParameter("@ClientIdB",ClientIdB),
                                       new SqlParameter("@VenderIdB",VenderIdB),

                                       new SqlParameter("@BizType",BizType),
                                       new SqlParameter("@bizDate",bizDate),

                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@CheckPrice",CheckPrice),

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

        #region Update a Record
        /// <summary>
        /// Update a Records
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE CheckBill WITH (ROWLOCK)
                           SET 
                               clientIdA = @clientIdA,
                               shopId = @shopId,
                               venderIdA = @venderIdA,
                               clientIdB = @clientIdB,
                               venderIdB = @venderIdB,                                    
                               bizDate = @bizDate,
                               bizType = @bizType,                                   
                               makeId = @makeId,                                                        
                               makeDate = @makeDate,
                               CheckPrice = @CheckPrice,
                               Flag = @Flag,
                               remarks = @remarks              
                           WHERE id=@id";

            SqlParameter[] param = {
                new SqlParameter("@ShopId",ShopId),
                new SqlParameter("@ClientIdA",ClientIdA),
                new SqlParameter("@VenderIdA",VenderIdA),
                new SqlParameter("@ClientIdB",ClientIdB),
                new SqlParameter("@VenderIdB",VenderIdB),
                new SqlParameter("@bizDate",bizDate),
                new SqlParameter("@BizType",BizType),
                new SqlParameter("@makeId",makeId),
                new SqlParameter("@MakeDate",MakeDate),
                new SqlParameter("@CheckPrice",CheckPrice),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Remarks",Remarks),
                new SqlParameter("@Id",Id)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete a Record
        /// <summary>
        /// Delete a Record
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += "               select 1 from CheckBill WITH (NOLOCK) where flag='Audited' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from CheckBillItemPayMent where pId='" + Id + "'; delete from CheckBillItemPur where pId='" + Id + "'; ";
            sql += " delete from CheckBillItemReceivable where pId='" + Id + "'; delete from CheckBillItemSales where pId='" + Id + "'; delete from CheckBill where Id='" + Id + "';";
            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Query Check Bills (Vendor Payment Verification)

        /// <summary>
        /// Query Check Bills (Vendor Payment Verification)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModelVender(int shopId, string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewCheckBill where bizDate>=@start and bizDate<=@end and bizType=2 ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (!string.IsNullOrEmpty(key))
            {
                sql += " and (venderName like '%" + key + "%' or number like '%" + key + "%' or remarks like '%" + key + "%') ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                new SqlParameter("@start", start),
                new SqlParameter("@end", end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query Check Bills by ID

        /// <summary>
        /// Query Check Bills by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewCheckBill where id='" + id + "' order by number ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql);

        }

        #endregion

        #region Audit a Record

        /// <summary>
        /// Audit a Record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="checkerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int checkerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select 1 from CheckBill WITH (NOLOCK) where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update CheckBill WITH (ROWLOCK) set flag='" + flag + "' ";
            if (flag == "Audited")
            {
                sql += " ,checkId='" + checkerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "Saved")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += " where id = '" + Id + "'";
            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Query Verification Bills -------- Payment Verification --- Clients

        /// <summary>
        /// Query verification bills -------- Payment verification --- Clients
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModelClient(int shopId, string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewCheckBill where bizDate>=@start and bizDate<=@end and bizType=1 ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (!string.IsNullOrEmpty(key))
            {
                sql += " and (clientName like '%" + key + "%' or number like '%" + key + "%' or remarks like '%" + key + "%' ) ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                               new SqlParameter("@start", start),
                               new SqlParameter("@end", end)
                           };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


    }

}