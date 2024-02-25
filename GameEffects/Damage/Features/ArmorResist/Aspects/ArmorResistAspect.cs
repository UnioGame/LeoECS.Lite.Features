namespace Game.Ecs.Gameplay.ArmorResist.Aspects
{
	using System;
	using Ability.Common.Components;
	using Characteristics.ArmorResist.Components;
	using Damage.Components.Request;
	using Effects.Components;
	using GameEffects.DamageEffect.DamageTypes.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Armor resist aspect.
	/// </summary>
	[Serializable]
	public class ArmorResistAspect : EcsAspect
	{
		// Armor resist value
		public EcsPool<ArmorResistComponent> ArmorResist;
		// Default ability
		public EcsPool<DefaultAbilityComponent> DefaultAbility;
		// Types of damage
		public EcsPool<PhysicsDamageComponent> PhysicsDamage;
		
		// request
		// Apply damage request. Need to recalculate damage by armor resist
		public EcsPool<ApplyDamageRequest> ApplyDamageRequest;
	}
}