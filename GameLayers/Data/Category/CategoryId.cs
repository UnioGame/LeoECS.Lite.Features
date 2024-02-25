namespace Game.Code.GameLayers.Category
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct CategoryId
    {
        [SerializeField]
        private int _value;

        public static implicit operator int(CategoryId v)
        {
            return v._value;
        }

        public static explicit operator CategoryId(int v)
        {
            return new CategoryId { _value = v };
        }

        public override string ToString() => _value.ToString();

        public override int GetHashCode() => _value;

        public CategoryId FromInt(int data)
        {
            _value = data;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is CategoryId mask)
                return mask._value == _value;
            
            return false;
        }
    }
}
