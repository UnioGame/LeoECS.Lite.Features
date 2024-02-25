namespace Game.Ecs.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

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
    public sealed class CreateEffectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private EffectAspect _effectAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<CreateEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var effectEntity in _filter)
            {
                ref var request = ref _effectAspect.Create.Get(effectEntity);
                ref var effectComponent = ref _effectAspect.Effect.Add(effectEntity);
                
                request.Destination.Unpack(_world,out var destinationEntity);
                
                if (request.Source.Unpack(_world, out var sourceEntity) && 
                    _effectAspect.Power.Has(sourceEntity))
                {
                    _effectAspect.Power.Copy(sourceEntity, effectEntity);
                }

                effectComponent.Destination = request.Destination;
                effectComponent.Source = request.Source;
                
                request.Effect.ComposeEntity(_world, effectEntity);

                ref var owner = ref _effectAspect.Owner.Add(effectEntity);
                owner.Value = request.Destination;

                ref var list = ref _effectAspect.List.GetOrAddComponent(destinationEntity);
                list.Effects.Add(_world.PackEntity(effectEntity));
            }
        }
    }
}