namespace Game.Ecs.Ability.SubFeatures.ComeToTarget.Systems
{
    using System;
    using Aspects;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Components;
    using Core;
    using Core.Components;
    using Core.Death.Components;
    using Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using Target.Components;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class UpdateComePointFromTargetSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private ComeFromTargetAspect _aspect;
        
        private EntityVector[] _entityPoints = new EntityVector[TargetSelectionData.MaxTargets];
        private float3[] _points = new float3[TargetSelectionData.MaxTargets];
    
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<UpdateComePointComponent>()
                .Inc<AbilityTargetsComponent>()
                .Inc<OwnerComponent>()
                .Inc<RadiusComponent>()
                .Inc<AbilityInHandComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _aspect.Owner.Get(entity);
                
                if(!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;
                
                ref var targets = ref _aspect.Targets.Get(entity);
                if (targets.Count == 0 || _aspect.Dead.Has(ownerEntity))
                {
                    _aspect.Deferred.Del(entity);
                    _aspect.Update.Del(entity);
                    continue;
                }
                
                ref var ownerTransformComponent = ref _aspect.Position.Get(ownerEntity);
                var ownerPosition = ownerTransformComponent.Position;
                var counter = 0;
                
                for (var i = 0; i < targets.Count; i++)
                {
                    var target = targets.Entities[i];
                    if(!target.Unpack(_world, out var selectedEntity)) continue;
                                    
                    ref var selectedTransform = ref _aspect.Position.Get(selectedEntity);
                    var position = selectedTransform.Position;
                    
                    if (_aspect.Avatar.Has(selectedEntity))
                    {
                        ref var avatar = ref _aspect.Avatar.Get(selectedEntity);
                        position = EntityHelper.GetPoint(ownerPosition, position, ref avatar.Bounds);
                    }
                    
                    _entityPoints[counter] = new EntityVector(selectedEntity, position);;
                    _points[counter] = position;
                    
                    counter++;
                }
                
                var gravityCenter = AbilityHelper.FindGravityCenter(_points,counter);
                ref var radius = ref _aspect.Radius.Get(entity);
                var distance = math.distance(gravityCenter, ownerPosition);
                
                var delta = distance - radius.Value;
                if (delta < 0.0f || Mathf.Approximately(delta, 0.0f))
                {
                    _aspect.Update.Del(entity);
                    _aspect.Revoke.GetOrAddComponent(ownerEntity);
                    continue;
                }

                var directionPoint = gravityCenter - ownerPosition;
                var direction = math.normalize(directionPoint);
                var point = ownerPosition + direction * delta;

                ref var comeTarget = ref _aspect.Point
                    .GetOrAddComponent(ownerEntity);
                comeTarget.Value = point;
            }
        }
    }
}