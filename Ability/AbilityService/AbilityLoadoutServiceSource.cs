namespace Game.Code.Services.AbilityLoadout
{
	using Ability.Data;
	using Ability.Data.Arena;
	using Abstract;
	using Cysharp.Threading.Tasks;
	using Data;
	using UniGame.AddressableTools.Runtime;
	using UniGame.Context.Runtime.Extension;
	using UniGame.Core.Runtime;
	using UniGame.GameFlow.Runtime.Services;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	[CreateAssetMenu(menuName = "Game/Services/AbilityLoadoutService Source", fileName = "AbilityLoadoutService Source")]
	public class AbilityLoadoutServiceSource : DataSourceAsset<IAbilityLoadoutService>
	{
		public AssetReferenceT<AbilityDataBase> abilityDatabase;
		public AssetReferenceT<AbilitySlotsData> abilitySlotMap;
		public AssetReferenceT<AbilityRarityData> abilityRarityData;
		
		protected override async UniTask<IAbilityLoadoutService> CreateInternalAsync(IContext context)
		{
			var data = new AbilityLoadoutData();
			
			var abilityDataBase = await abilityDatabase.LoadAssetInstanceTaskAsync(context.LifeTime, true);
			var slotMap = await abilitySlotMap.LoadAssetInstanceTaskAsync(context.LifeTime, true);
			var rarityMap = await abilityRarityData.LoadAssetInstanceTaskAsync(context.LifeTime, true);
			
			data.abilityDataBase = abilityDataBase;
			data.abilitySlotMap = slotMap;
			data.abilityRarityData = rarityMap;

			var profileData = new AbilityProfileData();
			context.Publish(profileData);
            
			var service = new AbilityLoadoutService(profileData,data);
			return service;
		}
		
	}
}