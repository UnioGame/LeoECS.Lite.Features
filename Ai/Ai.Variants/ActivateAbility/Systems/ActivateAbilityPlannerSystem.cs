namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Cysharp.Threading.Tasks;
    using Ecs.AI.Components;
    using Ecs.AI.Systems;
    using Ecs.Core.Components;
    using Ecs.GameAi.ActivateAbility;
    using Ecs.GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    
    /// <summary>
    /// Show and Hides HealthBars based on UnderTheTargetComponent 
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public class ActivateAbilityPlannerSystem : BasePlannerSystem<ActivateAbilityActionComponent>,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ActivateAbilityActionComponent> _actionPool;
        private EcsPool<ActivateAbilityPlannerComponent> _activateAbilityPool;
        private EcsPool<AbilityAiActionTargetComponent> _abilityTargetPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<AiAgentComponent>()
                .Inc<AbilityAiActionTargetComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<LayerIdComponent>()
                .Inc<ActivateAbilityPlannerComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }
        
        public override void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var targetComponent = ref _abilityTargetPool.Get(entity);
                ref var plannerComponent = ref _activateAbilityPool.Get(entity);

                ref var abilityActionComponent = ref _actionPool.GetOrAddComponent(entity);
                abilityActionComponent.Target = targetComponent.AbilityTarget;
                abilityActionComponent.AbilityCellId = targetComponent.AbilityCellId;
                abilityActionComponent.Ability = targetComponent.Ability;
    
                ApplyPlanningResult(systems, entity, plannerComponent.PlannerData);
            }
        }

        protected override UniTask OnInitialize(int id, IEcsSystems systems)
        {
            systems.DelHere<AbilityAiActionTargetComponent>();
            //systems.Add(new SelectAbilityTargetsDataPlannerSystem());
            systems.Add(new SelectAbilityTargetsByRangePlannerSystem());
            systems.Add(new SelectAbilityTargetsPlannerSystem());
            
            return UniTask.CompletedTask;
        }
    }
}