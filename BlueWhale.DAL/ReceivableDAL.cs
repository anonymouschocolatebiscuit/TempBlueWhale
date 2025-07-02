using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class ReceivableDAL
    {
        public ReceivableDAL()
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

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
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

        #region 自动生成单据编号

        /// <summary>
        /// 生成单据编号
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {

                                       new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","XSSK"),//单据代码前四位字母
                                       new SqlParameter("@tableName","Receivable")//表

                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();

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

            string sql = "insert into Receivable(shopId,number,wlId,bizDate,makeId,makeDate,disPrice,PayPriceNowMore,flag,remarks,orderNumber)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@makeId,@makeDate,@disPrice,@PayPriceNowMore,@flag,@remarks,@orderNumber)   select @@identity ";
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
                                       new SqlParameter("@Remarks",Remarks),
                                       new SqlParameter("@OrderNumber",OrderNumber)

                                     };

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, param);
            if (sdr.Read())
            {
                id = sdr[0].ToString();

            }

            return int.Parse(id);
        }

        #endregion

        #region 修改一条记录

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <returns></returns>
        public int Update()
        {

            string sql = @"UPDATE Receivable
                                       SET 
                                           wlId = @wlId    
                                          ,shopId = @shopId                                   
                                          ,bizDate = @bizDate                                      
                                          ,makeId = @makeId                                                                                         
                                          ,makeDate = @makeDate
                                          ,DisPrice = @DisPrice                                                                                         
                                          ,PayPriceNowMore = @PayPriceNowMore
                                                                 
                                          ,Flag = @Flag 
                                          ,remarks = @remarks
                                          ,orderNumber = orderNumber                
                                        
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

        #region 删除成员信息

        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += "               select * from Receivable where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from ReceivableAccountItem  where pId='" + Id + "' delete from ReceivableSourceBillItem  where pId='" + Id + "' delete from Receivable where Id='" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from viewReceivable order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion

        #region 查询收款单

        /// <summary>
        /// 查询收款单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewReceivable  where bizDate>=@start and bizDate<=@end  ";

            if (shopId != 0)
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

        #region 查询收款单

        /// <summary>
        /// 查询收款单
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int wlId, DateTime start, DateTime end, string number)
        {
            string sql = "select *,'收款' types from viewReceivable  where bizDate>=@start and bizDate<=@end and wlId='" + wlId + "' and (isnull(payPriceSum,0)-isnull(priceCheckNowSum,0))> 0 ";
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

        #region 查询收款单-------待审核的

        /// <summary>
        /// 查询收款单-------待审核的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModelCheckNo(int shopId)
        {
            string sql = "select * from viewReceivable  where flag<>'审核'  ";

            sql += " order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 查询收款单-----通过编号

        /// <summary>
        /// 查询收款单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewReceivable where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 查询收款单-----通过编号

        /// <summary>
        /// 查询收款单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewReceivable where number='" + number + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 查询收款单-----通过订单编号

        /// <summary>
        /// 查询收款单-----通过订单编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModelByOrderNumber(string number)
        {
            string sql = "select * from viewReceivable where orderNumber='" + number + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
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
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from Receivable where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update Receivable set flag='" + flag + "' ";
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

    }
}
