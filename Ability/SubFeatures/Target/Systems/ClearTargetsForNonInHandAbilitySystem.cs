namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ClearTargetsForNonInHandAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        
        private EcsFilter _filter;
        private EcsWorld _world;

        private TargetAbilityAspect _targetAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityTargetsComponent>()
                .Exc<AbilityInHandComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {   
                ref var targets = ref _targetAspect.AbilityTargets.Get(entity);
                targets.Count = 0;
                targets.PreviousCount = 0;
            }
        }
    }
}