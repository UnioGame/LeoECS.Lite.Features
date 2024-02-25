namespace Game.Ecs.Gameplay.Damage.Systems
{
    using Characteristics.Block.Components;
    using Characteristics.Dodge.Components;
    using Characteristics.Dodge.Components.Events;
    using Components.Request;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CheckDamageBlockSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly int _minBlock;
        private readonly int _maxBlock;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ApplyDamageRequest> _requestPool;
        private EcsPool<BlockComponent> _blockPool;
        private EcsPool<BlockableDamageComponent> _canBlockedPool;

        public CheckDamageBlockSystem(int minBlock = 0, int maxBlock = 100)
        {
            _minBlock = minBlock;
            _maxBlock = maxBlock;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ApplyDamageRequest>().End();
            
            _requestPool = _world.GetPool<ApplyDamageRequest>();
            _blockPool = _world.GetPool<BlockComponent>();
            _canBlockedPool = _world.GetPool<BlockableDamageComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _filter)
            {
                ref var request = ref _requestPool.Get(requestEntity);
                if(!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;
                
                if(!request.Source.Unpack(_world, out var sourceEntity))
                    continue;
                
                var effectorAlive = request.Effector.Unpack(_world,out var effectorEntity);
                var isBlockableEffector = effectorAlive && !_canBlockedPool.Has(effectorEntity);
                
                if(!_blockPool.Has(destinationEntity))
                    continue;
                
                if(!_canBlockedPool.Has(sourceEntity) && 
                   !_canBlockedPool.Has(requestEntity) &&
                   !isBlockableEffector)
                    continue;
                
                ref var blockComponent = ref _blockPool.Get(destinationEntity);
                var blockChance = blockComponent.Value;
                var chance = Random.Range(_minBlock, _maxBlock);
                var isBlocked = chance < blockChance;
                
                if(!isBlocked) continue;

                var eventEntity = _world.NewEntity();
                ref var missedEvent = ref _world.AddComponent<BlockedDamageEvent>(eventEntity);
                missedEvent.Source = request.Source;
                missedEvent.Destination = request.Destination;
                
                _requestPool.Del(requestEntity);
            }
        }
    }
}