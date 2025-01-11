using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
    public class GoodsDAL
    {
        public GoodsDAL()
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

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
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

        private int brandId;
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
        }

        private string spec;
        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }

        private int unitId;
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        private int ckId;
        public int CkId
        {
            get { return ckId; }
            set { ckId = value; }
        }

        private string place;
        public string Place
        {
            get { return place; }
            set { place = value; }
        }

        private decimal priceCost;
        public decimal PriceCost
        {
            get { return priceCost; }
            set { priceCost = value; }
        }

        private decimal priceSalesRetal;
        public decimal PriceSalesRetail
        {
            get { return priceSalesRetal; }
            set { priceSalesRetal = value; }
        }

        private decimal priceSalesWhole;
        public decimal PriceSalesWhole
        {
            get { return priceSalesWhole; }
            set { priceSalesWhole = value; }
        }

        private int numMin;
        public int NumMin
        {
            get { return numMin; }
            set { numMin = value; }
        }

        private int numMax;
        public int NumMax
        {
            get { return numMax; }
            set { numMax = value; }
        }

        private int bzDays;
        public int BzDays
        {
            get { return bzDays; }
            set { bzDays = value; }
        }

        private int isWeight;
        public int IsWeight
        {
            get { return isWeight; }
            set { isWeight = value; }
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

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private string fieldA;
        public string FieldA
        {
            get { return fieldA; }
            set { fieldA = value; }
        }

        private string fieldB;
        public string FieldB
        {
            get { return fieldB; }
            set { fieldB = value; }
        }

        private string fieldC;
        public string FieldC
        {
            get { return fieldC; }
            set { fieldC = value; }
        }


        private string fieldD;
        public string FieldD
        {
            get { return fieldD; }
            set { fieldD = value; }


        }

        private decimal tichengRate;
        public decimal TichengRate
        {
            get { return tichengRate; }
            set { tichengRate = value; }
        }

        private int isShow;
        public int IsShow
        {
            get { return isShow; }
            set { isShow = value; }
        }

        private string showType;
        public string ShowType
        {
            get { return showType; }
            set { showType = value; }
        }


        #endregion


        #region 成员方法

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(string code)
        {
            bool flag = false;

            string sql = "select * from goods where code='" + code + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        public bool isExistsCodeAddWeixin(string code)
        {
            bool flag = false;

            string sql = "select * from goodsWeixin where code='" + code + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }



        /// <summary>
        /// 编号是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id,int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from goods where code='" + code + "' and id<>'" + id + "' and shopId='"+shopId+"' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        public bool isExistsCodeEditWeixin(int id, string code)
        {
            bool flag = false;

            string sql = "select * from goodsWeixin where code='" + code + "' and id<>'" + id + "' ";

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
        public bool isExistsNamesAdd(string names)
        {
            bool flag = false;

            string sql = "select * from goods where names='" + names + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// 编码、条码、名称、规格是否重复
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsAdd(int shopId,string code,string barcode,string names,string spec)
        {
            bool flag = false;

            string sql = "select * from goods where shopId='" + shopId + "' and code='" + code + "' and barcode='" + barcode + "' and names='" + names + "' and spec='" + spec + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            
            return flag;
        }


        /// <summary>
        /// 编码、条码、名称、规格是否重复
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsEdit(int id,int shopId,string code, string barcode, string names, string spec)
        {
            bool flag = false;

            string sql = "select * from goods where shopId='" + shopId + "' and code='" + code + "' and barcode='" + barcode + "' and names='" + names + "' and spec='" + spec + "'  and id<> '" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();

            return flag;
        }



        public bool isExistsNamesAddWeixin(int shopId,string names)
        {
            bool flag = false;

            string sql = "select * from goodsWeixin where names='" + names + "' ";

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
        public bool isExistsNamesEdit(int id, int shopId, string names)
        {
            bool flag = false;

            string sql = "select * from goods where names='" + names + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();

            return flag;
        }


        public bool isExistsNamesEditWeixin(int id, string names)
        {
            bool flag = false;

            string sql = "select * from goodsWeixin where names='" + names + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();

            return flag;
        }

        #endregion


        #region 查商品信息

        /// <summary>
        /// 查商品信息
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="CodeName"></param>
        /// <returns></returns>
        public DataSet GetGoods(int shopId,int typeId,string CodeName)
        {
            string sql = "select * from viewGoods where shopId='"+shopId+"' ";

            if (typeId != 0)
            {
                sql += " and typeId='"+typeId+"'";
            }
            if (CodeName != "")
            {
                sql += " and (code like '%" + CodeName + "%' or names like '%"+CodeName+"%') ";
 
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 通过名称获取ID

        /// <summary>
        /// 通过名称获取ID
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId,string names)
        {
            int id = 0;
            string sql = "select * from goods where names='" + names + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"].ToString());
            }
            return id;
        }

        #endregion


        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select *,code+' '+names CodeName from viewGoods order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,code+' '+names CodeName  ");
            strSql.Append(" FROM viewGoods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by code asc ");

            LogUtil.WriteLog(" sql:" + strSql);

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }


        #endregion

        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView()
        {
            string sql = "select * from viewGoods order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(string key)
        {
            string sql = "select * from viewGoods where 1=1 ";
            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取所有成员-------视图的-------微信的

        /// <summary>
        /// 获取所有成员-------视图的----微信的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelViewWeixin(string key)
        {
            string sql = "select * from viewGoodsWeixin where 1=1 ";
            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int id)
        {
            string sql = "select *,priceSalesRetail typeNamePrice from viewGoods where 1=1 ";
            if (id != 0)
            {
                sql += " and id ='" + id + "' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取推荐商品-------通过编号

        /// <summary>
        /// 获取推荐商品-------通过编号
        /// </summary>
        /// <returns></returns>
        public DataSet GetOtherGoodsByCode(int shopId,string code)
        {
            string sql = "select top 5 * from viewGoods where code<>'" + code + "' and shopId='"+shopId+"' and isShow=1  ";

            sql += " and typeId=(select top 1 typeId from goods where code='" + code + "' and shopId='" + shopId + "' ) ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取成员-------通过编号

        /// <summary>
        /// 获取成员-------通过编号
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByCode()
        {
            string sql = "select * from viewGoods where code='" + Code + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

       

        #region 获取商品-------通过ID

        /// <summary>
        /// 获取商品-------通过ID
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelById(int id)
        {
            string sql = "select * from goods where id='" + id + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取商品图片-------通过ID

        /// <summary>
        /// 获取商品图片-------通过ID
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoodsImagesById(int id)
        {
            string sql = "select * from goodsImages where goodsId='" + id + "' order by moren desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 删除图片信息
        /// <summary>
        /// 删除图片信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteImages(int id)
        {
            string sql = "delete from goodsImages where id='" + id + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 默认显示图片信息
        /// <summary>
        /// 默认显示图片信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int SetMorenImages(int id,int goodsId)
        {

            string sql = " update goodsImages set moren=0 where goodsId='" + goodsId + "' update  goodsImages set moren=1 where id='" + id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion




        #region 新增产品图片
        /// <summary>
        /// 新增产品图片
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddGoodsImages(int goodsId, string imagePath,int moren)
        {
            string sql = "";
            if (moren == 1)
            {
                sql += " update goodsImages set moren=0 where goodsId='" + goodsId + "' ";
            }

            sql += " insert into  goodsImages(goodsId,imagesPath,moren) values('"+goodsId+"','"+imagePath+"','"+moren+"') ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }

        #endregion


        #region 插入客户分类价格

        public int AddClientTypePrice(int goodsId, int typeId, decimal price)
        {
            string sql = " if not exists(select * from goodsPriceClientType where goodsId=@goodsId and typeId=@typeId ) ";
         
            sql += "begin ";
            sql += " insert into goodsPriceClientType(goodsId,typeId,price) values(@goodsId,@typeId,@price)  ";
            sql += " end ";

            sql += " else ";

            sql += "begin ";

            sql += " delete from  goodsPriceClientType where goodsId=@goodsId and typeId=@typeId  ";

            sql += " insert into goodsPriceClientType(goodsId,typeId,price) values(@goodsId,@typeId,@price)  ";

            sql += " end ";


            SqlParameter[] param = {

                                       new SqlParameter("@goodsId",goodsId),
                                       new SqlParameter("@typeId",typeId),
                                       new SqlParameter("@price",price)


                                   };



            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion


        #region 获取所有客户分类价格
        /// <summary>
        /// 获取所有客户分类价格--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllClientTypePrice(int goodsId)
        {
            string sql = @"select a.*,isnull(num,0) num,isnull(t.price,0) price 
                            from ClientType a
                            left join
                            (
                            select TypeId,count(*) num
                            from client
                            group by TypeId
                            ) b
                            on a.id=b.TypeId 
                            
                            left join goodsPriceClientType t on a.id=t.typeId and t.goodsId=@goodsId

                             order by flag desc ";

            SqlParameter[] param = {

                                       new SqlParameter("@goodsId",goodsId)


                                   };



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion

        #region 删除等级价格
        /// <summary>
        /// 删除等级价格
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public int DeleteGoodsPriceClientType(int goodsId)
        {
            string sql = "delete from GoodsPriceClientType where goodsId='" + goodsId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion



        #region 获取商品-------通过ID-------微信

        /// <summary>
        /// 获取商品-------通过ID-------微信
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByIdWeixin(int id)
        {
            string sql = "select * from goodsWeixin where id='" + id + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 新增商品初始库存
        /// <summary>
        /// 新增商品初始库存
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ckId"></param>
        /// <param name="num"></param>
        /// <param name="priceCost"></param>
        /// <param name="sumPrice"></param>
        /// <returns></returns>
        public int AddGoodsNumStart(int goodsId, int ckId, decimal num, decimal priceCost, decimal sumPrice)
        {
            string sql = " if not exists(select * from goodsNumStart where goodsId='" + goodsId + "' and ckId='" + ckId + "') ";
            sql += "insert into goodsNumStart(goodsId,ckId,num,priceCost,sumPrice) values('" + goodsId + "','" + ckId + "','" + num + "','" + priceCost + "','" + sumPrice + "')";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 获取商品初始库存-------通过编号

        /// <summary>
        /// 获取商品初始库存-------通过编号
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoodsNumStartById(int goodsId)
        {
            string sql = "select a.*,ck.names ckName from GoodsNumStart a left join cangku ck on a.ckId=ck.id  where goodsId='" + goodsId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 删除商品初始库存
        /// <summary>
        /// 删除成商品初始库存
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteGoodsNumStart(int Id)
        {
            string sql = "delete from goodsNumStart where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 新增成员信息
        /// <summary>
        /// 新增成员信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";
            string sql = @"insert into goods
                                           (shopId
                                           ,code
                                           ,barcode
                                           ,names
                                           ,typeId
                                           ,brandId
                                           ,spec
                                           ,unitId
                                           ,ckId
                                           ,place
                                           ,priceCost
                                           ,priceSalesWhole
                                           ,priceSalesRetail
                                           ,numMin
                                           ,numMax
                                           ,bzDays
                                           ,isWeight
                                           ,remarks
                                           ,makeDate
                                           ,imagePath
                                           ,flag
                                           ,tichengRate
                                           ,isShow
                                           ,showType
                                           ,fieldA
                                           ,fieldB
                                           ,fieldC
                                           ,fieldD


                                             )

                                          values
                                           (@shopId
                                           ,@code
                                           ,@barcode
                                           ,@names
                                           ,@typeId
                                           ,@brandId
                                           ,@spec
                                           ,@unitId
                                           ,@ckId
                                           ,@place
                                           ,@priceCost
                                           ,@priceSalesWhole
                                           ,@priceSalesRetail
                                           ,@numMin
                                           ,@numMax
                                           ,@bzDays
                                           ,@isWeight
                                           ,@remarks
                                           ,@makeDate
                                           ,@imagePath
                                           ,@flag
                                           ,@tichengRate
                                           ,@isShow
                                           ,@showType
                                           ,@fieldA
                                           ,@fieldB
                                           ,@fieldC
                                           ,@fieldD

                                            )   select @@identity  ";

            SqlParameter[] param = {
                                       new SqlParameter("@shopId",ShopId),
                                       new SqlParameter("@Code",Code),
                                       new SqlParameter("@Barcode",Barcode),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@TypeId",TypeId),
                                       new SqlParameter("@BrandId",BrandId),
                                       new SqlParameter("@Spec",Spec),
                                       new SqlParameter("@UnitId",UnitId),
                                       new SqlParameter("@CkId",CkId),
                                       new SqlParameter("@Place",Place),
                                       new SqlParameter("@PriceCost",PriceCost),
                                       new SqlParameter("@PriceSalesWhole",PriceSalesWhole),
                                       new SqlParameter("@PriceSalesRetail",PriceSalesRetail),
                                       new SqlParameter("@NumMin",NumMin),
                                       new SqlParameter("@NumMax",NumMax),
                                       new SqlParameter("@BzDays",BzDays),
                                       new SqlParameter("@IsWeight",IsWeight),
                                       new SqlParameter("@Remarks",Remarks),
                                       new SqlParameter("@MakeDate",MakeDate),
                                       new SqlParameter("@ImagePath",ImagePath),
                                       new SqlParameter("@Flag",Flag),
                                       new SqlParameter("@TichengRate",TichengRate),
                                       new SqlParameter("@IsShow",IsShow),
                                       new SqlParameter("@ShowType",ShowType),
                                       new SqlParameter("@FieldA",FieldA),
                                       new SqlParameter("@FieldB",FieldB),
                                       new SqlParameter("@FieldC",FieldC),
                                       new SqlParameter("@FieldD",FieldD)
                                      

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


        




        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql="";

            sql = @"UPDATE goods
                                   SET 
                                       code = @code
                                      ,barcode = @barcode
                                      ,names = @names
                                      ,typeId = @typeId
                                      ,brandId = @brandId
                                      ,spec = @spec
                                      ,unitId = @unitId
                                      ,ckId = @ckId
                                      ,place = @place
                                      ,priceCost = @priceCost
                                      ,PriceSalesWhole = @PriceSalesWhole
                                      ,PriceSalesRetail = @PriceSalesRetail
                                      ,numMin = @numMin
                                      ,numMax = @numMax
                                      ,bzDays = @bzDays
                                      ,isWeight = @isWeight
                                    
                                      ,imagePath = @imagePath
                                      ,flag = @flag

                                      ,tichengRate = @tichengRate
                                      ,fieldA = @fieldA
                                      ,fieldB = @fieldB
                                      ,fieldC = @fieldC
                                      ,fieldD = @fieldD
                                      ,isShow = @isShow
                                      ,showType = @showType


                                 WHERE id=@id ";



            SqlParameter[] param = {
                                  
                                       
                                           new SqlParameter("@Id",Id),
                                           new SqlParameter("@Code",Code),
                                           new SqlParameter("@Barcode",Barcode),
                                           new SqlParameter("@Names",Names),
                                           new SqlParameter("@TypeId",TypeId),
                                           new SqlParameter("@BrandId",BrandId),
                                           new SqlParameter("@Spec",Spec),
                                           new SqlParameter("@UnitId",UnitId),
                                           new SqlParameter("@CkId",CkId),
                                           new SqlParameter("@Place",Place),
                                           new SqlParameter("@PriceCost",PriceCost),
                                           new SqlParameter("@PriceSalesWhole",PriceSalesWhole),
                                           new SqlParameter("@PriceSalesRetail",PriceSalesRetail),
                                           new SqlParameter("@NumMin",NumMin),
                                           new SqlParameter("@NumMax",NumMax),
                                           new SqlParameter("@BzDays",BzDays),
                                           new SqlParameter("@IsWeight",IsWeight),
                                                                   
                                           new SqlParameter("@ImagePath",ImagePath),
                                           new SqlParameter("@Flag",Flag),
                                                                                  
                                           new SqlParameter("@TichengRate",TichengRate),
                                           new SqlParameter("@FieldA",FieldA),
                                           new SqlParameter("@FieldB",FieldB),
                                           new SqlParameter("@FieldC",FieldC),
                                           new SqlParameter("@FieldD",FieldD),
                                           new SqlParameter("@IsShow",IsShow),
                                           new SqlParameter("@ShowType",ShowType)

                                   };


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

           
                

        }
        #endregion

        #region 修改成员信息------微信
        /// <summary>
        /// 修改成员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int UpdateWeixin()
        {
            string sql = "";
            if (!this.isExistsCodeEdit(Id,ShopId, Code) && !this.isExistsNamesEdit(Id,ShopId, Names))
            {

                sql = "update goodsWeixin set Names='" + Names + "',Code='" + Code + "',typeId='" + TypeId + "',Spec='" + Spec + "',ckId='" + CkId + "',UnitId='" + UnitId + "',PriceCost='" + PriceCost + "',PriceSalesRetail='" + PriceSalesRetail + "',Remarks='" + Remarks + "',ImagePath='" + ImagePath + "' where Id='" + Id + "'";

                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }


        }
        #endregion


        #region 修改成员信息------商品描述
        /// <summary>
        /// 修改成员信息------商品描述
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int UpdateRemarks(int id,string remarks)
        {
            string sql = "";
            sql = "update goods set  Remarks='" + remarks + "'  where Id='" + id + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }


        #endregion

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {


            string sql = " delete from goods where id='" + Id + "' delete from goodsNumStart where goodsId='"+Id+"' ";



            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteWeixin(int Id)
        {
            string sql = "delete from goodsWeixin where id='" + Id + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 查询商品库存余额表

        /// <summary>
        /// 查询商品库存余额表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNum(DateTime start, DateTime end, int goodsId, int ckId)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,--入库数量
                            sum(numOut) sumNumOut,--出库数量
                            sum(numIn-numOut) sumNum,--剩余数量
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）                             

                            sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
                            sum(sumPriceOut) sumPriceOut--出库金额
                            from viewGoodsOutInFlow where bizDate<=@end ";

            if (goodsId != 0)
            {
                sql += " and goodsId='"+goodsId+"'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            
            sql+=@"
                            group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";

            sql += "union all ";


            sql += @"select '' goodsId,code,goodsName+'-小计：' goodsName,'' spec,'' unitName,'' ckId,'所有仓库' ckName,
                                sum(numIn) sumNumIn,--入库数量
                                sum(numOut) sumNumOut,--出库数量
                                sum(numIn-numOut) sumNum,--剩余数量
                                case sum(numIn) when 0 then 0
                                else
                                sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）   
                                sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
                                sum(sumPriceOut) sumPriceOut--出库金额

                        from viewGoodsOutInFlow
                        where bizDate<=@end ";

            if (goodsId != 0)
            {

                sql += " and goodsId='"+goodsId+"'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql+=@"

                        group by goodsId,code,goodsName,spec,unitName 

                        order by code,goodsName";
        
            SqlParameter[] param = {
                                       
                                       new SqlParameter("@end",end)
                                     
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text,sql, param);
        }


        #endregion

        #region 查询商品库存余额表-----------NewUI

        /// <summary>
        /// 查询商品库存余额表-----------NewUI
        /// </summary>       
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNum(int shopId,DateTime end, string code, string ckName)
        {
            string sqlGoods = @" select a.Id goodsId,a.code,a.Names goodsName,a.spec,a.unitName,a.remarks,b.ckId,b.ckName,a.priceCost,
fieldA,fieldB,fieldC,fieldD ";

            sqlGoods += ",sumNumIn,sumNumOut,sumNum,sumPriceNow,sumPriceIn,sumPriceOut,a.priceCost,a.priceCost*sumNum sumPriceStore ";

            sqlGoods += " from viewGoods a left join (";


            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,--入库数量
                            sum(numOut) sumNumOut,--出库数量
                            sum(numIn-numOut) sumNum,--剩余数量
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）                             

                            sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
                            sum(sumPriceOut) sumPriceOut--出库金额
                            from viewGoodsOutInFlow where bizDate<=@end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }


            sql += @"
                            group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";


            sqlGoods += sql;

            sqlGoods += " ) b  on a.id=b.goodsId ";


            sqlGoods += " where a.shopId='" + shopId + "' ";

            if (code != "")
            {
                sqlGoods += " and a.code in(" + code + ")   ";
            }


            #region 所有仓库合计

//            sql += "union all ";


//            sql += @"select '' goodsId,code,goodsName+'-小计：' goodsName,'' spec,'' unitName,'' ckId,'所有仓库' ckName,
//                                sum(numIn) sumNumIn,--入库数量
//                                sum(numOut) sumNumOut,--出库数量
//                                sum(numIn-numOut) sumNum,--剩余数量
//                                case sum(numIn) when 0 then 0
//                                else
//                                sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）   
//                                sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
//                                sum(sumPriceOut) sumPriceOut--出库金额
//
//                        from viewGoodsOutInFlow
//                        where bizDate<=@end ";

//            if (shopId >= 0)
//            {
//                sql += " and shopId='" + shopId + "' ";
//            }

//            if (code != "")
//            {
//                sql += " and code in(" + code + ")";
//            }

//            if (ckName != "")
//            {
//                sql += " and ckName in(" + ckName + ")";
//            }

//            sql += @"
//
//                        group by goodsId,code,goodsName,spec,unitName 
//
//                        order by code,goodsName";

            #endregion


            SqlParameter[] param = {
                                       
                                       new SqlParameter("@end",end)
                                     
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sqlGoods, param);
        }


        #endregion


        #region 查询商品库存余额表-----------NewUI---weixinQY

        /// <summary>
        /// 查询商品库存余额表-----------NewUI---weixinQY
        /// </summary>       
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNumWeinxQY(int shopId, DateTime end, string code, string ckName)
        {
            string sqlGoods = @" select a.Id goodsId,a.code,a.Names goodsName,a.spec,a.unitName,a.remarks,b.ckId,b.ckName,a.priceCost ";

            sqlGoods += ",sumNumIn,sumNumOut,sumNum,sumPriceNow,sumPriceIn,sumPriceOut,a.priceCost,a.priceCost*sumNum sumPriceStore ";

            sqlGoods += " from viewGoods a left join (";


            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,--入库数量
                            sum(numOut) sumNumOut,--出库数量
                            sum(numIn-numOut) sumNum,--剩余数量
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）                             

                            sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
                            sum(sumPriceOut) sumPriceOut--出库金额
                            from viewGoodsOutInFlow where bizDate<=@end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and (code like '%" + code + "' or goodsName like '%" + code + "%' or spec like '%" + code + "%' ) ";
            }

            if (ckName != "")
            {
                sql += " and ckName ='" + ckName + "' ";
            }


            sql += @"
                            group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";


            sqlGoods += sql;

            sqlGoods += " ) b  on a.id=b.goodsId ";


            sqlGoods += " where a.shopId='" + shopId + "' ";

            if (code != "")
            {
                sqlGoods += " and (a.code like '%" + code + "' or a.names like '%" + code + "%' or a.spec like '%" + code + "%' ) ";
            }


            #region 所有仓库合计

            //            sql += "union all ";


            //            sql += @"select '' goodsId,code,goodsName+'-小计：' goodsName,'' spec,'' unitName,'' ckId,'所有仓库' ckName,
            //                                sum(numIn) sumNumIn,--入库数量
            //                                sum(numOut) sumNumOut,--出库数量
            //                                sum(numIn-numOut) sumNum,--剩余数量
            //                                case sum(numIn) when 0 then 0
            //                                else
            //                                sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）   
            //                                sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
            //                                sum(sumPriceOut) sumPriceOut--出库金额
            //
            //                        from viewGoodsOutInFlow
            //                        where bizDate<=@end ";

            //            if (shopId >= 0)
            //            {
            //                sql += " and shopId='" + shopId + "' ";
            //            }

            //            if (code != "")
            //            {
            //                sql += " and code in(" + code + ")";
            //            }

            //            if (ckName != "")
            //            {
            //                sql += " and ckName in(" + ckName + ")";
            //            }

            //            sql += @"
            //
            //                        group by goodsId,code,goodsName,spec,unitName 
            //
            //                        order by code,goodsName";

            #endregion


            SqlParameter[] param = {
                                       
                                       new SqlParameter("@end",end)
                                     
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sqlGoods, param);
        }


        #endregion


        #region 查询商品库存余额表-----------NewUI

        /// <summary>
        /// 查询商品库存余额表-----------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNum(string key)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,--入库数量
                            sum(numOut) sumNumOut,--出库数量
                            sum(numIn-numOut) sumNum,--剩余数量
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）                             

                            sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
                            sum(sumPriceOut) sumPriceOut--出库金额
                            from viewGoodsOutInFlow where 1=1  ";

            if (key != "")
            {
                sql += " and goodsName like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }


            sql += @"
                            group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";

            sql += "union all ";


            sql += @"select '' goodsId,code,goodsName+'-小计：' goodsName,'' spec,'' unitName,'' ckId,'所有仓库' ckName,
                                sum(numIn) sumNumIn,--入库数量
                                sum(numOut) sumNumOut,--出库数量
                                sum(numIn-numOut) sumNum,--剩余数量
                                case sum(numIn) when 0 then 0
                                else
                                sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,--库存余额=入库金额-出库数量*（入库金额/入库数量）   
                                sum(sumPriceIn) sumPriceIn,--入库金额、包含有成本调整的在里面了。
                                sum(sumPriceOut) sumPriceOut--出库金额

                        from viewGoodsOutInFlow
                        where 1=1  ";

            if (key != "")
            {
                sql += " and goodsName like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }

            sql += @"

                        group by goodsId,code,goodsName,spec,unitName 

                        order by code,goodsName";



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 库存查询----横向显示仓库库存

        /// <summary>
        /// 库存查询----横向显示仓库库存
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelViewAndNum(DateTime end, string code)
        {
            string sql = "select a.*,isnull(sumNum,0) sumNum ,a.priceCost*sumNum sumPriceCost from viewGoods a ";

            sql += " left join ";



            sql += @"(
                       select goodsId,
                        sum(numIn-numOut) as 'sumNum'
                      
                        from viewGoodsOutInFlow
                        where  bizDate<=@end 
                        group by goodsId 


                 )";

            sql += " n on a.id=n.goodsId ";


            sql += "  where 1=1  ";


            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }


            sql += " order by code ";


            SqlParameter[] param = {
                                       
                                       new SqlParameter("@end",end)
                                     
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);

        }




        #endregion


        #region 商品查询---包括库存一起

        /// <summary>
        /// 商品查询---包括库存一起
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelViewAndNumKeys(DateTime end, string Keys)
        {
            string sql = "select a.*,isnull(sumNum,0) sumNum ,a.priceCost*sumNum sumPriceCost from viewGoods a ";

            sql += " left join ";



            sql += @"(
                       select goodsId,
                        sum(numIn-numOut) as 'sumNum'
                      
                        from viewGoodsOutInFlow
                        where  bizDate<=@end 
                        group by goodsId 


                 )";

            sql += " n on a.id=n.goodsId ";


            sql += "  where 1=1  ";


            if (Keys != "")
            {
                sql += " and ( code like  '%" + Keys + "%' or barcode like  '%" + Keys + "%' or names  like  '%" + Keys + "%' or spec like  '%" + Keys + "%' )";
            }


            sql += " order by code ";


            SqlParameter[] param = {
                                       
                                       new SqlParameter("@end",end)
                                     
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);

        }







        #endregion

        #region 查询商品库存--盘点用

        /// <summary>
        /// 查询商品库存--盘点用
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="ckId"></param>
        /// <param name="goodsName"></param>
        /// <param name="zero"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNumNow(int typeId, int ckId,string goodsName,int zero)
        {
            string sql = "select ckId,ckName,goodsId,code,goodsName,unitName,spec,sum(num) sumNum from viewGoodsStoreNum where 1=1 ";

            if (typeId != 0)
            {

                sql += " and typeId='"+typeId+"' ";
            }
            if (ckId != 0)
            {

                sql += " and ckId='" + ckId + "' ";
            }

          

            if (goodsName != "")
            {

                sql += " and goodsName like '%" + goodsName + "%' ";
            }
                
            sql+=  "group by ckId,ckName,goodsId,code,goodsName,unitName,spec ";

            if (zero != 0)
            {

                sql += " having sum(Num)=0 ";
            }

            sql += "  order by ckId,code ";

            

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql,null);
        }


        #endregion


        #region 查询商品库存--盘点用----------NewUI

        /// <summary>
        /// 查询商品库存--盘点用----------NewUI
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="ckId"></param>
        /// <param name="goodsName"></param>
        /// <param name="zero"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNumNow(int shopId,string typeId, string ckId, string code)
        {

            string sql = "select ckId,ckName,goodsId,code,goodsName,unitName,spec,sum(num) sumNum from viewGoodsStoreNum where 1=1 ";

            if (shopId != 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (typeId != "")
            {

                sql += " and typeName in (" + typeId + ")";
            }


            if (ckId != "")
            {

                sql += " and ckName in (" + ckId + ") ";
            }



            if (code != "")
            {

                sql += " and code in (" + code + ") ";
            }

            sql += "group by ckId,ckName,goodsId,code,goodsName,unitName,spec ";

          

            sql += "  order by ckId,code ";



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion




        #region 查询商品库存--检查负库存用

        /// <summary>
        /// 查询商品库存--检查负库存用
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public decimal  GetGoodsStoreNumNow(int goodsId, int ckId)
        {
            decimal num = 0;
            string sql = "select ckId,ckName,goodsId,code,goodsName,unitName,spec,sum(num) sumNum from viewGoodsStoreNum where 1=1 ";

            if (goodsId != 0)
            {

                sql += " and goodsId='" + goodsId + "' ";
            }
            if (ckId != 0)
            {

                sql += " and ckId='" + ckId + "' ";
            }
            sql += "group by ckId,ckName,goodsId,code,goodsName,unitName,spec ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

            if (ds.Tables[0].Rows.Count > 0)
            {
                num = ConvertTo.ConvertDec(ds.Tables[0].Rows[0]["sumNum"].ToString());

            }

            return num;


        }


        #endregion


        #region 查询商品收发明细表-------------已经废弃

        /// <summary>
        /// 查询商品收发明细表-------------已经废弃
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInDetailReportBack(DateTime start, DateTime end, int goodsId, int ckId)
        {

            string sql = @"select wlName,'期初余额' bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                            0 numBegin,0 numIn,0 numOut,sum(numIn-numOut) numEnd,priceIn,sumPriceIn,priceOut,sumPriceOut,sumPriceIn-sumPriceOut sumPriceEnd
                            from viewGoodsOutInFlow 
                            where bizDate<@start ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }
            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql += " group by wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName ";

            sql += " union ";

            sql += @"select  wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,

                            numBegin=isnull
                            (
                              ( select sum(numIn-numOut) from viewGoodsOutInFlow t1
                               where t1.goodsId=goodsId and t1.bizDate>bizDate and t1.ckId=ckId
                              )
                            ,0),
                            numIn,
                            numOut,
                            numEnd=isnull(

                              (   select sum(numIn-numOut) from viewGoodsOutInFlow 
                                  where t1.goodsId=goodsId and t1.bizDate>bizDate and t1.ckId=ckId
                              ),0)
                            +numIn-numOut,
                            sumPriceEnd=isnull
                            (
                             (
                                select sum(sumPriceIn-sumPriceOut) from viewGoodsOutInFlow t1
                               where t1.goodsId=goodsId and t1.bizDate>bizDate and t1.ckId=ckId
                              )
                            ,0),
                            priceIn,sumPriceIn,priceOut,sumPriceOut,sumPriceIn-sumPriceOut sumPriceEnd
                            from viewGoodsOutInFlow t1

                            where bizDate>=@start and bizDate<=@end ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }
            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql += " order by goodsId,code,ckName,bizDate";

          
            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@goodsId",goodsId),
                                       new SqlParameter("@ckId",ckId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text,sql, param);
        }


        #endregion



        #region 查询商品收发明细表

        /// <summary>
        /// 查询商品收发明细表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInDetailReport(DateTime start, DateTime end, int goodsId, int ckId)
        {

            string sql = @"select isnull(wlName,'') wlName,'期初余额' bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                            0 numBegin,0 priceBegin,0 sumPriceBegin,0 numIn,0 priceIn,0 sumPriceIn,0 numOut,0 priceOut,0 sumPriceOut,sum(numIn-numOut) numEnd,0 priceEnd,sum(sumPriceIn-sumPriceOut) sumPriceEnd
                            from viewGoodsOutInFlow 
                            where bizDate<@start ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }
            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql += " group by wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName ";

            sql += " union ";

            sql += @"  select isnull(wlName,'') wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                            0 numBegin,0 priceBegin,0 sumPriceBegin,numIn,priceIn,sumPriceIn,numOut,priceOut,sumPriceOut,numIn-numOut numEnd,0 priceEnd,sumPriceIn-sumPriceOut sumPriceEnd
                        from viewGoodsOutInFlow 
                            where bizDate>=@start and bizDate<=@end ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }
            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql += " order by goodsId,code,ckName,bizDate";


            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@goodsId",goodsId),
                                       new SqlParameter("@ckId",ckId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询商品收发明细表

        /// <summary>
        /// 查询商品收发明细表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsBalanceDetail(DateTime start, DateTime end, int goodsId, int ckId)
        {
            //sum(isnull(b.sumPriceIn,0)-isnull(b.sumPriceOut,0)) sumPriceEnd -----------这里不知该如何取数？？？
            string sql = @"select '1' seq,'' wlName,'期初余额' bizType,'' number,'' bizDate,id goodsId,names goodsName,g.typeId,g.typeName,g.spec,g.code,g.unitName, --商品基础资料表
                                        b.ckId,
                                        b.ckName,
                                        0 numBegin,
                                        0 priceBegin,
                                        0 sumPriceBegin,
                                        0 numIn,
                                        0 priceIn,
                                        0 sumPriceIn,
                                        0 numOut,
                                        0 priceOut,
                                        0 sumPriceOut,
                                        sum(isnull(b.numIn,0)-isnull(b.numOut,0)) numEnd,--期末数量
                                        case
                                         sum( 
                                             
                                             isnull(b.numIn,0)-isnull(b.numOut,0)

                                             ) when 0 then 0
                                        else

                                        (
                                             sum(sumPriceIn)-sum(NumOut)*( case sum(numIn) when 0 then 0 else   sum(sumPriceIn)/sum(numIn)   end  )
                                        ) 
                                        /
                                          
                                        (
                                           sum(
                                                isnull(b.numIn,0)-isnull(b.numOut,0)
                                               )

                                           )
                                        end priceEnd,    --0 priceEnd,--期初单位成本=库存余额/库存数量
                                        case sum(numIn) when 0 then 0 
                                        else      
                    
                                        sum(sumPriceIn)-sum(NumOut)*(case sum(numIn) when 0 then 0 else  sum(sumPriceIn)/sum(numIn)  end ) end sumPriceEnd--库存余额=入库金额-出库数量*（入库金额/入库数量）

                                        from viewGoods g
                                        left join viewGoodsOutInFlow b on g.id=b.goodsId and b.bizDate<@start ";

            if (ckId != 0)
            {
                sql += " and b.ckId='" + ckId + "'";
            }


            sql += "   where 1=1  ";

            if (goodsId != 0)
            {
                sql += " and g.Id='" + goodsId + "'";
            }
           

            sql += @" group by  id,g.code,g.spec,names,g.typeId,g.typeName,g.unitName, --商品基础资料表
                            b.ckId,
                            b.ckName";

            sql += " union all ";

            sql += @"  select '2' seq,isnull(wlName,'') wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                                    0 numBegin,
                                    0 priceBegin,
                                    0 sumPriceBegin,
                                    numIn,
                                    priceIn,
                                    sumPriceIn,
                                    numOut,
                                    priceOut,
                                    sumPriceOut,
                                    0 numEnd,--先赋值0，c#结合期初进行处理
                                    0 priceEnd,--先赋值0，c#结合期初进行处理
                                    0 sumPriceEnd --先赋值0，c#结合期初进行处理

                        from viewGoodsOutInFlow 
                            where bizDate>=@start and bizDate<=@end  and bizType<>'初始余额'";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }
            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql += " order by goodsId,code,seq,ckName,bizDate";


            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@goodsId",goodsId),
                                       new SqlParameter("@ckId",ckId)
                                   };

            return  SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询商品收发明细表--------NewUI

        /// <summary>
        /// 查询商品收发明细表--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsBalanceDetail(int shopId,DateTime start, DateTime end, string code, string ckName)
        {
            //sum(isnull(b.sumPriceIn,0)-isnull(b.sumPriceOut,0)) sumPriceEnd -----------这里不知该如何取数？？？
            string sql = @"select '1' seq,'' wlName,'期初余额' bizType,'' number,'' bizDate,id goodsId,names goodsName,g.typeId,g.typeName,g.spec,g.code,g.unitName, --商品基础资料表
                                        b.ckId,
                                        b.ckName,
                                        0 numBegin,
                                        0 priceBegin,
                                        0 sumPriceBegin,
                                        0 numIn,
                                        0 priceIn,
                                        0 sumPriceIn,
                                        0 numOut,
                                        0 priceOut,
                                        0 sumPriceOut,
                                        sum(isnull(b.numIn,0)-isnull(b.numOut,0)) numEnd,--期末数量
                                        case
                                         sum( 
                                             
                                             isnull(b.numIn,0)-isnull(b.numOut,0)

                                             ) when 0 then 0
                                        else

                                        (
                                             sum(sumPriceIn)-sum(NumOut)*( case sum(numIn) when 0 then 0 else   sum(sumPriceIn)/sum(numIn)   end  )
                                        ) 
                                        /
                                          
                                        (
                                           sum(
                                                isnull(b.numIn,0)-isnull(b.numOut,0)
                                               )

                                           )
                                        end priceEnd,    --0 priceEnd,--期初单位成本=库存余额/库存数量
                                        case sum(numIn) when 0 then 0 
                                        else      
                    
                                        sum(sumPriceIn)-sum(NumOut)*(case sum(numIn) when 0 then 0 else  sum(sumPriceIn)/sum(numIn)  end ) end sumPriceEnd--库存余额=入库金额-出库数量*（入库金额/入库数量）

                                        from viewGoods g
                                        left join viewGoodsOutInFlow b on g.id=b.goodsId and b.bizDate<@start ";

            //if (ckId != 0)
            //{
            //    sql += " and b.ckId='" + ckId + "'";
            //}


            if (shopId >= 0)
            {
                sql += " and b.shopId='" + shopId + "' ";
            }

            if (ckName != "")
            {
                sql += " and b.ckName in(" + ckName + ")";
            }



            sql += "   where 1=1  ";

            if (shopId >= 0)
            {
                sql += " and g.shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and g.code in(" + code + ")";
            }


            sql += @" group by  id,g.code,g.spec,names,g.typeId,g.typeName,g.unitName, --商品基础资料表
                            b.ckId,
                            b.ckName";

            sql += " union all ";

            sql += @"  select '2' seq,isnull(wlName,'') wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                                    0 numBegin,
                                    0 priceBegin,
                                    0 sumPriceBegin,
                                    numIn,
                                    priceIn,
                                    sumPriceIn,
                                    numOut,
                                    priceOut,
                                    sumPriceOut,
                                    0 numEnd,--先赋值0，c#结合期初进行处理
                                    0 priceEnd,--先赋值0，c#结合期初进行处理
                                    0 sumPriceEnd --先赋值0，c#结合期初进行处理

                        from viewGoodsOutInFlow 
                            where bizDate>=@start and bizDate<=@end  and bizType<>'初始余额'";

            //if (goodsId != 0)
            //{
            //    sql += " and goodsId='" + goodsId + "'";
            //}

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }


            //if (ckId != 0)
            //{
            //    sql += " and ckId='" + ckId + "'";
            //}

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }




            sql += " order by goodsId,code,seq,ckName,bizDate";


            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                      
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion



        #region 查询商品收发汇总表------------已经废弃

        /// <summary>
        /// 查询商品收发汇总表------------已经废弃
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInSumReportBack(DateTime start, DateTime end, int goodsId, int ckId)
        {

            string sql = @"select fs.goodsId,fs.code,fs.goodsName,fs.spec,fs.unitName,fs.ckId,fs.ckName,
                            qc.numBegin,--期初
                            max(case bizType when '调拨入库' then numIn else 0 end) numInDB,--1
                            max(case bizType when '采购入库' then numIn else 0 end) numInPur,--2
                            max(case bizType when '采购退货' then numIn else 0 end) numInTH,--3
                            max(case bizType when '其他入库' then numIn else 0 end) numInQT,--4
                            max(case bizType when '盘盈入库' then numIn else 0 end) numInPY,--5
                            max(case types when 1 then numIn else 0 end) numInSum,--入库合计 --6

                            ----上面是入库的，下面是出库的

                            max(case bizType when '调拨出库' then numOut else 0 end) numOutDB,--1
                            max(case bizType when '销售出库' then numOut else 0 end) numOutSales,--2
                            max(case bizType when '销售退货' then numOut else 0 end) numOutTH,--3
                            max(case bizType when '其他出库' then numOut else 0 end) numOutQT,--4
                            max(case bizType when '盘亏出库' then numOut else 0 end) numOutPK,--5
                            max(case types  when -1 then numOut else 0 end) numOutSum,--6 出库合计

                            qc.numBegin+max(case types when 1 then numIn else 0 end)-max(case types  when -1 then numOut else 0 end) numEnd

                            from viewGoodsOutInFlowSum fs
                            left join 
                            (
                                ---这里是期初余额
                                select goodsId,ckId,
                                sum(numIn-numOut) numBegin

                                from viewGoodsOutInFlowSum
                                
                                where bizDate<@start ";
            if (goodsId != 0)
            {
                sql += " and goodsId='"+goodsId+"'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }


           
            sql+=@"

                                group by goodsId,ckId
                            )
                            qc on fs.goodsId=qc.goodsId and fs.ckId=qc.ckId 

                            where bizDate>=@start and bizDate<=@end ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }


            sql += " group by fs.goodsId,fs.code,fs.goodsName,fs.spec,fs.unitName,fs.ckId,fs.ckName,qc.numBegin ";


            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@goodsId",goodsId),
                                       new SqlParameter("@ckId",ckId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询商品收发汇总表

        /// <summary>
        /// 查询商品收发汇总表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInSumReport(DateTime start, DateTime end, int goodsId, int ckId)
        {

            string sql = @"select g.goodsId,g.code,g.goodsName,g.spec,g.unitName,g.ckId,g.ckName,
                                    sumNumBegin,sumPriceBegin,--期初数量、金额
                                    isnull(sumNumIn,0) sumNumIn,isnull(sumPriceIn,0) sumPriceIn,  --本期入库数量、金额
                                    isnull(sumNumOut,0) sumNumOut,isnull(sumPriceOut,0) sumPriceOut,--本期出库数量、金额
                                    sumNumInAll,sumPriceInAll,--本年累计入库数量、金额
                                    sumNumOutAll,sumPriceOutAll,--本年累计出库数量、金额
                                    isnull(sumNumBegin,0)+isnull(sumNumIn,0)-isnull(sumNumOut,0) sumNumEnd,--结存数量
                                    sumPriceBegin+isnull(sumPriceIn,0)-isnull(sumPriceOut,0) sumPriceEnd --结存金额

                                    from (select distinct goodsId,code,goodsName,spec,unitName,ckId,ckName from viewGoodsOutInFlow) g

                                    left join
                                    (
                                    --先求期初的 当前时间之前的汇总
                                    select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                                    sum(NumIn-NumOut) as sumNumBegin,
                                    0 priceBegin,
                                    sum(sumPriceIn-sumPriceOut) as sumPriceBegin
                                    from viewGoodsOutInFlow

                                    where bizDate<@start ";


            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            
            
            sql+=@"

                                    group by goodsId,code,goodsName,spec,unitName,ckId,ckName

                                    ) qc on g.goodsId=qc.goodsId and g.ckId=qc.ckId

                                    --------------本期发生的出、入库

                                    left join 
                                    (
                                    select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                                    sum(NumIn) sumNumIn,
                                    0 priceIn,
                                    sum(sumPriceIn) sumPriceIn,

                                    sum(NumOut) sumNumOut,
                                    0 priceOut,
                                    sum(sumPriceOut) sumPriceOut

                                    from viewGoodsOutInFlow
                                    where bizDate<=@end and bizDate>=@start ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql+=@"
                                    group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                                    )
                                     fs on g.goodsId=fs.goodsId and g.ckId=fs.ckId

                                    --------------本年发生的出、入库汇总

                                    left join
                                    (
                                    select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                                    sum(NumIn) sumNumInAll,
                                    0 priceInAll,
                                    sum(sumPriceIn) sumPriceInAll,

                                    sum(NumOut) sumNumOutAll,
                                    0 priceOutAll,
                                    sum(sumPriceOut) sumPriceOutAll

                                    from viewGoodsOutInFlow
                                    where bizDate<=@end ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            
            sql+=@"
                                    group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                                    )
                                    t on g.goodsId=t.goodsId and g.ckId=t.ckId  where 1=1 ";


            if (goodsId != 0)
            {
                sql += " and g.goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and g.ckId='" + ckId + "'";
            }


            sql+=@"




                                    order by g.code,g.ckId ";


            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end),
                                       new SqlParameter("@goodsId",goodsId),
                                       new SqlParameter("@ckId",ckId)
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion


        #region 查询商品收发汇总表-------------NewUI

        /// <summary>
        /// 查询商品收发汇总表-------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInSumReport(int shopId,DateTime start, DateTime end, string code, string ckName)
        {

            string sql = @"select g.goodsId,g.code,g.goodsName,g.spec,g.unitName,g.ckId,g.ckName,
                                    sumNumBegin,sumPriceBegin,--期初数量、金额
                                    isnull(sumNumIn,0) sumNumIn,isnull(sumPriceIn,0) sumPriceIn,  --本期入库数量、金额
                                    isnull(sumNumOut,0) sumNumOut,isnull(sumPriceOut,0) sumPriceOut,--本期出库数量、金额
                                    sumNumInAll,sumPriceInAll,--本年累计入库数量、金额
                                    sumNumOutAll,sumPriceOutAll,--本年累计出库数量、金额
                                    isnull(sumNumBegin,0)+isnull(sumNumIn,0)-isnull(sumNumOut,0) sumNumEnd,--结存数量
                                    sumPriceBegin+isnull(sumPriceIn,0)-isnull(sumPriceOut,0) sumPriceEnd --结存金额

                                    from (select distinct shopId,goodsId,code,goodsName,spec,unitName,ckId,ckName from viewGoodsOutInFlow) g

                                    left join
                                    (
                                    --先求期初的 当前时间之前的汇总
                                    select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                                    sum(NumIn-NumOut) as sumNumBegin,
                                    0 priceBegin,
                                    sum(sumPriceIn-sumPriceOut) as sumPriceBegin
                                    from viewGoodsOutInFlow

                                    where bizDate<@start ";


            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            //if (ckId != 0)
            //{
            //    sql += " and ckId='" + ckId + "'";
            //}

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }


            sql += @"

                                    group by goodsId,code,goodsName,spec,unitName,ckId,ckName

                                    ) qc on g.goodsId=qc.goodsId and g.ckId=qc.ckId

                                    --------------本期发生的出、入库

                                    left join 
                                    (
                                    select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                                    sum(NumIn) sumNumIn,
                                    0 priceIn,
                                    sum(sumPriceIn) sumPriceIn,

                                    sum(NumOut) sumNumOut,
                                    0 priceOut,
                                    sum(sumPriceOut) sumPriceOut

                                    from viewGoodsOutInFlow
                                    where bizDate<=@end and bizDate>=@start ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            //if (ckId != 0)
            //{
            //    sql += " and ckId='" + ckId + "'";
            //}

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }

            sql += @"
                                    group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                                    )
                                     fs on g.goodsId=fs.goodsId and g.ckId=fs.ckId

                                    --------------本年发生的出、入库汇总

                                    left join
                                    (
                                    select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                                    sum(NumIn) sumNumInAll,
                                    0 priceInAll,
                                    sum(sumPriceIn) sumPriceInAll,

                                    sum(NumOut) sumNumOutAll,
                                    0 priceOutAll,
                                    sum(sumPriceOut) sumPriceOutAll

                                    from viewGoodsOutInFlow
                                    where bizDate<=@end ";

            if (shopId >= 0)
            {
                sql += " and shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and code in(" + code + ")";
            }

            //if (ckId != 0)
            //{
            //    sql += " and ckId='" + ckId + "'";
            //}

            if (ckName != "")
            {
                sql += " and ckName in(" + ckName + ")";
            }


            sql += @"
                                    group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                                    )
                                    t on g.goodsId=t.goodsId and g.ckId=t.ckId  where 1=1 ";


            //if (goodsId != 0)
            //{
            //    sql += " and g.goodsId='" + goodsId + "'";
            //}

            //if (ckId != 0)
            //{
            //    sql += " and g.ckId='" + ckId + "'";
            //}


            if (shopId >= 0)
            {
                sql += " and g.shopId='" + shopId + "' ";
            }

            if (code != "")
            {
                sql += " and g.code in(" + code + ")";
            }

            //if (ckId != 0)
            //{
            //    sql += " and ckId='" + ckId + "'";
            //}

            if (ckName != "")
            {
                sql += " and g.ckName in(" + ckName + ")";
            }


            sql += @"




                                    order by g.code,g.ckId ";


            SqlParameter[] param = {
                                       new SqlParameter("@start",start),
                                       new SqlParameter("@end",end)
                                      
                                   };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }


        #endregion








        //------------------------------------------以下是在线订货的方法

        #region 查询商品信息

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetGoodsInfo(string code, string names, string spec, int typeId, string salesFlag, string flag, int xp, int tj, int cx)
        {
            string sql = "select * from viewgoods where 1=1 ";
            if (code != "")
            {
                sql += " and code like '%" + code + "%'";
            }

            if (names != "")
            {
                sql += " and names like '%" + names + "%'";
            }


            if (spec != "")
            {
                sql += " and spec like '%" + spec + "%'";
            }

            if (typeId != 0)
            {
                sql += " and typeId = '" + typeId + "'";
            }

            //if (xp != 0)
            //{
            //    sql += " and xp = '" + xp + "'";
            //}

            //if (cx != 0)
            //{
            //    sql += " and cx = '" + cx + "'";
            //}

            //if (tj != 0)
            //{
            //    sql += " and tj = '" + tj + "'";
            //}

            if (salesFlag != "0")
            {
                sql += " and salesFlag = '" + salesFlag + "'";
            }

            if (flag != "0")
            {
                sql += " and flag = '" + flag + "'";
            }
            sql += " order by code ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 查询商品信息

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetGoodsInfo(string keys)
        {
            string sql = "select * from viewgoods where names like '%" + keys + "%' or code like '%" + keys + "%'  order by code ";
         
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 设置商品--------删除

        /// <summary>
        /// 设置商品--------删除
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public int DeleteGoodsShow(int goodsId)
        {
            string sql = " delete from goodsShow where goodsId='" + goodsId + "' ";
           

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }


        #endregion

        #region 设置商品

        /// <summary>
        /// 设置商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="tj"></param>
        /// <param name="xp"></param>
        /// <param name="cx"></param>
        /// <returns></returns>
        public int SetGoodsShow(int goodsId, int tj, int xp, int cx)
        {
            string sql = " delete from goodsShow where goodsId='"+goodsId+"' ";
            sql += " insert into goodsShow(goodsId,tj,xp,cx) values('" + goodsId + "','" + tj + "','" + xp + "','" + cx + "') ";

            

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr,CommandType.Text,sql,null);



        }


        #endregion


        #region 设置商品

        /// <summary>
        /// 设置商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="tj"></param>
        /// <param name="xp"></param>
        /// <param name="cx"></param>
        /// <returns></returns>
        public int SetGoodsShow(int goodsId,string cloumn, int selectYes)
        {
            string sql = "if not exists( select * from goodsShow where goodsId='"+goodsId+"') ";

            sql += " begin ";

            sql += " insert into goodsShow(goodsId,tj,xp,cx) values('" + goodsId + "',0,0,0) ";


            sql += " end ";//如果没有，就先插入一条

          

            sql += " update goodsShow set " + cloumn + "='" + selectYes + "'  where goodsId='" + goodsId + "' ";


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }


        #endregion


        #region 查询订货页面的商品-------通过存储过程

        /// <summary>
        /// 查询订货页面的商品-------通过存储过程
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataSet GetAllModelView(int clientTypeId,int typeId, string keys)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@clientTypeId",clientTypeId),
                                      new SqlParameter("@typeId",typeId),
                                      new SqlParameter("@keys",keys)
                                 };


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "procGetGoodsPrice", param);

        }


        #endregion

        

        #region 查询订货页面的商品-------通过存储过程、客户等级ID 和商品编号

        /// <summary>
        /// -------通过存储过程、客户等级ID 和商品编号
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataSet GetGoodsDetailByClientTypeIdAndGodsId(int clientTypeId, int goodsId)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@clientTypeId",clientTypeId),
                                      new SqlParameter("@goodsId",goodsId)
                                 };


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "procGetGoodsPriceByGoodsId", param);

        }


        #endregion

        #region 查询订货页面的商品-----------NewUI--------后台

        /// <summary>
        /// 查询订货页面的商品----------NewUI--------后台
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelViewBaseSet(int typeId, string key,int isShow)
        {
            string sql = "select *,priceSalesRetail typeNamePrice from viewGoods where 1=1 ";

            if (typeId != 0)
            {
                sql += " and typeId ='" + typeId + "' or typeId in (select id from goodsType where parentId = " + typeId + " )";
            }

            if (isShow != -1)
            {
                sql += " and isShow ='" + isShow + "' ";
            }


            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 查询订货页面的商品-----------NewUI--------游客

        /// <summary>
        /// 查询订货页面的商品----------NewUI--------游客
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int typeId, string key)
        {
            string sql = "select *,priceSalesRetail typeNamePrice from viewGoods where isShow=1 ";

            if (typeId != 0)
            {
                sql += " and typeId ='" + typeId + "' or typeId in (select id from goodsType where parentId = " + typeId + " )";
            }

            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 查询订货页面的商品----推荐、促销、新品----------NewUI

        /// <summary>
        /// 查询订货页面的商品----推荐、促销、新品----------NewUI
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(string typeId, string key)
        {
            string sql = "select * from viewGoodsShow where 1=1 ";

            if (typeId != "")
            {
                sql += " and typeName in (" + typeId + ")";
            }

            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 查询订货页面的商品----推荐、促销、新品

        /// <summary>
        /// 查询订货页面的商品----推荐、促销、新品
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelViewWeixin(int typeId, string key)
        {
            string sql = "select * from viewGoodsWeixin where 1=1 ";

            if (typeId != 0)
            {
                sql += " and typeId='" + typeId + "' ";
            }

            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 分页读取数据

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="Pager1"></param>
        /// <param name="Pager2"></param>
        /// <param name="typeId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetAllDataSet(int Pager1, int Pager2, int typeId, string tableName)
        {
            string sql = "select  * from [" + tableName + "] where 1=1  ";

            if (typeId != 0)
            {
                sql += " and typeId=" + typeId.ToString();
            }
            sql += "  order by [id] desc ";

          

            DataSet ds = SQLHelper.SqlDataAdapter(Pager1,Pager2,SQLHelper.ConStr, CommandType.Text, sql, null);

            return ds;

          


        }


        #endregion



        /// <summary>
        /// 返回的记录数
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetALLDataRowCount(int typeId, string tableName)
        {
            int count = 0;

            string sql = "select  * from [" + tableName + "] where 1=1 ";

            if (typeId != 0)
            {
                sql += " and typeId=" + typeId.ToString();
            }
          //  sql += "  order by [id]  ";


            count = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null).Tables[0].Rows.Count;

            return count;
        }

        #region 查询订货页面的商品----推荐、促销、新品

        /// <summary>
        /// 查询订货页面的商品----推荐、促销、新品
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int tj, int xp, int cx)
        {
            string sql = "select * from viewGoods where 1=1 ";

            //if (tj != -1)
            //{
            //    sql += " and tj='" + tj + "' ";
            //}
            //if (cx != -1)
            //{
            //    sql += " and cx='" + cx + "' ";
            //}

            //if (xp != -1)
            //{
            //    sql += " and xp='" + xp + "' ";
            //}

            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 通过客户类别ID-查询商品-获取不同等级价格

        /// <summary>
        /// 通过客户类别ID-查询商品-获取不同等级价格
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public DataSet GetAllGoodsByClientTypeId(int typeId)
        {

            SqlParameter[] param ={
                                      new SqlParameter("@typeId",typeId)
                                 };


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "procGetGoodsPrice", param);
        }


        #endregion


        #region 热销商品排行

        /// <summary>
        /// 热销商品排行
        /// </summary>
        /// <param name="orderBy">排行：sumNum 按数量，sumPriceAll 按金额</param>
        /// <param name="desc">desc 畅销，asc 滞销</param>
        /// <returns></returns>
        public DataSet TopGoodsList(string orderBy,string desc)
        {
            string sql = @" select top 10  a.*,isnull(s.sumNum,0) sumNum,isnull(s.sumPriceAll,0) sumPriceAll
                                from viewGoods a
                                left join
                                (
	                                select goodsId,sum(num) sumNum,sum(sumPriceAll) sumPriceAll
	                                from salesOrderItem
	                                group by goodsId
                                )
                                s on a.id=s.goodsId ";

            sql += " order by  "+ orderBy + "   " + desc;

           



            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region 收藏商品

        public int SaveGoods(int clientId, int goodsId)
        {
            string sql = " delete  from goodsSave where clientId='" + clientId + "' and goodsId='" + goodsId + "'  ";
            sql += " insert into goodsSave(clientId,goodsId,makeDate) values('"+clientId+"','"+goodsId+"',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }


        public int SaveGoods(string openId, int goodsId)
        {
            string sql = " delete  from goodsSave where openId='" + openId + "' and goodsId='" + goodsId + "'  ";
            sql += " insert into goodsSave(openId,goodsId,makeDate) values('" + openId + "','" + goodsId + "',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        public int SaveGoods(string openId,int clientId, int goodsId)
        {
            string sql = " delete  from goodsSave where openId='" + openId + "' and goodsId='" + goodsId + "'  ";
            sql += " insert into goodsSave(openId,goodsId,makeDate) values('" + openId + "','" + goodsId + "',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }



        #endregion


        #region 查询收藏商品

        /// <summary>
        /// 查询收藏商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetSaveGoodsList(int clientId)
        {
            string sql = "select * from viewGoodsSave where clientId = '" + clientId + "'  order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 批量上下架商品

        /// <summary>
        /// 批量上下架商品
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateGoodsIsShow(int goodsId,int isShow)
        {
            string sql = " update goods  set isShow='" + isShow + "' where id='" + goodsId + "' ";
          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion


        #region 修改价格

        /// <summary>
        /// 修改价格
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int UpdateGoodsPrice(int goodsId, decimal price)
        {
            string sql = " update goods  set priceSalesRetail='" + price + "' where id='" + goodsId + "' ";


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }




        #endregion
    }
}
