namespace Game.Ecs.Ability.SubFeatures.Area.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using Target.Aspects;
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
    public sealed class RotateToAreaSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private AreaAspect _areaAspect;
        private TargetAbilityAspect _aspect;
        
        private EcsPool<RotateToPointSelfRequest> _rotateRequestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AreableAbilityComponent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityValidationSelfRequest>()
                .Inc<AreaLocalPositionComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _areaAspect.Owner.Get(entity);
                
                if (!owner.Value.Unpack(_world, out var ownerEntity)) continue;
                if(!_areaAspect.CanLookAtPool.Has(ownerEntity)) continue;

                ref var ownerTransform = ref _areaAspect.Position.Get(ownerEntity);
                var ownerPosition = ownerTransform.Position;
                ref var areaPosition = ref _areaAspect.AreaPosition.Get(entity);

                ref var request = ref _aspect.RotateTo.GetOrAddComponent(ownerEntity);
                request.Point = ownerPosition + areaPosition.Value;
            }
        }
    }
}