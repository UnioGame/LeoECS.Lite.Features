namespace Game.Ecs.GameEffects.BlockAutoAttackEffect
{
	using Cysharp.Threading.Tasks;
	using Effects.Feature;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Effects/Block Auto Attack Effect Feature")]
	public class BlockAutoAttackEffectFeature : EffectFeatureAsset
	{
		protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// Adds the effect of blocking auto attack on an ability.
			ecsSystems.Add(new ProcessBlockAutoAttackEffectSystem());
			// Removes the effect of blocking auto attack on an ability. Check PauseAbilityComponent.
			// If duration is over, then remove the effect.
			ecsSystems.Add(new RemoveBlockAutoAttackEffectSystem());
			return UniTask.CompletedTask;
		}
	}
}