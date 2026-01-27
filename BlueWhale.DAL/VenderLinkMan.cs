using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
	public class VenderLinkManDAL
	{
		public VenderLinkManDAL()
		{

		}

		#region Attributes

		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}


		/// <summary>
		/// Vender ID
		/// </summary>
		private int pId;
		public int PId
		{
			get { return pId; }
			set { pId = value; }
		}

		private string names;
		public string Names
		{
			get { return names; }
			set { names = value; }
		}

		private string phone;
		public string Phone
		{
			get { return phone; }
			set { phone = value; }
		}

		private string tel;
		public string Tel
		{
			get { return tel; }
			set { tel = value; }
		}

		private int moren;
		public int Moren
		{
			get { return moren; }
			set { moren = value; }
		}
		#endregion


		/// <summary>
		/// Is User Name Exists
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public bool isExistsNames(int pId, string names)
		{
			bool flag = false;

			string sql = "select * from venderLinkMan where pId='" + pId + "' and names='" + names + "' ";

			SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
			while (reader.Read())
			{
				flag = true;
			}
			return flag;
		}


		#region Get all contact------ by vender PId

		/// <summary>
		/// Get all contact------ by vender PId
		/// </summary>
		/// <returns></returns>
		public DataSet GetMolderByPId()
		{
			string sql = "SELECT * FROM venderLinkMan WHERE pId = '" + PId + "' ORDER BY ID ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Add new contact
		/// <summary>
		/// Add new contact
		/// </summary>
		/// <returns></returns>
		public int Add()
		{
			string sql = "";
			if (Moren == 1)
			{
				sql += " UPDATE venderLinkMan SET moren = 0 WHERE pid = '" + PId + "' ";
			}
			sql += " INSERT INTO venderLinkMan ( pid, names, phone, tel, moren) VALUES('" + PId + "','" + Names + "','" + Phone + "','" + Tel + "','" + Moren + "')";

			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
		}

		#endregion

		#region 修改联系人信息
		/// <summary>
		/// Update contact
		/// </summary>
		/// <returns></returns>
		public int Update()
		{
			string sql = "";
			if (Moren == 1)
			{
				sql += "UDPATE venderLinkMan SET moren = 0  WHERE pid = '" + PId + "' ";
			}

			sql += " UPDATE venderLinkMan SET Names = '" + Names + "', Phone = '" + Phone + "', Tel = '" + Tel + "', Moren = '" + Moren + "' WHERE Id = '" + Id + "'";

			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
		}
		#endregion


		#region Delete Contact
		/// <summary>
		/// Delete Contact
		/// </summary>
		/// <param name="fId"></param>
		/// <returns></returns>
		public int Delete(int Id)
		{
			string sql = "DELETE FROM venderLinkMan WHERE id = '" + Id + "'";

			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
		}

		#endregion
	}
}