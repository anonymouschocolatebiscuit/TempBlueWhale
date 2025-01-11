using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class CityDAL
    {
        #region 查询省信息
        /// <summary>
        ///查询省信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvinceInfo()
        {
            string sql = "select * from Province ";
            

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 查询市信息
        /// <summary>
        /// 查询市信息
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public DataSet GetCityInfo(int proId)
        {
            string sql = "select * from city where proId='"+proId+"' ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

       



        #region 查询省信息--根据编号
        /// <summary>
        /// 查询省信息--根据编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetProvinceInfoById(int id)
        {
            string sql = "select * from Province where id='"+id+"'";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 查询市信息--根据编号
        /// <summary>
        /// 查询市信息--根据编号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetCityInfoByProId(int proId)
        {
            string sql = "select * from city where  proId='"+proId+"' ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region 查询区信息--根据编号
        /// <summary>
        /// 查询区信息--根据编号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetAreaInfoByCtId(int ctId)
        {
            string sql = "select * from area where ctId='" + ctId + "'  ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion



    }
}
