namespace Game.Code.Services.AbilityLoadout.Data
{
	using System.Collections.Generic;
	using Sirenix.OdinInspector;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Configurations/Ability/Ability Slots Map",fileName = nameof(AbilitySlotsData))]
	public class AbilitySlotsData : ScriptableObject
	{
		[InlineProperty]
		public List<AbilitySlot> slots = new List<AbilitySlot>();
	}
}