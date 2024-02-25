namespace Game.Ecs.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Data;
    using Leopotam.EcsLite;
    using Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// select parent by effect root id
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
    public sealed class SelectChildParentByRootIdSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EffectAspect _effectAspect;
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
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);
                if(!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;
                
                if(!_effectAspect.Position.Has(destinationEntity))
                    continue;
                
                ref var idComponent = ref _effectAspect.EffectRootId.Get(entity);
                var key = _roots[idComponent.Value];
                if(!key.isChild) continue;
                
                ref var targetTransformComponent = ref _effectAspect.Transform.Get(destinationEntity);
                var transform = targetTransformComponent.Value;
                var gameObject = transform.gameObject;
                
                ref var parentComponent = ref _effectAspect.Parent.Add(entity);
                var parentValue = gameObject.FindChildGameObject(key.objectName);         
                parentComponent.Value = parentValue == null ? null : parentValue.transform;
            }
        }
    }
}