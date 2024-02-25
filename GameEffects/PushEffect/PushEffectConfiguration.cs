namespace Game.Ecs.GameEffects.PushEffect
{
	using System;
	using Components;
	using DG.Tweening;
	using Effects;
	using Leopotam.EcsLite;
	using Sirenix.OdinInspector;
	using UnityEngine.Serialization;

	[Serializable]
	public class PushEffectConfiguration : EffectConfiguration
	{
		#region Inspector
		[BoxGroup("Push Effect")]
		public bool forwardFromSource;
		[BoxGroup("Push Effect")]
		public float distance;
		[BoxGroup("Push Effect")]
		public float durationOffset;
		[BoxGroup("Push Effect")]
		public Ease ease;

		#endregion
		
		protected override void Compose(EcsWorld world, int effectEntity)
		{
			ref var dataComponent = ref world.GetPool<PushEffectDataComponent>().Add(effectEntity);
			dataComponent.FromSource = forwardFromSource;
			dataComponent.Distance = distance;
			dataComponent.DurationOffset = durationOffset;
			dataComponent.Ease = ease;
		}
	}
}