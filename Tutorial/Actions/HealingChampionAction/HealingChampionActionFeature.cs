namespace Game.Ecs.Gameplay.Tutorial.Actions.HealingChampionAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Healing Champion Action", 
		fileName = "Healing Champion Action Feature")]
	public class HealingChampionActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Heals champion
			ecsSystems.Add(new HealingChampionActionSystem());
		}
	}
}