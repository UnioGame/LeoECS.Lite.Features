namespace Game.Code.Animations
{
    using Unity.IL2CPP.CompilerServices;
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif


    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    [ValueDropdown("@Game.Code.Animations.AnimationClipId.GetIds()")]
    public struct AnimationClipId
    {
        [SerializeField]
        public string id;

        public string Id => id;

        public static implicit operator string(AnimationClipId abilityId) => abilityId.Id;

        public static explicit operator AnimationClipId(string abilityId) => new AnimationClipId { id = abilityId };

        public override string ToString() => id;

        public override int GetHashCode() => string.IsNullOrEmpty(id)
            ? 0
            : id.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is AnimationClipId abilityId)
                return abilityId.Id == Id;

            return false;
        }

        public static IEnumerable<ValueDropdownItem<AnimationClipId>> GetIds()
        {
#if UNITY_EDITOR

            var configuration = AssetEditorTools.GetAsset<AnimationClipIdConfiguration>();
            foreach (var id in configuration.ids)
            {
                yield return new
                    ValueDropdownItem<AnimationClipId>()
                    {
                        Text = id,
                        Value = (AnimationClipId)id
                    };
            }

#endif

            yield break;
        }

    }
}