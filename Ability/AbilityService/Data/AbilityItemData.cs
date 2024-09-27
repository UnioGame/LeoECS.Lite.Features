namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using Ability.Data;
	using Configuration.Runtime.Ability;
	using Configuration.Runtime.Ability.Description;
	using Sirenix.OdinInspector;
	using UniGame.Core.Runtime;
	using UniModules.UniCore.Runtime.Utils;
	using UnityEngine;
	using UnityEngine.AddressableAssets;
	using UnityEngine.Serialization;

	[Serializable]
	public class AbilityItemData : IUnique, ISearchFilterable
	{
		public static readonly AbilityItemData EmptyItem = new AbilityItemData();

		[TitleGroup("settings")]
		public int id;
		
		[TitleGroup("settings")]
		[InlineProperty]
		[HideLabel]
		public AbilityData data = new AbilityData();

		[TitleGroup("behaviour")]
		public AssetReferenceT<AbilityConfiguration> configurationReference;
		
		[TitleGroup("visual")]
		[InlineProperty]
		[HideLabel]
		[SerializeField]
		public AbilityVisualDescription visualDescription;
		
		public int Id => id;
		
		public bool IsMatch(string searchString)
		{
			if (string.IsNullOrEmpty(searchString)) return true;
			
			var slotName = AbilitySlotId.GetSlotName((AbilitySlotId)data.slotType);
			if(CheckString(slotName, searchString)) return true;
			if(CheckString(id.ToStringFromCache(), searchString)) return true;

			return false;
		}
		
		private bool CheckString(string target, string search)
		{
			if (string.IsNullOrEmpty(target)) return false;
			return target.Contains(search, StringComparison.OrdinalIgnoreCase);
		}
		
	}
}