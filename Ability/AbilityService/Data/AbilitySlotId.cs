namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    using UnityEngine;

    [Serializable]
    [ValueDropdown("@Game.Code.Services.AbilityLoadout.Data.AbilitySlotId.GetAbilitySlots()", IsUniqueList = true,
        DropdownTitle = "Ability Slot")]
    public struct AbilitySlotId
    {
        public static readonly AbilitySlotId EmptyAbilitySlot = (AbilitySlotId)(-1);
        public static readonly AbilitySlotId DefaultAbilitySlot = (AbilitySlotId)(0);

        [SerializeField] public int _value;

        #region statis editor data

        private static AbilitySlotsData _abilitySlotsAsset;

        public static IEnumerable<ValueDropdownItem<int>> GetAbilitySlotIds()
        {
            foreach (var slot in GetAbilitySlots())
            {
                yield return new ValueDropdownItem<int>()
                {
                    Text = slot.Text,
                    Value = slot.Value,
                };
            }
        }

    public static IEnumerable<ValueDropdownItem<AbilitySlotId>> GetAbilitySlots()
        {
#if UNITY_EDITOR
            _abilitySlotsAsset ??= AssetEditorTools.GetAsset<AbilitySlotsData>();
            var slotMap = _abilitySlotsAsset;
            if (slotMap != null)
            {
                foreach (var slot in slotMap.slots)
                {
                    yield return new ValueDropdownItem<AbilitySlotId>()
                    {
                        Text = slot.name,
                        Value = (AbilitySlotId) slot.id,
                    };
                }
            }
            
            yield return new ValueDropdownItem<AbilitySlotId>()
            {
                Text = "EMPTY",
                Value = EmptyAbilitySlot,
            };
#endif
            yield break;
        }
        
        public static string GetSlotName(AbilitySlotId slotId)
        {
#if UNITY_EDITOR
            var slots = GetAbilitySlots();
            var slot = slots
                .FirstOrDefault(x => x.Value == slotId);
            var slotName = slot.Text;
            return string.IsNullOrEmpty(slotName) ? string.Empty : slotName ;
#endif
            return string.Empty;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            _abilitySlotsAsset = null;
        }

                
        #endregion

        public static implicit operator int(AbilitySlotId v)
        {
            return v._value;
        }

        public static explicit operator AbilitySlotId(int v)
        {
            return new AbilitySlotId() { _value = v };
        }

        public override string ToString() => _value.ToString();

        public override int GetHashCode() => _value;

        public AbilitySlotId FromInt(int data)
        {
            _value = data;
            
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is AbilitySlotId mask)
                return mask._value == _value;
            
            return false;
        }
	}
}