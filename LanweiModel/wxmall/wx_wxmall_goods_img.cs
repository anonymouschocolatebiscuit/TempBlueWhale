
using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// 商品图片信息
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_goods_img
	{
        public wx_wxmall_goods_img()
		{}
		#region Model
        private long _id;
        private int _goodsId;
        private string _imgname;
        private string _imguri;
		/// <summary>
		/// 主键编号
		/// </summary>
		public long id
		{
			set{ _id=value;}
			get{return _id;}
		}
      

		/// <summary>
		/// 商品编号
		/// </summary>
        public int goodsId
		{
            set { _goodsId = value; }
            get { return _goodsId; }
		}
        /// <summary>
        /// 图片名称
        /// </summary>
        public string imgName
        {
            set { _imgname = value; }
            get { return _imgname; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string imgUri
        {
            set { _imguri = value; }
            get { return _imguri; }
        }
		#endregion Model

	}
}

