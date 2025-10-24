using BlueWhale.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueWhale.DAL
{
    public class DisassembleDAL
    {
        public DisassembleDAL()
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

        private DateTime bizDate;
        public DateTime BizDate
        {
            get { return bizDate; }
            set { bizDate = value; }
        }

        private decimal fee;
        public decimal Fee
        {
            get { return fee; }
            set { fee = value; }
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
        /// Automatically generate bill number
        /// </summary>
        /// <returns></returns>
        public string GetBillNumberAuto(int shopId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = {
                                      new SqlParameter("@shopId",shopId),
                                      new SqlParameter("@NumberHeader","SPCX"),//First four letters of the bill no
                                      new SqlParameter("@tableName","GoodsOpen")//Table
                                     };

            ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "makeBillNumber", param);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Add a new record
        /// <summary>
        /// Add a new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";
            string sql = "insert into GoodsOpen(shopId,number,bizDate,fee,remarks,makeId,makeDate,bizId,flag)";
            sql += " values(@shopId,@number,@bizDate,@fee,@remarks,@makeId,@makeDate,@bizId,@flag)   select @@identity ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@number",Number),
                                       new SqlParameter("@bizDate",bizDate),
                                       new SqlParameter("@Fee",Fee),
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

        #region Modify a record
        /// <summary>
        /// Modify a record
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE GoodsOpen
                                       SET 
                                          shopId=@shopId                                
                                          ,bizDate = @bizDate                                                                
                                          ,fee = @fee                                       
                                          ,remarks = @remarks                                    
                                      
                                          ,BizId = @BizId                                     
                                          ,Flag = @Flag    
                                        
                                     WHERE id=@id";
            SqlParameter[] param = {
                                           new SqlParameter("@ShopId",ShopId),
                                           new SqlParameter("@BizDate",BizDate),
                                           new SqlParameter("@Fee",Fee),
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
            sql += "               select * from GoodsOpen where flag='Review' and id='" + Id + "' )";

            sql += " begin ";

            sql += " delete from GoodsOpenItem  where pId='" + Id + "' delete from GoodsOpen where Id='" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get all model

        /// <summary>
        /// Get all model
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select * from viewGoodsOpen order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region View product disassembly order

        /// <summary>
        /// View product disassembly order
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int shopId, string key, DateTime start, DateTime end)
        {
            string sql = "select * from viewGoodsOpen  where bizDate>=@start and bizDate<=@end and types=-1 ";

            if (shopId != 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (key != "")
            {
                sql += " and( number like '%" + key + "%' or remarks  like '%" + key + "%' or remarksItem  like '%" + key + "%') ";
            }

            sql += " order by number ";

            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region View product disassembly order by id

        /// <summary>
        /// View product disassembly order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(int id)
        {
            string sql = "select * from viewGoodsOpen where id='" + id + "' and types=-1 order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region View product disassembly order by number

        /// <summary>
        /// View product disassembly order by number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAllModel(string number)
        {
            string sql = "select * from viewGoodsOpen where number='" + number + "'  and types=-1 order by number ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Review a record

        /// <summary>
        /// Review a record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from GoodsOpen where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update GoodsOpen set flag='" + flag + "' ";
            if (flag == "Review")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "Save")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

    }
}
