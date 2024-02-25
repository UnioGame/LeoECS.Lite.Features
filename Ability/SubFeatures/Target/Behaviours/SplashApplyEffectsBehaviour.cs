namespace Game.Ecs.Ability.SubFeatures.Target.Behaviours
{
	using System;
	using System.Collections.Generic;
	using Abstract;
	using Code.Configuration.Runtime.Ability.Description;
	using Code.Configuration.Runtime.Effects.Abstract;
	using Components;
	using Leopotam.EcsLite;
	using UnityEngine;

	[Serializable]
	public class SplashApplyEffectsBehaviour : IAbilityBehaviour
	{
		[SerializeReference]
		public IZoneTargetsDetector _zoneTargetsDetector;
		[SerializeReference]
		public List<IEffectConfiguration> _mainTargetEffects = new List<IEffectConfiguration>();
		[SerializeReference]
		public List<IEffectConfiguration> _otherTargetsEffects = new List<IEffectConfiguration>();

		public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
		{
			var splashPool = world.GetPool<SplashEffectSourceComponent>();
			ref var splashComponent = ref splashPool.Add(abilityEntity);
			splashComponent.MainTargetEffects.AddRange(_mainTargetEffects);
			splashComponent.OtherTargetsEffects.AddRange(_otherTargetsEffects);
			splashComponent.ZoneTargetsDetector = _zoneTargetsDetector;
		}
	}
}