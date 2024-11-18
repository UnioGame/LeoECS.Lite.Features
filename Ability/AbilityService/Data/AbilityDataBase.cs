namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Cysharp.Threading.Tasks;
	using DataBase.Runtime;
	using DataBase.Runtime.Abstract;
	using Sirenix.OdinInspector;
	using UniGame.Core.Runtime;

#if UNITY_EDITOR
	using UniModules.Editor;
	using UniModules.UniGame.AddressableExtensions.Editor;
#endif
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	/// <summary>
	/// Ability category
	/// </summary>
	[CreateAssetMenu(menuName = "Game/AbilityInventory/Configuration/Ability Database")]
	public sealed class AbilityDataBase : GameDataCategory
	{
		#region inspector
		
		public string id = "Ability";
		
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
		[PropertyOrder(2)]
		[ListDrawerSettings(ListElementLabelName = "@Label")]
		public AbilityRecord[] abilities = Array.Empty<AbilityRecord>();
		
		#endregion

		private Dictionary<string, IGameResourceRecord> _map = new(64);
		private Dictionary<string, IGameResourceRecord[]> _filtersMap = new(64);

		public override Dictionary<string, IGameResourceRecord> Map => _map;
		
		public override UniTask<CategoryInitializeResult> InitializeAsync(ILifeTime lifeTime)
		{
			_map.Clear();
			
			foreach (var abilityRecord in abilities)
			{
				_map[abilityRecord.Id] = abilityRecord;
			}

			return UniTask.FromResult(new CategoryInitializeResult()
			{
				category = this,
				complete = true,
				error = string.Empty,
				categoryName = id,
			});
		}

		public override IGameResourceRecord Find(string filter)
		{
			_map.TryGetValue(filter,out var record);
			return record;
		}

		public override IGameResourceRecord[] FindResources(string filter)
		{
			if(_filtersMap.TryGetValue(filter, out var records))
				return records;
			
			var items = abilities
				.Select(x => x as IGameResourceRecord)
				.Where(x => x.CheckRecord(filter))
				.ToArray();
			
			_filtersMap[filter] = items;
			return items;
		}

		public override IGameResourceRecord[] Records => abilities;

		public AbilityRecord Find(int recordId)
		{
			foreach (var abilityRecord in abilities)
			{
				if (abilityRecord.id == recordId)
					return abilityRecord;
			}
			return AbilityRecord.Empty;
		}
		
#if UNITY_EDITOR
		
		[Button(ButtonSizes.Large,Icon = SdfIconType.ArchiveFill)]
		public override IReadOnlyList<IGameResourceRecord> FillCategory()
		{
			var foundAbilities = new List<AbilityRecord>();
			var abilityItemAssets = AssetEditorTools.GetAssets<AbilityItemAsset>();
			abilityItemAssets.Sort((x,y) => Comparer<int>.Default.Compare(x.Id,y.Id));
			
			foreach (var item in abilityItemAssets)
			{
				if(item == null || !item.IsInAnyAddressableAssetGroup()) continue;
                
				var record = new AbilityRecord();

				var itemData = item.data;
				record.name = item.name;
				record.id = itemData.id;
				record.data = itemData.data;
				
				record.ability = new AssetReferenceAbility()
				{
					reference = new AssetReferenceT<AbilityItemAsset>(item.GetGUID())
				};
                
				foundAbilities.Add(record);
			}

			abilities = foundAbilities.ToArray();
			
			this.MarkDirty();

			return foundAbilities;
		}

		[Button]
		private void ValidateMeta()
		{
			var abilityItemAssets = AssetEditorTools.GetAssets<AbilityItemAsset>();
			foreach (var abilityItemAsset in abilityItemAssets)
			{
				if(abilityItemAsset.data.visualDescription == null)
					Debug.LogError($"Ability {abilityItemAsset.name} has no configuration reference",abilityItemAsset);
			}
		}
		
#endif
	}
}