namespace Game.Ecs.GameEffects.ManaEffect.Behaviours
{
	using System;
	using Characteristics.Mana.Components;
	using Code.Configuration.Runtime.Ability.Description;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine.Serialization;

	[Serializable]
	public class ManaCostBehaviour : IAbilityBehaviour
	{
		public float mana;
		public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
		{
			ref var manaCostComponent = ref world.AddComponent<ManaCostComponent>(abilityEntity);
			manaCostComponent.Mana = mana;
		}
	}
}