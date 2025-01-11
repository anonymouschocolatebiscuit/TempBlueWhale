using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class UrlDAL
    {


        #region 插入URL信息
        /// <summary>
        /// 插入URL信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="code"></param>
        /// <param name="url"></param>
        /// <param name="openId"></param>
        /// <param name="fromOpenId"></param>
        /// <param name="hot"></param>
        /// <param name="forward"></param>
        /// <param name="saves"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public int Add(int goodsId,string code,string url,string openId,string fromOpenId,int hot,int forward,int saves,string remarks)
        {
            string id = "0";

            string sql = "insert into urlShare(goodsId,code,url,openId,fromOpenId,hot,forward,saves,remarks,flag,makeDate) ";

            sql += "  values('" + goodsId + "','" + code + "','" + url + "','" + openId + "','" + fromOpenId + "','" + hot + "','" + forward + "','" + saves + "','" + remarks + "',getdate())  ";

            sql += "    select @@identity   ";

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (sdr.Read())
            {
                id = sdr[0].ToString();

            }

            return int.Parse(id);


        }
        #endregion





        #region 查询完整URL信息、通过生成的code
        /// <summary>
        /// 查询完整URL信息、通过生成的code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataSet GetURLInfo(string code)
        {
            string sql = "select * from urlShare where code='" + code + "' ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region 更新Flag
        /// <summary>
        /// 更新Flag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateFlag(int id,int flag)
        {
            int temp = 0;
            string sql = " update urlShare set flag='"+flag+"' where id='" + id + "'  ";

            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }

        #endregion


        #region 更新Code
        /// <summary>
        /// 更新Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateCode(int id,string code)
        {
            int temp = 0;
            string sql = " update urlShare set Code='"+code+"',flag=1  where id='" + id + "'  ";

            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }

        #endregion


        #region 更新URL阅读次数

        public int UpdateHot(string code)
        {
            int temp = 0;
            string sql = "update urlShare set hot=hot+1 where code='" + code + "' ";

            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }

        #endregion


        #region 更新URL转发次数

        public int UpdateForward(string code)
        {
            int temp = 0;
            string sql = "update urlShare set Forward=Forward+1 where code='" + code + "' ";

            temp = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            return temp;
        }

        #endregion



        



    }
}
