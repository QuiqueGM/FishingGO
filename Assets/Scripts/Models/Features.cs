namespace VFG.Models
{
    public class Features
    {
        private float _commonSize;
        private float _maxSize;
        private int _minDepth;
        private int _maxDepth;


        public float CommonSize
        {
            get { return _commonSize; }
            set { _commonSize = value; }
        }

        public float MaxSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }

        public int MinDepth
        {
            get { return _minDepth; }
            set { _minDepth = value; }
        }

        public int MaxDepth
        {
            get { return _maxDepth; }
            set { _maxDepth = value; }
        }

        public Features()
        {
        }

        public Features(float pCommonSize, float pMaxSize, int pMinDepth, int pMaxDepth)
        {
            this._commonSize = pCommonSize;
            this._maxSize = pMaxSize;
            this._minDepth = pMinDepth;
            this._maxDepth = pMaxDepth;
        }
    }
}