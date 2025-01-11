using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Lanwei.Weixin.Common;

namespace LanweiBLL
{
    public partial class userconfig
    {
        private readonly Lanwei.Weixin.DAL.userconfig dal = new Lanwei.Weixin.DAL.userconfig();

        /// <summary>
        ///  读取用户配置文件
        /// </summary>
        public Lanwei.Weixin.Model.userconfig loadConfig()
        {
            Lanwei.Weixin.Model.userconfig model = CacheHelper.Get<Lanwei.Weixin.Model.userconfig>(MXKeys.CACHE_USER_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(MXKeys.CACHE_USER_CONFIG, dal.loadConfig(Utils.GetXmlMapPath(MXKeys.FILE_USER_XML_CONFING)),
                    Utils.GetXmlMapPath(MXKeys.FILE_USER_XML_CONFING));
                model = CacheHelper.Get<Lanwei.Weixin.Model.userconfig>(MXKeys.CACHE_USER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存用户配置文件
        /// </summary>
        public Lanwei.Weixin.Model.userconfig saveConifg(Lanwei.Weixin.Model.userconfig model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(MXKeys.FILE_USER_XML_CONFING));
        }

    }
}
