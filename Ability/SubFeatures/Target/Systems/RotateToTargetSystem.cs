namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RotateToTargetSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private TargetAbilityAspect _aspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityTargetsComponent>()
                .Inc<TargetableAbilityComponent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityValidationSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _aspect.Owner.Get(entity);
                
                if(!owner.Value.Unpack(_world, out var ownerEntity)) continue;
                if(!_aspect.CanLookAt.Has(ownerEntity)) continue;
                
                var soloTargetComponent = _aspect.SoloTarget.Get(entity);
                if (!soloTargetComponent.Value.Unpack(_world, out var soloTargetEntity)) continue;
                
                if(!_aspect.Position.Has(soloTargetEntity)) continue;
                if(!_aspect.Direction.Has(soloTargetEntity)) continue;
                
                ref var positionComponent = ref _aspect.Position.Get(soloTargetEntity);
                var gravityCenter = positionComponent.Position;

                ref var request = ref _aspect.RotateTo.GetOrAddComponent(ownerEntity);
                request.Point = gravityCenter;
            }
        }
    }
}