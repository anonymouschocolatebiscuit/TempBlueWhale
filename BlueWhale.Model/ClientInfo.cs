namespace BlueWhale.Model
{
    public class ClientInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }


        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }


        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string pwd;
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }


        private int typeId;
        public int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        private decimal dis;
        public decimal Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private string openId;
        public string OpenId
        {
            get { return openId; }
            set { openId = value; }
        }

    }
}
