namespace Game.Ecs.AbilityInventory
{
	using System;
	using Code.Services.Ability.Data.Arena;
	using Code.Services.AbilityLoadout.Abstract;
	using Components;
	using Converters;
	using Cysharp.Threading.Tasks;
	using Equip.Components;
	using Leopotam.EcsLite;
	using Leopotam.EcsLite.ExtendedSystems;
	using Systems;
	using UniGame.Context.Runtime.Extension;
	using UniGame.Core.Runtime;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
	public class AbilityInventoryFeature : LeoEcsFeature
	{
		public AbilityCatalog abilityCatalog;
		
		protected override async UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			var context = ecsSystems.GetShared<IContext>();
			var world = ecsSystems.GetWorld();
			
			var abilityLoadoutService = await context.ReceiveFirstAsync<IAbilityLoadoutService>();
			var inventoryTool = new AbilityInventoryTool();

			world.SetGlobal(inventoryTool);
			world.SetGlobal(abilityLoadoutService);
			//world.SetGlobal(abilityCatalog);
			
			ecsSystems.Add(inventoryTool);
			//initialize player inventor ability data
			ecsSystems.Add(new AbilityInventoryInitSystem());
			ecsSystems.Add(new AbilityRarityMapSystem(abilityLoadoutService));

			ecsSystems.DelHere<AbilityInventorySaveCompleteEvent>();
			
			ecsSystems.Add(new AbilityInventoryUpdateSlotDataSystem(abilityLoadoutService));
			ecsSystems.Add(new AbilityInventorySpawnSystem(abilityLoadoutService));
			
			ecsSystems.Add(new EquipAbilityByIdToChampionSystem());
			//try find ability by name and equip it
			ecsSystems.Add(new EquipAbilityByNameSystem());
			//get request to equip ability by id and load configuration from meta data
			ecsSystems.Add(new EquipAbilityByIdSystem());
			//get request to equip ability with configuration data in request
			ecsSystems.Add(new EquipAbilityReferenceSystem());
			
			//validate is ability with same id already equipped
			ecsSystems.Add(new ValidateExistsAbilityOnEquipSystem());

			//if validation failed - destroy entity
			ecsSystems.Add(new InvalidatedAbilitySystem());
			//request to build new ability if validation success
			ecsSystems.Add(new EquipAbilityToInventorySystem());
			//load ability meta by request 
			ecsSystems.Add(new LoadAbilityMetaSystem());
			//check is ability data loaded
			ecsSystems.Add(new CheckEquipAbilityLoadingSystem());
			//attach ability configuration to entity and load if needed
			ecsSystems.Add(new AttachAbilityConfigurationSystem());
			//update component on ability request entity when meta is linked
			ecsSystems.Add(new CollectAbilityMetaInformationSystem());
			
			//build an ability
			ecsSystems.Add(new AbilityBuildByConfigurationSystem());
			
			//mark ability as ready and fire AbilityEquipChangedEvent
			ecsSystems.DelHere<AbilityEquipChangedEvent>();
			ecsSystems.Add(new AbilityEquipSystem());

			ecsSystems.DelHere<LoadAbilityMetaRequest>();
			ecsSystems.DelHere<EquipAbilityNameSelfRequest>();
		}
	}
}