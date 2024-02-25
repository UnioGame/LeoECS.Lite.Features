namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Characteristics.Cooldown.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CooldownRevokeAbilityRequestSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<ApplyAbilitySelfRequest> _applyAbilityRequestPool;
        private EcsPool<SetInHandAbilitySelfRequest> _setInHandAbilityRequestPool;
        private EcsPool<CooldownStateComponent> _cooldownStatePool;
        private EcsPool<CooldownComponent> _cooldownPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<SetInHandAbilitySelfRequest>()
                .Inc<ApplyAbilitySelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _filter)
            {
                ref var applyAbilityRequest = ref _applyAbilityRequestPool.Get(requestEntity);
                if(!applyAbilityRequest.Value.Unpack(_world,out var abilityEntity))
                    continue;
       
                ref var cooldownComponent = ref _cooldownPool.Get(abilityEntity);
                ref var cooldownStateComponent = ref _cooldownStatePool.Get(abilityEntity);
                
                var lastUseTime = cooldownStateComponent.LastTime;
                var nextUseTime = lastUseTime + cooldownComponent.Value;
                
                var remainTime = nextUseTime - Time.time;
                
                if (!(remainTime > 0.0f) || Mathf.Approximately(remainTime, 0.0f)) continue;
                
                _setInHandAbilityRequestPool.Del(requestEntity);
                _applyAbilityRequestPool.Del(requestEntity);
            }
        }
    }
}