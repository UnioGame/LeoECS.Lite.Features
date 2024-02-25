namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using System;
    using Aspects;
    using Code.GameTools.Runtime;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using TargetSelection;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;

    /// <summary>
    /// Target detection system in the area of the ability
    /// </summary>

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RectangleZoneDetectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<RectangleZoneDetectionComponent> _zoneDetectionPool;

        private TargetAbilityAspect _targetAspect;
        private ILifeTime _lifeTime;
        private NativeHashSet<int> _nativeHashSet;
        
        private EcsPackedEntity[] _targets = new EcsPackedEntity[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _lifeTime = _world.GetWorldLifeTime();
            _nativeHashSet = new NativeHashSet<int>(TargetSelectionData.MaxTargets, 
                    Allocator.Persistent).AddTo(_lifeTime);
            
            _filter = _world
                .Filter<AbilityTargetsComponent>()
                .Inc<RectangleZoneDetectionComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _nativeHashSet.Clear();
                
                ref var ownerComponent = ref _targetAspect.Owner.Get(entity);
                if (!ownerComponent.Value.Unpack(_world, out var rootObject))
                    continue;

                ref var zoneDetectionComponent = ref _zoneDetectionPool.Get(entity);
                var zoneOffset = zoneDetectionComponent.Offset;
                var zoneSize = zoneDetectionComponent.Size;
                
                ref var transformComponentSource = ref _targetAspect.Position.Get(rootObject);
                ref var directionComponent = ref _targetAspect.Direction.Get(rootObject);
                ref var abilityTargets = ref _targetAspect.AbilityTargets.Get(entity);

                ref var sourcePosition = ref transformComponentSource.Position;
                ref var forward = ref directionComponent.Forward;
                ref var right = ref directionComponent.Right;
                
                var amount = abilityTargets.Count;
                var counter = 0;
                
                for (var i = 0; i < amount; i++)
                {
                    ref var packedEntity = ref abilityTargets.Entities[i];
                    if(!packedEntity.Unpack(_world,out var targetEntity))
                        continue;
                    
                    if(_nativeHashSet.Contains(targetEntity)) continue;
                    if(_targetAspect.PrepareToDeath.Has(targetEntity)) continue;
                    if (_targetAspect.Position.Has(targetEntity) == false) continue;
                    if (_targetAspect.Direction.Has(targetEntity) == false) continue;
                    
                    ref var transformTargetComponent = ref _targetAspect.Position.Get(targetEntity);
                    var positionTarget = transformTargetComponent.Position;
                    
                    if (!ZoneDetectionMathTool.IsPointWithin(positionTarget,
                            sourcePosition,forward,right, zoneOffset, zoneSize))
                        continue;

                    _targets[counter] = packedEntity;
                    
                    counter++;
                }

                abilityTargets.SetEntities(_targets,counter);
            }
        }
    }
}