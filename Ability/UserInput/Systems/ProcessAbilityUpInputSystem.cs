namespace Game.Ecs.Ability.UserInput.Systems
{
    using Common.Components;
    using Components;
    using Core.Death.Components;
    using Input.Components;
    using Input.Components.Ability;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class ProcessAbilityUpInputSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<SetInHandAbilityBySlotSelfRequest> _inHandRequestPool;
        private EcsPool<AbilityUpInputRequest> _upPool;
        private EcsPool<AbilityMapComponent> _abilityMapPool;
        private EcsPool<CanApplyWhenUpInputComponent> _canUpPool;
        private EcsPool<ApplyAbilityBySlotSelfRequest> _applyRequestPool;
        private EcsPool<AbilityActiveTimeComponent> _activateTimePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AbilityUpInputRequest>()
                .Inc<UserInputTargetComponent>()
                .Inc<AbilityMapComponent>()
                .Exc<DisabledComponent>()
                .End();
            
            _inHandRequestPool = _world.GetPool<SetInHandAbilityBySlotSelfRequest>();
            _upPool = _world.GetPool<AbilityUpInputRequest>();
            _abilityMapPool = _world.GetPool<AbilityMapComponent>();
            _canUpPool = _world.GetPool<CanApplyWhenUpInputComponent>();
            _applyRequestPool = _world.GetPool<ApplyAbilityBySlotSelfRequest>();
            _activateTimePool = _world.GetPool<AbilityActiveTimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var up = ref _upPool.Get(entity);
                ref var abilityMap = ref _abilityMapPool.Get(entity);

                if (!abilityMap.AbilityEntities[up.InputId].Unpack(_world, out var abilityEntity))
                    continue;

                if (!_canUpPool.Has(abilityEntity))
                    continue;

                if (_applyRequestPool.Has(entity))
                    continue;
                
                if (!_inHandRequestPool.Has(entity))
                {
                    ref var inHand = ref _inHandRequestPool.Add(entity);
                    inHand.AbilityCellId = up.InputId;
                }
                
                ref var activateTime = ref _activateTimePool.GetOrAddComponent(abilityEntity);
                activateTime.Time = up.ActiveTime;

                ref var request = ref _applyRequestPool.Add(entity);
                request.AbilitySlot = up.InputId;
            }
        }
    }
}