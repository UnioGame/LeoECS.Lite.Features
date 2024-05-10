namespace Ai.Ai.Variants.Prioritizer.Components
{
    using System;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Layer;
    using Game.Ecs.Shared.Generated;
    using UniModules.UniGame.Core.Runtime.DataStructure;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CategoryPrioritizerComponent
    {
        [HideInInspector]
        public ActionType ActionId;
        public SerializableDictionary<CategoryId, int> Value;
    }
}