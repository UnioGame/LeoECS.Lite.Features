namespace Game.Ecs.Ability.Tools
{
    using System;
    using System.Runtime.CompilerServices;
    using AbilityInventory.Components;
    using Animation.Data;
    using Animations.Components;
    using Animations.Components.Requests;
    using Aspects;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Characteristics.Radius.Component;
    using Code.Animations;
    using Code.Animations.EffectMilestones;
    using Code.Configuration.Runtime.Ability;
    using Code.Configuration.Runtime.Ability.Description;
    using Code.Services.AbilityLoadout.Data;
    using Common.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
    using Cysharp.Threading.Tasks;
    using GameLayers.Category.Components;
    using GameLayers.Relationship.Components;
    using global::Ability.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using SubFeatures.AbilityAnimation.Components;
    using Time.Service;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public class AbilityTools : IEcsInitSystem
    {
        private EcsFilter _abilityFilter;
        private EcsFilter _abilityInUseFilter;
        private EcsFilter _existsAbilityFilter;
        private AbilityOwnerAspect _abilityOwnerAspect;
        private AbilityAspect _abilityAspect;

        private EcsWorld _world;

        public EcsPool<RadiusComponent> _radius;
        private EcsPool<AbilityIdComponent> _abilityIdPool;
        private EcsPool<AbilityUsingComponent> _abilityInUsePool;
        private EcsPool<CategoryIdComponent> _category;
        private EcsPool<RelationshipIdComponent> _relationship;
        private EcsPool<EquipAbilityIdSelfRequest> _eqiupAbilityPool;
        private EcsPool<EquipAbilityReferenceSelfRequest> _eqiupAbilityReferencePool;
        private EcsPool<AbilityIdComponent> _abiilityIdPool;
        private EcsPool<AbilityMapComponent> _abilityMapPool;
        private EcsPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;
        private EcsPool<AbilityInHandComponent> _abilityInHandPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<AbilityVisualComponent> _visual;
        private EcsPool<IconComponent> _icon;
        private EcsPool<DescriptionComponent> _description;
        private EcsPool<NameComponent> _name;
        private EcsPool<CooldownStateComponent> _cooldownState;
        private EcsPool<CooldownComponent> _cooldown;
        private EcsPool<BaseCooldownComponent> _baseCooldown;
        private EcsPool<DurationComponent> _duration;
        private EcsPool<AbilityActiveAnimationComponent> _animation;
        private EcsPool<UserInputAbilityComponent> _input;
        private EcsPool<AbilityBlockedComponent> _blocked;
        private EcsPool<AbilitySlotComponent> _slot;
        private EcsPool<AbilityIdComponent> _id;
        private EcsPool<OwnerLinkComponent> _ownerLink;
        private EcsPool<CompleteAbilitySelfRequest> _completeAbilityPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _world = world;
            _abilityFilter = world
                .Filter<AbilityIdComponent>()
                .Inc<ActiveAbilityComponent>()
                .Inc<OwnerComponent>()
                .End();

            _abilityInUseFilter = world
                .Filter<AbilityIdComponent>()
                .Inc<AbilityUsingComponent>()
                .Inc<ActiveAbilityComponent>()
                .Inc<OwnerComponent>()
                .End();

            _existsAbilityFilter = _world
                .Filter<AbilityIdComponent>()
                .Inc<OwnerLinkComponent>()
                .Inc<ActiveAbilityComponent>()
                .End();
        }

        public void BuildAbility(
            int abilityEntity,
            ref EcsPackedEntity ownerEntity,
            AbilityConfiguration abilityConfiguration,
            ref AbilityBuildData buildData)
        {
            var packedAbility = _world.PackEntity(abilityEntity);
            ref var slotComponent = ref _slot.GetOrAddComponent(abilityEntity);
            ref var abilityIdComponent = ref _id.GetOrAddComponent(abilityEntity);
            ref var ownerComponent = ref _ownerPool.GetOrAddComponent(abilityEntity);
            ref var ownerLinkComponent = ref _ownerLink.GetOrAddComponent(abilityEntity);
            
            ownerLinkComponent.Value = ownerEntity;
            ownerComponent.Value = ownerEntity;
            slotComponent.SlotType = buildData.Slot;
            abilityIdComponent.AbilityId = buildData.AbilityId;
            
            if (buildData.IsUserInput) _input.GetOrAddComponent(abilityEntity);
            if (buildData.IsBlocked) _blocked.GetOrAddComponent(abilityEntity);
                
            //add visual
            if (_visual.Has(abilityEntity))
            {
                ref var visualComponent = ref _visual.Get(abilityEntity);
                ComposeAbilityVisualDescription(ref visualComponent,abilityEntity);
            }
                
            ComposeAbilitySpecification(abilityConfiguration.specification, abilityEntity);
            
            var abilityLink = abilityConfiguration.animationLink.reference;

            ref var durationComponent = ref _duration.GetOrAddComponent(abilityEntity);
            durationComponent.Value = abilityConfiguration.duration;
            ref var milestonesComponent = ref _world.GetOrAddComponent<AbilityEffectMilestonesComponent>(abilityEntity);
            milestonesComponent.Milestones = new[]
            {
                new EffectMilestone { Time = 0f }
            };
            
            if (abilityConfiguration.useAnimation)
            {
#if UNITY_EDITOR
                if (abilityLink == null || !abilityLink.RuntimeKeyIsValid())
                {
                    Debug.LogError($"Missing ability animation link FOR {abilityConfiguration.name}");
                }
#endif
                switch (abilityConfiguration.animationType)
                {
                    case AnimationType.Animator:
                        ComposeAbilityAnimation(_world, ownerEntity, packedAbility,abilityConfiguration.animationClipId);
                        ref var abilityCooldownComponent = ref _abilityAspect.AbilityCooldownValues.GetOrAddComponent(abilityEntity);
                        abilityCooldownComponent.baseCooldown = abilityConfiguration.specification.Cooldown;
                        abilityCooldownComponent.currentCooldown = abilityConfiguration.specification.Cooldown;
                        break;
                    case AnimationType.PlayableDirector:
                        ComposeAbilityAnimationAsync(_world, ownerEntity,packedAbility,abilityLink).Forget();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var abilityBehaviour in abilityConfiguration.abilityBehaviours)
                abilityBehaviour.Compose(_world, abilityEntity, buildData.IsDefault);
        }

        private void ComposeAbilityAnimation(EcsWorld world,
            EcsPackedEntity animationTarget,
            EcsPackedEntity ability,
            AnimationClipId clipId)
        {
            
            if(!ability.Unpack(world,out var abilityEntity)) return;
            
            if (clipId == string.Empty)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarning($"There is no animation clip id with {clipId}");
#endif
                return;
            }
            
            ref var triggeredAnimationIdComponent = ref world.GetOrAddComponent<TriggeredAnimationIdComponent>(abilityEntity);
            triggeredAnimationIdComponent.animationId = (string)clipId;
            //todo trim ability cooldown to animation duration if cooldown is bigger
            
            _abilityAspect.AwaitAnimationTrigger.GetOrAddComponent(abilityEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAbilityById(ref EcsPackedEntity ownerEntity, AbilityId abilityId, out int resultAbility)
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                ref var ownerComponent = ref _ownerPool.Get(abilityEntity);
                if (!ownerEntity.EqualsTo(ownerComponent.Value)) continue;

                ref var abilityIdComponent = ref _abiilityIdPool.Get(abilityEntity);
                if (abilityId != abilityIdComponent.AbilityId) continue;

                resultAbility = abilityEntity;
                return true;
            }

            resultAbility = -1;
            return false;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetExistsAbility(int abilityId, ref EcsPackedEntity targetEntity)
        {
            foreach (var abilityEntity in _existsAbilityFilter)
            {
                ref var abilityIdComponent = ref _abilityIdPool.Get(abilityEntity);
                ref var ownerComponent = ref _ownerPool.Get(abilityEntity);

                if (abilityIdComponent.AbilityId != abilityId ||
                    !ownerComponent.Value.EqualsTo(targetEntity)) continue;

                return abilityEntity;
            }

            return -1;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetActivatedAbility(int ownerEntity)
        {
	        ref var abilityMap = ref _abilityMapPool.Get(ownerEntity);
	        foreach (var abilityMapAbilityEntity in abilityMap.AbilityEntities)
	        { 
		        if (!abilityMapAbilityEntity.Unpack(_world, out var abilityEntity))
			        continue;
		        if (!_abilityInUsePool.Has(abilityEntity))
			        continue;
		        if (_completeAbilityPool.Has(abilityEntity))
			        continue;
		        return abilityEntity;
	        }
	        return -1;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ChangeInHandAbility(EcsWorld world, int entity, int newAbility)
        {
            ref var targetInHandComponent = ref _abilityInHandLinkPool.Get(entity);
            var abilityEntity = targetInHandComponent.AbilityEntity;

            if (abilityEntity.Unpack(world, out var previousAbility))
            {
                if (previousAbility == newAbility)
                    return;

                _abilityInHandPool.TryRemove(previousAbility);
            }

            if (!_abilityInHandPool.Has(newAbility))
                _abilityInHandPool.Add(newAbility);

            abilityEntity = world.PackEntity(newAbility);
            targetInHandComponent.AbilityEntity = abilityEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAbilityCooldownPassed(int abilityEntity)
        {
            //проверяем кулдаун абилки, если он не прошел - игнорируем
            if (!_world.EntityHasAll<CooldownComponent, CooldownStateComponent>(abilityEntity))
                return true;

            ref var coolDownComponent = ref _world.GetComponent<CooldownComponent>(abilityEntity);
            ref var coolDownStateComponent = ref _world.GetComponent<CooldownStateComponent>(abilityEntity);
            var timePassed = (GameTime.Time - coolDownStateComponent.LastTime) - coolDownComponent.Value;
            return timePassed > 0;
        }


#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetAbilityBySlot(int ownerEntity, int slotId)
        {
            ref var abilityMapComponent = ref _abilityOwnerAspect.AbilityMap.Get(ownerEntity);
            var abilityMap = abilityMapComponent.AbilityEntities;

            if (slotId < 0 || slotId >= abilityMap.Count)
                return AbilitySlotId.EmptyAbilitySlot;

            var packedAbility = abilityMap[slotId];
            return packedAbility.Unpack(_world, out var abilityEntity)
                ? abilityEntity
                : AbilitySlotId.EmptyAbilitySlot;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetAbilitySlot(EcsWorld world, int ownerEntity, int abilityEntity)
        {
            ref var abilityMap = ref _abilityMapPool.Get(ownerEntity);
            var counter = 0;

            foreach (var abilityPacked in abilityMap.AbilityEntities)
            {
                if (!abilityPacked.Unpack(world, out var targetAbility)) continue;
                if (abilityEntity == targetAbility) return counter;
                counter++;
            }

            return AbilitySlotId.EmptyAbilitySlot;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAnyAbilityInUse(int entity)
        {
            if (!_abilityMapPool.Has(entity)) return false;
            
            ref var abilityMapComponent = ref _abilityMapPool.Get(entity);
            var abilityMap = abilityMapComponent.AbilityEntities;
            
            for (var i = 0; i < abilityMap.Count; i++)
            {
                var packedAbility = abilityMap[i];
                if(!packedAbility.Unpack(_world,out var abilityEntity))
                    continue;

                if (_abilityInUsePool.Has(abilityEntity))
                    return true;
            }
            
            return false;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetNonDefaultAbilityInUse(int entity)
        {
            if(!_abilityOwnerAspect.AbilityInProcessing.Has(entity))
                return -1;
            
            ref var processingComponent = ref _abilityOwnerAspect
                .AbilityInProcessing.Get(entity);

            if (processingComponent.IsDefault) return -1;
            
            if(!processingComponent.Ability.Unpack(_world,out var abilityEntity))
                return -1;
            
            return abilityEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EquipAbilityById(ref EcsPackedEntity owner, ref AbilityId abilityId)
        {
            return EquipAbilityById(ref owner, abilityId, AbilitySlotId.EmptyAbilitySlot);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EquipAbilityById(ref EcsPackedEntity owner, AbilityId abilityId,
            AbilitySlotId slot, bool isDefault = false)
        {
            var activeAbility = GetActiveAbility(ref owner, ref abilityId);
            if (activeAbility > 0) return activeAbility;

            var requestEntity = _world.NewEntity();
            ref var request = ref _eqiupAbilityPool.Add(requestEntity);
            request.AbilityId = abilityId;
            request.AbilitySlot = slot;
            request.Owner = owner;
            request.IsDefault = isDefault;
            request.IsUserInput = false;

            return requestEntity;
        }
        
        
#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EquipAbilityByReference(ref EcsPackedEntity owner, 
            AbilityConfiguration configuration,
            AbilitySlotId slot, bool isDefault = false)
        {
            var requestEntity = _world.NewEntity();
            ref var request = ref _eqiupAbilityReferencePool.Add(requestEntity);
            request.AbilitySlot = slot;
            request.Owner = owner;
            request.IsDefault = isDefault;
            request.IsUserInput = false;
            request.Reference = configuration;

            return requestEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetActiveAbility(ref EcsPackedEntity owner, ref AbilityId abilityId)
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                ref var ownerComponent = ref _ownerPool.Get(abilityEntity);
                if (!owner.EqualsTo(ownerComponent.Value)) continue;

                ref var abilityIdComponent = ref _abiilityIdPool.Get(abilityEntity);
                if (abilityId != abilityIdComponent.AbilityId) continue;

                return abilityEntity;
            }

            return -1;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IsActiveAbilityEntity(ref EcsPackedEntity owner, int targetAbility)
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                ref var ownerComponent = ref _ownerPool.Get(abilityEntity);
                if (!owner.EqualsTo(ownerComponent.Value)) continue;

                if (targetAbility != abilityEntity) continue;
                return abilityEntity;
            }

            return -1;
        }
        
#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ComposeAbilityVisualDescription(ref AbilityVisualComponent visualDescription, int abilityEntity)
        {
            ref var visualComponent = ref _visual.GetOrAddComponent(abilityEntity);
            visualComponent.Description = visualDescription.Description;
            visualComponent.Icon = visualDescription.Icon;
            visualComponent.Name = visualDescription.Name;

            ref var iconComponent = ref _icon.GetOrAddComponent(abilityEntity);
            ref var descriptionComponent = ref _description.GetOrAddComponent(abilityEntity);
            ref var nameComponent = ref _name.GetOrAddComponent(abilityEntity);
            
            nameComponent.Value = visualDescription.Name;
            descriptionComponent.Description = visualDescription.Description;
            iconComponent.Value = visualDescription.Icon;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ComposeAbilitySpecification(AbilitySpecification specification, int abilityEntity)
        {
            ref var radiusComponent = ref _radius.GetOrAddComponent(abilityEntity);
            ref var category = ref _category.GetOrAddComponent(abilityEntity);
            ref var relationship = ref _relationship.GetOrAddComponent(abilityEntity);
            ref var abilityCooldownComponent = ref _cooldownState.GetOrAddComponent(abilityEntity);
            ref var cooldownComponent = ref _cooldown.GetOrAddComponent(abilityEntity);
            ref var baseCooldown = ref _baseCooldown.GetOrAddComponent(abilityEntity);
            
            baseCooldown.Value = specification.Cooldown;
            cooldownComponent.Value = specification.Cooldown;
            abilityCooldownComponent.LastTime = GameTime.Time - specification.Cooldown;
            radiusComponent.Value = specification.Radius;
            relationship.Value = specification.RelationshipId;
            category.Value = specification.CategoryId;
        }
        

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask ComposeAbilityAnimationAsync(EcsWorld world, 
            EcsPackedEntity animationTarget,
            EcsPackedEntity abilityEntity,
            AssetReferenceT<AnimationLink> animationLinkReference)
        {
            var lifetime = world.GetWorldLifeTime();
            var animationLink = await animationLinkReference.LoadAssetTaskAsync(lifetime);
            ComposeAbilityAnimation(world,ref animationTarget,ref abilityEntity, animationLink);
        }
        
        #if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ComposeAbilityAnimation(EcsWorld world, 
            ref EcsPackedEntity animationTarget,
            ref EcsPackedEntity ability,
            AnimationLink animationLink)
        {
            if(!ability.Unpack(world,out var abilityEntity)) return;
            
            ref var activeAnimationComponent = ref _animation.GetOrAddComponent(abilityEntity);
            ref var durationComponent = ref _duration.GetOrAddComponent(abilityEntity);

            if (animationLink == null)
            {
                ComposeEffectMilestones(world, null, 0.0f, abilityEntity);
                return;
            }
            
            var animation = animationLink.animation;
            var duration = animationLink.duration;
            duration = duration <= 0 && animation!=null ? (float)animation.duration : duration;
                
            durationComponent.Value = duration;
                
            ref var animationComponent = ref world.GetOrAddComponent<AnimationDataLinkComponent>(abilityEntity);
            ref var wrapModeComponent = ref world.GetOrAddComponent<AnimationWrapModeComponent>(abilityEntity);
            ref var linkToAnimationComponent = ref world.GetOrAddComponent<LinkToAnimationComponent>(abilityEntity);

            var animationEntity = _world.NewEntity();
            ref var createAnimationRequest = ref _world
                .GetOrAddComponent<CreateAnimationLinkSelfRequest>(animationEntity);
                
            var packedAnimation = world.PackEntity(animationEntity);
                
            createAnimationRequest.Data = animationLink;
            createAnimationRequest.Owner = world.PackEntity(abilityEntity);
            createAnimationRequest.Target = animationTarget;
                
            linkToAnimationComponent.Value = packedAnimation;
            activeAnimationComponent.Value = packedAnimation;
                
            animationComponent.AnimationLink = animationLink;
            wrapModeComponent.Value = animationLink.wrapMode;
                
            ComposeEffectMilestones(world, animationLink.milestones, animationLink.Duration, abilityEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ComposeEffectMilestones(EcsWorld world, EffectMilestonesData milestonesInfo, float duration,
            int abilityEntity)
        {
            if (milestonesInfo == null) return;
            
            ref var effectMilestones = ref world.GetOrAddComponent<AbilityEffectMilestonesComponent>(abilityEntity);

            var effects = milestonesInfo.effectMilestones;
            if (effects.Count == 0)
            {
                effectMilestones.Milestones = new[]
                {
                    new EffectMilestone { Time = duration }
                };

                return;
            }

            effectMilestones.Milestones = new EffectMilestone[effects.Count];
            
            for (var i = 0; i < effectMilestones.Milestones.Length; i++)
            {
                var sourceMilestone = effects[i];
                var cloneMilestone = sourceMilestone.Clone();
                effectMilestones.Milestones[i] = cloneMilestone;
            }
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbility(EcsWorld world, int entity, int abilityEntity)
        {
            var packedAbilityEntity = world.PackEntity(abilityEntity);
            ref var setInHand = ref world.GetOrAddComponent<SetInHandAbilitySelfRequest>(entity);
            setInHand.Value = packedAbilityEntity;

            ref var abilityUseRequest = ref world.GetOrAddComponent<ApplyAbilitySelfRequest>(entity);
            abilityUseRequest.Value = packedAbilityEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbility(EcsWorld world, int abilityEntity)
        {
            if (!world.HasComponent<OwnerComponent>(abilityEntity)) return;

            ref var ownerComponent = ref world.GetComponent<OwnerComponent>(abilityEntity);
            if (!ownerComponent.Value.Unpack(world, out var ownerEntity)) return;

            ActivateAbility(world, ownerEntity, abilityEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilitySlot(EcsWorld world, int entity, int slot)
        {
            ref var setInHand = ref world.GetOrAddComponent<SetInHandAbilityBySlotSelfRequest>(entity);
            setInHand.AbilityCellId = slot;

            var abilityInputPool = world.GetPool<ApplyAbilityBySlotSelfRequest>();
            ref var abilityUseRequest = ref abilityInputPool.GetOrAddComponent(entity);
            abilityUseRequest.AbilitySlot = slot;
        }
        
#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilityId(ref EcsPackedEntity entity, int abilityId)
        {
            var requestEntity = _world.NewEntity();
            ref var activateRequest = ref _world.GetOrAddComponent<ActivateAbilityByIdRequest>(requestEntity);
            activateRequest.AbilityId = abilityId;
            activateRequest.Target = entity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public bool TryGetInHandAbility(EcsWorld world, int entity, out int abilityEntity)
        {
            abilityEntity = -1;
            var abilityInHandPool = world.GetPool<AbilityInHandLinkComponent>();
            if (!abilityInHandPool.Has(entity))
                return false;

            ref var inHandAbility = ref abilityInHandPool.Get(entity);
            if (!inHandAbility.AbilityEntity.Unpack(world, out var ability))
                return false;

            abilityEntity = ability;
            return true;
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public bool TryGetDefaultAbility(EcsWorld world, int entity, out int abilityEntity)
        {
            abilityEntity = -1;
            var abilityMapPool = world.GetPool<AbilityMapComponent>();
            var defaultPool = world.GetPool<DefaultAbilityComponent>();

            if (!abilityMapPool.Has(entity)) return false;

            ref var map = ref abilityMapPool.Get(entity);

            foreach (var mapAbilityEntity in map.AbilityEntities)
            {
                if (!mapAbilityEntity.Unpack(world, out var abilityEntityValue)) continue;
                if (!defaultPool.Has(abilityEntityValue)) continue;
                abilityEntity = abilityEntityValue;
                return true;
            }

            return false;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int TryGetAbility(int entity, int slot)
        {
            var abilityEntity = -1;

            if (!_abilityOwnerAspect.AbilityMap.Has(entity)) return abilityEntity;

            ref var map = ref _abilityOwnerAspect.AbilityMap.Get(entity);
            var count = map.AbilityEntities.Count;
            var isInBounds = slot >= 0 && count > slot;
            if(!isInBounds) return abilityEntity;
            
            return map.AbilityEntities[slot].Unpack(_world, out var ability) 
                ? ability : abilityEntity;
        }

    }
}