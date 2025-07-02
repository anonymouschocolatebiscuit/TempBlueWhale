using BlueWhale.DBUtility;
using BlueWhale.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class RoleDAL
    {
        #region Member Fields

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

        #region Member Methods

        /// <summary>
        /// Check if role name exists when adding
        /// </summary>
        /// <param name="names"></param>
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
        /// Check if role name exists when editing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="names"></param>
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

        #region Check Role Existence

        /// <summary>
        /// Check if role exists
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

        #region Get All Roles

        /// <summary>
        /// Get all role information based on ParentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<RoleInfo> GetAllRole(int parentId)
        {
            string sql = "select * from Roles where ParentId='" + parentId + "'";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null))
            {
                List<RoleInfo> list = new List<RoleInfo>();
                while (read.Read())
                {
                    RoleInfo s = new RoleInfo
                    {
                        RoleId = read.GetInt32(0),
                        RoleName = read.GetString(1),
                        ParentId = read.GetInt32(2),
                        Flag = read.GetInt32(3)
                    };
                    list.Add(s);
                }
                return list;
            }
        }

        #endregion

        #region Get All Roles (DataSet)

        /// <summary>
        /// Get all role information as DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllRoleDataSet()
        {
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

        #region Get Role Info By RoleId

        /// <summary>
        /// Get role information by roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetRoleInfoByRoleId(int roleId)
        {
            string sql = "select * from Roles where roleId='" + roleId + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add New Role

        /// <summary>
        /// Add new role information
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public int AddRole(int parentId, string roleName, int flag)
        {
            string sql = "insert into roles(roleName,parentId,flag) values('" + roleName + "','" + parentId + "','" + flag + "')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add New Role (Using Member Fields)

        /// <summary>
        /// Add new role information using member fields
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into roles(roleName,parentId,flag) values('" + RoleName + "','" + ParentId + "','" + Flag + "')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update Role Information

        /// <summary>
        /// Update role information
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="parentId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateRole(int roleId, string roleName, int parentId, int flag)
        {
            string sql = "update roles set roleName='" + roleName + "',parentId='" + parentId + "',flag='" + flag + "'  where roleId='" + roleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Update Role Information (Using Member Fields)

        /// <summary>
        /// Update role information using member fields
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = "update roles set roleName='" + RoleName + "',parentId='" + ParentId + "',flag='" + Flag + "'  where roleId='" + RoleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Role

        /// <summary>
        /// Delete role information by roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DeleteRole(int roleId)
        {
            string sql = "delete from roles where roleId='" + roleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Role (Using Member Fields)

        /// <summary>
        /// Delete role information using member fields
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int Delete(int roleId)
        {
            string sql = "delete from roles where roleId='" + roleId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
