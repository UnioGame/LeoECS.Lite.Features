namespace Game.Code.Services.AbilityLoadout.Data
{
    using System;
    using System.Collections.Generic;
    using Ability.Data;
    using Ability.Data.Arena;
    using Sirenix.OdinInspector;

    [Serializable]
    public class AbilityData
    {
        [ValueDropdown(nameof(GetAbilitiesSlots))]
        public int slotType;
        public int unlockLevel;
        public bool isDefault;
        public bool isHidden;
        public bool isBlock;
		
        private IEnumerable<ValueDropdownItem<int>> GetAbilitiesSlots()
        {
            foreach (var slotId in AbilitySlotId.GetAbilitySlotIds())
                yield return slotId;
        }
    }
}