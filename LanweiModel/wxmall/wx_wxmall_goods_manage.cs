using System;
using System.Collections.Generic;

namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wx_wxmall_goods_manage:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_goods_manage
	{
		public wx_wxmall_goods_manage()
		{}
		#region Model
		private int _id;
		private int? _typeid;
		private string _namescn;
		private string _namesen;
		private string _capacity;
		private string _grade;
		private string _winery;
		private string _place;
		private string _varieties;
		private string _years;
		private string _degree;
		private string _temperature;
		private string _remarks;
		private int? _num;
		private decimal? _costprice;
		private decimal? _showprice;
		private decimal? _salesprice;
		private int? _sort;
		private string _showimgs;
		private int? _status;
		private int? _ishot;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? typeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}


        public int? wId{  set;  get; }
        public int? shopId { set; get; }


		/// <summary>
		/// 
		/// </summary>
		public string namesCN
		{
			set{ _namescn=value;}
			get{return _namescn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string namesEN
		{
			set{ _namesen=value;}
			get{return _namesen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string capacity
		{
			set{ _capacity=value;}
			get{return _capacity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string winery
		{
			set{ _winery=value;}
			get{return _winery;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string place
		{
			set{ _place=value;}
			get{return _place;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string varieties
		{
			set{ _varieties=value;}
			get{return _varieties;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string years
		{
			set{ _years=value;}
			get{return _years;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string degree
		{
			set{ _degree=value;}
			get{return _degree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string temperature
		{
			set{ _temperature=value;}
			get{return _temperature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? costPrice
		{
			set{ _costprice=value;}
			get{return _costprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? showPrice
		{
			set{ _showprice=value;}
			get{return _showprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? salesPrice
		{
			set{ _salesprice=value;}
			get{return _salesprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string showImgs
		{
			set{ _showimgs=value;}
			get{return _showimgs;}
		}



        private List<Model.wx_wxmall_goods_img> _goodsimgs;

        /// <summary>
        /// 图片相册
        /// </summary>
        public List<Model.wx_wxmall_goods_img> goodsImgs
        {
            set { _goodsimgs = value; }
            get { return _goodsimgs; }
        }


		/// <summary>
		/// 
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ishot
		{
			set{ _ishot=value;}
			get{return _ishot;}
		}
		#endregion Model

	}
}

