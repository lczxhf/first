using System;


namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- CarItems表映射类.
    /// 最后修改时间:2014-06-17 18:55:20
    /// </summary>
    public class CarItems
    {
        public CarItems()
        { }

        #region 字段们
        protected int _cItemId;
        protected int _cCarId;
        protected Car _cCarIdMODEL;
        protected Users _cUserMODEL;
        protected int _cPId;
        protected Product _cPIdMODEL;
        protected int _cCount;
        protected DateTime _cTime;
        #endregion

        #region 属性们
        /// <summary>
        /// 购物车 明细表 id
        /// </summary>
        public int CItemId
        {
            set { _cItemId = value; }
            get { return _cItemId; }
        }

        /// <summary>
        /// 购物车id
        /// </summary>
        public int CCarId
        {
            set { _cCarId = value; }
            get { return _cCarId; }
        }
        /// <summary>
        ///  购物车id-外键实体
        /// </summary>
        public Car CCarIdMODEL
        {
            set { _cCarIdMODEL = value; }
            get { return _cCarIdMODEL; }
        }

        public Users CUserMODEL
        {
            set { _cUserMODEL = value; }
            get { return _cUserMODEL; }
        }

        /// <summary>
        /// 产品id
        /// </summary>
        public int CPId
        {
            set { _cPId = value; }
            get { return _cPId; }
        }
        /// <summary>
        ///  产品id-外键实体
        /// </summary>
        public Product CPIdMODEL
        {
            set { _cPIdMODEL = value; }
            get { return _cPIdMODEL; }
        }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int CCount
        {
            set { _cCount = value; }
            get { return _cCount; }
        }

        public DateTime CTime
        {
            set { _cTime = value; }
            get { return _cTime; }
        }
        #endregion
    }
}
