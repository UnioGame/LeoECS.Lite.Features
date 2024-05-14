namespace Ai.Ai.Variants.Attack.Systems
{
    using System;
    using Aspects;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.AI.Components;
    using Game.Ecs.AI.Data;
    using Game.Ecs.AI.Systems;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AttackPlannerSystem : BasePlannerSystem<AttackActionComponent>, 
        IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private AttackAspect _attackAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<AttackPlannerComponent>()
                .Inc<AttackTargetComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public override void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var priorityAttackTargetComponent = ref _attackAspect.Target.Get(entity);
                ref var attackPlannerComponent = ref _attackAspect.Planner.Get(entity);
                int priority = AiConstants.PriorityNever;

                if (priorityAttackTargetComponent.Value.Unpack(_world, out var targetEntity))
                {
                    priority = attackPlannerComponent.PlannerData.Priority;
                    ref var attackActionComponent = ref _attackAspect.Action.GetOrAddComponent(entity);
                    attackActionComponent.Value = priorityAttackTargetComponent.Value;
                }
                
                ApplyPlanningResult(systems, entity, new AiPlannerData
                {
                    Priority = priority
                });
            }
        }

        protected override UniTask OnInitialize(IEcsSystems systems)
        {
            systems.DelHere<AttackTargetComponent>();
            systems.Add(new AttackChaseTargetSystem());
            return UniTask.CompletedTask;
        }
    }
}