namespace Game.Ecs.GameEffects.TeleportEffect.Systems
{
    using System;
    using Components;
    using Core;
    using Core.Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessTeleportEffectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<EffectComponent> _effectPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<EntityAvatarComponent> _avatarPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<TeleportEffectComponent>()
                .Inc<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectPool.Get(entity);
                if (!effect.Source.Unpack(_world, out var sourceEntity)
                    || !_transformPool.Has(sourceEntity) || !_avatarPool.Has(sourceEntity))
                    continue;

                if (!effect.Destination.Unpack(_world, out var destinationEntity)
                    || !_transformPool.Has(destinationEntity) || !_avatarPool.Has(destinationEntity))
                    continue;

                ref var sourceTransform = ref _transformPool.Get(sourceEntity);
                float3 sourcePosition = sourceTransform.Value.position;

                ref var sourceAvatar = ref _avatarPool.Get(sourceEntity);

                ref var destinationTransform = ref _transformPool.Get(destinationEntity);
                var destinationPosition = destinationTransform.Value.position;

                ref var destinationAvatar = ref _avatarPool.Get(destinationEntity);

                var teleportPosition = EntityHelper
                    .GetPoint(sourcePosition,  destinationPosition, ref destinationAvatar.Bounds);
                
                var point = teleportPosition - sourcePosition;
                var direction = math.normalize(point);
                sourceTransform.Value.position = teleportPosition - direction * sourceAvatar.Bounds.Radius * 4.0f;
            }
        }
    }
}