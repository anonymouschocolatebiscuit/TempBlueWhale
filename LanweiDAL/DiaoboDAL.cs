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
    public class DiaoboDAL
    {
        public DiaoboDAL()
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

        private DateTime bizDate;
        public DateTime BizDate
        {
            get { return bizDate; }
            set { bizDate = value; }
        }

        private int bizId;
        public int BizId
        {
            get { return bizId; }
            set { bizId = value; }
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
                                       new SqlParameter("@NumberHeader","CKDB"),//单据代码前四位字母
                                       new SqlParameter("@tableName","diaobo")//表
                                      
                                       

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

            string sql = "insert into diaobo(shopId,number,bizDate,remarks,makeId,makeDate,flag)";
            sql += " values(@shopId,@number,@bizDate,@remarks,@makeId,@makeDate,@flag)   select @@identity ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                      
                                       new SqlParameter("@bizDate",bizDate),
                                    
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                 
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

            string sql = @"UPDATE diaobo
                                       SET 
                                          shopId = @shopId                                                                   
                                          ,bizDate = @bizDate                                      
                                                                             
                                          ,remarks = @remarks                                    
                                          ,makeId = @makeId                                                                                         
                                          ,makeDate = @makeDate
                                                                   
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id";
            SqlParameter[] param = {                                       
                                         
                                           new SqlParameter("@ShopId",ShopId),                                 
                                           new SqlParameter("@BizDate",BizDate),                                      
                                                                          
                                           new SqlParameter("@Remarks",Remarks),         
                                           new SqlParameter("@MakeId",MakeId),                                        
                                           new SqlParameter("@MakeDate",MakeDate),            
                                                                              
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
            sql += "               select * from diaobo where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from diaoboItem  where pId='" + Id + "' delete from diaobo where Id='" + Id + "'";

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
            string sql = "select * from viewDiaobo order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询仓库调拨单

        /// <summary>
        /// 查询仓库调拨单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckIdIn"></param>
        /// <param name="ckIdOut"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int ckIdIn,int ckIdOut)
        {
            string sql = "select * from viewDiaobo  where bizDate>=@start and bizDate<=@end  ";
            if (key != "")
            {
                sql += " and( goodsName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }
            if (ckIdIn != 0)
            {
                sql += " and ckIdIn = '" + ckIdIn + "' ";
            }
            if (ckIdOut != 0)
            {
                sql += " and ckIdOut = '" + ckIdOut + "' ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询仓库调拨单------------NewUI

        /// <summary>
        /// 查询仓库调拨单------------NewUI
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckIdIn"></param>
        /// <param name="ckIdOut"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId,string key, DateTime start, DateTime end, string ckIdIn, string ckIdOut)
        {
            string sql = "select * from viewDiaobo  where bizDate>=@start and bizDate<=@end  ";
           
            if (shopId != 0)
            {
                sql += " and shopId='"+shopId+"' ";

            }
            
            if (key != "")
            {
                sql += " and( goodsName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' ) ";
            }
            if (ckIdIn != "")
            {
                sql += " and ckNameIn = '" + ckIdIn + "' ";
            }
            if (ckIdOut != "")
            {
                sql += " and ckNameOut = '" + ckIdOut + "' ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        #region 查询仓库调拨单-----通过编号

        /// <summary>
        /// 查询仓库调拨单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewDiaobo where id='"+id+"' order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询仓库调拨单-----通过编号

        /// <summary>
        /// 查询仓库调拨单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewDiaobo where number='" + number + "' order by number ";

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
            string sql = " if not exists(select * from diaobo where flag='"+flag+"' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update diaobo set flag='" + flag + "' ";
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
