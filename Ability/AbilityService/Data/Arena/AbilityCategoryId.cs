
namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    using UnityEngine;


    /// <summary>
    /// Shelf Category Id
    /// </summary>
    [Serializable]
    [ValueDropdown("@Game.Code.Services.Ability.Data.Arena.AbilityCategoryId.GetIds()")]
    public struct AbilityCategoryId
    {
        public int value;

        #region statis editor data

        private static AbilityCategoryIdData _abilityCategoryMap;

        public static IEnumerable<ValueDropdownItem<AbilityCategoryId>> GetIds()
        {
#if UNITY_EDITOR
            _abilityCategoryMap ??= AssetEditorTools.GetAsset<AbilityCategoryIdData>();
            var map = _abilityCategoryMap;
            if (map == null)
            {
                yield return new ValueDropdownItem<AbilityCategoryId>()
                {
                    Text = "EMPTY",
                    Value = (AbilityCategoryId)0,
                };
                yield break;
            }

            foreach (var slot in map.categoryIds)
            {
                yield return new ValueDropdownItem<AbilityCategoryId>()
                {
                    Text = slot.title,
                    Value = (AbilityCategoryId) slot.id,
                };
            }
#endif
            yield break;
        }

        public static string GetName(AbilityCategoryId slotId)
        {
#if UNITY_EDITOR
            var slots = GetIds();
            var slot = slots
                .FirstOrDefault(x => x.Value == slotId);
            var slotName = slot.Text;
            return string.IsNullOrEmpty(slotName) ? string.Empty : slotName;
#endif
            return string.Empty;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            _abilityCategoryMap = null;
        }


        #endregion

        public static implicit operator int(AbilityCategoryId v)
        {
            return v.value;
        }

        public static explicit operator AbilityCategoryId(int v)
        {
            return new AbilityCategoryId {value = v};
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public AbilityCategoryId FromInt(int data)
        {
            value = data;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is AbilityCategoryId mask)
                return mask.value == value;

            return false;
        }
    }
}