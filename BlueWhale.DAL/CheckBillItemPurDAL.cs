using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class CheckBillItemPurDAL
    {
        public CheckBillItemPurDAL()
        {

        }

        #region Attributes

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int pId;
        public int PId
        {
            get { return pId; }
            set { pId = value; }
        }

        private string sourceNumber;
        public string SourceNumber
        {
            get { return sourceNumber; }
            set { sourceNumber = value; }
        }

        private decimal priceCheckNow;
        public decimal PriceCheckNow
        {
            get { return priceCheckNow; }
            set { priceCheckNow = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        #endregion

        #region Add a Record
        /// <summary>
        /// Add a Record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into CheckBillItemPur(pId,sourceNumber,priceCheckNow,flag)";
            sql += " values(@pId,@sourceNumber,@priceCheckNow,@flag)  ";
            SqlParameter[] param = {
                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@SourceNumber",SourceNumber),
                                       new SqlParameter("@PriceCheckNow",PriceCheckNow),
                                       new SqlParameter("@Flag",Flag)
                                     };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region Delete Member Information
        /// <summary>
        /// Delete Member Information
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int PId)
        {
            string sql = "DELETE FROM CheckBillItemPur WHERE pId = @PId";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@PId", PId)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, parameters);
        }
        #endregion

        #region Get All Members

        /// <summary>
        /// Get All Members
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "SELECT * FROM viewCheckBillItemPur WHERE pId = @pId";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@pId", pId)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, parameters);
        }

        #endregion
    }
}
