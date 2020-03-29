using System.Collections.Generic;
using VFG.Core;

namespace VFG.Models
{
    public class VRItem
    {
        private string _id;
        private TypeOfItem _typeOfItem;
        private Classification _classification;
        private float _minSize;
        private float _maxSize;
        private int _minSchool;
        private int _maxSchool;
        private int _percentage;
        private int _area;
        private bool _isUnlocked;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public TypeOfItem TypeOfItem
        {
            get { return _typeOfItem; }
            set { _typeOfItem = value; }
        }

        public Classification Classification
        {
            get { return _classification; }
            set { _classification = value; }
        }

        public int MinSchool
        {
            get { return _minSchool; }
            set { _minSchool = value; }
        }

        public int MaxSchool
        {
            get { return _maxSchool; }
            set { _maxSchool = value; }
        }

        public int Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

        public int Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public bool IsUnlocked
        {
            get { return _isUnlocked; }
            set { _isUnlocked = value; }
        }

        public VRItem()
        {
        }

        public VRItem(string pId, Classification pClassification, float pMinSize, float pMaxSize, int pMinSchool, int pMaxSchool, int pPercentage, int pArea)
        {
            this._id = pId;
            this._classification = pClassification;
            this._minSize = pMinSize;
            this._maxSize = pMaxSize;
            this._minSchool = pMinSchool;
            this._maxSchool = pMaxSchool;
            this._percentage = pPercentage;
            this._area = pArea;
        }
    }
}