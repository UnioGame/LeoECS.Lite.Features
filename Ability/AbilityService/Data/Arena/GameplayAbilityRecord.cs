namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using Sirenix.OdinInspector;

    [Serializable]
    public class GameplayAbilityRecord
    {
        public AbilityCategoryId categoryId;
        public bool isPassive = false;
        [HideLabel]
        [InfoBox("Ability assets by level", InfoMessageType.None)]
        [ShowInInspector]
        [TableList]
        public AbilityRef[] abilityAssets = Array.Empty<AbilityRef>();
    }
}