namespace Game.Ecs.AbilityInventory.Components
{
	using System.Collections.Generic;
	using Code.Services.Ability.Data;

	public struct AbilityRarityMapComponent
	{
		public AbilityRaritySlot DisableSlot;
		public List<AbilityRaritySlot> Slots;
	}
}