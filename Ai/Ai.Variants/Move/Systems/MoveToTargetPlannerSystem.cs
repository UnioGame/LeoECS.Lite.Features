namespace Game.Ecs.GameAi.Move.Systems
{
    using System;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.AI.Components;
    using Game.Ecs.AI.Systems;
    using Components;
    using Core.Death.Components;
    using AI.Data;
    using global::Ai.Ai.Variants.MoveToTarget.Aspects;
    using UniGame.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Movement.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToTargetPlannerSystem : BasePlannerSystem<MoveActionComponent>, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private MoveAspect _moveAspect;
        private EcsPool<TransformPositionComponent> _positionPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<MovePlannerComponent>()
                .Inc<MoveToTargetComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public override void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveToTargetPlannerComponent = ref _moveAspect.Planner.Get(entity);
                int priority = AiConstants.PriorityNever;
                ref var targetComponent = ref _moveAspect.Target.Get(entity);
                if (targetComponent.Value.Unpack(_world, out var targetEntity))
                {
                    ref var positionComponent = ref _positionPool.Get(targetEntity);
                    ref var moveToTargetActionComponent = ref _moveAspect.MoveAction.GetOrAddComponent(entity);
                    moveToTargetActionComponent.Position = positionComponent.Position;
                    priority = moveToTargetPlannerComponent.PlannerData.Priority;
                } 
                
                ApplyPlanningResult(systems, entity, new AiPlannerData
                {
                    Priority = priority
                });
            }
        }

        protected override UniTask OnInitialize(IEcsSystems systems)
        {
            systems.DelHere<MoveToTargetComponent>();
            systems.Add(new MoveToChaseTargetSystem());
            systems.Add(new MoveToDefaultTargetSystem());
            return UniTask.CompletedTask;
        }
    }
}