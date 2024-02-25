namespace Game.Ecs.Characteristics.ArmorResist.Systems
{
	using System;
	using Aspects;
	using Base.Components;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Update armor resist value by characteristic value.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class UpdateArmorResistSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private ArmorResistAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<CharacteristicChangedComponent<ArmorResistComponent>>()
				.Inc<CharacteristicComponent<ArmorResistComponent>>()
				.Inc<ArmorResistComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var characteristicComponent = ref _aspect.Characteristic.Get(entity);
				ref var armorResistComponent = ref _aspect.ArmorResist.Get(entity);
				armorResistComponent.Value = characteristicComponent.Value;
			}
		}
	}
}