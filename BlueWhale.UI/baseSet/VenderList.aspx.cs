using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhaleUI.baseSet
{
	public partial class VenderList : BasePage
	{

		public VenderDAL dal = new VenderDAL();

		protected void Page_Load(object sender, EventArgs e)
		{

			if (Request.Params["Action"] == "GetDataList")
			{
				GetDataList();
				Response.End();
			}

			if (Request.Params["Action"] == "GetDataListSearch")
			{
				string keys = Request.Params["keys"].ToString();

				GetDataListSearch(keys);
				Response.End();
			}

			if (Request.Params["Action"] == "GetDDLList")
			{
				GetDDLList();
				Response.End();
			}

			if (Request.Params["Action"] == "delete")
			{
				string idString = Request.Params["idString"].ToString();
				DeleteRow(idString);
				Response.End();
			}
		}

		void GetDataList()
		{
			string isWhere = " shopId='" + LoginUser.ShopId + "' ";
			DataSet ds = dal.GetList(isWhere);

			IList<object> list = new List<object>();
			for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
			{

				list.Add(new
				{
					id = ds.Tables[0].Rows[i]["id"].ToString(),
					shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
					shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
					code = ds.Tables[0].Rows[i]["code"].ToString(),
					names = ds.Tables[0].Rows[i]["names"].ToString(),
					typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
					typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
					dueDate = ds.Tables[0].Rows[i]["dueDate"].ToString(),
					payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
					payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),

					tax = ds.Tables[0].Rows[i]["tax"].ToString(),
					remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

					taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
					bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
					bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
					address = ds.Tables[0].Rows[i]["address"].ToString(),


					makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
					makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
					checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
					checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

					balance = ds.Tables[0].Rows[i]["balance"].ToString(),

					linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),

					phone = ds.Tables[0].Rows[i]["phone"].ToString(),
					tel = ds.Tables[0].Rows[i]["tel"].ToString(),
					qq = ds.Tables[0].Rows[i]["qq"].ToString(),
					flag = ds.Tables[0].Rows[i]["flag"].ToString()



				});

			}
			var griddata = new { Rows = list };

			string s = new JavaScriptSerializer().Serialize(griddata);

			Response.Write(s);
		}

		void GetDataListSearch(string key)
		{
			string isWhere = " shopId = '" + LoginUser.ShopId + "' ";
			isWhere += " AND (names LIKE'%" + key + "%' OR  code LIKE'%" + key + "%' OR  linkMan LIKE'%" + key + "%') ";
			DataSet ds = dal.GetList(isWhere);

			IList<object> list = new List<object>();
			for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				list.Add(new
				{
					id = ds.Tables[0].Rows[i]["id"].ToString(),
					shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
					shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
					code = ds.Tables[0].Rows[i]["code"].ToString(),
					names = ds.Tables[0].Rows[i]["names"].ToString(),
					typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
					typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
					dueDate = ds.Tables[0].Rows[i]["dueDate"].ToString(),
					payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
					payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),

					tax = ds.Tables[0].Rows[i]["tax"].ToString(),
					remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

					taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
					bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
					bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
					address = ds.Tables[0].Rows[i]["address"].ToString(),

					makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
					makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
					checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
					checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

					balance = ds.Tables[0].Rows[i]["balance"].ToString(),

					linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),

					phone = ds.Tables[0].Rows[i]["phone"].ToString(),
					tel = ds.Tables[0].Rows[i]["tel"].ToString(),
					qq = ds.Tables[0].Rows[i]["qq"].ToString(),
					flag = ds.Tables[0].Rows[i]["flag"].ToString()
					
				});
			}
			var griddata = new { Rows = list };
			string s = new JavaScriptSerializer().Serialize(griddata);
			Response.Write(s);
		}

		void GetDDLList()
		{
			string isWhere = " shopId='" + LoginUser.ShopId + "' ";
			DataSet ds = dal.GetList(isWhere);

			IList<object> list = new List<object>();
			for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
			{

				list.Add(new
				{
					id = ds.Tables[0].Rows[i]["id"].ToString(),

					shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
					shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
					code = ds.Tables[0].Rows[i]["code"].ToString(),
					names = ds.Tables[0].Rows[i]["names"].ToString(),
					typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
					typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
					dueDate = ds.Tables[0].Rows[i]["dueDate"].ToString(),
					payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
					payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),

					tax = ds.Tables[0].Rows[i]["tax"].ToString(),
					remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

					taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
					bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
					bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
					address = ds.Tables[0].Rows[i]["address"].ToString(),

					makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
					makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
					checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
					checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

					balance = ds.Tables[0].Rows[i]["balance"].ToString(),

					linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),

					phone = ds.Tables[0].Rows[i]["phone"].ToString(),
					tel = ds.Tables[0].Rows[i]["tel"].ToString(),
					qq = ds.Tables[0].Rows[i]["qq"].ToString(),
					flag = ds.Tables[0].Rows[i]["flag"].ToString()


				});

			}

			string s = new JavaScriptSerializer().Serialize(list);//Pass to dropdownList


			Response.Write(s);
		}
		void DeleteRow(string id)
		{
			if (Session["userInfo"] != null)
			{

				LogsDAL logs = new LogsDAL();

				string[] idString = id.Split(',');

				int num = 0;

				if (idString.Length > 0)
				{
					for (int i = 0; i < idString.Length; i++)
					{
						int delId = ConvertTo.ConvertInt(idString[i].ToString());

						int del = dal.Delete(delId);
						if (del > 0)
						{
							num += 1;

							logs.ShopId = LoginUser.ShopId;
							logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
							logs.Events = "Delete supplier - ID：" + id.ToString();
							logs.Ip = Request.UserHostAddress.ToString();
							logs.Add();

						}
					}
				}


				if (num > 0)
				{
					Response.Write("Successfully delete " + num + " records!");

				}
				else
				{
					Response.Write("Fail to delete!");
				}

			}
			else
			{
				Response.Write("Connection timeout! Please login again!");
			}
		}
	}
}
