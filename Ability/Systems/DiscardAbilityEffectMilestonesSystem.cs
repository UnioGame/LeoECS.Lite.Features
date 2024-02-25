namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Unity.IL2CPP.CompilerServices;

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public sealed class DiscardAbilityEffectMilestonesSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<CompleteAbilitySelfRequest>()
                .Inc<AbilityEffectMilestonesComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var milestonesPool = _world.GetPool<AbilityEffectMilestonesComponent>();

            foreach (var entity in _filter)
            {
                ref var milestones = ref milestonesPool.Get(entity);
                for (var i = 0; i < milestones.Milestones.Length; i++)
                {
                    ref var milestone = ref milestones.Milestones[i];
                    milestone.IsApplied = false;
                }
            }
        }
    }
}