namespace Game.Ecs.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;

    /// <summary>
    /// Show view effect by effect data
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
    public sealed class ShowEffectViewSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EffectAspect _effectAspect;
        private EffectViewAspect _effectViewAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<EffectAppliedSelfEvent>()
                .Inc<EffectComponent>()
                .Inc<EffectViewDataComponent>()
                .Inc<EffectDurationComponent>()
                .Exc<EffectShowCompleteComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);
                
                if(!effect.Destination.Unpack(_world, out var destinationEntity) || 
                   !_effectAspect.Avatar.Has(destinationEntity))
                    continue;

                ref var avatarComponent = ref _effectAspect.Avatar.Get(destinationEntity);
                ref var viewDataComponent = ref _effectAspect.ViewData.Get(entity);
                ref var durationComponent = ref _effectAspect.Duration.Get(entity);
                ref var parentComponent = ref _effectAspect.Parent.Get(entity);

                var parent = parentComponent.Value;
                
                var effectViewEntity = _world.NewEntity();
                _effectAspect.Parent.Copy(entity,effectViewEntity);
                
                var viewInstance = viewDataComponent.View.Spawn(parent);
                var size = avatarComponent.Bounds.Radius * 2.0f;
                viewInstance.transform.localScale = new Vector3(size, size, size);
                viewInstance.transform.localPosition = Vector3.zero;
                
                ref var effectView = ref _effectViewAspect.View.Add(effectViewEntity);
                ref var owner = ref _effectViewAspect.Owner.Add(effectViewEntity);
                
                var effectViewDuration = viewDataComponent.LifeTime < 0.0f
                    ? durationComponent.Duration
                    : viewDataComponent.LifeTime;
                
                effectView.ViewInstance = viewInstance;
                effectView.DeadTime = Time.time + effectViewDuration;

                var targetPackEntity =  viewDataComponent.AttachToSource 
                    ? effect.Source 
                    : effect.Destination;
                
                owner.Value = targetPackEntity;

                _effectAspect.ShowComplete.Add(entity);
            }
        }
    }
}