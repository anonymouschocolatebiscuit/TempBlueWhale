﻿using System;
using System.Data;
using System.Collections.Generic;
using LanweiCommon;

namespace LanweiBLL
{
    /// <summary>
    /// 管理日志
    /// </summary>
    public partial class manager_log
    {
        private readonly LanweiModel.siteconfig siteConfig = new LanweiBLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly LanweiDAL.manager_log dal;
        public manager_log()
        {
            dal = new LanweiDAL.manager_log(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法==============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加管理日志
        /// </summary>
        /// <param name="用户id"></param>
        /// <param name="用户名"></param>
        /// <param name="操作类型"></param>
        /// <param name="备注"></param>
        /// <returns></returns>
        public int Add(int user_id, string user_name, string action_type, string remark)
        {
            LanweiModel.manager_log manager_log_model = new LanweiModel.manager_log();
            manager_log_model.user_id = user_id;
            manager_log_model.user_name = user_name;
            manager_log_model.action_type = action_type;
            manager_log_model.remark = remark;
            manager_log_model.user_ip = MXRequest.GetIP();
            return dal.Add(manager_log_model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LanweiModel.manager_log model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LanweiModel.manager_log GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据用户名返回上一次登录记录
        /// </summary>
        public LanweiModel.manager_log GetModel(string user_name, int top_num, string action_type)
        {
            return dal.GetModel(user_name, top_num, action_type);
        }

        /// <summary>
        /// 删除7天前的日志数据
        /// </summary>
        public int Delete(int dayCount)
        {
            return dal.Delete(dayCount);
        }
                            
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion
    }
}
