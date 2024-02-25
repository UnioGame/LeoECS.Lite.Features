namespace Game.Ecs.Characteristics.Base.Modification
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public struct Modification
    {
        public float baseValue;
        public int counter;
        public bool isPercent;
        public bool isMaxLimitModification;
        public bool allowedSummation;

        public Modification(
            float modificationValue, 
            bool isPercent,
            bool allowedSummation, 
            bool isMaxLimitModification,
            int counter = 1)
        {
            this.baseValue = modificationValue;
            this.isPercent = isPercent;
            this.allowedSummation = allowedSummation;
            this.isMaxLimitModification = isMaxLimitModification;
            this.counter = counter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IncreaseCounter()
        {
            counter++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DecreaseCounter()
        {
            counter--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetValue()
        {
            return baseValue * counter;
        }
    }
}