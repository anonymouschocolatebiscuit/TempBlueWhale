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
    public class DisassembleItemDAL
    {
        public DisassembleItemDAL()
        {

        }

        #region Attribute

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

        private int types;
        public int Types
        {
            get { return types; }
            set { types = value; }
        }



        private int goodsId;
        public int GoodsId
        {
            get { return goodsId; }
            set { goodsId = value; }
        }

        private decimal num;
        public decimal Num
        {
            get { return num; }
            set { num = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private decimal sumPrice;
        public decimal SumPrice
        {
            get { return sumPrice; }
            set { sumPrice = value; }
        }


        private int ckId;
        public int CkId
        {
            get { return ckId; }
            set { ckId = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }


        #endregion


        #region Add a new record
        /// <summary>
        /// Add a new record
        /// </summary>
        /// <returns></returns>
        public int Add()
        {

            string sql = "insert into GoodsOpenItem(pId,types,goodsId,num,price,sumPrice,ckId,remarks)";
            sql += " values(@pId,@types,@goodsId,@num,@price,@sumPrice,@ckId,@remarks)  ";
            SqlParameter[] param = {

                                       new SqlParameter("@PId",PId),
                                       new SqlParameter("@Types",Types),
                                       new SqlParameter("@GoodsId",GoodsId),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@SumPrice",SumPrice),
                                       new SqlParameter("@CkId",CkId),
                                       new SqlParameter("@Remarks",Remarks)


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
        public int Delete(int PId)
        {
            string sql = " ";


            sql += " delete from GoodsOpenItem  where pId='" + PId + "' ";





            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region Get all models by id

        /// <summary>
        /// Get all models by id
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId)
        {
            string sql = "select * from viewGoodsOpenItem where pId='" + pId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region Get all models by id and types

        /// <summary>
        /// Get all models by id and types
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(int pId, int types)
        {
            string sql = "select * from viewGoodsOpenItem where pId='" + pId + "' ";

            if (types != 0)
            {
                sql += " and types='" + types + "'";
            }

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


    }
}
