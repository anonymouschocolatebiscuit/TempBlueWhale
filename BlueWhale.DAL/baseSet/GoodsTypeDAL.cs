using System;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class GoodsTypeDAL
    {
        public GoodsTypeDAL()
        {

        }

        #region Property

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

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }
        #endregion

        #region GetAllModelList

        /// <summary>
        /// GetAllModelList
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList()
        {
            string sql = "select * from viewGoodsType order by seq  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region GetAllGoodType

        /// <summary>
        /// Get Data List
        /// </summary>
        public DataSet GetList(string strWhere)
        {

            string sql = @" select a.*,isnull(num,0) num
                            from viewGoodsTypeList a
                            left join
                            (
                            select typeId,count(*) num
                            from goods
                            group by typeId
                            ) b
                            on a.id=b.typeId ";

            if (strWhere != "")
            {
                sql += " where " + strWhere;
            }

            sql += "  order by seq ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql.ToString(), null);
        }

        #endregion

        #region GetIdByName

        /// <summary>
        /// GetIdByName
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId, string names)
        {
            int id = 0;
            string sql = "select * from goodsType where names='" + names + "' and shopId='" + shopId + "'  ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

            if (ds.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            }

            return id;
        }

        #endregion

        #region Insert

        public int Add()
        {
            string sql = "insert into goodsType(shopId,names,parentId,seq) values('" + ShopId + "','" + Names + "','" + ParentId + "','" + Seq + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion Insert

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " delete from GoodsPriceClientType where typeId='" + id + "' delete from goodsType where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region CheckIsNameExist

        public bool isExistsNamesAdd(int shopId, int parentId, string names)
        {
            bool flag = false;
            string sql = "select * from goodsType where names='" + names + "' and parentId='" + parentId + "' and shopId='" + shopId + "' ";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);

            while (reader.Read())
            {
                flag = true;
            }

            return flag;
        }

        public bool isExistsNamesEdit(int id, int shopId, int parentId, string names)
        {
            bool flag = false;
            string sql = "select * from goodsType where names='" + names + "' and id<>'" + id + "' and parentId='" + parentId + "' and shopId='" + shopId + "' ";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);

            while (reader.Read())
            {
                flag = true;
            }

            return flag;
        }

        #endregion CheckIsNameExist

        #region Edit

        public int Update()
        {
            string sql = "";

            if (!this.isExistsNamesEdit(Id, ShopId, ParentId, Names))
            {
                sql = "update goodsType set ShopId='" + ShopId + "',Names='" + Names + "',ParentId='" + ParentId + "',Seq='" + Seq + "' where Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int UpdatePic(int typeId, int isShowXCX, int isShowGZH, string picUrl)
        {
            string sql = "";

            if (this.CheckPic(typeId))
            {
                sql = "update goodsTypePicList set isShowXCX='" + isShowXCX + "',isShowGZH='" + isShowGZH + "',picUrl='" + picUrl + "' where typeId='" + typeId + "'";
            }
            else
            {
                sql = "insert into goodsTypePicList(typeId,isShowXCX,isShowGZH,picUrl) values('" + typeId + "','" + isShowXCX + "','" + isShowGZH + "','" + picUrl + "')";

            }

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public bool CheckPic(int id)
        {
            bool flag = false;
            string sql = "select * from goodsTypePicList where typeId='" + id + "' ";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);

            while (reader.Read())
            {
                flag = true;
            }

            return flag;
        }

        #endregion
    }
}
