namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.TimerForAbilitySwitcher
{
	using System;
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	/// <summary>
	/// Feature for ability switcher that uses counter to switch between abilities.
	/// </summary>
	[Serializable]
	[CreateAssetMenu(menuName = "Game/Feature/Ability/AbilitySwitcherConfigurations/Timer Ability Switcher Feature", 
		fileName = "Timer Ability Switcher Feature")]
	public class TimerForAbilitySwitcherFeature : AbilitySwitcherAssetFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Evaluate timer for ability switcher. Set ready if timer is over.
			ecsSystems.Add(new EvaluateTimerForAbilitySwitcherSystem());
			// Switch ability if timer is ready.Await ApplyAbilitySelfRequest
			// and TimerForAbilitySwitcherReadyComponent
			ecsSystems.Add(new TimerForAbilitySwitcherSystem());
		}
	}
}