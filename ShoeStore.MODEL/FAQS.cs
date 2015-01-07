using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- FAQS表映射类.
    /// 最后修改时间:2014-06-20 3:24:42
    /// </summary>
    public class FAQS
    {
        public FAQS()
        { }

        #region 字段们
        protected int _pId;
        protected string _question;
        protected string _answer;
        protected int _fSort;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int PId
        {
            set { _pId = value; }
            get { return _pId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FSort
        {
            set { _fSort = value; }
            get { return _fSort; }
        }
        #endregion
    }
}
