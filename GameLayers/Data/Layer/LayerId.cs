namespace Game.Code.GameLayers.Layer
{
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;
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
        public int value;

        public static implicit operator int(LayerId v)
        {
            return v.value;
        }

        public static explicit operator LayerId(int v)
        {
            return new LayerId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public LayerId FromInt(int data)
        {
            value = data;
            return this;
        }

        public bool Equals(LayerId other) => other.value == value;
        
        public bool Equals(int other) => other == value;

        public override bool Equals(object obj)
        {
            if (obj is LayerId mask)
                return mask.value == value;
            
            return false;
        }
    }
}