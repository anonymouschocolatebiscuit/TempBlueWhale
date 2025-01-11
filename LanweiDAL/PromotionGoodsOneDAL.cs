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
    public class PromotionGoodsOneDAL
    {
        public PromotionGoodsOneDAL()
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

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private DateTime dateStart;
        public DateTime DateStart
        {
            get { return dateStart; }
            set { dateStart = value; }
        }

        private DateTime dateEnd;
        public DateTime DateEnd
        {
            get { return dateEnd; }
            set { dateEnd = value; }
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

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private string stop;
        public string Stop
        {
            get { return stop; }
            set { stop = value; }
        }

        private DateTime stopDate;
        public DateTime StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
        }

        private int stopId;
        public int StopId
        {
            get { return stopId; }
            set { stopId = value; }
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
                                       
                                      
                                       new SqlParameter("@NumberHeader","DPCX"),//单据代码前四位字母
                                       new SqlParameter("@tableName","PromotionGoodsOne")//表
                                      
                                       

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

            string sql = @"insert into PromotionGoodsOne(number
                                                   ,shopId
                                                   ,names
                                                   ,dateStart
                                                   ,dateEnd
                                                   ,makeId
                                                   ,makeDate
                                                   ,remarks
                                                   ,flag) 
                                                values
                                                   (@number
                                                   ,@shopId
                                                   ,@names
                                                   ,@dateStart
                                                   ,@dateEnd
                                                   ,@makeId
                                                   ,@makeDate
                                                   ,@remarks
                                                   ,@flag
                                                   )   select @@identity ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@names",names),                                       
                                     
                                      
                                       new SqlParameter("@DateStart",DateStart),
                                       new SqlParameter("@DateEnd",DateEnd),
                                   
                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@remarks",remarks),
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

            string sql = @"UPDATE PromotionGoodsOne
                                       SET 
                                           shopId=@shopId
                                           ,names=@names
                                           ,dateStart=@dateStart
                                           ,dateEnd=@dateEnd
                                           ,remarks=@remarks                                   
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id and flag='保存' ";
            SqlParameter[] param = {                                       
                                         
                                           new SqlParameter("@ShopId",ShopId),                                         
                                           new SqlParameter("@Names",Names),                                          
                                           new SqlParameter("@DateStart",DateStart),              
                                           new SqlParameter("@DateEnd",DateEnd),              
                                           new SqlParameter("@Remarks",Remarks), 
                                                                                  
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
            sql += "               select * from PromotionGoodsOne where flag<>'保存' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from PromotionGoodsOneItem  where pId='" + Id + "' delete from PromotionGoodsOne where Id='" + Id + "'";

            sql += " end ";

          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 强制终止促销
        /// <summary>
        /// 强制终止促销
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int StopBill(int Id,int stopId)
        {
            string sql = " ";

      
            sql += " update PromotionGoodsOne set stopId='" + stopId + "',stopDate=getdate(),stop='是' where Id='" + Id + "' and flag='审核' and stop<>'是' ";

        


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
            string sql = "select * from viewPromotionGoodsOne order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询单品促销单

        /// <summary>
        /// 查询单品促销单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int shopId)
        {
            string sql = "select * from viewPromotionGoodsOne  where makeDate>=@start and makeDate<=@end  ";
            if (key != "")
            {
                sql += " and( names like '%" + key + "%' or remarks  like '%" + key + "%' )";
            }
            if (shopId != 0)
            {
                sql += " and shopId='"+shopId+"' ";
            }
         
            sql += " order by makeDate desc ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion





        #region 查询单品促销单-----通过编号

        /// <summary>
        /// 查询单品促销单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewPromotionGoodsOne where id='" + id + "' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询单品促销单-----通过编号

        /// <summary>
        /// 查询单品促销单-----通过编号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewPromotionGoodsOne where number='" + number + "' order by number ";

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
            string sql = " if not exists(select * from PromotionGoodsOne where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update PromotionGoodsOne set flag='" + flag + "' ";
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
