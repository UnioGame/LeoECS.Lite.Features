namespace Game.Code.Services.AbilityLoadout.Data
{
	using System.Collections.Generic;
	using DataBase.Runtime;
	using DataBase.Runtime.Abstract;
	using Sirenix.OdinInspector;
	using UniGame.AddressableTools.Runtime.AssetReferencies;

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
		public string id = "Ability";
		
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
		[PropertyOrder(2)]
		[ListDrawerSettings(ListElementLabelName = "@Label")]
		public List<AbilityRecord> abilities = new List<AbilityRecord>();

		public override IReadOnlyList<IGameDatabaseRecord> Records => abilities;

		public AbilityRecord Find(int recordId)
		{
			foreach (var abilityRecord in abilities)
			{
				if (abilityRecord.id == recordId)
					return abilityRecord;
			}
			return AbilityRecord.Empty;
		}
		
		[Button(ButtonSizes.Large,Icon = SdfIconType.ArchiveFill)]
		public override void FillCategory()
		{
#if UNITY_EDITOR
			abilities.Clear();
            
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
                
				abilities.Add(record);
			}

			this.MarkDirty();
#endif
		}
		
#if UNITY_EDITOR

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