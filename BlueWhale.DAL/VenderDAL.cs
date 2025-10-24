using BlueWhale.DBUtility;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace BlueWhale.DAL
{
	public class VenderDAL
	{
		public VenderDAL()
		{
		}

		#region Attributes

		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private int shopId;
		public int ShopId
		{
			get { return shopId; }
			set { shopId = value; }
		}

		private string code;
		public string Code
		{
			get { return code; }
			set { code = value; }
		}

		private string names;
		public string Names
		{
			get { return names; }
			set { names = value; }
		}

		private int typeId;
		public int TypeId
		{
			get { return typeId; }
			set { typeId = value; }
		}

		private DateTime dueDate;
		public DateTime DueDate
		{
			get { return dueDate; }
			set { dueDate = value; }
		}

		private decimal payNeed;
		public decimal PayNeed
		{
			get { return payNeed; }
			set { payNeed = value; }
		}

		private decimal payReady;
		public decimal PayReady
		{
			get { return payReady; }
			set { payReady = value; }
		}

		private int tax;
		public int Tax
		{
			get { return tax; }
			set { tax = value; }
		}


		private string remarks;
		public string Remarks
		{
			get { return remarks; }
			set { remarks = value; }
		}

		private DateTime makeDate;
		public DateTime MakeDate
		{
			get { return makeDate; }
			set { makeDate = value; }
		}

		private string taxNumber;
		public string TaxNumber
		{
			get { return taxNumber; }
			set { taxNumber = value; }
		}

		private string bankName;
		public string BankName
		{
			get { return bankName; }
			set { bankName = value; }
		}

		private string bankNumber;
		public string BankNumber
		{
			get { return bankNumber; }
			set { bankNumber = value; }
		}

		private string address;
		public string Address
		{
			get { return address; }
			set { address = value; }
		}


		private string flag;
		public string Flag
		{
			get { return flag; }
			set { flag = value; }
		}
		#endregion

		#region Function

		/// <summary>
		/// Check if vender exist -- before Insert
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public bool isExistsCodeAdd(int shopId, string code)
		{
			bool flag = false;

			string sql = "select * from vender where code='" + code + "' and shopId='" + shopId + "' ";

			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			reader.Close();
			return flag;
		}

		/// <summary>
		/// Check if vender code exist -- during edit
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public bool isExistsCodeEdit(int id, int shopID, string code)
		{
			bool flag = false;

			string sql = "select * from vender where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";

			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}

		/// <summary>
		/// 名称是否存在
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public bool isExistsNamesAdd(int shopId, string names)
		{
			bool flag = false;

			string sql = "select * from vender where names='" + names + "' and shopId='" + shopId + "'  ";

			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			reader.Close();
			return flag;
		}

		/// <summary>
		/// 名称是否存在--修改的时候
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public bool isExistsNamesEdit(int shopId, string code, string names)
		{
			bool flag = false;

			string sql = "select * from vender where names='" + names + "' and code<>'" + code + "' and shopId='" + shopId + "'  ";

			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}

		#endregion



		#region Get all vender

		/// <summary>
		/// Get all vender
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllModel()
		{
			string sql = "select *,code+' '+names CodeName from viewVender order by code ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Get all vender -- view

		/// <summary>
		/// Get all vender - view
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllModelView()
		{
			string sql = "select * from viewVender order by code ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Search vender -- view

		/// <summary>
		/// Search vender -- view
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllModelView(string key)
		{
			string sql = "select * from viewVender where 1=1 ";
			if (key != "")
			{
				sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  linkMan like'%" + key + "%' ";
			}
			sql += " order by code ";


			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Get vender by ID

		/// <summary>
		/// Get vender by ID
		/// </summary>
		/// <returns></returns>
		public DataSet GetModelByCode()
		{
			string sql = "select * from Vender where id='" + Id + "' ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}

		#endregion


		#region Add new vender
		/// <summary>
		/// Add new vender
		/// </summary>
		/// <returns></returns>
		public int Add()
		{
			string id = "0";

			string sql = @" insert into vender
                                             (
                                                shopId,
                                                code,
                                                names,
                                                TypeId,
                                                dueDate,
                                                payNeed,
                                                payReady,
                                                tax,
                                                remarks,
                                                makeDate,
                                                taxNumber,
                                                bankName,
                                                bankNumber,
                                                address,
                                                flag
                                                ) ";
			sql += @"  values(
                                @shopId,
                                @code,
                                @names,
                                @TypeId,
                                @dueDate,
                                @payNeed,
                                @payReady,
                                @tax,
                                @remarks,
                                @makeDate,
                                @taxNumber,
                                @bankName,
                                @bankNumber,
                                @address,
                                @flag
                                )      select @@identity ";

			SqlParameter[] param = {
									   new SqlParameter("@shopId",shopId),
									   new SqlParameter("@code",code),
									   new SqlParameter("@names",names),
									   new SqlParameter("@TypeId",typeId),
									   new SqlParameter("@dueDate",dueDate),
									   new SqlParameter("@payNeed",payNeed),
									   new SqlParameter("@payReady",payReady),
									   new SqlParameter("@tax",tax),
									   new SqlParameter("@remarks",remarks),
									   new SqlParameter("@makeDate",makeDate),
									   new SqlParameter("@taxNumber",taxNumber),
									   new SqlParameter("@bankName",bankName),
									   new SqlParameter("@bankNumber",bankNumber),
									   new SqlParameter("@address",address),
									   new SqlParameter("@flag",flag)

								   };


			SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, param);
			if (sdr.Read())
			{
				id = sdr[0].ToString();

			}

			sdr.Close();

			return int.Parse(id);
		}

		#endregion



		#region Delete vender
		/// <summary>
		/// Delete vender
		/// </summary>
		/// <param name="fId"></param>
		/// <returns></returns>
		public int Delete(int Id)
		{
			string sql = "delete from vender where id='" + Id + "' delete from venderLinkMan where pId='" + Id + "'";
			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
		}
		#endregion

		#region Edit vender details
		/// <summary>
		/// Edit vender details
		/// </summary>
		/// <param name="Id"></param>
		/// <param name="names"></param>
		/// <returns></returns>
		public int Update()
		{
			string sql = "";
			if (!this.isExistsCodeEdit(Id, ShopId, Code))
			{
				sql = @" update vender set
                                                shopId = @shopId,
                                                code = @code,
                                                names = @names,
                                                TypeId = @TypeId,
                                                dueDate = @dueDate,
                                                payNeed = @payNeed,
                                                payReady = @payReady,
                                                tax = @tax,
                                                remarks = @remarks,	
                                                makeDate = @makeDate,
                                                taxNumber = @taxNumber,
                                                bankName = @bankName,
                                                bankNumber = @bankNumber,
                                                address = @address
                                          where Id = @Id  ";

				SqlParameter[] param = {
									   new SqlParameter("@shopId",shopId),
									   new SqlParameter("@code",code),
									   new SqlParameter("@names",names),
									   new SqlParameter("@TypeId",TypeId),
									   new SqlParameter("@dueDate",dueDate),
									   new SqlParameter("@payNeed",payNeed),
									   new SqlParameter("@payReady",payReady),
									   new SqlParameter("@tax",tax),
									   new SqlParameter("@remarks",remarks),
									   new SqlParameter("@makeDate",makeDate),
									   new SqlParameter("@taxNumber",taxNumber),
									   new SqlParameter("@bankName",bankName),
									   new SqlParameter("@bankNumber",bankNumber),
									   new SqlParameter("@address",address),
									   new SqlParameter("@Id",Id)
								   };
				return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
			}
			else
			{
				return 0;
			}
		}
		#endregion

		#region Get all vender type info
		/// <summary>
		/// Get all vender type info -- return DataSet
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllVenderType()
		{
			string sql = @"select a.*,isnull(num,0) num
                            from VenderType a
                            left join
                            (
                            select TypeId,count(*) num
                            from Vender
                            group by TypeId
                            ) b
                            on a.id=b.TypeId  order by flag desc ";
			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

		}


		/// <summary>
		/// Get vender list
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *,code+' '+names CodeName   ");
			strSql.Append(" FROM viewVender ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}

			strSql.Append(" order by code  ");

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
		}


		#endregion
	}
}