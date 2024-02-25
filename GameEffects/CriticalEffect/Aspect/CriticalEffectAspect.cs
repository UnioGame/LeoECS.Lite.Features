namespace Game.Ecs.GameEffects.CriticalEffect.Aspect
{
	using System;
	using Components;
	using Effects.Components;
	using Gameplay.CriticalAttackChance.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
	[Serializable]
	public class CriticalEffectAspect : EcsAspect
	{
		public EcsPool<CriticalAttackMarkerComponent> CriticalAttackMarker;
		public EcsPool<CriticalEffectComponent> CriticalEffect;
		public EcsPool<CriticalEffectReadyComponent> CriticalEffectReady;
		public EcsPool<EffectComponent> Effect;
	}
}