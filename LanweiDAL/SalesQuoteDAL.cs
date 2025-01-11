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
    public class SalesQuoteDAL
    {
        public SalesQuoteDAL()
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

        private string isTax;
        public string IsTax
        {
            get { return isTax; }
            set { isTax = value; }
        }

        private string isFreight;
        public string IsFreight
        {
            get { return isFreight; }
            set { isFreight = value; }
        }

        private string freightWay;
        public string FreightWay
        {
            get { return freightWay; }
            set { freightWay = value; }
        }

        private string package;
        public string Package
        {
            get { return package; }
            set { package = value; }
        }

        private string sendDate;
        public string SendDate
        {
            get { return sendDate; }
            set { sendDate = value; }
        }

        private string sendPlace;
        public string SendPlace
        {
            get { return sendPlace; }
            set { sendPlace = value; }
        }

        private DateTime deadLine;
        public DateTime DeadLine
        {
            get { return deadLine; }
            set { deadLine = value; }
        }

        private string payWay;
        public string PayWay
        {
            get { return payWay; }
            set { payWay = value; }
        }

        private string payDate;
        public string PayDate
        {
            get { return payDate; }
            set { payDate = value; }
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
                                       
                                       new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","XSBJ"),//单据代码前四位字母
                                       new SqlParameter("@tableName","salesQuote")//表
                                      
                                       

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

            string sql = "insert into salesQuote(shopId,number,wlId,bizDate,isTax,isFreight,freightWay,package,sendDate,SendPlace,DeadLine,payWay,payDate,remarks,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@isTax,@isFreight,@freightWay,@package,@sendDate,@SendPlace,@DeadLine,@payWay,@payDate,@remarks,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
            SqlParameter[] param = {
                                       new SqlParameter("@shopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@BizDate",BizDate),
                                       new SqlParameter("@IsTax",IsTax),
                                       new SqlParameter("@IsFreight",IsFreight),
                                       new SqlParameter("@FreightWay",FreightWay),
                                       new SqlParameter("@Package",Package),
                                     
                                       new SqlParameter("@SendDate",SendDate),
                                       
                                       new SqlParameter("@SendPlace",SendPlace),
                                       new SqlParameter("@DeadLine",DeadLine),
                                       
                                       new SqlParameter("@PayWay",PayWay),
                                       new SqlParameter("@PayDate",PayDate),
                                       
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

        #region 修改一条记录
        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <returns></returns>
        public int Update()
        {

            string sql = @"UPDATE salesQuote
                                       SET 
                                          wlId = @wlId     
                                          ,shopId = @shopId                                   
                                          ,bizDate = @bizDate                                      
                                          ,sendDate = @sendDate

                                          ,SendPlace = @SendPlace
                                          ,DeadLine = @DeadLine   
  
                                          ,isTax=@isTax
                                          ,isFreight=@isFreight
                                          ,freightWay=@freightWay
                                          ,package=@package
                                          ,payWay = @payWay    
                                          ,payDate=@payDate                                   
                                          ,remarks = @remarks   
                                    
                                          ,makeId = @makeId                                                                                         
                                          ,makeDate = @makeDate
                                          ,BizId = @BizId                                     
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id and flag<>'审核'  ";
            SqlParameter[] param = {                                       
                                         
                                      new SqlParameter("@shopId",ShopId),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@BizDate",BizDate),
                                       new SqlParameter("@IsTax",IsTax),
                                       new SqlParameter("@IsFreight",IsFreight),
                                       new SqlParameter("@FreightWay",FreightWay),
                                       new SqlParameter("@Package",Package),
                                     
                                       new SqlParameter("@SendDate",SendDate),

                                       new SqlParameter("@SendPlace",SendPlace),
                                       new SqlParameter("@DeadLine",DeadLine),

                                       new SqlParameter("@PayWay",PayWay),
                                       new SqlParameter("@PayDate",PayDate),
                                       
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
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
            sql += "               select * from salesQuote where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from salesQuoteItem  where pId='" + Id + "' delete from salesQuote where Id='" + Id + "'";

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
            string sql = "select * from viewSalesQuote order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM viewSalesQuote ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by number  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }



        #endregion

        #region 查询销售订单

        /// <summary>
        /// 查询销售订单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId,string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewSalesQuoteList  where bizDate>=@start and bizDate<=@end  ";

            if (shopId != 0)
            {
                sql += " and shopId='"+shopId+"' ";
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

        #region 查询销售报价单

        /// <summary>
        /// 查询销售报价单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int types,int clientId)
        {
            string sql = "select * from viewSalesQuoteList  where bizDate>=@start and bizDate<=@end and wlId='" + clientId + "' ";
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

        #region 查询销售订单--------------客户查询

        /// <summary>
        /// 查询销售订单--------------客户查询
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
	                                where flag='审核'
	                                group by sourceNumber
                                )
                                b on a.number=b.sourceNumber

                                left join 
                                (
                                   select * from viewReceivable
                                )
                                c on a.number=c.orderNumber";

            sql += "  where a.bizDate>=@start and a.bizDate<=@end and a.wlId='" + clientId + "' ";

            if (key != "")
            {
                sql += " and (a.number like '%" + key + "%' or a.remarks  like '%" + key + "%' ) ";
            }


            if (types != 0) //0 所有 1 未入库 2 部分入库 3 全部入库
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


        #region 查询销售订单--------------客户查询

        /// <summary>
        /// 查询销售订单--------------客户查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListByClient(int id)
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
	                                where flag='审核'
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



        #region 查询销售订单-----通过编号

        /// <summary>
        /// 查询销售订单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewSalesQuote where id='"+id+"' order by number ";

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
            string sql = " if not exists(select * from SalesQuote where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update SalesQuote set flag='" + flag + "' ";
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

        #region 查询销售订单----------跟踪表

        /// <summary>
        /// 查询销售订单----------跟踪表
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

            if (types != 0) //0 所有 1 未入库 2 部分入库 3 全部入库
            {
                if (types == 1) //未出库
                {
                    sql += " and getNumNo = num ";
                }

                if (types == 2) //部分出库num>getNum and getNumNo<>0
                {
                    sql += " and getNum>0 and num>getNum ";
                }

                if (types == 3) //全部出库
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


        #region 查询销售报价记录----------跟踪表-----------NewUI

        /// <summary>
        /// 查询销售报价记录----------跟踪表-----------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="wlId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataSet GetSalesQuoteListReport(DateTime start, DateTime end,string wlId, string code)
        {
            string sql = "select * from viewSalesQuoteDetail  where bizDate>=@start and bizDate<=@end   ";

            if (wlId != "")
            {
                sql += " and wlId in (select id from client where code in (" + wlId + ") )";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

          

            sql += " order by code,number,wlId ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                      
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询销售报价记录----------跟踪表-----------NewUI

        /// <summary>
        /// 查询销售报价记录----------跟踪表-----------NewUI
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataSet GetSalesQuoteListReport(string key)
        {
            string sql = "select * from viewSalesQuoteDetail  where flag='审核'   ";



            if (key != "")
            {
                sql += " and goodsName like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }


            sql += " order by code,number,wlId ";



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        //--------------以下是网上订货前台

        #region 获取购物车信息
        /// <summary>
        /// 获取购物车信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetCartListByClientId(int clientId)
        {
            string sql = "select * from viewCartList where 1=1 ";
            if (clientId != 0)
            {
              sql+= " and  clientId='" + clientId + "' ";
            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion


        #region 获取购物车信息-----------汇总数量-金额等
        /// <summary>
        /// 获取购物车信息-------------汇总数量-金额等
        /// </summary>
        /// <returns></returns>
        public DataSet GetCartListSumByClientId(int clientId)
        {
            string sql = "select clientId,sum(num) sumNum,sum(salesPrice*num) sumSalesPrice,sum(sumPrice) sumPriceVip from viewCartList where 1=1 ";
            if (clientId != 0)
            {
              sql+= " and  clientId='" + clientId + "' ";
            }
            sql+=" group by clientId ";

             

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region 新增购物车信息
        /// <summary>
        /// 新增购物车信息
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

        



        #region 删除购物车信息

        /// <summary>
        /// 删除购物车信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteCartListById(int Id)
        {
            string sql = "delete from cartList where Id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 删除购物车信息-----清空

        /// <summary>
        /// 删除购物车信息-----清空
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteCartListByClientId(int Id)
        {
            string sql = "delete from cartList where clientId='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion



        #region 新增订单送货地址
        /// <summary>
        /// 新增订单送货地址
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="company"></param>
        /// <param name="address"></param>
        /// <param name="getName"></param>
        /// <param name="tel"></param>
        /// <param name="postCode"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public int AddSalesOrdersAddress(int pId, string company, string address, string getName, string tel, string phone, string postCode, string remarks)
        {
            string sql = "insert into SalesOrdersAddress(pId,company,address,getName,tel,phone,postCode,remarks) values('" + pId + "','" + company + "','" + address + "','" + getName + "','" + tel + "','" + phone + "','" + postCode + "','" + remarks + "') ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region 查询订单送货地址

        /// <summary>
        /// 查询订单送货地址
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetSalesOrdersAddress(int pId)
        {
            string sql = "select * from SalesOrdersAddress where pId='" + pId + "'";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 删除订单送货信息

        /// <summary>
        /// 删除订单送货信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteSalesOrdersAddress(int Id)
        {
            string sql = "delete from SalesOrdersAddress where pId='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 生成PDF，修改PDF路径

        /// <summary>
        /// 生成PDF，修改PDF路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int MakePDF(int id, string number)
        {
            string sql = " update salesQuote set pdfURL='" + number + "' where id='" + id + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

    }
}
