using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- TransactionLog表映射类.
    /// 最后修改时间:2014-06-17 19:58:16
    /// </summary>
    public class TransactionLog
    {
        public TransactionLog()
        { }

        #region 字段们
        protected int _id;
        protected int _pId;
        protected int _userId;
        protected Users _userModel;
        protected Product _pIdMODEL;
        protected int _pCount;
        protected DateTime _pTime;
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
        public int UserId
        {
            set { _userId = value; }
            get { return _userId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PId
        {
            set { _pId = value; }
            get { return _pId; }
        }
        /// <summary>
        ///  -外键实体
        /// </summary>
        public Product PIdMODEL
        {
            set { _pIdMODEL = value; }
            get { return _pIdMODEL; }
        }


        public Users UserMODEL
        {
            set { _userModel = value; }
            get { return _userModel;  }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PCount
        {
            set { _pCount = value; }
            get { return _pCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PTime
        {
            set { _pTime = value; }
            get { return _pTime; }
        }
        #endregion
    }
}
