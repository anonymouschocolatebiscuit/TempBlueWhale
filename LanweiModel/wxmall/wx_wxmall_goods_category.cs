using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// 数据表6
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_goods_category
	{
        public wx_wxmall_goods_category()
		{}
		#region Model
		private int _id;
		private int? _shopid;
		private int _sortid;
		private string _categoryname;
        private string _picUrl;
		private int _isstart;
		private DateTime? _createdate;
		/// <summary>
		/// 编号
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}

        private int _wId;

        /// <summary>
        /// 商家id
        /// </summary>
        public int wId
        {
            set { _wId = value; }
            get { return _wId; }
        }

		/// <summary>
		/// 商家id
		/// </summary>
		public int? shopid
		{
			set{ _shopid=value;}
			get{return _shopid;}
		}


		/// <summary>
		/// 显示顺序
		/// </summary>
		public int sortid
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 主分类名称
		/// </summary>
		public string categoryName
		{
			set{ _categoryname=value;}
			get{return _categoryname;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string picUrl
		{
            set { _picUrl = value; }
            get { return _picUrl; }
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		public int isStart
		{
			set{ _isstart=value;}
			get{return _isstart;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

