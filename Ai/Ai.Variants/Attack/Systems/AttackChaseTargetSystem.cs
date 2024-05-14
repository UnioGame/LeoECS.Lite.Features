namespace Ai.Ai.Variants.Attack.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Characteristics.CriticalChance.Components;
    using Game.Ecs.Characteristics.Radius.Component;
    using Game.Ecs.Units.Components;
    using Leopotam.EcsLite;
    using Prioritizer.Aspects;
    using Prioritizer.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AttackChaseTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter _filter;
        private AttackAspect _attackAspect;
        private PrioritizerAspect _prioritizerAspect;

        private EcsPool<TransformPositionComponent> _positionPool;
        private EcsPool<AttackRangeComponent> _attackRangePool;
        private EcsPool<UnitComponent> _unitPool;
        private EcsPool<RadiusComponent> _radiusPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AttackChaseTargetComponent>()
                .Inc<ChaseTargetComponent>()
                .Inc<TransformPositionComponent>()
                //.Inc<AttackRangeComponent>()
                .Inc<UnitComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var chaseEntity in _filter)
            {
                ref var chaseTargetComponent = ref _prioritizerAspect.Chase.Get(chaseEntity);
                if (!chaseTargetComponent.Value.Unpack(_world, out var chaseTargetEntity))
                {
                    continue;
                }
                
                ref var attackChaseTargetComponent = ref _attackAspect.Chase.Get(chaseEntity);
                ref var attackTargetComponent = ref _attackAspect.Target.GetOrAddComponent(chaseEntity);
                ref var radiusComponent = ref _radiusPool.Get(chaseTargetEntity);

                //ref var attackRangeComponent = ref _attackRangePool.Get(chaseEntity);
                ref var sourcePositionComponent = ref _positionPool.Get(chaseEntity);
                ref var chaseTargetPositionComponent = ref _positionPool.Get(chaseTargetEntity);
                ref var unitComponent = ref _unitPool.Get(chaseEntity);
                var unit = unitComponent.Value;

                var attackRange = unit.GetAttackRange() * 0.99f + radiusComponent.Value;
                var attackRangeSqr = attackRange * attackRange;
                var distanceSqr = math.distancesq(sourcePositionComponent.Position, chaseTargetPositionComponent.Position);
                
                if (distanceSqr <= attackRangeSqr && attackChaseTargetComponent.Priority > attackTargetComponent.Priority)
                {
                    attackTargetComponent.Priority = attackTargetComponent.Priority;
                    attackTargetComponent.Value = chaseTargetComponent.Value;
                }
            }
        }
    }
}