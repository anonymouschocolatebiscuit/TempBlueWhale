using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Senparc.Weixin;

using Senparc.Weixin.Work.Containers;

using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;



namespace LanweiUI.src
{
    public class CommonApiQY:BasePage
    {
      
        public CommonApiQY()
        {
            //全局只需注册一次
           // AccessTokenContainer.Register(qyInfo.CorpId, _corpSecret);
        }
    }
}