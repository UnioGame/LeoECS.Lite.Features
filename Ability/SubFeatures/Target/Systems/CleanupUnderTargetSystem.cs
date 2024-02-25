namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using System;
    using Aspects;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using TargetSelection;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CleanupUnderTargetSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _underTargetFilter;
        private EcsWorld _world;
        private TargetAbilityAspect _targetAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _underTargetFilter = _world
                .Filter<UnderTheTargetComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _underTargetFilter)
            {
                ref var underTargetComponent = ref _targetAspect.UnderTheTarget.Get(entity);
                underTargetComponent.Count = 0;
            }
        }
    }
}