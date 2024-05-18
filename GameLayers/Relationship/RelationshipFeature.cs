namespace Game.Ecs.GameLayers.Relationship
{
    using System;
    using Code.GameLayers.Layer;
    using Code.GameLayers.Relationship;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Relationship feature", fileName = "Relationship feature")]
    [Serializable]
    public sealed class RelationshipFeature : BaseLeoEcsFeature
    {
        [SerializeField]
        private RelationshipIdMap _relationshipIdMap;
        [SerializeField]
        private RelationshipId _selfRelationship;
    
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new RelationshipToolsSystem(_relationshipIdMap, _selfRelationship));
            return UniTask.CompletedTask;
        }
    }
}
