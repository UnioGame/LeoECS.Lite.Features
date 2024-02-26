namespace Game.Ecs.Gameplay.Tutorial.Actions.EquipAbilityAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Equip Ability Action Feature", 
		fileName = "Equip Ability Action Feature")]
	public class EquipAbilityActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Equip ability to champion
			ecsSystems.Add(new EquipAbilityActionSystem());
		}
	}
}