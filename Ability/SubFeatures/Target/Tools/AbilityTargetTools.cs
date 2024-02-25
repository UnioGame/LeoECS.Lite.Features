namespace Game.Ecs.Ability.SubFeatures.Target.Tools
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Ability.Aspects;
    using Aspects;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
	public class AbilityTargetTools : IEcsInitSystem
	{
        private EcsPackedEntity[] _buffer = new EcsPackedEntity[TargetSelectionData.MaxTargets];
        private EcsWorld _world;

        private AbilityAspect _abilityAspect;
        private TargetAbilityAspect _targetAbilityAspect;
        private AbilityOwnerAspect _ownerAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
#if ENABLE_IL2CPP
	    [Il2CppSetOption(Option.NullChecks, false)]
	    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilityForTarget(int entity, int targetEntity, int slot)
        {
            var packedEntity = _world.PackEntity(targetEntity);
            ActivateAbilityForTarget( entity, packedEntity, slot);
        }
        
#if ENABLE_IL2CPP
	    [Il2CppSetOption(Option.NullChecks, false)]
	    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearAbilityTargets(int entity, int slot)
        {
            if (!TryGetAbility(entity, slot, out var abilityEntity))
                return;

            ClearAbilityTargets(abilityEntity);
        }

#if ENABLE_IL2CPP
	    [Il2CppSetOption(Option.NullChecks, false)]
	    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearAbilityTargets(int abilityEntity)
        {
            //set ability target
            ref var targetsComponent = ref _targetAbilityAspect
                .AbilityTargets
                .GetOrAddComponent(abilityEntity);
            
            targetsComponent.SetEmpty();
        }

#if ENABLE_IL2CPP
	    [Il2CppSetOption(Option.NullChecks, false)]
	    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public void SetAbilityTarget(int entity, EcsPackedEntity targetEntity, int slot)
        {
            if (!TryGetAbility(entity, slot, out var abilityEntity)) return;

            //set ability target
            ref var targetsComponent = ref _targetAbilityAspect
                .AbilityTargets
                .GetOrAddComponent(abilityEntity);
            
            targetsComponent.SetEntity(targetEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public void ActivateAbilityForTarget(int entity, EcsPackedEntity targetEntity, int slot)
        {
            if (!TryGetAbility(entity, slot, out var abilityEntity))
                return;

            ActivateAbilityWithTarget(entity, abilityEntity,ref targetEntity);
        }
        
#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilityWithTarget(int entity,int abilityEntity,ref EcsPackedEntity targetEntity)
        {
            //set ability target
            ref var targetsComponent = ref _targetAbilityAspect
                .AbilityTargets
                .GetOrAddComponent(abilityEntity);
            
            targetsComponent.SetEntity(targetEntity);

            //activate ability
            ActivateAbility( entity, abilityEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilityWithTargets(EcsWorld world, int entity,
            int abilityEntity,EcsPackedEntity[] targets,int count)
        {
            for (var i = 0; i < count; i++)
                _buffer[i] = targets[i];
            
            //set ability target
            ref var targetsComponent = ref _targetAbilityAspect
                .AbilityTargets
                .GetOrAddComponent(abilityEntity);
            
            targetsComponent.SetEntities(_buffer, count);

            //activate ability
            ActivateAbility( entity, abilityEntity);
        }
        

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilityWithTargets(int entity, int abilityEntity,IReadOnlyList<EcsPackedEntity> targets)
        {
            var count = targets.Count;
            for (var i = 0; i < targets.Count; i++)
                _buffer[i] = targets[i];
            
            //set ability target
            ref var targetsComponent =  ref _targetAbilityAspect
                .AbilityTargets
                .GetOrAddComponent(abilityEntity);
            
            targetsComponent.SetEntities(_buffer, count);

            //activate ability
            ActivateAbility( entity, abilityEntity);
        }
        
#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public void ActivateAbility(int ownerEntity, int abilityEntity)
        {
            var packedAbilityEntity = _world.PackEntity(abilityEntity);
            ref var setInHand = ref _ownerAspect.SetInHandAbility.GetOrAddComponent(ownerEntity);
            ref var abilityUseRequest = ref _ownerAspect.ApplyAbility.GetOrAddComponent(ownerEntity);
            
            setInHand.Value = packedAbilityEntity;
            abilityUseRequest.Value = packedAbilityEntity;
        }
        
#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAbility(int entity, int slot, out int abilityEntity)
        {
            abilityEntity = -1;
            var minLength = slot + 1;

            if (!_ownerAspect.AbilityMap.Has(entity))
                return false;

            ref var map = ref _ownerAspect.AbilityMap.Get(entity);

            if (map.AbilityEntities.Count < minLength || 
                !map.AbilityEntities[slot].Unpack(_world, out var ability))
                return false;

            abilityEntity = ability;
            return true;
        }


    }
}