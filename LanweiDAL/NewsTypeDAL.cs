using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class NewsTypeDAL
    {
        public NewsTypeDAL()
        {

        }
        #region 成员字段

    

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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
        public bool isExistsNamesAdd(int parentId,string names)
        {
            bool flag = false;

            string sql = "select * from NewsType where names='" + names + "' and parentId='"+parentId+"' ";

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
        public bool isExistsNamesEdit(int id,int parentId, string names)
        {
            bool flag = false;

            string sql = "select * from NewsType where names='" + names + "' and id<>'" + id + "' and parentId='"+parentId+"' ";

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
        public int GetIdByName(string names)
        {
            int id = 0;
            string sql = "select * from CGCaseType where names='" + names + "' ";

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr,CommandType.Text,sql,null);
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
            string sql = "select * from NewsType order by seq  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into NewsType(names,parentId,seq) values('" + Names + "','" + ParentId + "','" + Seq + "')";

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
            string sql="";

            if (!this.isExistsNamesEdit(Id,ParentId, Names))
            {
                sql = "update NewsType set Names='" + Names + "',ParentId='" + ParentId + "',Seq='" + Seq + "' where Id='" + Id + "'";

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
            string sql = "delete from NewsType where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 获取所有类别信息 填充到TreeView
        /// <summary>
        /// 获取所有类别信息--填充到TreeView
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<NewsTypeInfo> GetAlLGoosTypesTreeView(int parentId)
        {
            string sql = "select * from NewsType where parentId='" + parentId + "' order by seq ";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, sql, null))
            {
                List<NewsTypeInfo> list = new List<NewsTypeInfo>();
                while (read.Read())
                {
                    NewsTypeInfo s = new NewsTypeInfo();

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
            string sql = "select * from NewsType where Id='" + Id + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
