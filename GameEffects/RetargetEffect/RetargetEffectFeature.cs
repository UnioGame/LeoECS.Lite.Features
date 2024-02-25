namespace Game.Ecs.GameEffects.RetargetEffect
{
	using Cysharp.Threading.Tasks;
	using Effects.Feature;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Effects/Retarget Effect Feature", fileName = "Retarget Effect Feature")]
	public class RetargetEffectFeature : EffectFeatureAsset
	{
		protected override async UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.Add(new RetargetEffectSystem());
		}
	}
}