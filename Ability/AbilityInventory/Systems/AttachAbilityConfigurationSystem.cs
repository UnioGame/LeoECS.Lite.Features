namespace Game.Ecs.AbilityInventory.Systems
{
    using System;
    using System.Collections.Generic;
    using Ability.Common.Components;
    using Aspects;
    using Code.Configuration.Runtime.Ability;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
    using UnityEngine.AddressableAssets;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AttachAbilityConfigurationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterRequest;
        
        private AbilityInventoryAspect _abilityInventory;
        private AbilityMetaAspect _metaAspect;
        private EcsFilter _metaFilter;
        private ILifeTime _lifeTime;
        private List<MetaConfigurationValue> _configurationLoading;
        private NativeHashSet<int> _metaLoading;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _lifeTime = _world.GetWorldLifeTime();
            _configurationLoading = new List<MetaConfigurationValue>();
            _metaLoading = new NativeHashSet<int>(64, Allocator.Persistent).AddTo(_lifeTime);
            
            _filterRequest = _world
                .Filter<EquipAbilitySelfRequest>()
                .Inc<AbilityMetaLinkComponent>()
                .Exc<AbilityBuildingComponent>()
                .Exc<AbilityConfigurationComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var value in _configurationLoading)
            {
                if(!value.MetaEntity.Unpack(_world,out var meta)) continue;
                
                _metaLoading.Remove(meta);
                
                ref var configurationComponent = ref _metaAspect.Configuration.Add(meta);
                configurationComponent.Value = value.Configuration;
            }
            
            _configurationLoading.Clear();
            
            foreach (var requestEntity in _filterRequest)
            {
                ref var metaLinkComponent = ref _abilityInventory.MetaLink.Get(requestEntity);

                if (!metaLinkComponent.Value.Unpack(_world, out var targetMetaEntity))
                {
                    _world.DelEntity(requestEntity);
                    continue;
                }
                
                if (_metaAspect.Configuration.Has(targetMetaEntity))
                {
                    _metaAspect.Configuration.Copy(targetMetaEntity, requestEntity);
                    continue;
                }
                
                if(_metaLoading.Contains(targetMetaEntity)) continue;

                ref var referenceComponent = ref _metaAspect.ConfigurationReference.Get(targetMetaEntity);
                
                _metaLoading.Add(targetMetaEntity);

                var metaPackedEntity = _world.PackEntity(targetMetaEntity);
                
                LoadConfigurationAsync(metaPackedEntity, referenceComponent.AbilityConfiguration).Forget();
            }
        }
        
        private async UniTask LoadConfigurationAsync(EcsPackedEntity meta,
            AssetReferenceT<AbilityConfiguration> referenceComponent)
        {
            var configuration = await referenceComponent
                .LoadAssetInstanceTaskAsync(_lifeTime,true);
            
            _configurationLoading.Add(new MetaConfigurationValue()
            {
                Configuration = configuration,
                MetaEntity = meta,
            });
        }
    }
    
    public struct MetaConfigurationValue
    {
        public EcsPackedEntity MetaEntity;
        public AbilityConfiguration Configuration;
    }
}