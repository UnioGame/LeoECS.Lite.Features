namespace Game.Ecs.AbilityInventory.Systems
{
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Search ability in inventory
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [ECSDI]
    public class EquipAbilityByIdToChampionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _championFilter;

        private EcsPool<EquipAbilityIdToChampionRequest> _requestPool;
        private EcsPool<EquipAbilityIdSelfRequest> _equipRequestPool;
        

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
			
            _filter = _world
                .Filter<EquipAbilityIdToChampionRequest>()
                .End();
            
            _championFilter = _world
                .Filter<ChampionComponent>()
                .End();

            _requestPool = _world.GetPool<EquipAbilityIdToChampionRequest>();
            _equipRequestPool = _world.GetPool<EquipAbilityIdSelfRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var request in _filter)
            {
                ref var requestComponent = ref _requestPool.Get(request);
				
                foreach (var entity in _championFilter)
                {
                    ref var abilityEquipRequest = ref _equipRequestPool.Add(request);
					
                    abilityEquipRequest.AbilityId = requestComponent.AbilityId;
                    abilityEquipRequest.AbilitySlot = requestComponent.AbilitySlot;
                    abilityEquipRequest.IsUserInput = requestComponent.IsUserInput;
                    abilityEquipRequest.IsDefault = requestComponent.IsDefault;
                    abilityEquipRequest.Owner = _world.PackEntity(entity);
					
                    _requestPool.Del(request);
                }
            }
        }
    }
}