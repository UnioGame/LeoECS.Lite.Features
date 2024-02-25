namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher
{
	using System;
	using System.Collections.Generic;
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Leopotam.EcsLite.ExtendedSystems;
	using Sirenix.OdinInspector;
	using Systems;
	using Target.Tools;
	using Tools;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	/// <summary>
	/// add critical animations if critical hit exist
	/// </summary>
	[Serializable]
	[CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Switcher SubFeature", fileName = "Ability Switcher SubFeature")]
	public class AbilitySwitcherSubFeature : AbilitySubFeature
	{
		#region Inspector

		[SerializeReference]
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
		public List<AbilitySwitcherAssetFeature> features = new List<AbilitySwitcherAssetFeature>();

		#endregion
		
		private AbilityTools _abilityTools;
		
		public override UniTask<IEcsSystems> OnInitializeSystems(IEcsSystems ecsSystems)
		{
			var world = ecsSystems.GetWorld();
			_abilityTools = world.GetGlobal<AbilityTools>();
			
			foreach (var abilitySwitcherAssetFeature in features) 
				abilitySwitcherAssetFeature.InitializeFeatureAsync(ecsSystems);
			
			// Do ability switch. Await for AbilitySwitcherRequest and switch ability.
			ecsSystems.Add(new AbilitySwitcherSystem(_abilityTools));

			ecsSystems.DelHere<AbilitySwitcherRequest>();
			
			return UniTask.FromResult(ecsSystems);
		}
	}
}