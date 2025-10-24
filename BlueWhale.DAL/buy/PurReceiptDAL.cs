using System;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class PurReceiptDAL
    {
        public PurReceiptDAL()
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



        #endregion

        #region Get

        /// <summary>
        /// Get Purchase Receipt
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end, int types)
        {
            string sql = "select * from viewPurReceipt  where bizDate>=@start and bizDate<=@end  ";

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

        /// <summary>
        /// View Purchase Receipt by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewPurReceipt where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
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

            string sql = "insert into purReceipt(shopId,number,wlId,bizDate,types,remarks,dis,disPrice,sumPrice,payNow,payNowNo,bkId,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@types,@remarks,@dis,@disPrice,@sumPrice,@payNow,@payNowNo,@bkId,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
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

        #region Query Purchase Receipt

        /// <summary>
        /// Query Purchase Receipt
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetAllModelByWLId(string key, DateTime start, DateTime end, int venderId)
        {
            string sql = "select * from viewPurReceipt  where bizDate>=@start and bizDate<=@end and wlId='" + venderId + "' and flag='审核' and (isnull(sumPriceAll,0)-isnull(priceCheckNowSum,0))> 0 ";
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

        #region Audit Data

        /// <summary>
        /// Audit Data
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from PurReceipt where flag='" + flag + "' and id='" + Id + "') ";

            sql += " begin ";

            sql += " update PurReceipt set flag='" + flag + "' ";

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

        #region Delete Purchase Receipt Item
        /// <summary>
        /// Delete Purchase Receipt Item
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";

            sql += " select * from purReceipt where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from purReceiptItem  where pId='" + Id + "' delete from purReceipt where Id='" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        public string GetBillNumberAuto(int shopId)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","CGRK"),
                                       new SqlParameter("@tableName","purReceipt")
                                     };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();

        }

        #region Search Purchase Order List Sum Goods-----------By Item-------NewUI

        /// <summary>
        /// Search Purchase Order List Sum Goods-----------By Item-------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetPurReceiptItemSumGoods(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(num) sumNum,
                            sum(sumPriceNow) sumPriceNow,
                            sum(sumPriceDis) sumPriceDis,
                            sum(sumPriceTax) sumPriceTax,
                            sum(sumPriceAll) sumPriceAll 

                            from viewPurReceiptDetail where bizDate<=@end and bizDate >=@start ";

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
                sql += " and wlId in (select id from vender where code in (" + wlId + ") ) ";
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

        #region  Search Purchase Order List Sum Vender-----------By Vender-------NewUI

        /// <summary>
        ///  Search Purchase Order List Sum Vender-----------By Vender-------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetPurReceiptItemSumVender(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = @"select wlId,wlName,goodsId,code,goodsName,spec,unitName,ckId,ckName,
                           sum(num) sumNum,
                            sum(sumPriceNow) sumPriceNow,
                            sum(sumPriceDis) sumPriceDis,
                            sum(sumPriceTax) sumPriceTax,
                            sum(sumPriceAll) sumPriceAll 
                      
                           from viewPurReceiptDetail where bizDate<=@end and bizDate >=@start ";

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
                sql += " and wlId in (select id from vender where code in (" + wlId + ") ) ";
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

        #region Search product purchase details-------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetPurReceiptItemDetail(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = "select * from viewPurReceiptDetail where bizDate<=@end and bizDate >=@start ";

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
                sql += " and wlId in (select id from vender where code in (" + wlId + ") ) ";
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
    }
}
