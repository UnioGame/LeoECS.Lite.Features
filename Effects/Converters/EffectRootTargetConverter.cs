namespace Game.Ecs.Effects.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Data;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// mark gameobject as effect root target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class EffectRootTargetConverter : GameObjectConverter
    {
        public EffectRootId effectRootId;
        public bool useRootTransform = true;
        [HideIf(nameof(useRootTransform))]
        public Transform rootValue;
        
        protected override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var effectRootIdComponent = ref world.AddComponent<EffectRootIdComponent>(entity);
            ref var targetComponent = ref world.AddComponent<EffectRootTargetComponent>(entity);
            ref var parentComponent = ref world.AddComponent<EffectParentComponent>(entity);
            
            effectRootIdComponent.Value = effectRootId;
            parentComponent.Value = useRootTransform 
                ? target.transform 
                : rootValue;
        }
    }
}