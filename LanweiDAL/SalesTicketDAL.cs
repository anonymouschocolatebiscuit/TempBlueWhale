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
    public class SalesTicketDAL 
    {
        public SalesTicketDAL()
        {

        }

        #region 成员属性

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private string billNo;
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
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
        public string GetBillNumberAuto()
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                                       
                                      
                                       new SqlParameter("@NumberHeader","XSFP"),//单据代码前四位字母
                                       new SqlParameter("@tableName","SalesTicket")//表
                                      
                                       

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

            string sql = "insert into SalesTicket(number,wlId,bizDate,types,remarks,dis,disPrice,sumPrice,payNow,payNowNo,bkId,makeId,makeDate,bizId,flag,billNo,imagePath)";
            sql += " values(@number,@wlId,@bizDate,@types,@remarks,@dis,@disPrice,@sumPrice,@payNow,@payNowNo,@bkId,@makeId,@makeDate,@bizId,@flag,@billNo,@imagePath)   select @@identity ";
            SqlParameter[] param = {
                                       
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
                                       new SqlParameter("@billNo",BillNo),
                                       new SqlParameter("@ImagePath",ImagePath),
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

            string sql = @"UPDATE SalesTicket
                                       SET 
                                           wlId = @wlId                                       
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
                                          ,BillNo = @BillNo
                                          ,ImagePath = @ImagePath                                    
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id";
            SqlParameter[] param = {                                       
                                         
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
                                           new SqlParameter("@BillNo",BillNo),
                                           new SqlParameter("@ImagePath",ImagePath),
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
            sql += "               select * from SalesTicket where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from SalesTicketItem  where pId='" + Id + "' delete from SalesTicket where Id='" + Id + "'";

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
            string sql = "select * from viewSalesTicket order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询采购发票

        /// <summary>
        /// 查询采购发票
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int types)
        {
            string sql = "select * from viewSalesTicket  where bizDate>=@start and bizDate<=@end  ";
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

        #region 查询采购发票

        /// <summary>
        /// 查询采购发票
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetAllModelByWLId(string key, DateTime start, DateTime end, int venderId)
        {
            string sql = "select * from viewSalesTicket  where bizDate>=@start and bizDate<=@end and wlId='" + venderId + "' and flag='审核' and (isnull(sumPriceAll,0)-isnull(priceCheckNowSum,0))> 0 ";
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

        #region 查询采购发票-----通过编号

        /// <summary>
        /// 查询采购发票-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewSalesTicket where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询采购发票-----通过编号

        /// <summary>
        /// 查询采购发票-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewSalesTicket where number='" + number + "' order by number ";

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
            string sql = " if not exists(select * from SalesTicket where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update SalesTicket set flag='" + flag + "' ";
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
