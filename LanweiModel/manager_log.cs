﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lanwei.Weixin.Model
{
    /// <summary>
    /// 管理日志:实体类
    /// </summary>
    [Serializable]
    public partial class manager_log
    {

        public manager_log()
		{}
		#region Model
		private int _id;
		private int _user_id;
		private string _user_name;
		private string _action_type;
		private string _remark;
		private string _user_ip;
		private DateTime _add_time= DateTime.Now;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public int user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string user_name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 操作类型
		/// </summary>
		public string action_type
		{
			set{ _action_type=value;}
			get{return _action_type;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 用户IP
		/// </summary>
		public string user_ip
		{
			set{ _user_ip=value;}
			get{return _user_ip;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime add_time
		{
			set{ _add_time=value;}
			get{return _add_time;}
		}
		#endregion Model
    }
}
