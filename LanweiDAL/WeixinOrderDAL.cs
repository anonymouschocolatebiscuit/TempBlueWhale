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
    public class WeixinOrderDAL
    {
        public WeixinOrderDAL()
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



        private string nickName;
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        private string wxNo;
        public string WxNo
        {
            get { return wxNo; }
            set { wxNo = value; }
        }

        private string sex;
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }



        private string province;
        public string Province
        {
            get { return province; }
            set { province = value; }
        }


        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private int goodsId;
        public int GoodsId
        {
            get { return goodsId; }
            set { goodsId = value; }
        }

        private string goodsName;
        public string GoodsName
        {
            get { return goodsName; }
            set { goodsName = value; }
        }

        private int num;
        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private decimal sumPrice;
        public decimal SumPrice
        {
            get { return sumPrice; }
            set { sumPrice = value; }
        }

        private DateTime dateStart;
        public DateTime DateStart
        {
            get { return dateStart; }
            set { dateStart = value; }
        }

        private DateTime datePay;
        public DateTime DatePay
        {
            get { return datePay; }
            set { datePay = value; }
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
                                       
                                      
                                       new SqlParameter("@NumberHeader","WXDD"),//单据代码前四位字母
                                       new SqlParameter("@tableName","weixinOrder")//表
                                      
                                       

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

            string sql = @"insert into weixinOrder
                                            (
                                               number,
                                               nickname,
                                               wxNo,
                                               sex,
                                               province,
                                               city,
                                               imagePath,
                                               goodsId ,
                                               goodsName,
                                               num ,
                                               price,
                                               sumPrice ,
                                               dateStart,
                                               flag
                                            ) values

                                            (
                                               @number,
                                               @nickname,
                                               @wxNo,
                                               @sex,
                                               @province,
                                               @city,
                                               @imagePath,
                                               @goodsId ,
                                               @goodsName,
                                               @num ,
                                               @price,
                                               @sumPrice ,
                                               @dateStart,
                                               @flag
                                            )   select @@identity

                                            ";

            SqlParameter[] param = {
                                       
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@NickName",NickName),
                                       new SqlParameter("@WxNo",WxNo),
                                       new SqlParameter("@Sex",Sex),
                                       new SqlParameter("@Province",Province),
                                       new SqlParameter("@City",City),
                                       new SqlParameter("@ImagePath",ImagePath),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@GoodsName",GoodsName),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@SumPrice",SumPrice),
                                       new SqlParameter("@DateStart",DateStart),                                      
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

            string sql = @"UPDATE weixinOrder
                                       SET 
                                           flag ='已付款'                                      
                                          ,datePay =getdate()  
                                        
                                     WHERE number=@number";
            SqlParameter[] param = {                                       
                                         
                                                                         
                                           new SqlParameter("@Number",Number) 

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
        public DataSet GetAllModel(string key, DateTime start, DateTime end, int types)
        {
            string sql = "select * from viewPurOrderList  where bizDate>=@start and bizDate<=@end  ";
            if (key != "")
            {
                sql += " and ( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%' )";
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

        #region 查询采购订单-----通过编号

        /// <summary>
        /// 查询采购订单-----通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from weixinOrder where number='" + number + "' order by number ";

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


    }
}
