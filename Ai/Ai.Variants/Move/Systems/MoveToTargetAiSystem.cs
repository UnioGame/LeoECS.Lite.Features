namespace Game.Ecs.GameAi.Move.Systems
{
    using System;
    using Ability.Tools;
    using AI.Abstract;
    using Components;
    using Core.Death.Components;
    using Game.Ecs.AI.Components;
    using Game.Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Game.Ecs.Units.Components;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToTargetAiSystem : IAiActionSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private AbilityTools _abilityTools;

        private EcsPool<UnitComponent> _unitsPool;
        private EcsPool<MoveActionComponent> _moveToTargetPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<MoveActionComponent>()
                .Inc<UnitComponent>()
                .Inc<AiAgentComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveToTargetComponent = ref _moveToTargetPool.Get(entity);
                ref var unitComponent = ref _unitsPool.Get(entity);
                
                unitComponent.Value.MoveOrder(moveToTargetComponent.Position);
            }
        }
    }
}
