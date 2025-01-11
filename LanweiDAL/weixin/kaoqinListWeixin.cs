using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:kaoqinListWeixin
	/// </summary>
	public partial class kaoqinListWeixin
	{
		public kaoqinListWeixin()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "kaoqinListWeixin"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from kaoqinListWeixin");
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
        public int Add(Lanwei.Weixin.Model.kaoqinListWeixin model)
        {
            StringBuilder strSql = new StringBuilder();

          

            strSql.Append("if not exists(select * from kaoqinListWeixin where shopId=@shopId and userid=@userId and checkin_time=@checkin_time ) ");

            strSql.Append("insert into kaoqinListWeixin(");
            strSql.Append("shopId,deptId,deptName,userid,username,groupname,checkin_type,exception_type,checkin_time,location_title,location_detail,wifiname,notes,wifimac,mediaids)");
            strSql.Append(" values (");
            strSql.Append("@shopId,@deptId,@deptName,@userid,@username,@groupname,@checkin_type,@exception_type,@checkin_time,@location_title,@location_detail,@wifiname,@notes,@wifimac,@mediaids)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@shopId", model.shopId),
                    new SqlParameter("@deptId", model.deptId),
                    new SqlParameter("@deptName", model.deptName),
					new SqlParameter("@userid", model.userid),
                    new SqlParameter("@username", model.username),
					new SqlParameter("@groupname", model.groupname),
					new SqlParameter("@checkin_type", model.checkin_type),
					new SqlParameter("@exception_type", model.exception_type),
					new SqlParameter("@checkin_time", model.checkin_time),
					new SqlParameter("@location_title", model.location_title),
					new SqlParameter("@location_detail", model.location_detail),
					new SqlParameter("@wifiname",model.wifiname),
					new SqlParameter("@notes", model.notes),
					new SqlParameter("@wifimac", model.wifimac),
					new SqlParameter("@mediaids", model.mediaids)
                                        };
		



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
		public bool Update(Lanwei.Weixin.Model.kaoqinListWeixin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update kaoqinListWeixin set ");
			strSql.Append("shopId=@shopId,");
			strSql.Append("userid=@userid,");
			strSql.Append("groupname=@groupname,");
			strSql.Append("checkin_type=@checkin_type,");
			strSql.Append("exception_type=@exception_type,");
			strSql.Append("checkin_time=@checkin_time,");
			strSql.Append("location_title=@location_title,");
			strSql.Append("location_detail=@location_detail,");
			strSql.Append("wifiname=@wifiname,");
			strSql.Append("notes=@notes,");
			strSql.Append("wifimac=@wifimac,");
			strSql.Append("mediaids=@mediaids");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.VarChar,100),
					new SqlParameter("@groupname", SqlDbType.VarChar,1000),
					new SqlParameter("@checkin_type", SqlDbType.VarChar,1000),
					new SqlParameter("@exception_type", SqlDbType.VarChar,1000),
					new SqlParameter("@checkin_time", SqlDbType.DateTime),
					new SqlParameter("@location_title", SqlDbType.VarChar,1000),
					new SqlParameter("@location_detail", SqlDbType.VarChar,1000),
					new SqlParameter("@wifiname", SqlDbType.VarChar,100),
					new SqlParameter("@notes", SqlDbType.VarChar,1000),
					new SqlParameter("@wifimac", SqlDbType.VarChar,100),
					new SqlParameter("@mediaids", SqlDbType.VarChar,1000),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.shopId;
			parameters[1].Value = model.userid;
			parameters[2].Value = model.groupname;
			parameters[3].Value = model.checkin_type;
			parameters[4].Value = model.exception_type;
			parameters[5].Value = model.checkin_time;
			parameters[6].Value = model.location_title;
			parameters[7].Value = model.location_detail;
			parameters[8].Value = model.wifiname;
			parameters[9].Value = model.notes;
			parameters[10].Value = model.wifimac;
			parameters[11].Value = model.mediaids;
			parameters[12].Value = model.id;

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
			strSql.Append("delete from kaoqinListWeixin ");
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
			strSql.Append("delete from kaoqinListWeixin ");
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
		public Lanwei.Weixin.Model.kaoqinListWeixin GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,shopId,userid,groupname,checkin_type,exception_type,checkin_time,location_title,location_detail,wifiname,notes,wifimac,mediaids from kaoqinListWeixin ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.kaoqinListWeixin model=new Lanwei.Weixin.Model.kaoqinListWeixin();
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
		public Lanwei.Weixin.Model.kaoqinListWeixin DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.kaoqinListWeixin model=new Lanwei.Weixin.Model.kaoqinListWeixin();
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
				if(row["userid"]!=null)
				{
					model.userid=row["userid"].ToString();
				}
				if(row["groupname"]!=null)
				{
					model.groupname=row["groupname"].ToString();
				}
				if(row["checkin_type"]!=null)
				{
					model.checkin_type=row["checkin_type"].ToString();
				}
				if(row["exception_type"]!=null)
				{
					model.exception_type=row["exception_type"].ToString();
				}
				if(row["checkin_time"]!=null && row["checkin_time"].ToString()!="")
				{
					model.checkin_time=DateTime.Parse(row["checkin_time"].ToString());
				}
				if(row["location_title"]!=null)
				{
					model.location_title=row["location_title"].ToString();
				}
				if(row["location_detail"]!=null)
				{
					model.location_detail=row["location_detail"].ToString();
				}
				if(row["wifiname"]!=null)
				{
					model.wifiname=row["wifiname"].ToString();
				}
				if(row["notes"]!=null)
				{
					model.notes=row["notes"].ToString();
				}
				if(row["wifimac"]!=null)
				{
					model.wifimac=row["wifimac"].ToString();
				}
				if(row["mediaids"]!=null)
				{
					model.mediaids=row["mediaids"].ToString();
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
            strSql.Append(" FROM kaoqinListWeixin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by checkin_time desc,username asc ");
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
			strSql.Append(" id,shopId,userid,groupname,checkin_type,exception_type,checkin_time,location_title,location_detail,wifiname,notes,wifimac,mediaids ");
            strSql.Append(" FROM viewkaoqinListWeixin ");
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
            strSql.Append("select count(1) FROM viewkaoqinListWeixin ");
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
			strSql.Append(")AS Row, T.*  from kaoqinListWeixin T ");
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
			parameters[0].Value = "kaoqinListWeixin";
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

