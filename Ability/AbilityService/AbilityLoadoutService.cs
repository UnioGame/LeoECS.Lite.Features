namespace Game.Code.Services.AbilityLoadout
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Ability.Data;
	using Abstract;
	using Cysharp.Threading.Tasks;
	using Data;
	using UniGame.AddressableTools.Runtime;
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UniGame.UniNodes.GameFlow.Runtime;
	using UnityEngine.Pool;

	/// <summary>
	/// Store all abilities and ability loadout. Helps to manage abilities metadata
	/// </summary>
	public class AbilityLoadoutService : GameService, IAbilityLoadoutService
	{
		private AbilityProfileData _profileData;
		private AbilityDataBase _abilityDataBase;
		private AbilitySlotsData _abilitySlotData;
		private AbilityRarityData _abilityRarityData;

		private int[] _allAbilities = Array.Empty<int>();
		private int[] _inventoryAbilities = Array.Empty<int>();
		
		private List<AbilityRecord> _inventoryAbilitieRecords = new List<AbilityRecord>();
		
		private Dictionary<int,AbilityItemAsset> _abilityAssetMap = new();
		
		public AbilityLoadoutService(
			AbilityProfileData profileData,
			AbilityLoadoutData abilityLoadoutData)
		{
			_profileData = profileData;
			_abilityDataBase = abilityLoadoutData.abilityDataBase;
			_abilitySlotData = abilityLoadoutData.abilitySlotMap;
			_abilityRarityData = abilityLoadoutData.abilityRarityData;
			
			var records = _abilityDataBase.abilities;
			var length = records.Length;
			_allAbilities = new int[length];

			for (var i = 0; i < length; i++)
				_allAbilities[i] = records[i].id;

			for (var i = 0; i < length; i++)
			{
				var record = records[i];
				var isInterface = !record.data.isHidden && !record.data.isDefault;
				if(isInterface)
					_inventoryAbilitieRecords.Add(record);
			}

			_inventoryAbilities = new int[_inventoryAbilitieRecords.Count];
			for (var i = 0; i < _inventoryAbilitieRecords.Count; i++)
				_inventoryAbilities[i] = _inventoryAbilitieRecords[i].id;
		}
		
		public int[] AllAbilities => _allAbilities;
		
		public int[] InventoryAbilities => _inventoryAbilities;
		
		public IReadOnlyList<AbilitySlot> Slots => _abilitySlotData.slots;
		public IReadOnlyList<int> Inventory => _profileData.inventory;
		public IReadOnlyList<AbilitySlotData> AbilitySlotData => _profileData.abilities;
		
		public AbilityRarityData AbilityRarityData => _abilityRarityData;
		
		private Dictionary<int, List<AbilityItemData>> _availableAbilitySlotMap = new(8);
		
		public async UniTask<AbilityItemData> GetAbilityDataAsync(int abilityId)
		{
			if (_abilityAssetMap.TryGetValue(abilityId, out var ability))
				return ability.data;

			var abilityItem = _abilityDataBase.Find(abilityId);
			if(abilityItem == AbilityRecord.Empty)
				return AbilityItemData.EmptyItem;

			var reference = abilityItem.ability;
			var addressableReference = reference.reference;
			var abilityAsset = await addressableReference.LoadAssetTaskAsync(LifeTime);
			var data = abilityAsset.data;
			
			_abilityAssetMap[data.id] = abilityAsset;

			return data;
		}

		public async UniTask<bool> EquipAsync(int abilityId, int slotType)
		{
			var abilityData = _profileData.abilities;
			var abilityItem = await GetAbilityDataAsync(abilityId);
			if (abilityItem == AbilityItemData.EmptyItem) return false;
            
			if(_abilitySlotData.slots.All(x => x.id != slotType))
				return false;

			var abilitySlot = abilityData.FirstOrDefault(x => x.slotType == slotType);

			if (abilitySlot == null)
			{
				abilitySlot = new AbilitySlotData() {slotType = slotType};
				abilityData.Add(abilitySlot);
			}
            
			abilitySlot.ability = abilityItem.id;
			return true;
		}

		public async UniTask<AbilityItemData> CreateAbilityAsync(AbilitySlotId slotType)
		{
			await UpdateAllAvailableAbilityItems();

			if (!_availableAbilitySlotMap.TryGetValue(slotType, out var abilitySlotItems))
				return AbilityItemData.EmptyItem;
            
			var result = abilitySlotItems.Count <= 0 
				? AbilityItemData.EmptyItem
				: abilitySlotItems.GetRandomValue();
            
			return result;
		}

		private async UniTask UpdateAllAvailableAbilityItems()
		{
			foreach (var value in _availableAbilitySlotMap)
				value.Value.Clear();

			var abilityTasks = ListPool<UniTask<AbilityItemData>>.Get();

			foreach (var itemData in _allAbilities)
			{
				if (_profileData.inventory.Contains(itemData)) continue;
				abilityTasks.Add(GetAbilityDataAsync(itemData));
			}

			var abilities = await UniTask.WhenAll(abilityTasks);

			foreach (var item in abilities)
			{
				var slotId = item.data.slotType;
                    
				if (!_availableAbilitySlotMap.TryGetValue(slotId, out var items))
				{
					items = new List<AbilityItemData>();
					_availableAbilitySlotMap[slotId] = items;
				}
                
				if(items.Contains(item)) continue;
				
				items.Add(item);
			}

			abilityTasks.Despawn();
		}
	}
}