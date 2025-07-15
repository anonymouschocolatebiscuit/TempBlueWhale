using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BlueWhale.DBUtility;
using BlueWhale.Model;

namespace BlueWhale.DAL
{
    public class GoodsTypeDAL
    {
        public GoodsTypeDAL()
        {

        }
        #region 成员字段



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


        #region  成员方法


        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
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







        /// <summary>
        /// 名称是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 判断是否有被引用，能否删除？
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckBeUsed(int id)
        {
            bool flag = false;

            string sql = "select * from goods where typeId='" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }





        #region 通过名称获取编号

        /// <summary>
        /// 通过名称获取ID
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




        #endregion

        #region 获取所有信息

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList()
        {
            string sql = "select * from viewGoodsType order by seq  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有信息------微信

        /// <summary>
        /// 获取所有信息------微信
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelListWeixin()
        {
            string sql = "select * from viewGoodsTypeWeixin order by seq  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有信息
        /// <summary>
        /// 获取所有信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllGoodsType()
        {


            string sql = @"select a.*,isnull(num,0) num
                            from viewGoodsType a
                            left join
                            (
                            select typeId,count(*) num
                            from goods
                            group by typeId
                            ) b
                            on a.id=b.typeId  order by seq ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        /// <summary>
        /// 获得数据列表
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




        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into goodsType(shopId,names,parentId,seq) values('" + ShopId + "','" + Names + "','" + ParentId + "','" + Seq + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 修改一条信息
        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
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
        #endregion

        #region 删除一条信息
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " delete from GoodsPriceClientType where typeId='" + id + "' delete from goodsType where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 获取所有类别信息 填充到TreeView
        /// <summary>
        /// 获取所有类别信息--填充到TreeView
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<GoodsTypeInfo> GetAlLGoosTypesTreeView(int parentId, int shopId)
        {
            string sql = "select * from goodsType where parentId='" + parentId + "' and shopId='" + shopId + "'  order by seq ";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, sql, null))
            {
                List<GoodsTypeInfo> list = new List<GoodsTypeInfo>();
                while (read.Read())
                {
                    GoodsTypeInfo s = new GoodsTypeInfo();

                    s.Id = read.GetInt32(0);
                    s.TypeName = read.GetString(1);
                    s.ParentId = read.GetInt32(2);
                    s.Seq = read.GetInt32(3);
                    list.Add(s);
                }
                return list;
            }

        }
        #endregion


        #region 查询类别信息---通过编号
        /// <summary>
        /// 查询类别信息---通过编号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetModelById(int Id)
        {
            string sql = "select * from goodsType where Id='" + Id + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion



        #region 修改一条信息
        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
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
