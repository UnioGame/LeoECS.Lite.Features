namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Ability.Components.Requests;
    using Characteristics.Cooldown.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ResetAbilityCooldownAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private EcsPool<CooldownStateComponent> _cooldownPool;
        private EcsPool<ResetAbilityCooldownSelfRequest> _resetPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<ResetAbilityCooldownSelfRequest>()
                .Inc<CooldownStateComponent>()
                .Inc<ActiveAbilityComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var abilityCooldownComponent = ref _cooldownPool.Get(entity);
                abilityCooldownComponent.LastTime = 0;
                
                _resetPool.Del(entity);
            }
        }
    }
}