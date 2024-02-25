namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using System.Runtime.CompilerServices;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Ability Id Data", menuName = "Game/Configurations/Arena/Abilities/Ability Type Id Data", order = 0)]
    public class AbilityCategoryIdData : ScriptableObject
    {
        public AbilityCategory defaultCategory = new AbilityCategory();

        [InlineProperty] public AbilityCategory[] categoryIds = Array.Empty<AbilityCategory>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AbilityCategory GetInfo(int id)
        {
            if (id < 0 || id >= categoryIds.Length)
                return defaultCategory;

            return categoryIds[id];
        }
    }
}