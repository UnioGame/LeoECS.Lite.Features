namespace Game.Ecs.Characteristics.ManaRegeneration.Systems
{
	using Base.Components.Requests.OwnerRequests;
	using Components;
	using Leopotam.EcsLite;
	using Mana.Components;
	using Time.Service;
	using UniCore.Runtime.ProfilerTools;
	using UniModules.UniCore.Runtime.Time;
	using UnityEngine;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
	

	/// <summary>
	/// Regenerate mana.
	/// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	public class ProcessManaRegenerationSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<ManaRegenerationComponent> _manaRegenerationPool;
		private EcsPool<ManaRegenerationTimerComponent> _manaRegenerationTimerPool;
		private EcsPool<ManaComponent> _manaPool;
		private EcsPool<ChangeCharacteristicBaseRequest<ManaComponent>> _manaChangePool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			
			_filter = _world.Filter<ManaRegenerationComponent>()
				.Inc<ManaRegenerationTimerComponent>()
				.Inc<ManaComponent>()
				.End();
			
			_manaRegenerationPool = _world.GetPool<ManaRegenerationComponent>();
			_manaRegenerationTimerPool = _world.GetPool<ManaRegenerationTimerComponent>();
			_manaChangePool = _world.GetPool<ChangeCharacteristicBaseRequest<ManaComponent>>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var manaRegenerationComponent = ref _manaRegenerationPool.Get(entity);
				ref var manaRegenerationTimerComponent = ref _manaRegenerationTimerPool.Get(entity);
				
				if (GameTime.Time < manaRegenerationTimerComponent.LastTickTime)
					continue;
				
				var manaRegeneration = manaRegenerationComponent.Value * manaRegenerationTimerComponent.TickTime;
				manaRegenerationTimerComponent.LastTickTime = GameTime.Time + manaRegenerationTimerComponent.TickTime;
				
				var requestEntity = _world.NewEntity();
				ref var request = ref _manaChangePool.Add(requestEntity);
				
				request.Value = manaRegeneration;
				request.Source = _world.PackEntity(entity);
				request.Target = _world.PackEntity(entity);
			}
		}
	}
}