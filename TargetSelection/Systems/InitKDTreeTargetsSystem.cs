using DataStructures.ViliWonka.KDTree;
using Game.Ecs.TargetSelection.Components;
using Unity.Mathematics;

namespace Game.Ecs.TargetSelection.Systems
{
    using System;
    using System.Linq;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;


#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitKDTreeTargetsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _kdDataFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _kdDataFilter = _world
                .Filter<KDTreeDataComponent>()
                .Inc<KDTreeComponent>()
                .Inc<KDTreeQueryComponent>()
                .End();
            
            InitKDTreeTargets();
        }

        public void Run(IEcsSystems systems)
        {
            if (_kdDataFilter.GetEntitiesCount() == 0)
                InitKDTreeTargets();
        }
        
        private void InitKDTreeTargets()
        {
            var treeEntity = _world.NewEntity();

            ref var treeDataComponent = ref _world.AddComponent<KDTreeDataComponent>(treeEntity);
            ref var treeComponent = ref _world.AddComponent<KDTreeComponent>(treeEntity);
            ref var radiusQueryComponent = ref _world.AddComponent<KDTreeQueryComponent>(treeEntity);

            var treeData = new float3[TargetSelectionData.MaxAgents];
            var tree = new KDTree(treeData, TargetSelectionData.MaxTargets);
            tree.Build(treeData, TargetSelectionData.MaxAgents, TargetSelectionData.MaxTargets);

            treeComponent.Value = tree;

            var radiusQuery = new KDQuery();
            radiusQueryComponent.Value = radiusQuery;
        }
    }
}