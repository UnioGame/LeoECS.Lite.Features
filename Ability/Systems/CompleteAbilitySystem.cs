namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CompleteAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private AbilityAspect _abilityAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AbilityUsingComponent>()
                .Inc<CompleteAbilitySelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _abilityAspect.AbilityUsing.Del(entity);
                if(_abilityAspect.Evaluate.Has(entity))
                    _abilityAspect.Evaluate.Del(entity);
                
                _abilityAspect.CompleteEvent.GetOrAddComponent(entity);
            }
        }
    }
}