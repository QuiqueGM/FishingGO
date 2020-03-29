namespace VFG.Models
{
    public class Catches
    {
        private VRItem _item;
        private int _catches;
        private bool _isShown;
        
        public VRItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public int NumCatches
        {
            get { return _catches; }
            set { _catches = value; }
        }

        public bool IsShown
        {
            get { return _isShown; }
            set { _isShown = value; }
        }

        public Catches()
        {
        }

        public Catches(VRItem pItem, int pCatches, bool pIsShown)
        {
            this._item = pItem;
            this._catches = pCatches;
            this._isShown = pIsShown;
        }
    }
}