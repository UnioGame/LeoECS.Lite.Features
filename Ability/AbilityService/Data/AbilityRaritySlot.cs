namespace Game.Code.Services.Ability.Data
{
	using System;
	using System.Collections.Generic;
	using AbilityLoadout.Data;
	using Sirenix.OdinInspector;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	[Serializable]
	public class AbilityRaritySlot
	{
		[ValueDropdown(nameof(GetRarity))]
		public int id;

		public AssetReferenceT<Sprite> background;
		
		private IEnumerable<ValueDropdownItem<int>> GetRarity()
		{
			foreach (var abilitySlot in AbilitySlotId.GetAbilitySlots())
			{
				yield return new ValueDropdownItem<int>()
				{
					Text = abilitySlot.Text,
					Value = abilitySlot.Value
				};
			}
		}
	}
}