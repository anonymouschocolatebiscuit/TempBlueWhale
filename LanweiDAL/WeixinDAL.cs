using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
    public class WeixinDAL
    {
        #region 绑定保姆微信号

        /// <summary>
        /// 绑定保姆微信号
        /// </summary>
        /// <param name="weixin"></param>
        /// <param name="number"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int BindWeixinBaomu(string weixin, string number, string cardNo)
        {
            int bmId = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@number",number),
                                       new SqlParameter("@cardNo",cardNo),
                                       new SqlParameter("@weixin",weixin)

                                   };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "BindWeixinBaomu", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                bmId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["bmId"].ToString());

            }
            return bmId;

        }



        #endregion


        #region 保姆自动上线-----通过微信

        /// <summary>
        /// 保姆自动上线
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int AutoShowBaomuWeixin(string weixin)
        {
            int bmId = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@weixin",weixin)

                                   };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "AutoShowBaomuWeixin", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                bmId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["bmId"].ToString());

            }
            return bmId;

        }

        #endregion


        #region 绑定雇主微信号

        /// <summary>
        /// 绑定雇主微信号
        /// </summary>
        /// <param name="weixin"></param>
        /// <param name="number"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int BindWeixinGuzhu(string weixin, string number, string cardNo)
        {
            int bmId = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@number",number),
                                       new SqlParameter("@cardNo",cardNo),
                                       new SqlParameter("@weixin",weixin)

                                   };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "BindWeixinGuzhu", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                bmId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["gzId"].ToString());

            }
            return bmId;

        }



        #endregion

        #region 雇主自动上线-----通过微信

        /// <summary>
        /// 雇主自动上线
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int AutoShowGuzhuWeixin(string weixin)
        {
            int bmId = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@weixin",weixin)

                                   };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "AutoShowGuzhuWeixin", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                bmId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["gzId"].ToString());

            }
            return bmId;

        }

        #endregion





        #region 修改中奖数量
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int UpdateLottery(int id)
        {
            string sql = "update prize set numOK =numOk+1 where Id='" + id + "' update prize set groupId=groupId+1 ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 获取所有奖品信息
        /// <summary>
        /// 获取所有奖品信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public List<Lottery> GetLotteryListAll()
        {


            string sql = "select * from prize ";

            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, sql, null))
            {
                List<Lottery> list = new List<Lottery>();
                while (read.Read())
                {
                    Lottery s = new Lottery();

                    s.id = read.GetInt32(0);
                    s.groupId = read.GetInt32(1);
                    s.prize = read.GetString(2);
                    s.min = read.GetInt32(3);
                    s.max = read.GetInt32(4);
                    s.v = read.GetInt32(7);
                    s.num = read.GetInt32(8);
                    list.Add(s);
                }
                return list;
            }


         
        }

        #endregion

        #region 中奖概率算法-----获取奖品

        /// <summary>
        /// 中奖概率算法-----获取奖品
        /// </summary>
        /// <param name="ArrayV"></param>
        /// <returns></returns>
        public Lottery GetLotteryList(List<Lottery> ArrayV)
        {
            Lottery result = new Lottery();

            int proSum = 0;
            foreach (Lottery model in ArrayV)
            {
                proSum += model.v;
            }

            foreach (Lottery model in ArrayV)
            {
                Random rd = new Random((int)DateTime.Now.Ticks);
                int randNum = rd.Next(1, proSum);
                if (randNum <= model.v)
                {
                    result = model;

                    break;

                }
                else
                {
                    proSum -= model.v;

                }
            }

            return result;


        }

        #endregion

        #region 获取所有奖品信息
        /// <summary>
        /// 获取所有奖品信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public List<LotteryGGK> GetLotteryGGKListAll()
        {


            string sql = "select * from prizeGGK ";

            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, sql, null))
            {
                List<LotteryGGK> list = new List<LotteryGGK>();
                while (read.Read())
                {
                    LotteryGGK s = new LotteryGGK();

                    s.id = read.GetInt32(0);
                    s.groupId = read.GetInt32(1);
                    s.prize = read.GetString(2);
                    s.v = read.GetInt32(3);
                    s.num = read.GetInt32(4);
                    list.Add(s);
                }
                return list;
            }


          

        }

        #endregion

        #region 中奖概率算法-----获取奖品刮刮卡

        /// <summary>
        /// 中奖概率算法-----获取奖品刮刮卡
        /// </summary>
        /// <param name="ArrayV"></param>
        /// <returns></returns>
        public LotteryGGK GetLotteryGGKList(List<LotteryGGK> ArrayV)
        {
            LotteryGGK result = new LotteryGGK();

            int proSum = 0;
            foreach (LotteryGGK model in ArrayV)
            {
                proSum += model.v;
            }

            foreach (LotteryGGK model in ArrayV)
            {
                Random rd = new Random((int)DateTime.Now.Ticks);
                int randNum = rd.Next(1, proSum);
                if (randNum <= model.v)
                {
                    result = model;

                    break;

                }
                else
                {
                    proSum -= model.v;

                }
            }

            return result;


        }

        #endregion

        #region 修改中奖数量
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int UpdateLotteryGGK(int id)
        {
            string sql = "update prizeGGK set numOK =numOk+1 where Id='" + id + "' update prizeGGK set groupId=groupId+1 ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion


        #region 获取所有分店信息
        /// <summary>
        /// 获取所有分店信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllShopDataSet()
        {
            string sql = "select * from Shop";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region 获取欢迎词

        /// <summary>
        /// 获取欢迎词 select top 1 * from systemSet 
        /// </summary>
        /// <returns></returns>
        public DataSet GetWeixinWelcome(int shopId)
        {
            string sql = "select top 1 * from weixinWelcome where shopId='" + shopId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion
    }
}
