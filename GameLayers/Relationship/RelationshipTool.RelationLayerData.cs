namespace Game.Code.GameLayers.Relationship
{
    using System;
    using Layer;

    public struct RelationLayerData : IEquatable<RelationLayerData>
    {
        public readonly LayerId Target;
        public readonly RelationshipId RelationshipId;
        public readonly int Hash;

        public RelationLayerData(LayerId target, RelationshipId relationshipId)
        {
            Target = target;
            RelationshipId = relationshipId;
            Hash = HashCode.Combine(Target, RelationshipId);
        }

        public bool Equals(RelationLayerData data) => 
            data.Target == Target && data.RelationshipId == RelationshipId;

        public override int GetHashCode() => Hash;
    }
}