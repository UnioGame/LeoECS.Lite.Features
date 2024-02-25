namespace Game.Ecs.GameEffects.CriticalEffect
{
	using Cysharp.Threading.Tasks;
	using Effects.Feature;
	using Leopotam.EcsLite;
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Effects/Critical Effect Feature", fileName = "Critical Effect Feature")]
	public class CriticalEffectFeature : EffectFeatureAsset
	{
		protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.Add(new CriticalEffectSystem());
			return UniTask.CompletedTask;
		}
	}
}