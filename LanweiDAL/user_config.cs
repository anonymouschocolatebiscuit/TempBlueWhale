using System;
using System.Collections.Generic;
using System.Text;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
    /// <summary>
    /// 数据访问类:会员配置
    /// </summary>
    public partial class userconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Lanwei.Weixin.Model.userconfig loadConfig(string configFilePath)
        {
            return (Lanwei.Weixin.Model.userconfig)SerializationHelper.Load(typeof(Lanwei.Weixin.Model.userconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Lanwei.Weixin.Model.userconfig saveConifg(Lanwei.Weixin.Model.userconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}
