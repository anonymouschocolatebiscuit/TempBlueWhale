using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Model
{
    public class alibabaDDInfo
    {

        private int appId;
        public int AppId
        {
            get { return appId; }
            set { appId = value; }
        }



 
        private string appName;
        public string AppName
        {
            get { return appName; }
            set { appName = value; }
        }

 

        private string corpId;
        public string CorpId
        {
            get { return corpId; }
            set { corpId = value; }


        }

        private string corpSecret;
        public string CorpSecret
        {
            get { return corpSecret; }
            set { corpSecret = value; }


        }

   

    }
}
