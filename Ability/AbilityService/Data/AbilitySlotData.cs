namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using System.Collections.Generic;
	using Sirenix.OdinInspector;
	using UnityEngine.Serialization;

#if UNITY_EDITOR
	using UniModules.Editor;
#endif

	[Serializable]
	public class AbilitySlotData
	{
		public const string EmptySlot = "Empty";
        
		[ValueDropdown(nameof(GetAbilitySlots))]
		public int slotType;

		[ValueDropdown(nameof(GetAbilityIds))]
		public int ability;

		public IEnumerable<ValueDropdownItem<int>> GetAbilitySlots()
		{
			var slots = AbilitySlotId.GetAbilitySlots();
			foreach (var slot in slots)
			{
				yield return new ValueDropdownItem<int>()
				{
					Text = slot.Text,
					Value = (int)slot.Value,
				};
			}
		}

		public IEnumerable<ValueDropdownItem<int>> GetAbilityIds()
		{
#if UNITY_EDITOR
			var abilityItems = AssetEditorTools.GetAssets<AbilityItemAsset>();
            
			if (abilityItems == null)
			{
				yield return new ValueDropdownItem<int>()
				{
					Text = EmptySlot,
					Value = -1,
				};
				yield break;
			}
            
			foreach (var item in abilityItems)
			{
				var itemData = item.data;
				var data = itemData.data;
				
				if(data.slotType != slotType)
					continue;

				var itemName = string.IsNullOrEmpty(itemData.visualDescription.name)
					? item.name
					: itemData.visualDescription.name;

				yield return new ValueDropdownItem<int>()
				{
					Text = itemName,
					Value = itemData.id,
				};
			}
#endif
			yield break;
		}
	}
}