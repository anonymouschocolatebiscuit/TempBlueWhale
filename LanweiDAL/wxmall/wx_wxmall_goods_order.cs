using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_wxmall_goods_order
	/// </summary>
	public partial class wx_wxmall_goods_order
	{
		public wx_wxmall_goods_order()
		{}
		#region  BasicMethod


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_wxmall_goods_order ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Lanwei.Weixin.Model.wx_wxmall_goods_order model)
		{

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wxmall_goods_order(");
            strSql.Append("orderNum,openId,totalNum,totalMoney,isPay,isComDone,isWrite,isUserDone,isExpress,isFinish,addTime,addressText,remarks,totalMoneyAll,isKaipiao,isYuejie,yuejieDays)");
			strSql.Append(" values (");
            strSql.Append("@orderNum,@openId,@totalNum,@totalMoney,@isPay,@isComDone,@isWrite,@isUserDone,@isExpress,@isFinish,@addTime,@addressText,@remarks,@totalMoneyAll,@isKaipiao,@isYuejie,@yuejieDays)");
			strSql.Append(";select @@IDENTITY");

			SqlParameter[] parameters = {

					new SqlParameter("@orderNum", model.orderNum),
					new SqlParameter("@openId", model.openId),   
                   
                    new SqlParameter("@totalNum", model.totalNum),
                    new SqlParameter("@totalMoney", model.totalMoney),	

					new SqlParameter("@isPay", model.isPay),
                    new SqlParameter("@isComDone", model.isComDone),					
					new SqlParameter("@isWrite", model.isWrite),
                    new SqlParameter("@isUserDone",model.isUserDone),
                    new SqlParameter("@isExpress", model.isExpress),
                    new SqlParameter("@isFinish", model.isFinish),
					
                    new SqlParameter("@addTime", model.addTime),
					new SqlParameter("@addressText", model.addressText),					
                    new SqlParameter("@remarks", model.remarks),

                    new SqlParameter("@totalMoneyAll", model.totalMoneyAll),

                    new SqlParameter("@isKaipiao", model.isKaipiao),
                    new SqlParameter("@isYuejie", model.isYuejie),
                    new SqlParameter("@yuejieDays", model.yuejieDays),

                    
                                     
                                        };
			


			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return ConvertTo.ConvertInt(obj.ToString());
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_goods_order model)
		{

			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wxmall_goods_order set ");
			strSql.Append("orderNum=@orderNum,");
			strSql.Append("openId=@openId,");
            strSql.Append("totalNum=@totalNum,");
			strSql.Append("totalMoney=@totalMoney,");
			strSql.Append("isPay=@isPay,");
			strSql.Append("isUserDone=@isUserDone,");
			strSql.Append("isComDone=@isComDone,");
			strSql.Append("addTime=@addTime,");
			strSql.Append("payTime=@payTime,");
			strSql.Append("addressNum=@addressNum,");
			strSql.Append("addressText=@addressText,");
			strSql.Append("payWay=@payWay,");
			strSql.Append("isExpress=@isExpress,");
			strSql.Append("expressName=@expressName,");
			strSql.Append("expressNum=@expressNum,");
			strSql.Append("expressTime=@expressTime,");
			strSql.Append("comDoneTime=@comDoneTime,");
            strSql.Append("isWrite=@isWrite,");
            strSql.Append("writeTime=@writeTime,");
            strSql.Append("isFinish=@isFinish,");
            strSql.Append("finishTime=@finishTime,");

            strSql.Append("remarks=@remarks");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					

					new SqlParameter("@id", model.id),
                    new SqlParameter("@orderNum", model.orderNum),
					new SqlParameter("@openId", model.openId),
                    new SqlParameter("@totalNum", model.totalNum),
					new SqlParameter("@totalMoney", model.totalMoney),
					new SqlParameter("@isPay", model.isPay),
					new SqlParameter("@isUserDone",model.isUserDone),
					new SqlParameter("@isComDone", model.isComDone),
					new SqlParameter("@addTime", model.addTime),
					new SqlParameter("@payTime", model.payTime),
					new SqlParameter("@addressNum", model.addressNum),
					new SqlParameter("@addressText", model.addressText),
					new SqlParameter("@payWay", model.payWay),
					new SqlParameter("@isExpress", model.isExpress),
					new SqlParameter("@expressName", model.expressName),
					new SqlParameter("@expressNum", model.expressNum),
					new SqlParameter("@expressTime",model.expressTime),
					new SqlParameter("@comDoneTime", model.comDoneTime),

                     new SqlParameter("@isWrite", model.isWrite),
                    new SqlParameter("@writeTime", model.writeTime),
                    new SqlParameter("@isFinish", model.isFinish),
                    new SqlParameter("@finishTime", model.finishTime),

                    new SqlParameter("@remarks", model.remarks)
                                       

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsPay(Lanwei.Weixin.Model.wx_wxmall_goods_order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_wxmall_goods_order set ");
            
            strSql.Append("isPay=@isPay,");           
            strSql.Append("payTime=@payTime,");
            strSql.Append("payNumber=@payNumber");

            strSql.Append(" where orderNum=@orderNum ");
            SqlParameter[] parameters = {
                    new SqlParameter("@orderNum", model.orderNum),				
					new SqlParameter("@isPay", model.isPay),
					new SqlParameter("@payTime",model.payTime),
					new SqlParameter("@payNumber", model.payNumber)

                                       

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateComDone(int id, int isComDone,DateTime comDoneTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update wx_wxmall_goods_order set ");

            strSql.Append("isComDone=@isComDone, ");

            strSql.Append("comDoneTime=@comDoneTime ");

            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {

                    new SqlParameter("@isComDone", isComDone),	
                    new SqlParameter("@comDoneTime", comDoneTime),	
					new SqlParameter("@id", id)

        
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsWrite(int id, int isWrite,DateTime writeTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_wxmall_goods_order set ");

            strSql.Append(" isWrite=@isWrite,");
            strSql.Append(" writeTime=@writeTime ");

            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {

                    new SqlParameter("@writeTime", writeTime),				
					new SqlParameter("@isWrite", isWrite),
					new SqlParameter("@id",id)

                                       

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateUserDone(int id, int isUserDone,DateTime userDoneTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update wx_wxmall_goods_order set ");

            strSql.Append("isUserDone=@isUserDone, ");
            strSql.Append("userDoneTime=@userDoneTime ");


            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                    new SqlParameter("@isUserDone", isUserDone),	
			        new SqlParameter("@userDoneTime", userDoneTime),	
					new SqlParameter("@id", id)

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateExpress(int id, int isExpress, DateTime expressTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update wx_wxmall_goods_order set ");

            strSql.Append("isExpress=@isExpress, ");

            strSql.Append("expressTime=@expressTime ");

            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                    new SqlParameter("@expressTime", expressTime),	
			        new SqlParameter("@isExpress", isExpress),	
					new SqlParameter("@id", id)

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

        public bool UpdateExpressNum(int id, string expressName,string expressNum)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update wx_wxmall_goods_order set ");

            strSql.Append("expressName=@expressName, ");

            strSql.Append("expressNum=@expressNum ");

            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                    new SqlParameter("@expressName", expressName),	
			        new SqlParameter("@expressNum", expressNum),	
					new SqlParameter("@id", id)

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsFinish(int id,int isFinish,DateTime finishTime)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_wxmall_goods_order set ");

            strSql.Append("isFinish=@isFinish,");
            strSql.Append("finishTime=@finishTime ");

            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", id),				
					new SqlParameter("@isFinish", isFinish),
					new SqlParameter("@finishTime",finishTime)

                                       

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
		public bool Delete(long id)
		{
			
			StringBuilder strSql=new StringBuilder();

            strSql.Append("delete from wx_wxmall_goods_orderdetal ");
            strSql.Append(" where orderId=@id ");


			strSql.Append(" delete from wx_wxmall_goods_order ");
			strSql.Append(" where id=@id ");


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



		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wxmall_goods_order ");
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_order GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from wx_wxmall_goods_order ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wx_wxmall_goods_order model=new Lanwei.Weixin.Model.wx_wxmall_goods_order();
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_order DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_goods_order model=new Lanwei.Weixin.Model.wx_wxmall_goods_order();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=long.Parse(row["id"].ToString());
				}
				if(row["orderNum"]!=null)
				{
					model.orderNum=row["orderNum"].ToString();
				}


                

				if(row["openId"]!=null)
				{
					model.openId=row["openId"].ToString();
				}

                if (row["totalNum"] != null && row["totalNum"].ToString() != "")
                {
                    model.totalNum = int.Parse(row["totalNum"].ToString());
                }

				if(row["totalMoney"]!=null && row["totalMoney"].ToString()!="")
				{
					model.totalMoney=decimal.Parse(row["totalMoney"].ToString());
				}


				if(row["isPay"]!=null && row["isPay"].ToString()!="")
				{
					model.isPay=int.Parse(row["isPay"].ToString());
				}
                if (row["payTime"] != null && row["payTime"].ToString() != "")
                {
                    model.payTime = DateTime.Parse(row["payTime"].ToString());
                }
                if (row["payNumber"] != null)
                {
                    model.payNumber = row["payNumber"].ToString();
                }


                if (row["isComDone"] != null && row["isComDone"].ToString() != "")
                {
                    model.isComDone = int.Parse(row["isComDone"].ToString());
                }
                if (row["comDoneTime"] != null && row["comDoneTime"].ToString() != "")
                {
                    model.comDoneTime = DateTime.Parse(row["comDoneTime"].ToString());
                }

                if (row["isWrite"] != null && row["isWrite"].ToString() != "")
                {
                    model.isWrite = int.Parse(row["isWrite"].ToString());
                }
                if (row["writeTime"] != null && row["writeTime"].ToString() != "")
                {
                    model.writeTime = DateTime.Parse(row["writeTime"].ToString());
                }


				if(row["isUserDone"]!=null && row["isUserDone"].ToString()!="")
				{
					model.isUserDone=int.Parse(row["isUserDone"].ToString());
				}
                if (row["userDoneTime"] != null && row["userDoneTime"].ToString() != "")
                {
                    model.userDoneTime = DateTime.Parse(row["userDoneTime"].ToString());
                }


                if (row["isExpress"] != null && row["isExpress"].ToString() != "")
                {
                    model.isExpress = int.Parse(row["isExpress"].ToString());
                }
                if (row["expressTime"] != null && row["expressTime"].ToString() != "")
                {
                    model.expressTime = DateTime.Parse(row["expressTime"].ToString());
                }

                if (row["isFinish"] != null && row["isFinish"].ToString() != "")
                {
                    model.isFinish = int.Parse(row["isFinish"].ToString());
                }

                if (row["finishTime"] != null && row["finishTime"].ToString() != "")
                {
                    model.finishTime = DateTime.Parse(row["finishTime"].ToString());
                }


			
                if(row["addTime"]!=null && row["addTime"].ToString()!="")
				{
					model.addTime=DateTime.Parse(row["addTime"].ToString());
				}

				

				if(row["addressNum"]!=null)
				{
					model.addressNum=row["addressNum"].ToString();
				}
				if(row["addressText"]!=null)
				{
					model.addressText=row["addressText"].ToString();
				}
				if(row["payWay"]!=null && row["payWay"].ToString()!="")
				{
					model.payWay=int.Parse(row["payWay"].ToString());
				}
				
				if(row["expressName"]!=null)
				{
					model.expressName=row["expressName"].ToString();
				}
				if(row["expressNum"]!=null)
				{
					model.expressNum=row["expressNum"].ToString();
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM wx_wxmall_goods_order ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 查询SQL
        /// </summary>
        public DataSet GetListSQL(string strWhere)
        {
            return DbHelperSQL.Query(strWhere.ToString());
        }


        /// <summary>
        /// 增加搜索关键词
        /// </summary>
        public int AddHotKey(string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_wxmall_goods_hotKeys(");
            strSql.Append("keys,makeDate)");
            strSql.Append(" values (");
            strSql.Append("@key,getdate() )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                                            new SqlParameter("@key", key)
                                        };

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
           

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
			strSql.Append(" FROM wx_wxmall_goods_order ");
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
			strSql.Append("select count(1) FROM wx_wxmall_goods_order ");
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
			strSql.Append(")AS Row, T.*  from wx_wxmall_goods_order T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}



        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM wx_wxmall_goods_order");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


		#endregion  BasicMethod

        #region  ExtensionMethod

        /// <summary>
        /// 计算某个字段的总和值
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataSet GetSumField(string field, string where)
        {
            String sql = @"select SUM({0}) as {0} from  wx_wxmall_goods_order {1}";
            if (String.IsNullOrEmpty(field))
                return null;
            else
            {
                sql = String.Format(sql, field, String.IsNullOrEmpty(where) ? "" : " where " + where);

            }
            return DbHelperSQL.Query(sql);
        }


        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount, string isOur)
        {
            String sql = @"select * from wx_wxmall_goods_order {0}";
            sql = string.Format(sql, strWhere.Trim() != "" ? " where " + strWhere : "");
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(sql)));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, sql, filedOrder));
        }


        /// <summary>
        /// 修改物流打单
        /// </summary>
        public bool UpdateField(long id, string strValue)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update wx_wxmall_goods_order set " + strValue);
                strSql.Append(" where id=" + id);
                DbHelperSQL.ExecuteSql(strSql.ToString());
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }




        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string strValue, string where)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update wx_wxmall_goods_order set " + strValue);
                strSql.Append(" where " + where);
                DbHelperSQL.ExecuteSql(strSql.ToString());
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion  ExtensionMethod
	}
}

