namespace Game.Ecs.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UnityEngine;
    using Component = UnityEngine.Component;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyPawnGameObjectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public GameResourceAspect _resourceAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<GameSpawnedResourceComponent>()
                .Inc<UnityObjectComponent>()
                .Inc<GameObjectComponent>()
                .Exc<GameSpawnCompleteComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var objectComponent = ref _resourceAspect.Object.Get(entity);

                var asset = objectComponent.Value;
                var gameObject = asset is Component component
                    ? component.gameObject
                    : asset as GameObject;
                
                if(gameObject == null) continue;

                var isSelfTarget = _resourceAspect.Target.Has(entity);
                var converter = gameObject.GetComponent<ILeoEcsMonoConverter>();
                if (!isSelfTarget && converter is { AutoCreate: true })
                {
                    gameObject.SetActive(true); 
                    continue;
                }

                gameObject.ConvertGameObjectToEntity(_world, entity);
                gameObject.SetActive(true);
            }

        }

    }
    
    
}