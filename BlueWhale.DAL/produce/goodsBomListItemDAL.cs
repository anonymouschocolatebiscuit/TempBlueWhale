using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL.produce
{
    /// <summary>
	/// 数据访问类:goodsBomListItem
	/// </summary>
    public partial class goodsBomListItem
    {
        public goodsBomListItem()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "goodsBomListItem");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from goodsBomListItem");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.produce.goodsBomListItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into goodsBomListItem(");
            strSql.Append("pId,itemId,goodsId,num,rate,remarks)");
            strSql.Append(" values (");
            strSql.Append("@pId,@itemId,@goodsId,@num,@rate,@remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@itemId", SqlDbType.Int,4),
                    new SqlParameter("@goodsId", SqlDbType.Int,4),
                    new SqlParameter("@num", SqlDbType.Float,8),
                    new SqlParameter("@rate", SqlDbType.Float,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,1000)};
            parameters[0].Value = model.pId;
            parameters[1].Value = model.itemId;
            parameters[2].Value = model.goodsId;
            parameters[3].Value = model.num;
            parameters[4].Value = model.rate;
            parameters[5].Value = model.remarks;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.produce.goodsBomListItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update goodsBomListItem set ");
            strSql.Append("pId=@pId,");
            strSql.Append("itemId=@itemId,");
            strSql.Append("goodsId=@goodsId,");
            strSql.Append("num=@num,");
            strSql.Append("rate=@rate,");
            strSql.Append("remarks=@remarks");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@itemId", SqlDbType.Int,4),
                    new SqlParameter("@goodsId", SqlDbType.Int,4),
                    new SqlParameter("@num", SqlDbType.Float,8),
                    new SqlParameter("@rate", SqlDbType.Float,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,1000),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.pId;
            parameters[1].Value = model.itemId;
            parameters[2].Value = model.goodsId;
            parameters[3].Value = model.num;
            parameters[4].Value = model.rate;
            parameters[5].Value = model.remarks;
            parameters[6].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from goodsBomListItem ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(int pId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from goodsBomListItem ");
            strSql.Append(" where pId=@pId");
            SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4)
            };
            parameters[0].Value = pId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from goodsBomListItem ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.produce.goodsBomListItem GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,pId,itemId,goodsId,num,rate,remarks from goodsBomListItem ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            Model.produce.goodsBomListItem model = new Model.produce.goodsBomListItem();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.produce.goodsBomListItem DataRowToModel(DataRow row)
        {
            Model.produce.goodsBomListItem model = new Model.produce.goodsBomListItem();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["pId"] != null && row["pId"].ToString() != "")
                {
                    model.pId = int.Parse(row["pId"].ToString());
                }
                if (row["itemId"] != null && row["itemId"].ToString() != "")
                {
                    model.itemId = int.Parse(row["itemId"].ToString());
                }
                if (row["goodsId"] != null && row["goodsId"].ToString() != "")
                {
                    model.goodsId = int.Parse(row["goodsId"].ToString());
                }
                if (row["num"] != null && row["num"].ToString() != "")
                {
                    model.num = decimal.Parse(row["num"].ToString());
                }
                if (row["rate"] != null && row["rate"].ToString() != "")
                {
                    model.rate = decimal.Parse(row["rate"].ToString());
                }
                if (row["remarks"] != null)
                {
                    model.remarks = row["remarks"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM viewGoodsBomListItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,pId,itemId,goodsId,num,rate,remarks ");
            strSql.Append(" FROM goodsBomListItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM goodsBomListItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from goodsBomListItem T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "goodsBomListItem";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
