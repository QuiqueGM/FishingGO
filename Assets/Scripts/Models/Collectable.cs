using VFG.Core;

namespace VFG.Models
{
    public class Collectable
    {
        private string _id;
        private string _scientificName;
        private TypeOfItem _typeOfItem;
        private GameState.State _state;
        private string _color;
        private Classification _classification;
        private Features _features;
        private float _initScale;
        private string _place;
        private int _catches;
        
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ScientificName
        {
            get { return _scientificName; }
            set { _scientificName = value; }
        }

        public TypeOfItem TypeOfItem
        {
            get { return _typeOfItem; }
            set { _typeOfItem = value; }
        }

        public GameState.State State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Classification Classification
        {
            get { return _classification; }
            set { _classification = value; }
        }

        public Features Features
        {
            get { return _features; }
            set { _features = value; }
        }

        public int NumCatches
        {
            get { return _catches; }
            set { _catches = value; }
        }

        public string Place
        {
            get { return _place; }
            set { _place = value; }
        }

        public Collectable()
        {
        }

        public Collectable(string pId, string pScientificName, TypeOfItem pTypeOf, string pColor, Classification pClassification, Features pFeatures, string pPlace, float pScale, int pNumCatches)
        {
            this._id = pId;
            this._scientificName = pScientificName;
            this._typeOfItem = pTypeOf;
            this._color = pColor;
            this._classification = pClassification;
            this._features = pFeatures;
            this._initScale = pScale;
            this._place = pPlace;
            this._catches = pNumCatches;
        }
    }
}