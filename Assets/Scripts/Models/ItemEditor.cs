using UnityEngine;
using VFG.Core;

namespace VFG.Models
{
    public class ItemEditor
    {
        private string _id;
        private TypeOfItem _typeOfItem;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;
        private bool _isFreezed;

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

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Quaternion Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public bool IsFreezed
        {
            get { return _isFreezed; }
            set { _isFreezed = value; }
        }

        public ItemEditor()
        {
        }

		public ItemEditor(string pId, TypeOfItem pTypeOf, Vector3 pPos, Quaternion pRot, Vector3 pScl, bool pIsFrz)
        {
            this._id = pId;
            this._typeOfItem = pTypeOf;
            this._position = pPos;
            this._rotation = pRot;
            this._scale = pScl;
            this._isFreezed = pIsFrz;
        }
    }
}