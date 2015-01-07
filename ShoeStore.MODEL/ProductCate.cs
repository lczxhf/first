using System;

namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- ProductCate表映射类.
    /// 最后修改时间:2014-06-17 18:53:28
    /// </summary>
    public class ProductCate
    {
        public ProductCate()
        { }

        #region 字段们
        protected int _id;
        protected string _pName;
        protected int _pcSort;
        protected int _pId;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PName
        {
            set { _pName = value; }
            get { return _pName; }
        }
        public int PcSort
        {
            set { _pcSort = value; }
            get { return _pcSort; }
        }
        public int PId
        {
            set { _pId = value; }
            get { return _pId; }
        }
        #endregion
    }
}

