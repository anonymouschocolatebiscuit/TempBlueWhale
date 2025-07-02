using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class AccountDAL
    {
        public AccountDAL()
        {

        }

        #region Member Fields

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

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private DateTime yueDate;
        public DateTime YueDate
        {
            get { return yueDate; }
            set { yueDate = value; }
        }

        private decimal yuePrice;
        public decimal YuePrice
        {
            get { return yuePrice; }
            set { yuePrice = value; }
        }

        private string types;
        public string Types
        {
            get { return types; }
            set { types = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        #endregion

        #region Member Methods

        /// <summary>
        /// Checks if the code exists -- when adding new
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from account where code='" + code + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Checks if the code exists -- when editing
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id, int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from account where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Checks if the name exists
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(int shopId, string names)
        {
            bool flag = false;

            string sql = "select * from account where names='" + names + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Checks if the name exists -- when editing
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int id, int shopId, string names)
        {
            bool flag = false;

            string sql = "select * from account where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// Checks if it is being referenced, can it be deleted?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckBeUsed(int id)
        {
            bool flag = false;

            string sql = " select * from PayMentAccountItem ReceivableAccountItem where bkId='" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        #endregion

        /// <summary>
        /// Get the list of data
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,code+' '+names CodeName ");
            strSql.Append(" FROM account ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by code  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #region Get All Members

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = " select *,code+' '+names CodeName from account order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Members

        /// <summary>
        /// Get all members by id
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select *,code+' '+names CodeName from account where id='" + id + "' order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 获取现金银行流水报表

        /// <summary>
        /// 获取现金银行流水报表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="bkId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReport(DateTime start, DateTime end, int bkId)
        {
            string sql = @"select bkId,code,bkName,'' wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            0 priceBegin,0 priceIn,0 priceOut,sum(priceIn-priceOut) priceEnd

                            from viewAccountFlow where bizDate<@start ";

            if (bkId != 0)
            {
                sql += " and bkId='" + bkId + "'";
            }

            sql += " group by bkId,code,bkName ";

            sql += " union ";

            sql += @"select bkId,code,bkName,wlName,number,bizType,bizDate,

                            priceBegin=isnull
                            (
                              ( select sum(priceIn-priceOut) from viewAccountFlow t1
                               where t1.bkId=bkId and t1.bizDate>bizDate
                              )
                            ,0),
                            priceIn,
                            priceOut,
                            priceEnd=isnull(

                              (   select sum(priceIn-priceOut) from viewAccountFlow 
                                  where t1.bkId=bkId and t1.bizDate>bizDate
                              ),0)
                            +priceIn-priceOut

                            from viewAccountFlow t1

                            where bizDate>=@start and bizDate<=@end ";

            if (bkId != 0)
            {
                sql += " and bkId='" + bkId + "'";
            }

            sql += " order by bkId,code,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@bkId",bkId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 获取现金银行流水报表-----------NewUI

        /// <summary>
        /// 获取现金银行流水报表-----------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="bkId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReport(int shopId, DateTime start, DateTime end, string code)
        {
            string sql = @"select bkId,code,bkName,'' wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            0 priceBegin,0 priceIn,0 priceOut,sum(priceIn-priceOut) priceEnd

                            from viewAccountFlow where bizDate<@start ";

            if (shopId != 0)
            {
                sql += " and shopId ='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and bkName in (" + code + ")";
            }

            sql += " group by bkId,code,bkName ";

            sql += " union all ";

            sql += @"select bkId,code,bkName,wlName,number,bizType,bizDate,

                            priceBegin=isnull
                            (
                              ( select sum(priceIn-priceOut) from viewAccountFlow t1
                               where t1.bkId=bkId and t1.bizDate>bizDate
                              )
                            ,0),
                            priceIn,
                            priceOut,
                            priceEnd=isnull(

                              (   select sum(priceIn-priceOut) from viewAccountFlow 
                                  where t1.bkId=bkId and t1.bizDate>bizDate
                              ),0)
                            +priceIn-priceOut

                            from viewAccountFlow t1

                            where bizDate>=@start and bizDate<=@end ";

            if (shopId != 0)
            {
                sql += " and shopId ='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and bkName in (" + code + ")";
            }


            sql += " order by bkId,code,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)

                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region Get Vender Payable Amount Report

        /// <summary>
        /// Get Vender Payable Amount Report
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReportVenderNeedPay(DateTime start, DateTime end, int wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            0 payNeed,sum(payReady) payReady,sum(payNeed-payReady) payEnd

                            from viewVenderNeedPayFlow where bizDate<@start ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }

            sql += " group by wlId,wlName ";

            sql += " union ";

            sql += @"select wlId,wlName,number,bizType,bizDate,payNeed,payReady,payNeed-payReady payEnd

                            from viewVenderNeedPayFlow

                            where bizDate>=@start and bizDate<=@end ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }


            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Vender Payable Amount Report-------------NewUI

        /// <summary>
        /// Get Vender Payable Amount Report-------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReportVenderNeedPay(int shopId, DateTime start, DateTime end, string wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            0 payNeed,sum(payReady) payReady,sum(payNeed-payReady) payEnd

                            from viewVenderNeedPayFlow where bizDate<@start ";

            if (shopId != 0)
            {
                sql += " and shopId ='" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }

            sql += " group by wlId,wlName ";

            sql += " union all ";

            sql += @"select wlId,wlName,number,bizType,bizDate,payNeed,payReady,payNeed-payReady payEnd

                            from viewVenderNeedPayFlow

                            where bizDate>=@start and bizDate<=@end ";

            if (shopId != 0)
            {
                sql += " and shopId ='" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }


            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        #region 获取客户应收账款报表

        /// <summary>
        /// 获取客户应收账款报表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReportClientNeedPay(DateTime start, DateTime end, int wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            sum(payNeed) payNeed,sum(payReady) payReady,sum(payNeed-payReady) payEnd

                            from viewClientNeedPayFlow where bizDate<@start ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }

            sql += " group by wlId,wlName ";

            sql += " union ";

            sql += @"select wlId,wlName,number,bizType,bizDate,payNeed,payReady,payNeed-payReady payEnd

                            from viewClientNeedPayFlow

                            where bizDate>=@start and bizDate<=@end ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }


            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 获取客户应收账款报表-----------------NewUI

        /// <summary>
        /// 获取客户应收账款报表-----------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReportClientNeedPay(int shopId, DateTime start, DateTime end, string wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            sum(payNeed) payNeed,sum(payReady) payReady,sum(payNeed-payReady) payEnd

                            from viewClientNeedPayFlow where bizDate<@start ";
            if (shopId != 0)
            {
                sql += " and shopId ='" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }

            sql += " group by wlId,wlName ";

            sql += " union all ";

            sql += @"select wlId,wlName,number,bizType,bizDate,payNeed,payReady,payNeed-payReady payEnd

                            from viewClientNeedPayFlow

                            where bizDate>=@start and bizDate<=@end ";

            if (shopId != 0)
            {
                sql += " and shopId ='" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }


            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 获取客户对账单报表

        /// <summary>
        /// 获取客户对账单报表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelClientStatement(DateTime start, DateTime end, int wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            sum(sumPrice) sumPrice,sum(disPrice) disPrice,0 payNeed,0 payReady,sum(payNeed-payReady) payEnd

                            from viewStatementClient where bizDate<@start ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }

            sql += " group by wlId,wlName ";

            sql += " union ";

            sql += @"select wlId,wlName,number,bizType,bizDate,sumPrice,disPrice,payNeed,payReady,payNeed-payReady payEnd

                            from viewStatementClient

                            where bizDate>=@start and bizDate<=@end ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }


            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 获取客户对账单报表----------NewUI

        /// <summary>
        /// 获取客户对账单报表----------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelClientStatement(int shopId, DateTime start, DateTime end, string wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            sum(sumPrice) sumPrice,sum(disPrice) disPrice,0 payNeed,0 payReady,sum(payNeed-payReady) payEnd

                            from viewStatementClient where bizDate<@start ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";

            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }

            sql += " group by wlId,wlName ";

            sql += " union ";

            sql += @"select wlId,wlName,number,bizType,bizDate,sumPrice,disPrice,payNeed,payReady,payNeed-payReady payEnd

                            from viewStatementClient

                            where bizDate>=@start and bizDate<=@end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";

            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }


            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region Get Vender Statement Report

        /// <summary>
        /// Get Vender Statement Report
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelVenderStatement(DateTime start, DateTime end, int wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            sum(sumPrice) sumPrice,sum(disPrice) disPrice,0 payNeed,0 payReady,sum(payNeed-payReady) payEnd

                            from viewStatementVender where bizDate<@start ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }

            sql += " group by wlId,wlName ";

            sql += " union ";

            sql += @"select wlId,wlName,number,bizType,bizDate,sumPrice,disPrice,payNeed,payReady,payNeed-payReady payEnd

                            from viewStatementVender

                            where bizDate>=@start and bizDate<=@end ";

            if (wlId != 0)
            {
                sql += " and wlId='" + wlId + "'";
            }

            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Vender Statement Report---------------NewUI

        /// <summary>
        /// Get Vender Statement Report---------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelVenderStatement(int shopId, DateTime start, DateTime end, string wlId)
        {
            string sql = @"select wlId, wlName,'' number,'Opening Balance' bizType,'' bizDate,

                            sum(sumPrice) sumPrice,sum(disPrice) disPrice,0 payNeed,0 payReady,sum(payNeed-payReady) payEnd

                            from viewStatementVender where bizDate<@start ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";

            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }

            sql += " group by wlId,wlName ";

            sql += " union ";

            sql += @"select wlId,wlName,number,bizType,bizDate,sumPrice,disPrice,payNeed,payReady,payNeed-payReady payEnd

                            from viewStatementVender

                            where bizDate>=@start and bizDate<=@end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";

            }

            if (wlId != "")
            {
                sql += " and wlName in(" + wlId + ")";
            }

            sql += " order by wlId,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@wlId",wlId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 获取其他收支报表-------------NewUI

        /// <summary>
        /// 获取其他收支报表-------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetOtherGetPayFlowReport(int shopId, DateTime start, DateTime end, string bizType, string typeIdString, string bizId)
        {
            string sql = @"select * from viewOtherGetPayFlow where bizDate>=@start and bizDate<=@end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "'  ";
            }

            if (bizType != "ALL")
            {
                sql += " and bizType='" + bizType + "'  ";
            }

            if (typeIdString != "")
            {
                sql += " and typeId in(" + typeIdString + ") ";
            }

            if (bizId != "0")
            {
                sql += " and bizId='" + bizId + "'  ";
            }

            sql += " order by wlName,bizDate";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 新增成员信息
        /// <summary>
        /// Add new member information
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into account(shopId,code,names,yueDate,yuePrice,types,makeDate) values('" + ShopId + "','" + Code + "','" + Names + "','" + YueDate + "','" + YuePrice + "','" + Types + "',getdate())";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update Member Information
        /// <summary>
        /// Update member information
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = "";
            if (!this.isExistsCodeEdit(Id, ShopId, Code) && !this.isExistsNamesEdit(Id, ShopId, Names))
            {
                sql = "update account set shopId='" + shopId + "',Names='" + Names + "',Code='" + Code + "',YueDate='" + YueDate + "',YuePrice='" + YuePrice + "',Types='" + Types + "' where Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Delete Member Information
        /// <summary>
        /// Delete member information
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from account where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
