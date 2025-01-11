using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references
namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:produceGetList
	/// </summary>
	public partial class produceGetList
	{
		public produceGetList()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "produceGetList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from produceGetList");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        #region 自动生成单据编号


        /// <summary>
        /// 生成单据编号
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                                       
                                      new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","SCLL"),//单据代码前四位字母                                 
                                       new SqlParameter("@tableName","produceGetList")//表
                                      
                                       

                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();

        }

        #endregion

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Lanwei.Weixin.Model.produceGetList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into produceGetList(");
			strSql.Append("shopId,number,deptId,planNumber,goodsId,num,makeId,makeDate,bizId,bizDate,remarks,flag)");
			strSql.Append(" values (");
			strSql.Append("@shopId,@number,@deptId,@planNumber,@goodsId,@num,@makeId,@makeDate,@bizId,@bizDate,@remarks,@flag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@number", SqlDbType.VarChar,100),
					new SqlParameter("@deptId", SqlDbType.Int,4),
					new SqlParameter("@planNumber", SqlDbType.VarChar,100),
					new SqlParameter("@goodsId", SqlDbType.Int,4),
					new SqlParameter("@num", SqlDbType.Float,8),
					new SqlParameter("@makeId", SqlDbType.Int,4),
					new SqlParameter("@makeDate", SqlDbType.DateTime),
					new SqlParameter("@bizId", SqlDbType.Int,4),
					new SqlParameter("@bizDate", SqlDbType.DateTime),
					new SqlParameter("@remarks", SqlDbType.VarChar,1000),
                    new SqlParameter("@flag", SqlDbType.VarChar,100)                    
                                        };
			parameters[0].Value = model.shopId;
			parameters[1].Value = model.number;
			parameters[2].Value = model.deptId;
			parameters[3].Value = model.planNumber;
			parameters[4].Value = model.goodsId;
			parameters[5].Value = model.num;
			parameters[6].Value = model.makeId;
			parameters[7].Value = model.makeDate;
			parameters[8].Value = model.bizId;
			parameters[9].Value = model.bizDate;
			parameters[10].Value = model.remarks;
            parameters[11].Value = model.flag;

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
		public bool Update(Lanwei.Weixin.Model.produceGetList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update produceGetList set ");

			strSql.Append("shopId=@shopId,");
			strSql.Append("number=@number,");
			strSql.Append("deptId=@deptId,");
			strSql.Append("planNumber=@planNumber,");
			strSql.Append("goodsId=@goodsId,");
			strSql.Append("num=@num,");
			strSql.Append("bizId=@bizId,");
			strSql.Append("bizDate=@bizDate,");
			strSql.Append("remarks=@remarks");

			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@number", SqlDbType.VarChar,100),
					new SqlParameter("@deptId", SqlDbType.Int,4),
					new SqlParameter("@planNumber", SqlDbType.VarChar,100),
					new SqlParameter("@goodsId", SqlDbType.Int,4),
					new SqlParameter("@num", SqlDbType.Float,8),					
					new SqlParameter("@bizId", SqlDbType.Int,4),
					new SqlParameter("@bizDate", SqlDbType.DateTime),					
					new SqlParameter("@remarks", SqlDbType.VarChar,1000),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.shopId;
			parameters[1].Value = model.number;
			parameters[2].Value = model.deptId;
			parameters[3].Value = model.planNumber;
			parameters[4].Value = model.goodsId;
			parameters[5].Value = model.num;		
			parameters[6].Value = model.bizId;
			parameters[7].Value = model.bizDate;		
			parameters[8].Value = model.remarks;
			parameters[9].Value = model.id;

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
			strSql.Append("delete from produceGetList ");
			strSql.Append(" where id=@id and flag<>'审核' ");
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
			strSql.Append("delete from produceGetList ");
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
		public Lanwei.Weixin.Model.produceGetList GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from viewProduceGetList ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.produceGetList model=new Lanwei.Weixin.Model.produceGetList();
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
		public Lanwei.Weixin.Model.produceGetList DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.produceGetList model=new Lanwei.Weixin.Model.produceGetList();
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
				if(row["number"]!=null)
				{
					model.number=row["number"].ToString();
				}
				if(row["deptId"]!=null && row["deptId"].ToString()!="")
				{
					model.deptId=int.Parse(row["deptId"].ToString());
				}
				if(row["planNumber"]!=null)
				{
					model.planNumber=row["planNumber"].ToString();
				}
				if(row["goodsId"]!=null && row["goodsId"].ToString()!="")
				{
					model.goodsId=int.Parse(row["goodsId"].ToString());
				}
				if(row["num"]!=null && row["num"].ToString()!="")
				{
					model.num=decimal.Parse(row["num"].ToString());
				}
				if(row["makeId"]!=null && row["makeId"].ToString()!="")
				{
					model.makeId=int.Parse(row["makeId"].ToString());
				}
				if(row["makeDate"]!=null && row["makeDate"].ToString()!="")
				{
					model.makeDate=DateTime.Parse(row["makeDate"].ToString());
				}
				if(row["bizId"]!=null && row["bizId"].ToString()!="")
				{
					model.bizId=int.Parse(row["bizId"].ToString());
				}
				if(row["bizDate"]!=null && row["bizDate"].ToString()!="")
				{
					model.bizDate=DateTime.Parse(row["bizDate"].ToString());
				}
				if(row["checkId"]!=null && row["checkId"].ToString()!="")
				{
					model.checkId=int.Parse(row["checkId"].ToString());
				}
				if(row["checkDate"]!=null && row["checkDate"].ToString()!="")
				{
					model.checkDate=DateTime.Parse(row["checkDate"].ToString());
				}
				if(row["remarks"]!=null)
				{
					model.remarks=row["remarks"].ToString();
				}

                if (row["flag"] != null)
                {
                    model.flag = row["flag"].ToString();
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
			strSql.Append(" FROM viewProduceGetList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        #region 审核一条记录

        /// <summary>
        /// 审核一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from ProduceGetList where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update ProduceGetList set flag='" + flag + "' ";
            if (flag == "审核")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "保存")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

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
			strSql.Append(" FROM viewProduceGetList ");
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
            strSql.Append("select count(1) FROM viewProduceGetList ");
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
            strSql.Append(")AS Row, T.*  from viewProduceGetList T ");
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
			parameters[0].Value = "produceGetList";
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

