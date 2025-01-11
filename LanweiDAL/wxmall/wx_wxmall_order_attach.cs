using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Lanwei.Weixin.DBUtility;//Please add references

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_wxmall_order_attach
	/// </summary>
	public partial class wx_wxmall_order_attach
	{
		public wx_wxmall_order_attach()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Lanwei.Weixin.Model.wx_wxmall_order_attach model)
		{
			StringBuilder strSql=new StringBuilder();


			strSql.Append("insert into wx_wxmall_order_attach(");
			strSql.Append(" orderId,attachId,filePath,makeDate,flag)");
			strSql.Append(" values (");
			strSql.Append("@orderId,@attachId,@filePath,@makeDate,@flag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.Int,4),
					new SqlParameter("@attachId", SqlDbType.Int,4),
					new SqlParameter("@filePath", SqlDbType.VarChar,1000),
					new SqlParameter("@makeDate", SqlDbType.DateTime),
					new SqlParameter("@flag", SqlDbType.VarChar,100)};
			parameters[0].Value = model.orderId;
			parameters[1].Value = model.attachId;
			parameters[2].Value = model.filePath;
			parameters[3].Value = model.makeDate;
			parameters[4].Value = model.flag;

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
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_order_attach model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wxmall_order_attach set ");
			strSql.Append("filePath=@filePath,");
			strSql.Append("makeDate=@makeDate,");
			strSql.Append("flag=@flag");
            strSql.Append(" where orderId=@orderId and attachId=@attachId ");
			SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.Int,4),
					new SqlParameter("@attachId", SqlDbType.Int,4),
					new SqlParameter("@filePath", SqlDbType.VarChar,1000),
					new SqlParameter("@makeDate", SqlDbType.DateTime),
					new SqlParameter("@flag", SqlDbType.VarChar,100),

                                        };
			parameters[0].Value = model.orderId;
			parameters[1].Value = model.attachId;
			parameters[2].Value = model.filePath;
			parameters[3].Value = model.makeDate;
			parameters[4].Value = model.flag;

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
		public bool Delete(long id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wxmall_order_attach ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)
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

        public bool DeleteByOrderId(int orderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_wxmall_order_attach ");
            strSql.Append(" where orderId=@orderId");
            SqlParameter[] parameters = {
					new SqlParameter("@orderId",orderId)
			};
           

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wxmall_order_attach ");
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
		public Lanwei.Weixin.Model.wx_wxmall_order_attach GetModel(long id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,orderId,attachId,filePath,makeDate,flag from wx_wxmall_order_attach ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wx_wxmall_order_attach model=new Lanwei.Weixin.Model.wx_wxmall_order_attach();
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
		public Lanwei.Weixin.Model.wx_wxmall_order_attach DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_order_attach model=new Lanwei.Weixin.Model.wx_wxmall_order_attach();
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
				if(row["attachId"]!=null && row["attachId"].ToString()!="")
				{
					model.attachId=int.Parse(row["attachId"].ToString());
				}
				if(row["filePath"]!=null)
				{
					model.filePath=row["filePath"].ToString();
				}
				if(row["makeDate"]!=null && row["makeDate"].ToString()!="")
				{
					model.makeDate=DateTime.Parse(row["makeDate"].ToString());
				}
				if(row["flag"]!=null)
				{
					model.flag=row["flag"].ToString();
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
			strSql.Append("select id,orderId,attachId,filePath,makeDate,flag ");
			strSql.Append(" FROM wx_wxmall_order_attach ");
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
			strSql.Append(" id,orderId,attachId,filePath,makeDate,flag ");
			strSql.Append(" FROM wx_wxmall_order_attach ");
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
			strSql.Append("select count(1) FROM wx_wxmall_order_attach ");
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
			strSql.Append(")AS Row, T.*  from wx_wxmall_order_attach T ");
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
			parameters[0].Value = "wx_wxmall_order_attach";
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

