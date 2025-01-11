using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Model;

namespace Lanwei.Weixin.DAL
{
    public class RoleDAL
    {


        #region 成员字段



        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }


        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }


        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }


        #endregion


        #region  成员方法


        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(string names)
        {
            bool flag = false;

            string sql = "select * from roles where roleName='" + names + "' ";

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
        public bool isExistsNamesEdit(int id, string names)
        {
            bool flag = false;

            string sql = "select * from roles where roleName='" + names + "' and roleId<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }





        #endregion


        #region 判断角色是否存在
        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int CheckRole(string names)
        {
            string sql = "select roleId from Roles where roleName='" + names + "'";
            int i = 0;
            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (sdr.Read())
            {
                i = 1;

            }
            return i;
        }
        #endregion


        #region 获取所有角色信息
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<RoleInfo> GetAllRole(int parentId)
        {
            string sql = "select * from Roles where ParentId='" + parentId + "'";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, sql, null))
            {
                List<RoleInfo> list = new List<RoleInfo>();
                while (read.Read())
                {
                    RoleInfo s = new RoleInfo();

                    s.RoleId = read.GetInt32(0);
                    s.RoleName = read.GetString(1);
                    s.ParentId = read.GetInt32(2);
                    s.Flag = read.GetInt32(3);
                    list.Add(s);
                }
                return list;
            }
        }

        #endregion

        #region 获取所有角色信息
        /// <summary>
        /// 获取所有角色信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllRoleDataSet()
        {
            //string sql = "select * from roles";

            string sql = @"select a.*,isnull(num,0) num
                            from viewRoles a
                            left join
                            (
                            select roleId,count(*) num
                            from users
                            group by roleId
                            ) b
                            on a.roleId=b.roleId  order by flag ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region 查询角色信息---通过角色编号
        /// <summary>
        /// 查询角色信息---通过角色编号
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetRoleInfoByRoleId(int roleId)
        {
            string sql = "select * from Roles where roleId='" + roleId + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 新增角色信息
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public int AddRole(int parentId, string roleName,int flag)
        {
            string sql = "insert into roles(roleName,parentId,flag) values('" + roleName + "','" + parentId + "','"+flag+"')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 新增角色信息
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into roles(roleName,parentId,flag) values('" + RoleName + "','" + ParentId + "','" + Flag + "')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 修改角色信息
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int UpdateRole(int roleId, string roleName, int parentId,int flag)
        {
            string sql = "update roles set roleName='" + roleName + "',parentId='" + parentId + "',flag='" + flag + "'  where roleId='" + roleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 修改角色信息
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = "update roles set roleName='" + RoleName + "',parentId='" + ParentId + "',flag='" + Flag + "'  where roleId='" + RoleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 删除角色信息
        public int DeleteRole(int roleId)
        {
            string sql = "delete from roles where roleId='" + roleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 删除角色信息
        public int Delete(int roleId)
        {
            string sql = "delete from roles where roleId='" + roleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion
    }
}
