using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using System;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.AI.Components;
    using Game.Ecs.AI.Systems;
    using Components;
    using Core.Death.Components;
    using Ai.Ai.Variants.MoveToTarget.Aspects;
    using AI.Aspects;
    using TargetSelection;
    using TargetSelection.Aspects;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    using Leopotam.EcsLite;
    using Movement.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Game.Ecs.AI.Data;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToTargetPlannerSystem : BasePlannerSystem<MoveToTargetActionComponent>, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<MoveToTargetActionComponent> _moveToTargetActionPool;
        private EcsPool<MoveToTargetComponent> _moveToTargetPool;
        private EcsPool<MoveToTargetPlannerComponent> _moveToTargetPlannerPool;
        
        public void Init(IEcsSystems systems)
        {
            Debug.Log("MoveToTargetPlanner init");
            
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<MoveToTargetComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public override void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveToTargetPlannerComponent = ref _moveToTargetPlannerPool.Get(entity);
                ref var moveToTargetComponent = ref _moveToTargetPool.Get(entity);
                ref var moveToTargetActionComponent = ref _moveToTargetActionPool.GetOrAddComponent(entity);
                moveToTargetActionComponent.Position = moveToTargetComponent.TargetPosition;

                ApplyPlanningResult(systems, entity, moveToTargetPlannerComponent.PlannerData);
            }
        }

        protected override UniTask OnInitialize(int id, IEcsSystems systems)
        {
            systems.DelHere<MoveToTargetComponent>();
            systems.Add(new MoveByCategoryTargetSelectionSystem(id));

            return UniTask.CompletedTask;
        }
    }
}