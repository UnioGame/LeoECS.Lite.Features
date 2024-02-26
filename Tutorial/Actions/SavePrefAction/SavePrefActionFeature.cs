namespace Game.Ecs.Gameplay.Tutorial.Actions.SavePrefAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Save Pref Action Feature", 
		fileName = "Save Pref Action Feature")]
	public class SavePrefActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.Add(new SavePrefSystem());
		}
	}
}