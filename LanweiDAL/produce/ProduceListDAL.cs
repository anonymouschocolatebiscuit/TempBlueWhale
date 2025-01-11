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
    public class ProduceListDAL
    {
        public ProduceListDAL()
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

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
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


        private string orderNumber;
        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private decimal num;
        public decimal Num
        {
            get { return num; }
            set { num = value; }
        }

  

     

        private int goodsId;
        public int GoodsId
        {
            get { return goodsId; }
            set { goodsId = value; }
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
                                       new SqlParameter("@NumberHeader","SCJH"),//单据代码前四位字母
                                       new SqlParameter("@tableName","ProduceList")//表
                                      
                                       

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

            string sql = @"insert into produceList(
                                                       shopId,
                                                       number,
                                                       typeName ,
                                                       orderNumber ,
                                                       goodsId ,
                                                       num ,
                                                       remarks,
                                                       dateStart ,
                                                       dateEnd ,
                                                       makeId ,
                                                       makeDate ,
                                                       flag 
                                                    )";
                                      sql += @" values(@shopId,
                                                       @number,
                                                       @typeName ,
                                                       @orderNumber ,
                                                       @goodsId ,
                                                       @num ,
                                                       @remarks,
                                                       @dateStart ,
                                                       @dateEnd ,
                                                       @makeId ,
                                                       @makeDate ,
                                                       @flag )  
                                          select @@identity ";
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@TypeName",TypeName),
                                       new SqlParameter("@OrderNumber",OrderNumber),                                       
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@DateStart",DateStart),
                                       new SqlParameter("@DateEnd",DateEnd),                                    
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

            string sql = @"UPDATE produceList
                                       SET
                                        shopId=@shopId,
                                        typeName=@typeName,
                                        orderNumber=@orderNumber,
                                        goodsId=@goodsId,
                                        num=@num,
                                        remarks=@remarks,
                                        dateStart=@dateStart,
                                        dateEnd=@dateEnd,
                                        flag=@flag
                                        
                                     WHERE id=@id";
            SqlParameter[] param = {                                       
                                         
                                       new SqlParameter("@ShopId",ShopId),                                    
                                       new SqlParameter("@TypeName",TypeName),
                                       new SqlParameter("@OrderNumber",OrderNumber),                                       
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@DateStart",DateStart),
                                       new SqlParameter("@DateEnd",DateEnd),                                                                        
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
            sql += "               select * from produceList where flag='审核' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from produceListItem  where pId='" + Id + "' delete from produceList where Id='" + Id + "'";

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
            string sql = "select * from viewProduceList order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
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
        public DataSet GetAllModel(int shopId,string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewProduceList  where makeDate>=@start and makeDate<=@end  ";

            if (shopId != 0)
            {
                sql += " and shopId='"+shopId+"' ";
            }
            
            if (key != "")
            {
                sql += " and( goodsName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' )";
            }
          
            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
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
            string sql = " if not exists(select * from ProduceList where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update ProduceList set flag='" + flag + "' ";
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


        



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *   ");
            strSql.Append(" FROM viewProduceList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #region 查询生产计划----------跟踪表-----------NewUI

        /// <summary>
        /// 查询生产计划----------跟踪表-----------NewUI
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetProduceListReport(string keys,int shopId, DateTime start, DateTime end, string wlId, string code, string flag)
        {

            string sql = "select * from viewProduceList  where makeDate>=@start and makeDate<=@end  ";

            if (keys != "")
            {
                sql += " and (orderNumber like '%" + keys + "%'  or remarks like '%" + keys + "%' ) ";
            }

            if (shopId >= 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (wlId != "")
            {
                sql += " and wlId in (select id from client where code in (" + wlId + ") )";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            if (flag != "") //0 所有 1 未入库 2 部分入库 3 全部入库
            {
                sql += " and ( ";

                if (flag.Contains("未生产"))
                {
                    sql += "  finishNumNo = num ";
                }

                if (flag.Contains("未生产") && flag.Contains("进行中"))
                {
                    sql += " or (finishNum>0 and num>finishNum )";
                }

                if (!flag.Contains("未生产") && flag.Contains("进行中"))
                {
                    sql += " (finishNum>0 and num>finishNum ) ";
                }


                if (flag.Contains("已完成") && !flag.Contains("未生产") && !flag.Contains("进行中"))
                {
                    sql += " finishNumNo <= 0 ";
                }

                if (flag.Contains("已完成") && (flag.Contains("未生产") || flag.Contains("进行中")))
                {
                    sql += " or finishNumNo <= 0 ";
                }

                sql += " )";

             


            }

            sql += " order by makeDate,code,number,wlId ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end.AddDays(1))
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion




    }
}
