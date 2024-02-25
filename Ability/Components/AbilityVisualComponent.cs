namespace Game.Ecs.AbilityInventory.Components
{
    using System;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

    /// <summary>
    /// Ability visual description
    /// </summary>
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct AbilityVisualComponent
    {
        public string Name;
        public string Description;
        public string ManaCost;
        public AssetReferenceT<Sprite> Icon;
    }
}