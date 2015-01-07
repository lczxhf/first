using System;

namespace ShoeStore.MODEL
{
    public class NewBuy
    {
        public NewBuy() { }
        protected int _pCount;
        protected Product _pIdMODEL;

        public int PCount
        {
            set { _pCount = value; }
            get { return _pCount; }
        }
        public Product PIdMODEL
        {
            set { _pIdMODEL = value; }
            get { return _pIdMODEL; }
        }
    }
}
