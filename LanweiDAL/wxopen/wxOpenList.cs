using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wxOpenList
	/// </summary>
	public partial class wxOpenList
	{
		public wxOpenList()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "wxOpenList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wxOpenList");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Lanwei.Weixin.Model.wxOpenList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wxOpenList(");
			strSql.Append("shopId,appName,appId,appSecret,mchId,appKey)");
			strSql.Append(" values (");
			strSql.Append("@shopId,@appName,@appId,@appSecret,@mchId,@appKey)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@appName", SqlDbType.VarChar,100),
					new SqlParameter("@appId", SqlDbType.VarChar,100),
					new SqlParameter("@appSecret", SqlDbType.VarChar,1000),
					new SqlParameter("@mchId", SqlDbType.VarChar,100),
					new SqlParameter("@appKey", SqlDbType.VarChar,1000)};
			parameters[0].Value = model.shopId;
			parameters[1].Value = model.appName;
			parameters[2].Value = model.appId;
			parameters[3].Value = model.appSecret;
			parameters[4].Value = model.mchId;
			parameters[5].Value = model.appKey;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Lanwei.Weixin.Model.wxOpenList model)
		{

			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wxOpenList set ");
			strSql.Append("shopId=@shopId,");
			strSql.Append("appName=@appName,");
			strSql.Append("appId=@appId,");
			strSql.Append("appSecret=@appSecret,");
			strSql.Append("mchId=@mchId,");
			strSql.Append("appKey=@appKey");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@appName", SqlDbType.VarChar,100),
					new SqlParameter("@appId", SqlDbType.VarChar,100),
					new SqlParameter("@appSecret", SqlDbType.VarChar,1000),
					new SqlParameter("@mchId", SqlDbType.VarChar,100),
					new SqlParameter("@appKey", SqlDbType.VarChar,1000),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.shopId;
			parameters[1].Value = model.appName;
			parameters[2].Value = model.appId;
			parameters[3].Value = model.appSecret;
			parameters[4].Value = model.mchId;
			parameters[5].Value = model.appKey;
			parameters[6].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wxOpenList ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wxOpenList ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Lanwei.Weixin.Model.wxOpenList GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,shopId,appName,appId,appSecret,mchId,appKey from wxOpenList ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wxOpenList model=new Lanwei.Weixin.Model.wxOpenList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        public Lanwei.Weixin.Model.wxOpenList GetModelShopId(int shopId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopId,appName,appId,appSecret,mchId,appKey from wxOpenList ");
            strSql.Append(" where shopId=@shopId");
            SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4)
			};
            parameters[0].Value = shopId;

            Lanwei.Weixin.Model.wxOpenList model = new Lanwei.Weixin.Model.wxOpenList();
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

        public Lanwei.Weixin.Model.wxOpenList GetModel(string appId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopId,appName,appId,appSecret,mchId,appKey from wxOpenList ");
            strSql.Append(" where appId=@appId");
            
            SqlParameter[] parameters = {

					new SqlParameter("@appId",appId)
			};
           

            Lanwei.Weixin.Model.wxOpenList model = new Lanwei.Weixin.Model.wxOpenList();
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

        public Lanwei.Weixin.Model.wxOpenList GetModelMchId(string mchId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopId,appName,appId,appSecret,mchId,appKey from wxOpenList ");
            strSql.Append(" where mchId=@mchId");

            SqlParameter[] parameters = {

					new SqlParameter("@mchId",mchId)
			};


            Lanwei.Weixin.Model.wxOpenList model = new Lanwei.Weixin.Model.wxOpenList();
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
		public Lanwei.Weixin.Model.wxOpenList DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wxOpenList model=new Lanwei.Weixin.Model.wxOpenList();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["shopId"]!=null && row["shopId"].ToString()!="")
				{
					model.shopId=int.Parse(row["shopId"].ToString());
				}
				if(row["appName"]!=null)
				{
					model.appName=row["appName"].ToString();
				}
				if(row["appId"]!=null)
				{
					model.appId=row["appId"].ToString();
				}
				if(row["appSecret"]!=null)
				{
					model.appSecret=row["appSecret"].ToString();
				}
				if(row["mchId"]!=null)
				{
					model.mchId=row["mchId"].ToString();
				}
				if(row["appKey"]!=null)
				{
					model.appKey=row["appKey"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,shopId,appName,appId,appSecret,mchId,appKey ");
			strSql.Append(" FROM wxOpenList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,shopId,appName,appId,appSecret,mchId,appKey ");
			strSql.Append(" FROM wxOpenList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM wxOpenList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from wxOpenList T ");
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
			parameters[0].Value = "wxOpenList";
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

