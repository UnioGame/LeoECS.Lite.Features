namespace Game.Ecs.Gameplay.Tutorial.Actions.HealingChampionAction.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Characteristics.Health.Components;
	using Code.Configuration.Runtime.Effects;
	using Components;
	using Core.Components;
	using GameEffects.HealingEffect;
	using Leopotam.EcsLite;
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Heals the champion
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class HealingChampionActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private HealingChampionActionAspect _aspect;
		private EcsFilter _championFilter;
		private EcsFilter _actionFilter;
		private HealingEffectConfiguration _healingEffectConfiguration;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_championFilter = _world
				.Filter<ChampionComponent>()
				.Inc<HealthComponent>()
				.End();
			
			_actionFilter = _world
				.Filter<HealingChampionActionComponent>()
				.Exc<CompletedHealingChampionActionComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var actionEntity in _actionFilter)
			{
				if (_championFilter.GetEntitiesCount() == 0) 
					continue;
				var championEntity = _championFilter.GetRawEntities().FirstOrDefault();
				var healthComponent = _aspect.Healths.Get(championEntity);
				ref var healingActionComponent = ref _aspect.HealingChampionAction.Get(actionEntity);
				var currentHealth = healthComponent.Health;
				var maxHealth = healthComponent.MaxHealth;
				
				var healOverMax = healingActionComponent.HealOverMax;
				var healDuration = healingActionComponent.HealDuration;
				var healPeriod = healingActionComponent.HealPeriod;
				
				_healingEffectConfiguration = new HealingEffectConfiguration()
				{
					duration = healDuration,
					periodicity = healPeriod,
					healingValue = 0.3f,
					targetType = TargetType.Target,
				};

				if(Mathf.Approximately(maxHealth,currentHealth)) continue;

				var healingEntity = _world.NewEntity();
                    
				var overrideMaxHealth = healOverMax + maxHealth;
				var difference = overrideMaxHealth - currentHealth;
				var healAtTick = healDuration / healPeriod;
				var healValue = difference / healAtTick;

				_healingEffectConfiguration.healingValue = healValue;
				_healingEffectConfiguration.duration = healDuration;
				_healingEffectConfiguration.ComposeEntity(_world,healingEntity);
                
				ref var applyEffectRequest = ref _aspect.ApplyEffectRequest.Add(healingEntity);
                
				ref var effectComponent = ref _aspect.Effects.Add(healingEntity);
				effectComponent.Destination = _world.PackEntity(championEntity);
				effectComponent.Source = _world.PackEntity(healingEntity);
				
				_aspect.CompletedHealingChampionAction.Add(actionEntity);
			}
		}
	}
}