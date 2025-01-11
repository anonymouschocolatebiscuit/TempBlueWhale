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
    public class CheckBillDAL
    {
        public CheckBillDAL()
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
                                       new SqlParameter("@NumberHeader","DJHX"),//单据代码前四位字母
                                       new SqlParameter("@tableName","CheckBill")//表
                                      
                                       

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

        #region 修改一条记录
        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <returns></returns>
        public int Update()
        {

            string sql = @"UPDATE CheckBill
                                       SET 
                                           clientIdA = @clientIdA  
                                          ,shopId = @shopId
                                          ,venderIdA = @venderIdA
                                          ,clientIdB = @clientIdB  
                                          ,venderIdB = @venderIdB                                       
                                          ,bizDate = @bizDate 
                                          ,bizType = @bizType                                      
                                          ,makeId = @makeId                                                                                         
                                          ,makeDate = @makeDate
                                          ,CheckPrice = @CheckPrice                                                                                                                                                          
                                          ,Flag = @Flag 
                                          ,remarks = @remarks                
                                        
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

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " if not exists( ";
            sql += "               select * from CheckBill where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from CheckBillItemPayMent  where pId='" + Id + "' delete from CheckBillItemPur  where pId='" + Id + "'  ";
            sql += " delete from CheckBillItemReceivable  where pId='" + Id + "' delete from CheckBillItemSales  where pId='" + Id + "' delete from CheckBill where Id='" + Id + "'";
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
            string sql = "select * from viewCheckBill order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询核销单

        /// <summary>
        /// 查询核销单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId,string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewCheckBill  where bizDate>=@start and bizDate<=@end  ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and (clientName like '%" + key + "%' or venderName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }
           
            sql+= " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        #region 查询核销单--------收款核销---客户

        /// <summary>
        /// 查询核销单--------收款核销---客户
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModelClient(int shopId,string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewCheckBill  where bizDate>=@start and bizDate<=@end and bizType=1 ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and (clientName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        #region 查询核销单--------付款核销---供应商

        /// <summary>
        /// 查询核销单--------付款核销---供应商
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetAllModelVender(int shopId,string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewCheckBill  where bizDate>=@start and bizDate<=@end  and bizType=2 ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and (venderName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询核销单-----通过编号

        /// <summary>
        /// 查询核销单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewCheckBill where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 查询核销单-----通过编号

        /// <summary>
        /// 查询核销单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewCheckBill where number='" + number + "' order by number ";

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
            string sql = " if not exists(select * from CheckBill where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update CheckBill set flag='" + flag + "' ";
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
