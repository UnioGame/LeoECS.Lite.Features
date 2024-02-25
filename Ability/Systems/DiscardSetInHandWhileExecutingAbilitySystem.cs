namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// initialize ui equip view with link to its data
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DiscardSetInHandWhileExecutingAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private EcsFilter _filter;
        private EcsWorld _world;
        private AbilityOwnerAspect _abilityOwnerAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<SetInHandAbilitySelfRequest>()
                .Inc<AbilityMapComponent>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                var executingAbility = _abilityTools.GetNonDefaultAbilityInUse(entity);
                if(executingAbility < 0) continue;
                
                _abilityOwnerAspect.SetInHandAbility.Del(entity);
            }
        }
    }
}