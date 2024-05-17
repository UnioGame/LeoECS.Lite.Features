namespace Ai.Ai.Variants.Attack.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.AI.Abstract;
    using Game.Ecs.AI.Components;
    using Game.Ecs.Core.Death.Components;
    using Game.Ecs.Units.Components;
    using Girand.Runtime.Server;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AttackAiSystem : IEcsInitSystem, IAiActionSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<UnitComponent> _unitPool;
        private EcsPool<GameObjectComponent> _gameObjectComponent;
        private AttackAspect _attackAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AttackActionComponent>()
                .Inc<UnitComponent>()
                .Inc<AiAgentComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var unitComponent = ref _unitPool.Get(entity);
                if (unitComponent.Value.IsDead && unitComponent.Value.CurrentHealth <= 0f)
                {
                    continue;
                }
                
                ref var attackActionComponent = ref _attackAspect.Action.Get(entity);
                if (!attackActionComponent.Value.Unpack(_world, out var targetEntity))
                {
                    continue;
                }

                ref var targetGameObjectComponent = ref _gameObjectComponent.Get(targetEntity);
                var targetTargetable = targetGameObjectComponent.Value.GetComponent<Targetable>();

                unitComponent.Value.ChaseTarget = targetTargetable;
                unitComponent.Value.AttackOrder();
            }
        }
    }
}