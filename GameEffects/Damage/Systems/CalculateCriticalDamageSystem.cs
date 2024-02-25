namespace Game.Ecs.Gameplay.Damage.Systems
{
    using System;
    using Characteristics.Base.Components.Requests.OwnerRequests;
    using Characteristics.CriticalMultiplier.Components;
    using Characteristics.Health.Components;
    using Characteristics.Shield.Components;
    using Components;
    using Components.Request;
    using Events;
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
    public sealed class CalculateCriticalDamageSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
     
        private EcsPool<ApplyDamageRequest> _requestPool;
        private EcsPool<CriticalMultiplierComponent> _criticalMultiplierPool;

        private float _defaultMultiplier = 0f;
        
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
                if (!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                if(!request.IsCritical) continue;
                
                var multiplier = _defaultMultiplier;

                if (_criticalMultiplierPool.Has(sourceEntity))
                {
                    ref var criticalMultiplierComponent = ref _criticalMultiplierPool.Get(sourceEntity);
                    var characteristicValue = criticalMultiplierComponent.Value;
                    multiplier+=characteristicValue;
                }

                multiplier /= 100f;
                
                var damage = request.Value + request.Value * multiplier;
                request.Value = damage;
            }
        }
    }
}