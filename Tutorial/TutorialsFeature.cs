namespace Game.Ecs.Gameplay.Tutorial
{
	using System.Collections.Generic;
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Leopotam.EcsLite.ExtendedSystems;
	using Sirenix.OdinInspector;
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial Feature", fileName = "Tutorial Feature")]
	public class TutorialsFeature : BaseLeoEcsFeature
	{
		[SerializeReference]
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
		public List<TutorialFeature> tutorialFeatures = new List<TutorialFeature>();
		
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			foreach (var feature in tutorialFeatures)
				await feature.InitializeFeatureAsync(ecsSystems);
			
			// Run tutorial actions. Await RunTutorialActionsRequest.
			ecsSystems.Add(new RunTutorialActionsSystem());
			// Delayed tutorial system. After delay compose tutorial actions/triggers.
			ecsSystems.Add(new DelayedTutorialSystem());
			ecsSystems.DelHere<RunTutorialActionsRequest>();
		}
	}
}