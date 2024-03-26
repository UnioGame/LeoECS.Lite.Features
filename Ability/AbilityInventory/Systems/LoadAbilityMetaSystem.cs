namespace Game.Ecs.AbilityInventory.Systems
{
    using System.Collections.Generic;
    using Ability.Common.Components;
    using Aspects;
    using Code.Services.AbilityLoadout.Abstract;
    using Code.Services.AbilityLoadout.Data;
    using Components;
    using Converters;
    using Cysharp.Threading.Tasks;
    using Equip.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Initialize meta data for all abilities
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [ECSDI]
    public class LoadAbilityMetaSystem : IEcsRunSystem,IEcsInitSystem
    {
        private IAbilityLoadoutService _abilityLoadoutService;
        private AbilityInventoryTool _converter;
        private AbilityMetaAspect _metaAspect;
        private AbilityInventoryAspect _inventoryAspect;
        
        private ILifeTime _lifeTime;
        private EcsWorld _world;
        private EcsFilter _filter;
        private NativeHashSet<int> _loadingAbilities;
        private bool _loaded;
        private EcsFilter _metaFilter;
        private List<AbilityItemData> _abilityItems = new();

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _lifeTime = _world.GetWorldLifeTime();
            _abilityLoadoutService = _world.GetGlobal<IAbilityLoadoutService>();
            _converter = _world.GetGlobal<AbilityInventoryTool>();
            
            _loadingAbilities = new NativeHashSet<int>(
                    _abilityLoadoutService.AllAbilities.Length, 
                Allocator.Persistent)
                .AddTo(_lifeTime);

            _metaFilter = _world
                .Filter<AbilityMetaComponent>()
                .Inc<AbilityIdComponent>()
                .End();
            
            _filter = _world
                .Filter<LoadAbilityMetaRequest>()
                .End();
        }
		
        public void Run(IEcsSystems systems)
        {
            foreach (var itemData in _abilityItems)
            {
                var entity = _world.NewEntity();
                _converter.Convert(itemData, entity);
            }
            
            _abilityItems.Clear();
            
            foreach (var entity in _filter)
            {
                ref var requestComponent = ref _inventoryAspect.LoadMeta.Get(entity);
                var found = _loadingAbilities.Contains(requestComponent.AbilityId);

                if (!found)
                {
                    foreach (var metaEntity in _metaFilter)
                    {
                        ref var abilityMetaIdComponent = ref _metaAspect.Id.Get(metaEntity);
                        if (abilityMetaIdComponent.AbilityId != requestComponent.AbilityId) continue;
                        found = true;
                        break;
                    }
                }
                
                if (found)
                {
                    _inventoryAspect.LoadMeta.Del(entity);
                    continue;
                }
                
                //mark ability meta as loading
                _loadingAbilities.Add(requestComponent.AbilityId);
                //start ability meta loading
                LoadAbilities(requestComponent.AbilityId).Forget();
            }
        }

        private async UniTask LoadAbilities(int record)
        {
            var abilityData = await _abilityLoadoutService.GetAbilityDataAsync(record);
            _abilityItems.Add(abilityData);
        }
    }
}