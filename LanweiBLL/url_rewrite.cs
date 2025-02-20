﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Lanwei.Weixin.Common;

namespace LanweiBLL
{
    public class url_rewrite
    {
        private readonly Lanwei.Weixin.DAL.url_rewrite dal = new Lanwei.Weixin.DAL.url_rewrite();

        #region 增、删、改操作=================================================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Lanwei.Weixin.Model.url_rewrite model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Lanwei.Weixin.Model.url_rewrite model)
        {
            return dal.Edit(model);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            return dal.Remove(attrName, attrValue);
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            return dal.Remove(xnList);
        }
        #endregion

        #region 扩展方法=====================================================

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            foreach (Lanwei.Weixin.Model.url_rewrite modelt in GetListAll())
            {
                if (modelt.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Lanwei.Weixin.Model.url_rewrite GetInfo(string attrValue)
        {
            return dal.GetInfo(attrValue);
        }

        /// <summary>
        /// 根据频道名称和类别返回一条URL映射
        /// </summary>
        public Lanwei.Weixin.Model.url_rewrite GetInfo(string channel, string attrType)
        {
            foreach (Lanwei.Weixin.Model.url_rewrite modelt in GetListAll())
            {
                if (channel != "" && channel != modelt.channel)
                {
                    continue;
                }
                if (attrType != "" && attrType != modelt.type)
                {
                    continue;
                }
                return modelt;
            }
            return null;
        }

        /// <summary>
        /// 返回URL映射列表
        /// </summary>
        public Hashtable GetList()
        {
            Hashtable ht = CacheHelper.Get<Hashtable>(MXKeys.CACHE_SITE_URLS);
            if (ht == null)
            {
                CacheHelper.Insert(MXKeys.CACHE_SITE_URLS, dal.GetList(), Utils.GetXmlMapPath(MXKeys.FILE_URL_XML_CONFING));
                ht = CacheHelper.Get<Hashtable>(MXKeys.CACHE_SITE_URLS);
            }
            return ht;
        }

        /// <summary>
        /// 返回URL映射List列表
        /// </summary>
        public List<Lanwei.Weixin.Model.url_rewrite> GetListAll()
        {
            List<Lanwei.Weixin.Model.url_rewrite> ls = CacheHelper.Get<List<Lanwei.Weixin.Model.url_rewrite>>(MXKeys.CACHE_SITE_URLS_LIST);
            if (ls == null)
            {
                CacheHelper.Insert(MXKeys.CACHE_SITE_URLS_LIST, dal.GetList(""), Utils.GetXmlMapPath(MXKeys.FILE_URL_XML_CONFING));
                ls = CacheHelper.Get<List<Lanwei.Weixin.Model.url_rewrite>>(MXKeys.CACHE_SITE_URLS_LIST);
            }
            return ls;
        }

        /// <summary>
        /// 根据频道名称返回URL映射列表
        /// </summary>
        public List<Lanwei.Weixin.Model.url_rewrite> GetList(string channel)
        {
            List<Lanwei.Weixin.Model.url_rewrite> ls = GetListAll();
            if (channel == "")
            {
                return ls;
            }
            List<Lanwei.Weixin.Model.url_rewrite> nls = new List<Lanwei.Weixin.Model.url_rewrite>();
            foreach (Lanwei.Weixin.Model.url_rewrite modelt in ls)
            {
                if (modelt.channel == channel)
                {
                    nls.Add(modelt);
                }
            }
            return nls;
        }

        /// <summary>
        /// 根据频道名称和类别返回URL映射列表
        /// </summary>
        public List<Lanwei.Weixin.Model.url_rewrite> GetList(string channel, string attrType)
        {
            List<Lanwei.Weixin.Model.url_rewrite> nls = new List<Lanwei.Weixin.Model.url_rewrite>();
            foreach (Lanwei.Weixin.Model.url_rewrite modelt in GetListAll())
            {
                if (channel != "" && channel != modelt.channel)
                {
                    continue;
                }
                if (attrType != "" && attrType != modelt.type)
                {
                    continue;
                }
                nls.Add(modelt);
            }
            return nls;
        }

        #endregion
    }
}
