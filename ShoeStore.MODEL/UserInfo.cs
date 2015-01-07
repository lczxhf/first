using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- UserInfo表映射类.
    /// 最后修改时间:2014-06-22 1:40:18
    /// </summary>
    public class UserInfo
    {
        public UserInfo()
        { }

        #region 字段们
        protected int _iId;
        protected int _userId;
        protected Users _userMODEL;
        protected string _iName;
        protected int _iAge;
        protected string _iAddress;
        protected decimal _iMoney;
        protected string _phone;
        protected int _iPostCode;
        protected string _iEmail;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int IId
        {
            set { _iId = value; }
            get { return _iId; }
        }
         public Users UserMODEL
        {
            set { _userMODEL = value; }
            get { return _userMODEL; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UserId
        {
            set { _userId = value; }
            get { return _userId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IName
        {
            set { _iName = value; }
            get { return _iName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IAge
        {
            set { _iAge = value; }
            get { return _iAge; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IAddress
        {
            set { _iAddress = value; }
            get { return _iAddress; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal IMoney
        {
            set { _iMoney = value; }
            get { return _iMoney; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IPostCode
        {
            set { _iPostCode = value; }
            get { return _iPostCode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IEmail
        {
            set { _iEmail = value; }
            get { return _iEmail; }
        }
        #endregion
    }
}
