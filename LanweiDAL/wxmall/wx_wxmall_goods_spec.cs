using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_wxmall_goods_spec
	/// </summary>
	public partial class wx_wxmall_goods_spec
	{
		public wx_wxmall_goods_spec()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "wx_wxmall_goods_spec"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wxmall_goods_spec");
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
		public int Add(Lanwei.Weixin.Model.wx_wxmall_goods_spec model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wxmall_goods_spec(");
			strSql.Append("goodsId,names,picUrl,price,isStop,sortId)");
			strSql.Append(" values (");
			strSql.Append("@goodsId,@names,@picUrl,@price,@isStop,@sortId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@goodsId", SqlDbType.Int,4),
					new SqlParameter("@names", SqlDbType.VarChar,100),
					new SqlParameter("@picUrl", SqlDbType.VarChar,1000),
					new SqlParameter("@price", SqlDbType.Float,8),
					new SqlParameter("@isStop", SqlDbType.Int,4),
					new SqlParameter("@sortId", SqlDbType.Int,4)};
			parameters[0].Value = model.goodsId;
			parameters[1].Value = model.names;
			parameters[2].Value = model.picUrl;
			parameters[3].Value = model.price;
			parameters[4].Value = model.isStop;
			parameters[5].Value = model.sortId;

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
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_goods_spec model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wxmall_goods_spec set ");
			strSql.Append("goodsId=@goodsId,");
			strSql.Append("names=@names,");
			strSql.Append("picUrl=@picUrl,");
			strSql.Append("price=@price,");
			strSql.Append("sortId=@sortId");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@goodsId", SqlDbType.Int,4),
					new SqlParameter("@names", SqlDbType.VarChar,100),
					new SqlParameter("@picUrl", SqlDbType.VarChar,1000),
					new SqlParameter("@price", SqlDbType.Float,8),				
					new SqlParameter("@sortId", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.goodsId;
			parameters[1].Value = model.names;
			parameters[2].Value = model.picUrl;
			parameters[3].Value = model.price;
			parameters[4].Value = model.sortId;
			parameters[5].Value = model.id;

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


        public bool UpdateIsStop(int id, int isStop)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_wxmall_goods_spec set ");

            strSql.Append("isStop=@isStop ");


            strSql.Append(" where id=@id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),				
                    new SqlParameter("@isStop", SqlDbType.Int,4)
					};
            parameters[0].Value = id;
            parameters[1].Value = isStop;



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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wxmall_goods_spec ");
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
			strSql.Append("delete from wx_wxmall_goods_spec ");
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_spec GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,goodsId,names,picUrl,price,isStop,sortId from wx_wxmall_goods_spec ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wx_wxmall_goods_spec model=new Lanwei.Weixin.Model.wx_wxmall_goods_spec();
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


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Lanwei.Weixin.Model.wx_wxmall_goods_spec DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_goods_spec model=new Lanwei.Weixin.Model.wx_wxmall_goods_spec();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["goodsId"]!=null && row["goodsId"].ToString()!="")
				{
					model.goodsId=int.Parse(row["goodsId"].ToString());
				}
				if(row["names"]!=null)
				{
					model.names=row["names"].ToString();
				}
				if(row["picUrl"]!=null)
				{
					model.picUrl=row["picUrl"].ToString();
				}
				if(row["price"]!=null && row["price"].ToString()!="")
				{
					model.price=decimal.Parse(row["price"].ToString());
				}
				if(row["isStop"]!=null && row["isStop"].ToString()!="")
				{
					model.isStop=int.Parse(row["isStop"].ToString());
				}
				if(row["sortId"]!=null && row["sortId"].ToString()!="")
				{
					model.sortId=int.Parse(row["sortId"].ToString());
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
			strSql.Append("select id,goodsId,names,picUrl,price,isStop,sortId ");
			strSql.Append(" FROM wx_wxmall_goods_spec ");
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
			strSql.Append(" id,goodsId,names,picUrl,price,isStop,sortId ");
			strSql.Append(" FROM wx_wxmall_goods_spec ");
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
			strSql.Append("select count(1) FROM wx_wxmall_goods_spec ");
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
			strSql.Append(")AS Row, T.*  from wx_wxmall_goods_spec T ");
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
			parameters[0].Value = "wx_wxmall_goods_spec";
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

