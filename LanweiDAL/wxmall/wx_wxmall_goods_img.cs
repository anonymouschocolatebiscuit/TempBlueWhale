
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Common;
using System.Collections.Generic;
namespace Lanwei.Weixin.DAL
{
	/// <summary>
    /// 商品图片
	/// </summary>
	public partial class wx_wxmall_goods_img
	{
		public wx_wxmall_goods_img()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "wx_wxmall_goods_img"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wxmall_goods_img");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Lanwei.Weixin.Model.wx_wxmall_goods_img model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wxmall_goods_img(");
            strSql.Append("goodsId,imgName,imgUri)");
			strSql.Append(" values (");
            strSql.Append("@goodsId,@imgName,@imgUri)");
			SqlParameter[] parameters = {
				
					new SqlParameter("@goodsId",model.goodsId),
					new SqlParameter("@imgName", model.imgName),
					new SqlParameter("@imgUri", model.imgUri)
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
		public bool Update(Lanwei.Weixin.Model.wx_wxmall_goods_img model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update wx_wxmall_goods_img set ");

            strSql.Append("goodsId=@goodsId,");
            strSql.Append("imgName=@imgName,");
            strSql.Append("imgUri=@imgUri");
			strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@goodsId",model.goodsId),
					new SqlParameter("@imgName", model.imgName),
					new SqlParameter("@imgUri", model.imgUri),
					new SqlParameter("@id",model.id)};
           

			

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
			strSql.Append("delete from wx_wxmall_goods_img ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)			};
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
        public bool DeleteRows(int goodsId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_wxmall_goods_img ");
            strSql.Append(" where goodsId=@goodsId ");
            SqlParameter[] parameters = {
					new SqlParameter("@goodsId", goodsId)		
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
			strSql.Append("delete from wx_wxmall_goods_img ");
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_img GetModel(long id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from wx_wxmall_goods_img ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)			};
			parameters[0].Value = id;
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
        /// 通过商品编号
        /// </summary>
        public Lanwei.Weixin.Model.wx_wxmall_goods_img GetModel(string goodsNum,string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from wx_wxmall_goods_img ");
            strSql.Append(" where goodsNum=@goodsNum and " + where);
            SqlParameter[] parameters = {
					new SqlParameter("@goodsNum", SqlDbType.NVarChar,50)			};
            parameters[0].Value = goodsNum;
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
		public Lanwei.Weixin.Model.wx_wxmall_goods_img DataRowToModel(DataRow row)
		{
			Lanwei.Weixin.Model.wx_wxmall_goods_img model=new Lanwei.Weixin.Model.wx_wxmall_goods_img();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=long.Parse(row["id"].ToString());
                }


                if (row["goodsId"] != null)
				{
                    model.goodsId =ConvertTo.ConvertInt(row["goodsId"].ToString());
                }
                if (row["imgName"] != null)
                {
                    model.imgName = row["imgName"].ToString();
                }
                if (row["imgUri"] != null)
                {
                    model.imgUri = row["imgUri"].ToString();
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
			strSql.Append(" FROM wx_wxmall_goods_img ");
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
			strSql.Append(" FROM wx_wxmall_goods_img ");
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
			strSql.Append("select count(1) FROM wx_wxmall_goods_img ");
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
			strSql.Append(")AS Row, T.*  from wx_wxmall_goods_img T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.wx_wxmall_goods_img> models, String goodsNum,string where)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.wx_wxmall_goods_img modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from wx_wxmall_goods_img where goodsNum='" + goodsNum + "' and "+where);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from wx_wxmall_goods_img where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    Utils.DeleteFile(dr["imgName"].ToString());//删除原图
                    Utils.DeleteFile("thumb_" + dr["imgName"].ToString());  //删除缩略图
                }
            }
        }

        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Model.wx_wxmall_goods_img> models)
        {
            if (models != null)
            {
                foreach (Model.wx_wxmall_goods_img modelt in models)
                {
                    Utils.DeleteFile(modelt.imgName);//删除原图
                    Utils.DeleteFile("thumb_" + modelt.imgName);  //删除缩略图
                }
            }
        }
        /// <summary>
        /// 通过文件路径删除文件
        /// </summary>
        /// <param name="file"></param>
        public void DeleteFile(String file) {
            try
            {
                Utils.DeleteFile(file);//删除原图
            }
            catch (Exception) { }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.wx_wxmall_goods_img> GetList2(int goodsId,string where)
        {
            List<Model.wx_wxmall_goods_img> modelList = new List<Model.wx_wxmall_goods_img>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from wx_wxmall_goods_img ");
            strSql.Append(" FROM wx_wxmall_goods_img ");
            strSql.Append(" where goodsId='" + goodsId + "' and " + where);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.wx_wxmall_goods_img model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.wx_wxmall_goods_img();
                    model = DataRowToModel(dt.Rows[n]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

		#endregion  BasicMethod
		#region  ExtensionMethod

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string strValue, string where)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update wx_wxmall_goods_img set " + strValue);
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

