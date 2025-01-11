using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_wxmall_users_address
	/// </summary>
	public partial class wx_wxmall_users_address
	{
		public wx_wxmall_users_address()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "wx_wxmall_users_address"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wxmall_users_address");
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
		public int Add(Lanwei.Weixin.Model.wx_wxmall_users_address model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wxmall_users_address(");
			strSql.Append("openId,username,phone,province,city,country,addressDetail,flag,makeDate)");
			strSql.Append(" values (");
			strSql.Append("@openId,@username,@phone,@province,@city,@country,@addressDetail,@flag,@makeDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@openId", model.openId),
					new SqlParameter("@username", model.username),
					new SqlParameter("@phone", model.phone),
					new SqlParameter("@province", model.province),				
					new SqlParameter("@city", model.city),					
					new SqlParameter("@country", model.country),					
					new SqlParameter("@addressDetail", model.addressDetail),
					new SqlParameter("@flag", model.flag),
					new SqlParameter("@makeDate", model.makeDate)};
		

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
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_users_address model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wxmall_users_address set ");
			strSql.Append("username=@username,");
			strSql.Append("phone=@phone,");
			strSql.Append("province=@province,");
			strSql.Append("city=@city,");
			strSql.Append("country=@country,");	
			strSql.Append("addressDetail=@addressDetail");

			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					
					new SqlParameter("@username",model.username),
					new SqlParameter("@phone", model.phone),
					new SqlParameter("@province", model.province),					
					new SqlParameter("@city", model.city),					
					new SqlParameter("@country", model.country),				
					new SqlParameter("@addressDetail", model.addressDetail),

					new SqlParameter("@id", model.id)};
			
			

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
        /// 更新一条数据
        /// </summary>
        public bool Update(int id,string openId)
        {
            StringBuilder strSql = new StringBuilder();
           
            strSql.Append(" update wx_wxmall_users_address set ");
            strSql.Append("flag=1 ");
            strSql.Append(" where id=@id and openId=@openId ");

            strSql.Append(" update wx_wxmall_users_address set ");
            strSql.Append("flag=0 ");
            strSql.Append(" where id<>@id and openId=@openId");




            SqlParameter[] parameters = {
					
					new SqlParameter("@id",id),
					new SqlParameter("@openId", openId)
					
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wxmall_users_address ");
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
			strSql.Append("delete from wx_wxmall_users_address ");
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
		public Lanwei.Weixin.Model.wx_wxmall_users_address GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,openId,username,phone,province,codeProvince,city,codeCity,country,codeCountry,addressDetail,flag,makeDate from wx_wxmall_users_address ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wx_wxmall_users_address model=new Lanwei.Weixin.Model.wx_wxmall_users_address();
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
		public Lanwei.Weixin.Model.wx_wxmall_users_address DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_users_address model=new Lanwei.Weixin.Model.wx_wxmall_users_address();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["openId"]!=null && row["openId"].ToString()!="")
				{
                    model.openId = row["openId"].ToString();
				}
				if(row["username"]!=null)
				{
					model.username=row["username"].ToString();
				}
				if(row["phone"]!=null)
				{
					model.phone=row["phone"].ToString();
				}
				if(row["province"]!=null)
				{
					model.province=row["province"].ToString();
				}
				if(row["codeProvince"]!=null)
				{
					model.codeProvince=row["codeProvince"].ToString();
				}
				if(row["city"]!=null)
				{
					model.city=row["city"].ToString();
				}
				if(row["codeCity"]!=null)
				{
					model.codeCity=row["codeCity"].ToString();
				}
				if(row["country"]!=null)
				{
					model.country=row["country"].ToString();
				}
				if(row["codeCountry"]!=null)
				{
					model.codeCountry=row["codeCountry"].ToString();
				}
				if(row["addressDetail"]!=null)
				{
					model.addressDetail=row["addressDetail"].ToString();
				}
				if(row["flag"]!=null && row["flag"].ToString()!="")
				{
					model.flag=int.Parse(row["flag"].ToString());
				}
				if(row["makeDate"]!=null && row["makeDate"].ToString()!="")
				{
					model.makeDate=DateTime.Parse(row["makeDate"].ToString());
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
			strSql.Append("select id,openId,username,phone,province,codeProvince,city,codeCity,country,codeCountry,addressDetail,flag,makeDate ");
			strSql.Append(" FROM wx_wxmall_users_address ");
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
			strSql.Append(" id,openId,username,phone,province,codeProvince,city,codeCity,country,codeCountry,addressDetail,flag,makeDate ");
			strSql.Append(" FROM wx_wxmall_users_address ");
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
			strSql.Append("select count(1) FROM wx_wxmall_users_address ");
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
			strSql.Append(")AS Row, T.*  from wx_wxmall_users_address T ");
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
			parameters[0].Value = "wx_wxmall_users_address";
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

