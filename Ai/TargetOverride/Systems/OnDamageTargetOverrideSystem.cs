namespace Game.Ecs.AI.TargetOverride.Systems
{
    using System;
    using AI.Aspects;
    using AI.Components;
    using Aspects;
    using Components;
    using Core.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using Shared.Components;
    using TargetSelection.Aspects;
    using UniGame.LeoEcs.Shared.Extensions;
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
    public class OnDamageTargetOverrideSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private TargetSelectionAspect _targetSelectionAspect;
        private AIAspect _aiAspect;

        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<TEMPORARY_HealthChangedEvent> _healthChangedPool;
        private EcsPool<LayerIdComponent> _layerIdPool;
        private EcsPool<ChildrenComponent> _childrenPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<OnDamageTargetOverrideComponent>()
                .Inc<AiGroupAgentComponent>()
                .Inc<TEMPORARY_HealthChangedEvent>()
                .Inc<OwnerComponent>()
                .Exc<TargetOverrideLockComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var ownerComponent = ref _ownerPool.Get(entity);
                if (!ownerComponent.Value.Unpack(_world, out var targetOwnerEntity))
                {
                    continue;
                }
                
                ref var healthChangedComponent = ref _healthChangedPool.Get(entity);
                var damageDealer = healthChangedComponent.Dealer;
                if (!damageDealer.Unpack(_world, out var damageDealerEntity))
                {
                    continue;
                }

                ref var damageDealerLayerIdComponent = ref _layerIdPool.Get(damageDealerEntity);
                ref var childrenComponent = ref _childrenPool.Get(targetOwnerEntity);
                foreach (var groupAgentEntity in childrenComponent.Children)
                {
                    if (!groupAgentEntity.Unpack(_world, out var targetEntity))
                    {
                        continue;
                    }

                    ref var layerOverrideComponent = ref _targetSelectionAspect.LayerOverride.GetOrAddComponent(targetEntity);
                    layerOverrideComponent.Value |= damageDealerLayerIdComponent.Value;
                    Debug.Log(layerOverrideComponent.Value);
                }
            }
        }
    }
}