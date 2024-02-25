namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Systems
{
    using System;
    using System.Linq;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// activate new ability by behavior
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyAbilityBehaviorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {

        }
    }
}