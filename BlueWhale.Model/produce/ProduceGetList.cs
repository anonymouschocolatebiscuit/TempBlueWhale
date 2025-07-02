using System;

namespace BlueWhale.Model.produce
{
    /// <summary>
    /// produceGetList
    /// </summary>
    [Serializable]
    public partial class ProduceGetList
    {
        public ProduceGetList()
        { }
        #region Model
        private int _id;
        private int? _shopid;
        private string _number;
        private int? _deptid;
        private string _plannumber;
        private int? _goodsid;
        private decimal? _num;
        private int? _makeid;
        private DateTime? _makedate;
        private int? _bizid;
        private DateTime? _bizdate;
        private int? _checkid;
        private DateTime? _checkdate;
        private string _remarks;
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
        public string number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? deptId
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string planNumber
        {
            set { _plannumber = value; }
            get { return _plannumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? goodsId
        {
            set { _goodsid = value; }
            get { return _goodsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? num
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? makeId
        {
            set { _makeid = value; }
            get { return _makeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? makeDate
        {
            set { _makedate = value; }
            get { return _makedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bizId
        {
            set { _bizid = value; }
            get { return _bizid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? bizDate
        {
            set { _bizdate = value; }
            get { return _bizdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? checkId
        {
            set { _checkid = value; }
            get { return _checkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? checkDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }

        private string _flag;

        /// <summary>
        /// 
        /// </summary>
        public string flag
        {
            set { _flag = value; }
            get { return _flag; }
        }

        #endregion Model
    }
}
