﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references
namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:usersList
	/// </summary>
	public partial class usersList
	{
		public usersList()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "usersList"); 
		}


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string openId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from usersList");
            strSql.Append(" where openId=@openId");
            SqlParameter[] parameters = {
					new SqlParameter("@openId",openId)
			};
           


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 判断是否有存在身份证号码的企业
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public bool ExistsCardNumber(string cardNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from companyList");
            strSql.Append(" where cardNumber=@cardNumber");
            SqlParameter[] parameters = {
					new SqlParameter("@cardNumber",cardNumber)
			};



            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistsTaxNumber(string taxNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from companyList");
            strSql.Append(" where taxNumber=@taxNumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@taxNumber",taxNumber)
			};
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistsTaxNumberAndPhone(string taxNumber,string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from companyList");
            strSql.Append(" where taxNumber=@taxNumber and phone=@phone");
            SqlParameter[] parameters = {
					new SqlParameter("@taxNumber",taxNumber),
                    new SqlParameter("@phone",phone)
			};
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistsTaxNumberBind(string taxNumber,string openId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from usersList ");
            strSql.Append(" where cardNumber=@taxNumber and openId<>@openId ");           
            SqlParameter[] parameters = {
					new SqlParameter("@taxNumber",taxNumber),
                    new SqlParameter("@openId",openId)
			};
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from usersList");
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
		public int Add(Lanwei.Weixin.Model.usersList model)
		{

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into usersList(");
			strSql.Append("openId,nickName,gender,province,country,city,avatarUrl,makeDate,fromWhere,cardNumber,names,phone,location,locationX,locationY)");
			strSql.Append(" values (");
            strSql.Append("@openId,@nickName,@gender,@province,@country,@city,@avatarUrl,@makeDate,@fromWhere,@cardNumber,@names,@phone,@location,@locationX,@locationY)");
			strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

					new SqlParameter("@openId", model.openId),
					new SqlParameter("@nickName", model.nickName),
					new SqlParameter("@gender", model.gender),
					new SqlParameter("@province", model.province),
					new SqlParameter("@country", model.country),
                    new SqlParameter("@city", model.city),
					new SqlParameter("@avatarUrl", model.avatarUrl),
					new SqlParameter("@makeDate", model.makeDate),
					new SqlParameter("@fromWhere", model.fromWhere),
                    new SqlParameter("@cardNumber", model.cardNumber),
                    new SqlParameter("@names", model.names),
                    new SqlParameter("@phone", model.phone),
					new SqlParameter("@location", model.location),
					new SqlParameter("@locationX", model.locationX),
					new SqlParameter("@locationY", model.locationY)

                                        };

			

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
		public bool Update(Lanwei.Weixin.Model.usersList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update usersList set ");
			strSql.Append("nickName=@nickName,");
			strSql.Append("gender=@gender,");
			strSql.Append("province=@province,");
			strSql.Append("country=@country,");
			strSql.Append("avatarUrl=@avatarUrl,");
            strSql.Append("city=@city,");
            strSql.Append("cardNumber=@cardNumber,");
            strSql.Append("names=@names,");
            strSql.Append("phone=@phone,");
			strSql.Append("location=@location,");
			strSql.Append("locationX=@locationX,");
			strSql.Append("locationY=@locationY");
            strSql.Append(" where openId=@openId");
			SqlParameter[] parameters = {
					new SqlParameter("@openId", model.openId),
					new SqlParameter("@nickName", model.nickName),
					new SqlParameter("@gender", model.gender),
					new SqlParameter("@province", model.province),
					new SqlParameter("@country", model.country),
                    new SqlParameter("@city", model.city),
					new SqlParameter("@avatarUrl", model.avatarUrl),	
                    new SqlParameter("@cardNumber", model.cardNumber),
	                new SqlParameter("@names", model.names),
                    new SqlParameter("@phone", model.phone),
					new SqlParameter("@location", model.location),
					new SqlParameter("@locationX", model.locationX),
					new SqlParameter("@locationY", model.locationY)

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from usersList ");
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
			strSql.Append("delete from usersList ");
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
		public Lanwei.Weixin.Model.usersList GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from usersList ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.usersList model=new Lanwei.Weixin.Model.usersList();
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
        public Lanwei.Weixin.Model.usersList GetModel(string openId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from usersList ");
            strSql.Append(" where openId=@openId");
            SqlParameter[] parameters = {
					new SqlParameter("@openId",openId)
			};
          

            Lanwei.Weixin.Model.usersList model = new Lanwei.Weixin.Model.usersList();
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
		public Lanwei.Weixin.Model.usersList DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.usersList model=new Lanwei.Weixin.Model.usersList();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["openId"]!=null)
				{
					model.openId=row["openId"].ToString();
				}
				if(row["nickName"]!=null)
				{
					model.nickName=row["nickName"].ToString();
				}
				if(row["gender"]!=null && row["gender"].ToString()!="")
				{
					model.gender=int.Parse(row["gender"].ToString());
				}
				if(row["province"]!=null)
				{
					model.province=row["province"].ToString();
				}
				if(row["country"]!=null)
				{
					model.country=row["country"].ToString();
				}
                if (row["city"] != null)
                {
                    model.city = row["city"].ToString();
                }
				if(row["avatarUrl"]!=null)
				{
					model.avatarUrl=row["avatarUrl"].ToString();
				}
				if(row["makeDate"]!=null && row["makeDate"].ToString()!="")
				{
					model.makeDate=DateTime.Parse(row["makeDate"].ToString());
				}
				if(row["fromWhere"]!=null)
				{
					model.fromWhere=row["fromWhere"].ToString();
				}

                if (row["cardNumber"] != null)
                {
                    model.cardNumber = row["cardNumber"].ToString();
                }

                if (row["names"] != null)
                {
                    model.names = row["names"].ToString();
                }
                if (row["phone"] != null)
                {
                    model.phone = row["phone"].ToString();
                }

				if(row["location"]!=null)
				{
					model.location=row["location"].ToString();
				}
				if(row["locationX"]!=null)
				{
					model.locationX=row["locationX"].ToString();
				}
				if(row["locationY"]!=null)
				{
					model.locationY=row["locationY"].ToString();
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
			strSql.Append(" FROM usersList ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM usersList ");
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
			strSql.Append("select count(1) FROM usersList ");
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
			strSql.Append(")AS Row, T.*  from usersList T ");
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
			parameters[0].Value = "usersList";
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

