using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class SalesOrderDAL
    {
        public SalesOrderDAL()
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

        private DateTime sendDate;
        public DateTime SendDate
        {
            get { return sendDate; }
            set { sendDate = value; }
        }

        private decimal dis;
        public decimal Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        private decimal sumPriceDis;
        public decimal SumPriceDis
        {
            get { return sumPriceDis; }
            set { sumPriceDis = value; }
        }

        private decimal sumPriceAll;
        public decimal SumPriceAll
        {
            get { return sumPriceAll; }
            set { sumPriceAll = value; }
        }

        private decimal sumPriceWY;
        public decimal SumPriceWY
        {
            get { return sumPriceWY; }
            set { sumPriceWY = value; }
        }

        private decimal sumPricePayReady;
        public decimal SumPricePayReady
        {
            get { return sumPricePayReady; }
            set { sumPricePayReady = value; }
        }

        private decimal sumPricePayNeed;
        public decimal SumPricePayNeed
        {
            get { return sumPricePayNeed; }
            set { sumPricePayNeed = value; }
        }

        private int sendTypeId;
        public int SendTypeId
        {
            get { return sendTypeId; }
            set { sendTypeId = value; }
        }

        private decimal sendPrice;
        public decimal SendPrice
        {
            get { return sendPrice; }
            set { sendPrice = value; }
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

        #region AutoGenerateInvoiceNo

        /// <summary>
        /// AutoGenerateInvoiceNo
        /// </summary>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {

                                      new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","XSDD"),   //The first four letters of the bill
                                       new SqlParameter("@tableName","salesOrder") //Table

                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = @"insert into salesOrder(
                                                    shopId
                                                   ,number
                                                   ,wlId
                                                   ,bizDate
                                                   ,sendDate
                                                   ,remarks
                                                   ,makeId
                                                   ,makeDate
                                                   ,bizId
                                                   ,flag
                                                    )
                                                     values(
                                                    @shopId
                                                   ,@number
                                                   ,@wlId
                                                   ,@bizDate
                                                   ,@sendDate
                                                   ,@remarks
                                                   ,@makeId
                                                   ,@makeDate
                                                   ,@bizId
                                                   ,@flag)   select @@identity ";
            SqlParameter[] param = {

                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@SendDate",SendDate),
                                       new SqlParameter("@Remarks",Remarks),
                                       new SqlParameter("@makeId",MakeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@BizId",BizId),
                                       new SqlParameter("@BizDate",BizDate),
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

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        public int Update()
        {

            string sql = @"UPDATE salesOrder
                                       SET shopId = @shopId                                       
                                          ,wlId = @wlId
                                          ,sendDate = @sendDate
                                          ,remarks = @remarks
                                          ,bizId = @bizId
                                          ,bizDate = @bizDate
                                        
                                     WHERE id=@id and flag<>'Review'  ";
            SqlParameter[] param = {

                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@SendDate",SendDate),
                                       new SqlParameter("@Remarks",Remarks),
                                       new SqlParameter("@BizId",BizId),
                                       new SqlParameter("@BizDate",BizDate),
                                       new SqlParameter("@Id",Id)
                                       };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += "               select * from salesOrder where flag='Review' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from salesOrderPay   where pId='" + Id
                + "' delete from salesOrderItem  where pId='" + Id
                + "' delete from SalesOrderAddress where pId='" + Id
                + "' delete from salesOrder where Id='" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetAllModel

        /// <summary>
        /// GetAllModel
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from viewSalesOrder order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetAllModel

        /// <summary>
        /// GetAllModel
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end, int types)
        {
            string sql = "select * from viewSalesOrderList  where bizDate>=@start and bizDate<=@end  ";

            if (shopId != 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and (wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region Query sales order

        /// <summary>
        /// Query sales order
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, DateTime start, DateTime end)
        {
            string sql = "select * from viewSalesOrderList  where bizDate>=@start and bizDate<=@end  and flag='Review' ";

            if (shopId != 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }



            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion



        #region Query sales order --- Pending processing"

        /// <summary>
        /// Query sales order --- Pending processing"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModelCheckNo(int shopId)
        {
            string sql = "select * from viewSalesOrderList  where  flag='pending' ";

            if (shopId != 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }



            sql += " order by number ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region Query sales order

        /// <summary>
        /// Query sales order
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int types, int clientId)
        {
            string sql = "select * from viewSalesOrderList  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
            if (key != "")
            {
                sql += " and (number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
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



        #region Query sales order ----- Apple client

        /// <summary>
        /// Query sales order ----- Apple client
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, string flag)
        {
            string sql = "select * from viewSalesOrderList  where bizDate>=@start and bizDate<=@end  ";
            if (key != "")
            {
                sql += " and (number like '%" + key + "%' or wlName  like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }
            if (flag != "")
            {
                sql += " and flag = '" + flag + "' ";
            }
            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }





        #endregion


        #region Query order information ---------- By number
        /// <summary>
        /// Query order information ---------- By number
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetOrderList(string number)
        {

            string sql = " select * from  viewSalesOrderList where number='" + number + "'";

            sql += " order by id desc ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion


        #region Query order information ---------- By number
        /// <summary>
        /// Query order information ---------- By number
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetOrderList(int id)
        {

            string sql = " select * from  viewSalesOrderList where id='" + id + "'";

            sql += " order by id desc ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion


        #region Update payment status

        /// <summary>
        /// Update payment status
        /// </summary>
        /// <param name="number">System order number</param>
        /// <param name="flagPay">Payment status</param>
        /// <param name="payNumber">Payment transaction number</param>
        /// <returns></returns>
        public int UpdatePayFlag(string number, string flagPay, string payNumber)
        {

            string sql = "update salesOrder set payType='ApplePay',flagPay='" + flagPay + "',payNumber='" + payNumber + "',payDate=getdate()  where number='" + number + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }


        #endregion


        #region Query sales order -------------- Customer search

        /// <summary>
        /// Query sales order -------------- Customer search
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListByClient(string key, DateTime start, DateTime end, int types, int clientId)
        {

            string sql = @"select a.*,
                                isnull(b.sumNumGet,0) sumNumGet,
                                a.sumNum-isnull(b.sumNumGet,0) sumNumGetNo,
                                isnull(c.payPriceSum,0) sumPricePay,
                                a.sumPriceAll-isnull(c.payPriceSum,0) sumPricePayNo
                                 
                                from viewSalesOrderList a
                                left join
                                (
	                                select sourceNumber,
	                                sum(num) sumNumGet 
	                                from viewSalesReceiptItem
	                                where flag='Review'
	                                group by sourceNumber
                                )
                                b on a.number=b.sourceNumber

                                left join 
                                (
                                   select * from viewReceivable
                                )
                                c on a.number=c.orderNumber";

            sql += "  where a.bizDate>=@start and a.bizDate<=@end ";

            if (clientId != 0)
            {
                sql += " and a.wlId='" + clientId + "'  ";
            }

            if (key != "")
            {
                sql += " and (a.number like '%" + key + "%' or a.remarks  like '%" + key + "%' ) ";
            }


            if (types != 0) //0 All, 1 Not In, 2 Partially In, 3 Fully In 
            {
                if (types == 1)
                {
                    sql += " and isnull(b.sumNumGet,0) = 0 ";
                }

                if (types == 2)
                {
                    sql += " and a.sumNum>isnull(b.sumNumGet,0) and isnull(b.sumNumGet,0)<>0 ";
                }

                if (types == 3)
                {
                    sql += " and a.sumNum = isnull(b.sumNumGet,0) ";
                }


            }



            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region Query sales order -------------- Customer search

        /// <summary>
        /// Query sales order -------------- Customer search
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListByClient(string key, DateTime start, DateTime end, int types, string openId)
        {

            string sql = @" select  a.*,
                                isnull(b.sumNumGet,0) sumNumGet,
                                a.sumNum-isnull(b.sumNumGet,0) sumNumGetNo,
                                isnull(c.payPriceSum,0) sumPricePay,
                                a.sumPriceAll-isnull(c.payPriceSum,0) sumPricePayNo
                                 
                                from viewSalesOrderList a
                                left join
                                (
	                                select sourceNumber,
	                                sum(num) sumNumGet 
	                                from viewSalesReceiptItem
	                                where flag='Review'
	                                group by sourceNumber
                                )
                                b on a.number=b.sourceNumber

                                left join 
                                (
                                   select * from viewReceivable
                                )
                                c on a.number=c.orderNumber";

            sql += "  where a.bizDate>=@start and a.bizDate<=@end ";

            if (openId != "")
            {
                sql += " and a.wlId=(select top 1 id from viewClient where openId= '" + openId + "' ) ";
            }

            if (key != "")
            {
                sql += " and (a.number like '%" + key + "%' or a.remarks  like '%" + key + "%' ) ";
            }


            if (types != 0) //0 All, 1 Not In, 2 Partially In, 3 Fully In 
            {
                if (types == 1)
                {
                    sql += " and isnull(b.sumNumGet,0) = 0 ";
                }

                if (types == 2)
                {
                    sql += " and a.sumNum>isnull(b.sumNumGet,0) and isnull(b.sumNumGet,0)<>0 ";
                }

                if (types == 3)
                {
                    sql += " and a.sumNum = isnull(b.sumNumGet,0) ";
                }


            }



            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region Query sales order -------------- Customer query

        /// <summary>
        /// Query sales order -------------- Customer query
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListByClient(int id)
        {

            string sql = @" select a.*,
                                isnull(b.sumNumGet,0) sumNumGet,
                                a.sumNum-isnull(b.sumNumGet,0) sumNumGetNo,
                                isnull(c.payPriceSum,0) sumPricePay,
                                a.sumPriceAll-isnull(c.payPriceSum,0) sumPricePayNo
                                 
                                from viewSalesOrderList a
                                left join
                                (
	                                select sourceNumber,
	                                sum(num) sumNumGet 
	                                from viewSalesReceiptItem
	                                where flag='Review'
	                                group by sourceNumber
                                )
                                b on a.number=b.sourceNumber

                                left join 
                                (
                                   select * from viewReceivable
                                )
                                c on a.number=c.orderNumber";

            sql += "  where 1=1 ";

            if (id != 0)
            {
                sql += " and a.id = '" + id + "'  ";
            }



            sql += " order by number ";



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region Query sales order — By order number

        /// <summary>
        /// Query sales order — By order number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewSalesOrderList where id='" + id + "' order by number ";

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
            string sql = " if not exists(select * from salesOrder where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update salesOrder set flag='" + flag + "' ";
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

        #region Query sales order ---------- Tracking sheet

        /// <summary>
        /// Query sales order ---------- Tracking sheet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetSalesOrderListReport(DateTime start, DateTime end, DateTime sendStart, DateTime sendEnd, int wlId, int goodsId, int types)
        {
            string sql = "select * from viewSalesOrderListReport  where bizDate>=@start and bizDate<=@end  and sendDate>=@sendStart and sendDate<=@sendEnd ";

            if (wlId != 0)
            {
                sql += " and wlId = '" + wlId + "' ";
            }

            if (goodsId != 0)
            {
                sql += " and goodsId = '" + goodsId + "' ";
            }

            if (types != 0) //0 All, 1 Not In, 2 Partially In, 3 Fully In 
            {
                if (types == 1) //Not in
                {
                    sql += " and getNumNo = num ";
                }

                if (types == 2) //Partially num>getNum and getNumNo<>0
                {
                    sql += " and getNum>0 and num>getNum ";
                }

                if (types == 3) //Fully in
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


        #region Query sales order ---------- Tracking sheet ---------- NewUI

        /// <summary>
        /// Query sales order ---------- Tracking sheet ---------- NewUI
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetSalesOrderListReport(int shopId, DateTime start, DateTime end, DateTime sendStart, DateTime sendEnd, string wlId, string code, string types)
        {

            string sql = "select * from viewSalesOrderListReport  where bizDate>=@start and bizDate<=@end  and sendDate>=@sendStart and sendDate<=@sendEnd ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlId in (select id from client where code in (" + wlId + ") )";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            if (types != "") //0 All 1 Not Out 2 Partially Out 3 All Out
            {
                sql += " and ( ";
                if (types.Contains("Not Out"))
                {
                    sql += "  getNumNo = num ";
                }

                if (types.Contains("Not Out") && types.Contains("Partially Out"))
                {
                    sql += " or (getNum>0 and num>getNum )";
                }

                if (!types.Contains("Not Out") && types.Contains("Partially Out"))
                {
                    sql += " (getNum>0 and num>getNum )";
                }


                if (types.Contains("All Out") && !types.Contains("Not Out") && !types.Contains("Partially Out"))
                {
                    sql += " getNumNo <= 0 ";
                }

                if (types.Contains("All Out") && (types.Contains("Not Out") || types.Contains("Partially Out")))
                {
                    sql += " or getNumNo <= 0 ";
                }

                sql += " )";

                //if (types == 1)//Not Out
                //{
                //    sql += " and getNumNo = num ";
                //}

                //if (types == 2)//Partially Out
                //{
                //    sql += " and getNum>0 and num>getNum ";
                //}

                //if (types == 3)//All Out
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

        //The following is the online order 

        #region Retrieve cart information 
        /// <summary>
        /// Retrieve cart information — Return dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetCartListByClientId(int clientId)
        {
            string sql = "select * from viewCartList where 1=1 ";
            if (clientId != 0)
            {
                sql += " and  clientId='" + clientId + "' ";
            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion


        #region Retrieve cart information
        /// <summary>
        /// Retrieve cart information — Return dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetCartListByClientId(string openId)
        {
            string sql = "select * from viewCartList where 1=1 ";
            if (openId != "")
            {
                sql += " and  openId='" + openId + "' ";
            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion


        #region Retrieve cart information — Summary of quantity, amount, etc
        /// <summary>
        /// Retrieve cart information — Summary of quantity, amount, etc
        /// </summary>
        /// <returns></returns>
        public DataSet GetCartListSumByClientId(int clientId)
        {
            string sql = "select clientId,sum(num) sumNum,sum(salesPrice*num) sumSalesPrice,sum(sumPrice) sumPriceVip from viewCartList where 1=1 ";
            if (clientId != 0)
            {
                sql += " and  clientId='" + clientId + "' ";
            }
            sql += " group by clientId ";



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region Get cart information — Summary of quantity, amount, etc.
        /// <summary>
        /// Get cart information — Summary of quantity, amount, etc.
        /// </summary>
        /// <returns></returns>
        public DataSet GetCartListSumByClientId(string openId)
        {
            string sql = "select clientId,sum(num) sumNum,sum(salesPrice*num) sumSalesPrice,sum(sumPrice) sumPriceVip from viewCartList where 1=1 ";
            if (openId != "")
            {
                sql += " and  openId='" + openId + "' ";
            }
            sql += " group by clientId ";



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region Add cart information
        /// <summary>
        /// Add cart information
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="goodsId"></param>
        /// <param name="salesPrice"></param>
        /// <param name="salesPriceVip"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int AddCartList(int clientId, int goodsId, decimal salesPrice, decimal salesPriceVip, decimal num)
        {
            string sql = "if not exists(select * from cartlist where clientId='" + clientId + "' and goodsId='" + goodsId + "') ";
            sql += "  insert into cartList(clientId,goodsId,salesPrice,salesPriceVip,num,sumPrice) values('" + clientId + "','" + goodsId + "','" + salesPrice + "','" + salesPriceVip + "','" + num + "','" + num * salesPriceVip + "')";

            sql += " else ";

            sql += " update cartList set num='" + num + "',salesPrice='" + salesPrice + "',salesPriceVip='" + salesPriceVip + "',sumPrice='" + num * salesPriceVip + "' where clientId='" + clientId + "' and goodsId='" + goodsId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add cart information
        /// <summary>
        /// Add cart information
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="goodsId"></param>
        /// <param name="salesPrice"></param>
        /// <param name="salesPriceVip"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int AddCartList(string openId, int goodsId, decimal salesPrice, decimal salesPriceVip, decimal num)
        {
            string sql = "if not exists(select * from cartlist where openId='" + openId + "' and goodsId='" + goodsId + "') ";
            sql += "  insert into cartList(openId,goodsId,salesPrice,salesPriceVip,num,sumPrice) values('" + openId + "','" + goodsId + "','" + salesPrice + "','" + salesPriceVip + "','" + num + "','" + num * salesPriceVip + "')";

            sql += " else ";

            sql += " update cartList set num='" + num + "',salesPrice='" + salesPrice + "',salesPriceVip='" + salesPriceVip + "',sumPrice='" + num * salesPriceVip + "' where openId='" + openId + "' and goodsId='" + goodsId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion





        #region Delete cart information

        /// <summary>
        /// Delete cart information
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteCartListById(int Id)
        {
            string sql = "delete from cartList where Id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion





        #region Delete cart information — Clear all

        /// <summary>
        /// Delete cart information — Clear all
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteCartListByClientId(int Id)
        {
            string sql = "delete from cartList where clientId='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region Delete cart information — Clear all

        /// <summary>
        /// Delete cart information — Clear all
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteCartListByClientId(string openId)
        {
            string sql = "delete from cartList where openId='" + openId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region Add new order delivery address
        /// <summary>
        /// Add new order delivery address
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="proId"></param>
        /// <param name="ctId"></param>
        /// <param name="areaId"></param>
        /// <param name="address"></param>
        /// <param name="postCode"></param>
        /// <param name="names"></param>
        /// <param name="phone"></param>
        /// <param name="tel"></param>
        /// <returns></returns>
        public int AddSalesOrdersAddress(int pId, int proId, int ctId, int areaId, string address, string postCode, string names, string phone, string tel)
        {
            string sql = "insert into SalesOrderAddress(pId,proId,ctId,areaId,dizhi,postCode,names,phone,tel)  ";
            sql += " values('" + pId
                + "','" + proId
                + "','" + ctId
                + "','" + areaId
                + "','" + address
                + "','" + postCode
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "') ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region Query the order delivery address

        /// <summary>
        /// Query the order delivery address
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetSalesOrdersAddress(int pId)
        {
            string sql = "select *,proName+','+ctName+','+areaName+','+dizhi+' '+phone dizhiAll from viewSalesOrderAddress where pId='" + pId + "'";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region Delete order delivery information

        /// <summary>
        /// Delete order delivery information
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteSalesOrdersAddress(int Id)
        {
            string sql = "delete from SalesOrdersAddress where pId='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion



        #region Query the order shipment status, shipping address, and tracking number

        /// <summary>
        /// Query the order shipment status, shipping address, and tracking number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetSalesOrdersSendAddress(string number)
        {
            string sql = "select * from viewSalesReceiptList where id in(select distinct pId from salesReceiptItem where sourceNumber='" + number + "' )";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

    }


}
