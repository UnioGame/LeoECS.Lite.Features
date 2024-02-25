namespace Game.Code.GameLayers.Relationship
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct RelationshipId
    {
        [SerializeField]
        private int _value;

        public static implicit operator int(RelationshipId v)
        {
            return v._value;
        }

        public static explicit operator RelationshipId(int v)
        {
            return new RelationshipId { _value = v };
        }

        public override string ToString() => _value.ToString();

        public override int GetHashCode() => _value;

        public RelationshipId FromInt(int data)
        {
            _value = data;
            
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is RelationshipId mask)
                return mask._value == _value;
            
            return false;
        }
    }
}