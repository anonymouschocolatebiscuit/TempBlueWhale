using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Common;//Please add references


namespace Lanwei.Weixin.DAL
{
	/// <summary>
	/// 数据访问类:company
	/// </summary>
	public partial class CompanyDAL
	{
        public CompanyDAL()
		{
        

        
        }

        #region 实体类----表字段

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

    

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

     


        private DateTime dateStart;
        public DateTime DateStart
        {
            get { return dateStart; }
            set { dateStart = value; }
        }

        private DateTime dateEnd;
        public DateTime DateEnd
        {
            get { return dateEnd; }
            set { dateEnd = value; }
        }

   

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

    
        private int proId;
        public int ProId
        {
            get { return proId; }
            set { proId = value; }
        }

        private int ctId;
        public int CtId
        {
            get { return ctId; }
            set { ctId = value; }
        }


        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private DateTime checkDate;
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }


        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

      

        #endregion


        #region  BasicMethod

        /// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("id", "company"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from company");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 是否存在公司名称
        /// </summary>
        public bool Exists(string names)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from company");
            strSql.Append(" where names=@names");
            SqlParameter[] parameters = {
					new SqlParameter("@names",names)
			};
          

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在公司名称
        /// </summary>
        public bool ExistsEdit(int id,string names)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from company");
            strSql.Append(" where names=@names and id<>@id ");
            SqlParameter[] parameters = {
                                            new SqlParameter("@id",id),
					                        new SqlParameter("@names",names)
			};


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 是否存在手机号码
        /// </summary>
        public bool isExists(string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from company");
            strSql.Append(" where phone=@phone");
            SqlParameter[] parameters = {
					new SqlParameter("@phone",phone)
			};


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在手机号码
        /// </summary>
        public bool isExistsEdit(int id,string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from company");
            strSql.Append(" where phone=@phone and id<>@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@phone",phone),
                    new SqlParameter("@id",id)
			};


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePhone(int id, string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update company set ");


            strSql.Append("phone=@phone ");

            strSql.Append(" where id=@id");


            SqlParameter[] parameters = {
				
                    new SqlParameter("@phone",phone),
                  			
					new SqlParameter("@id", id)
                                        
                                        };


            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }





		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into Company(");
            strSql.Append("names,typeName,dateStart,dateEnd,phone,proId,ctId,makeDate,flag)");
			strSql.Append(" values (");
            strSql.Append("@names,@typeName,@dateStart,@dateEnd,@phone,@proId,@ctId,@makeDate,@flag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
				
					new SqlParameter("@names", Names),					
					new SqlParameter("@typeName",typeName),					
					new SqlParameter("@dateStart",dateStart),
					new SqlParameter("@dateEnd", dateEnd),
					new SqlParameter("@phone", phone),
					new SqlParameter("@proId", proId),
					new SqlParameter("@ctId", ctId),
					new SqlParameter("@makeDate", makeDate),
					new SqlParameter("@flag",Flag)

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
		public int Update()
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update company set ");
            strSql.Append("ctId=@ctId,");
            strSql.Append("proId=@proId,");
            strSql.Append("phone=@phone,");
            strSql.Append("dateEnd=@dateEnd,");
            strSql.Append("dateStart=@dateStart,");           
            strSql.Append("typeName=@typeName,");            
            strSql.Append("names=@names ");
		
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
				
                    new SqlParameter("@names", Names),					
					new SqlParameter("@typeName",typeName),					
					new SqlParameter("@dateStart",dateStart),
					new SqlParameter("@dateEnd", dateEnd),
					new SqlParameter("@phone", phone),
					new SqlParameter("@proId", proId),
					new SqlParameter("@ctId", ctId),				
				
					new SqlParameter("@id", Id)
                                        
                                        };


            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int id)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from company ");
            strSql.Append(" where id ='" + id + "' ");
            strSql.Append(" and flag ='待审核' ");

            return DbHelperSQL.ExecuteSql(strSql.ToString());
          


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
            string sql = " if not exists(select * from company where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update company set flag='" + flag + "' ";
            if (flag == "已审核")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "保存")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            int rows = DbHelperSQL.ExecuteSql(sql.ToString());
            if (rows > 0)
            {
                return rows;
            }
            else
            {
                return 0;


            }

        }

        #endregion


		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from company ");
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM viewCompany ");
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
            //id,wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2
			strSql.Append(" * ");
            strSql.Append(" FROM company ");
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
            strSql.Append("select count(1) FROM company ");
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
			strSql.Append(")AS Row, T.*  from company T ");
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
			parameters[0].Value = "wx_diancai_shopinfo";
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
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            //id,wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2 
            strSql.Append("select * ");
            strSql.Append(" FROM viewCompany ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }



        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM viewCompany ");
          
            return DbHelperSQL.Query(strSql.ToString());
        }

        
		#endregion  ExtensionMethod
	}
}

