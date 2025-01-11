using System;
using System.Collections.Generic;
using System.Text;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.DAL
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public partial class siteconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Lanwei.Weixin.Model.siteconfig loadConfig(string configFilePath)
        {
            return (Lanwei.Weixin.Model.siteconfig)SerializationHelper.Load(typeof(Lanwei.Weixin.Model.siteconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Lanwei.Weixin.Model.siteconfig saveConifg(Lanwei.Weixin.Model.siteconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}
