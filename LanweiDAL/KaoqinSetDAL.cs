using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class KaoqinSetDAL
    {
        public KaoqinSetDAL()
        {

        }

        #region 成员字段



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

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private string workAddress;
        public string WorkAddress
        {
            get { return workAddress; }
            set { workAddress = value; }
        }

        private string locationX;
        public string LocationX
        {
            get { return locationX; }
            set { locationX = value; }
        }

        private string locationY;
        public string LocationY
        {
            get { return locationY; }
            set { locationY = value; }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private int startHH;
        public int StartHH
        {
            get { return startHH; }
            set { startHH = value; }
        }

        private int startMM;
        public int StartMM
        {
            get { return startMM; }
            set { startMM = value; }
        }

        private int endHH;
        public int EndHH
        {
            get { return endHH; }
            set { endHH = value; }
        }

        private int endMM;
        public int EndMM
        {
            get { return endMM; }
            set { endMM = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }


        #endregion


        #region  成员方法

     
        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(string names)
        {
            bool flag = false;

            string sql = "select * from kaoqinList where names='" + names + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 名称是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int id, string names)
        {
            bool flag = false;

            string sql = "select * from kaoqinList where names='" + names + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

      
      


        #endregion

        #region 获取所有信息

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList()
        {
            string sql = "select * from kaoqinSet order by id  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM kaoqinSet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id desc  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }


      
        #endregion

        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @" if not exists(select * from kaoqinSet where shopId=@ShopId) 
                            insert into kaoqinSet(
                            shopId,
                            names,
                            typeName,
                            workAddress,
                            locationX,
                            locationY,
                            Distance,
                            startHH,
                            startMM,
                            endHH,
                            endMM,
                            remarks
                            )  
                      values(
                            @shopId,
                            @names,
                            @typeName,
                            @workAddress,
                            @locationX,
                            @locationY,
                            @Distance,
                            @startHH,
                            @startMM,
                            @endHH,
                            @endMM,
                            @remarks

                            )

                            ";

            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@TypeName",TypeName),
                                       new SqlParameter("@WorkAddress",WorkAddress),
                                       new SqlParameter("@LocationX",LocationX),
                                       new SqlParameter("@LocationY",LocationY),
                                       new SqlParameter("@Distance",Distance),
                                       new SqlParameter("@startHH",startHH),
                                       new SqlParameter("@StartMM",StartMM),
                                       new SqlParameter("@EndHH",EndHH),
                                       new SqlParameter("@EndMM",EndMM),
                                       new SqlParameter("@Remarks",Remarks)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion


        #region 修改一条信息
        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        public int Update()
        {

            string sql = @" 
                            update kaoqinSet set
                            
                            names=@names,
                            typeName=@typeName,
                            workAddress=@workAddress,
                            locationX=@locationX,
                            locationY=@locationY,
                            Distance=@Distance,
                            startHH=@startHH,
                            startMM=@startMM,
                            endHH=@endHH,
                            endMM=@endMM,
                            remarks=@remarks

                         where shopId=@shopId
                            ";

            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@TypeName",TypeName),
                                       new SqlParameter("@WorkAddress",WorkAddress),
                                       new SqlParameter("@LocationX",LocationX),
                                       new SqlParameter("@LocationY",LocationY),
                                       new SqlParameter("@Distance",Distance),
                                       new SqlParameter("@startHH",startHH),
                                       new SqlParameter("@StartMM",StartMM),
                                       new SqlParameter("@EndHH",EndHH),
                                       new SqlParameter("@EndMM",EndMM),
                                       new SqlParameter("@Remarks",Remarks)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion
        

        #region 删除一条信息
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from kaoqinSet where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

    }
}
