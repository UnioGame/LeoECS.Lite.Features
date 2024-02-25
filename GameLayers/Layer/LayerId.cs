namespace Game.Code.GameLayers.Layer
{
    using System;
    using UnityEngine;
#if ENABLE_IL2CPP
    using System;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct LayerId  : IEquatable<LayerId>, IEquatable<int>
    {
        [SerializeField]
        private int _value;

        public static implicit operator int(LayerId v)
        {
            return v._value;
        }

        public static explicit operator LayerId(int v)
        {
            return new LayerId { _value = v };
        }

        public override string ToString() => _value.ToString();

        public override int GetHashCode() => _value;

        public LayerId FromInt(int data)
        {
            _value = data;
            return this;
        }

        public bool Equals(LayerId other) => other._value == _value;
        
        public bool Equals(int other) => other == _value;

        public override bool Equals(object obj)
        {
            if (obj is LayerId mask)
                return mask._value == _value;
            
            return false;
        }
    }
}