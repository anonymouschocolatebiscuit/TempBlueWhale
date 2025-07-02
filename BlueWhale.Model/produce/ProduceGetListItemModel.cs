using System;

namespace BlueWhale.Model.produce
{
    /// <summary>
    /// produceGetListItem
    /// </summary>
    [Serializable]
    public partial class ProduceGetListItemModel
    {
        public ProduceGetListItemModel()
        { }
        #region Model
        private int _id;
        private int? _pid;
        private int? _goodsid;
        private int? _ckid;
        private string _pihao;
        private decimal? _numapply;
        private decimal? _num;
        private decimal? _price;
        private decimal? _sumprice;
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
        public int? pId
        {
            set { _pid = value; }
            get { return _pid; }
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
        public int? ckId
        {
            set { _ckid = value; }
            get { return _ckid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pihao
        {
            set { _pihao = value; }
            get { return _pihao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? numApply
        {
            set { _numapply = value; }
            get { return _numapply; }
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
        public decimal? price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? sumPrice
        {
            set { _sumprice = value; }
            get { return _sumprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model
    }
}
