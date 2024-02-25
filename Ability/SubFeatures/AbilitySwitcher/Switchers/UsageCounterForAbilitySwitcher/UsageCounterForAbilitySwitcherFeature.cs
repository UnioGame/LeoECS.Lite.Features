namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.UsageCounterForAbilitySwitcher
{
	using System;
	using AbilitySequence.Tools;
	using Cysharp.Threading.Tasks;
	using Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Abstracts;
	using Leopotam.EcsLite;
	using Systems;
	using Tools;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	/// <summary>
	/// Feature for ability switcher that uses counter to switch between abilities.
	/// </summary>
	[Serializable]
	[CreateAssetMenu(menuName = "Game/Feature/Ability/AbilitySwitcherConfigurations/Usage Counter Ability Switcher Feature", 
		fileName = "Usage Counter Ability Switcher Feature")]
	public class UsageCounterForAbilitySwitcherFeature : AbilitySwitcherAssetFeature
	{
		private AbilitySequenceTools _abilitySequenceTools;
		private AbilityTools _abilityTools;
		
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			var world = ecsSystems.GetWorld();
			_abilitySequenceTools = world.GetGlobal<AbilitySequenceTools>();
			_abilityTools = world.GetGlobal<AbilityTools>();
			
			// Counts usages of ability and switches it to another ability after count of usages.
			ecsSystems.Add(new UsageCounterForAbilitySwitcherSystem(_abilityTools));
		}
	}
}