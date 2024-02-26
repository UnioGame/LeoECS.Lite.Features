namespace Game.Ecs.Gameplay.Tutorial.Actions.OpenWindowAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Open Window Action Feature", 
		fileName = "Open Window Action Feature")]
	public class OpenWindowActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Open window
			ecsSystems.Add(new OpenWindowActionSystem());
		}
	}
}