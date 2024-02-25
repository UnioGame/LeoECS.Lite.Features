namespace Game.Ecs.Characteristics.Base.Modification
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public abstract class ModificationHandler
    {
        [FormerlySerializedAs("_value")] 
        [SerializeField]
        public float value;
        
        [FormerlySerializedAs("_isPercent")] 
        [SerializeField]
        public bool isPercent;
        
        [FormerlySerializedAs("_allowedSummation")] 
        [SerializeField]
        public bool allowedSummation = true;

        [SerializeField]
        public bool isMaxLimitModification;

        protected virtual Modification Modification => Create();

        public abstract void AddModification(EcsWorld world,int sourceEntity, int destinationEntity);
        
        public abstract void RemoveModification(EcsWorld world,int sourceEntity, int destinationEntity);

        public virtual Modification Create()
        {
            return new Modification(value, isPercent,allowedSummation,isMaxLimitModification);
        }
    }
}