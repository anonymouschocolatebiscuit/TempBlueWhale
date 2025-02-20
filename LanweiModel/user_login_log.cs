﻿using System;
namespace Lanwei.Weixin.Model
{
    /// <summary>
    /// 用户登录日志
    /// </summary>
    [Serializable]
    public partial class user_login_log
    {
        public user_login_log()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private string _user_name = "";
        private string _remark = "";
        private DateTime _login_time = DateTime.Now;
        private string _login_ip = "";
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime login_time
        {
            set { _login_time = value; }
            get { return _login_time; }
        }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string login_ip
        {
            set { _login_ip = value; }
            get { return _login_ip; }
        }
        #endregion Model

    }
}