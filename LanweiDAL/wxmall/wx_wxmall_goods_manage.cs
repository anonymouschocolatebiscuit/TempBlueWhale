using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;//Please add references
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:wx_wxmall_goods_manage
	/// </summary>
	public partial class wx_wxmall_goods_manage
	{
		public wx_wxmall_goods_manage()
		{}
		#region  BasicMethod




		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Lanwei.Weixin.Model.wx_wxmall_goods_manage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wxmall_goods_manage(");
			strSql.Append("wId,shopId,typeId,namesCN,namesEN,capacity,grade,winery,place,varieties,years,degree,temperature,remarks,num,costPrice,showPrice,salesPrice,sort,showImgs,status,ishot)");
			strSql.Append(" values (");
            strSql.Append("@wId,@shopId,@typeId,@namesCN,@namesEN,@capacity,@grade,@winery,@place,@varieties,@years,@degree,@temperature,@remarks,@num,@costPrice,@showPrice,@salesPrice,@sort,@showImgs,@status,@ishot)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@namesCN", SqlDbType.NVarChar,200),
					new SqlParameter("@namesEN", SqlDbType.NVarChar,200),
					new SqlParameter("@capacity", SqlDbType.NVarChar,50),
					new SqlParameter("@grade", SqlDbType.NVarChar,2000),
					new SqlParameter("@winery", SqlDbType.NVarChar,100),
					new SqlParameter("@place", SqlDbType.NVarChar,100),
					new SqlParameter("@varieties", SqlDbType.NVarChar,100),
					new SqlParameter("@years", SqlDbType.NVarChar,100),
					new SqlParameter("@degree", SqlDbType.NVarChar,100),
					new SqlParameter("@temperature", SqlDbType.NVarChar,20),
					new SqlParameter("@remarks", SqlDbType.Text),
					new SqlParameter("@num", SqlDbType.Int,4),
					new SqlParameter("@costPrice", SqlDbType.Decimal,9),
					new SqlParameter("@showPrice", SqlDbType.Decimal,9),
					new SqlParameter("@salesPrice", SqlDbType.Decimal,9),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@showImgs", SqlDbType.NVarChar,1000),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@ishot", SqlDbType.Int,4),
                    new SqlParameter("@wId", SqlDbType.Int,4),
                    new SqlParameter("@shopId", SqlDbType.Int,4)
                                        
                                        };
			parameters[0].Value = model.typeId;
			parameters[1].Value = model.namesCN;
			parameters[2].Value = model.namesEN;
			parameters[3].Value = model.capacity;
			parameters[4].Value = model.grade;
			parameters[5].Value = model.winery;
			parameters[6].Value = model.place;
			parameters[7].Value = model.varieties;
			parameters[8].Value = model.years;
			parameters[9].Value = model.degree;
			parameters[10].Value = model.temperature;
			parameters[11].Value = model.remarks;
			parameters[12].Value = model.num;
			parameters[13].Value = model.costPrice;
			parameters[14].Value = model.showPrice;
			parameters[15].Value = model.salesPrice;
			parameters[16].Value = model.sort;
			parameters[17].Value = model.showImgs;
			parameters[18].Value = model.status;
			parameters[19].Value = model.ishot;

            parameters[20].Value = model.wId;
            parameters[21].Value = model.shopId;


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
        /// 增加一条数据
        /// </summary>
        public int AddGoodsAttach(int goodsId,int attachId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_wxmall_goods_attach(goodsId,attachId) ");
            strSql.Append(" values ('"+goodsId+"','"+attachId+"')");
            strSql.Append(";select @@IDENTITY");
           


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
		/// 更新一条数据
		/// </summary>
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_goods_manage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wxmall_goods_manage set ");
			strSql.Append("typeId=@typeId,");
			strSql.Append("namesCN=@namesCN,");
			strSql.Append("namesEN=@namesEN,");
			strSql.Append("capacity=@capacity,");
			strSql.Append("grade=@grade,");
			strSql.Append("winery=@winery,");
			strSql.Append("place=@place,");
			strSql.Append("varieties=@varieties,");
			strSql.Append("years=@years,");
			strSql.Append("degree=@degree,");
			strSql.Append("temperature=@temperature,");
			strSql.Append("remarks=@remarks,");
			strSql.Append("num=@num,");
			strSql.Append("costPrice=@costPrice,");
			strSql.Append("showPrice=@showPrice,");
			strSql.Append("salesPrice=@salesPrice,");
			strSql.Append("sort=@sort,");
			strSql.Append("showImgs=@showImgs,");
			strSql.Append("status=@status,");
			strSql.Append("ishot=@ishot");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@namesCN", SqlDbType.NVarChar,200),
					new SqlParameter("@namesEN", SqlDbType.NVarChar,200),
					new SqlParameter("@capacity", SqlDbType.NVarChar,50),
					new SqlParameter("@grade", SqlDbType.NVarChar,2000),
					new SqlParameter("@winery", SqlDbType.NVarChar,100),
					new SqlParameter("@place", SqlDbType.NVarChar,100),
					new SqlParameter("@varieties", SqlDbType.NVarChar,100),
					new SqlParameter("@years", SqlDbType.NVarChar,100),
					new SqlParameter("@degree", SqlDbType.NVarChar,100),
					new SqlParameter("@temperature", SqlDbType.NVarChar,20),
					new SqlParameter("@remarks", SqlDbType.Text),
					new SqlParameter("@num", SqlDbType.Int,4),
					new SqlParameter("@costPrice", SqlDbType.Decimal,9),
					new SqlParameter("@showPrice", SqlDbType.Decimal,9),
					new SqlParameter("@salesPrice", SqlDbType.Decimal,9),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@showImgs", SqlDbType.NVarChar,1000),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@ishot", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.typeId;
			parameters[1].Value = model.namesCN;
			parameters[2].Value = model.namesEN;
			parameters[3].Value = model.capacity;
			parameters[4].Value = model.grade;
			parameters[5].Value = model.winery;
			parameters[6].Value = model.place;
			parameters[7].Value = model.varieties;
			parameters[8].Value = model.years;
			parameters[9].Value = model.degree;
			parameters[10].Value = model.temperature;
			parameters[11].Value = model.remarks;
			parameters[12].Value = model.num;
			parameters[13].Value = model.costPrice;
			parameters[14].Value = model.showPrice;
			parameters[15].Value = model.salesPrice;
			parameters[16].Value = model.sort;
			parameters[17].Value = model.showImgs;
			parameters[18].Value = model.status;
			parameters[19].Value = model.ishot;
			parameters[20].Value = model.id;

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
			strSql.Append("delete from wx_wxmall_goods_manage ");
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteAttach(int goodsId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_wxmall_goods_attach ");
            strSql.Append(" where goodsId=@goodsId");
            SqlParameter[] parameters = {
					new SqlParameter("@goodsId", SqlDbType.Int,4)
			};
            parameters[0].Value = goodsId;

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
			strSql.Append("delete from wx_wxmall_goods_manage ");
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_manage GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from wx_wxmall_goods_manage ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Lanwei.Weixin.Model.wx_wxmall_goods_manage model=new Lanwei.Weixin.Model.wx_wxmall_goods_manage();
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
        public Lanwei.Weixin.Model.wx_wxmall_goods_manage GetModel(string id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from wx_wxmall_goods_manage ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Lanwei.Weixin.Model.wx_wxmall_goods_manage model = new Lanwei.Weixin.Model.wx_wxmall_goods_manage();
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_manage DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_goods_manage model=new Lanwei.Weixin.Model.wx_wxmall_goods_manage();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["typeId"]!=null && row["typeId"].ToString()!="")
				{
					model.typeId=int.Parse(row["typeId"].ToString());
				}
				if(row["namesCN"]!=null)
				{
					model.namesCN=row["namesCN"].ToString();
				}
				if(row["namesEN"]!=null)
				{
					model.namesEN=row["namesEN"].ToString();
				}
				if(row["capacity"]!=null)
				{
					model.capacity=row["capacity"].ToString();
				}
				if(row["grade"]!=null)
				{
					model.grade=row["grade"].ToString();
				}
				if(row["winery"]!=null)
				{
					model.winery=row["winery"].ToString();
				}
				if(row["place"]!=null)
				{
					model.place=row["place"].ToString();
				}
				if(row["varieties"]!=null)
				{
					model.varieties=row["varieties"].ToString();
				}
				if(row["years"]!=null)
				{
					model.years=row["years"].ToString();
				}
				if(row["degree"]!=null)
				{
					model.degree=row["degree"].ToString();
				}
				if(row["temperature"]!=null)
				{
					model.temperature=row["temperature"].ToString();
				}
				if(row["remarks"]!=null)
				{
					model.remarks=row["remarks"].ToString();
				}
				if(row["num"]!=null && row["num"].ToString()!="")
				{
					model.num=int.Parse(row["num"].ToString());
				}
				if(row["costPrice"]!=null && row["costPrice"].ToString()!="")
				{
					model.costPrice=decimal.Parse(row["costPrice"].ToString());
				}
				if(row["showPrice"]!=null && row["showPrice"].ToString()!="")
				{
					model.showPrice=decimal.Parse(row["showPrice"].ToString());
				}
				if(row["salesPrice"]!=null && row["salesPrice"].ToString()!="")
				{
					model.salesPrice=decimal.Parse(row["salesPrice"].ToString());
				}
				if(row["sort"]!=null && row["sort"].ToString()!="")
				{
					model.sort=int.Parse(row["sort"].ToString());
				}
				if(row["showImgs"]!=null)
				{
					model.showImgs=row["showImgs"].ToString();
				}
				if(row["status"]!=null && row["status"].ToString()!="")
				{
					model.status=int.Parse(row["status"].ToString());
				}
				if(row["ishot"]!=null && row["ishot"].ToString()!="")
				{
					model.ishot=int.Parse(row["ishot"].ToString());
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
            strSql.Append(" FROM view_wx_wxmall_goods_manage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListSQL(string strWhere)
        {
            return DbHelperSQL.Query(strWhere.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int isOnShelf, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_wx_wxmall_goods_manage where 1=1 ");
            strSql.Append(" and status = " + (isOnShelf > 0 ? isOnShelf : 1));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and  " + strWhere);
            }
           

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
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
			strSql.Append(" FROM view_wx_wxmall_goods_manage ");
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
			strSql.Append("select count(1) FROM wx_wxmall_goods_manage ");
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
			strSql.Append(")AS Row, T.*  from view_wx_wxmall_goods_manage T ");
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
			parameters[0].Value = "wx_wxmall_goods_manage";
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

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(long id, string strValue)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update wx_wxmall_goods_manage set " + strValue);
                strSql.Append(" where id=" + id);
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

