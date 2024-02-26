namespace Game.Ecs.Gameplay.Tutorial.Triggers.StepTrigger
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialTrigger/Step Trigger Feature", 
		fileName = "Step Trigger Feature")]
	public class StepTriggerFeature : TutorialFeature
	{
		public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.Add(new StepTriggerSystem());
			
			return UniTask.CompletedTask;
		}
	}
}