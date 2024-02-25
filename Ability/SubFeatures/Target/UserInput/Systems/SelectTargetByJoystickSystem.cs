namespace Game.Ecs.Ability.SubFeatures.Target.UserInput.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Aspects;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using Selection.Components;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SelectTargetByJoystickSystem : IEcsRunSystem,IEcsInitSystem, IComparer<EntityFloat>
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private TargetAbilityAspect _targetAspect;
        
        private EcsPool<RadiusComponent> _radiusPool;
        
        private int[] _unpacked = new int[TargetSelectionData.MaxTargets];
        private EntityFloat[] _distanceEntity = new EntityFloat[TargetSelectionData.MaxTargets];
        private EcsPackedEntity[] _packedEntities = new EcsPackedEntity[TargetSelectionData.MaxTargets];
        private EntityFloatComparer _comparer = new();
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<OwnerComponent>()
                .Inc<AbilityTargetsComponent>()
                .Inc<RadiusComponent>()
                .Inc<SelectedTargetsComponent>()
                .Inc<TargetableAbilityComponent>()
                .Exc<DefaultAbilityComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _targetAspect.Owner.Get(entity);
                if(!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;

                ref var transformComponent = ref _targetAspect.Position.Get(ownerEntity);
                var ourPosition = transformComponent.Position;
                
                ref var radius = ref _radiusPool.Get(entity);
                var sqrRadius = radius.Value * radius.Value;
                
                ref var selectedTargets = ref _targetAspect.SelectedTargets.Get(entity);
                var amount = _world.UnpackAll(selectedTargets.Entities,_unpacked, selectedTargets.Count);
                var index = 0;
                for (var i = 0; i < amount; i++)
                {
                    var targetEntity = _unpacked[i];
                    
                    ref var targetTransformComponent = ref _targetAspect.Position.Get(targetEntity);
                    var distance = math.distancesq(ourPosition, targetTransformComponent.Position);
                    if (distance > sqrRadius) continue;
                    
                    _distanceEntity[index] = new EntityFloat(targetEntity,distance);
                    
                    index++;
                }

                if(index <= 0) continue;

                Array.Sort(_distanceEntity,0,index,_comparer);

                for (int i = 0; i < index; i++)
                {
                    ref var value = ref _distanceEntity[i];
                    var entityValue = value.entity;
                    _packedEntities[i] = _world.PackEntity(entityValue);
                }
                
                ref var abilityTargets = ref _targetAspect.AbilityTargets.Get(entity);
                abilityTargets.SetEntities(_packedEntities,index);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(EntityFloat x, EntityFloat y)
        {
            if (x.entity == 0) return 1;
            if (y.entity == 0) return -1;
            if (x.value - y.value < 0) return -1;
            return 1;
        }

    }
}