namespace Game.Ecs.Ability.Systems
{
	using System;
	using Components;
	using Leopotam.EcsLite;

	/// <summary>
	/// Ability unlock. Wait for unlock event.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	public class AbilityUnlockSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _eventFilter;
		private EcsPool<AbilityUnlockEvent> _abilityUnlockEventPool;
		private EcsPool<AbilityUnlockComponent> _abilityUnlockPool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_eventFilter = _world.Filter<AbilityUnlockEvent>()
				.End();
			_abilityUnlockEventPool = _world.GetPool<AbilityUnlockEvent>();
			_abilityUnlockPool = _world.GetPool<AbilityUnlockComponent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _eventFilter)
			{
				ref var unlockEvent = ref _abilityUnlockEventPool.Get(entity);
				if (!unlockEvent.Ability.Unpack(_world, out var abilityEntity))
					continue;
				if (_abilityUnlockPool.Has(abilityEntity))
					continue;
				_abilityUnlockPool.Add(abilityEntity);
			}
		}
	}
}