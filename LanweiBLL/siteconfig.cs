using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.Model;

namespace LanweiBLL
{
    public partial class siteconfig
    {
        private readonly Lanwei.Weixin.DAL.siteconfig dal = new Lanwei.Weixin.DAL.siteconfig();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Lanwei.Weixin.Model.siteconfig loadConfig()
        {
            Lanwei.Weixin.Model.siteconfig model = CacheHelper.Get<Lanwei.Weixin.Model.siteconfig>(MXKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(MXKeys.CACHE_SITE_CONFIG, dal.loadConfig(Utils.GetXmlMapPath(MXKeys.FILE_SITE_XML_CONFING)),
                    Utils.GetXmlMapPath(MXKeys.FILE_SITE_XML_CONFING));
                model = CacheHelper.Get<Lanwei.Weixin.Model.siteconfig>(MXKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Lanwei.Weixin.Model.siteconfig saveConifg(Lanwei.Weixin.Model.siteconfig model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(MXKeys.FILE_SITE_XML_CONFING));
        }

    }
}
