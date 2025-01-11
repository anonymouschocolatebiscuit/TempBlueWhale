using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// kaoqinListWeixin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class kaoqinListWeixin
	{
		public kaoqinListWeixin()
		{}

        #region Model
        private int _id;
        private int? _shopid;
        private string _deptId;
        private string _deptName;

        private string _userid;
        private string _username;
        private string _groupname;
        private string _checkin_type;
        private string _exception_type;
        private DateTime? _checkin_time;
        private string _location_title;
        private string _location_detail;
        private string _wifiname;
        private string _notes;
        private string _wifimac;
        private string _mediaids;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? shopId
        {
            set { _shopid = value; }
            get { return _shopid; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string deptId
        {
            set { _deptId = value; }
            get { return _deptId; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string deptName
        {
            set { _deptName = value; }
            get { return _deptName; }
        }



        /// <summary>
        /// 
        /// </summary>
        public string userid
        {
            set { _userid = value; }
            get { return _userid; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }



        /// <summary>
        /// 
        /// </summary>
        public string groupname
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string checkin_type
        {
            set { _checkin_type = value; }
            get { return _checkin_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string exception_type
        {
            set { _exception_type = value; }
            get { return _exception_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? checkin_time
        {
            set { _checkin_time = value; }
            get { return _checkin_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string location_title
        {
            set { _location_title = value; }
            get { return _location_title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string location_detail
        {
            set { _location_detail = value; }
            get { return _location_detail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string wifiname
        {
            set { _wifiname = value; }
            get { return _wifiname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string wifimac
        {
            set { _wifimac = value; }
            get { return _wifimac; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mediaids
        {
            set { _mediaids = value; }
            get { return _mediaids; }
        }
        #endregion Model

	}
}

