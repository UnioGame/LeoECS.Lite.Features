namespace Game.Code.Services.Ability.Data
{
	using System.Collections.Generic;
	using Sirenix.OdinInspector;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Configurations/AbilityRarity Map", fileName = "Ability Rarity Map")]
	public class AbilityRarityData : ScriptableObject
	{
		public AbilityRaritySlot disableSlot = new AbilityRaritySlot();
        
		[InlineProperty] 
		public List<AbilityRaritySlot> slots = new List<AbilityRaritySlot>();
	}
}