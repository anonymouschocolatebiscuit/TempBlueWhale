using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.Model;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
	public class StorageDAL
	{
		public StorageDAL()
		{

		}
		#region Member Fields



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

		private DateTime makeDate;
		public DateTime MakeDate
		{
			get { return makeDate; }
			set { makeDate = value; }
		}

		private int flag;
		public int Flag
		{
			get { return flag; }
			set { flag = value; }
		}

		#endregion

		#region Member Methods

		/// <summary>
		/// Check if the code exists when adding a new entry.
		/// </summary>
		/// <param name="UserName"></param>
		public bool isExistsCodeAdd(int shopId, string code)
		{
			bool flag = false;
			string sql = "select * from storage where code='" + code + "' and shopId='" + shopId + "' ";
			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}

		/// <summary>
		/// Check if the code exists when modifying an entry.
		/// </summary>
		/// <param name="UserName"></param>
		public bool isExistsCodeEdit(int id, int shopId, string code)
		{
			bool flag = false;
			string sql = "select * from storage where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";
			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}

		/// <summary>
		/// Check if the name exists.
		/// </summary>
		/// <param name="UserName"></param>
		public bool isExistsNamesAdd(int shopId, string names)
		{
			bool flag = false;
			string sql = "select * from storage where names='" + names + "' and shopId='" + shopId + "'  ";
			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}

		/// <summary>
		/// Check if the name exists when modifying an entry.
		/// </summary>
		/// <param name="UserName"></param>
		public bool isExistsNamesEdit(int id, int shopId, string names)
		{
			bool flag = false;
			string sql = "select * from storage where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";
			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}

		#endregion

		#region Get All Information

		/// <summary>
		/// Retrieve all information.
		/// </summary>
		public DataSet GetALLModelList()
		{
			string sql = "select * from storage order by code";
			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}

		/// <summary>
		/// Get a data list.
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *,code+' '+names CodeName ");
			strSql.Append(" FROM storage ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}

			strSql.Append(" order by code  ");

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
		}


		#endregion


		#region Retrieve All Information

		/// <summary>
		/// Retrieve all information
		/// </summary>
		/// <returns></returns>
		public DataSet GetALLModelList(int shopId)
		{
			string sql = "select * from storage where shopId='" + shopId + "' order by code  ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Add a New Entry
		/// <summary>
		/// Add a new entry.
		/// </summary>
		public int Add()
		{
			string sql = "insert into storage(shopId,code,names,flag) values('" + ShopId + "','" + Code + "','" + Names + "',1)";
			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
		}
		#endregion

		#region Edit an Entry
		/// <summary>
		/// Edit an entry.
		/// </summary>
		/// <param name="Id"></param>
		/// <param name="names"></param>
		public int Update()
		{
			string sql = "";
			if (!this.isExistsCodeEdit(Id, ShopId, Code) && !this.isExistsNamesEdit(Id, ShopId, Names))
			{
				sql = "update storage set Names='" + Names + "',code='" + Code + "' where Id='" + Id + "'";
				return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
			}
			else
			{
				return 0;
			}
		}
		#endregion

		#region Edit Status
		/// <summary>
		/// Edit status.
		/// </summary>
		/// <param name="Id"></param>
		/// <param name="flag"></param>
		public int UpdateFlag(int Id)
		{
			string sqls = "if exists(select * from storage where id='" + Id + "' and flag=1) ";
			sqls += " update storage set flag=0 where id='" + Id + "'";
			sqls += " else update storage set flag=1 where id='" + Id + "'";
			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqls, null);
		}
		#endregion

		#region Delete an Entry
		/// <summary>
		/// Delete an entry.
		/// </summary>
		/// <param name="fId"></param>
		public int Delete(int Id)
		{
			string sql = "delete from storage where id='" + Id + "' and flag=0 ";
			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
		}
		#endregion

		#region Get ID by Name
		/// <summary>
		/// Get ID by name.
		/// </summary>
		/// <param name="names"></param>
		public int GetIdByName(int shopId, string names)
		{
			int id = 0;
			string sql = "select * from storage where names='" + names + "' and shopId='" + shopId + "' ";
			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				id = Convert.ToInt32(reader["id"].ToString());
			}
			reader.Close();
			return id;
		}
		#endregion
	}
}