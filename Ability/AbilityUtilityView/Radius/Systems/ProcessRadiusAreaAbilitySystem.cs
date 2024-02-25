namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Systems
{
    using Component;
    using Core.Components;
    using Leopotam.EcsLite;
    using SubFeatures.Area.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessRadiusAreaAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AreableAbilityComponent>()
                .Inc<OwnerComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var ownerPool = _world.GetPool<OwnerComponent>();
            var radiusViewDataPool = _world.GetPool<RadiusViewDataComponent>();
            var avatarPool = _world.GetPool<EntityAvatarComponent>();
            var radiusViewPool = _world.GetPool<RadiusViewComponent>();
            
            foreach (var entity in _filter)
            {
                ref var owner = ref ownerPool.Get(entity);
                if (!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;

                if (!radiusViewDataPool.Has(ownerEntity) || !avatarPool.Has(ownerEntity))
                    continue;
                
                ref var radiusViewData = ref radiusViewDataPool.Get(ownerEntity);
                ref var avatar = ref avatarPool.Get(ownerEntity);
                
                ref var radiusView = ref radiusViewPool.GetOrAddComponent(entity);
                radiusView.RadiusView = radiusViewData.ValidRadiusView;
                radiusView.Root = avatar.Feet;
            }
        }
    }
}