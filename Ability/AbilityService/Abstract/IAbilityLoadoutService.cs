namespace Game.Code.Services.AbilityLoadout.Abstract
{
	using System.Collections.Generic;
	using Ability.Data;
	using Cysharp.Threading.Tasks;
	using Data;
	using UniGame.GameFlow.Runtime.Interfaces;

	public interface IAbilityLoadoutService : IGameService
	{
		int[] AllAbilities { get; }
		
		int[] InventoryAbilities { get; }
        
		IReadOnlyList<AbilitySlot> Slots { get; }

		IReadOnlyList<int> Inventory { get; }
        
		IReadOnlyList<AbilitySlotData> AbilitySlotData { get; }
		
		AbilityRarityData AbilityRarityData { get; }

		UniTask<AbilityItemData> GetAbilityDataAsync(int abilityId);

		UniTask<bool> EquipAsync(int abilityId, int slotType);

		UniTask<AbilityItemData> CreateAbilityAsync(AbilitySlotId slotType);
	}
}