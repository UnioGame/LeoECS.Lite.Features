namespace Game.Ecs.Gameplay.Damage.Systems
{
    using System;
    using Components;
    using Components.Request;
    using Events;
    using Game.Ecs.Characteristics.Base.Components.Requests.OwnerRequests;
    using Game.Ecs.Characteristics.Health.Components;
    using Game.Ecs.Characteristics.Shield.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

    /// <summary>
    /// apply damage to target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ApplyDamageSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ApplyDamageRequest> _requestPool;
        private EcsPool<ChangeCharacteristicBaseRequest<HealthComponent>> _changeHealthPool;
        private EcsPool<ChangeShieldRequest> _changeShieldPool;
        private EcsPool<ShieldComponent> _shieldPool;
        private EcsPool<MadeDamageEvent> _madeDamagePool;
        private EcsPool<CriticalDamageEvent> _criticalDamagePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ApplyDamageRequest>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _requestPool.Get(entity);
                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                var healthDamage = request.Value;
                
                var healthRequestEntity = _world.NewEntity();
                ref var healthRequest = ref _changeHealthPool.Add(healthRequestEntity);
                healthRequest.Source = request.Source;
                healthRequest.Target = request.Destination;
                healthRequest.Value = -healthDamage;

                request.Source.Unpack(_world, out var sourceEntity);
                
                var eventEntity = _world.NewEntity();
                ref var madeDamage = ref _madeDamagePool.Add(eventEntity);
                madeDamage.Value = request.Value;
                madeDamage.Source = request.Source;
                madeDamage.Destination = request.Destination;
                madeDamage.IsCritical = request.IsCritical;

                if (!request.IsCritical) continue;
                
                ref var criticalEventComponent = ref _criticalDamagePool.Add(eventEntity);
                criticalEventComponent.Value = request.Value;
                criticalEventComponent.Source = request.Source;
                criticalEventComponent.Destination = request.Destination;
            }
        }
    }
}