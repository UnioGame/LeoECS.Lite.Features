namespace Game.Ecs.AbilityInventory.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// if ability with same id already exists in inventory - remove it
    /// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class EquipAbilityReferenceSystem : IEcsInitSystem, IEcsRunSystem
    {
	    private AbilityInventoryAspect _inventoryAspect;
	    private int _generatedAbilityId = int.MaxValue;
	    
	    private EcsWorld _world;
	    private EcsFilter _filterRequest;

	    public void Init(IEcsSystems systems)
	    {
		    _world = systems.GetWorld();

		    _filterRequest = _world
			    .Filter<EquipAbilityReferenceSelfRequest>()
			    .End();
	    }

	    public void Run(IEcsSystems systems)
	    {
		    foreach (var request in _filterRequest)
		    {
			    ref var requestComponent = ref _inventoryAspect.EquipByReference.Get(request);
			    ref var abilityEquipRequest = ref _inventoryAspect.Equip.Add(request);
			    ref var configurationComponent = ref _inventoryAspect.Configuration.GetOrAddComponent(request);

			    abilityEquipRequest.AbilityId = _generatedAbilityId;
			    abilityEquipRequest.IsUserInput = requestComponent.IsUserInput;
			    abilityEquipRequest.AbilitySlot = requestComponent.AbilitySlot;
			    abilityEquipRequest.Target = requestComponent.Owner;
			    abilityEquipRequest.IsDefault = requestComponent.IsDefault;
			    abilityEquipRequest.Hide = false;
			    abilityEquipRequest.IsBlocked = false;

			    configurationComponent.Value = requestComponent.Reference;
			    
			    _generatedAbilityId--;
			    _inventoryAspect.EquipByReference.Del(request);
		    }
	    }
    }
}