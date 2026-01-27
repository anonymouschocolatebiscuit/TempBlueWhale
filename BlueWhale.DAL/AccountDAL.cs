using BlueWhale.DBUtility;
using System;
using System.Collections.Generic;
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

            string sql = "SELECT * FROM account WITH (NOLOCK) WHERE code = @code AND shopId = @shopId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@code", code),
                new SqlParameter("@shopId", shopId)
            };

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, parameters);
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

            string sql = "select * from account WITH (NOLOCK) where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

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

            string sql = "SELECT 1 FROM account WITH (NOLOCK) WHERE names = @names AND shopId = @shopId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@names", names),
                new SqlParameter("@shopId", shopId)
            };

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

            string sql = "SELECT 1 FROM account WITH (NOLOCK) WHERE names = @names AND id <> @id AND shopId = @shopId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@names", names),
                new SqlParameter("@id", id),
                new SqlParameter("@shopId", shopId)
            };

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

            string sql = "SELECT 1 FROM PayMentAccountItem WITH (NOLOCK) WHERE bkId = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

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
            strSql.Append(" FROM account WITH (NOLOCK)");
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
            string sql = " select *,code+' '+names CodeName from account WITH (NOLOCK) order by code ";

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
            string sql = "select *,code+' '+names CodeName from account WITH (NOLOCK) where id='" + id + "' order by code ";

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

        #region Get All Model Report Client Need Pay

        /// <summary>
        /// GetAllModelReportClientNeedPay
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

        #region GetAll Model Report Client Need Pay-----------------NewUI

        /// <summary>
        /// GetAllModelReportClientNeedPay-----------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetAllModelReportClientNeedPay(int shopId, DateTime start, DateTime end, string wlId)
        {
            string sql = @"select wlId,wlName,number,

                            CASE 
                                WHEN bizType = N'期初收款' THEN 'Opening Balance'
                                WHEN bizType = N'其他收款' THEN 'Other Balance'
                                WHEN bizType = N'销售收款' THEN 'Sales Balance'
                                ELSE 'Default Balance'
                            END AS bizType,

                            bizDate,payNeed,payReady,payNeed-payReady payEnd

                            from viewClientNeedPayFlow

                            where bizDate>=@start AND bizDate<=@end";

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

        #region Get All Model Client Statement

        /// <summary>
        /// GetAllModelClientStatement
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

        #region Get AllModel Client Statement----------NewUI

        /// <summary>
        /// GetAllModelClientStatement----------NewUI
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

        #region Get Other Get Pay Flow Report-------------NewUI

        /// <summary>
        /// GetOtherGetPayFlowReport-------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <returns></returns>
        public DataSet GetOtherGetPayFlowReport(int shopId, DateTime start, DateTime end, string bizType, string typeIdString, string bizId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM viewOtherGetPayFlow WHERE bizDate >= @start AND bizDate <= @end");

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@start", start),
                new SqlParameter("@end", end)
            };

            if (shopId >= 0)
            {
                sql.Append(" AND shopId = @shopId");
                parameters.Add(new SqlParameter("@shopId", shopId));
            }

            if (bizType != "ALL")
            {
                sql.Append(" AND bizType = @bizType");
                parameters.Add(new SqlParameter("@bizType", bizType));
            }

            if (!string.IsNullOrWhiteSpace(typeIdString))
            {
                // typeIdString 必須是數字列表，例如 "1,2,3"
                sql.Append(" AND typeId IN (" + typeIdString + ")");
                // 這裡 IN 無法用參數化，只能保證 typeIdString 已驗證為安全數字列表
            }

            if (bizId != "0")
            {
                sql.Append(" AND bizId = @bizId");
                parameters.Add(new SqlParameter("@bizId", bizId));
            }

            sql.Append(" ORDER BY wlName, bizDate");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql.ToString(), parameters.ToArray());
        }


        #endregion

        #region Add new member information
        /// <summary>
        /// Add new member information
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @" INSERT INTO account 
                            (shopId, code, names, yueDate, yuePrice, types, makeDate) 
                            VALUES 
                            (@ShopId, @Code, @Names, @YueDate, @YuePrice, @Types, GETDATE())";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ShopId", ShopId),
                new SqlParameter("@Code", Code),
                new SqlParameter("@Names", Names),
                new SqlParameter("@YueDate", YueDate),
                new SqlParameter("@YuePrice", YuePrice),
                new SqlParameter("@Types", Types)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, parameters);
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
            if (!this.isExistsCodeEdit(Id, ShopId, Code) && !this.isExistsNamesEdit(Id, ShopId, Names))
            {
                string sql = @" UPDATE account WITH (ROWLOCK)
                                SET shopId = @shopId,
                                    Names = @Names,
                                    Code = @Code,
                                    YueDate = @YueDate,
                                    YuePrice = @YuePrice,
                                    Types = @Types
                                WHERE Id = @Id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@shopId", ShopId),
                    new SqlParameter("@Names", Names),
                    new SqlParameter("@Code", Code),
                    new SqlParameter("@YueDate", YueDate),
                    new SqlParameter("@YuePrice", YuePrice),
                    new SqlParameter("@Types", Types),
                    new SqlParameter("@Id", Id)
                };

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, parameters);
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
            string sql = "DELETE FROM account WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, parameters);
        }

        #endregion
    }
}
