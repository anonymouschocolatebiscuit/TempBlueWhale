using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL.produce
{
    /// <summary>
    /// produceGetListItem
    /// </summary>
    public partial class ProduceGetListItem
    {
        public ProduceGetListItem()
        { }
        #region  BasicMethod

        /// <summary>
        /// Get the maximum ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "produceGetListItem");
        }

        /// <summary>
        /// Check if record exist
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from produceGetListItem");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// Add data
        /// </summary>
        public int Add(Model.produce.ProduceGetListItemModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into produceGetListItem(");
            strSql.Append("pId,goodsId,ckId,pihao,numApply,num,price,sumPrice,remarks)");
            strSql.Append(" values (");
            strSql.Append("@pId,@goodsId,@ckId,@pihao,@numApply,@num,@price,@sumPrice,@remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@goodsId", SqlDbType.Int,4),
                    new SqlParameter("@ckId", SqlDbType.Int,4),
                    new SqlParameter("@pihao", SqlDbType.VarChar,100),
                    new SqlParameter("@numApply", SqlDbType.Float,8),
                    new SqlParameter("@num", SqlDbType.Float,8),
                    new SqlParameter("@price", SqlDbType.Float,8),
                    new SqlParameter("@sumPrice", SqlDbType.Float,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,1000)};
            parameters[0].Value = model.pId;
            parameters[1].Value = model.goodsId;
            parameters[2].Value = model.ckId;
            parameters[3].Value = model.pihao;
            parameters[4].Value = model.numApply;
            parameters[5].Value = model.num;
            parameters[6].Value = model.price;
            parameters[7].Value = model.sumPrice;
            parameters[8].Value = model.remarks;

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
        /// Add Data
        /// </summary>
        public bool Update(Model.produce.ProduceGetListItemModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update produceGetListItem set ");
            strSql.Append("pId=@pId,");
            strSql.Append("goodsId=@goodsId,");
            strSql.Append("ckId=@ckId,");
            strSql.Append("pihao=@pihao,");
            strSql.Append("numApply=@numApply,");
            strSql.Append("num=@num,");
            strSql.Append("price=@price,");
            strSql.Append("sumPrice=@sumPrice,");
            strSql.Append("remarks=@remarks");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@goodsId", SqlDbType.Int,4),
                    new SqlParameter("@ckId", SqlDbType.Int,4),
                    new SqlParameter("@pihao", SqlDbType.VarChar,100),
                    new SqlParameter("@numApply", SqlDbType.Float,8),
                    new SqlParameter("@num", SqlDbType.Float,8),
                    new SqlParameter("@price", SqlDbType.Float,8),
                    new SqlParameter("@sumPrice", SqlDbType.Float,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,1000),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.pId;
            parameters[1].Value = model.goodsId;
            parameters[2].Value = model.ckId;
            parameters[3].Value = model.pihao;
            parameters[4].Value = model.numApply;
            parameters[5].Value = model.num;
            parameters[6].Value = model.price;
            parameters[7].Value = model.sumPrice;
            parameters[8].Value = model.remarks;
            parameters[9].Value = model.id;

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

        #region produce get list new ui

        /// <summary>
        /// produce get list new ui
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetProduceGetListItemDetail(int shopId, DateTime start, DateTime end, string ckName, string code)
        {
            string sql = "select * from viewProduceGetListItem where bizDate<=@end and bizDate >=@start ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            sql += " order by bizDate asc,number asc,code asc ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Query the production inventory summary table NewUI

        /// <summary>
        /// Query the production inventory summary table NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ckId"></param>
        /// <param name="wlId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public DataSet GetProduceGetListItemSumGoods(int shopId, DateTime start, DateTime end, string ckName, string code)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(num) sumNum,                           
                            sum(sumPrice) sumPrice 
                           from viewProduceGetListItem where bizDate<=@end and bizDate >=@start ";

            if (shopId != 0)
            {
                sql += " and shopId = '" + shopId + "' ";
            }

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            sql += " group by goodsId,code,goodsName,spec,unitName,ckId,ckName order by code asc ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        /// <summary>
        /// Delete row data
        /// </summary>
        public bool Delete(int pId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from produceGetListItem ");
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
        /// Batch Delete
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from produceGetListItem ");
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
        public Model.produce.ProduceGetListItemModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,pId,goodsId,ckId,pihao,numApply,num,price,sumPrice,remarks from produceGetListItem ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            Model.produce.ProduceGetListItemModel model = new Model.produce.ProduceGetListItemModel();
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
        /// Get Object
        /// </summary>
        public Model.produce.ProduceGetListItemModel DataRowToModel(DataRow row)
        {
            Model.produce.ProduceGetListItemModel model = new Model.produce.ProduceGetListItemModel();
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
                if (row["goodsId"] != null && row["goodsId"].ToString() != "")
                {
                    model.goodsId = int.Parse(row["goodsId"].ToString());
                }
                if (row["ckId"] != null && row["ckId"].ToString() != "")
                {
                    model.ckId = int.Parse(row["ckId"].ToString());
                }
                if (row["pihao"] != null)
                {
                    model.pihao = row["pihao"].ToString();
                }
                if (row["numApply"] != null && row["numApply"].ToString() != "")
                {
                    model.numApply = decimal.Parse(row["numApply"].ToString());
                }
                if (row["num"] != null && row["num"].ToString() != "")
                {
                    model.num = decimal.Parse(row["num"].ToString());
                }
                if (row["price"] != null && row["price"].ToString() != "")
                {
                    model.price = decimal.Parse(row["price"].ToString());
                }
                if (row["sumPrice"] != null && row["sumPrice"].ToString() != "")
                {
                    model.sumPrice = decimal.Parse(row["sumPrice"].ToString());
                }
                if (row["remarks"] != null)
                {
                    model.remarks = row["remarks"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// Get list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM viewProduceGetListItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Get top few list
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,pId,goodsId,ckId,pihao,numApply,num,price,sumPrice,remarks ");
            strSql.Append(" FROM produceGetListItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Get record count
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM produceGetListItem ");
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
        /// Pagination
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
            strSql.Append(")AS Row, T.*  from produceGetListItem T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  BasicMethod

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
