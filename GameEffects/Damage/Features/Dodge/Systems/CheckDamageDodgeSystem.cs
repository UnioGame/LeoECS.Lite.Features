namespace Game.Ecs.Gameplay.Dodge.Systems
{
    using Damage.Components.Request;
    using Events;
    using Game.Ecs.Characteristics.Attack.Components;
    using Game.Ecs.Characteristics.Dodge.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class CheckDamageDodgeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly int _minDodge;
        private readonly int _maxDodge;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ApplyDamageRequest> _requestPool;
        private EcsPool<DodgeComponent> _dodgePool;

        public CheckDamageDodgeSystem(int minDodge = 0, int maxDodge = 100)
        {
            _minDodge = minDodge;
            _maxDodge = maxDodge;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ApplyDamageRequest>().End();
            
            _requestPool = _world.GetPool<ApplyDamageRequest>();
            _dodgePool = _world.GetPool<DodgeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _filter)
            {
                ref var request = ref _requestPool.Get(requestEntity);
                if(!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;
                
                if(!_dodgePool.Has(destinationEntity))
                    continue;
                
                ref var dodgeComponent = ref _dodgePool.Get(destinationEntity);
                var dodgeChance = dodgeComponent.Value;
                var chance = Random.Range(_minDodge, _maxDodge);
                var isDodge = chance < dodgeChance;
                
                if(!isDodge) continue;

                var eventEntity = _world.NewEntity();
                ref var missedEvent = ref _world.AddComponent<MissedEvent>(eventEntity);
                missedEvent.Source = request.Source;
                missedEvent.Destination = request.Destination;
                
                _requestPool.Del(requestEntity);
            }
        }
    }
}