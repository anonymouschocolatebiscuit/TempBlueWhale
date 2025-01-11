using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class ProduceInDAL
    {
        public ProduceInDAL()
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


        #region 自动生成单据编号


        /// <summary>
        /// 生成单据编号
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@shopId",shopId),//单据代码前四位字母
                                       new SqlParameter("@NumberHeader","SCRK"),//单据代码前四位字母
                                       new SqlParameter("@tableName","ProduceIn")//表
                                      
                                       

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

            string sql = @"insert into ProduceIn(shopId,number,wlId,bizDate,remarks,makeId,makeDate,bizId,flag)";
            sql += @" values(@shopId,@number,@wlId,@bizDate,@remarks,@makeId,@makeDate,@bizId,@flag)  

                select @@identity ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@bizDate",BizDate),                                       
                                     
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

        #region 修改一条记录
        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <returns></returns>
        public int Update()
        {

            string sql = @"UPDATE ProduceIn
                                       SET
                                           shopId = @shopId 
                                           ,wlId = @wlId                                       
                                          ,bizDate = @bizDate                                                                                                                 
                                          ,remarks = @remarks                                    
                                          ,BizId = @BizId 
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id";
            SqlParameter[] param = {                                       
                                         
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@WlId",WlId),                                         
                                       new SqlParameter("@BizDate",BizDate),                                                                                 
                                       new SqlParameter("@Remarks",Remarks),          
                                       new SqlParameter("@BizId",BizId),                                          
                                       new SqlParameter("@Flag",Flag),
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
            sql += "               select * from ProduceIn where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from ProduceInItem  where pId='" + Id + "' delete from ProduceIn where Id='" + Id + "'";

            sql += " end ";

          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        

        #region 查询销售出库单

        /// <summary>
        /// 查询销售出库单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId,string key, DateTime start, DateTime end, int types)
        {
            string sql = "select * from viewProduceIn  where bizDate>=@start and bizDate<=@end  ";

            if (shopId != 0)
            {
                sql += " and shopId='"+shopId+"' ";
            }
            
            if (key != "")
            {
                sql += " and( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' )";
            }
            if (types != 0)
            {
                sql += " and types = '"+types+"' ";
            }
            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        
        


        #region 查询销售出库单-----通过编号

        /// <summary>
        /// 查询销售出库单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewSalesReceipt where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询销售出库单-----通过编号

        /// <summary>
        /// 查询销售出库单-----通过编号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewSalesReceipt where number='" + number + "' order by number ";

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
            string sql = " if not exists(select * from ProduceIn where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update ProduceIn set flag='" + flag + "' ";
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


        #region 查询商品销售明细

        /// <summary>
        /// 查询商品销售明细
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemDetail(DateTime start,DateTime end,int ckId,int wlId,int goodsId)
        {
            string sql = "select * from viewSalesReceiptDetail where bizDate<=@end and bizDate >=@start ";
            if (ckId != 0)
            {
                sql += " and ckId ='"+ckId+"' ";
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



        #region 查询生产入库明细--------NewUI

        /// <summary>
        /// 查询生产入库明细--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetProduceInItemDetail(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = "select * from viewProduceInItem where bizDate<=@end and bizDate >=@start ";

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



        #region 查询销售汇总表-----------按商品

        /// <summary>
        /// 查询销售汇总表-----------按商品
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

        #region 查询生产入库汇总表-----------按商品--------NewUI

        /// <summary>
        /// 查询生产入库汇总表-----------按商品--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetProduceInItemSumGoods(int shopId, DateTime start, DateTime end, string ckName, string wlId, string code)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(num) sumNum,
                            sum(sumPriceNow) sumPriceNow,
                            sum(sumPriceDis) sumPriceDis,
                            sum(sumPriceTax) sumPriceTax,
                            sum(sumPriceAll) sumPriceAll  

                           from viewProduceInDetail where bizDate<=@end and bizDate >=@start ";

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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *   ");
            strSql.Append(" FROM viewProduceIn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }




        #region 查询销售汇总表-----------客户

        /// <summary>
        /// 查询销售汇总表-----------客户
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


        #region 查询销售汇总表-----------客户--------NewUI

        /// <summary>
        /// 查询销售汇总表-----------客户--------NewUI
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


        #region 根据类别查询销售汇总

        /// <summary>
        /// 根据类别查询销售汇总
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet GetSalesReceiptItemDetailGroupByTypeName(DateTime start, DateTime end)
        {
            string sql = @"

                            select isnull(sum(sumPrice*types),0) sumPrice,
                            isnull(sum(num),0) sumNum
                            from viewSalesReceiptItem where flag='审核' 
                            and bizDate<=@end and bizDate >=@start 


";




            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);


        }


        #endregion


        #region 查询每月销售汇总-----按年

        /// <summary>
        /// 查询每月销售汇总-----按年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumByMoth(int year,int shopId)
        {
            string sql = @"select 
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

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr,CommandType.Text,sql,param);

        }

        #endregion 

        #region 查询每月销售汇总-----按年

        /// <summary>
        /// 查询每月销售汇总-----按年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumByMoth(int year,string databaseURL)
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



        #region 查询销售占比-----按日期段

        /// <summary>
        /// 查询销售占比-----按日期段
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet getSalesPercent(int shopId,DateTime start,DateTime end)
        {
            string sql = @"select typeId,typeName,
                                sum(sumPriceAll) sumPriceAll
                                from viewSalesReceiptItem where  flag='审核' 
                                and pId in(select id from SalesReceipt where shopId=@shopId )
                               
                                group by typeId,typeName ";//

            SqlParameter[] param = {
                                       new SqlParameter("@shopId",shopId)
                                   };


           

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion 


        #region 查询销售占比-----按日期段

        /// <summary>
        /// 查询销售占比-----按日期段
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet getSalesPercent(DateTime start, DateTime end,string databaseURL)
        {
            string sql = @"select typeId,typeName,
                                sum(sumPrice) sumPrice
                                from viewSalesReceiptItem where  flag='审核' 

                                group by typeId,typeName";//




            return SQLHelper.SqlDataAdapter(databaseURL, CommandType.Text, sql, null);

        }

        #endregion 





        #region 查询每月销售量汇总-----按年------曲线图

        /// <summary>
        /// 查询每月销售量汇总-----按年------曲线图
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumNumByMoth(int year,int shopId)
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

        #region 查询每月销售量汇总-----按年------曲线图

        /// <summary>
        /// 查询每月销售量汇总-----按年------曲线图
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet getSalesSumNumByMoth(int year,string databaseURL)
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
