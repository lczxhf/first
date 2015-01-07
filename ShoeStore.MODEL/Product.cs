using System;

namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- Product表映射类.
    /// 最后修改时间:2014-06-17 18:54:24
    /// </summary>
    public class Product
    {
        public Product()
        { }

        #region 字段们
        protected int _pId;
        protected string _pName;
        protected decimal _pPrice;
        protected string _pRemark;
        protected string _pSrc;
        protected int _pNum;
        protected bool _pIsrec;
        protected int _pCateid;
        protected ProductCate _pCateidMODEL;
        protected int _pSort;
        #endregion

        #region 属性们
        /// <summary>
        /// 产品表 id
        /// </summary>
        public int PId
        {
            set { _pId = value; }
            get { return _pId; }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string PName
        {
            set { _pName = value; }
            get { return _pName; }
        }

        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal PPrice
        {
            set { _pPrice = value; }
            get { return _pPrice; }
        }

        /// <summary>
        /// 产品介绍
        /// </summary>
        public string PRemark
        {
            set { _pRemark = value; }
            get { return _pRemark; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PSrc
        {
            set { _pSrc = value; }
            get { return _pSrc; }
        }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int PNum
        {
            set { _pNum = value; }
            get { return _pNum; }
        }

        /// <summary>
        /// 是否为推荐的商品
        /// </summary>
        public bool PIsrec
        {
            set { _pIsrec = value; }
            get { return _pIsrec; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PCateid
        {
            set { _pCateid = value; }
            get { return _pCateid; }
        }
        /// <summary>
        ///  -外键实体
        /// </summary>
        public ProductCate PCateidMODEL
        {
            set { _pCateidMODEL = value; }
            get { return _pCateidMODEL; }
        }

        public int PSort
        {
            set { _pSort = value; }
            get { return _pSort; }
        }
        #endregion
    }
}

