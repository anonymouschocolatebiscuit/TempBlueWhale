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
    public class PurOrderDAL
    {
        public PurOrderDAL()
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

        private DateTime sendDate;
        public DateTime SendDate
        {
            get { return sendDate; }
            set { sendDate = value; }
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
                                       new SqlParameter("@NumberHeader","CGDD"),//单据代码前四位字母
                                 
                                       new SqlParameter("@tableName","purOrder")//表
                                      
                                       

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

            string sql = "insert into purOrder(shopId,number,wlId,bizDate,sendDate,remarks,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@sendDate,@remarks,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@bizDate",BizDate),
                                       new SqlParameter("@SendDate",SendDate),
                                     
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

            string sql = @"UPDATE purOrder
                                       SET 
                                           wlId = @wlId                                       
                                          ,bizDate = @bizDate                                      
                                          ,sendDate = @sendDate                                         
                                                                       
                                          ,remarks = @remarks                                    
                                        
                                          ,BizId = @BizId                                     
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id and flag<>'审核' ";
            SqlParameter[] param = {                                       
                                         
                                           new SqlParameter("@WlId",WlId),                                         
                                           new SqlParameter("@BizDate",BizDate),                                      
                                           new SqlParameter("@SendDate",SendDate),                                          
                                                               
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
            sql += "               select * from purOrder where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from purOrderItem  where pId='" + Id + "' delete from purOrder where Id='" + Id + "'";

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
            string sql = "select * from viewPurOrderList order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询采购订单

        /// <summary>
        /// 查询采购订单
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int shopId)
        {
            string sql = "select * from viewPurOrderList  where bizDate>=@start and bizDate<=@end  ";
            if (key != "")
            {
                sql += " and ( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' )";
            }
            if (shopId >= 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }
            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion

        #region 查询采购订单-----通过编号

        /// <summary>
        /// 查询采购订单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewPurOrderList where id='" + id + "' order by number ";

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
            string sql = " if not exists(select * from purOrder where flag='"+flag+"' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update purOrder set flag='" + flag + "' ";
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

        #region 查询采购订单----------跟踪表

        /// <summary>
        /// 查询采购订单----------跟踪表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListReport(DateTime start, DateTime end, DateTime sendStart, DateTime sendEnd, int wlId, int goodsId, int types)
        {
            string sql = "select * from viewPurOrderListReport  where bizDate>=@start and bizDate<=@end  and sendDate>=@sendStart and sendDate<=@sendEnd ";

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
                if (types == 1)//未入库
                {
                    sql += " and getNumNo = num ";
                }

                if (types == 2)//部分入库
                {
                    sql += " and getNum>0 and num>getNum ";
                }

                if (types == 3)//全部入库
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


        #region 查询采购订单----------跟踪表----NewUI

        /// <summary>
        /// 查询采购订单----------跟踪表----NewUI
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetPurOrderListReport(int shopId,DateTime start, DateTime end, DateTime sendStart, DateTime sendEnd, string wlId, string code, string types)
        {

            string sql = @" select * from viewPurOrderListReport  
                            where bizDate>=@start and bizDate<=@end  and sendDate>=@sendStart and sendDate<=@sendEnd ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlId in (select id from vender where code in (" + wlId + ") and shopId='"+shopId+"' )";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            if (types != "") //0 所有 1 未入库 2 部分入库 3 全部入库
            {
                sql += " and ( ";
                if (types.Contains("未入库"))
                {
                    sql += "  getNumNo = num ";
                }

                if (types.Contains("未入库") && types.Contains("部分入库"))
                {
                    sql += " or (getNum>0 and num>getNum )";
                }

                if (!types.Contains("未入库") && types.Contains("部分入库"))
                {
                    sql += " (getNum>0 and num>getNum )";
                }


                if (types.Contains("全部入库") && !types.Contains("未入库") && !types.Contains("部分入库"))
                {
                    sql += " getNumNo <= 0 ";
                }

                if (types.Contains("全部入库") && (types.Contains("未入库") || types.Contains("部分入库")))
                {
                    sql += " or getNumNo <= 0 ";
                }

                sql += " )";

                //if (types == 1)//未入库
                //{
                //    sql += " and getNumNo = num ";
                //}

                //if (types == 2)//部分入库
                //{
                //    sql += " and getNum>0 and num>getNum ";
                //}

                //if (types == 3)//全部入库
                //{
                //    sql += " and getNumNo <= 0 ";
                //}


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


    }
}
