namespace Game.Ecs.GameEffects.ManaEffect
{
	using System;
	using Characteristics.Base.Components.Requests.OwnerRequests;
	using Characteristics.Mana.Components;
	using Effects;
	using Effects.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Push effect to change mana value.
	/// </summary>
	[Serializable]
	public class ManaEffectConfiguration : EffectConfiguration
	{
		public float mana;
		protected override void Compose(EcsWorld world, int effectEntity)
		{
			var effectPool = world.GetPool<EffectComponent>();
			ref var effectComponent = ref effectPool.Get(effectEntity);
			if (!effectComponent.Source.Unpack(world, out var source))
				return;

			var manaRequestEntity = world.NewEntity();
			ref var changeManaComponent = ref world
				.AddComponent<ChangeCharacteristicBaseRequest<ManaComponent>>(manaRequestEntity);
			changeManaComponent.Value = mana;
			changeManaComponent.Target = effectComponent.Destination;
			changeManaComponent.Source = effectComponent.Source;
		}
	}
}