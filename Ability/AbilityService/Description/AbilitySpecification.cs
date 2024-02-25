namespace Game.Code.Configuration.Runtime.Ability.Description
{
    using System;
    using Ecs.Cooldown;
    using GameLayers.Category;
    using GameLayers.Relationship;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class AbilitySpecification
    {
        [FormerlySerializedAs("_cooldown")] 
        [SerializeField]
        public float cooldown;
        
        [FormerlySerializedAs("_cooldownType")] 
        [SerializeField]
        public CooldownType cooldownType = CooldownType.Cooldown;

        [FormerlySerializedAs("_radius")] 
        [SerializeField]
        public float radius;

        [FormerlySerializedAs("_relationshipId")]
        [SerializeField]
        [RelationshipIdMask]
        public RelationshipId relationshipId = (RelationshipId)~0;

        [FormerlySerializedAs("_categoryId")]
        [SerializeField]
        [CategoryIdMask]
        public CategoryId categoryId = (CategoryId)~0;
        
        public float Cooldown
        {
            get
            {
                if (cooldownType != CooldownType.Speed) return cooldown;
                if (Mathf.Approximately(cooldown, 0f)) return 0;
                return 1.0f / cooldown;
            }
        }

        public float Radius => radius;

        public RelationshipId RelationshipId => relationshipId;

        public CategoryId CategoryId => categoryId;
    }
}