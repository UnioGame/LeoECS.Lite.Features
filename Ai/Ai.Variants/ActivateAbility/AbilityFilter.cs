namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Sirenix.OdinInspector;

    [Serializable]
    public struct AbilityFilter
    {
        public bool UsePriority;
        
        /// <summary>
        /// Приоритет выбора. Первые элементы более приоритетны остальным
        /// </summary>
        [ShowIf(nameof(UsePriority))]
        public CategoryPriority[] Priorities;

        /// <summary>
        /// ability slot number
        /// </summary>
        public int Slot;
    }
}