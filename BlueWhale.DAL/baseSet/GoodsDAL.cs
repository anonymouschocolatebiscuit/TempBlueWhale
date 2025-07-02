using BlueWhale.Common;
using BlueWhale.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class GoodsDAL
    {
        public GoodsDAL()
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

        #region Method

        /// <summary>
        /// Check code exists--When Add
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
        /// Check code exists--When Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id, int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from goods where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Check names exists--When Add
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
        /// Check shodId, code, barcode, names and spec is exists--When Add
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsAdd(int shopId, string code, string barcode, string names, string spec)
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
        /// Check shodId, code, barcode, names and spec is exists--When Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsEdit(int id, int shopId, string code, string barcode, string names, string spec)
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

        /// <summary>
        /// Check names is exists--When Edit
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

        #region Search Goods Detail

        /// <summary>
        /// Search Goods Detail
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="CodeName"></param>
        /// <returns></returns>
        public DataSet GetGoods(int shopId, int typeId, string CodeName)
        {
            string sql = "select * from viewGoods where shopId='" + shopId + "' ";

            if (typeId != 0)
            {
                sql += " and typeId='" + typeId + "'";
            }
            if (CodeName != "")
            {
                sql += " and (code like '%" + CodeName + "%' or names like '%" + CodeName + "%') ";

            }
            sql += " order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get ID By Name

        /// <summary>
        /// Get ID By Name
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId, string names)
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

        #region Get All Model

        /// <summary>
        /// Get All Model
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select *,code+' '+names CodeName from viewGoods order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// Get All Data List
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

        #region Get All Model-------View

        /// <summary>
        /// Get All Model-------View
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView()
        {
            string sql = "select * from viewGoods order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Model-------View

        /// <summary>
        /// Get All Model-------View
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

        #region Get All Model-------View

        /// <summary>
        /// Get All Model-------View
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

        #region Get Recommend Goods-------By Code

        /// <summary>
        /// Get Recommend Goods-------By Code
        /// </summary>
        /// <returns></returns>
        public DataSet GetOtherGoodsByCode(int shopId, string code)
        {
            string sql = "select top 5 * from viewGoods where code<>'" + code + "' and shopId='" + shopId + "' and isShow=1  ";

            sql += " and typeId=(select top 1 typeId from goods where code='" + code + "' and shopId='" + shopId + "' ) ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Model-------By Code

        /// <summary>
        /// Get Model-------By Code
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByCode()
        {
            string sql = "select * from viewGoods where code='" + Code + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Goods-------By ID

        /// <summary>
        /// Get Goods-------By ID
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelById(int id)
        {
            string sql = "select * from goods where id='" + id + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Goods Images-------By ID

        /// <summary>
        /// Get Goods Images-------By ID
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoodsImagesById(int id)
        {
            string sql = "select * from goodsImages where goodsId='" + id + "' order by moren desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Images
        /// <summary>
        /// Delete Images
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteImages(int id)
        {
            string sql = "delete from goodsImages where id='" + id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Display Default Images
        /// <summary>
        /// Display Default Images
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int SetMorenImages(int id, int goodsId)
        {

            string sql = " update goodsImages set moren=0 where goodsId='" + goodsId + "' update  goodsImages set moren=1 where id='" + id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Set Default Images

        /// <summary>
        /// Set Default Images
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int SetDefaultImages(int id, int goodsId)
        {
            string sql = " update goodsImages set moren=0 where goodsId='" + goodsId + "' update  goodsImages set moren=1 where id='" + id + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Goods Images

        /// <summary>
        /// Add Goods Images
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddGoodsImages(int goodsId, string imagePath, int defaultStatus)
        {
            string sql = "";
            if (defaultStatus == 1)
            {
                sql += " update goodsImages set moren=0 where goodsId='" + goodsId + "' ";
            }

            sql += " insert into  goodsImages(goodsId,imagesPath,moren) values('" + goodsId + "','" + imagePath + "','" + defaultStatus + "') ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Client Type Price

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

        #region Get All Client Type Price

        /// <summary>
        /// Get All Client Type Price--Return Dataset
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

        #region Delete Goods Price Client Type

        /// <summary>
        /// Delete Goods Price Client Type
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public int DeleteGoodsPriceClientType(int goodsId)
        {
            string sql = "delete from GoodsPriceClientType where goodsId='" + goodsId + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Goods Number When Start
        /// <summary>
        /// Add Goods Number When Start
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

        #region Get Goods Number When Start-------By goodsId

        /// <summary>
        /// Get Goods Number When Start-------By goodsId
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoodsNumStartById(int goodsId)
        {
            string sql = "select a.*,ck.names ckName from GoodsNumStart a left join cangku ck on a.ckId=ck.id  where goodsId='" + goodsId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Goods Number When Start

        /// <summary>
        /// Delete Goods Number When Start
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteGoodsNumStart(int Id)
        {
            string sql = "delete from goodsNumStart where id='" + Id + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Goods

        /// <summary>
        /// Add Goods
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";
            string sql = @"insert into goods
                            (
                                shopId
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
                            (
                                @shopId
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
                            )
                            select @@identity";

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

        #region Edit Goods

        /// <summary>
        /// Edit Goods
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = "";

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

        #region Edit------Goods Remarks

        /// <summary>
        /// Edit------Goods Remarks
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int UpdateRemarks(int id, string remarks)
        {
            string sql = "";
            sql = "update goods set  Remarks='" + remarks + "'  where Id='" + id + "'";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Goods

        /// <summary>
        /// Delete Goods
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = " delete from goods where id='" + Id + "' delete from goodsNumStart where goodsId='" + Id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Goods Store Number

        /// <summary>
        /// Get Goods Store Number
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNum(DateTime start, DateTime end, int goodsId, int ckId)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,
                            sum(numOut) sumNumOut,
                            sum(numIn-numOut) sumNum,
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,                             
                            sum(sumPriceIn) sumPriceIn,
                            sum(sumPriceOut) sumPriceOut
                            from viewGoodsOutInFlow where bizDate<=@end ";

            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }


            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";

            sql += "union all ";

            sql += @"select '' goodsId,code,goodsName+'-Subtotal：' goodsName,'' spec,'' unitName,'' ckId,'All Inventory' ckName,
                    sum(numIn) sumNumIn,
                    sum(numOut) sumNumOut,
                    sum(numIn-numOut) sumNum,
                    case sum(numIn) when 0 then 0
                    else
                    sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,   
                    sum(sumPriceIn) sumPriceIn,
                    sum(sumPriceOut) sumPriceOut
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

            sql += @"group by goodsId,code,goodsName,spec,unitName 
                     order by code,goodsName";

            SqlParameter[] param = {
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Goods Store Number-----------NewUI

        /// <summary>
        /// Get Goods Store Number-----------NewUI
        /// </summary>       
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNum(int shopId, DateTime end, string code, string ckName)
        {
            string sqlGoods = @" select a.Id goodsId,a.code,a.Names goodsName,a.spec,a.unitName,a.remarks,b.ckId,b.ckName,a.priceCost,
                                fieldA,fieldB,fieldC,fieldD ";

            sqlGoods += ",sumNumIn,sumNumOut,sumNum,sumPriceNow,sumPriceIn,sumPriceOut,a.priceCost,a.priceCost*sumNum sumPriceStore ";

            sqlGoods += " from viewGoods a left join (";

            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,
                            sum(numOut) sumNumOut,
                            sum(numIn-numOut) sumNum,
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,                      
                            sum(sumPriceIn) sumPriceIn,
                            sum(sumPriceOut) sumPriceOut
                            from viewGoodsOutInFlow where bizDate<=@end";

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

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";

            sqlGoods += sql;

            sqlGoods += " ) b  on a.id=b.goodsId ";

            sqlGoods += " where a.shopId='" + shopId + "' ";

            if (code != "")
            {
                sqlGoods += " and a.code in(" + code + ")   ";
            }

            SqlParameter[] param = {
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sqlGoods, param);
        }

        #endregion

        #region Get Goods Store Number-----------NewUI

        /// <summary>
        /// Get Goods Store Number-----------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNum(string key)
        {
            string sql = @"select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(numIn) sumNumIn,
                            sum(numOut) sumNumOut,
                            sum(numIn-numOut) sumNum,
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,                             
                            sum(sumPriceIn) sumPriceIn,
                            sum(sumPriceOut) sumPriceOut
                            from viewGoodsOutInFlow where 1=1 ";

            if (key != "")
            {
                sql += " and goodsName like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName ";

            sql += "union all ";

            sql += @"select '' goodsId,code,goodsName+'-Subtotal：' goodsName,'' spec,'' unitName,'' ckId,'All Invetory' ckName,
                            sum(numIn) sumNumIn,
                            sum(numOut) sumNumOut,
                            sum(numIn-numOut) sumNum,
                            case sum(numIn) when 0 then 0
                            else
                            sum(sumPriceIn)-sum(NumOut)*(sum(sumPriceIn)/sum(numIn)) end sumPriceNow,   
                            sum(sumPriceIn) sumPriceIn,
                            sum(sumPriceOut) sumPriceOut
                        from viewGoodsOutInFlow
                        where 1=1  ";

            if (key != "")
            {
                sql += " and goodsName like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%' ";
            }

            sql += @"group by goodsId,code,goodsName,spec,unitName 
                     order by code,goodsName";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Model View And Number

        /// <summary>
        /// Get All Model View And Number
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

        #region Get All Model View And Number Keys

        /// <summary>
        /// Get All Model View And Number Keys
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

        #region Get Goods Store Number Now

        /// <summary>
        /// Get Goods Store Number Now
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="ckId"></param>
        /// <param name="goodsName"></param>
        /// <param name="zero"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNumNow(int typeId, int ckId, string goodsName, int zero)
        {
            string sql = "select ckId,ckName,goodsId,code,goodsName,unitName,spec,sum(num) sumNum from viewGoodsStoreNum where 1=1 ";

            if (typeId != 0)
            {
                sql += " and typeId='" + typeId + "' ";
            }
            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "' ";
            }

            if (goodsName != "")
            {
                sql += " and goodsName like '%" + goodsName + "%' ";
            }

            sql += "group by ckId,ckName,goodsId,code,goodsName,unitName,spec ";

            if (zero != 0)
            {
                sql += " having sum(Num)=0 ";
            }

            sql += "  order by ckId,code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Goods Store Number Now----------NewUI

        /// <summary>
        /// Get Goods Store Number Now----------NewUI
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="ckId"></param>
        /// <param name="goodsName"></param>
        /// <param name="zero"></param>
        /// <returns></returns>
        public DataSet GetGoodsStoreNumNow(int shopId, string typeId, string ckId, string code)
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

        #region Get Goods Store Number Now

        /// <summary>
        /// Get Goods Store Number Now
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public decimal GetGoodsStoreNumNow(int goodsId, int ckId)
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

        #region Get Goods Out In Detail Report Back

        /// <summary>
        /// Get Goods Out In Detail Report Back
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInDetailReportBack(DateTime start, DateTime end, int goodsId, int ckId)
        {
            string sql = @"select wlName,'NumStart' bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
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

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Goods Out In Detail Report

        /// <summary>
        /// Get Goods Out In Detail Report
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInDetailReport(DateTime start, DateTime end, int goodsId, int ckId)
        {
            string sql = @"select isnull(wlName,'') wlName,'NumStart' bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
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

        #region Get Goods Balance Detail

        /// <summary>
        /// Get Goods Balance Detail
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsBalanceDetail(DateTime start, DateTime end, int goodsId, int ckId)
        {
            //sum(isnull(b.sumPriceIn,0)-isnull(b.sumPriceOut,0)) sumPriceEnd
            string sql = @"select '1' seq,'' wlName,'NumStart' bizType,'' number,'' bizDate,id goodsId,names goodsName,g.typeId,g.typeName,g.spec,g.code,g.unitName,
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
                            sum(isnull(b.numIn,0)-isnull(b.numOut,0)) numEnd,
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
                            end priceEnd,    --0 priceEnd,
                            case sum(numIn) when 0 then 0 
                            else         
                            sum(sumPriceIn)-sum(NumOut)*(case sum(numIn) when 0 then 0 else  sum(sumPriceIn)/sum(numIn)  end ) end sumPriceEnd
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

            sql += @" group by  id,g.code,g.spec,names,g.typeId,g.typeName,g.unitName,
                    b.ckId,
                    b.ckName";

            sql += " union all ";

            sql += @"select '2' seq,isnull(wlName,'') wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                    0 numBegin,
                    0 priceBegin,
                    0 sumPriceBegin,
                    numIn,
                    priceIn,
                    sumPriceIn,
                    numOut,
                    priceOut,
                    sumPriceOut,
                    0 numEnd,
                    0 priceEnd,
                    0 sumPriceEnd
                    from viewGoodsOutInFlow 
                    where bizDate>=@start and bizDate<=@end  and bizType<>'NumStart'";

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

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Goods Balance Detail--------NewUI

        /// <summary>
        /// Get Goods Balance Detail--------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsBalanceDetail(int shopId, DateTime start, DateTime end, string code, string ckName)
        {
            //sum(isnull(b.sumPriceIn,0)-isnull(b.sumPriceOut,0)) sumPriceEnd
            string sql = @"select '1' seq,'' wlName,'Opening Balance' bizType,'' number,'' bizDate,id goodsId,names goodsName,g.typeId,g.typeName,g.spec,g.code,g.unitName,
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
                                        sum(isnull(b.numIn,0)-isnull(b.numOut,0)) numEnd,
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
                                        end priceEnd,    --0 priceEnd,
                                        case sum(numIn) when 0 then 0 
                                        else      
                                        sum(sumPriceIn)-sum(NumOut)*(case sum(numIn) when 0 then 0 else  sum(sumPriceIn)/sum(numIn)  end ) end sumPriceEnd
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

            sql += @" group by  id,g.code,g.spec,names,g.typeId,g.typeName,g.unitName,
                    b.ckId,
                    b.ckName";

            sql += " union all ";

            sql += @"select '2' seq,isnull(wlName,'') wlName,bizType,number,bizDate,goodsId,goodsName,typeId,typeName,spec,code,unitName,ckId,ckName,
                    0 numBegin,
                    0 priceBegin,
                    0 sumPriceBegin,
                    numIn,
                    priceIn,
                    sumPriceIn,
                    numOut,
                    priceOut,
                    sumPriceOut,
                    0 numEnd,
                    0 priceEnd,
                    0 sumPriceEnd
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

        #region Get Goods Out In Sum Report Back

        /// <summary>
        /// Get Goods Out In Sum Report Back
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInSumReportBack(DateTime start, DateTime end, int goodsId, int ckId)
        {

            string sql = @"select fs.goodsId,fs.code,fs.goodsName,fs.spec,fs.unitName,fs.ckId,fs.ckName,
                            qc.numBegin,
                            max(case bizType when 'Transfer Inbound' then numIn else 0 end) numInDB,--1
                            max(case bizType when 'Purchase Inbound' then numIn else 0 end) numInPur,--2
                            max(case bizType when 'Purchase Return"" or ""Purchase Refund"", depending on the context.' then numIn else 0 end) numInTH,--3
                            max(case bizType when 'Other Inbound' then numIn else 0 end) numInQT,--4
                            max(case bizType when 'Inventory Gain Inbound' then numIn else 0 end) numInPY,--5
                            max(case types when 1 then numIn else 0 end) numInSum, --6

                            ----Upper is Inbound，Below is Outbound

                            max(case bizType when 'Transfer Outbound' then numOut else 0 end) numOutDB,--1
                            max(case bizType when 'Sales Outbound' then numOut else 0 end) numOutSales,--2
                            max(case bizType when 'Sales Return' then numOut else 0 end) numOutTH,--3
                            max(case bizType when 'Other Outbound' then numOut else 0 end) numOutQT,--4
                            max(case bizType when 'Inventory Loss Outbound' then numOut else 0 end) numOutPK,--5
                            max(case types  when -1 then numOut else 0 end) numOutSum,--6

                            qc.numBegin+max(case types when 1 then numIn else 0 end)-max(case types  when -1 then numOut else 0 end) numEnd

                            from viewGoodsOutInFlowSum fs
                            left join 
                            (
                                select goodsId,ckId,
                                sum(numIn-numOut) numBegin
                                from viewGoodsOutInFlowSum                           
                                where bizDate<@start ";
            if (goodsId != 0)
            {
                sql += " and goodsId='" + goodsId + "'";
            }

            if (ckId != 0)
            {
                sql += " and ckId='" + ckId + "'";
            }

            sql += @"group by goodsId,ckId
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

            sql += "group by fs.goodsId,fs.code,fs.goodsName,fs.spec,fs.unitName,fs.ckId,fs.ckName,qc.numBegin ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end),
                new SqlParameter("@goodsId",goodsId),
                new SqlParameter("@ckId",ckId)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Goods Out In Sum Report

        /// <summary>
        /// Get Goods Out In Sum Report
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInSumReport(DateTime start, DateTime end, int goodsId, int ckId)
        {
            string sql = @"select g.goodsId,g.code,g.goodsName,g.spec,g.unitName,g.ckId,g.ckName,
                                    sumNumBegin,sumPriceBegin,
                                    isnull(sumNumIn,0) sumNumIn,isnull(sumPriceIn,0) sumPriceIn,
                                    isnull(sumNumOut,0) sumNumOut,isnull(sumPriceOut,0) sumPriceOut,
                                    sumNumInAll,sumPriceInAll,
                                    sumNumOutAll,sumPriceOutAll,
                                    isnull(sumNumBegin,0)+isnull(sumNumIn,0)-isnull(sumNumOut,0) sumNumEnd,
                                    sumPriceBegin+isnull(sumPriceIn,0)-isnull(sumPriceOut,0) sumPriceEnd
                                    from (select distinct goodsId,code,goodsName,spec,unitName,ckId,ckName from viewGoodsOutInFlow) g
                                    left join
                                    (
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

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                    ) qc on g.goodsId=qc.goodsId and g.ckId=qc.ckId
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

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                    )
                    fs on g.goodsId=fs.goodsId and g.ckId=fs.ckId
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

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName
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

            sql += @"order by g.code,g.ckId";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end),
                new SqlParameter("@goodsId",goodsId),
                new SqlParameter("@ckId",ckId)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Get Goods Out In Sum Report-------------NewUI

        /// <summary>
        /// Get Goods Out In Sum Report-------------NewUI
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="goodsId"></param>
        /// <param name="ckId"></param>
        /// <returns></returns>
        public DataSet GetGoodsOutInSumReport(int shopId, DateTime start, DateTime end, string code, string ckName)
        {

            string sql = @"select g.goodsId,g.code,g.goodsName,g.spec,g.unitName,g.ckId,g.ckName,
                            sumNumBegin,sumPriceBegin,
                            isnull(sumNumIn,0) sumNumIn,isnull(sumPriceIn,0) sumPriceIn,
                            isnull(sumNumOut,0) sumNumOut,isnull(sumPriceOut,0) sumPriceOut,
                            sumNumInAll,sumPriceInAll,
                            sumNumOutAll,sumPriceOutAll,
                            isnull(sumNumBegin,0)+isnull(sumNumIn,0)-isnull(sumNumOut,0) sumNumEnd,
                            sumPriceBegin+isnull(sumPriceIn,0)-isnull(sumPriceOut,0) sumPriceEnd
                            from (select distinct shopId,goodsId,code,goodsName,spec,unitName,ckId,ckName from viewGoodsOutInFlow) g
                            left join
                            (
                            select goodsId,code,goodsName,spec,unitName,ckId,ckName,
                            sum(NumIn-NumOut) as sumNumBegin,
                            0 priceBegin,
                            sum(sumPriceIn-sumPriceOut) as sumPriceBegin
                            from viewGoodsOutInFlow
                            where bizDate<@start";

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

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                     ) qc on g.goodsId=qc.goodsId and g.ckId=qc.ckId
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

            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName
                    )
                    fs on g.goodsId=fs.goodsId and g.ckId=fs.ckId
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


            sql += @"group by goodsId,code,goodsName,spec,unitName,ckId,ckName
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

            sql += @"order by g.code,g.ckId ";

            SqlParameter[] param = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        //------------------------------------------Below is Online Order

        #region Get Goods Info
        /// <summary>
        /// Get Goods Info
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

        #region Get Goods Info

        /// <summary>
        /// Get Goods Info
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetGoodsInfo(string keys)
        {
            string sql = "select * from viewgoods where names like '%" + keys + "%' or code like '%" + keys + "%'  order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Setting Goods--------Delete

        /// <summary>
        /// Setting Goods--------Delete
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public int DeleteGoodsShow(int goodsId)
        {
            string sql = " delete from goodsShow where goodsId='" + goodsId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Setting Goods

        /// <summary>
        /// Setting Goods
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="tj"></param>
        /// <param name="xp"></param>
        /// <param name="cx"></param>
        /// <returns></returns>
        public int SetGoodsShow(int goodsId, int tj, int xp, int cx)
        {
            string sql = " delete from goodsShow where goodsId='" + goodsId + "' ";
            sql += " insert into goodsShow(goodsId,tj,xp,cx) values('" + goodsId + "','" + tj + "','" + xp + "','" + cx + "') ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Setting Goods

        /// <summary>
        /// Setting Goods
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="tj"></param>
        /// <param name="xp"></param>
        /// <param name="cx"></param>
        /// <returns></returns>
        public int SetGoodsShow(int goodsId, string cloumn, int selectYes)
        {
            string sql = "if not exists( select * from goodsShow where goodsId='" + goodsId + "') ";

            sql += " begin ";

            sql += " insert into goodsShow(goodsId,tj,xp,cx) values('" + goodsId + "',0,0,0) ";

            sql += " end ";

            sql += " update goodsShow set " + cloumn + "='" + selectYes + "'  where goodsId='" + goodsId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Model View--With SP

        /// <summary>
        /// Get All Model View--With SP
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataSet GetAllModelView(int clientTypeId, int typeId, string keys)
        {
            SqlParameter[] param ={
                new SqlParameter("@clientTypeId",clientTypeId),
                new SqlParameter("@typeId",typeId),
                new SqlParameter("@keys",keys)
            };

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "procGetGoodsPrice", param);
        }

        #endregion

        #region Get Goods Detail By Client Type Id And Gods Id-------With clientTypeId and goodsId

        /// <summary>
        /// Get Goods Detail By Client Type Id And Gods Id-------With clientTypeId and goodsId
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

        #region Get All Model View Base Set-----------NewUI

        /// <summary>
        /// Get All Model View Base Set-----------NewUI
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelViewBaseSet(int typeId, string key, int isShow)
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

        #region Get All Model View

        /// <summary>
        /// Get All Model View
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

        #region Get All Model View----------NewUI

        /// <summary>
        /// Get All Model View----------NewUI
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

        #region Get All Data Set

        /// <summary>
        /// Get All Data Set
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

            DataSet ds = SQLHelper.SqlDataAdapter(Pager1, Pager2, SQLHelper.ConStr, CommandType.Text, sql, null);

            return ds;
        }

        #endregion

        /// <summary>
        /// Get ALL Data Row Count
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

        #region Get All Model View

        /// <summary>
        /// Get All Model View
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

        #region Get All Goods ByClient Type Id

        /// <summary>
        /// Get All Goods ByClient Type Id
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

        #region Top Goods List

        /// <summary>
        /// Top Goods List
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public DataSet TopGoodsList(string orderBy, string desc)
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

            sql += " order by  " + orderBy + "   " + desc;

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Save Goods

        public int SaveGoods(int clientId, int goodsId)
        {
            string sql = " delete  from goodsSave where clientId='" + clientId + "' and goodsId='" + goodsId + "'  ";
            sql += " insert into goodsSave(clientId,goodsId,makeDate) values('" + clientId + "','" + goodsId + "',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int SaveGoods(string openId, int goodsId)
        {
            string sql = " delete  from goodsSave where openId='" + openId + "' and goodsId='" + goodsId + "'  ";
            sql += " insert into goodsSave(openId,goodsId,makeDate) values('" + openId + "','" + goodsId + "',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int SaveGoods(string openId, int clientId, int goodsId)
        {
            string sql = " delete  from goodsSave where openId='" + openId + "' and goodsId='" + goodsId + "'  ";
            sql += " insert into goodsSave(openId,goodsId,makeDate) values('" + openId + "','" + goodsId + "',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Save Goods List

        /// <summary>
        /// Get Save Goods List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetSaveGoodsList(int clientId)
        {
            string sql = "select * from viewGoodsSave where clientId = '" + clientId + "'  order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update Goods Is Show

        /// <summary>
        /// Update Goods Is Show
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateGoodsIsShow(int goodsId, int isShow)
        {
            string sql = " update goods  set isShow='" + isShow + "' where id='" + goodsId + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update Goods Price

        /// <summary>
        /// Update Goods Price
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