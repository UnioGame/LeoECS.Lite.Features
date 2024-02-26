namespace Game.Ecs.Gameplay.Tutorial.Triggers.DistanceTrigger
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Systems;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialTrigger/Distance Trigger Feature", 
		fileName = "Distance Trigger Feature")]
	public class DistanceTriggerFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Sends request to run tutorial actions when champion is in trigger distance.
			ecsSystems.Add(new TutorialTriggerDistanceSystem());
		}
	}
}