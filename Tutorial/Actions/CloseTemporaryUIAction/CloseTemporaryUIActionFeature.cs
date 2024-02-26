namespace Game.Ecs.Gameplay.Tutorial.Actions.CloseTemporaryUIAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Close Temporary UI Action Feature", 
		fileName = "Close Temporary UI Action Feature")]
	public class CloseTemporaryUIActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.Add(new CloseTemporaryUIActionSystem());
		}
	}
}