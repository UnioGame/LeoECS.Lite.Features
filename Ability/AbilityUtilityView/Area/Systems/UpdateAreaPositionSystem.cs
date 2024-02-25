namespace Game.Ecs.Ability.AbilityUtilityView.Area.Systems
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using SubFeatures.Area.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class UpdateAreaPositionSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<AreaInstanceComponent> _areaInstancePool;
        private EcsPool<AreaLocalPositionComponent> _areaPositionPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<TransformPositionComponent> _transformPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AreaInstanceComponent>()
                .Inc<AreaLocalPositionComponent>()
                .Inc<OwnerComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);
                if(!owner.Value.Unpack(_world, out var ownerEntity) || !_transformPool.Has(ownerEntity))
                    continue;

                ref var ownerTransform = ref _transformPool.Get(ownerEntity);
                ref var areaInstance = ref _areaInstancePool.Get(entity);
                ref var areaPosition = ref _areaPositionPool.Get(entity);

                var ownerPosition = ownerTransform.Position;
                var position = areaPosition.Value;

                areaInstance.Instance.transform.position = ownerPosition + position;
            }
        }
    }
}