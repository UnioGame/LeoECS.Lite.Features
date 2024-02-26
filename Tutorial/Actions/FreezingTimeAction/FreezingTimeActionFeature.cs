namespace Game.Ecs.Gameplay.Tutorial.Actions.FreezingTimeAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Freezing Time Action Feature", 
		fileName = "Freezing Time Action Feature")]
	public class FreezingTimeActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Freezes time
			ecsSystems.Add(new FreezingTimeActionSystem());
		}
	}
}