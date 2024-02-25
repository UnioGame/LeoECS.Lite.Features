namespace Game.Ecs.GameEffects.BlockAutoAttackEffect.Systems
{
	using Ability.Aspects;
	using Ability.Common.Components;
	using Ability.Tools;
	using Components;
	using Effects.Aspects;
	using Effects.Components;
	using Leopotam.EcsLite;
	using Time.Service;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Add an empty target to an ability
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[ECSDI]
	public class ProcessBlockAutoAttackEffectSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private AbilityAspect _abilityAspect;
		private AbilityOwnerAspect _abilityOwnerAspect;
		private EffectAspect _effectAspect;
		
		private EcsPool<BlockAutoAttackEffectReadyComponent> _blockAttackEffectReadyPool;
		private EcsPool<BlockAutoAttackEffectComponent> _blockAttackEffectPool;
		private AbilityTools _abilityTools;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_abilityTools = _world.GetGlobal<AbilityTools>();
			
			_filter = _world
				.Filter<BlockAutoAttackEffectComponent>()
				.Exc<BlockAutoAttackEffectReadyComponent>()
				.End();
			
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var effectComponent = ref _effectAspect.Effect.Get(entity);
				if (!effectComponent.Destination.Unpack(_world, out var target))
					continue;

				var abilityEntity = _abilityTools.TryGetAbility(target, 0);
				if (abilityEntity < 0) continue;
				
				_blockAttackEffectReadyPool.Add(entity);
				ref var blockAttackEffectComponent = ref _blockAttackEffectPool.Get(entity);
				
				ref var pauseAbilityComponent = ref _abilityAspect.Pause.GetOrAddComponent(abilityEntity);
				pauseAbilityComponent.Duration = GameTime.Time + blockAttackEffectComponent.Duration;
			}
		}
	}
}