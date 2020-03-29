using System.Collections.Generic;

namespace VFG.Models
{
    public class Place
    {
        private string _id;
        private List<VRItem> _item;

        public string  Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<VRItem> Items
        {
            get { return _item; }
            set { _item = value; }
        }

        public Place()
        {
        }

        public Place(string pId, List<VRItem> pItems, string pWorldPos = "", string pBack = "")
        {
            this._id = pId;
            this._item = pItems;
        }
    }
}