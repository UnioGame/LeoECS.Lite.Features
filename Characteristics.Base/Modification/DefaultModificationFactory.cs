namespace Game.Ecs.Characteristics.Base.Modification
{
    using System;

    [Serializable]
    public class DefaultModificationFactory<TModification> : IModificationFactory
        where TModification :ModificationHandler, new()
    {
        public float defaultValue;
        public bool allowedSummation = false;
        public bool isPercent = false;
        public bool isMaxValueModification = false;
        public bool revertSign = false;
        
        public Type TargetType => typeof(TModification);
        
        public virtual ModificationHandler Create(float value)
        {
            if(revertSign) value = -value;
            
            var targetValue = value == 0 && !isPercent ? defaultValue : value;
            
            return new TModification()
            {
                allowedSummation = allowedSummation,
                value = targetValue,
                isPercent = isPercent,
                isMaxLimitModification = isMaxValueModification,
            };
        }
    }
}