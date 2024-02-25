namespace Game.Ecs.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    public sealed class SelectBakedParentByRootIdSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EffectAspect _effectAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
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
                
                if(!effect.Destination.Unpack(_world, out var destinationEntity) || 
                   !_effectAspect.Transforms.Has(destinationEntity))
                    continue;
                
                ref var transformsComponent = ref _effectAspect.Transforms.Get(destinationEntity);
                ref var idComponent = ref _effectAspect.EffectRootId.Get(entity);
                
                var transforms = transformsComponent.Value;
                var index = idComponent.Value;
                
                var parentValue = transforms.Length <= 0 || transforms.Length <= index 
                    ? null 
                    : transformsComponent.Value[index];
                
                ref var parentComponent = ref _effectAspect.Parent.Add(entity);
                parentComponent.Value = parentValue;
            }
        }
    }
}