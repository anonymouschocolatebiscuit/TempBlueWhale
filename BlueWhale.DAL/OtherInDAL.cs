using System;
using System.Data.SqlClient;
using System.Data;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class OtherInDAL
    {
        public OtherInDAL()
        {
        }

        #region Attribute

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

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int wlId;
        public int WlId
        {
            get { return wlId; }
            set { wlId = value; }
        }

        private DateTime bizDate;
        public DateTime BizDate
        {
            get { return bizDate; }
            set { bizDate = value; }
        }

        private int types;
        public int Types
        {
            get { return types; }
            set { types = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private int makeId;
        public int MakeId
        {
            get { return makeId; }
            set { makeId = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private int bizId;
        public int BizId
        {
            get { return bizId; }
            set { bizId = value; }
        }

        private int checkId;
        public int CheckId
        {
            get { return checkId; }
            set { checkId = value; }
        }

        private DateTime checkDate;
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

		#endregion

		#region Automatically generate bill number


		/// <summary>
		/// Generate bill number
		/// </summary>
		/// <returns></returns>
		public string GetBillNumberAuto(int shopId)
		{
			DataSet ds = new DataSet();
			SqlParameter[] param = {

									   new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@NumberHeader","QTRK"),
                                       new SqlParameter("@tableName","OtherIn")
                                      
                                       

                                     };

			ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
			return ds.Tables[0].Rows[0][0].ToString();

		}

		#endregion

		#region Add new record
		/// <summary>
		/// Add new record
		/// </summary>
		/// <returns></returns>
		public int Add()
        {
            string id = "0";

            string sql = "insert into OtherIn(shopId,number,wlId,bizDate,types,remarks,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@wlId,@bizDate,@types,@remarks,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
            SqlParameter[] param = {

                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@wlId",WlId),
                                       new SqlParameter("@bizDate",bizDate),

                                       new SqlParameter("@Types",Types),
                                       new SqlParameter("@remarks",remarks),
                                       new SqlParameter("@makeId",makeId),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@BizId",BizId),
                                       new SqlParameter("@Flag",Flag)

                                     };

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, param);
            if (sdr.Read())
            {
                id = sdr[0].ToString();

            }

            return int.Parse(id);
        }
		#endregion

		#region Edit record
		/// <summary>
		/// Edit record
		/// </summary>
		/// <returns></returns>
		public int Update()
		{

			string sql = @"UPDATE OtherIn
                                       SET 
                                           shopId = @shopId
                                          ,wlId = @wlId                                       
                                          ,bizDate = @bizDate                                                                
                                          ,types = @types                                       
                                          ,remarks = @remarks                                  
                                          ,BizId = @BizId                                     
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id";
			SqlParameter[] param = {

									   new SqlParameter("@ShopId",ShopId),
									   new SqlParameter("@WlId",WlId),
										   new SqlParameter("@BizDate",BizDate),
										   new SqlParameter("@Types",Types),
										   new SqlParameter("@Remarks",Remarks),

										   new SqlParameter("@BizId",BizId),
										   new SqlParameter("@Flag",Flag),
										   new SqlParameter("@Id",Id)

									   };

			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

		}

		#endregion

		#region Delete member information
		/// <summary>
		/// Delete member information
		/// </summary>
		/// <param name="fId"></param>
		/// <returns></returns>
		public int Delete(int Id)
		{
			string sql = " if not exists( ";
			sql += "               select * from OtherIn where flag='Review' and id='" + Id + "' )";

			sql += " begin ";

			sql += " delete from OtherInItem  where pId='" + Id + "' delete from OtherIn where Id='" + Id + "'";

			sql += " end ";



			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


		}

		#endregion

		#region Retrieve all member 

		/// <summary>
		/// Retrieve all member 
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllModel()
		{
			string sql = "select * from viewOtherIn order by number ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Search other warehouse order

		/// <summary>
		/// Search other warehouse order
		/// </summary>
		/// <param name="key"></param>
		/// <param name="dateStart"></param>
		/// <param name="dateEnd"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end, int types)
		{
			string sql = "select * from viewOtherIn  where bizDate>=@start and bizDate<=@end  ";

			if (shopId != 0)
			{
				sql += " and shopId='" + shopId + "' ";
			}

			if (key != "")
			{
				sql += " and( wlName like '%" + key + "%' or number like '%" + key + "%' or remarks  like '%" + key + "%') ";
			}
			if (types != 0)
			{
				sql += " and types = '" + types + "' ";
			}
			sql += " order by number ";

			SqlParameter[] param = {
									   new SqlParameter("@start",start),
									   new SqlParameter("@end",end)
								   };

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
		}


		#endregion

		#region Search other warehouse order by bill number

		/// <summary>
		/// Search other warehouse order by bill number
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public DataSet GetAllModel(int id)
		{
			string sql = "select * from viewOtherIn where id='" + id + "' order by number ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Search other warehouse order by bill number

		/// <summary>
		/// Search other warehouse order by bill number
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public DataSet GetAllModel(string number)
		{
			string sql = "select * from viewOtherIn where number='" + number + "' order by number ";

			return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
		}


		#endregion

		#region Review order

		/// <summary>
		/// Review order
		/// </summary>
		/// <param name="Id"></param>
		/// <param name="chekerId"></param>
		/// <param name="checker"></param>
		/// <param name="checkDate"></param>
		/// <param name="flag"></param>
		/// <returns></returns>
		public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
		{
			string sql = @"
				IF NOT EXISTS(SELECT 1 FROM OtherIn WHERE flag = @flag AND id = @id)
				BEGIN
					UPDATE OtherIn 
					SET flag = @flag
						/* only set checkId and checkDate if Review */
						" + (flag == "Review" ? " ,checkId = @chekerId, checkDate = @checkDate " : "") + @"
						/* reset checkId and checkDate if Save */
						" + (flag == "Save" ? " ,checkId = NULL, checkDate = NULL " : "") + @"
					WHERE id = @id
				END";

			var parameters = new[]
			{
				new SqlParameter("@flag", SqlDbType.VarChar) { Value = flag },
				new SqlParameter("@id", SqlDbType.Int) { Value = Id },
				new SqlParameter("@chekerId", SqlDbType.Int) { Value = chekerId },
				new SqlParameter("@checkDate", SqlDbType.DateTime) { Value = checkDate }
			};

			return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, parameters);

		}

		#endregion
	}
}
