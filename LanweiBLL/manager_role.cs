using System;
using System.Data;
using System.Collections.Generic;
using LanweiCommon;

namespace LanweiBLL
{
    /// <summary>
    /// �����ɫ
    /// </summary>
    public partial class manager_role
    {
        private readonly LanweiModel.siteconfig siteConfig = new LanweiBLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly LanweiDAL.manager_role dal;
        public manager_role()
        {
            dal = new LanweiDAL.manager_role(siteConfig.sysdatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// ����Ƿ���Ȩ��
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
        /// ���ؽ�ɫ����
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(LanweiModel.manager_role model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(LanweiModel.manager_role model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public LanweiModel.manager_role GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        #endregion  Method
    }
}