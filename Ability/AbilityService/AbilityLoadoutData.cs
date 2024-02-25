namespace Game.Code.Services.AbilityLoadout
{
    using System;
    using Ability.Data;
    using Data;

    [Serializable]
    public class AbilityLoadoutData
    {
        public AbilityDataBase abilityDataBase;
        public AbilitySlotsData abilitySlotMap;
        public AbilityRarityData abilityRarityData;
    }
}