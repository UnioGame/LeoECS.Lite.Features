namespace Game.Ecs.Gameplay.ArmorResist.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Damage.Components.Request;
	using Leopotam.EcsLite;
	using UniGame.Core.Runtime.Extension;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Recalculated damage value by armor resist.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RecalculatedDamageArmorResistSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private ArmorResistAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<ApplyDamageRequest>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var request = ref _aspect.ApplyDamageRequest.Get(entity);
				if (!request.Effector.Unpack(_world, out var effectorEntity))
					continue;
				if (!_aspect.PhysicsDamage.Has(effectorEntity))
					continue;
				if (!request.Destination.Unpack(_world, out var destinationEntity))
					continue;
				if (!_aspect.ArmorResist.Has(destinationEntity))
					continue;
				ref var armorResistComponent = ref _aspect.ArmorResist.Get(destinationEntity);
				var damage = request.Value;
				var resist = armorResistComponent.Value / 100f;
				request.Value = damage - damage * resist;
			}
		}
	}
}