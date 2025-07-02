using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class SalesReceiptDAL
    {
        public SalesReceiptDAL()
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

        private int types;
        public int Types
        {
            get { return types; }
            set { types = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private decimal dis;
        public decimal Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        private decimal disPrice;
        public decimal DisPrice
        {
            get { return disPrice; }
            set { disPrice = value; }
        }

        private decimal sumPrice;
        public decimal SumPrice
        {
            get { return sumPrice; }
            set { sumPrice = value; }
        }

        private decimal payNow;
        public decimal PayNow
        {
            get { return payNow; }
            set { payNow = value; }
        }

        private decimal payNowNo;
        public decimal PayNowNo
        {
            get { return payNowNo; }
            set { payNowNo = value; }
        }

        private int bkId;
        public int BkId
        {
            get { return bkId; }
            set { bkId = value; }
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

        private int sendId;
        public int SendId
        {
            get { return sendId; }
            set { sendId = value; }
        }

        private string sendNumber;
        public string SendNumber
        {
            get { return sendNumber; }
            set { sendNumber = value; }
        }

        private string sendPayType;
        public string SendPayType
        {
            get { return sendPayType; }
            set { sendPayType = value; }
        }

        private decimal sendPrice;
        public decimal SendPrice
        {
            get { return sendPrice; }
            set { sendPrice = value; }
        }

        private string getName;
        public string GetName
        {
            get { return getName; }
            set { getName = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        #endregion

        #region Auto Generate Bill Number

        /// <summary>
        /// Generate Bill Number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                new SqlParameter("@shopId",shopId),// First 4 alphabet in bill number
                new SqlParameter("@NumberHeader","XSCK"),// First 4 alphabet in bill number
                new SqlParameter("@tableName","salesReceipt")// Table                                   
            };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Add new record
        /// <summary>
        /// Add new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = @"insert into SalesReceipt(shopId,number,wlId,bizDate,types,remarks,dis,disPrice,
            sumPrice,payNow,payNowNo,bkId,makeId,makeDate,bizId,flag,sendId,sendNumber,sendPayType,sendPrice,getName,phone,address)";
            sql += @" values(@shopId,@number,@wlId,@bizDate,@types,@remarks,@dis,@disPrice
           ,@sumPrice,@payNow,@payNowNo,@bkId,@makeId,@makeDate,@bizId,@flag,@sendId,@sendNumber,@sendPayType,@sendPrice,@getName,@phone,@address)  
            select @@identity ";

            SqlParameter[] param = {
                new SqlParameter("@ShopId",ShopId),
                new SqlParameter("@number",Number),
                new SqlParameter("@wlId",WlId),
                new SqlParameter("@bizDate",bizDate),
                new SqlParameter("@Types",Types),
                new SqlParameter("@remarks",remarks),
                new SqlParameter("@Dis",Dis),
                new SqlParameter("@DisPrice",DisPrice),
                new SqlParameter("@SumPrice",SumPrice),
                new SqlParameter("@PayNow",PayNow),
                new SqlParameter("@PayNowNo",PayNowNo),
                new SqlParameter("@BkId",BkId),
                new SqlParameter("@makeId",makeId),
                new SqlParameter("@MakeDate",MakeDate),
                new SqlParameter("@BizId",BizId),
                new SqlParameter("@sendId",SendId),
                new SqlParameter("@sendNumber",SendNumber),
                new SqlParameter("@SendPayType",SendPayType),
                new SqlParameter("@SendPrice",SendPrice),
                new SqlParameter("@getName",GetName),
                new SqlParameter("@Phone",Phone),
                new SqlParameter("@address",Address),
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

        #region Modify a record
        /// <summary>
        /// Modify a record
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE SalesReceipt
            SET
                shopId = @shopId 
                ,wlId = @wlId                                       
                ,bizDate = @bizDate                                                                             
                ,types = @types                                       
                ,remarks = @remarks
                ,Dis = @Dis  
                ,DisPrice = @DisPrice  
                ,SumPrice = @SumPrice  
                ,PayNow = @PayNow  
                ,PayNowNo = @PayNowNo
                ,BkId = @BkId                                      
                ,makeId = @makeId                                                                                         
                ,makeDate = @makeDate
                ,BizId = @BizId 
                ,sendId= @sendId
                ,sendNumber= @sendNumber
                ,sendPayType= @sendPayType
                ,sendPrice= @sendPrice
                ,getName= @getName
                ,phone= @phone
                ,address= @address                                  
                ,Flag = @Flag                                      
            WHERE id=@id";

            SqlParameter[] param = {
                new SqlParameter("@ShopId",ShopId),
                new SqlParameter("@WlId",WlId),
                new SqlParameter("@BizDate",BizDate),
                new SqlParameter("@Types",Types),
                new SqlParameter("@Remarks",Remarks),
                new SqlParameter("@Dis",Dis),
                new SqlParameter("@DisPrice",DisPrice),
                new SqlParameter("@SumPrice",SumPrice),
                new SqlParameter("@PayNow",PayNow),
                new SqlParameter("@PayNowNo",PayNowNo),
                new SqlParameter("@BkId",BkId),
                new SqlParameter("@MakeId",MakeId),
                new SqlParameter("@MakeDate",MakeDate),
                new SqlParameter("@BizId",BizId),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@sendId",SendId),
                new SqlParameter("@sendNumber",SendNumber),
                new SqlParameter("@SendPayType",SendPayType),
                new SqlParameter("@SendPrice",SendPrice),
                new SqlParameter("@getName",GetName),
                new SqlParameter("@Phone",Phone),
                new SqlParameter("@address",Address),
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
            sql += " select * from SalesReceipt where flag='review' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from SalesReceiptItem  where pId='" + Id + "' delete from SalesReceipt where Id='" + Id + "'";

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
            string sql = "select * from viewSalesReceipt order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Sales Outbound Order Inquiry

        /// <summary>
        /// Sales Outbound Order Inquiry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end, int types)
        {
            string sql = "select * from viewSalesReceiptList  where bizDate>=@start and bizDate<=@end  ";

            if (shopId != 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' )";
            }
            if (types != 0)
            {
                sql += " and types = '" + types + "' ";
            }
            sql += " order by number ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region Search Sales Receipt

        /// <summary>
        /// Search Sales Receipt
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetAllModelByWLId(string key, DateTime start, DateTime end, int clientId)
        {
            string sql = "select * from viewSalesReceipt  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' and flag='Check' and (isnull(sumPriceAll,0)-isnull(priceCheckNowSum,0))> 0  ";
            if (key != "")
            {
                sql += " and number like '%" + key + "%'  ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region Search Sales Receipt-----By id

        /// <summary>
        /// Search Sales Receipt-----By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewSalesReceipt where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Search Sales Receipt-----By number

        /// <summary>
        /// Search Sales Receipt-----By number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewSalesReceipt where number='" + number + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Review A Record

        /// <summary>
        /// Review A Record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from SalesReceipt where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update SalesReceipt set flag='" + flag + "' ";
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

        #region Search Sales Receipt Iteam Detail

        /// <summary>
        /// Search Sales Receipt Iteam Detail
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemDetail(DateTime start, DateTime end, int ckId, int wlId, int goodsId)
        {
            string sql = "select * from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";
            if (ckId != 0)
            {
                sql += " and ckId ='" + ckId + "' ";
            }
            if (wlId != 0)
            {
                sql += " and wlId ='" + wlId + "' ";
            }
            if (goodsId != 0)
            {
                sql += " and goodsId ='" + goodsId + "' ";
            }

            sql += " order by bizDate asc,number asc,code asc ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Search Sales Receipt Iteam Detail--------NewUI

        /// <summary>
        /// Search Sales Receipt Iteam Detail--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>

        public DataSet GetSalesReceiptItemDetail(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = "select * from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }
            if (wlId != "")
            {
                sql += " and wlId in (select id from client where code in (" + wlId + ") ) ";
            }
            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            sql += " order by bizDate asc,number asc,code asc ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query sales summary table----------by product

        /// <summary>
        /// Query sales summary table----------by product
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemSumGoods(DateTime start, DateTime end, int ckId, int wlId, int goodsId)
        {
            string sql = "select goodsId,code,goodsName,spec,unitName,ckId,ckName,sum(num) sumNum,sum(sumPrice) sumPrice from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";
            if (ckId != 0)
            {
                sql += " and ckId ='" + ckId + "' ";
            }
            if (wlId != 0)
            {
                sql += " and wlId ='" + wlId + "' ";
            }
            if (goodsId != 0)
            {
                sql += " and goodsId ='" + goodsId + "' ";
            }

            sql += " group by goodsId,code,goodsName,spec,unitName,ckId,ckName order by code asc ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query sales summary table----------by product--------NewUI

        /// <summary>
        /// Query sales summary table----------by product--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemSumGoods(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(num) sumNum,
                            sum(sumPriceNow) sumPriceNow,
                            sum(sumPriceDis) sumPriceDis,
                            sum(sumPriceTax) sumPriceTax,
                            sum(sumPriceAll) sumPriceAll  
                           from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }
            if (wlId != "")
            {
                sql += " and wlId in (select id from client where code in (" + wlId + ") ) ";
            }
            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            sql += " group by goodsId,code,goodsName,spec,unitName,ckId,ckName order by code asc ";

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
            strSql.Append(" FROM viewSalesReceipt ");
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
            strSql.Append("select wlName,sum(sumPriceAll) sumPriceAll  ");
            strSql.Append(" FROM viewSalesReceipt ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" group by wlName  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #region Query Sales Summary Table----------Customer

        /// <summary>
        /// Query Sales Summary Table----------Customer
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemSumClient(DateTime start, DateTime end, int ckId, int wlId, int goodsId)
        {
            string sql = "select wlId,wlName,goodsId,code,goodsName,spec,unitName,ckId,ckName,sum(num) sumNum,sum(sumPrice) sumPrice from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";
            if (ckId != 0)
            {
                sql += " and ckId ='" + ckId + "' ";
            }
            if (wlId != 0)
            {
                sql += " and wlId ='" + wlId + "' ";
            }
            if (goodsId != 0)
            {
                sql += " and goodsId ='" + goodsId + "' ";
            }

            sql += " group by wlId,wlName,goodsId,code,goodsName,spec,unitName,ckId,ckName order by wlId asc,code asc ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query sales summary table----------Customer--------NewUI

        /// <summary>
        /// Query sales summary table----------Customer--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemSumClient(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {

            string sql = @"select wlId,wlName,goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(num) sumNum,
                            sum(sumPriceNow) sumPriceNow,
                            sum(sumPriceDis) sumPriceDis,
                            sum(sumPriceTax) sumPriceTax,
                            sum(sumPriceAll) sumPriceAll 
                            from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }
            if (wlId != "")
            {
                sql += " and wlId in (select id from client where code in (" + wlId + ") ) ";
            }
            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            sql += " group by wlId,wlName,goodsId,code,goodsName,spec,unitName,ckId,ckName order by wlId asc,code asc ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query sales summary by category

        /// <summary>
        /// Query sales summary by category
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemDetailGroupByTypeName(DateTime start, DateTime end)
        {
            string sql = @"select isnull(sum(sumPrice*types),0) sumPrice,
                            isnull(sum(num),0) sumNum
                            from viewSalesReceiptItem where flag='Review' 
                            and bizDate<=@end and bizDate >=@start";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query monthly sales summary-----by year

        /// <summary>
        /// Query monthly sales summary-----by year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumByMoth(int year, int shopId)
        {
            string sql = @" select 
                            year(bizDate) years,
                            months,
                            sum(isnull(sumPriceAll,0)) sumPrice
                            from months a
                            left join viewSalesReceipt b on a.months=month(bizDate) and year(bizDate)=@year
                            and shopId=@shopId 
                            group by year(bizDate),months  order by months";

            SqlParameter[] param = {
                new SqlParameter("@year",year),
                new SqlParameter("@shopId",shopId)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion 

        #region Query monthly sales summary-----by year

        /// <summary>
        /// Query monthly sales summary-----by year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumByMoth(int year, string databaseURL)
        {
            string sql = @"select 
                            year(bizDate) years,
                            months,
                            sum(isnull(sumPrice,0)) sumPrice
                            from months a
                            left join viewSalesReceipt b on a.months=month(bizDate) and year(bizDate)=@year
                            group by year(bizDate),months  order by months";

            SqlParameter[] param = {
                new SqlParameter("@year",year)
            };

            return SQLHelper.SqlDataAdapter(databaseURL, CommandType.Text, sql, param);
        }

        #endregion 

        #region Query sales proportion-----by date range

        /// <summary>
        /// Query sales proportion-----by date range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet getSalesPercent(int shopId, DateTime start, DateTime end)
        {
            string sql = @"select typeId,typeName,
                            sum(sumPriceAll) sumPriceAll
                            from viewSalesReceiptItem where  flag='Review' 
                            and pId in(select id from SalesReceipt where shopId=@shopId )
                            group by typeId,typeName ";

            SqlParameter[] param = {
                new SqlParameter("@shopId",shopId)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion 

        #region Query sales proportion-----by date range

        /// <summary>
        /// Query sales proportion-----by date range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet getSalesPercent(DateTime start, DateTime end, string databaseURL)
        {
            string sql = @"select typeId,typeName,
                            sum(sumPrice) sumPrice
                            from viewSalesReceiptItem where  flag='review' 
                            group by typeId,typeName";

            return SQLHelper.SqlDataAdapter(databaseURL, CommandType.Text, sql, null);
        }

        #endregion 

        #region Query monthly sales summary-----by year------curve chart

        /// <summary>
        /// Query monthly sales summary-----by year------curve chart
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumNumByMoth(int year, int shopId)
        {
            string sql = @"select 
                            year(bizDate) years,
                            months,
                            sum(isnull(sumNum,0)) sumPrice
                            from months a
                            left join viewSalesReceipt b on a.months=month(bizDate) and year(bizDate)=@year
                            and shopId=@shopId 
                            group by year(bizDate),months  order by months";

            SqlParameter[] param = {
                new SqlParameter("@year",year),
                new SqlParameter("@shopId",shopId)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion 

        #region Query monthly sales summary-----by year------curve chart

        /// <summary>
        /// Query monthly sales summary-----by year------curve chart
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumNumByMoth(int year, string databaseURL)
        {
            string sql = @"select 
                            year(bizDate) years,
                            months,
                            sum(isnull(sumNum,0)) sumPrice
                            from months a
                            left join viewSalesReceipt b on a.months=month(bizDate) and year(bizDate)=@year
                            group by year(bizDate),months  order by months";

            SqlParameter[] param = {
                new SqlParameter("@year",year)
            };

            return SQLHelper.SqlDataAdapter(databaseURL, CommandType.Text, sql, param);
        }

        #endregion 
    }
}