using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_wxmall_goods_orderdetal
	/// </summary>
	public partial class wx_wxmall_goods_orderdetal
	{
		public wx_wxmall_goods_orderdetal()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wxmall_goods_orderdetal(");
			strSql.Append("orderId,goodsId,price,numbers,totalMoney,amountId)");
			strSql.Append(" values (");
			strSql.Append("@orderId,@goodsId,@price,@numbers,@totalMoney,@amountId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.Int,4),
					new SqlParameter("@goodsId", SqlDbType.Int,4),
					new SqlParameter("@price", SqlDbType.Decimal,9),
					new SqlParameter("@numbers", SqlDbType.Int,4),
					new SqlParameter("@totalMoney", SqlDbType.Decimal,9),
					new SqlParameter("@amountId", SqlDbType.Int,4)};
			parameters[0].Value = model.orderId;
			parameters[1].Value = model.goodsId;
			parameters[2].Value = model.price;
			parameters[3].Value = model.numbers;
			parameters[4].Value = model.totalMoney;
			parameters[5].Value = model.amountId;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wxmall_goods_orderdetal set ");
			strSql.Append("orderId=@orderId,");
			strSql.Append("goodsId=@goodsId,");
			strSql.Append("price=@price,");
			strSql.Append("numbers=@numbers,");
			strSql.Append("totalMoney=@totalMoney,");
			strSql.Append("amountId=@amountId");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.Int,4),
					new SqlParameter("@goodsId", SqlDbType.Int,4),
					new SqlParameter("@price", SqlDbType.Decimal,9),
					new SqlParameter("@numbers", SqlDbType.Int,4),
					new SqlParameter("@totalMoney", SqlDbType.Decimal,9),
					new SqlParameter("@amountId", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
			parameters[0].Value = model.orderId;
			parameters[1].Value = model.goodsId;
			parameters[2].Value = model.price;
			parameters[3].Value = model.numbers;
			parameters[4].Value = model.totalMoney;
			parameters[5].Value = model.amountId;
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
        public bool DeleteByOrderId(int orderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wxmall_goods_orderdetal ");
            strSql.Append(" where orderId=@orderId");
			SqlParameter[] parameters = {
					new SqlParameter("@orderId", orderId)
			};
			

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
			strSql.Append("delete from wx_wxmall_goods_orderdetal ");
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal GetModel(long id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,orderId,goodsId,price,numbers,totalMoney,amountId from wx_wxmall_goods_orderdetal ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal model=new Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal();
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal model=new Lanwei.Weixin.Model.wx_wxmall_goods_orderdetal();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=long.Parse(row["id"].ToString());
				}
				if(row["orderId"]!=null && row["orderId"].ToString()!="")
				{
					model.orderId=int.Parse(row["orderId"].ToString());
				}
				if(row["goodsId"]!=null && row["goodsId"].ToString()!="")
				{
					model.goodsId=int.Parse(row["goodsId"].ToString());
				}
				if(row["price"]!=null && row["price"].ToString()!="")
				{
					model.price=decimal.Parse(row["price"].ToString());
				}
				if(row["numbers"]!=null && row["numbers"].ToString()!="")
				{
					model.numbers=int.Parse(row["numbers"].ToString());
				}
				if(row["totalMoney"]!=null && row["totalMoney"].ToString()!="")
				{
					model.totalMoney=decimal.Parse(row["totalMoney"].ToString());
				}
				if(row["amountId"]!=null && row["amountId"].ToString()!="")
				{
					model.amountId=int.Parse(row["amountId"].ToString());
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
			strSql.Append("select * ");
            strSql.Append(" FROM view_wx_wxmall_goods_orderdetalList ");//换成关联蓝微ERP的商品表viewGoods
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
			strSql.Append(" id,orderId,goodsId,price,numbers,totalMoney,amountId ");
			strSql.Append(" FROM wx_wxmall_goods_orderdetal ");
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
			strSql.Append("select count(1) FROM wx_wxmall_goods_orderdetal ");
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
			strSql.Append(")AS Row, T.*  from wx_wxmall_goods_orderdetal T ");
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
			parameters[0].Value = "wx_wxmall_goods_orderdetal";
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

