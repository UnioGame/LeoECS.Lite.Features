namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using System;
    using Aspects;
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
    public sealed class RemoveZeroUnderTargetSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _underTargetFilter;
        private EcsWorld _world;
        private TargetAbilityAspect _targetAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _underTargetFilter = _world
                .Filter<UnderTheTargetComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _underTargetFilter)
            {
                ref var underTargetComponent = ref _targetAspect.UnderTheTarget.Get(entity);
                if(underTargetComponent.Count > 0) continue;
                _targetAspect.UnderTheTarget.Del(entity);
            }
        }
    }
}