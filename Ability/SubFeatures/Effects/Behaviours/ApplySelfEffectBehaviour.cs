namespace Game.Ecs.Ability.SubFeatures.Effects.Behaviours
{
	using System;
	using System.Collections.Generic;
	using Code.Configuration.Runtime.Ability.Description;
	using Code.Configuration.Runtime.Effects.Abstract;
	using Ecs.Effects.Components;
	using Leopotam.EcsLite;
	using UnityEngine;

	[Serializable]
	public class ApplySelfEffectBehaviour : IAbilityBehaviour
	{
		[SerializeReference] 
		public List<IEffectConfiguration> Effects = new List<IEffectConfiguration>();
		
		public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
		{
			var selfEffectsPool = world.GetPool<SelfEffectsComponent>();
			ref var effectsComponent = ref selfEffectsPool.Add(abilityEntity);
			effectsComponent.Effects.AddRange(Effects);
		}
	}
}