using System.Collections.Generic;

namespace VFG.Models
{
    public class Region
    {
        private string _id;
        private int _numberOfItemsToUnlockedNextRegion;
        private List<Place> _places;

        public string  Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int MinimumNumberOfItemsToUnlockNextRegion
        {
            get { return _numberOfItemsToUnlockedNextRegion; }
            set { _numberOfItemsToUnlockedNextRegion = value; }
        }

        public List<Place> Places
        {
            get { return _places; }
            set { _places = value; }
        }

        public Region()
        {
        }

        public Region(string pId, int pMinNum, List<Place> pPlaces, string pWorldPos = "", string pBack = "")
        {
            this._id = pId;
            this._numberOfItemsToUnlockedNextRegion = pMinNum;
            this._places = pPlaces;
        }
    }
}