namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Systems
{
    using System;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Component;
    using Core;
    using Core.Components;
    using Leopotam.EcsLite;
    using SubFeatures.Selection.Components;
    using SubFeatures.Target.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessRadiusForTargetAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<TransformPositionComponent> _positionPool;
        private EcsPool<RadiusComponent> _radiusPool;
        private EcsPool<SelectedTargetsComponent> _targetsPool;
        private EcsPool<RadiusViewDataComponent> _radiusViewDataPool;
        private EcsPool<EntityAvatarComponent> _avatarPool;
        private EcsPool<RadiusViewComponent> _radiusViewPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<TargetableAbilityComponent>()
                .Inc<AbilityInHandComponent>()
                .Inc<SelectedTargetsComponent>()
                .Inc<OwnerComponent>()
                .Inc<RadiusComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);
                if(!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;

                if(!_radiusViewDataPool.Has(ownerEntity) || !_avatarPool.Has(ownerEntity))
                    continue;

                ref var radiusViewData = ref _radiusViewDataPool.Get(ownerEntity);
                ref var avatar = ref _avatarPool.Get(ownerEntity);
                ref var targets = ref _targetsPool.Get(entity);
                
                ref var ownerTransform = ref _positionPool.Get(ownerEntity);
                var ownerPosition = ownerTransform.Position;
                
                ref var radius = ref _radiusPool.Get(entity);
                
                var hasAnyValidTargets = false;
                var radiusValue = radius.Value * radius.Value;
                
                for (int i = 0; i < targets.Count; i++)
                {
                    ref var packedEntity = ref targets.Entities[i];
                    if(!packedEntity.Unpack(_world, out var targetEntity) || 
                       !_positionPool.Has(targetEntity) || !_avatarPool.Has(targetEntity))
                        continue;

                    ref var targetTransform = ref _positionPool.Get(targetEntity);
                    var targetPosition = targetTransform.Position;
                    ref var targetAvatar = ref _avatarPool.Get(targetEntity);

                    var value = EntityHelper.IsSqrClosest(
                        ref ownerPosition,
                        ref targetPosition, 
                        ref targetAvatar.Bounds, radiusValue);
                    
                    if (!value.isClosest) continue;

                    hasAnyValidTargets = true;
                    break;
                }
                
                var selectedRadiusView = hasAnyValidTargets 
                    ? radiusViewData.ValidRadiusView 
                    : radiusViewData.InvalidRadiusView;

                ref var radiusView = ref _radiusViewPool.GetOrAddComponent(entity);
                radiusView.RadiusView = selectedRadiusView;
                radiusView.Root = avatar.Feet;
            }
        }
    }
}