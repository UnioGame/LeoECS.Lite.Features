namespace Game.Ecs.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Data;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// select parent from global effect targets
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;
	/// <summary>
	/// Assembling ability
	/// </summary>
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SelectGlobalParentByRootIdSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _rootsFilter;
        private EcsFilter _globalFilter;
        
        private EffectAspect _effectAspect;
        private EffectGlobalAspect _effectGlobalAspect;
        private EffectTargetAspect _targetAspect;
        
        private EffectsRootData _effectsRootData;
        private EffectRootKey[] _roots;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _effectsRootData = _world.GetGlobal<EffectsRootData>();
            _roots = _effectsRootData.roots;
            
            _filter = _world
                .Filter<EffectAppliedSelfEvent>()
                .Inc<EffectRootIdComponent>()
                .Exc<EffectParentComponent>()
                .End();

            _rootsFilter = _world
                .Filter<EffectRootTargetComponent>()
                .Inc<TransformComponent>()
                .Inc<EffectRootIdComponent>()
                .End();

            _globalFilter = _world
                .Filter<EffectGlobalRootMarkerComponent>()
                .Inc<EffectRootTransformsComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var idComponent = ref _effectAspect.EffectRootId.Get(entity);
                ref var parentComponent = ref _effectAspect.Parent.Add(entity);
                
                foreach (var globalEntity in _globalFilter)
                {
                    ref var globalTransformsComponent = ref _effectGlobalAspect.Transforms.Get(globalEntity);
                    var targetParent = globalTransformsComponent.Value[idComponent.Value];
                    parentComponent.Value = targetParent;
                    
                    if(targetParent !=null) break;

                    //rebuild global transforms cache
                    foreach (var rootEntity in _rootsFilter)
                    {
                        ref var rootIdComponent = ref _targetAspect.Id.Get(rootEntity);
                        ref var transformComponent = ref _targetAspect.Transform.Get(rootEntity);
                        var targetTransform = transformComponent.Value;
                        if(targetTransform == null) continue;
                        globalTransformsComponent.Value[rootIdComponent.Value] = transformComponent.Value;
                    }
                    
                    parentComponent.Value =  globalTransformsComponent.Value[idComponent.Value];
                }
            }
        }
    }
}