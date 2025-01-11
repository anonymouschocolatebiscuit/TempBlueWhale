using System;
using System.Data;
using System.Collections.Generic;
using LanweiCommon;

namespace LanweiBLL
{
    /// <summary>
    /// 管理角色
    /// </summary>
    public partial class manager_role
    {
        private readonly LanweiModel.siteconfig siteConfig = new LanweiBLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly LanweiDAL.manager_role dal;
        public manager_role()
        {
            dal = new LanweiDAL.manager_role(siteConfig.sysdatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 检查是否有权限
        /// </summary>
        public bool Exists(int role_id, string nav_name, string action_type)
        {
            LanweiModel.manager_role model = dal.GetModel(role_id);
            if (model != null)
            {
                if (model.role_type == 1)
                {
                    return true;
                }
                LanweiModel.manager_role_value modelt = model.manager_role_values.Find(p => p.nav_name == nav_name && p.action_type == action_type);
                if (modelt != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LanweiModel.manager_role model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LanweiModel.manager_role model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LanweiModel.manager_role GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        #endregion  Method
    }
}