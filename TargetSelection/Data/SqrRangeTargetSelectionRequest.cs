using System;
using Game.Code.GameLayers.Category;
using Game.Code.GameLayers.Layer;
using Game.Code.GameLayers.Relationship;
using Leopotam.EcsLite;
using UnityEngine.Serialization;

namespace Game.Ecs.TargetSelection
{
    [Serializable]
    public struct SqrRangeTargetSelectionRequest
    {
        public bool Processed;
        public float Radius;
        public CategoryId Category;
        public RelationshipId Relationship;
        public LayerId SourceLayer;
        public EcsPackedEntity Target;
    }
}