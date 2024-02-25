namespace Game.Code.Configuration.Runtime.Ability
{
    using System;
    using Description;
    using Services.AbilityLoadout.Data;
    using Sirenix.OdinInspector;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    [Serializable]
    public struct AbilityCell
    {
        public bool IsDefault;
        
        [InlineButton(nameof(PingAbility), "Ping")]
        public AbilityId AbilityId;
        public AbilitySlotId SlotId;
        
        public AbilityCell(AbilityId abilityId, AbilitySlotId slotId, bool isDefault = false)
        {
            AbilityId = abilityId;
            SlotId = slotId;
            IsDefault = isDefault;
        }


        private void PingAbility()
        {
#if UNITY_EDITOR
            var record = AbilityId.GetAbilityRecord(AbilityId);
            if (record == null) return;
            var asset = record.ability.EditorValue;
            if (asset == null) return;
            asset.PingInEditor();
#endif
        }
     
    }
}