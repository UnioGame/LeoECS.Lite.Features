namespace Game.Code.Configuration.Runtime.Ability.Description
{
    using System;
    using System.Collections.Generic;
    using Services.AbilityLoadout.Data;
    using Sirenix.OdinInspector;
#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    using UnityEngine;

    [ValueDropdown("@Game.Code.Configuration.Runtime.Ability.Description.AbilityId.GetAbilityIds()")]
    [Serializable]
    public struct AbilityId
    {
        public static readonly AbilityId None = (AbilityId)(-1);
        
        [SerializeField]
        private int _id;

        public int Id => _id;

        public static implicit operator int(AbilityId abilityId)
        {
            return abilityId.Id;
        }

        public static explicit operator AbilityId(int abilityId)
        {
            return new AbilityId
            {
                _id = abilityId
            };
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is AbilityId abilityId)
                return abilityId.Id == Id;

            return false;
        }

        public static AbilityRecord GetAbilityRecord(int abilityId)
        {
#if UNITY_EDITOR
            foreach (var recordValue in GetAbilityRecords())
            {
                var record = recordValue.Value;
                if(record.id == abilityId)
                    return record;
            }
#endif
            return default;
        }
        
        public static IEnumerable<ValueDropdownItem<AbilityRecord>> GetAbilityRecords()
        {
#if UNITY_EDITOR
            var abilityDatabase = AssetEditorTools.GetAsset<AbilityDataBase>();
            foreach (var abilityRecord in abilityDatabase.abilities)
            {
                var asset = abilityRecord.ability.EditorValue;
                
                if(asset == null) continue;
                var name = asset.data.visualDescription.name;
                name = string.IsNullOrEmpty(name) ? asset.name : name;
                
                yield return new ValueDropdownItem<AbilityRecord>()
                {
                    Value = abilityRecord,
                    Text = name
                };
            }
#endif
            yield break;
        }
        
        public static IEnumerable<ValueDropdownItem<AbilityId>> GetAbilityIds()
        {
#if UNITY_EDITOR
            var abilityDatabase = AssetEditorTools.GetAsset<AbilityDataBase>();
            foreach (var abilityRecord in abilityDatabase.abilities)
            {
                var asset = abilityRecord.ability.EditorValue;
                if(asset == null) continue;
                var name = asset.data.visualDescription.name;
                name = string.IsNullOrEmpty(name) ? asset.name : name;
                
                yield return new ValueDropdownItem<AbilityId>()
                {
                    Value = (AbilityId)abilityRecord.id,
                    Text = name
                };
            }
#endif
            yield break;
        }
    }
}